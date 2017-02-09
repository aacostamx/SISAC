//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Process
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Models.Enums;
    using Newtonsoft.Json;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Process;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Process;
    using VOI.SISAC.Web.Resources;
    using VOI.SISAC.Web.Services;

    /// <summary>
    /// Manage Invoice Controller
    /// </summary>
    public class NationalJetFuelInvoiceController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NationalJetFuelInvoiceController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = "National Jet Fuel Invoice";

        /// <summary>
        /// The national invoice control business
        /// </summary>
        private readonly INationalJetFuelInvoiceControlBusiness invoiceControlBusiness;

        /// <summary>
        /// The invoice policy business
        /// </summary>
        private readonly INationalJetFuelInvoicePolicyBusiness invoicePolicyBusiness;

        /// <summary>
        /// The policy service
        /// </summary>
        private readonly IPolicyService policyService;

        /// <summary>
        /// The generic business
        /// </summary>
        private readonly IGenericCatalogBusiness genericBusiness;

        /// <summary>
        /// The page report
        /// </summary>
        private readonly IPageReportBusiness pageReportBusiness;

        /// <summary>
        /// The invoice detail business
        /// </summary>
        private readonly INationalJetFuelInvoiceDetailBusiness invoiceDetailBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceController" /> class.
        /// </summary>
        /// <param name="invoiceControlBusiness">The invoice control business.</param>
        /// <param name="genericBusiness">The generic business.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        /// <param name="invoiceDetailBusiness">The invoice detail business.</param>
        /// <param name="policyService">The policy service.</param>
        /// <param name="invoicePolicyBusiness">The invoice policy business.</param>
        public NationalJetFuelInvoiceController(
            INationalJetFuelInvoiceControlBusiness invoiceControlBusiness,
            IGenericCatalogBusiness genericBusiness,
            IPageReportBusiness pageReportBusiness,
            INationalJetFuelInvoiceDetailBusiness invoiceDetailBusiness,
            IPolicyService policyService,
            INationalJetFuelInvoicePolicyBusiness invoicePolicyBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);

            this.invoiceControlBusiness = invoiceControlBusiness;
            this.pageReportBusiness = pageReportBusiness;
            this.genericBusiness = genericBusiness;
            this.invoiceDetailBusiness = invoiceDetailBusiness;
            this.policyService = policyService;
            this.invoicePolicyBusiness = invoicePolicyBusiness;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NTLJETINVO-IDX")]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Advances the search remittances invoices.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NTLJETINVO-SEARCH")]
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
        /// Indexes this instance.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        public ContentResult GetNationalInvoiceData(NationalFuelInvoiceControlVO search)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            IList<NationalJetFuelInvoiceControlDto> invoiceControl = new List<NationalJetFuelInvoiceControlDto>();
            IList<NationalFuelInvoiceControlVO> invoiceControlVO = new List<NationalFuelInvoiceControlVO>();

            try
            {
                totalRows = this.invoiceControlBusiness.CountNationalInvoiceRecords();
                invoiceControl = this.invoiceControlBusiness.GetNationalInvoicePaginated(search.PageNumber, search.PageSize);
                invoiceControlVO = Mapper.Map<IList<NationalFuelInvoiceControlVO>>(invoiceControl);
                jsonConvert.total = totalRows;
                jsonConvert.rows = invoiceControlVO;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                result = this.Content(json);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return result;
        }

        /// <summary>
        /// Searches the national invoice.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>Content result.</returns>
        public ContentResult SearchNationalInvoice(RemittanceSearchVO search)
        {
            var totalRows = 0;
            var json = string.Empty;
            var result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            var invoiceCTRL = new List<NationalJetFuelInvoiceControlDto>();
            var searchDto = new RemittanceSearchDto();

            try
            {
                //var stringDate = search.MonthYear;
                //search.MonthYear = string.Empty;
                searchDto = Mapper.Map<RemittanceSearchDto>(search);
                //searchDto.MonthYear = this.ParseMonthYearToDate(stringDate);
                invoiceCTRL = this.invoiceDetailBusiness.GetSearchInvoiceControlDetail(searchDto).ToList();
                totalRows = invoiceCTRL != null && invoiceCTRL.Count() > 0 ? invoiceCTRL.FirstOrDefault().TotalRows : 0;
                jsonConvert.total = totalRows;
                jsonConvert.rows = invoiceCTRL;
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
        /// Shows the report.
        /// </summary>
        /// <param name="remittanceID">The remittance identifier.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "NTLJETINVO-PRINTREP")]
        public ActionResult ShowInvoiceSummary(string remittanceID, string monthYear, string period)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("NationalJetFuelInvoiceDetail");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(remittanceID.ToString()) && !string.IsNullOrEmpty(monthYear.ToString()) && !string.IsNullOrEmpty(period.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("RemittanceID", remittanceID.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("MonthYear", monthYear.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("Period", period.ToString(), false)
                        });

                    model.PageSourceUrl = this.Url.Action("Index", "NationalJetFuelInvoice", new { Area = "Process" });
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
        /// Get airlines actives
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public JsonResult AirlineComboBox()
        {
            IList<GenericCatalogDto> airlines = new List<GenericCatalogDto>();
            try
            {
                airlines = this.genericBusiness.GetAirlineCatalog();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }

            return this.Json(airlines, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Shows the errors report.
        /// </summary>
        /// <param name="remittanceID">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLJETINVO-PRINTREP")]
        public ActionResult ShowErrorsReport(string remittanceID, string monthYear, string period)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("NationalJetFuelInvoiceDetailError");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(remittanceID.ToString()) && !string.IsNullOrEmpty(monthYear.ToString()) && !string.IsNullOrEmpty(period.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("RemittanceID", remittanceID.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("MonthYear", monthYear.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("Period", period.ToString(), false)
                        });

                    model.PageSourceUrl = this.Url.Action("Index", "NationalJetFuelInvoice", new { Area = "Process" });
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
        /// Shows the detail export report.
        /// </summary>
        /// <param name="remittanceID">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLJETINVO-PRINTREP")]
        public ActionResult ShowDetailExportReport(string remittanceID, string monthYear, string period)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("NationalJetFuelInvoiceDetailExport");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(remittanceID.ToString()) && !string.IsNullOrEmpty(monthYear.ToString()) && !string.IsNullOrEmpty(period.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("RemittanceID", remittanceID.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("MonthYear", monthYear.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("Period", period.ToString(), false)
                        });

                    model.PageSourceUrl = this.Url.Action("Index", "NationalJetFuelInvoice", new { Area = "Process" });
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
        /// Shows the invoice policies report.
        /// </summary>
        /// <param name="remittanceID">The remittance identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NTLJETINVO-PRINTREP")]
        public ActionResult ShowInvoicePoliciesReport(string remittanceID, string monthYear, string period)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("NationalJetFuelInvoiceDetailPolicy");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(remittanceID.ToString()) && !string.IsNullOrEmpty(monthYear.ToString()) && !string.IsNullOrEmpty(period.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("RemittanceID", remittanceID.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("MonthYear", monthYear.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("Period", period.ToString(), false)
                        });

                    model.PageSourceUrl = this.Url.Action("Index", "NationalJetFuelInvoice", new { Area = "Process" });
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
        /// Sends the specified remittance identifier.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLJETINVO-SEND")]
        public ActionResult Send(string remittanceId, string monthYear, string period)
        {
            if (string.IsNullOrWhiteSpace(remittanceId) || string.IsNullOrWhiteSpace(monthYear) || string.IsNullOrWhiteSpace(period))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                string information = string.Empty;
                List<NationalJetFuelInvoicePolicyVO> nationalPolicies = new List<NationalJetFuelInvoicePolicyVO>();
                List<NationalJetFuelInvoicePolicyDto> nationalPoliciesDto = new List<NationalJetFuelInvoicePolicyDto>();
                nationalPolicies = this.policyService.SendNationalInvoicePolicyToService(remittanceId, monthYear, period, ref information).ToList();
                nationalPoliciesDto = Mapper.Map<List<NationalJetFuelInvoicePolicyDto>>(nationalPolicies);
                this.invoicePolicyBusiness.UpdateNationalInvoicePolicyResponse(nationalPoliciesDto);
                this.TempData["OperationSuccess"] = information;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage();
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns>Content result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "NTLJETINVO-DEL")]
        public ContentResult Delete(NationalFuelInvoiceControlVO invoice)
        {
            var delete = 0;
            var json = string.Empty;
            var result = new ContentResult();
            dynamic modal = new ExpandoObject();

            try
            {
                delete = this.invoiceControlBusiness.DeletedNationalInvoicePolicy(new NationalJetFuelInvoiceControlDto() { RemittanceID = invoice.RemittanceId, MonthYear = invoice.MonthYear, Period = invoice.Period });
                var enumInvoice = (NationalInvoiceVO)delete;

                switch (enumInvoice)
                {
                    case NationalInvoiceVO.ErrorOrNotFound:
                        modal.title = Resource.Error;
                        modal.text = Resource.ErrorOrNotFound;
                        modal.type = "error";
                        break;
                    case NationalInvoiceVO.ReadySentPolicy:
                        modal.title = Resource.Error;
                        modal.text = Resource.ReadySentPolicy;
                        modal.type = "error";
                        break;
                    case NationalInvoiceVO.DeletedSucess:
                        modal.title = Resource.Sucess;
                        modal.text = Resource.SuccessDelete;
                        modal.type = "success";
                        break;
                    case NationalInvoiceVO.PendingReconciled:
                        modal.title = Resource.Warning;
                        modal.text = Resource.PendingReconciled;
                        modal.type = "warning";
                        break;
                    default:
                        modal.title = Resource.Error;
                        modal.text = Resource.ErrorOrNotFound;
                        modal.type = "error";
                        break;
                }

                json = json = JsonConvert.SerializeObject(modal, Formatting.None);
                result = this.Content(json);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
            }

            return result;
        }

        /// <summary>
        /// Creates the policy.
        /// </summary>
        /// <param name="invoiceParameters">The invoice control.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "NTLJETINVO-ADD")]
        public ActionResult CreatePolicy(NationalInvoiceControlCreateParameter invoiceParameters)
        {
            if (invoiceParameters == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                NationalFuelInvoiceControlVO invoiceControl = Mapper.Map<NationalFuelInvoiceControlVO>(invoiceParameters);
                long policyResult = this.TryCreatePolicy(invoiceControl);
                switch (policyResult)
                {
                    case 0:
                        this.TempData["ErrorMessage"] = Resource.CreatePolicyInvoiceErrorInProcess;
                        break;
                    case -1:
                        this.TempData["ErrorMessage"] = string.Format(Resource.CreatePolicyInvoiceErrorPolicyExist, '{' + invoiceControl.RemittanceId + ',' + invoiceControl.MonthYear + ',' + invoiceControl.Period + '}');
                        break;
                    case -2:
                        this.TempData["ErrorMessage"] = string.Format(Resource.CreatePolicyInvoiceErrorInRemittance, '{' + invoiceControl.RemittanceId + ',' + invoiceControl.MonthYear + ',' + invoiceControl.Period + '}');
                        break;
                    default:
                        this.TempData["OperationSuccess"] = string.Format(Resource.CreatePolicyInvoiceSuccessful, '{' + invoiceControl.RemittanceId + ',' + invoiceControl.MonthYear + ',' + invoiceControl.Period + '}');
                        break;
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
            catch (ArgumentException)
            {
                this.TempData["ErrorMessage"] = Resource.MonthYearFormatError;
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Tries the create policy.
        /// </summary>
        /// <param name="invoiceControl">The invoice control.</param>
        /// <returns>
        /// -2: Error caused because there are errors in the remittance.
        /// -1: Error. The policy has been created..
        /// 0: An error during the call of the store procedure.
        /// Greater than zero indicates the process was completed successfuly.
        /// </returns>
        private long TryCreatePolicy(NationalFuelInvoiceControlVO invoiceControl)
        {
            NationalJetFuelInvoiceControlDto invoiceControlDto;
            int month;
            int year;

            string monthYearString = invoiceControl.MonthYear;
            //invoiceControl.MonthYear = null;
            invoiceControlDto = Mapper.Map<NationalJetFuelInvoiceControlDto>(invoiceControl);

            if (!int.TryParse(monthYearString.Substring(0, 2), out month))
            {
                throw new ArgumentException();
            }

            if (!int.TryParse(monthYearString.Substring(2, 2), out year))
            {
                throw new ArgumentException();
            }

            if (month < 1 || month > 12)
            {
                throw new ArgumentException();
            }

            //invoiceControlDto.MonthYear = monthYearString;

            int verifyPolicy = this.invoiceControlBusiness.ValidateInvoicePolicy(invoiceControlDto);

            if (verifyPolicy > 0)
            {
                switch (verifyPolicy)
                {
                    case 1:

                        // The remittance has already policies.
                        return -1;
                    case 2:

                        // There are errors in the remittance.
                        return -2;
                    default:

                        // Error during the process.
                        return 0;
                }
            }

            int createResult = this.invoiceControlBusiness.CreatePoliciesForRemittance(invoiceControlDto);

            // 1 - the process creates the policies successfully.
            // 0 - errors during the process.
            return createResult > 0 ? 1 : 0;
        }

        /// <summary>
        /// Parses the month-year to date.
        /// </summary>
        /// <param name="monthyear">The monthyear.</param>
        /// <returns>The month-year in DateTime type.</returns>
        private DateTime ParseMonthYearToDate(string monthyear)
        {
            int month = 0;
            int year = 0;
            int digits = 0;
            DateTime date = new DateTime();

            try
            {
                if (!string.IsNullOrEmpty(monthyear))
                {
                    Int32.TryParse(monthyear.Substring(0, 2), out month);
                    Int32.TryParse(monthyear.Substring(2, 2), out year);

                    if (month >= 1 && month <= 12)
                    {
                        digits = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(0, 2));
                        var fullyear = digits.ToString() + year.ToString();
                        date = new DateTime(int.Parse(fullyear), month, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }

            return date;
        }
    }
}