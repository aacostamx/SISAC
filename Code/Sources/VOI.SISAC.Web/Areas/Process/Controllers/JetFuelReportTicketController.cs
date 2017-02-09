//------------------------------------------------------------------------
// <copyright file="JetFuelReportTicketController.cs" company="AACOSTA">
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
    /// Jet Fuel Report Ticket Controller
    /// </summary>
    [CustomAuthorize]
    public class JetFuelReportTicketController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(JetFuelLogErrorController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Page Report
        /// </summary>
        private readonly IPageReportBusiness pageReport;


        /// <summary>
        /// The process
        /// </summary>
        private readonly IJetFuelProcessBusiness jetFuelProcess;

        /// <summary>
        /// The controller name
        /// </summary>
        private readonly string controllerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelLogErrorController" /> class.
        /// </summary>
        /// <param name="pageReport">The page report.</param>
        /// <param name="jetFuelProcess">The jet fuel process.</param>
        public JetFuelReportTicketController(IPageReportBusiness pageReport, IJetFuelProcessBusiness jetFuelProcess)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);

            this.jetFuelProcess = jetFuelProcess;
            this.controllerName = "Jet Fuel Report Ticket";
            this.pageReport = pageReport;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Action result</returns>
        [CustomAuthorize(Roles = "INTFUELREP-IDX")]
        public ActionResult Index()
        {
            PeriodVO Period = new PeriodVO();
            this.ViewBag.Periods = GetAllPeriods();
            return View(Period);
        }

        /// <summary>
        /// Exports the specified period code.
        /// </summary>
        /// <param name="PeriodCode">The period code.</param>
        /// <returns>File with the errors found.</returns>
        [CustomAuthorize(Roles = "INTFUELREP-EXP")]
        public ActionResult Export(string PeriodCode)
        {
            PeriodVO Period = new PeriodVO();
            this.ViewBag.Periods = GetAllPeriods();

            if (string.IsNullOrWhiteSpace(PeriodCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.controllerName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReport.GetPageReportByPageName("JetFuelReportTicket");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!String.IsNullOrEmpty(reportPath) && !String.IsNullOrEmpty(PeriodCode.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        { 
                            new Microsoft.Reporting.WebForms.ReportParameter("PeriodCode",PeriodCode.ToString(),false) 
                        });

                    model.PageSourceUrl = this.Url.Action("Index", "JetFuelReportTicket", new { Area = "Process" });
                    return this.View("Report/ViewReport", model);
                }
                else
                {
                    return this.View("Index", Period);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View("Index", Period);
            }
        }

        /// <summary>
        /// Gets the periods.
        /// </summary>
        /// <returns>Json result.</returns>
        public JsonResult GetPeriods()
        {
            List<PeriodVO> periods = this.GetAllPeriods();
            return this.Json(periods, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the periods.
        /// </summary>
        /// <returns>Json result.</returns>
        public JsonResult GetDates(string periodCode)
        {
            PeriodVO period;
            if (periodCode == null)
            {
                period = new PeriodVO();
            }

            period = this.GetDatesByPeriod(periodCode);
            return this.Json(period, JsonRequestBehavior.AllowGet);
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
    }
}