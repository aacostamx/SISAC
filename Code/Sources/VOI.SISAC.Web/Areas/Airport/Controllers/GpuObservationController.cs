//------------------------------------------------------------------------
// <copyright file="GpuObservationController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------
namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Business.ExceptionBusiness;
    using Business.GpuObservation;
    using Helpers;
    using Models.VO.Airport;
    using Resources;    
    using VOI.SISAC.Business.Dto.Airports;
    using Web.Controllers;

    /// <summary>
    /// Gpu Observation Controller
    /// </summary>
    [CustomAuthorize]
    public class GpuObservationController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(GpuObservationController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.GpuObservationTitle;

        /// <summary>
        /// Interface
        /// </summary>
        private readonly IGpuObservationBusiness GpuObservationBusiness;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="GpuObservationBusiness"></param>
        public GpuObservationController(IGpuObservationBusiness GpuObservationBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.GpuObservationBusiness = GpuObservationBusiness;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "GPUOBS-IDX")]
        public ActionResult Index()
        {
            IList<GpuObservationDto> gpuObservation = new List<GpuObservationDto>();
            IList<GpuObservationVO> gpuVo = new List<GpuObservationVO>();
            try
            {
                gpuObservation = GpuObservationBusiness.GetActivesGpuObservations();
                gpuVo = Mapper.Map<IList<GpuObservationDto>, IList<GpuObservationVO>>(gpuObservation);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(gpuVo);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "GPUOBS-ADD")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create POST
        /// </summary>
        /// <param name="GpuObservation"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "GPUOBS-ADD")]
        public ActionResult Create(GpuObservationVO GpuObservation)
        {
            if (GpuObservation == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GpuObservationDto dto = new GpuObservationDto();
            try
            {
                if (ModelState.IsValid)
                {
                    dto = Mapper.Map<GpuObservationVO, GpuObservationDto>(GpuObservation);
                    GpuObservationBusiness.AddGpuObservation(dto);
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
                message = ex.Number == 10 ? string.Format(message, Resource.GpuObservationCode) : message;
                this.ViewBag.ErrorMessage = message;
            }

            return this.View(GpuObservation);
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "GPUOBS-UPD")]
        public ActionResult Edit(string id)
        {
            GpuObservationDto GpuObservation = new GpuObservationDto();
            GpuObservationVO GpuObservationVO = new GpuObservationVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                GpuObservation = GpuObservationBusiness.FindGpuObservationById(id);
                if (GpuObservation == null)
                {
                    return this.HttpNotFound();
                }
                GpuObservationVO = Mapper.Map<GpuObservationDto, GpuObservationVO>(GpuObservation);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }

            return this.View(GpuObservationVO);
        }

        /// <summary>
        /// Edit POST
        /// </summary>
        /// <param name="GpuObservation"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "GPUOBS-UPD")]
        public ActionResult Edit(GpuObservationVO GpuObservation)
        {
            if (GpuObservation == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GpuObservationDto dto = new GpuObservationDto();
            try
            {
                if (ModelState.IsValid)
                {
                    dto = Mapper.Map<GpuObservationVO, GpuObservationDto>(GpuObservation);
                    GpuObservationBusiness.UpdateGpuObservation(dto);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return View(GpuObservation);
        }

        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "GPUOBS-VIE")]
        public ActionResult Details(string id)
        {
            GpuObservationDto GpuObservation = new GpuObservationDto();
            try
            {
                GpuObservation = GpuObservationBusiness.FindGpuObservationById(id);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return View(GpuObservation);
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "GPUOBS-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GpuObservationDto GpuObservation = new GpuObservationDto();
            GpuObservationVO GpuObservationVo = new GpuObservationVO();
            try
            {
                GpuObservation = GpuObservationBusiness.FindGpuObservationById(id);
                if (GpuObservation == null)
                {
                    return this.HttpNotFound();
                }

                GpuObservationVo = Mapper.Map<GpuObservationDto, GpuObservationVO>(GpuObservation);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            
            return View(GpuObservationVo);
        }

        /// <summary>
        /// Delete POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "GPUOBS-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GpuObservationDto GpuObservation = new GpuObservationDto();
            GpuObservationVO GpuObservationVo = new GpuObservationVO();
            try
            {
                GpuObservation = GpuObservationBusiness.FindGpuObservationById(id);
                if (GpuObservation == null)
                {
                    return this.HttpNotFound();
                }

                GpuObservationVo = Mapper.Map<GpuObservationDto, GpuObservationVO>(GpuObservation);
                GpuObservationBusiness.DeleteGpuObservation(GpuObservation);
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

            return this.View(GpuObservationVo);
        }
    }
}