//------------------------------------------------------------------------
// <copyright file="NationalPolicyHistoryController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Process.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Business.Dto.Process;
    using Business.Dto.Security;
    using Business.ExceptionBusiness;
    using Business.Process;
    using Business.Security;
    using Helpers;
    using Models.VO.Process;
    using Newtonsoft.Json;
    using Resources;
    using VOI.SISAC.Web.Services;
    using Web.Controllers;

    /// <summary>
    /// National Policy History Controller
    /// </summary>
    [CustomAuthorize]
    public class NationalPolicyHistoryController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(PoliciesHistoryController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// module name
        /// </summary>
        private readonly string moduleName = Resource.PoliciesHistoryTitle;

        /// <summary>
        /// Interface for Airport service contract operations
        /// </summary>
        private readonly INationalJetFuelPolicyControlBusiness nationalPolicyControlBusiness;

        /// <summary>
        /// The page report
        /// </summary>
        private readonly IPageReportBusiness pageReportBusiness;

        /// <summary>
        /// the policy service
        /// </summary>
        private readonly IPolicyService policyService;

        /// <summary>
        /// the jet fuel policy control
        /// </summary>
        private readonly INationalJetFuelPolicyBusiness nationalPolicyBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalPolicyHistoryController" /> class.
        /// </summary>
        /// <param name="nationalPolicyControlBusiness">The national policy control business.</param>
        /// <param name="nationalPolicyBusiness">The national policy business.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        /// <param name="policyService">The policy service.</param>
        public NationalPolicyHistoryController(
            INationalJetFuelPolicyControlBusiness nationalPolicyControlBusiness,
            INationalJetFuelPolicyBusiness nationalPolicyBusiness,
            IPageReportBusiness pageReportBusiness,
            IPolicyService policyService)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);

            this.nationalPolicyControlBusiness = nationalPolicyControlBusiness;
            this.nationalPolicyBusiness = nationalPolicyBusiness;
            this.pageReportBusiness = pageReportBusiness;
            this.policyService = policyService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NAPOLHIS-IDX")]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="policies">The policies.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "NAPOLHIS-SEARCH")]
        public ActionResult Search(NationalJetFuelPolicyHistoryVO policies)
        {
            return this.View();
        }

        /// <summary>
        /// Gets the paged policies history.
        /// </summary>
        /// <param name="policies">The policies.</param>
        /// <returns>
        /// Content result.
        /// </returns>
        public ContentResult GetPagedPoliciesHistory(NationalJetFuelPolicyHistoryVO policies)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            List<NationalJetFuelPolicyHistoryVO> history = new List<NationalJetFuelPolicyHistoryVO>();
            List<NationalJetFuelPolicyControlDto> historyDto = new List<NationalJetFuelPolicyControlDto>();

            try
            {
                totalRows = this.nationalPolicyControlBusiness.CountPoliciesByYear();
                historyDto = this.nationalPolicyControlBusiness.GetPagedPoliciesHistory(policies.PageSize, policies.PageNumber).ToList();
                history = Mapper.Map<List<NationalJetFuelPolicyHistoryVO>>(historyDto);
                jsonConvert.total = totalRows;
                jsonConvert.rows = history;
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
        /// Gets the paged search policies history.
        /// </summary>
        /// <param name="policies">The policies.</param>
        /// <returns>Content result.</returns>
        public ContentResult GetPagedSearhPoliciesHistory(NationalJetFuelPolicyHistoryVO policies)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            List<NationalJetFuelPolicyHistoryVO> history = new List<NationalJetFuelPolicyHistoryVO>();
            List<NationalJetFuelPolicyControlDto> historyDto = new List<NationalJetFuelPolicyControlDto>();

            try
            {
                totalRows = this.nationalPolicyControlBusiness.CountAllPoliciesSeach(Mapper.Map<NationalJetFuelPolicyHistoryDto>(policies));
                historyDto = this.nationalPolicyControlBusiness.GetPagedPoliciesHistorySearch(Mapper.Map<NationalJetFuelPolicyHistoryDto>(policies)).ToList();
                history = Mapper.Map<List<NationalJetFuelPolicyHistoryVO>>(historyDto);
                jsonConvert.total = totalRows;
                jsonConvert.rows = history;
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
        /// Shows the report.
        /// </summary>
        /// <param name="nationalPolicyId">The policy identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NAPOLHIS-PRINTREP")]
        public ActionResult ShowReport(long nationalPolicyId)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("NationalJetFuelPolicyDetail");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) && !string.IsNullOrEmpty(nationalPolicyId.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("PolicyID", nationalPolicyId.ToString(), false)
                        });

                    model.PageSourceUrl = this.Url.Action("Index", "NationalPolicyHistory", new { Area = "Process" });
                    return this.View("Report/ViewReport", model);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Send the policy to SAP.
        /// </summary>
        /// <param name="nationalPolicyId">The policy identifier.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "NAPOLHIS-SEND")]
        public ActionResult Send(long nationalPolicyId)
        {
            if (nationalPolicyId <= 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                string information = string.Empty;
                string status = string.Empty;
                List<NationalJetFuelPolicyVO> nationalPolicies = new List<NationalJetFuelPolicyVO>();
                List<NationalJetFuelPolicyDto> nationalPoliciesDto = new List<NationalJetFuelPolicyDto>();
                nationalPolicies = this.policyService.SendNationalPolicyToService(nationalPolicyId, ref information, ref status).ToList();
                nationalPoliciesDto = Mapper.Map<List<NationalJetFuelPolicyDto>>(nationalPolicies);
                this.nationalPolicyBusiness.UpdateNationalPoliciesResponse(nationalPoliciesDto);

                //update status error o sucess
                NationalJetFuelPolicyControlDto policyControl = new NationalJetFuelPolicyControlDto();                
                if (status != string.Empty)
                {
                    policyControl = this.nationalPolicyControlBusiness.FindNationalPolicyCTRL(new NationalJetFuelPolicyControlDto() { NationalPolicyID = nationalPolicyId });
                    policyControl.Status = status;
                    this.nationalPolicyControlBusiness.UpdateNationalPolicyCTRL(policyControl);
                }

                this.TempData["OperationSuccess"] = information;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
            }

            return this.View("Index");
        }

        /// <summary>
        /// Cancels the specified policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NAPOLHIS-CANCEL")]
        public ActionResult Cancel(NationalJetFuelPolicyHistoryVO policy)
        {
            if (policy == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                NationalJetFuelPolicyControlDto nationalPolicy = new NationalJetFuelPolicyControlDto()
                {
                    NationalPolicyID = policy.NationalPolicyId
                };

                bool canceled = this.nationalPolicyControlBusiness.CancelNationalJetFuelPolicy(nationalPolicy);
                if (canceled)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.ViewBag.ErrorMessage = Resource.CancelError;
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
            }

            return this.View("Index");
        }
    }
}