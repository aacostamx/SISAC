//------------------------------------------------------------------------
// <copyright file="CreatePolicyController.cs" company="AACOSTA">
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
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Process;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Process;
    using VOI.SISAC.Web.Resources;
    using VOI.SISAC.Web.Services;

    /// <summary>
    /// Create Policy Controller.
    /// </summary>
    [CustomAuthorize]
    public class CreatePolicyController : BaseController
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
        /// the jet fuel policy control
        /// </summary>
        private readonly IJetFuelPolicyControlBusiness jetFuelPolicyControl;

        /// <summary>
        /// the jet fuel policy control
        /// </summary>
        private readonly IJetFuelPolicyBusiness jetFuelPolicy;

        /// <summary>
        /// the policy service
        /// </summary>
        private readonly IPolicyService policyService;

        /// <summary>
        /// The controller name
        /// </summary>
        private readonly string controllerName;

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePolicyController" /> class.
        /// </summary>
        /// <param name="genericCatalog">The generic catalog.</param>
        /// <param name="jetFuelPolicyControl">The jet fuel policy control.</param>
        /// <param name="policyService">The policy service.</param>
        public CreatePolicyController(
            IGenericCatalogBusiness genericCatalog,
            IJetFuelPolicyControlBusiness jetFuelPolicyControl,
            IPolicyService policyService,
            IJetFuelPolicyBusiness jetFuelPolicy)
        {
            this.genericCatalog = genericCatalog;
            this.jetFuelPolicyControl = jetFuelPolicyControl;
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.controllerName = "Jet Fuel Policy Control";
            this.policyService = policyService;
            this.jetFuelPolicy = jetFuelPolicy;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles="POLICYSAVE-ADD")]
        public ActionResult Index()
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
        /// <param name="control">The control.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "POLICYSAVE-ADD")]
        public ActionResult Create(JetFuelPolicyControlParamsVO control)
        {
            if (control == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View("Index", control);
            }

            try
            {
                JetFuelPolicyControlDto policyControl = new JetFuelPolicyControlDto();
                control.CreationDate = DateTime.Now;
                control.SendByUserName = this.User.Identity.Name;
                policyControl = Mapper.Map<JetFuelPolicyControlParamsVO, JetFuelPolicyControlDto>(control);
                List<string> validateCurrency = this.jetFuelPolicyControl.ValidateTypeChangeForCurrency(policyControl).ToList();

                if (validateCurrency.Count > 0)
                {
                    List<string> message = new List<string>();
                    foreach (string item in validateCurrency)
                    {
                        if (item.Length < 4)
                        {
                            message.Add("No exchange rates found for " + item);
                        }
                        else
                        {
                            message.Add(item);
                        }
                        
                    }

                    this.ViewBag.ListErrorMessage = message;
                    this.LoadCatalogs();
                    return this.View("Index", control);
                }

                control.PolicyId = this.jetFuelPolicyControl.CreatePolicy(policyControl);
                control.EndDateComplementary = control.EndDateComplementary != null ? control.EndDateComplementary : new DateTime();
                control.StartDateComplementary = control.StartDateComplementary != null ? control.StartDateComplementary : new DateTime();
                control.ItemText = control.ItemText != null ? control.ItemText : control.HeaderText;
                if (control.PolicyId > 0)
                {
                    this.TempData["OperationSuccess"] = string.Format(Resource.SuccessPolicyCreate, control.PolicyId);
                }
                else
                {
                    this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
            }

            return this.View(control);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="policyId">The policy identifier.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "POLICYSAVE-SEND")]
        public ActionResult Send(long? policyId)
        {
            if (policyId == null || policyId.Value <= 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                string information = string.Empty;
                string status = string.Empty;
                List<JetFuelPolicyVO> policies = new List<JetFuelPolicyVO>();
                List<JetFuelPolicyDto> policiesDto = new List<JetFuelPolicyDto>();
                policies = this.policyService.SendPolicyToService(policyId.Value, ref information, ref status).ToList();

                if (policies.Count > 0)
                {
                    policiesDto = Mapper.Map<List<JetFuelPolicyDto>>(policies);
                    this.jetFuelPolicy.UpdatePoliciesFromResponse(policiesDto);
                }

                //update status error o sucess
                JetFuelPolicyControlDto policyControl = new JetFuelPolicyControlDto();
                long policyID = policyId > 0 ? (long) policyId : 0;

                if (status != string.Empty)
                {
                    policyControl = this.jetFuelPolicyControl.FindById(policyID);
                    policyControl.Status = status;
                    this.jetFuelPolicyControl.Update(policyControl);
                }

                this.TempData["OperationSuccess"] = information;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
            }

            return this.View("Create", new JetFuelPolicyControlParamsVO());
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
            this.ViewBag.Process = this.genericCatalog.GetJetFuelProcessCatalog();
        }
    }
}