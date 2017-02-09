//------------------------------------------------------------------------
// <copyright file="ServiceController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Helpers;
    using Models.VO.Airport;
    using Resources;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using Web.Controllers;

    /// <summary>
    /// Controller for Service Section
    /// </summary>
    [CustomAuthorize]
    public class ServiceController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ServiceController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Interface for the business operations
        /// </summary>
        private readonly IServiceBusiness serviceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceController"/> class.
        /// </summary>
        /// <param name="serviceService"></param>
        public ServiceController(IServiceBusiness serviceService)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.serviceService = serviceService;
        }


        /// <summary>
        /// Principal View
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "SERVICE-IDX")]
        public ActionResult Index()
        {
            IList<ServiceVO> servicesVo = new List<ServiceVO>();
            IList<ServiceDto> services = new List<ServiceDto>();
            try
            {
                services = this.serviceService.GetActivesService();
                servicesVo = Mapper.Map<IList<ServiceDto>, IList<ServiceVO>>(services);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "SERVICE", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "SERVICE", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(servicesVo);
        }

        /// <summary>
        /// Create action for Service
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "SERVICE-ADD")]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Action for create a record in the Service by POST
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "SERVICE-ADD")]
        public ActionResult Create(ServiceVO service)
        {
            if (service == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "SERVICE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "SERVICE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    ServiceDto serviceDto = new ServiceDto();
                    serviceDto = Mapper.Map<ServiceVO, ServiceDto>(service);
                    this.serviceService.AddService(serviceDto);
                    TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, "SERVICE", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "SERVICE", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                if (ex.Number == 10)
                {
                    message = string.Format(message, VOI.SISAC.Web.Resources.Resource.ServiceCode);
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(service);
        }

        /// <summary>
        /// Edit View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "SERVICE-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "SERVICE"));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "SERVICE", this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ServiceDto service = new ServiceDto();
            ServiceVO serviceVo = new ServiceVO();
            try
            {
                service = this.serviceService.FindServiceById(id);
                serviceVo = Mapper.Map<ServiceDto, ServiceVO>(service);
                if (serviceVo == null)
                {
                    return this.HttpNotFound();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "SERVICE", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "SERVICE", this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }

            return this.View(serviceVo);
        }

        /// <summary>
        /// Action for edit a record in the Service by POST
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "SERVICE-UPD")]
        public ActionResult Edit(ServiceVO service)
        {
            if (service == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "SERVICE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "SERVICE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                if (this.ModelState.IsValid)
                {
                    ServiceDto serviceDto = new ServiceDto();
                    serviceDto = Mapper.Map<ServiceVO, ServiceDto>(service);
                    this.serviceService.UpdateService(serviceDto);
                    TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, "SERVICE", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, "SERVICE", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(service);
        }

        /// <summary>
        /// Action for Delete a record in the Service by POST
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "SERVICE-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "SERVICE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "SERVICE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ServiceDto service = new ServiceDto();
            ServiceVO serviceVo = new ServiceVO();
            try
            {
                service = this.serviceService.FindServiceById(id);
                serviceVo = Mapper.Map<ServiceDto, ServiceVO>(service);
                if (serviceVo == null)
                {
                    return this.HttpNotFound();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "SERVICE", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "SERVICE", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(serviceVo);
        }

        /// <summary>
        /// Action for confirm of delete of a record in the Service by POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "SERVICE-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "SERVICE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "SERVICE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceDto service = new ServiceDto();
            ServiceVO serviceVo = new ServiceVO();
            try
            {
                service = this.serviceService.FindServiceById(id);
                this.serviceService.DeleteService(service);
                serviceVo = Mapper.Map<ServiceDto, ServiceVO>(service);
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, "SERVICE", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, "SERVICE", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(serviceVo);
        }
    }
}