//------------------------------------------------------------------------
// <copyright file="PoliciesHistoryController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    using Business.Common;
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
    /// PoliciesHistoryController Class
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [CustomAuthorize]
    public class PoliciesHistoryController : BaseController
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
        private readonly IJetFuelPolicyControlBusiness policyCtrlBusiness;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

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
        private readonly IJetFuelPolicyBusiness jetFuelPolicy;

        /// <summary>
        /// the jet fuel policy control
        /// </summary>
        private readonly IJetFuelPolicyControlBusiness jetFuelPolicyControl;

        /// <summary>
        /// The provision business
        /// </summary>
        private readonly IJetFuelProvisionBusiness provisionBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="PoliciesHistoryController"/> class.
        /// </summary>
        /// <param name="policyCtrlBusiness">The policy control business.</param>
        /// <param name="generic">The generic.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        /// <param name="policyService">The policy service.</param>
        /// <param name="jetFuelPolicy">The jet fuel policy.</param>
        /// <param name="provisionBusiness">The provision business.</param>
        public PoliciesHistoryController(
            IJetFuelPolicyControlBusiness policyCtrlBusiness,
            IGenericCatalogBusiness generic,
            IPageReportBusiness pageReportBusiness,
            IPolicyService policyService,
            IJetFuelPolicyBusiness jetFuelPolicy,
            IJetFuelPolicyControlBusiness jetFuelPolicyControl,
            IJetFuelProvisionBusiness provisionBusiness)
        {
            this.userInfo = string.Format(
            LogMessages.UserInfo,
            Environment.UserDomainName,
            Environment.UserName,
            Environment.MachineName);
            this.policyCtrlBusiness = policyCtrlBusiness;
            this.generic = generic;
            this.pageReportBusiness = pageReportBusiness;
            this.policyService = policyService;
            this.jetFuelPolicyControl = jetFuelPolicyControl;
            this.jetFuelPolicy = jetFuelPolicy;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "POLICYHIST-IDX")]
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
        [CustomAuthorize(Roles = "POLICYHIST-SEARCH")]
        public ActionResult Search(JetFuelPoliciesHistoryVO policies)
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
        public ContentResult GetPagedPoliciesHistory(JetFuelPoliciesHistoryVO policies)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            IList<JetFuelPoliciesHistoryVO> history = new List<JetFuelPoliciesHistoryVO>();
            IList<JetFuelPolicyControlDto> historyDto = new List<JetFuelPolicyControlDto>();

            try
            {
                totalRows = this.policyCtrlBusiness.CountPoliciesByYear();
                historyDto = this.policyCtrlBusiness.GetPagedPoliciesHistory(policies.Pagesize, policies.Pagenumber);
                history = Mapper.Map<IList<JetFuelPoliciesHistoryVO>>(historyDto);
                jsonConvert.total = totalRows;
                jsonConvert.rows = history;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                result = Content(json);
            }
            catch (Exception ex)
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
        /// Gets the paged search policies history.
        /// </summary>
        /// <param name="policies">The policies.</param>
        /// <returns>Content result.</returns>
        public ContentResult GetPagedSearhPoliciesHistory(JetFuelPoliciesHistoryVO policies)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            IList<JetFuelPoliciesHistoryVO> history = new List<JetFuelPoliciesHistoryVO>();
            IList<JetFuelPolicyControlDto> historyDto = new List<JetFuelPolicyControlDto>();

            try
            {
                totalRows = this.policyCtrlBusiness.CountAllPoliciesSeach(Mapper.Map<JetFuelPoliciesHistoryDto>(policies));
                historyDto = this.policyCtrlBusiness.GetPagedPoliciesHistorySearch(Mapper.Map<JetFuelPoliciesHistoryDto>(policies));
                history = Mapper.Map<IList<JetFuelPoliciesHistoryVO>>(historyDto);
                jsonConvert.total = totalRows;
                jsonConvert.rows = history;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                result = Content(json);
            }
            catch (Exception ex)
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
        /// <param name="PolicyId">The policy identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "POLICYHIST-PRINTREP")]
        public ActionResult ShowReport(long PolicyId)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("JetFuelPolicyDetail");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!String.IsNullOrEmpty(reportPath) && !String.IsNullOrEmpty(PolicyId.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(reportPath, new List<Microsoft.Reporting.WebForms.ReportParameter>()
                    {
                        new Microsoft.Reporting.WebForms.ReportParameter("PolicyID",PolicyId.ToString(),false)
                    });

                    model.PageSourceUrl = this.Url.Action("Index", "PoliciesHistory", new { Area = "Process" });
                    return View("Report/ViewReport", model);
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
        /// Indexes this instance.
        /// </summary>
        /// <param name="policyId">The policy identifier.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "POLICYHIST-SEND")]
        public ActionResult Send(long policyId)
        {
            if (policyId <= 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IList<JetFuelPoliciesHistoryVO> policiesControlVo = new List<JetFuelPoliciesHistoryVO>();
            try
            {
                string information = string.Empty;
                string status = string.Empty;
                List<JetFuelPolicyVO> policies = new List<JetFuelPolicyVO>();
                List<JetFuelPolicyDto> policiesDto = new List<JetFuelPolicyDto>();
                policies = this.policyService.SendPolicyToService(policyId, ref information, ref status).ToList();
                policiesDto = Mapper.Map<List<JetFuelPolicyDto>>(policies);
                this.jetFuelPolicy.UpdatePoliciesFromResponse(policiesDto);

                //update status error o sucess
                JetFuelPolicyControlDto policyControl = new JetFuelPolicyControlDto();
                long policyID = policyId > 0 ? (long)policyId : 0;

                if (status != string.Empty)
                {
                    policyControl = this.jetFuelPolicyControl.FindById(policyID);
                    policyControl.Status = status;
                    this.jetFuelPolicyControl.Update(policyControl);
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
        [CustomAuthorize(Roles = "POLICYHIST-CANCEL")]
        public ActionResult Cancel(JetFuelPoliciesHistoryVO policy)
        {
            var canceled = false;
            try
            {
                canceled = this.policyCtrlBusiness.CancelJetFuelPolicy(policy.PolicyId);

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
    }
}