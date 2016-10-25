//------------------------------------------------------------------------
// <copyright file="ScheduleTypeController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Catalog.Controllers
{
    using AutoMapper;
    using Business.Catalog;
    using Business.Dto.Catalogs;
    using Business.ExceptionBusiness;
    using Helpers;
    using Models.VO.Catalog;
    using Models.VO.Finance;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using Web.Controllers;

    /// <summary>
    /// ScheduleTypeController Class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    [CustomAuthorize]
    public class ScheduleTypeController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ScheduleTypeController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = "Pendiente Agregar Recurso";

        /// <summary>
        /// Interface for Airport service contract operations
        /// </summary>
        private readonly IScheduleTypeBusiness scheduleBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleTypeController"/> class.
        /// </summary>
        /// <param name="scheduleBusiness">The exchange business.</param>
        public ScheduleTypeController(IScheduleTypeBusiness scheduleBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.scheduleBusiness = scheduleBusiness;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "SCHETYPE-IDX")]
        public ActionResult Index()
        {
            var schedule = new List<ScheduleTypeVO>();
            try
            {
                var scheduleDto = this.scheduleBusiness.GetActivesScheduleTypes();
                schedule = Mapper.Map<List<ScheduleTypeVO>>(scheduleDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(schedule);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "SCHETYPE-ADD")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the specified schedule vo.
        /// </summary>
        /// <param name="scheduleVO">The schedule vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "SCHETYPE-ADD")]
        public ActionResult Create(ScheduleTypeVO scheduleVO)
        {
            var scheduleDto = new ScheduleTypeDto();

            if (scheduleVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    scheduleDto.ScheduleTypeCode = scheduleVO.ScheduleTypeCode;
                    scheduleDto.ScheduleTypeName = scheduleVO.ScheduleTypeName;
                    scheduleDto = Mapper.Map<ScheduleTypeDto>(scheduleVO);
                    this.scheduleBusiness.AddScheduleType(scheduleDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                message = ex.Number == 10 ? string.Format(message, Resource.ExchangeRatesTitle) : message;
                this.ViewBag.ErrorMessage = message;
            }

            return this.View(scheduleVO);
        }

        /// <summary>
        /// Edits the specified schedule type code.
        /// </summary>
        /// <param name="ScheduleTypeCode">The schedule type code.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "SCHETYPE-UPD")]
        public ActionResult Edit(string ScheduleTypeCode)
        {
            var scheduleVO = new ScheduleTypeVO();

            if (string.IsNullOrEmpty(ScheduleTypeCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                scheduleVO = Mapper.Map<ScheduleTypeVO>(this.scheduleBusiness.FindScheduleType(new ScheduleTypeDto(ScheduleTypeCode)));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(scheduleVO);
        }

        /// <summary>
        /// Edits the specified schedule vo.
        /// </summary>
        /// <param name="scheduleVO">The schedule vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "SCHETYPE-UPD")]
        public ActionResult Edit(ScheduleTypeVO scheduleVO)
        {
            if (scheduleVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    var scheduleDto = Mapper.Map<ScheduleTypeDto>(scheduleVO);
                    this.scheduleBusiness.EditScheduleType(scheduleDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(scheduleVO);
        }

        /// <summary>
        /// Deletes the specified schedule type code.
        /// </summary>
        /// <param name="ScheduleTypeCode">The schedule type code.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "SCHETYPE-DEL")]
        public ActionResult Delete(string ScheduleTypeCode)
        {
            var scheduleVO = new ScheduleTypeVO();

            if (string.IsNullOrEmpty(ScheduleTypeCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                scheduleVO = Mapper.Map<ScheduleTypeVO>(this.scheduleBusiness.FindScheduleType(new ScheduleTypeDto(ScheduleTypeCode)));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(scheduleVO);
        }

        /// <summary>
        /// Deletes the specified schedule vo.
        /// </summary>
        /// <param name="scheduleVO">The schedule vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "SCHETYPE-DEL")]
        public ActionResult Delete(ScheduleTypeVO scheduleVO)
        {
            var scheduleDto = new ScheduleTypeDto();

            if (scheduleVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                scheduleDto = this.scheduleBusiness.FindScheduleType(new ScheduleTypeDto(scheduleVO.ScheduleTypeCode));

                if (scheduleDto != null)
                {
                    this.scheduleBusiness.DeleteScheduleType(scheduleDto);
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(scheduleVO);
        }
    }
}