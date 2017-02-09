//------------------------------------------------------------------------
// <copyright file="DrinkingWaterController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{    
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
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
    /// Controller for drinking water catalog
    /// </summary>
    [CustomAuthorize]
    public class DrinkingWaterController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DrinkingWaterController));

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
        private readonly IDrinkingWaterBusiness waterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrinkingWaterController" /> class.
        /// </summary>
        /// <param name="airplaneService">The airplane business.</param>
        /// <param name="waterService">The airplane type service.</param>
        public DrinkingWaterController(IAirplaneBusiness airplaneService, IDrinkingWaterBusiness waterService)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.airplaneService = airplaneService;
            this.waterService = waterService;
        }

        /// <summary>
        /// Main View.
        /// </summary>
        /// <returns>View for the drinking waters catalog.</returns>
        [CustomAuthorize(Roles = "DRINKWATER-IDX")]
        public ActionResult Index()
        {
            IList<DrinkingWaterDto> waters = new List<DrinkingWaterDto>();
            IList<DrinkingWaterVO> watersVo = new List<DrinkingWaterVO>();
            try
            {
                waters = this.waterService.GetActivesDrinkingWater();
                watersVo = Mapper.Map<IList<DrinkingWaterDto>, IList<DrinkingWaterVO>>(waters);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "DRINKING-WATER", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "DRINKING-WATER", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(watersVo.OrderBy(c => c.EquipmentNumber)
                    .ThenBy(c => c.Value)
                    .ToList());
        }

        /// <summary>
        /// Action create for drinking water.
        /// </summary>
        /// <returns>The Create view.</returns>
        [CustomAuthorize(Roles = "DRINKWATER-ADD")]
        public ActionResult Create()
        {
            try
            {
                this.ViewBag.AirplaneCatalog = this.airplaneService.GetActivesAirplane();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, "DRINKING-WATER", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "DRINKING-WATER", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                this.ViewBag.ErrorMessage = message;
                this.ViewBag.AirplaneCatalog = new List<AirplaneDto>();
            }
            
            return this.View();
        }

        /// <summary>
        /// Action for insert a new record in drinking water catalog by POST
        /// </summary>
        /// <param name="waterVo">Object that contains the form that will be inserted.</param>
        /// <returns>The Create view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "DRINKWATER-ADD")]
        public ActionResult Create(DrinkingWaterVO waterVo)
        {
            if (waterVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.ViewBag.AirplaneCatalog = this.airplaneService.GetActivesAirplane();
                if (this.ModelState.IsValid)
                {
                    DrinkingWaterDto water = Mapper.Map<DrinkingWaterVO, DrinkingWaterDto>(waterVo);
                    this.waterService.AddDrinkingWater(water);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, "DRINKING-WATER", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "DRINKING-WATER", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 11)
                {
                    message = string.Format(message, "Quantity (lts)");
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(waterVo);
        }

        /// <summary>
        /// Action for edit a record in the catalog Airplane.
        /// </summary>
        /// <param name="id">Id of the item that will be edited.</param>
        /// <returns>Returns the the Edit view with the item to be edited.</returns>
        [CustomAuthorize(Roles = "DRINKWATER-UPD")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DrinkingWaterVO waterVo = new DrinkingWaterVO();
            try
            {
                this.ViewBag.AirplaneTypeCatalog = this.airplaneService.GetActivesAirplane();
                DrinkingWaterDto water = this.waterService.FindDrinkingWaterById((long)id);                
                if (water == null)
                {
                    return this.HttpNotFound();
                }

                waterVo = Mapper.Map<DrinkingWaterDto, DrinkingWaterVO>(water);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "DRINKING-WATER", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "DRINKING-WATER", this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.View(waterVo);
        }

        /// <summary>
        /// Action for edit a record in the drinking water catalog by POST
        /// </summary>
        /// <param name="waterVo">Object that contains the form that will be edited.</param>
        /// <returns>If the The Edit View.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "DRINKWATER-UPD")]
        public ActionResult Edit(DrinkingWaterVO waterVo)
        {
            if (waterVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.ViewBag.AirplaneCatalog = this.airplaneService.GetActivesAirplane();
                if (this.ModelState.IsValid)
                {
                    DrinkingWaterDto water = Mapper.Map<DrinkingWaterVO, DrinkingWaterDto>(waterVo);
                    this.waterService.UpdateDrinkingWater(water);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, "DRINKING-WATER", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, "DRINKING-WATER", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 11)
                {
                    message = string.Format(message, "Value");
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(waterVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog drinking water.
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns the the Delete view with the item to be deleted.</returns>
        [CustomAuthorize(Roles = "DRINKWATER-DEL")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DrinkingWaterVO waterVo = new DrinkingWaterVO();
            try
            {
                DrinkingWaterDto water = this.waterService.FindDrinkingWaterById((long)id);                
                if (water == null)
                {
                    return this.HttpNotFound();
                }

                waterVo = Mapper.Map<DrinkingWaterDto, DrinkingWaterVO>(water);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "DRINKING-WATER", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "DRINKING-WATER", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(waterVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog drinking water by POST
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns to the Catalog view.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "DRINKWATER-DEL")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "DRINKING-WATER"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DrinkingWaterVO waterVo = new DrinkingWaterVO();
            try
            {
                DrinkingWaterDto water = this.waterService.FindDrinkingWaterById((long)id);
                waterVo = Mapper.Map<DrinkingWaterDto, DrinkingWaterVO>(water);
                this.waterService.DeleteDrinkingWater(water);
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

            return this.View(waterVo);
        }
    }
}