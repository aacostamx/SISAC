//------------------------------------------------------------------------
// <copyright file="TimelineController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Itineraries.Controllers
{
    using AutoMapper;
    using Business.Dto.Itineraries;
    using Business.ExceptionBusiness;
    using Business.Itineraries;
    using Helpers;
    using Models.VO.Itineraries;
    using Newtonsoft.Json;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Web.Controllers;

    /// <summary>
    /// TimelineController class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    public class TimelineController : BaseController
    {
        #region Properties
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(TimelineController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = Resource.Timeline;

        /// <summary>
        /// The timeline business
        /// </summary>
        private readonly ITimelineBusiness timelineBusiness;

        /// <summary>
        /// The movement business
        /// </summary>
        private readonly ITimelineMovementBusiness movementBusiness;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineController"/> class.
        /// </summary>
        /// <param name="timelineBusiness">The timeline business.</param>
        /// <param name="movementBusiness">The movement business.</param>
        public TimelineController(ITimelineBusiness timelineBusiness, ITimelineMovementBusiness movementBusiness)
        {
            this.userInfo = string.Format(LogMessages.UserInfo, Environment.UserDomainName, Environment.UserName, Environment.MachineName);
            this.timelineBusiness = timelineBusiness;
            this.movementBusiness = movementBusiness;
        }

        #endregion

        #region Controllers
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "TIMELINE-IDX")]
        public ActionResult Index()
        {
            var timelineURL = new ItineraryVO();
            try
            {
                //Cookie Timeline URL
                timelineURL = this.GetTimelineURL();
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(timelineURL);
        }
        #endregion

        #region Timeline Methods
        /// <summary>
        /// Adds the timeline movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddTimelineMovement(TimelineMovementVO movement)
        {

            //ADD Movement
            //var response = this.movementBusiness.AddTimelineMovement(new TimelineMovementDto()
            //{
            //    *Sequence = 1,
            //    *AirlineCode = "Y4",
            //    *FlightNumber = "111",
            //    *ItineraryKey = "20160226",
            //    *OperationTypeID = 2,
            //    *MovementTypeCode = "AR",
            //    *MovementDate = DateTime.Now,
            //    Position = "1",
            //    Remarks = "INSERT TEST - 1 Y4 111 20160226 2 AR NOW 1"
            //});

            var sucess = false;

            if (movement == null || string.IsNullOrEmpty(movement.ItineraryKey))
            {
                return Json(sucess);
            }

            try
            {
                sucess = this.movementBusiness.AddTimelineMovement(Mapper.Map<TimelineMovementDto>(movement));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return Json(sucess);
        }

        /// <summary>
        /// Updates the timeline movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateTimelineMovement(TimelineMovementVO movement)
        {
            //Update Movement
            //var response = this.movementBusiness.UpdateTimelineMovement(new TimelineMovementDto()
            //{
            //    *ID = 46,
            //    *Sequence = 1,
            //    *AirlineCode = "Y4",
            //    *FlightNumber = "111",
            //    *ItineraryKey = "20160226",
            //    OperationTypeID = 1,
            //    MovementTypeCode = "SC",
            //    MovementDate = new DateTime(2016, 10, 1),
            //    Position = "A1",
            //    Remarks = "Update2 example ID 46, MovementTypeCode was HA,  MovementDate was 2016-10-26 00:00:00.000"
            //});

            var sucess = false;

            if (movement == null || string.IsNullOrEmpty(movement.ItineraryKey))
            {
                return Json(sucess);
            }

            try
            {
                sucess = this.movementBusiness.UpdateTimelineMovement(Mapper.Map<TimelineMovementDto>(movement));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return Json(sucess);
        }

        /// <summary>
        /// Deletes the timeline movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteTimelineMovement(TimelineMovementVO movement)
        {
            //Delete Movement
            //var response = this.movementBusiness.DeleteTimelineMovement(new TimelineMovementDto()
            //{
            //    ID = 50
            //});

            var sucess = false;

            if (movement == null || movement.ID < 0)
            {
                return Json(sucess);
            }

            try
            {
                sucess = this.movementBusiness.DeleteTimelineMovement(Mapper.Map<TimelineMovementDto>(movement));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return Json(sucess);
        }

        /// <summary>
        /// Starts the timeline procress.
        /// </summary>
        /// <param name="timelineProcess">The timeline process.</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult StartTimelineProcress(TimelineVO timelineProcess)
        {
            var sucess = false;

            try
            {
                sucess = this.timelineBusiness.TimelineStartProcress(timelineProcess.StartDate, timelineProcess.EndDate);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return Json(sucess);
        }

        /// <summary>
        /// Gets the timeline by flight.
        /// </summary>
        /// <param name="timeline">The timeline.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTimelineByFlight(TimelineVO timeline)
        {
            //GetTimelineByFlight
            var response = this.timelineBusiness.GetTimelineByFlight(new TimelineDto()
            {
                Sequence = 1,
                AirlineCode = "Y4",
                FlightNumber = "826",
                ItineraryKey = "20160809"
            });

            var flights = new TimelineDto();
            var json = string.Empty;

            if (timeline == null || string.IsNullOrEmpty(timeline.ItineraryKey))
            {
                return Json(json);
            }

            try
            {
                flights = this.timelineBusiness.GetTimelineByFlight(Mapper.Map<TimelineDto>(timeline));
                json = JsonConvert.SerializeObject(flights, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return Json(json);
        }

        /// <summary>
        /// Gets the timeline by equipment number.
        /// </summary>
        /// <param name="itinerary">The itinerary.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTimelineByEquipmentNumber(ItineraryVO itinerary)
        {
            //Get by Equiment Number
            //var timeline = this.timelineBusiness.GetTimelineByEquipmentNumber(new TimelineDto() { Itinerary = new ItineraryDto { EquipmentNumber = "N513VL" } });

            var flights = new List<TimelineDto>();
            var json = string.Empty;

            if (itinerary == null || string.IsNullOrEmpty(itinerary.EquipmentNumber))
            {
                return Json(json);
            }

            try
            {
                flights = this.timelineBusiness.GetTimelineByEquipmentNumber(new TimelineDto() { Itinerary = new ItineraryDto { EquipmentNumber = itinerary.EquipmentNumber } }).ToList();
                json = JsonConvert.SerializeObject(flights.OrderBy(c => c.Itinerary.DepartureDate).Take(20), Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return Json(json);
        }

        /// <summary>
        /// Itineary URL
        /// </summary>
        /// <param name="itineary"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ItinearyURL(ItineraryVO itineary)
        {
            var sucess = false;
            var json = string.Empty;
            var encrypt = string.Empty;

            if (itineary == null || string.IsNullOrEmpty(itineary.ItineraryKey))
            {
                return Json(sucess);
            }

            try
            {
                json = JsonConvert.SerializeObject(itineary, Formatting.None);
                //encrypt = EncryptHelper.EncryptString(json);
                this.RemoveCookie("TimelineURL");
                var cookie = new HttpCookie("TimelineURL", json)
                {
                    Path = FormsAuthentication.FormsCookiePath,
                    Expires = DateTime.Now.AddDays(1),
                    HttpOnly = true
                };
                this.HttpContext.Response.Cookies.Add(cookie);
                sucess = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }

            return Json(sucess);

        }

        /// <summary>
        /// Gets the timeline URL.
        /// </summary>
        /// <returns></returns>
        private ItineraryVO GetTimelineURL()
        {
            var itinearyURL = new ItineraryVO();

            if (HttpContext.Request.Cookies["TimelineURL"] != null)
            {
                var TimelineURL = HttpContext.Request.Cookies["TimelineURL"];
                //var decrypt = EncryptHelper.DecryptString(TimelineURL.Value);
                itinearyURL = JsonConvert.DeserializeObject<ItineraryVO>(TimelineURL.Value);
            }

            return itinearyURL;
        }

        /// <summary>
        /// Removes the cookie.
        /// </summary>
        /// <param name="cookieName">Name of the cookie.</param>
        private void RemoveCookie(string cookieName)
        {
            try
            {
                if (Request.Cookies[cookieName] != null)
                {
                    var c = new HttpCookie(cookieName);
                    c.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(c);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorAuthenticate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorAuthenticate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the timeline movement.
        /// </summary>
        /// <param name="movementVO">The movement vo.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTimelineMovement(TimelineMovementVO movementVO)
        {
            var movementDto = new TimelineMovementDto();
            var json = string.Empty;

            if (movementVO == null || movementVO.ID < 1)
            {
                return Json(json);
            }

            try
            {
                movementDto = this.movementBusiness.GetTimelineMovement(new TimelineMovementDto { ID = movementVO.ID });
                json = JsonConvert.SerializeObject(movementDto, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return Json(json);
        }
        #endregion
    }
}