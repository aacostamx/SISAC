//------------------------------------------------------------------------
// <copyright file="AirportRepository.cs" company="AACOSTA">
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
    using Helpers;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Resources;
    using Business = VOI.SISAC.Business.Airport;

    /// <summary>
    /// Airport Group Class
    /// </summary>
    [CustomAuthorize]
    public class AirportGroupController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AirportGroupController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.AirportGroupTitle;

        /// <summary>
        /// Interface
        /// </summary>
        private readonly Business.IAirportGroupBusiness airportGroupBusiness;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="airportGroupBusiness">The airport group business.</param>
        public AirportGroupController(Business.IAirportGroupBusiness airportGroupBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.airportGroupBusiness = airportGroupBusiness;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "AIRPORTGRP-IDX")]
        public ActionResult Index()
        {
            IList<AirportGroupDto> airportgroupDto = new List<AirportGroupDto>();
            IList<AirportGroupVO> airportgroupVo = new List<AirportGroupVO>();
            try
            {
                airportgroupDto = airportGroupBusiness.GetActivesAirportGroups();
                airportgroupVo = Mapper.Map<IList<AirportGroupDto>, IList<AirportGroupVO>>(airportgroupDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(airportgroupVo);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "AIRPORTGRP-ADD")]
        public ActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Create POST
        /// </summary>
        /// <param name="airportgroup"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPORTGRP-ADD")]
        public ActionResult Create([Bind(Include = "AirportGroupCode,AirportGroupName")] AirportGroupVO airportgroup)
        {
            if (airportgroup == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirportGroupDto airportgroupDto = new AirportGroupDto();
            AirportGroupVO airportgroupVo = new AirportGroupVO();
            try
            {
                if (ModelState.IsValid)
                {
                    airportgroupDto = Mapper.Map<AirportGroupVO, AirportGroupDto>(airportgroup);
                    airportGroupBusiness.AddAirportGroup(airportgroupDto);
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
                message = ex.Number == 10 ? string.Format(message, Resource.AirportGroupCode) : message;
                this.ViewBag.ErrorMessage = message;
            }
            return this.View(airportgroupVo);
        }

        /// <summary>
        /// Edit Action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "AIRPORTGRP-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirportGroupDto airportgroupDto = new AirportGroupDto();
            AirportGroupVO airportgroupVo = new AirportGroupVO();
            try
            {
                airportgroupDto = airportGroupBusiness.FindAirportGroupById(id);
                if (airportgroupDto == null)
                {
                    return this.HttpNotFound();
                }
                airportgroupVo = Mapper.Map<AirportGroupDto, AirportGroupVO>(airportgroupDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }

            return this.View(airportgroupVo);
        }

        /// <summary>
        /// Edit post
        /// </summary>
        /// <param name="airportgroup"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPORTGRP-UPD")]
        public ActionResult Edit([Bind(Include = "AirportGroupCode,AirportGroupName")] AirportGroupVO airportgroup)
        {
            if (airportgroup == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirportGroupDto airportGroupDto = new AirportGroupDto();
            AirportGroupVO airportGroupVo = new AirportGroupVO();
            try
            {
                if (ModelState.IsValid)
                {
                    airportGroupDto = Mapper.Map<AirportGroupVO, AirportGroupDto>(airportgroup);
                    airportGroupBusiness.UpdateAirportGroup(airportGroupDto);
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
            return View(airportGroupVo);
        }

        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "AIRPORTGRP-VIE")]
        public ActionResult Details(string id)
        {
            AirportGroupDto airportGroupDto = new AirportGroupDto();
            AirportGroupVO airportGroupVo = new AirportGroupVO();
            try
            {
                airportGroupDto = airportGroupBusiness.FindAirportGroupById(id);
                airportGroupVo = Mapper.Map<AirportGroupDto, AirportGroupVO>(airportGroupDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return View(airportGroupVo);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "AIRPORTGRP-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirportGroupDto airportGroupDto = new AirportGroupDto();
            AirportGroupVO airportGroupVo = new AirportGroupVO();
            try
            {
                airportGroupDto = airportGroupBusiness.FindAirportGroupById(id);
                if (airportGroupDto == null)
                {
                    return this.HttpNotFound();
                }
                airportGroupVo = Mapper.Map<AirportGroupDto, AirportGroupVO>(airportGroupDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(airportGroupVo);
        }

        /// <summary>
        /// Delete POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPORTGRP-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirportGroupDto airporgrouptDto = new AirportGroupDto();
            AirportGroupVO airportgroupVo = new AirportGroupVO();
            try
            {
                airporgrouptDto = airportGroupBusiness.FindAirportGroupById(id);
                if (airporgrouptDto == null)
                {
                    return this.HttpNotFound();
                }
                airportgroupVo = Mapper.Map<AirportGroupDto, AirportGroupVO>(airporgrouptDto);
                this.airportGroupBusiness.DeleteAirportGroup(airporgrouptDto);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(airportgroupVo);
        }
    }
}