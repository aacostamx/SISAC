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
            //Sino tiene permiso para editar aeropuerto
            if (NotContainsAirport(departure))
            {
                return RedirectToAction("Unauthorized", "Home", new { area = "" });
            }

            PassengerInformationVO passengerInformationVO = new PassengerInformationVO();
            passengerInformationVO = this.PassengerInformationDetails(sequence, airlineCode, flightNumber, itineraryKey);
            passengerInformationVO.AppliesPreviousFlight = false;

            //Debe ser origen nacional y destino internacional para poder aplizar regla de TUA internacional anterior en otros exentos de actual
            if (this.IsMexicanAirport(passengerInformationVO.Departure) && this.IsInternationalAirport(passengerInformationVO.Arrival)) 
            {
                passengerInformationVO.AppliesPreviousFlight = true;
                int sequenceDefault = passengerInformationVO.PreviousSequence ?? 0;

                if (!string.IsNullOrEmpty(passengerInformationVO.PreviousAirlineCode)
                 && !string.IsNullOrEmpty(passengerInformationVO.PreviousFlightNumber)
                 && !string.IsNullOrEmpty(passengerInformationVO.PreviousItineraryKey))
                {
                    PassengerInformationDto passengerDto = new PassengerInformationDto();
                    passengerDto = this.passengerInformationBusiness.FindById(sequenceDefault, passengerInformationVO.PreviousAirlineCode, passengerInformationVO.PreviousFlightNumber, passengerInformationVO.PreviousItineraryKey);
                    if (passengerDto != null)
                    {
                        if (passengerInformationVO.Other < passengerDto.InternationalTua)
                        {
                            passengerInformationVO.Other = passengerDto.InternationalTua;
                        }
                    }                    
                }
            }

            if (passengerInformationVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SetSiteMapValues(passengerInformationVO, departure);
            return this.View(passengerInformationVO);
        }

        /// <summary>
        /// Edits the specified passenger information vo.
        /// </summary>
        /// <param name="passengerInformationVO">The passenger information vo.</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "PASSINFO-UPD")]
        public ActionResult Edit(PassengerInformationVO passengerInformationVO)
        {
            IList<string> errors = new List<string>();
            PassengerInformationVO passengerInformationVODetails = new PassengerInformationVO();

            if (passengerInformationVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.ViewBag.Users = this.genericCatalogBusiness.GetUserCatalog();
                PassengerInformationDto passengerInformationDto = new PassengerInformationDto();
                passengerInformationDto = Mapper.Map<PassengerInformationVO, PassengerInformationDto>(passengerInformationVO);
                bool isMexicanAirport = this.IsMexicanAirport(passengerInformationVO.Departure);
                bool isInternationalAirport = this.IsInternationalAirport(passengerInformationVO.Arrival);
                errors = this.passengerInformationBusiness.ValidatePassengerInformation(passengerInformationDto, isMexicanAirport, isInternationalAirport);
                if (errors.Count == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                }
                else
                {
                    this.TempData["ListErrorMessage"] = errors;
                }

                passengerInformationVODetails = this.PassengerInformationDetails(
                    passengerInformationVO.Sequence,
                    passengerInformationVO.AirlineCode,
                    passengerInformationVO.FlightNumber,
                    passengerInformationVO.ItineraryKey);
            }
            catch (BusinessException exception)
            {
                this.ViewBag.Users = this.genericCatalogBusiness.GetUserCatalog();
                Logger.Error(string.Format(LogMessages.ErrorUpdate, "PASSENGER INFORMATION", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, "PASSENGER INFORMATION", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.RedirectToAction("Index", passengerInformationVODetails);
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
                else
                {
                    passengerInformationVO = this.PassengerInformationDetails(sequence, airlineCode, flightNumber, itineraryKey);
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
        /// Passengers the information details.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>Passenger Information View Object.</returns>
        private PassengerInformationVO PassengerInformationDetails(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            PassengerInformationVO passengerInformationVO = new PassengerInformationVO();
            GenericCatalogDto dataFounded = new GenericCatalogDto();
            ItineraryDto itineraryDto = new ItineraryDto();
            bool isNational = false;
            bool isClose = false;
            bool isMexicanAirport = false;

            try
            {
                // Gets the Itinerary
                itineraryDto = this.itineraryBusiness.FindFlightById(sequence, airlineCode, flightNumber, itineraryKey);
                if (itineraryDto != null)
                {
                    // If Itinerary is found
                    if (itineraryDto.PassengerInformation != null)
                    {
                        // If Itinerary has Passenger Information only do the mapping
                        passengerInformationVO = Mapper.Map<PassengerInformationDto, PassengerInformationVO>(itineraryDto.PassengerInformation);

                        // Sets Information for the flight string
                        passengerInformationVO.ItineraryVo = new ItineraryVO();
                        passengerInformationVO.ItineraryVo.AirlineCode = itineraryDto.AirlineCode;
                        passengerInformationVO.ItineraryVo.FlightNumber = itineraryDto.FlightNumber;
                        passengerInformationVO.ItineraryVo.EquipmentNumber = itineraryDto.EquipmentNumber;
                        passengerInformationVO.ItineraryVo.DepartureDate = itineraryDto.DepartureDate;
                        passengerInformationVO.ItineraryVo.ArrivalDate = itineraryDto.ArrivalDate;
                        passengerInformationVO.ItineraryVo.DepartureTime = itineraryDto.DepartureTime;
                        passengerInformationVO.ItineraryVo.ArrivalTime = itineraryDto.ArriveTime;
                        passengerInformationVO.ItineraryVo.DepartureStation = itineraryDto.DepartureStation;
                        passengerInformationVO.ItineraryVo.ArrivalStation = itineraryDto.ArrivalStation;

                        // Use to know the station
                        passengerInformationVO.Departure = itineraryDto.DepartureStation;
                        passengerInformationVO.Arrival = itineraryDto.ArrivalStation;
                        passengerInformationVO.DepartureDate = itineraryDto.DepartureDate.ToString("yyyy-MM-dd");
                        passengerInformationVO.TimeDeparture = itineraryDto.DepartureDate.Hour + ":" + itineraryDto.DepartureDate.Minute;
                    }
                    else
                    {
                        // If not, mapping manually
                        passengerInformationVO.Sequence = sequence;
                        passengerInformationVO.AirlineCode = airlineCode;
                        passengerInformationVO.FlightNumber = flightNumber;
                        passengerInformationVO.ItineraryKey = itineraryKey;

                        // Sets Information for the flight string
                        passengerInformationVO.ItineraryVo = new ItineraryVO();
                        passengerInformationVO.ItineraryVo.AirlineCode = itineraryDto.AirlineCode;
                        passengerInformationVO.ItineraryVo.FlightNumber = itineraryDto.FlightNumber;
                        passengerInformationVO.ItineraryVo.EquipmentNumber = itineraryDto.EquipmentNumber;
                        passengerInformationVO.ItineraryVo.DepartureDate = itineraryDto.DepartureDate;
                        passengerInformationVO.ItineraryVo.ArrivalDate = itineraryDto.ArrivalDate;
                        passengerInformationVO.ItineraryVo.DepartureTime = itineraryDto.DepartureTime;
                        passengerInformationVO.ItineraryVo.ArrivalTime = itineraryDto.ArriveTime;
                        passengerInformationVO.ItineraryVo.DepartureStation = itineraryDto.DepartureStation;
                        passengerInformationVO.ItineraryVo.ArrivalStation = itineraryDto.ArrivalStation;

                        // Use to know the station
                        passengerInformationVO.Departure = itineraryDto.DepartureStation;
                        passengerInformationVO.Arrival = itineraryDto.ArrivalStation;
                        passengerInformationVO.DepartureDate = itineraryDto.DepartureDate.ToString("yyyy-MM-dd");
                        passengerInformationVO.TimeDeparture = itineraryDto.DepartureDate.Hour + ":" + itineraryDto.DepartureDate.Minute;
                    }

                    // Sets flags for customazing the view
                    isNational = this.IsNationalFlight(itineraryDto.DepartureStation, itineraryDto.ArrivalStation);
                    isClose = itineraryDto.GendecDepartureIsClose;
                    isMexicanAirport = this.IsMexicanAirport(itineraryDto.DepartureStation);
                }
                else
                {
                    // If not found retruns null
                    return null;
                }

                // USUARIOS ASC
                IList<GenericCatalogDto> userListFinal = new List<GenericCatalogDto>();

                // Asignamos a combo los proveedores viables
                userListFinal = this.genericCatalogBusiness.GetUserCatalog(itineraryDto.DepartureStation, "ASC");
                this.ViewBag.Users = userListFinal;
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
            this.ViewBag.Itinerary = itineraryDto;
            return passengerInformationVO;
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
        /// Determines whether [is international airport] [the specified station].
        /// </summary>
        /// <param name="station">The station.</param>
        /// <returns></returns>
        private bool IsInternationalAirport(string station)
        {
            AirportDto airport = this.airportBusiness.FindAirportById(station);
            bool isInternational = airport.Country.CountryCode != "MEX";

            return isInternational;
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