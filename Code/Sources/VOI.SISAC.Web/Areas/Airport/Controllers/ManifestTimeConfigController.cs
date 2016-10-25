//------------------------------------------------------------------------
// <copyright file="ManifestTimeConfigController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using AutoMapper;
    using Business.Airport;
    using Business.Dto.Airports;
    using Business.ExceptionBusiness;
    using Helpers;
    using Models.VO.Airport;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using Web.Controllers;

    /// <summary>
    /// ManifestTimeConfig Controller 
    /// </summary>
    [CustomAuthorize]
    public class ManifestTimeConfigController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ManifestTimeConfigController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.ManifestTimeConfigTitle;

        /// <summary>
        /// Manifest interface
        /// </summary>
        private readonly IManifestTimeConfigBusiness ManifestTimeConfigBusiness;

        /// <summary>
        /// Airline interface
        /// </summary>
        private readonly IAirlineBusiness AirlineBusiness;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ManifestTimeConfigBusiness"></param>
        /// <param name="AirlineBusiness"></param>
        public ManifestTimeConfigController(IManifestTimeConfigBusiness ManifestTimeConfigBusiness, IAirlineBusiness AirlineBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.ManifestTimeConfigBusiness = ManifestTimeConfigBusiness;
            this.AirlineBusiness = AirlineBusiness;
        }

        /// <summary>
        /// Index action
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "MANTIMECON-IDX")]
        public ActionResult Index()
        {
            IList<ManifestTimeConfigDto> manifestConfig = new List<ManifestTimeConfigDto>();
            IList<ManifestTimeConfigVO> manifestConfigVo = new List<ManifestTimeConfigVO>();
            try
            {
                manifestConfig = ManifestTimeConfigBusiness.GetActivesManifestTimeConfigs();
                manifestConfigVo = Mapper.Map<IList<ManifestTimeConfigDto>, IList<ManifestTimeConfigVO>>(manifestConfig);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(manifestConfigVo);
        }

        /// <summary>
        /// Create action
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "MANTIMECON-ADD")]
        public ActionResult Create()
        {
            ManifestTimeConfigModelVO ManifestViewModel = new ManifestTimeConfigModelVO();
            ManifestViewModel.AirlineVO = new List<AirlineVO>();
            IList<AirlineDto> AirlineDto = new List<AirlineDto>();
            try
            {
                if (ModelState.IsValid)
                {
                    AirlineDto = AirlineBusiness.GetActivesAirline();
                    ManifestViewModel.AirlineVO = Mapper.Map<IList<AirlineDto>, IList<AirlineVO>>(AirlineDto);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(ManifestViewModel);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Manifest"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "MANTIMECON-ADD")]
        public ActionResult Create(ManifestTimeConfigModelVO Manifest)
        {
            if (Manifest == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ManifestTimeConfigDto ManifestDto = new ManifestTimeConfigDto();
            try
            {
                if (ModelState.IsValid)
                {
                    ManifestDto = Mapper.Map<ManifestTimeConfigVO, ManifestTimeConfigDto>(Manifest.ManifestVO);
                    ManifestTimeConfigBusiness.AddManifestTimeConfig(ManifestDto);
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
                message = ex.Number == 10 ? string.Format(message, Resource.ManifestTimeConfigID) : message;
                this.ViewBag.ErrorMessage = message;
                Manifest.AirlineVO = Mapper.Map<IList<AirlineDto>, IList<AirlineVO>>(AirlineBusiness.GetActivesAirline());
            }

            return this.View(Manifest);
        }

        /// <summary>
        /// Edit action
        /// </summary>
        /// <param name="id">id manifest</param>
        /// <returns>View</returns>
        [CustomAuthorize(Roles = "MANTIMECON-UPD")]
        public ActionResult Edit(long id)
        {
            if (id <= 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ManifestTimeConfigModelVO model = new ManifestTimeConfigModelVO();
            ManifestTimeConfigDto dto = new ManifestTimeConfigDto();
            try
            {
                dto = ManifestTimeConfigBusiness.FindManifestTimeConfigById(id);
                if (dto == null)
                {
                    return this.HttpNotFound();
                }

                model.ManifestVO = Mapper.Map<ManifestTimeConfigDto, ManifestTimeConfigVO>(dto);
                model.AirlineVO = Mapper.Map<IList<AirlineDto>, IList<AirlineVO>>(AirlineBusiness.GetActivesAirline());
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                model.AirlineVO = Mapper.Map<IList<AirlineDto>, IList<AirlineVO>>(AirlineBusiness.GetActivesAirline());
            }

            return View(model);
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="manifest"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "MANTIMECON-UPD")]
        public ActionResult Edit(ManifestTimeConfigModelVO manifest)
        {
            if (manifest == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ManifestTimeConfigDto dto = new ManifestTimeConfigDto();
            try
            {
                if (ModelState.IsValid)
                {
                    dto = Mapper.Map<ManifestTimeConfigVO, ManifestTimeConfigDto>(manifest.ManifestVO);
                    ManifestTimeConfigBusiness.UpdateManifestTimeConfig(dto);
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
                manifest.AirlineVO = Mapper.Map<IList<AirlineDto>, IList<AirlineVO>>(AirlineBusiness.GetActivesAirline());
            }

            return View(manifest);
        }

        /// <summary>
        /// Details view
        /// </summary>
        /// <param name="id">id manifest</param>
        /// <returns>View</returns>
        [CustomAuthorize(Roles = "MANTIMECON-VIE")]
        public ActionResult Details(long id)
        {
            ManifestTimeConfigDto manifest = new ManifestTimeConfigDto();
            try
            {
                manifest = ManifestTimeConfigBusiness.FindManifestTimeConfigById(id);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return View(manifest);
        }

        /// <summary>
        /// Delete Action
        /// </summary>
        /// <param name="id">id manifest</param>
        /// <returns>View</returns>
        [CustomAuthorize(Roles = "MANTIMECON-DEL")]
        public ActionResult Delete(long id)
        {
            if (id <= 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ManifestTimeConfigDto manifest = new ManifestTimeConfigDto();
            ManifestTimeConfigVO manifestVO = new ManifestTimeConfigVO();
            try
            {
                manifest = ManifestTimeConfigBusiness.FindManifestTimeConfigById(id);
                if (manifest == null)
                {
                    return this.HttpNotFound();
                }

                manifestVO = Mapper.Map<ManifestTimeConfigDto, ManifestTimeConfigVO>(manifest);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(manifestVO);
        }

        /// <summary>
        /// Post delete action
        /// </summary>
        /// <param name="id">manifest</param>
        /// <returns>View</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "MANTIMECON-DEL")]
        public ActionResult DeleteConfirmed(long id)
        {
            if (id <= 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ManifestTimeConfigDto manifest = new ManifestTimeConfigDto();
            ManifestTimeConfigVO manifestVO = new ManifestTimeConfigVO();
            try
            {
                manifest = ManifestTimeConfigBusiness.FindManifestTimeConfigById(id);
                if (manifest == null)
                {
                    return this.HttpNotFound();
                }

                manifestVO = Mapper.Map<ManifestTimeConfigDto, ManifestTimeConfigVO>(manifest);
                ManifestTimeConfigBusiness.DeleteManifestTimeConfig(manifest);
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

            return this.View(manifest);
        }
    }
}