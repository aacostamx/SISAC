//------------------------------------------------------------------------
// <copyright file="ProviderController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.Finance;
    using VOI.SISAC.Business.ExceptionBusiness;
    using Resources;
    using Models.VO.Finance;
    using AutoMapper;
    using Helpers;
    using System.Diagnostics;
    using Web.Controllers;

    /// <summary>
    /// Controller for provider section
    /// </summary>
    [CustomAuthorize]
    public class ProviderController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ProviderController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Interface for the business operations
        /// </summary>
        private readonly IProviderBusiness providerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderController"/> class.
        /// </summary>
        /// <param name="providerService"></param>
        public ProviderController(IProviderBusiness providerService)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.providerService = providerService;
        }

        /// <summary>
        /// Vista Principal
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "PROVIDER-IDX")]
        public ActionResult Index()
        {
            IList<ProviderVO> providersVO = new List<ProviderVO>();
            IList<ProviderDto> providers = new List<ProviderDto>();
            try
            {
                providers = this.providerService.GetActivesProvider();
                providersVO = Mapper.Map<IList<ProviderDto>, IList<ProviderVO>>(providers);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "PROVIDER", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "PROVIDER", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(providersVO);
        }

        /// <summary>
        /// Create Action for Provider.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "PROVIDER-ADD")]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Action for create a record in the Provider by POST
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "PROVIDER-ADD")]
        public ActionResult Create(ProviderVO provider)
        {
            if (provider == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "PROVIDER"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "PROVIDER"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                if (this.ModelState.IsValid)
                {
                    ProviderDto providerDto = new ProviderDto();
                    providerDto = Mapper.Map<ProviderVO, ProviderDto>(provider);
                    this.providerService.AddProvider(providerDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, "PROVIDER", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "PROVIDER", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                if (ex.Number == 10)
                {
                    message = string.Format(message, VOI.SISAC.Web.Resources.Resource.ProviderNumber);
                }
                this.ViewBag.ErrorMessage = message;
            }
            return this.View(provider);
        }

        /// <summary>
        /// Action for edit a record in the Provider by POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "PROVIDER-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "PROVIDER"));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "PROVIDER", this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProviderDto provider = new ProviderDto();
            ProviderVO providerVo = new ProviderVO();
            try
            {
                provider = this.providerService.FindProviderById(id);
                providerVo = Mapper.Map<ProviderDto, ProviderVO>(provider);
                if (providerVo == null)
                {
                    return this.HttpNotFound();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "PROVIDER", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "PROVIDER", this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }

            return this.View(providerVo);
        }

        /// <summary>
        /// Action for edit a record in the Provider by POST
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "PROVIDER-UPD")]
        public ActionResult Edit(ProviderVO provider)
        {
            if (provider == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "PROVIDER"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "PROVIDER"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                if (this.ModelState.IsValid)
                {
                    ProviderDto providerDto = new ProviderDto();
                    providerDto = Mapper.Map<ProviderVO, ProviderDto>(provider);
                    this.providerService.UpdateProvider(providerDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, "PROVIDER", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, "PROVIDER", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(provider);
        }

        /// <summary>
        ///  Action for delete a record in the catalog Provider by POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "PROVIDER-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "PROVIDER"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "PROVIDER"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProviderDto provider = new ProviderDto();
            ProviderVO providerVO = new ProviderVO();
            try
            {
                provider = this.providerService.FindProviderById(id);
                providerVO = Mapper.Map<ProviderDto, ProviderVO>(provider);
                if (providerVO == null)
                {
                    return this.HttpNotFound();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "PROVIDER", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "PROVIDER", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(providerVO);
        }

        /// <summary>
        /// Action for delete a record in the catalog Provider by POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "PROVIDER-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "PROVIDER"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "PROVIDER"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProviderDto provider = new ProviderDto();
            ProviderVO providerVo = new ProviderVO();
            try
            {
                provider = this.providerService.FindProviderById(id);
                this.providerService.DeleteProvider(provider);
                providerVo = Mapper.Map<ProviderDto, ProviderVO>(provider);
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, "PROVIDER", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, "PROVIDER", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(providerVo);
        }

    }
}