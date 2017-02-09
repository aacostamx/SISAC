//------------------------------------------------------------------------
// <copyright file="AircraftMovementMessageController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Itineraries.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Itineraries;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Models.VO.Itineraries;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Aircraft movement message controller
    /// </summary>
    [CustomAuthorize]
    public class AircraftMovementMessageController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ManifestDepartureController));

        /// <summary>
        /// The MVT business
        /// </summary>
        private readonly IAircraftMovementMessageBusiness mvtBusiness;

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string moduleName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AircraftMovementMessageController" /> class.
        /// </summary>
        /// <param name="mvtBusiness">The aircraft movement message business.</param>
        public AircraftMovementMessageController(IAircraftMovementMessageBusiness mvtBusiness)
        {
            this.mvtBusiness = mvtBusiness;
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.moduleName = "MVT Controller";
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="itinerary">The itinerary.</param>
        /// <returns>
        /// View with the aircraft movement message.
        /// </returns>
        [HttpGet]
        public JsonResult GetAircraftMovementMessage(ItineraryVO itinerary)
        {
            AircraftMovementMessageDto message = new AircraftMovementMessageDto();
            try
            {
                message = this.mvtBusiness.GetAircraftMovementMessage(
                    itinerary.Sequence,
                    itinerary.AirlineCode,
                    itinerary.FlightNumber,
                    itinerary.ItineraryKey);

                return this.Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return null;
            }
        }
    }
}