//------------------------------------------------------------------------
// <copyright file="GendecArrivalController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Itineraries.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Business.Dto.Catalogs;
    using Business.Dto.Itineraries;
    using Business.Dto.Security;
    using Business.Security;
    using Helpers;
    using Models.VO.Airport;
    using Models.VO.Itineraries;
    using MvcSiteMapProvider;
    using Resources;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Itineraries;
    using Web.Controllers;

    /// <summary>
    /// Arrival general document controller
    /// </summary>
    [CustomAuthorize]
    public class GendecArrivalController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(GendecArrivalController));

        /// <summary>
        /// The catalog name
        /// </summary>
        private readonly string catalogName = "GendecArrival";

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// The general document arrival business/
        /// </summary>
        private readonly IGendecArrivalBusiness gendecArrivalBusiness;

        /// <summary>
        /// The crew business
        /// </summary>
        private readonly ICrewBusiness crewBusiness;

        /// <summary>
        /// The itinerary business
        /// </summary>
        private readonly IItineraryBusiness itineraryBusiness;

        /// <summary>
        /// The generic catalog business
        /// </summary>
        private readonly IGenericCatalogBusiness genericCatalogBusiness;

        /// <summary>
        /// The user business
        /// </summary>
        private readonly IUserBusiness userBusiness;

        /// <summary>
        /// The page report business
        /// </summary>
        private readonly IPageReportBusiness pageReportBusiness;

        /// <summary>
        /// The airplane business
        /// </summary>
        private readonly IAirplaneBusiness airplaneBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="GendecArrivalController" /> class.
        /// </summary>
        /// <param name="gendecArrivalBusiness">The general document arrival business.</param>
        /// <param name="crewBusiness">The crew business.</param>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        /// <param name="genericCatalogBusiness">The generic catalog business.</param>
        /// <param name="userBusiness">The user business.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        /// <param name="airplaneBusiness">The airplane business.</param>
        public GendecArrivalController(
            IGendecArrivalBusiness gendecArrivalBusiness,
            ICrewBusiness crewBusiness,
            IItineraryBusiness itineraryBusiness,
            IGenericCatalogBusiness genericCatalogBusiness,
            IUserBusiness userBusiness,
            IPageReportBusiness pageReportBusiness,
            IAirplaneBusiness airplaneBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);

            this.gendecArrivalBusiness = gendecArrivalBusiness;
            this.crewBusiness = crewBusiness;
            this.itineraryBusiness = itineraryBusiness;
            this.genericCatalogBusiness = genericCatalogBusiness;
            this.userBusiness = userBusiness;
            this.pageReportBusiness = pageReportBusiness;
            this.airplaneBusiness = airplaneBusiness;
        }

        /// <summary>
        /// Main view.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="departureStation">The departure station.</param>
        /// <param name="arrivalStation">The arrival station.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <returns>Action result.</returns>
        public ActionResult Index(
            int sequence,
            string airlineCode,
            string flightNumber,
            string itineraryKey,
            string departureStation,
            string arrivalStation,
            string equipmentNumber,
            string departureDate)
        {
            if (string.IsNullOrWhiteSpace(airlineCode)
                || string.IsNullOrWhiteSpace(flightNumber)
                || string.IsNullOrWhiteSpace(itineraryKey))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirportDto airportDto = new AirportDto();
            GendecArrivalVO gendecArrivalVO = new GendecArrivalVO();
            GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
            this.ViewBag.OperationTypeName = "LLEGADA";

            try
            {
                // Checks if the user is allowed to enter the GENDEC
                if (this.NotContainsAirport(arrivalStation))
                {
                    return this.RedirectToAction("Unauthorized", "Home", new { area = string.Empty });
                }

                // Loads catalog's data
                this.LoadCatalog(arrivalStation);

                // Gets the GENDEC information
                gendecArrivalDto = this.gendecArrivalBusiness.GetGendecArrival(sequence, airlineCode, flightNumber, itineraryKey);
                AirplaneDto airplaneInfo = this.airplaneBusiness.FindAirplaneById(equipmentNumber);

                // Gets the airplane information for client validations
                gendecArrivalDto.Itinerary.Airplane = airplaneInfo != null ? airplaneInfo : new AirplaneDto();
                gendecArrivalVO = Mapper.Map<GendecArrivalDto, GendecArrivalVO>(gendecArrivalDto);
                return this.View("Create", gendecArrivalVO);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                System.Diagnostics.Trace.TraceError(exception.InnerException.ToString());
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View("Create");
            }
        }

        /// <summary>
        /// Performs the action.
        /// </summary>
        /// <param name="gendecArrivalVo">The general document information.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "GENDECARR-UPD")]
        [CustomAuthorize(Roles = "GENDECARR-OPEN")]
        [CustomAuthorize(Roles = "GENDECARR-CLOSE")]
        public ActionResult PerformAction(GendecArrivalVO gendecArrivalVo)
        {
            if (gendecArrivalVo == null
                || string.IsNullOrWhiteSpace(gendecArrivalVo.AirlineCode)
                || string.IsNullOrWhiteSpace(gendecArrivalVo.FlightNumber)
                || string.IsNullOrWhiteSpace(gendecArrivalVo.Itinerarykey)
                || gendecArrivalVo.Action < 1
                || gendecArrivalVo.Action > 3)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            switch (gendecArrivalVo.Action)
            {
                case 1:

                    // Saves the manifest
                    this.Save(gendecArrivalVo);
                    break;
                case 2:

                    // Closes the manifest
                    this.Close(gendecArrivalVo);
                    break;
                case 3:

                    // Opens the manifest
                    this.Open(gendecArrivalVo);
                    break;
            }

            var routeValues = new
            {
                Sequence = gendecArrivalVo.Sequence,
                AirlineCode = gendecArrivalVo.AirlineCode,
                FlightNumber = gendecArrivalVo.FlightNumber,
                ItineraryKey = gendecArrivalVo.Itinerarykey,
                departureStation = gendecArrivalVo.DepartureStation,
                arrivalStation = gendecArrivalVo.ArrivalStation,
                equipmentNumber = gendecArrivalVo.EquipmentNumber
            };
            return this.RedirectToAction("Index", routeValues);
        }

        /// <summary>
        /// Gets the warning message configuration.
        /// </summary>
        /// <returns>Json result.</returns>
        public JsonResult GetWarningMessageConfiguration()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("Title", Resource.Warning);
            dictionary.Add("AlreadyExistCrew", Resource.AlreadyExistCrew);
            dictionary.Add("AlreadyExistSteward", Resource.AlreadyExistSteward);
            dictionary.Add("ChoiceCrew", Resource.ChoiceCrew);
            dictionary.Add("ChoiceSteward", Resource.ChoiceSteward);
            dictionary.Add("RequiredField", Resource.RequiredField);
            dictionary.Add("MaxLength8Val", Resource.LengthMax8);
            dictionary.Add("NotExistCrews", Resource.NotExistCrews);
            dictionary.Add("NotExistSteward", Resource.NotExistSteward);
            dictionary.Add("requiredCombo", Resource.SelectItem);
            dictionary.Add("rangePax", Resource.rangePax);
            dictionary.Add("rangeCrew", Resource.rangeCrew);
            dictionary.Add("NotCloseGendec", Resource.NotCloseGendec);
            dictionary.Add("NotEditGendec", Resource.NotEditGendec);

            return this.Json(dictionary, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Prints the specified sequence.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="departureStation">The departure station.</param>
        /// <param name="arrivalStation">The arrival station.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "GENDECARR-PRINTREP")]
        public ActionResult Print(
            int sequence,
            string airlineCode,
            string flightNumber,
            string itineraryKey,
            string departureStation,
            string arrivalStation,
            string equipmentNumber,
            string departureDate)
        {
            GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
            GendecArrivalVO gendecArrivalVO = new GendecArrivalVO();
            try
            {
                string reportPath = string.Empty;
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("GendecArrival");
                SetSiteMapValues(sequence, airlineCode, flightNumber, itineraryKey, departureStation, arrivalStation, equipmentNumber, departureDate);

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath)
                    && !string.IsNullOrEmpty(airlineCode.ToString())
                    && !string.IsNullOrEmpty(flightNumber.ToString())
                    && !string.IsNullOrEmpty(itineraryKey.ToString())
                    && sequence != 0)
                {
                    ReportingServiceViewModel reportServicelModel = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("Sequence", sequence.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("AirlineCode", airlineCode, false),
                            new Microsoft.Reporting.WebForms.ReportParameter("FlightNumber", flightNumber, false),
                            new Microsoft.Reporting.WebForms.ReportParameter("ItineraryKey", itineraryKey, false)
                        });

                    reportServicelModel.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("Gendec_Arrival").Url;
                    return this.View("Report/ViewReport", reportServicelModel);
                }
                else
                {
                    gendecArrivalDto = this.gendecArrivalBusiness.GetGendecArrival(sequence, airlineCode, flightNumber, itineraryKey);
                    gendecArrivalVO = Mapper.Map<GendecArrivalDto, GendecArrivalVO>(gendecArrivalDto);
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
                "Create", 
                new
                {
                    Sequence = gendecArrivalVO.Sequence,
                    AirlineCode = gendecArrivalVO.AirlineCode,
                    FlightNumber = gendecArrivalVO.FlightNumber,
                    ItineraryKey = gendecArrivalVO.Itinerarykey
                });
        }

        /// <summary>
        /// Set Site Map Values.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="departureStation">The departure station.</param>
        /// <param name="arrivalStation">The arrival station.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="departureDate">The departure date.</param>
        private static void SetSiteMapValues(
            int sequence,
            string airlineCode,
            string flightNumber,
            string itineraryKey,
            string departureStation,
            string arrivalStation,
            string equipmentNumber,
            string departureDate)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Gendec_Arrival");
                if (node != null)
                {
                    node.RouteValues["Sequence"] = sequence;
                    node.RouteValues["AirlineCode"] = airlineCode;
                    node.RouteValues["FlightNumber"] = flightNumber;
                    node.RouteValues["ItineraryKey"] = itineraryKey;
                    node.RouteValues["ArrivalStation"] = arrivalStation;
                    node.RouteValues["DepartureStation"] = departureStation;
                    node.RouteValues["EquipmentNumber"] = equipmentNumber;
                    node.RouteValues["DepartureDate"] = departureDate;
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
                Trace.TraceError(exception.Message, exception);
            }
        }

        /// <summary>
        /// Load ComboBox
        /// </summary>
        /// <param name="arrivalStation">The arrival station.</param>
        private void LoadCatalog(string arrivalStation)
        {
            // USUARIOS ASC
            IList<GenericCatalogDto> userListFinal = new List<GenericCatalogDto>();

            // Asignamos a combo los proveedores viables
            userListFinal = this.genericCatalogBusiness.GetUserCatalog(arrivalStation, "ASC");
            this.ViewBag.Users = userListFinal;
        }

        /////// <summary>
        /////// Gets the information manifiest.
        /////// </summary>
        /////// <param name="Sequence">The sequence.</param>
        /////// <param name="AirlineCode">The airline code.</param>
        /////// <param name="FlightNumber">The flight number.</param>
        /////// <param name="ItineraryKey">The itinerary key.</param>
        /////// <param name="DepartureStation">The departure station.</param>
        /////// <param name="ArrivalStation">The arrival station.</param>
        /////// <param name="EquipmentNumber">The equipment number.</param>
        /////// <param name="DepartureDate">The departure date.</param>
        /////// <returns></returns>
        ////private GendecArrivalVO GetInformationManifiest(
        ////    int Sequence,
        ////    string AirlineCode,
        ////    string FlightNumber,
        ////    string ItineraryKey,
        ////    string DepartureStation,
        ////    string ArrivalStation,
        ////    string EquipmentNumber,
        ////    string DepartureDate)
        ////{
        ////    CrewDto crewDto = new CrewDto();
        ////    CrewVO crewVO = new CrewVO();
        ////    GendecArrivalVO gendecArrivalVO = new GendecArrivalVO();
        ////    gendecArrivalVO.Crews = new List<CrewVO>();

        ////    WebServiceSisaRequestVO webServiceSisaRequestVO = new WebServiceSisaRequestVO();
        ////    webServiceSisaRequestVO.DepartureStation = DepartureStation;
        ////    webServiceSisaRequestVO.ArrivalStation = ArrivalStation;
        ////    webServiceSisaRequestVO.FlightNumber = FlightNumber;
        ////    webServiceSisaRequestVO.MaticulaAeronave = EquipmentNumber;
        ////    webServiceSisaRequestVO.DepartureDate = Convert.ToDateTime(DepartureDate).ToString("yyyyMMdd");

        ////    ResponseWebServiceSisaVO responseWebServiceSisaVO = ManifiestNationalSisa.ManifiestNationalGet(webServiceSisaRequestVO);

        ////    if (responseWebServiceSisaVO.Result == "OK")
        ////    {
        ////        gendecArrivalVO.TotalPax = responseWebServiceSisaVO.Data.PassengerAdultNumber + responseWebServiceSisaVO.Data.PassengerChildrenNumber + responseWebServiceSisaVO.Data.PassengerInfantNumber;
        ////        gendecArrivalVO.TotalCrew = responseWebServiceSisaVO.Data.CommandConf + responseWebServiceSisaVO.Data.StewarsConf;
        ////        gendecArrivalVO.Sequence = Sequence;
        ////        gendecArrivalVO.AirlineCode = AirlineCode;
        ////        gendecArrivalVO.FlightNumber = FlightNumber;
        ////        gendecArrivalVO.Itinerarykey = ItineraryKey;

        ////        string[] crewArray = responseWebServiceSisaVO.Data.TripulacionesID.Split(',');
        ////        string[] stewarsArray = responseWebServiceSisaVO.Data.SobrecargosID.Split(',');

        ////        foreach (string crewNumberEmployee in crewArray)
        ////        {
        ////            crewDto = this.crewBusiness.FindCrewByEmployeeNumber(crewNumberEmployee);
        ////            crewVO = Mapper.Map<CrewDto, CrewVO>(crewDto);

        ////            gendecArrivalVO.Crews.Add(crewVO);
        ////        }

        ////        foreach (string stewarNumberEmployee in stewarsArray)
        ////        {
        ////            crewDto = this.crewBusiness.FindCrewByEmployeeNumber(stewarNumberEmployee);
        ////            crewVO = Mapper.Map<CrewDto, CrewVO>(crewDto);

        ////            gendecArrivalVO.Crews.Add(crewVO);
        ////        }
        ////    }

        ////    gendecArrivalVO.Itinerary = GetItinerary(Sequence, AirlineCode, FlightNumber, ItineraryKey);
        ////    return gendecArrivalVO;
        ////}

        /////// <summary>
        /////// Gets the itinerary.
        /////// </summary>
        /////// <param name="Sequence">The sequence.</param>
        /////// <param name="AirlineCode">The airline code.</param>
        /////// <param name="FlightNumber">The flight number.</param>
        /////// <param name="ItineraryKey">The itinerary key.</param>
        /////// <returns>Itinerary view object.</returns>
        ////private ItineraryVO GetItinerary(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey)
        ////{
        ////    ItineraryDto itineraryDto = new ItineraryDto();
        ////    ItineraryVO itineraryVO = new ItineraryVO();

        ////    itineraryDto = this.itineraryBusiness.FindFlightById(Sequence, AirlineCode, FlightNumber, ItineraryKey);
        ////    itineraryVO = Mapper.Map<ItineraryDto, ItineraryVO>(itineraryDto);

        ////    return itineraryVO;
        ////}

        /// <summary>
        /// Gets the crews by identifier.
        /// </summary>
        /// <param name="gendecArrivalVO">The general document arrival vo.</param>
        /// <returns>General Arrival View Object.</returns>
        private GendecArrivalVO GetCrewsByID(GendecArrivalVO gendecArrivalVO)
        {
            CrewVO crewVO = new CrewVO();

            if (gendecArrivalVO.Crews != null)
            {
                List<CrewVO> crewVOS = gendecArrivalVO.Crews.ToList();
                gendecArrivalVO.Crews = new List<CrewVO>();
                foreach (CrewVO item in crewVOS)
                {
                    crewVO = Mapper.Map<CrewDto, CrewVO>(this.crewBusiness.GetAllCrewByID(item.CrewID));
                    gendecArrivalVO.Crews.Add(crewVO);
                }
            }

            return gendecArrivalVO;
        }

        /// <summary>
        /// Creates the specified general document arrival.
        /// </summary>
        /// <param name="gendecArrivalVO">The general document arrival view object.</param>
        private void Save(GendecArrivalVO gendecArrivalVO)
        {
            try
            {
                IList<string> errors = new List<string>();
                GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();

                gendecArrivalVO = this.GetCrewsByID(gendecArrivalVO);
                gendecArrivalDto = Mapper.Map<GendecArrivalVO, GendecArrivalDto>(gendecArrivalVO);
                errors = this.gendecArrivalBusiness.ValidateGendecArrivalInformation(gendecArrivalDto);
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
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
        }

        /// <summary>
        /// Closes the general document.
        /// </summary>
        /// <param name="gendecArrivalVO">The general document arrival.</param>
        private void Close(GendecArrivalVO gendecArrivalVO)
        {
            try
            {
                GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
                gendecArrivalDto = Mapper.Map<GendecArrivalVO, GendecArrivalDto>(gendecArrivalVO);
                this.gendecArrivalBusiness.CloseGendecArrival(gendecArrivalDto);
                this.TempData["OperationSuccess"] = Resource.SuccessInactive;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
        }

        /// <summary>
        /// Opens the general document.
        /// </summary>
        /// <param name="gendecArrivalVO">The general document arrival vo.</param>
        private void Open(GendecArrivalVO gendecArrivalVO)
        {
            try
            {
                GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
                gendecArrivalDto = Mapper.Map<GendecArrivalVO, GendecArrivalDto>(gendecArrivalVO);
                this.gendecArrivalBusiness.OpenGendecArrival(gendecArrivalDto);
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessInactive;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
        }

        /// <summary>
        /// Verify if the user has permission to enter the airport.
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