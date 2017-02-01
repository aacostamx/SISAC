//------------------------------------------------------------------------
// <copyright file="DelayController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Business.Airport;
    using Business.Catalog;
    using Business.Dto.Airports;
    using Business.Dto.Catalogs;
    using Business.ExceptionBusiness;
    using Helpers;
    using Models.VO.Airport;
    using Models.VO.Catalog;
    using Resources;
    using Web.Controllers;

    /// <summary>
    /// Delay Controller
    /// </summary>
    [CustomAuthorize]
    public class DelayController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DelayController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.DelayTitle;

        /// <summary>
        /// Interface
        /// </summary>
        private readonly IDelayBusiness DelayBusiness;

        /// <summary>
        /// Interface
        /// </summary>
        private readonly IFunctionalAreaBusiness AreaBusiness;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="DelayBusiness"></param>
        /// <param name="AreaBusiness"></param>
        public DelayController(IDelayBusiness DelayBusiness, IFunctionalAreaBusiness AreaBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.DelayBusiness = DelayBusiness;
            this.AreaBusiness = AreaBusiness;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "DELAY-IDX")]
        public ActionResult Index()
        {
            IList<DelayDto> delayDto = new List<DelayDto>();
            IList<DelayVO> delayVo = new List<DelayVO>();
            try
            {
                delayDto = DelayBusiness.GetActivesDelays();
                delayVo = Mapper.Map<IList<DelayDto>, IList<DelayVO>>(delayDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return View(delayVo);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "DELAY-ADD")]
        public ActionResult Create()
        {
            DelayModelVO delayViewModel = new DelayModelVO();
            IList<FunctionalAreaDto> areasDto = new List<FunctionalAreaDto>();
            try
            {
                if (ModelState.IsValid)
                {
                    areasDto = AreaBusiness.GetActivesFunctionalAreas();
                    delayViewModel.FuntionalAreasVo = Mapper.Map<IList<FunctionalAreaDto>, IList<FunctionalAreaVO>>(areasDto);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
                delayViewModel.FuntionalAreasVo = Mapper.Map<IList<FunctionalAreaDto>, IList<FunctionalAreaVO>>(areasDto);
            }
            return this.View(delayViewModel);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Delay"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "DELAY-ADD")]
        public ActionResult Create(DelayModelVO Delay)
        {
            if (Delay == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DelayDto DelayDto = new DelayDto();
            try
            {
                if (ModelState.IsValid)
                {
                    DelayDto = Mapper.Map<DelayVO, DelayDto>(Delay.DelayVo);
                    DelayDto.FunctionalArea = null;
                    DelayBusiness.AddDelay(DelayDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                message = ex.Number == 10 ? string.Format(message, Resource.DelayCode) : message;
                this.ViewBag.ErrorMessage = message;
                Delay.FuntionalAreasVo = Mapper.Map<IList<FunctionalAreaDto>, IList<FunctionalAreaVO>>(AreaBusiness.GetActivesFunctionalAreas());
            }
            return View(Delay);
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "DELAY-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DelayDto dto = new DelayDto();
            DelayModelVO delayModelVo = new DelayModelVO();
            try
            {
                if (dto == null)
                {
                    return this.HttpNotFound();
                }
                dto = DelayBusiness.FindDelayById(id);
                delayModelVo.DelayVo = Mapper.Map<DelayDto, DelayVO>(dto);
                delayModelVo.FuntionalAreasVo = Mapper.Map<IList<FunctionalAreaDto>, IList<FunctionalAreaVO>>(AreaBusiness.GetActivesFunctionalAreas());
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                delayModelVo.FuntionalAreasVo = Mapper.Map<IList<FunctionalAreaDto>, IList<FunctionalAreaVO>>(AreaBusiness.GetActivesFunctionalAreas());

            }
            return View(delayModelVo);
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="Delay"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "DELAY-UPD")]
        public ActionResult Edit(DelayModelVO Delay)
        {
            if (Delay == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DelayDto delayDto = new DelayDto();
            try
            {
                if (ModelState.IsValid)
                {
                    delayDto = Mapper.Map<DelayVO, DelayDto>(Delay.DelayVo);
                    delayDto.FunctionalArea = null;
                    DelayBusiness.UpdateDelay(delayDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
                Delay.FuntionalAreasVo = Mapper.Map<IList<FunctionalAreaDto>, IList<FunctionalAreaVO>>(AreaBusiness.GetActivesFunctionalAreas());
            }
            return View(Delay);
        }

        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "DELAY-VIE")]
        public ActionResult Details(string id)
        {
            DelayDto delay = new DelayDto();
            DelayVO delayVo = new DelayVO();
            try
            {
                delay = DelayBusiness.FindDelayById(id);
                delayVo = Mapper.Map<DelayDto, DelayVO>(delay);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return View(delayVo);
        }

        /// <summary>
        /// Delete POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "DELAY-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DelayDto delay = new DelayDto();
            DelayVO delayVo = new DelayVO();
            try
            {
                delay = DelayBusiness.FindDelayById(id);
                delayVo = Mapper.Map<DelayDto, DelayVO>(delay);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(delayVo);
        }

        /// <summary>
        /// Delete POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "DELAY-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DelayDto delay = new DelayDto();
            DelayVO delayVo = new DelayVO();
            try
            {
                delay = DelayBusiness.FindDelayById(id);
                delayVo = Mapper.Map<DelayDto, DelayVO>(delay);
                this.DelayBusiness.DeleteDelay(delay);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;
                return RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(delayVo);
        }

        /// <summary>
        /// Gets the delays.
        /// </summary>
        /// <returns>Object with delays information.</returns>
        [HttpGet]
        public JsonResult GetDelays()
        {
            IList<DelayDto> delays = new List<DelayDto>();
            try
            {
                delays = this.DelayBusiness.GetActivesDelays();
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return this.Json(delays, JsonRequestBehavior.AllowGet);
        }
    }
}