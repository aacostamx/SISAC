//------------------------------------------------------------------------
// <copyright file="PassengerInformationController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.Reporting.WebForms;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Itineraries;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Models.VO.Itineraries;
    using VOI.SISAC.Web.Resources;
    using MvcSiteMapProvider;

    /// <summary>
    /// Passenger Information Controller
    /// </summary>
    [CustomAuthorize]
    public class PassengerInformationController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(PassengerInformationController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// The passenger information business
        /// </summary>
        private readonly IPassengerInformationBusiness passengerInformationBusiness;

        /// <summary>
        /// The itinerary business
        /// </summary>
        private readonly IItineraryBusiness itineraryBusiness;

        /// <summary>
        /// The generic catalog business
        /// </summary>
        private readonly IGenericCatalogBusiness genericCatalogBusiness;

        /// <summary>
        /// The generic catalog business
        /// </summary>
        private readonly IAirportBusiness airportBusiness;

        /// <summary>
        /// The user business
        /// </summary>
        private readonly IUserBusiness userBusiness;

        /// <summary>
        /// The page report business
        /// </summary>
        private readonly IPageReportBusiness pageReportBusiness;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.PassengerInformation;

        /// <summary>
        /// Initializes a new instance of the <see cref="PassengerInformationController" /> class.
        /// </summary>
        /// <param name="passengerInformationBusiness">The passenger information business.</param>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        /// <param name="genericCatalogBusiness">generic catalog</param>
        /// <param name="airportBusiness">The airport business.</param>
        /// <param name="userBusiness">User business</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        public PassengerInformationController(
            IPassengerInformationBusiness passengerInformationBusiness,
            IItineraryBusiness itineraryBusiness,
            IGenericCatalogBusiness genericCatalogBusiness,
            IAirportBusiness airportBusiness,
            IUserBusiness userBusiness,
            IPageReportBusiness pageReportBusiness)
        {
            this.userInfo = string.Format(
            LogMessages.UserInfo,
            Environment.UserDomainName,
            Environment.UserName,
            Environment.MachineName);
            this.passengerInformationBusiness = passengerInformationBusiness;
            this.itineraryBusiness = itineraryBusiness;
            this.genericCatalogBusiness = genericCatalogBusiness;
            this.airportBusiness = airportBusiness;
            this.userBusiness = userBusiness;
            this.pageReportBusiness = pageReportBusiness;
        }

        /// <summary>
        /// Indexes the specified sequence.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="departure">The departure station.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "PASSINFO-IDX")]
        public ActionResult Index(int sequence, string airlineCode, string flightNumber, string itineraryKey, string departure)
        {
            if (string.IsNullOrWhiteSpace(airlineCode)
                || string.IsNullOrWhiteSpace(flightNumber)
                || string.IsNullOrWhiteSpace(itineraryKey)
                || string.IsNullOrWhiteSpace(departure))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // If the user is not allowed to access this airport's information
            if (NotContainsAirport(departure))
            {
                return RedirectToAction("Unauthorized", "Home", new { area = "" });
            }

            bool isNational = false;
            bool isClose = false;
            bool isMexicanAirport = false;
            PassengerInformationVO passengerInformationVO = new PassengerInformationVO();
            GenericCatalogDto dataFounded = new GenericCatalogDto();
            PassengerInformationDto passInfo = new PassengerInformationDto();

            try
            {
                // Gets the Itinerary
                passInfo = this.passengerInformationBusiness.FindById(sequence, airlineCode, flightNumber, itineraryKey);

                // If Itinerary is found
                if (passInfo != null && passInfo.Itinerary != null)
                {
                    // If Itinerary has Passenger Information only do the mapping
                    passengerInformationVO = Mapper.Map<PassengerInformationDto, PassengerInformationVO>(passInfo);

                    // Sets Information for the flight
                    passengerInformationVO.ItineraryVo = new ItineraryVO();

                    passengerInformationVO.AdditionalInformation = new AdditionalPassengerInformationVO();
                    passengerInformationVO.ItineraryVo.AirlineCode = passInfo.Itinerary.AirlineCode;
                    passengerInformationVO.ItineraryVo.FlightNumber = passInfo.Itinerary.FlightNumber;
                    passengerInformationVO.ItineraryVo.EquipmentNumber = passInfo.Itinerary.EquipmentNumber;
                    passengerInformationVO.ItineraryVo.DepartureDate = passInfo.Itinerary.DepartureDate;
                    passengerInformationVO.ItineraryVo.ArrivalDate = passInfo.Itinerary.ArrivalDate;
                    passengerInformationVO.ItineraryVo.DepartureTime = passInfo.Itinerary.DepartureTime;
                    passengerInformationVO.ItineraryVo.ArrivalTime = passInfo.Itinerary.ArriveTime;
                    passengerInformationVO.ItineraryVo.DepartureStation = passInfo.Itinerary.DepartureStation;
                    passengerInformationVO.ItineraryVo.ArrivalStation = passInfo.Itinerary.ArrivalStation;

                    // Use to know the station
                    passengerInformationVO.Departure = passInfo.Itinerary.DepartureStation;
                    passengerInformationVO.Arrival = passInfo.Itinerary.ArrivalStation;
                    passengerInformationVO.DepartureDate = passInfo.Itinerary.DepartureDate.ToString("yyyy-MM-dd");
                    passengerInformationVO.TimeDeparture = passInfo.Itinerary.DepartureDate.Hour + ":" + passInfo.Itinerary.DepartureDate.Minute;

                    // Sets the additional information
                    if (passInfo.AdditonalInformation != null)
                    {
                        passengerInformationVO.AdditionalInformation = Mapper.Map<AdditionalPassengerInformationVO>(passInfo.AdditonalInformation);
                    }

                    #region Applies Previous Flight
                    passengerInformationVO.AppliesPreviousFlight = false;

                    // Debe ser origen nacional y destino internacional para poder aplizar regla de TUA internacional anterior en otros exentos de actual
                    if (this.IsMexicanAirport(passengerInformationVO.Departure) && !this.IsMexicanAirport(passengerInformationVO.Arrival))
                    {
                        passengerInformationVO.AppliesPreviousFlight = true;
                        int sequenceDefault = passengerInformationVO.PreviousSequence ?? 0;

                        if (!string.IsNullOrEmpty(passengerInformationVO.PreviousAirlineCode)
                            && !string.IsNullOrEmpty(passengerInformationVO.PreviousFlightNumber)
                            && !string.IsNullOrEmpty(passengerInformationVO.PreviousItineraryKey))
                        {
                            PassengerInformationDto passengerDto = new PassengerInformationDto();
                            passengerDto = this.passengerInformationBusiness.FindById(
                                sequenceDefault,
                                passengerInformationVO.PreviousAirlineCode,
                                passengerInformationVO.PreviousFlightNumber,
                                passengerInformationVO.PreviousItineraryKey);

                            if (passengerDto != null)
                            {
                                if (passengerInformationVO.Other < passengerDto.InternationalTua)
                                {
                                    passengerInformationVO.Other = passengerDto.InternationalTua;
                                }
                            }
                        }
                    }
                    #endregion
                    
                    // Sets flags for customaze the view
                    isNational = this.IsNationalFlight(passInfo.Itinerary.DepartureStation, passInfo.Itinerary.ArrivalStation);
                    isClose = passInfo.Itinerary.GendecDepartureIsClose;
                    isMexicanAirport = this.IsMexicanAirport(passInfo.Itinerary.DepartureStation);

                    // USUARIOS ASC
                    IList<GenericCatalogDto> userListFinal = new List<GenericCatalogDto>();

                    // Asignamos a combo los proveedores viables
                    userListFinal = this.genericCatalogBusiness.GetUserCatalog(passInfo.Itinerary.DepartureStation, "ASC");
                    this.ViewBag.Users = userListFinal;
                }
                else
                {
                    ItineraryDto iti = this.itineraryBusiness.FindItineraryWithPassengerInformation(
                        sequence,
                        airlineCode,
                        flightNumber,
                        itineraryKey);

                    if (iti != null)
                    {
                        passengerInformationVO.Sequence = iti.Sequence;
                        passengerInformationVO.AirlineCode = iti.AirlineCode;
                        passengerInformationVO.FlightNumber = iti.FlightNumber;
                        passengerInformationVO.ItineraryKey = iti.ItineraryKey;

                        passengerInformationVO.Departure = iti.DepartureStation;
                        passengerInformationVO.Arrival = iti.ArrivalStation;
                        passengerInformationVO.DepartureDate = iti.DepartureDate.ToString("yyyy-MM-dd");
                        passengerInformationVO.TimeDeparture = iti.DepartureDate.Hour + ":" + iti.DepartureDate.Minute;

                        // Sets Information for the flight string
                        passengerInformationVO.ItineraryVo = Mapper.Map<ItineraryVO>(iti);

                        // Sets flags for customaze the view
                        isNational = this.IsNationalFlight(iti.DepartureStation, iti.ArrivalStation);
                        isClose = iti.GendecDepartureIsClose;
                        isMexicanAirport = this.IsMexicanAirport(iti.DepartureStation);

                        // USUARIOS ASC
                        IList<GenericCatalogDto> userListFinal = new List<GenericCatalogDto>();

                        // Asignamos a combo los proveedores viables
                        userListFinal = this.genericCatalogBusiness.GetUserCatalog(iti.DepartureStation, "ASC");
                        this.ViewBag.Users = userListFinal;
                    }
                    else
                    {
                        // If passenger information and itinerary do not exit, returns new objects
                        passengerInformationVO.Sequence = sequence;
                        passengerInformationVO.AirlineCode = airlineCode;
                        passengerInformationVO.FlightNumber = flightNumber;
                        passengerInformationVO.ItineraryKey = itineraryKey;

                        // Sets Information for the flight string
                        passengerInformationVO.ItineraryVo = new ItineraryVO();
                    }
                    
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            this.ViewBag.IsNationalFlag = isNational;
            this.ViewBag.IsClose = isClose;
            this.ViewBag.IsMexicanAirport = isMexicanAirport;
            this.ViewBag.Itinerary = passInfo;
            SetSiteMapValues(passengerInformationVO, departure);

            return this.View(passengerInformationVO);
        }

        /// <summary>
        /// Edits the specified passenger information vo.
        /// </summary>
        /// <param name="passengerInformation">The passenger information vo.</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "PASSINFO-UPD")]
        public ActionResult Edit(PassengerInformationVO passengerInformation)
        {
            if (passengerInformation == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PassengerInformationVO passengerInfo = new PassengerInformationVO();
            try
            {
                IList<string> errors = new List<string>();
                this.ViewBag.Users = this.genericCatalogBusiness.GetUserCatalog();
                PassengerInformationDto passengerInformationDto = new PassengerInformationDto();
                passengerInformationDto = Mapper.Map<PassengerInformationVO, PassengerInformationDto>(passengerInformation);
                passengerInformationDto.AdditonalInformation = Mapper.Map<AdditionalPassengerInformationDto>(passengerInformation.AdditionalInformation);
                bool isMexicanAirport = this.IsMexicanAirport(passengerInformation.Departure);
                bool isInternationalAirport = !this.IsMexicanAirport(passengerInformation.Arrival);
                errors = this.passengerInformationBusiness.ValidatePassengerInformation(passengerInformationDto, isMexicanAirport, isInternationalAirport);
                if (errors.Count == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                }
                else
                {
                    this.TempData["ListErrorMessage"] = errors;
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, "PASSENGER INFORMATION", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, "PASSENGER INFORMATION", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
            catch (NullReferenceException exception)
            {
                string error = "Element in the DOM is missing.";
                Logger.Error(string.Format(error, "PASSENGER INFORMATION", this.userInfo));
                Logger.Error(exception.Message + " " + error, exception);
                Trace.TraceError(string.Format(error, "PASSENGER INFORMATION", this.userInfo));
                Trace.TraceError(exception.Message + " " + error, exception);
                this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage();
            }

            var route = new
            {
                Sequence = passengerInformation.Sequence,
                AirlineCode = passengerInformation.AirlineCode,
                FlightNumber = passengerInformation.FlightNumber,
                ItineraryKey = passengerInformation.ItineraryKey,
                Departure = passengerInformation.Departure
            };
            return this.RedirectToAction("Index", route);
        }

        /// <summary>
        /// Gets the information message.
        /// </summary>
        /// <returns>The message to be display.</returns>
        [HttpPost]
        public string GetInformationMessage()
        {
            return Resource.GendecClosedMessage;
        }

        /// <summary>
        /// Print the passenger information report for the flight.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="isNational">if set to <c>true</c> is a national flight otherwise <c>false</c>.</param>
        /// <param name="departure">The departure station. Necessary to set the site map route propertly.</param>
        /// <returns>
        /// Action result
        /// </returns>
        [CustomAuthorize(Roles = "PASSINFO-PRINTREP")]
        public ActionResult Print(int sequence, string airlineCode, string flightNumber, string itineraryKey, bool isNational, string departure)
        {
            if (string.IsNullOrWhiteSpace(airlineCode)
                && string.IsNullOrWhiteSpace(flightNumber)
                && string.IsNullOrWhiteSpace(itineraryKey)
                && string.IsNullOrWhiteSpace(departure)
                && sequence <= 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PassengerInformationVO passengerInformationVO = new PassengerInformationVO();
            try
            {
                string reportPath = string.Empty;
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("PassengerInformation");

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath))
                {
                    ReportingServiceViewModel reportServicelModel = new ReportingServiceViewModel(
                        reportPath,
                        new List<ReportParameter>()
                        {
                            new ReportParameter("Sequence", sequence.ToString(), false),
                            new ReportParameter("AirlineCode", airlineCode, false),
                            new ReportParameter("FlightNumber", flightNumber, false),
                            new ReportParameter("ItineraryKey", itineraryKey, false),
                            new ReportParameter("IsNational", isNational.ToString(), false)
                        });

                    reportServicelModel.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("PassengerInformation_Key").Url;
                    return this.View("Report/ViewReport", reportServicelModel);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.RedirectToAction(
                "Index",
                new
                {
                    sequence = sequence,
                    airlineCode = airlineCode,
                    flightNumber = flightNumber,
                    itineraryKey = itineraryKey,
                    departure = departure
                });
        }

        /// <summary>
        /// Gets the airports.
        /// </summary>
        /// <returns>Object with airports information.</returns>
        public JsonResult GetAirports()
        {
            IList<AirportDto> airports = new List<AirportDto>();
            try
            {
                airports = this.airportBusiness.GetActivesAirports();
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return this.Json(airports, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the international tua.
        /// </summary>
        /// <param name="pSequence">The p sequence.</param>
        /// <param name="pAirlineCode">The p airline code.</param>
        /// <param name="pFlightNumber">The p flight number.</param>
        /// <param name="pItineraryKey">The p itinerary key.</param>
        /// <returns></returns>
        public JsonResult GetInternationalTUA(string pSequence, string pAirlineCode, string pFlightNumber, string pItineraryKey)
        {
            int tuaInternational = -1;
            try
            {
                int sequence = 0;
                bool successfullyParsed = int.TryParse(pSequence, out sequence);

                if (successfullyParsed
                 && !string.IsNullOrEmpty(pAirlineCode)
                 && !string.IsNullOrEmpty(pFlightNumber)
                 && !string.IsNullOrEmpty(pItineraryKey))
                {
                    PassengerInformationDto passengerDto = new PassengerInformationDto();
                    passengerDto = this.passengerInformationBusiness.FindById(sequence, pAirlineCode, pFlightNumber, pItineraryKey);
                    if (passengerDto != null)
                    {

                        tuaInternational = passengerDto.InternationalTua;

                    }
                }

            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return this.Json(tuaInternational, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the previus flight information.
        /// </summary>
        /// <param name="previousSequence">The previous sequence.</param>
        /// <param name="previousAirlineCode">The previous airline code.</param>
        /// <param name="previousFlightNumber">The previous flight number.</param>
        /// <param name="previousItineraryKey">The previous itinerary key.</param>
        /// <returns>The flight information.</returns>
        public JsonResult GetPreviousFlightInformation(int previousSequence, string previousAirlineCode, string previousFlightNumber, string previousItineraryKey)
        {
            ItineraryVO itinerary = new ItineraryVO();

            if (string.IsNullOrEmpty(previousAirlineCode)
                 && !string.IsNullOrEmpty(previousFlightNumber)
                 && !string.IsNullOrEmpty(previousItineraryKey))
            {
                return this.Json(itinerary, JsonRequestBehavior.AllowGet);
            }

            try
            {
                itinerary = Mapper.Map<ItineraryVO>(itineraryBusiness.FindFlightById(previousSequence, previousAirlineCode, previousFlightNumber, previousItineraryKey));
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.Json(itinerary, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sets the site map values.
        /// </summary>
        /// <param name="passengerInformation">The ticket vo.</param>
        private static void SetSiteMapValues(PassengerInformationVO passengerInformation, string departureStation)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("PassengerInformation_Key");
                if (node != null)
                {
                    node.RouteValues["Sequence"] = passengerInformation.Sequence;
                    node.RouteValues["AirlineCode"] = passengerInformation.AirlineCode;
                    node.RouteValues["FlightNumber"] = passengerInformation.FlightNumber;
                    node.RouteValues["ItineraryKey"] = passengerInformation.ItineraryKey;
                    node.RouteValues["Departure"] = departureStation;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Determines whether is a national flight.
        /// </summary>
        /// <param name="departureStation">The departure station.</param>
        /// <param name="arrivalStation">The arrival station.</param>
        /// <returns>
        ///   <c>true</c> if a national flight otherwise false.
        /// </returns>
        private bool IsNationalFlight(string departureStation, string arrivalStation)
        {
            AirportDto arrivalAirport = this.airportBusiness.FindAirportById(arrivalStation);
            AirportDto departureAirport = this.airportBusiness.FindAirportById(departureStation);
            bool isNational = departureAirport.Country.CountryCode == arrivalAirport.Country.CountryCode;

            return isNational;
        }

        /// <summary>
        /// Determines whether the specified station is in Mexico.
        /// </summary>
        /// <param name="station">The station.</param>
        /// <returns><c>true</c> if from Mexico otherwise <c>false</c>.</returns>
        private bool IsMexicanAirport(string station)
        {
            AirportDto airport = this.airportBusiness.FindAirportById(station);
            bool isMexican = airport.Country.CountryCode == "MEX";

            return isMexican;
        }

        /// <summary>
        /// Nots the contains airport.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool NotContainsAirport(string stationCode)
        {
            UserDto userDto = new UserDto();
            List<string> airports = new List<string>();
            userDto = this.userBusiness.GetUserByUserName(this.User.Identity.Name);
            if (userDto.UserAirports != null)
            {
                airports = userDto.UserAirports.Select(c => c.StationCode).ToList();
            }

            if (!airports.Contains(stationCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}