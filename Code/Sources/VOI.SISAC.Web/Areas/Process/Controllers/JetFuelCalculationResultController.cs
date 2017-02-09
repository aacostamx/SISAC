//------------------------------------------------------------------------
// <copyright file="JetFuelCalculationResultController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Process.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using log4net.Repository.Hierarchy;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Process;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Process;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Calculation result controller
    /// </summary>
    [CustomAuthorize]
    public class JetFuelCalculationResultController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(JetFuelLogErrorController));

        /// <summary>
        /// The generic catalog
        /// </summary>
        private readonly IGenericCatalogBusiness genericCatalog;

        /// <summary>
        /// The generic catalog
        /// </summary>
        private readonly IJetFuelProcessBusiness jetFuelProcess;

        /// <summary>
        /// The controller name
        /// </summary>
        private readonly string controllerName;

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// The page report business
        /// </summary>
        private readonly IPageReportBusiness pageReport;

        /// <summary>
        /// The catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.ViewInternationalFuelCalculationResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelCalculationResultController" /> class.
        /// </summary>
        /// <param name="genericCatalog">The generic catalog.</param>
        /// <param name="jetFuelProcess">The jet fuel process.</param>
        public JetFuelCalculationResultController(IGenericCatalogBusiness genericCatalog, IJetFuelProcessBusiness jetFuelProcess, IPageReportBusiness pageReport)
        {
            this.genericCatalog = genericCatalog;
            this.jetFuelProcess = jetFuelProcess;
            this.pageReport = pageReport;
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName
                );
            this.controllerName = "Jet Fuel Calculation Result";
        }

        /// <summary>
        /// Main view.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpGet]
        [CustomAuthorize(Roles = "INTVIEWRES-IDX")]
        public ActionResult Index(JetFuelCalculationResultParamsVO parameters)
        {
            try
            {
                this.LoadCatalogs();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
            }

            return this.View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        public ActionResult Search(JetFuelCalculationResultParamsVO parameters)
        {
            if (!this.ModelState.IsValid)
            {
                this.ViewBag.ErrorMessage = "Validation Errors";
            }

            return this.View("Index", parameters);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        public ActionResult CreatePolicy(JetFuelCalculationResultParamsVO parameters)
        {
            this.ViewBag.ErrorMessage = "Create policy";
            return this.View("Index", parameters);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "INTVIEWRES-PRINTREP")]
        public ActionResult GroupReport(JetFuelCalculationResultParamsVO parameters)
        {
            //this.ViewBag.ErrorMessage = "Group report";            

            try
            {
                this.LoadCatalogs();
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReport.GetPageReportByPageName("JetFuelProvisionGroup");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                //parametros dinamicos
                string airlineParam = "";
                string stationParam = "";
                string serviceParam = "";
                string providerParam = "";

                ParameterToSplit(parameters, ref airlineParam, ref stationParam, ref serviceParam, ref providerParam);

                ModelState["IsOpenPeriod"].Errors.Clear();
                if (!String.IsNullOrEmpty(reportPath) && !String.IsNullOrEmpty(parameters.PeriodCode) && ModelState.IsValid)
                {
                    return this.RedirectToAction("Grouped", "JetFuelCalculationResult", new { parameters = parameters.PeriodCode, reportPath = reportPath, airlineParam = airlineParam, stationParam = stationParam, serviceParam = serviceParam, providerParam = providerParam });
                }
                else
                {
                    return this.View("Index", parameters);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View("Index", parameters);
            }
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "INTVIEWRES-PRINTREP")]
        public ActionResult DetailedReport(JetFuelCalculationResultParamsVO parameters)
        {
            //this.ViewBag.ErrorMessage = "Detailed report";            

            try
            {
                this.LoadCatalogs();
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReport.GetPageReportByPageName("JetFuelProvisionDetail");

                if (pageReportDto != null)
                    reportPath = pageReportDto.PathReport;

                //parametros dinamicos
                string airlineParam = "";
                string stationParam = "";
                string serviceParam = "";
                string providerParam = "";

                ParameterToSplit(parameters, ref airlineParam, ref stationParam, ref serviceParam, ref providerParam);

                ModelState["IsOpenPeriod"].Errors.Clear();
                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(parameters.PeriodCode) && ModelState.IsValid)
                {
                    return this.RedirectToAction("Detailed", "JetFuelCalculationResult", new { parameters = parameters.PeriodCode, reportPath = reportPath, airlineParam = airlineParam, stationParam = stationParam, serviceParam = serviceParam, providerParam = providerParam });
                }
                else
                {
                    return this.View("Index", parameters);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View("Index", parameters);
            }
        }

        /// <summary>
        /// ErrorReport.
        /// </summary>
        /// <param name="parameters">parameters.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "INTVIEWRES-PRINTREP")]
        public ActionResult ErrorReport(JetFuelCalculationResultParamsVO parameters)
        {
            //this.ViewBag.ErrorMessage = "Detailed report";            

            try
            {
                this.LoadCatalogs();
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReport.GetPageReportByPageName("JetFuelProvisionError");

                if (pageReportDto != null)
                    reportPath = pageReportDto.PathReport;

                //parametros dinamicos
                string airlineParam = "";
                string stationParam = "";
                string serviceParam = "";
                string providerParam = "";

                ParameterToSplit(parameters, ref airlineParam, ref stationParam, ref serviceParam, ref providerParam);

                ModelState["IsOpenPeriod"].Errors.Clear();
                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(parameters.PeriodCode) && ModelState.IsValid)
                {
                    return this.RedirectToAction("WrongLog", "JetFuelCalculationResult", new { parameters = parameters.PeriodCode, reportPath = reportPath, airlineParam = airlineParam, stationParam = stationParam, serviceParam = serviceParam, providerParam = providerParam });
                }
                else
                {
                    return this.View("Index", parameters);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View("Index", parameters);
            }
        }

        /// <summary>
        /// ParameterToSplit
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="airlineParam">The airline parameter.</param>
        /// <param name="stationParam">The station parameter.</param>
        /// <param name="serviceParam">The service parameter.</param>
        /// <param name="providerParam">The provider parameter.</param>
        private static void ParameterToSplit(JetFuelCalculationResultParamsVO parameters, ref string airlineParam, ref string stationParam, ref string serviceParam, ref string providerParam)
        {
            //concatenar parametros airline
            airlineParam = parameters.AirlineCode != null ? string.Join(",", parameters.AirlineCode) : " ";
            
            //concatenar parametros station
            stationParam = parameters.StationCode != null ? string.Join(",", parameters.StationCode) : " ";
           
            //concatenar parametros service
            serviceParam = parameters.ServiceCode != null ? string.Join(",", parameters.ServiceCode) : " ";            

            //concatenar parametros provider
            providerParam = parameters.ProviderNumber != null ? string.Join(",", parameters.ProviderNumber) : " ";            
        }

        /// <summary>
        /// Detailed
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="reportPath"></param>
        /// <returns></returns>
        public ActionResult Detailed(string parameters, string reportPath, string airlineParam, string stationParam, string serviceParam, string providerParam)
        {
            if (string.IsNullOrWhiteSpace(parameters))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.controllerName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReportingServiceViewModel model = new ReportingServiceViewModel
            (
                reportPath,
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                { 
                    new Microsoft.Reporting.WebForms.ReportParameter("PeriodCode", parameters.ToString(), false),
                    new Microsoft.Reporting.WebForms.ReportParameter("AirlineCodes", airlineParam, false),
                    new Microsoft.Reporting.WebForms.ReportParameter("StationCodes", stationParam, false),
                    new Microsoft.Reporting.WebForms.ReportParameter("ServiceCodes", " ", false),
                    new Microsoft.Reporting.WebForms.ReportParameter("ProviderCodes", " ", false)
                }
            );

            model.PageSourceUrl = this.Url.Action("Index", "JetFuelCalculationResult", new { Area = "Process" });
            return View("Report/ViewReport", model);
        }

        /// <summary>
        /// Grouped
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="reportPath">The report path.</param>
        /// <param name="airlineParam">The airline parameter.</param>
        /// <param name="stationParam">The station parameter.</param>
        /// <param name="serviceParam">The service parameter.</param>
        /// <param name="providerParam">The provider parameter.</param>
        /// <returns></returns>
        public ActionResult Grouped(string parameters, string reportPath, string airlineParam, string stationParam, string serviceParam, string providerParam)
        {
            if (string.IsNullOrWhiteSpace(parameters))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.controllerName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReportingServiceViewModel model = new ReportingServiceViewModel
            (
                reportPath,
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                { 
                    new Microsoft.Reporting.WebForms.ReportParameter("PeriodCode", parameters.ToString(), false),
                    new Microsoft.Reporting.WebForms.ReportParameter("AirlineCodes", airlineParam, false),
                    new Microsoft.Reporting.WebForms.ReportParameter("StationCodes", stationParam, false),
                    new Microsoft.Reporting.WebForms.ReportParameter("ServiceCodes", " ", false),
                    new Microsoft.Reporting.WebForms.ReportParameter("ProviderCodes", " ", false) 
                }
            );

            model.PageSourceUrl = this.Url.Action("Index", "JetFuelCalculationResult", new { Area = "Process" });
            return View("Report/ViewReport", model);
        }

        /// <summary>
        /// Wrong Log
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="reportPath">The report path.</param>
        /// <param name="airlineParam">The airline parameter.</param>
        /// <param name="stationParam">The station parameter.</param>
        /// <param name="serviceParam">The service parameter.</param>
        /// <param name="providerParam">The provider parameter.</param>
        /// <returns></returns>
        public ActionResult WrongLog(string parameters, string reportPath, string airlineParam, string stationParam, string serviceParam, string providerParam)
        {
            if (string.IsNullOrWhiteSpace(parameters))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.controllerName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ReportingServiceViewModel model = new ReportingServiceViewModel
            (
                reportPath,
                new List<Microsoft.Reporting.WebForms.ReportParameter>()
                { 
                    new Microsoft.Reporting.WebForms.ReportParameter("PeriodCode", parameters.ToString(), false),
                    new Microsoft.Reporting.WebForms.ReportParameter("AirlineCodes", airlineParam, false),
                    //new Microsoft.Reporting.WebForms.ReportParameter("StationCodes", stationParam, false),
                    //new Microsoft.Reporting.WebForms.ReportParameter("ServiceCodes", " ", false),
                    //new Microsoft.Reporting.WebForms.ReportParameter("ProviderCodes", " ", false) 
                }
            );

            model.PageSourceUrl = this.Url.Action("Index", "JetFuelCalculationResult", new { Area = "Process" });
            return View("Report/ViewReport", model);
        }

        /// <summary>
        /// Gets the periods.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// Json result.
        /// </returns>
        public JsonResult GetDates(string periodCode)
        {
            PeriodVO period;
            if (periodCode == null)
            {
                return this.Json(new PeriodVO(), JsonRequestBehavior.AllowGet);
            }

            period = this.GetDatesByPeriod(periodCode);
            return this.Json(period, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the dates by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// Dates of the period.
        /// </returns>
        private PeriodVO GetDatesByPeriod(string periodCode)
        {
            try
            {
                PeriodVO period = new PeriodVO();
                JetFuelProcessDto process = new JetFuelProcessDto
                {
                    PeriodCode = periodCode
                };

                period = Mapper.Map<JetFuelProcessDto, PeriodVO>(this.jetFuelProcess.FindJetFuelProcess(process));
                return period;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return null;
            }
        }

        /// <summary>
        /// Gets all periods.
        /// </summary>
        /// <returns>List of periods.</returns>
        private List<PeriodVO> GetAllPeriods()
        {
            try
            {
                List<PeriodVO> periods = new List<PeriodVO>();
                periods = Mapper.Map<List<JetFuelProcessDto>, List<PeriodVO>>(this.jetFuelProcess.GetAllJetFuelProcesses().ToList());
                return periods;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return null;
            }
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
        private void LoadCatalogs()
        {
            this.ViewBag.Airline = this.genericCatalog.GetAirlineCatalog();
            this.ViewBag.Airport = this.genericCatalog.GetAirportsCatalog();
            this.ViewBag.Service = this.genericCatalog.GetServiceCatalog()
                .Where(c => c.Id.Contains("-EXT"))
                .ToList();
            this.ViewBag.Provider = this.genericCatalog.GetProviderCatalog();
            var process = this.genericCatalog.GetJetFuelProcessCatalog();
            this.ViewBag.Process = this.genericCatalog.GetJetFuelProcessCatalog();
        }
    }
}