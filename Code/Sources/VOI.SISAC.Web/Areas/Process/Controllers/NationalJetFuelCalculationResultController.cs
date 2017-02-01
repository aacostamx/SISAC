//------------------------------------------------------------------------
// <copyright file="NationalJetFuelCalculationResultController.cs" company="Volaris">
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
    /// Class NationalJetFuelCalculationResultController.
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    [CustomAuthorize]
    public class NationalJetFuelCalculationResultController : BaseController
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
        private readonly INationalJetFuelProcessBusiness nationalJetFuelProcess;

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
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.ViewNationalFuelCalculationResult;


        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelCalculationResultController"/> class.
        /// </summary>
        /// <param name="genericCatalog">The generic catalog.</param>
        /// <param name="nationalJetFuelProcess">The national jet fuel process.</param>
        /// <param name="pageReport">The page report.</param>
        public NationalJetFuelCalculationResultController(IGenericCatalogBusiness genericCatalog, INationalJetFuelProcessBusiness nationalJetFuelProcess, IPageReportBusiness pageReport)
        {
            this.genericCatalog = genericCatalog;
            this.nationalJetFuelProcess = nationalJetFuelProcess;
            this.pageReport = pageReport;
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName
                );
            this.controllerName = "National Jet Fuel Calculation Result";
        }

        /// <summary>
        /// Main view.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpGet]
        [CustomAuthorize(Roles = "NATVIEWRES-IDX")]
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
        [CustomAuthorize(Roles = "NATVIEWRES-PRINTREP")]
        public ActionResult GroupReport(JetFuelCalculationResultParamsVO parameters)
        {
            //this.ViewBag.ErrorMessage = "Group report";            

            try
            {
                this.LoadCatalogs();
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReport.GetPageReportByPageName("JetFuelNationalProvisionGroup");

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
                    return this.RedirectToAction("Grouped", "NationalJetFuelCalculationResult", new { parameters = parameters.PeriodCode, reportPath = reportPath, airlineParam = airlineParam, stationParam = stationParam, serviceParam = serviceParam, providerParam = providerParam });
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
        [CustomAuthorize(Roles = "NATVIEWRES-PRINTREP")]
        public ActionResult DetailedReport(JetFuelCalculationResultParamsVO parameters)
        {
            //this.ViewBag.ErrorMessage = "Detailed report";            

            try
            {
                this.LoadCatalogs();
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReport.GetPageReportByPageName("JetFuelNationalProvisionDetail");

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
                    return this.RedirectToAction("Detailed", "NationalJetFuelCalculationResult", new { parameters = parameters.PeriodCode, reportPath = reportPath, airlineParam = airlineParam, stationParam = stationParam, serviceParam = serviceParam, providerParam = providerParam });
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
        /// Errors the report.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NATVIEWRES-PRINTREP")]
        public ActionResult ErrorReport(JetFuelCalculationResultParamsVO parameters)
        {
            //this.ViewBag.ErrorMessage = "Detailed report";            

            try
            {
                this.LoadCatalogs();
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReport.GetPageReportByPageName("NationalJetFuelCostError");

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
                    return this.RedirectToAction("WrongLog", "NationalJetFuelCalculationResult", new { parameters = parameters.PeriodCode, reportPath = reportPath, airlineParam = airlineParam, stationParam = stationParam, serviceParam = serviceParam, providerParam = providerParam });
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

            model.PageSourceUrl = this.Url.Action("Index", "NationalJetFuelCalculationResult", new { Area = "Process" });
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

            model.PageSourceUrl = this.Url.Action("Index", "NationalJetFuelCalculationResult", new { Area = "Process" });
            return View("Report/ViewReport", model);
        }


        /// <summary>
        /// Wrongs the log.
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

            model.PageSourceUrl = this.Url.Action("Index", "NationalJetFuelCalculationResult", new { Area = "Process" });
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
                NationalJetFuelProcessDto process = new NationalJetFuelProcessDto
                {
                    PeriodCode = periodCode
                };

                period = Mapper.Map<NationalJetFuelProcessDto, PeriodVO>(this.nationalJetFuelProcess.FindNationalJetFuelProcess(process));
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
                periods = Mapper.Map<List<NationalJetFuelProcessDto>, List<PeriodVO>>(this.nationalJetFuelProcess.GetAllNationalJetFuelProcesses().ToList());
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
                .Where(c => c.Id.Equals("CM"))
                .ToList();
            this.ViewBag.Provider = this.genericCatalog.GetProviderCatalog();
            var process = this.genericCatalog.GetNationalJetFuelProcessCatalog();
            this.ViewBag.Process = this.genericCatalog.GetNationalJetFuelProcessCatalog();
        }
    }	
}