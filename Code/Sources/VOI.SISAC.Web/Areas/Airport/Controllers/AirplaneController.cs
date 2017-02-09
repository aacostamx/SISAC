//------------------------------------------------------------------------
// <copyright file="AirplaneController.cs" company="AACOSTA">
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
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Resources;
    using System.Dynamic;
    using Newtonsoft.Json;

    /// <summary>
    /// Controller for Airplane catalog
    /// </summary>
    [CustomAuthorize]
    public class AirplaneController : BaseController
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
        private readonly IAirplaneBusiness airplaneService;

        /// <summary>
        /// Interface for the business operations
        /// </summary>
        private readonly IAirplaneTypeBusiness airplaneTypeService;

        /// <summary>
        /// Interface for the business operations
        /// </summary>
        private readonly IGenericCatalogBusiness genericCatalog;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneController" /> class.
        /// </summary>
        /// <param name="airplaneService">The airplane business.</param>
        /// <param name="airplaneTypeService">The airplane type service.</param>
        /// <param name="genericCatalog">The generic catalog.</param>
        public AirplaneController(
            IAirplaneBusiness airplaneService,
            IAirplaneTypeBusiness airplaneTypeService,
            IGenericCatalogBusiness genericCatalog)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.airplaneService = airplaneService;
            this.airplaneTypeService = airplaneTypeService;
            this.genericCatalog = genericCatalog;
        }

        /// <summary>
        /// Main View
        /// </summary>
        /// <returns>View for the airplane catalog.</returns>
        [CustomAuthorize(Roles = "AIRPLANE-IDX")]
        public ActionResult Index()
        {
            IList<AirplaneDto> airplane = new List<AirplaneDto>();
            IList<AirplaneVO> airplanesVo = new List<AirplaneVO>();
            try
            {
                airplane = this.airplaneService.GetActivesAirplane();
                airplanesVo = Mapper.Map<IList<AirplaneDto>, IList<AirplaneVO>>(airplane);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airplanesVo);
        }

        /// <summary>
        /// Action create for Airplanes.
        /// </summary>
        /// <returns>The Create view.</returns>
        [CustomAuthorize(Roles = "AIRPLANE-ADD")]
        public ActionResult Create()
        {
            try
            {
                this.ViewBag.AirplaneTypeCatalog = this.airplaneTypeService.GetActivesAirplaneType();
                this.ViewBag.AirlineCatalog = this.genericCatalog.GetAirlineCatalog();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, "AIRPLANE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "AIRPLANE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                this.ViewBag.ErrorMessage = message;
                this.ViewBag.AirplaneTypeCatalog = new List<AirplaneTypeDto>();
            }

            return this.View();
        }

        /// <summary>
        /// Action for insert a new record in airport catalog by POST
        /// </summary>
        /// <param name="airplaneVo">The airplane view object.</param>
        /// <returns>The Create view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPLANE-ADD")]
        public ActionResult Create(AirplaneVO airplaneVo)
        {
            if (airplaneVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.ViewBag.AirplaneTypeCatalog = this.airplaneTypeService.GetActivesAirplaneType();
                this.ViewBag.AirlineCatalog = this.genericCatalog.GetAirlineCatalog();
                if (this.ModelState.IsValid)
                {
                    AirplaneDto airplane = Mapper.Map<AirplaneVO, AirplaneDto>(airplaneVo);
                    this.airplaneService.AddAirplane(airplane);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, "AIRPLANE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "AIRPLANE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 10)
                {
                    message = string.Format(message, "Equipment number");
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(airplaneVo);
        }

        /// <summary>
        /// Action for edit a record in the catalog Airplane.
        /// </summary>
        /// <param name="id">Id of the item that will be edited.</param>
        /// <returns>Returns the the Edit view with the item to be edited.</returns>
        [CustomAuthorize(Roles = "AIRPLANE-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirplaneVO airplaneVo = new AirplaneVO();
            try
            {
                this.ViewBag.AirplaneTypeCatalog = this.airplaneTypeService.GetActivesAirplaneType();
                this.ViewBag.AirlineCatalog = this.genericCatalog.GetAirlineCatalog();
                AirplaneDto airplane = this.airplaneService.FindAirplaneById(id);
                if (airplane == null)
                {
                    return this.HttpNotFound();
                }

                airplaneVo = Mapper.Map<AirplaneDto, AirplaneVO>(airplane);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.View(airplaneVo);
        }

        /// <summary>
        /// Action for edit a record in the Airplane catalog by POST
        /// </summary>
        /// <param name="airplaneVo">The airplane view object.</param>
        /// <returns>
        /// The Edit View.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPLANE-UPD")]
        public ActionResult Edit(AirplaneVO airplaneVo)
        {
            if (airplaneVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.ViewBag.AirplaneTypeCatalog = this.airplaneTypeService.GetActivesAirplaneType();
                this.ViewBag.AirlineCatalog = this.genericCatalog.GetAirlineCatalog();
                if (this.ModelState.IsValid)
                {
                    AirplaneDto airplane = Mapper.Map<AirplaneVO, AirplaneDto>(airplaneVo);
                    this.airplaneService.UpdateAirplane(airplane);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, "AIRPLANE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, "AIRPLANE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airplaneVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog Airplane.
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns the the Delete view with the item to be deleted.</returns>
        [CustomAuthorize(Roles = "AIRPLANE-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirplaneVO airplaneVo = new AirplaneVO();
            try
            {
                AirplaneDto airplane = this.airplaneService.FindAirplaneById(id);
                if (airplane == null)
                {
                    return this.HttpNotFound();
                }

                airplaneVo = Mapper.Map<AirplaneDto, AirplaneVO>(airplane);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airplaneVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog Airplane by POST
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns to the Catalog view.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPLANE-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "AIRPLANE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirplaneVO airplaneVo = new AirplaneVO();
            try
            {
                AirplaneDto airplane = this.airplaneService.FindAirplaneById(id);
                airplaneVo = Mapper.Map<AirplaneDto, AirplaneVO>(airplane);
                this.airplaneService.DeleteAirplane(airplane);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, "AIRPLANE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, "AIRPLANE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airplaneVo);
        }

        /// <summary>
        /// Gets the pilots.
        /// </summary>
        /// <returns>Object with the stewardess information.</returns>
        [HttpGet]
        public JsonResult GetAirplaneType(string equipmentNumber)
        {
            string json = null;
            dynamic jsonConvert = new ExpandoObject();
            try
            {
                AirplaneDto airplane = this.airplaneService.FindAirplaneById(equipmentNumber);
                if (airplane != null)
                {
                    AirplaneTypeDto airplaneType = this.airplaneTypeService.FindAirplaneTypeById(airplane.AirplaneModel);
                    jsonConvert.FuelInKg = airplaneType.FuelInKg;
                    jsonConvert.MaximumTakeoffWeight = airplaneType.MaximumTakeoffWeight;
                    jsonConvert.EmptyOperatingWeight = airplaneType.EmptyOperatingWeight;
                    json = JsonConvert.SerializeObject(
                        jsonConvert,
                        Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "AIRPLANE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}