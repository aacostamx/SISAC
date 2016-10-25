//------------------------------------------------------------------------
// <copyright file="NationalJetFuelReconcileInvoiceController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Process.Controllers
{
    using AutoMapper;
    using Business.Dto.Enums;
    using Business.Dto.Process;
    using Business.Dto.Security;
    using Business.ExceptionBusiness;
    using Business.Process;
    using Business.Security;
    using Helpers;
    using Models.Enums;
    using Models.VO.Process;
    using Newtonsoft.Json;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Web.Mvc;
    using Web.Controllers;
    using MvcSiteMapProvider;


    /// <summary>
    /// NationalJetFuelReconcileInvoiceController class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    public class NationalJetFuelReconcileInvoiceController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NationalJetFuelReconcileInvoiceController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = Resource.ReconcileInvoices;

        /// <summary>
        /// The national invoice control business
        /// </summary>
        private readonly INationalJetFuelInvoiceControlBusiness invoiceControlBusiness;

        /// <summary>
        /// The page report
        /// </summary>
        private readonly IPageReportBusiness pageReportBusiness;

        /// <summary>
        /// The invoice detail business/
        /// </summary>
        private readonly INationalJetFuelInvoiceDetailBusiness invoiceDetailBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelReconcileInvoiceController"/> class.
        /// </summary>
        /// <param name="invoiceControlBusiness">The invoice control business.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        /// <param name="invoiceDetailBusiness">The invoice detail business.</param>
        public NationalJetFuelReconcileInvoiceController(
            INationalJetFuelInvoiceControlBusiness invoiceControlBusiness,
            IPageReportBusiness pageReportBusiness,
            INationalJetFuelInvoiceDetailBusiness invoiceDetailBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);

            this.invoiceControlBusiness = invoiceControlBusiness;
            this.pageReportBusiness = pageReportBusiness;
            this.invoiceDetailBusiness = invoiceDetailBusiness;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NTLJETREC-IDX")]
        public ActionResult Index()
        {
            var date = string.Empty;
            var reconcileVO = new List<NationalJetFuelReconcileControlVO>();

            date = MonthYear();
            reconcileVO = Mapper.Map<List<NationalJetFuelReconcileControlVO>>(this.invoiceControlBusiness.GetTopNationalJetFuelReconcileControl(date));
            calculatePTC(reconcileVO);

            return this.View(reconcileVO);
        }

        /// <summary>
        /// Monthes the year.
        /// </summary>
        /// <returns></returns>
        private string MonthYear()
        {
            var date = string.Empty;

            try
            {
                var month = DateTime.Now.Month.ToString("d2");
                var year = DateTime.Now.Year.ToString();
                year = year.Substring(year.Length - 2);
                date = month + year;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }

            return date;
        }

        /// <summary>
        /// Advances the search.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLJETREC-SEARCH")]
        public ActionResult AdvanceSearch()
        {
            try
            {

            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View();
        }

        /// <summary>
        /// Searches the national invoice.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        public ContentResult SearchNationalInvoice(RemittanceSearchVO search)
        {
            var totalRows = 0;
            var json = string.Empty;
            var result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            var reconcileCTRL = new List<NationalJetFuelReconcileControlVO>();
            var searchDto = new RemittanceSearchDto();

            try
            {
                searchDto = Mapper.Map<RemittanceSearchDto>(search);
                reconcileCTRL = Mapper.Map<List<NationalJetFuelReconcileControlVO>>(this.invoiceDetailBusiness.GetSearchInvoiceControlDetail(searchDto).ToList());
                calculatePTC(reconcileCTRL);
                totalRows = reconcileCTRL != null && reconcileCTRL.Count() > 0 ? reconcileCTRL.FirstOrDefault().TotalRows : 0;
                jsonConvert.total = totalRows;
                jsonConvert.rows = reconcileCTRL;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                result = this.Content(json);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }

            return result;
        }

        /// <summary>
        /// Nationals the jet fuel reconcile process.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLJETREC-PROC")]
        public ActionResult NationalJetFuelReconcileProcess(NationalJetFuelReconcileControlVO process)
        {
            var reconcileVO = new NationalJetFuelReconcileControlVO();

            try
            {
                if (string.IsNullOrEmpty(process.RemittanceID) || string.IsNullOrEmpty(process.MonthYear) || string.IsNullOrEmpty(process.Period))
                {
                    return this.View("NotFound");
                }
                reconcileVO = Mapper.Map<NationalJetFuelReconcileControlVO>(this.invoiceControlBusiness.GetInvoiceControl(new NationalJetFuelInvoiceControlDto { RemittanceID = process.RemittanceID, MonthYear = process.MonthYear, Period = process.Period }));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(reconcileVO);
        }

        /// <summary>
        /// Nationals the jet fuel reconcile manual process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLJETREC-RECON")]
        public ActionResult NationalJetFuelReconcileManualProcess(NationalJetFuelReconcileControlVO process)
        {
            var reconcileVO = new NationalJetFuelReconcileControlVO();

            try
            {
                if (string.IsNullOrEmpty(process.RemittanceID) || string.IsNullOrEmpty(process.MonthYear) || string.IsNullOrEmpty(process.Period))
                {
                    return this.View("NotFound");
                }
                reconcileVO = Mapper.Map<NationalJetFuelReconcileControlVO>(this.invoiceControlBusiness.GetInvoiceControl(new NationalJetFuelInvoiceControlDto { RemittanceID = process.RemittanceID, MonthYear = process.MonthYear, Period = process.Period }));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(reconcileVO);
        }



        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="process">The jet fuel process.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "NTLJETREC-START")]
        public int StartProcess(NationalJetFuelReconcileControlVO process)
        {
            var start = 0;
            var processDto = new NationalJetFuelInvoiceControlDto();

            if (string.IsNullOrEmpty(process.RemittanceID) || string.IsNullOrEmpty(process.MonthYear) || string.IsNullOrEmpty(process.Period))
            {
                return start;
            }

            try
            {
                process.TypeProcess = process.ProcessPending == true ? NationalReconcileTypeProcessVO.NationalReconcileProcessPending : NationalReconcileTypeProcessVO.NationalReconcileProcessAll;
                process.ProcessedByUserName = this.User.Identity.Name;
                processDto = Mapper.Map<NationalJetFuelInvoiceControlDto>(process);
                start = this.invoiceControlBusiness.StartNationalReconcileProcess(processDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return start;
        }

        /// <summary>
        /// Reverts the process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "NTLJETREC-REVERT")]
        public int RevertProcess(NationalJetFuelReconcileControlVO process)
        {
            int revert = 0;

            if (string.IsNullOrEmpty(process.RemittanceID) || string.IsNullOrEmpty(process.MonthYear) || string.IsNullOrEmpty(process.Period))
            {
                return revert;
            }

            try
            {
                var processDto = new NationalJetFuelInvoiceControlDto { RemittanceID = process.RemittanceID, MonthYear = process.MonthYear, Period = process.Period, TypeProcess = NationalReconcileTypeProcessDto.NationalReconcileProcessRevert };
                revert = this.invoiceControlBusiness.RevertReconcileProcess(processDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
                revert = 0;
            }
            return revert;
        }


        /// <summary>
        /// Reverts the manual process.
        /// </summary>
        /// <param name="RemittanceID">The remittance identifier.</param>
        /// <param name="MonthYear">The month year.</param>
        /// <param name="Period">The period.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "NTLJETREC-REVERT")]
        public int RevertManualProcess(string RemittanceID, string MonthYear, string Period)
        {
            int revert = 0;

            try
            {
                if (string.IsNullOrEmpty(RemittanceID) || string.IsNullOrEmpty(MonthYear) || string.IsNullOrEmpty(Period))
                {
                    return 0;// this.View("NotFound");
                }

                var processDto = new NationalJetFuelInvoiceControlDto { RemittanceID = RemittanceID, MonthYear = MonthYear, Period = Period };
                revert = this.invoiceControlBusiness.RevertManualReconcileProcess(processDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return revert;
        }

        /// <summary>
        /// Exports the reconciliation detail.
        /// </summary>
        /// <param name="RemittanceID">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLJETREC-PRINTREP")]
        public ActionResult ExportReconciliationDetail(string RemittanceID, string monthYear, string period)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("NationalJetFuelReconciliationDetail");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(RemittanceID.ToString()) && !string.IsNullOrEmpty(monthYear.ToString()) && !string.IsNullOrEmpty(period.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("RemittanceID", RemittanceID.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("MonthYear", monthYear.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("Period", period.ToString(), false)
                        });

                    model.PageSourceUrl = this.Url.Action("Index", "NationalJetFuelReconcileInvoice", new { Area = "Process" });
                    return this.View("Report/ViewReport", model);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Exports the invoice detail.
        /// </summary>
        /// <param name="RemittanceID">The remittance identifier.</param>
        /// <param name="MonthYear">The month year.</param>
        /// <param name="Period">The period.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLJETREC-PRINTREP")]
        public ActionResult ExportInvoiceDetail(string RemittanceID, string MonthYear, string Period)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("DownloadNonReconciledInvoiceDetail");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(RemittanceID.ToString()) && !string.IsNullOrEmpty(MonthYear.ToString()) && !string.IsNullOrEmpty(Period.ToString()))
                {
                    SetSiteMapValues(RemittanceID, MonthYear, Period);
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("RemittanceID", RemittanceID.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("MonthYear", MonthYear.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("Period", Period.ToString(), false)
                        });

                    model.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("JetFuelReconcileInvoice_Manual").Url;
                    return this.View("Report/ViewReport", model);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.RedirectToAction("NationalJetFuelReconcileManualProcess", new
            {
                RemittanceID = RemittanceID,
                MonthYear = MonthYear,
                Period = Period
            });
        }

        /// <summary>
        /// Exports the cost detail.
        /// </summary>
        /// <param name="RemittanceID">The remittance identifier.</param>
        /// <param name="MonthYear">The month year.</param>
        /// <param name="Period">The period.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLJETREC-PRINTREP")]
        public ActionResult ExportCostDetail(string RemittanceID, string MonthYear, string Period)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("DownloadNonReconciledCostGroup");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(RemittanceID.ToString()) && !string.IsNullOrEmpty(MonthYear.ToString()) && !string.IsNullOrEmpty(Period.ToString()))
                {
                    SetSiteMapValues(RemittanceID, MonthYear, Period);
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("RemittanceID", RemittanceID.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("MonthYear", MonthYear.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("Period", Period.ToString(), false)
                        });

                    model.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("JetFuelReconcileInvoice_Manual").Url;
                    return this.View("Report/ViewReport", model);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.RedirectToAction("NationalJetFuelReconcileManualProcess", new
            {
                RemittanceID = RemittanceID,
                MonthYear = MonthYear,
                Period = Period
            });
        }

        /// <summary>
        /// Exports the load log.
        /// </summary>
        /// <param name="RemittanceID">The remittance identifier.</param>
        /// <param name="MonthYear">The month year.</param>
        /// <param name="Period">The period.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NTLJETREC-PRINTREP")]
        public ActionResult ExportLoadLog(string RemittanceID, string MonthYear, string Period)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("NationalJetFuelReconciliationManualLoadLog");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(RemittanceID.ToString()) && !string.IsNullOrEmpty(MonthYear.ToString()) && !string.IsNullOrEmpty(Period.ToString()))
                {
                    SetSiteMapValues(RemittanceID, MonthYear, Period);
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("RemittanceID", RemittanceID.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("MonthYear", MonthYear.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("Period", Period.ToString(), false)
                        });

                    model.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("JetFuelReconcileInvoice_Manual").Url;
                    return this.View("Report/ViewReport", model);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.RedirectToAction("NationalJetFuelReconcileManualProcess", new
            {
                RemittanceID = RemittanceID,
                MonthYear = MonthYear,
                Period = Period
            });
        }

        /// <summary>
        /// Sets the site map values.
        /// </summary>
        /// <param name="RemittanceID">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        private static void SetSiteMapValues(string RemittanceID, string monthYear, string period)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("JetFuelReconcileInvoice_Manual");
                if (node != null)
                {
                    node.RouteValues["RemittanceID"] = RemittanceID;
                    node.RouteValues["MonthYear"] = monthYear;
                    node.RouteValues["Period"] = period;
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
                Trace.TraceError(exception.Message, exception);
            }
        }

        /// <summary>
        /// Calculates the PTC.
        /// </summary>
        /// <param name="reconcileCTRL">The reconcile control.</param>
        private static void calculatePTC(List<NationalJetFuelReconcileControlVO> reconcileCTRL)
        {
            try
            {
                for (int i = 0; i < reconcileCTRL.Count; i++)
                {
                    if (reconcileCTRL[i].CountRecords.HasValue && reconcileCTRL[i].CountReconciledRecords.HasValue)
                    {
                        var ptc = ((double)reconcileCTRL[i].CountReconciledRecords / (double)reconcileCTRL[i].CountRecords) * 100;
                        reconcileCTRL[i].PctReconciledRecords = Math.Round(ptc, 2).ToString() + "%";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
        }
    }
}