//------------------------------------------------------------------------
// <copyright file="DetectCountryController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Controllers
{
    using Areas.Airport.Controllers;
    using System;
    using System.Diagnostics;
    using System.Web.Mvc;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Web.Models.VO.Itineraries;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// DetectCountryController Class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    public class DetectCountryController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(DetectCountryController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// The airport business
        /// </summary>
        private readonly IAirportBusiness airportBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetectCountryController"/> class.
        /// </summary>
        /// <param name="airportBusiness">The airport business.</param>
        public DetectCountryController(IAirportBusiness airportBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.airportBusiness = airportBusiness;
        }

        /// <summary>
        /// Jets the fuel ticket.
        /// </summary>
        /// <param name="itinerary">The route values.</param>
        public ActionResult JetFuelTicket(ItineraryVO itinerary)
        {
            var national = false;
            var controller = string.Empty;
            var routeValues = new
            {
                sequence = itinerary.Sequence,
                AirlineCode = itinerary.AirlineCode,
                FlightNumber = itinerary.FlightNumber,
                ItineraryKey = itinerary.ItineraryKey,
                OperationTypeName = itinerary.OperationTypeName,
                area = "Airport"
            };

            try
            {
                national = this.IsNationalDepartureStation(itinerary);
                controller = national ? "NationalJetFuelTicket" : "JetFuelTicket";
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return this.RedirectToAction("Index", controller, routeValues);
        }

        /// <summary>
        /// Determinates if the flight is national or international and shows the correct view.
        /// </summary>
        /// <param name="itinerary">The route values.</param>
        public ActionResult OfficialDocumentDeparture(ItineraryVO itinerary)
        {
            bool national = false;
            string controller = string.Empty;
            string action = string.Empty;

            try
            {
                var routeValues = new
                {
                    area = "Itineraries",
                    Sequence = itinerary.Sequence,
                    AirlineCode = itinerary.AirlineCode,
                    FlightNumber = itinerary.FlightNumber,
                    ItineraryKey = itinerary.ItineraryKey,
                };
                national = this.IsNationalStation(itinerary.DepartureStation);
                controller = national ? "ManifestDeparture" : "GendecDeparture";
                action = national ? "Index" : "Create";
                return this.RedirectToAction(action, controller, routeValues);
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.Message, exception);
            }

            return this.RedirectToAction("");
        }

        /// <summary>
        /// Determinates if the flight is national or international and shows the correct view.
        /// </summary>
        /// <param name="itinerary">The route values.</param>
        public ActionResult OfficialDocumentArrival(ItineraryVO itinerary)
        {
            bool national = false;
            string controller = string.Empty;
            string action = string.Empty;

            try
            {
                var routeValues = new
                {
                    area = "Itineraries",
                    Sequence = itinerary.Sequence,
                    AirlineCode = itinerary.AirlineCode,
                    FlightNumber = itinerary.FlightNumber,
                    ItineraryKey = itinerary.ItineraryKey,
                    DepartureStation = itinerary.DepartureStation,
                    ArrivalStation = itinerary.ArrivalStation,
                    EquipmentNumber = itinerary.EquipmentNumber,
                    DepartureDate = itinerary.DepartureDate.ToString("yyyy-MM-dd")
                };
                national = this.IsNationalStation(itinerary.ArrivalStation);
                controller = national ? "ManifestArrival" : "GendecArrival";
                action = "Index";
                return this.RedirectToAction(action, controller, routeValues);
            }
            catch (Exception exception)
            {
                Trace.TraceError(exception.Message, exception);
            }

            return this.RedirectToAction("");
        }

        /// <summary>
        /// Determines whether the departure airport of a itinerary is national.
        /// </summary>
        /// <param name="itinerary">The route values.</param>
        /// <returns><c>true</c> if the station is National otherwise <c>false</c>.</returns>
        private bool IsNationalDepartureStation(ItineraryVO itinerary)
        {
            AirportDto airport = new AirportDto();
            bool national = false;

            try
            {
                airport = this.airportBusiness.FindAirportById(itinerary.DepartureStation);
                if (!string.IsNullOrEmpty(airport.StationCode))
                {
                    national = airport.CountryCode.Equals("MEX") ? true : false;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return national;
        }

        /// <summary>
        /// Determines whether the departure airport of a itinerary is national.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <returns>
        ///   <c>true</c> if the station is National otherwise <c>false</c>.
        /// </returns>
        private bool IsNationalStation(string stationCode)
        {
            AirportDto airport = new AirportDto();
            bool national = false;

            try
            {
                airport = this.airportBusiness.FindAirportById(stationCode);
                if (!string.IsNullOrEmpty(airport.StationCode))
                {
                    national = airport.CountryCode.Equals("MEX") ? true : false;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return national;
        }
    }
}