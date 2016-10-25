//------------------------------------------------------------------------
// <copyright file="GpuController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    using Business.ExceptionBusiness;
    using Business.Gpu;
    using Models.VO.Airport;
    using Resources;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Web.Helpers;
    using Web.Controllers;

    /// <summary>
    /// GpuController class
    /// </summary>
    [CustomAuthorize]
    public class GpuController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(GpuController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.GpuTitle;

        /// <summary>
        /// Gpu interface
        /// </summary>
        private readonly IGpuBusiness GpuBusiness;

        /// <summary>
        /// Airport interface
        /// </summary>
        private readonly IAirportBusiness AirportBusiness;

        /// <summary>
        /// Gpu constructor
        /// </summary>
        /// <param name="GpuBusiness"></param>
        /// <param name="AirportBusiness"></param>
        public GpuController(IGpuBusiness GpuBusiness, IAirportBusiness AirportBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.GpuBusiness = GpuBusiness;
            this.AirportBusiness = AirportBusiness;
        }

        /// <summary>
        /// Index action
        /// </summary>
        /// <returns>View</returns>
        [CustomAuthorize(Roles = "GPU-IDX")]
        public ActionResult Index()
        {
            IList<GpuDto> gpu = new List<GpuDto>();
            IList<GpuVO> gpuVo = new List<GpuVO>();
            try
            {
                gpu = GpuBusiness.GetActivesGpus();
                gpuVo = Mapper.Map<IList<GpuDto>, IList<GpuVO>>(gpu);
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
        /// Create action
        /// </summary>
        /// <returns>View</returns>
        [CustomAuthorize(Roles = "GPU-ADD")]
        public ActionResult Create()
        {
            IList<AirportDto> airportDto = new List<AirportDto>();
            GpuModelVO GpuViewModel = new GpuModelVO();
            GpuViewModel.Airports = new List<AirportVO>();

            try
            {
                airportDto = AirportBusiness.GetActivesAirports();
                GpuViewModel.Airports = Mapper.Map<IList<AirportDto>, IList<AirportVO>>(airportDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return View(GpuViewModel);
        }

        /// <summary>
        /// Post action
        /// </summary>
        /// <param name="ViewGpu"></param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "GPU-ADD")]
        public ActionResult Create(GpuModelVO ViewGpu)
        {
            if (ViewGpu == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GpuDto gpuDto = new GpuDto();
            try
            {
                if (ModelState.IsValid)
                {
                    gpuDto = Mapper.Map<GpuVO, GpuDto>(ViewGpu.Gpu);
                    this.GpuBusiness.AddGpu(gpuDto);
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
                message = ex.Number == 10 ? string.Format(message, Resource.GpuCode) : message;
                this.ViewBag.ErrorMessage = message;
            }
            ViewGpu.Airports = Mapper.Map<IList<AirportDto>, IList<AirportVO>>(AirportBusiness.GetActivesAirports());
            return this.View(ViewGpu);
        }

        /// <summary>
        /// Edit Action
        /// </summary>
        /// <param name="id">Gpu</param>
        /// <returns>View</returns>
        [CustomAuthorize(Roles = "GPU-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GpuModelVO GpuView = new GpuModelVO();
            try
            {
                GpuView.Gpu = Mapper.Map<GpuDto, GpuVO>(GpuBusiness.FindGpuById(id));
                if (GpuView.Gpu == null)
                {
                    return this.HttpNotFound();
                }
                GpuView.Airports = Mapper.Map<IList<AirportDto>, IList<AirportVO>>(AirportBusiness.GetActivesAirports());
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                GpuView.Airports = Mapper.Map<IList<AirportDto>, IList<AirportVO>>(AirportBusiness.GetActivesAirports());
            }
            return this.View(GpuView);
        }

        /// <summary>
        /// Edit post action
        /// </summary>
        /// <param name="Gpu">Gpu object</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "GPU-UPD")]
        public ActionResult Edit(GpuVO Gpu)
        {
            if (Gpu == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GpuDto dto = new GpuDto();
            try
            {
                if (ModelState.IsValid)
                {
                    dto = Mapper.Map<GpuVO, GpuDto>(Gpu);
                    this.GpuBusiness.UpdateGpu(dto);
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
            }
            return this.View(Gpu);
        }

        /// <summary>
        /// Details action
        /// </summary>
        /// <param name="id">gpu</param>
        /// <returns>View</returns>
        [CustomAuthorize(Roles = "GPU-VIE")]
        public ActionResult Details(string id)
        {
            GpuDto dto = new GpuDto();
            try
            {
                dto = GpuBusiness.FindGpuById(id);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return View(dto);
        }

        /// <summary>
        /// Delete action
        /// </summary>
        /// <param name="id">gpu</param>
        /// <returns>View</returns>
        [CustomAuthorize(Roles = "GPU-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GpuDto gpu = new GpuDto();
            GpuModelVO gpuVo = new GpuModelVO();
            try
            {
                gpu = this.GpuBusiness.FindGpuById(id);
                if (gpu == null)
                {
                    return this.HttpNotFound();
                }
                gpuVo.Gpu = Mapper.Map<GpuDto, GpuVO>(gpu);
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
        /// Delete post action
        /// </summary>
        /// <param name="id">Gpu</param>
        /// <returns>View</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "GPU-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GpuDto Gpu = new GpuDto();
            GpuVO GpuVo = new GpuVO();
            try
            {
                Gpu = GpuBusiness.FindGpuById(id);
                if (Gpu == null)
                {
                    return this.HttpNotFound();
                }
                GpuVo = Mapper.Map<GpuDto, GpuVO>(Gpu);
                this.GpuBusiness.DeleteGpu(Gpu);
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
            return this.View(Gpu);
        }
    }
}