//------------------------------------------------------------------------
// <copyright file="NationalPolicyController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Process.Controllers
{
    using AutoMapper;
    using Business.Common;
    using Business.Dto.Catalogs;
    using Business.Dto.Process;
    using Business.ExceptionBusiness;
    using Business.Process;
    using Helpers;
    using Models.VO.Process;
    using Resources;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Web.Controllers;

    /// <summary>
    /// NationalPolicyController.
    /// </summary>
    [CustomAuthorize]
    public class NationalPolicyController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NationalPolicyController));

        /// <summary>
        /// The generic catalog
        /// </summary>
        private readonly IGenericCatalogBusiness genericCatalog;

        /// <summary>
        /// the jet fuel policy control
        /// </summary>
        private readonly INationalJetFuelPolicyControlBusiness ntlJetFuelCTRLBusiness;

        /// <summary>
        /// the jet fuel policy control
        /// </summary>
        private readonly INationalJetFuelPolicyBusiness ntlJetFuelBusiness;

        /// <summary>
        /// the policy service
        /// </summary>
        private readonly IPolicyService policyService;

        /// <summary>
        /// The controller name
        /// </summary>
        private readonly string controllerName = "NationalPolicyController";

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalPolicyController"/> class.
        /// </summary>
        /// <param name="genericCatalog">The generic catalog.</param>
        /// <param name="ntlJetFuelCTRLBusiness">The NTL jet fuel control business.</param>
        /// <param name="policyService">The policy service.</param>
        /// <param name="ntlJetFuelBusiness">The NTL jet fuel business.</param>
        public NationalPolicyController(
            IGenericCatalogBusiness genericCatalog,
            INationalJetFuelPolicyControlBusiness ntlJetFuelCTRLBusiness,
            IPolicyService policyService,
            INationalJetFuelPolicyBusiness ntlJetFuelBusiness)
        {
            this.genericCatalog = genericCatalog;
            this.ntlJetFuelCTRLBusiness = ntlJetFuelCTRLBusiness;
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.policyService = policyService;
            this.ntlJetFuelBusiness = ntlJetFuelBusiness;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NTLPOLICY-IDX")]
        public ActionResult Index()
        {
            try
            {
                this.LoadCatalogs();
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
            }

            return this.View();
        }

        /// <summary>
        /// Creates the national policy.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLPOLICY-ADD")]
        public ActionResult CreateNationalPolicy(NationalJetFuelPolicyControlVO policyCTRL)
        {
            var invalidCurrencies = false;
            var policyDto = new NationalJetFuelPolicyControlDto();

            if (policyCTRL == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!this.ModelState.IsValid)
            {
                this.LoadCatalogs();
                return this.View("Index", policyCTRL);
            }

            try
            {
                this.LoadCatalogs();
                policyCTRL.CreationDate = DateTime.Now;
                policyCTRL.SendByUserName = this.User.Identity.Name;
                policyDto = Mapper.Map<NationalJetFuelPolicyControlDto>(policyCTRL);
                this.ValidateTypeChangeForCurrency(policyDto, ref invalidCurrencies);

                if (invalidCurrencies)
                {
                    return this.View("Index", policyCTRL);
                }

                policyCTRL.NationalPolicyID = this.ntlJetFuelCTRLBusiness.CreateNationalPolicy(policyDto);
                policyCTRL.StartDateComplementary = policyCTRL.StartDateComplementary.HasValue ? policyCTRL.StartDateComplementary : new DateTime();
                policyCTRL.EndDateComplementary = policyCTRL.EndDateComplementary.HasValue ? policyCTRL.EndDateComplementary : new DateTime();
                policyCTRL.ItemText = !string.IsNullOrEmpty(policyCTRL.ItemText) ? policyCTRL.ItemText : policyCTRL.HeaderText;

                if (policyCTRL.NationalPolicyID > 0)
                {
                    this.TempData["OperationSuccess"] = string.Format(Resource.SuccessPolicyCreate, policyCTRL.NationalPolicyID);
                    return this.View("Send", policyCTRL);
                }
                else
                {
                    this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
            }
            return this.View("Index", policyCTRL);
        }

        /// <summary>
        /// Sends the national policy.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLPOLICY-SEND")]
        public ActionResult SendNationalPolicy(NationalJetFuelPolicyControlVO policyCTRL)
        {
            var info = string.Empty;
            var status = string.Empty;
            var ntlPolicies = new List<NationalJetFuelPolicyVO>();
            var ntlPoliciesDto = new List<NationalJetFuelPolicyDto>();

            if (policyCTRL.NationalPolicyID <= 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                ntlPolicies = this.policyService.SendNationalPolicyToService(policyCTRL.NationalPolicyID, ref info, ref status).ToList();
                ntlPoliciesDto = Mapper.Map<List<NationalJetFuelPolicyDto>>(ntlPolicies);
                this.ntlJetFuelBusiness.UpdateNationalPoliciesResponse(ntlPoliciesDto);

                //update status error o sucess
                NationalJetFuelPolicyControlDto policyControl = new NationalJetFuelPolicyControlDto();
                if (status != string.Empty)
                {
                    policyControl = this.ntlJetFuelCTRLBusiness.FindNationalPolicyCTRL(new NationalJetFuelPolicyControlDto() { NationalPolicyID = policyCTRL.NationalPolicyID });
                    policyControl.Status = status;
                    this.ntlJetFuelCTRLBusiness.UpdateNationalPolicyCTRL(policyControl);
                }

                this.TempData["OperationSuccess"] = info;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
            }
            return this.View("Send", policyCTRL);
        }

        /// <summary>
        /// Sets the defaults.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        private void SetDefaultsView(NationalJetFuelPolicyControlVO policyCTRL)
        {
            try
            {
                policyCTRL.StartDateComplementary = policyCTRL.StartDateComplementary.HasValue ? policyCTRL.StartDateComplementary : new DateTime();
                policyCTRL.EndDateComplementary = policyCTRL.EndDateComplementary.HasValue ? policyCTRL.EndDateComplementary : new DateTime();
                policyCTRL.ItemText = !string.IsNullOrEmpty(policyCTRL.ItemText) ? policyCTRL.ItemText : policyCTRL.HeaderText;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Validates the type change for currency.
        /// </summary>
        /// <param name="policyDto">The policy dto.</param>
        /// <param name="invalid">if set to <c>true</c> [valid currencies].</param>
        /// <returns></returns>
        private List<string> ValidateTypeChangeForCurrency(NationalJetFuelPolicyControlDto policyDto, ref bool invalid)
        {
            var invalidCurrencies = new List<string>();
            var errorMessages = new List<string>();

            try
            {
                invalidCurrencies = this.ntlJetFuelCTRLBusiness.ValidateTypeChangeForCurrency(policyDto).ToList();

                if (invalidCurrencies.Count > 0)
                {
                    errorMessages = invalidCurrencies.Select(c => string.Concat("No exchange rates found for ", c)).ToList();
                    this.ViewBag.ListErrorMessage = errorMessages;
                    this.LoadCatalogs();
                    invalid = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }

            return invalidCurrencies;
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
        private void LoadCatalogs()
        {
            try
            {
                this.ViewBag.Airline = this.genericCatalog.GetAirlineCatalog();
                this.ViewBag.Airport = this.genericCatalog.GetNationalAirportsCatalog();
                this.ViewBag.Provider = this.genericCatalog.GetProviderCatalog();
                this.ViewBag.Process = this.genericCatalog.GetNationalJetFuelProcessCatalog();
                this.ViewBag.Service = this.genericCatalog.GetNationalJetFuelServicesCatalog();
            }
            catch (Exception ex)
            {
                this.ViewBag.Airline = new List<GenericCatalogDto>();
                this.ViewBag.Airport = new List<GenericCatalogDto>();
                this.ViewBag.Provider = new List<GenericCatalogDto>();
                this.ViewBag.Process = new List<GenericCatalogDto>();
                this.ViewBag.Service = new List<GenericCatalogDto>();
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }

        }
    }
}