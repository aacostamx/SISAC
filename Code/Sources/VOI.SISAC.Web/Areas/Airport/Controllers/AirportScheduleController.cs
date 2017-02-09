//------------------------------------------------------------------------
// <copyright file="AirportScheduleController.cs" company="AACOSTA">
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
    using VOI.SISAC.Business.Catalog;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Class AirportScheduleController.
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />    
    [Authorize]
    public class AirportScheduleController : BaseController
    {
         /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AirportScheduleController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.AirportScheduleTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = VOI.SISAC.Web.Resources.Resource.AirportScheduleID;


        /// <summary>
        /// Interface for the business operations
        /// </summary>
        private readonly IAirportScheduleBusiness airportScheduleService;

        /// <summary>
        /// The schedule type service
        /// </summary>
        private readonly IScheduleTypeBusiness scheduleTypeService;

        /// <summary>
        /// The generic
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportScheduleController"/> class.
        /// </summary>
        /// <param name="waterService">The water service.</param>
        public AirportScheduleController(IAirportScheduleBusiness airportScheduleService, 
                                         IScheduleTypeBusiness scheduleTypeService,
                                         IGenericCatalogBusiness generic)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.airportScheduleService = airportScheduleService;
            this.scheduleTypeService = scheduleTypeService;
            this.generic = generic;
        }

              
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [CustomAuthorize(Roles = "AIRPSCHE-IDX")]
        public ActionResult Index()
        {
            IList<AirportScheduleDto> airportScheduleDto = new List<AirportScheduleDto>();
            IList<AirportScheduleVO> airportScheduleVo = new List<AirportScheduleVO>();
            try
            {
                airportScheduleDto = this.airportScheduleService.GetActivesAirportSchedules();
                airportScheduleVo = Mapper.Map<IList<AirportScheduleDto>, IList<AirportScheduleVO>>(airportScheduleDto);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airportScheduleVo.OrderBy(c => c.StationCode)
                    .ThenBy(c => c.StartTimeSchedule)
                    .ToList());
        }

        /// <summary>
        /// Action create for airport schedule.
        /// </summary>
        /// <returns>The Create view.</returns>
        [CustomAuthorize(Roles = "AIRPSCHE-ADD")]
        public ActionResult Create()
        {
            try
            {
                //airport and scheduleType
                LoadCatalog();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                this.ViewBag.ErrorMessage = message;
                this.ViewBag.AirplaneCatalog = new List<AirplaneDto>();
            }
            
            return this.View();
        }

        /// <summary>
        /// Action for insert a new record in airport schedule catalog by POST
        /// </summary>
        /// <param name="airportscheduleVo">Object that contains the form that will be inserted.</param>
        /// <returns>The Create view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPSCHE-ADD")]
        public ActionResult Create(AirportScheduleVO airportscheduleVo)
        {
            if (airportscheduleVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                //airport and scheduleType
                LoadCatalog();
                if (this.ModelState.IsValid)
                {
                    AirportScheduleDto schedule = Mapper.Map<AirportScheduleVO, AirportScheduleDto>(airportscheduleVo);
                    this.airportScheduleService.AddAirportSchedule(schedule);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 11)
                {
                    message = string.Format(message, "Schedule Times");
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(airportscheduleVo);
        }

        /// <summary>
        /// Action for edit a record in the catalog Airplane.
        /// </summary>
        /// <param name="id">Id of the item that will be edited.</param>
        /// <returns>Returns the the Edit view with the item to be edited.</returns>
        [CustomAuthorize(Roles = "AIRPSCHE-UPD")]
        public ActionResult Edit(long id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirportScheduleVO scheduleVo = new AirportScheduleVO();
            try
            {
                //airport and scheduleType
                LoadCatalog();
                AirportScheduleDto scheduleDto = this.airportScheduleService.FindAirportScheduleById(id);                
                if (scheduleDto == null)
                {
                    return this.HttpNotFound();
                }

                scheduleVo = Mapper.Map<AirportScheduleDto, AirportScheduleVO>(scheduleDto);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.View(scheduleVo);
        }

        /// <summary>
        /// Action for edit a record in the airport schedule catalog by POST
        /// </summary>
        /// <param name="airportscheduleVo">Object that contains the form that will be edited.</param>
        /// <returns>If the The Edit View.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPSCHE-UPD")]
        public ActionResult Edit(AirportScheduleVO airportscheduleVo)
        {
            if (airportscheduleVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                //airport and scheduleType
                LoadCatalog();
                if (this.ModelState.IsValid)
                {
                    AirportScheduleDto schedule = Mapper.Map<AirportScheduleVO, AirportScheduleDto>(airportscheduleVo);
                    this.airportScheduleService.UpdateAirportSchedule(schedule);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 11)
                {
                    message = string.Format(message, "Value");
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(airportscheduleVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog airport schedule.
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns the the Delete view with the item to be deleted.</returns>
        [CustomAuthorize(Roles = "AIRPSCHE-DEL")]
        public ActionResult Delete(long id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirportScheduleVO scheduleVo = new AirportScheduleVO();
            try
            {
                AirportScheduleDto schedule = this.airportScheduleService.FindAirportScheduleById(id);                
                if (schedule == null)
                {
                    return this.HttpNotFound();
                }

                scheduleVo = Mapper.Map<AirportScheduleDto, AirportScheduleVO>(schedule);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(scheduleVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog airport schedule by POST
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns to the Catalog view.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPSCHE-DEL")]
        public ActionResult DeleteConfirmed(long id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirportScheduleVO scheduleVo = new AirportScheduleVO();
            try
            {
                AirportScheduleDto scheduleDto = this.airportScheduleService.FindAirportScheduleById(id);
                scheduleVo = Mapper.Map<AirportScheduleDto, AirportScheduleVO>(scheduleDto);
                this.airportScheduleService.DeleteAirportSchedule(scheduleDto);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(scheduleVo);
        }

        /// <summary>
        /// Loads the catalog.
        /// </summary>
        private void LoadCatalog()
        {
            this.ViewBag.Airport = this.generic.GetAirportsCatalog();
            this.ViewBag.ScheduleType = this.scheduleTypeService.GetActivesScheduleTypes();
        }
	}
}