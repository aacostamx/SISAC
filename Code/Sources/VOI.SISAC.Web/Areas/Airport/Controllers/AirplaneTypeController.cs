//------------------------------------------------------------------------
// <copyright file="AirplaneTypeController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Controller for Airplane Type section
    /// </summary>
    [CustomAuthorize]
    public class AirplaneTypeController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AirplaneController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Interface for the business operations
        /// </summary>
        private readonly IAirplaneTypeBusiness airplaneTypeService;

        /// <summary>
        /// Interface for the business operations
        /// </summary>
        private readonly ICompartmentTypeBusiness compartmentTypeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneTypeController" /> class.
        /// </summary>
        /// <param name="airplaneTypeService">The airplane type service.</param>
        /// <param name="compartmentTypeService">The compartment type service.</param>
        public AirplaneTypeController(IAirplaneTypeBusiness airplaneTypeService, ICompartmentTypeBusiness compartmentTypeService)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.airplaneTypeService = airplaneTypeService;
            this.compartmentTypeService = compartmentTypeService;
        }

        /// <summary>
        /// Main View
        /// </summary>
        /// <returns>View for the airplane types catalog.</returns>
        [CustomAuthorize(Roles = "AIRPLANETY-IDX")]
        public ActionResult Index()
        {
            IList<AirplaneTypeVO> airplaneTypesVo = new List<AirplaneTypeVO>();
            IList<AirplaneTypeDto> airplaneTypes = new List<AirplaneTypeDto>();
            try
            {
                airplaneTypes = this.airplaneTypeService.GetActivesAirplaneType();
                airplaneTypesVo = Mapper.Map<IList<AirplaneTypeDto>, IList<AirplaneTypeVO>>(airplaneTypes);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE-TYPE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE-TYPE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airplaneTypesVo);
        }

        /// <summary>
        /// Action create for Airplanes Types.
        /// </summary>
        /// <returns>The Create view.</returns>
        [CustomAuthorize(Roles = "AIRPLANETY-ADD")]
        public ActionResult Create()
        {
            this.ViewBag.CompartmentType = this.compartmentTypeService.GetActiveCompartmentType();
            return this.View();
        }

        /// <summary>
        /// Action for insert a new record in airport type catalog by POST
        /// </summary>
        /// <param name="airplaneTypeVo">Object that contains the form that will be inserted.</param>
        /// <returns>The Create view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPLANETY-ADD")]
        public ActionResult Create(AirplaneTypeVO airplaneTypeVo)
        {
            if (airplaneTypeVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.ViewBag.CompartmentType = this.compartmentTypeService.GetActiveCompartmentType();
                if (this.ModelState.IsValid)
                {
                    AirplaneTypeDto airplaneType = Mapper.Map<AirplaneTypeVO, AirplaneTypeDto>(airplaneTypeVo);
                    this.airplaneTypeService.AddAirplaneType(airplaneType);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, "AIRPLANE-TYPE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "AIRPLANE-TYPE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 10)
                {
                    message = string.Format(message, "Airplane model");
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(airplaneTypeVo);
        }

        /// <summary>
        /// Action for edit a record in the catalog Airplane Type.
        /// </summary>
        /// <param name="id">Id of the item that will be edited.</param>
        /// <returns>Returns the the Edit view with the item to be edited.</returns>
        [CustomAuthorize(Roles = "AIRPLANETY-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE", this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirplaneTypeVO airplaneTypeVo = new AirplaneTypeVO();
            try
            {
                this.ViewBag.CompartmentType = this.compartmentTypeService.GetActiveCompartmentType();
                AirplaneTypeDto airplaneType = this.airplaneTypeService.FindAirplaneTypeById(id);
                if (airplaneType == null)
                {
                    return this.HttpNotFound();
                }

                airplaneTypeVo = Mapper.Map<AirplaneTypeDto, AirplaneTypeVO>(airplaneType);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE-TYPE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE-TYPE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.View(airplaneTypeVo);
        }

        /// <summary>
        /// Action for edit a record in the catalog Airplane Type by POST
        /// </summary>
        /// <param name="airplaneTypeVo">Object that contains the form that will be edited.</param>
        /// <returns>If the The Edit View.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPLANETY-UPD")]
        public ActionResult Edit(AirplaneTypeVO airplaneTypeVo)
        {
            if (airplaneTypeVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.ViewBag.CompartmentType = this.compartmentTypeService.GetActiveCompartmentType();
                if (this.ModelState.IsValid)
                {
                    AirplaneTypeDto airplaneType = Mapper.Map<AirplaneTypeVO, AirplaneTypeDto>(airplaneTypeVo);
                    this.airplaneTypeService.UpdateAirplaneType(airplaneType);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, "AIRPLANE-TYPE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, "AIRPLANE-TYPE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airplaneTypeVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog Airplane Type
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns the the Delete view with the item to be deleted.</returns>
        [CustomAuthorize(Roles = "AIRPLANETY-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirplaneTypeVO airplaneTypeVo = new AirplaneTypeVO();
            try
            {
                AirplaneTypeDto airplaneType = this.airplaneTypeService.FindAirplaneTypeById(id);
                if (airplaneType == null)
                {
                    return this.HttpNotFound();
                }

                airplaneTypeVo = Mapper.Map<AirplaneTypeDto, AirplaneTypeVO>(airplaneType);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE-TYPE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE-TYPE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airplaneTypeVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog Airplane Type by POST
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns to the Catalog view.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPLANETY-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE-TYPE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirplaneTypeVO airplaneTypeVo = new AirplaneTypeVO();
            try
            {
                AirplaneTypeDto airplaneType = this.airplaneTypeService.FindAirplaneTypeById(id);
                this.airplaneTypeService.DeleteAirplaneType(airplaneType);
                airplaneTypeVo = Mapper.Map<AirplaneTypeDto, AirplaneTypeVO>(airplaneType);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, "AIRPLANE-TYPE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, "AIRPLANE-TYPE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airplaneTypeVo);
        }
    }
}