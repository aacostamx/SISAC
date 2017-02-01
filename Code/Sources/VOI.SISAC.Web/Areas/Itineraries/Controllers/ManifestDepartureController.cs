//------------------------------------------------------------------------
// <copyright file="ManifestDepartureController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Itineraries.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Net;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using MvcSiteMapProvider;
    using Newtonsoft.Json;
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

    /// <summary>
    /// Manifest Document Departure Controller
    /// </summary>
    [CustomAuthorize]
    public class ManifestDepartureController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ManifestDepartureController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = "ManifestDeparture";

        /// <summary>
        /// The itinerary business
        /// </summary>
        private readonly IItineraryBusiness itineraryBusiness;

        /// <summary>
        /// Crew Business
        /// </summary>
        private readonly ICrewBusiness crewBusiness;

        /// <summary>
        /// The generic business
        /// </summary>
        private readonly IGenericCatalogBusiness genericBusiness;

        /// <summary>
        /// The manifest departure business
        /// </summary>
        private readonly IManifestDepartureBusiness manifestDepartureBusiness;

        /// <summary>
        /// The airport business
        /// </summary>
        private readonly IAirportBusiness airportBusiness;

        /// <summary>
        /// The airplane business
        /// </summary>
        private readonly IAirplaneBusiness airplaneBusiness;

        /// <summary>
        /// The page report
        /// </summary>
        private readonly IPageReportBusiness pageReportBusiness;

        /// <summary>
        /// The airplane type business
        /// </summary>
        private readonly IAirplaneTypeBusiness airplaneTypeBusiness;


        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestDepartureController"/> class.
        /// </summary>
        /// <param name="crewBusiness">The crew business.</param>
        /// <param name="genericBusiness">The generic business.</param>
        /// <param name="manifestDepartureBusiness">The manifest departure business.</param>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        /// <param name="airportBusiness">The airport business.</param>
        /// <param name="airplaneBusiness">The airplane business.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        /// <param name="airplaneTypeBusiness">The airplane type business.</param>
        public ManifestDepartureController(
            ICrewBusiness crewBusiness,
            IGenericCatalogBusiness genericBusiness,
            IManifestDepartureBusiness manifestDepartureBusiness,
            IItineraryBusiness itineraryBusiness,
            IAirportBusiness airportBusiness,
            IAirplaneBusiness airplaneBusiness,
            IPageReportBusiness pageReportBusiness,
            IAirplaneTypeBusiness airplaneTypeBusiness)
        {
            this.crewBusiness = crewBusiness;
            this.genericBusiness = genericBusiness;
            this.itineraryBusiness = itineraryBusiness;
            this.manifestDepartureBusiness = manifestDepartureBusiness;
            this.airportBusiness = airportBusiness;
            this.airplaneBusiness = airplaneBusiness;
            this.airplaneTypeBusiness = airplaneTypeBusiness;
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.pageReportBusiness = pageReportBusiness;
        }

        /// <summary>
        /// Main view.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns>
        /// Action Result.
        /// </returns>
        [CustomAuthorize(Roles = "MANIFDEP-IDX")]
        public ActionResult Index(ManifestDepartureVO manifest)
        {
            if (manifest == null
                || string.IsNullOrWhiteSpace(manifest.AirlineCode)
                || string.IsNullOrWhiteSpace(manifest.FlightNumber)
                || string.IsNullOrWhiteSpace(manifest.ItineraryKey))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ManifestDepartureVO manifestVo = new ManifestDepartureVO();
            try
            {
                ManifestDepartureDto manifestDto = new ManifestDepartureDto();
                manifestDto = this.manifestDepartureBusiness.GetManifestDepartureForFlight(
                    manifest.Sequence,
                    manifest.AirlineCode,
                    manifest.FlightNumber,
                    manifest.ItineraryKey);
                manifestVo = Mapper.Map<ManifestDepartureVO>(manifestDto);
                this.ModelState.Clear();

                string action = string.Empty;
                action = manifestVo.Closed ? "Closed" : "Index";
                return this.View(action, manifestVo);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            manifestVo.Itinerary = new ItineraryVO();
            manifestVo.Itinerary.AirlineCode = manifest.AirlineCode;
            manifestVo.Itinerary.FlightNumber = manifest.FlightNumber;
            manifestVo.Itinerary.ItineraryKey = manifest.ItineraryKey;
            manifestVo.Itinerary.Sequence = manifest.Sequence;
            return this.View(manifestVo);
        }

        /// <summary>
        /// Performs the action.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "MANIFDEP-UPD")]
        [CustomAuthorize(Roles = "MANIFDEP-OPEN")]
        [CustomAuthorize(Roles = "MANIFDEP-CLOSE")]
        public ActionResult PerformAction(ManifestDepartureVO manifest)
        {
            if (manifest == null
                || string.IsNullOrWhiteSpace(manifest.AirlineCode)
                || string.IsNullOrWhiteSpace(manifest.FlightNumber)
                || string.IsNullOrWhiteSpace(manifest.ItineraryKey)
                || manifest.Action < 1
                || manifest.Action > 3)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            switch (manifest.Action)
            {
                case 1:

                    // Saves the manifest
                    if (this.Save(manifest))
                    {
                        this.TempData["OperationSuccess"] = Resources.Resource.ManifestUpdatedSuccessfully;
                    }

                    break;
                case 2:

                    // Closes the manifest
                    if (this.Close(manifest))
                    {
                        this.TempData["OperationSuccess"] = Resources.Resource.ManifestClosedSuccessfully;
                    }

                    break;
                case 3:

                    // Opens the manifest
                    if (this.Open(manifest))
                    {
                        this.TempData["OperationSuccess"] = Resources.Resource.ManifestOpenedSuccessfully;
                    }

                    break;
            }

            var routeValues = new
            {
                Sequence = manifest.Sequence,
                AirlineCode = manifest.AirlineCode,
                FlightNumber = manifest.FlightNumber,
                ItineraryKey = manifest.ItineraryKey
            };
            return this.RedirectToAction("Index", routeValues);
        }

        /// <summary>
        /// Gets the delays for manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns>
        /// Content result.
        /// </returns>
        public ContentResult GetDelaysForManifest(ManifestDepartureVO manifest)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            IList<DelayDto> delays = new List<DelayDto>();
            IList<DelayVO> delaysVO = new List<DelayVO>();

            try
            {
                delays = this.manifestDepartureBusiness.GetDelaysForManifest(manifest.Sequence, manifest.AirlineCode, manifest.FlightNumber, manifest.ItineraryKey);
                delaysVO = Mapper.Map<IList<DelayVO>>(delays);
                jsonConvert.total = delaysVO != null ? delaysVO.Count : 0;
                jsonConvert.rows = delaysVO;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                result = this.Content(json);
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return result;
        }

        /// <summary>
        /// Gets the boarding for manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns></returns>
        public ContentResult GetBoardingForManifest(ManifestDepartureVO manifest)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            IList<ManifestDepartureBoardingDto> boardingsDto = new List<ManifestDepartureBoardingDto>();
            IList<ManifestDepartureBoardingVO> boardingsVO = new List<ManifestDepartureBoardingVO>();
            List<int> PositionsAll = new List<int>() { 1, 2, 3, 4, 5 }; //5 Stations
            List<int> PositionsBD = new List<int>();
            List<int> PositionsRes = new List<int>();

            try
            {
                boardingsDto = this.manifestDepartureBusiness.GetBoardingForManifest(manifest.Sequence, manifest.AirlineCode, manifest.FlightNumber, manifest.ItineraryKey);
                boardingsVO = Mapper.Map<IList<ManifestDepartureBoardingVO>>(boardingsDto);

                //Lista de Position en DB
                PositionsBD = boardingsVO.Select(c => c.Position).ToList();
                //Lista Restante de ALL - BD
                PositionsRes = PositionsAll.Except(PositionsBD).ToList();

                foreach (var item in PositionsRes)
                {
                    boardingsVO.Add(new ManifestDepartureBoardingVO
                    {
                        Sequence = manifest.Sequence,
                        AirlineCode = manifest.AirlineCode,
                        FlightNumber = manifest.FlightNumber,
                        ItineraryKey = manifest.ItineraryKey,
                        Position = item
                    });
                }

                jsonConvert.total = boardingsVO != null ? boardingsVO.Count : 0;
                jsonConvert.rows = boardingsVO;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                result = this.Content(json);
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return result;
        }

        /// <summary>
        /// Gets the boarding information for manifest.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <returns></returns>
        public JsonResult GetBoardingInformationForManifest(long boardingID, ManifestDepartureVO manifest)
        {
            if (string.IsNullOrWhiteSpace(boardingID.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return null;
            }

            List<ManifestDepartureBoardingInformationDto> boardingInformation = new List<ManifestDepartureBoardingInformationDto>();
            try
            {
                var itinerary = this.itineraryBusiness.FindFlightById(manifest.Sequence, manifest.AirlineCode, manifest.FlightNumber, manifest.ItineraryKey);

                if (itinerary.Airplane != null)
                {
                    boardingInformation = this.manifestDepartureBusiness.GetBoardingInformationForManifest(boardingID, itinerary.Airplane.AirplaneModel).ToList();
                }
            }
            catch (BusinessException exception)
            {
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(boardingInformation, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the boarding detail for manifest.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <returns></returns>
        public JsonResult GetBoardingDetailForManifest(long boardingID, ManifestDepartureVO manifest)
        {
            if (string.IsNullOrWhiteSpace(boardingID.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return null;
            }

            List<ManifestDepartureBoardingDetailDto> boardingDetail = new List<ManifestDepartureBoardingDetailDto>();
            try
            {
                var itinerary = this.itineraryBusiness.FindFlightById(manifest.Sequence, manifest.AirlineCode, manifest.FlightNumber, manifest.ItineraryKey);

                if (itinerary != null && itinerary.Airplane != null)
                {
                    boardingDetail = this.manifestDepartureBusiness.GetBoardingDetailForManifest(boardingID, itinerary.Airplane.AirplaneModel).ToList();
                }
            }
            catch (BusinessException exception)
            {
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(boardingDetail, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the users of type AOR.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <returns>
        /// Object with users information.
        /// </returns>
        [HttpGet]
        public JsonResult GetAorUsers(string stationCode)
        {
            IList<GenericCatalogDto> users = new List<GenericCatalogDto>();
            string role = "AOR";

            try
            {
                users = this.genericBusiness.GetUserCatalog(stationCode, role);
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return this.Json(users, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Shows the report.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "MANIFDEP-PRINTREP")]
        public ActionResult ViewReport(ManifestDepartureVO manifest)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            var reportName = string.Empty;

            try
            {
                reportName = manifest.DepartureStationCode == "MEX" ? "ManifestDepartureMex" : "ManifestDeparture";
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName(reportName);
                SetSiteMapValues(manifest);

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath)
                    && !string.IsNullOrEmpty(manifest.Sequence.ToString())
                    && !string.IsNullOrEmpty(manifest.AirlineCode.ToString())
                    && !string.IsNullOrEmpty(manifest.FlightNumber.ToString())
                    && !string.IsNullOrEmpty(manifest.ItineraryKey.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        { 
                            new Microsoft.Reporting.WebForms.ReportParameter("Sequence", manifest.Sequence.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("AirlineCode", manifest.AirlineCode.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("FlightNumber", manifest.FlightNumber.ToString(), false),
                            new Microsoft.Reporting.WebForms.ReportParameter("ItineraryKey", manifest.ItineraryKey.ToString(), false)
                        });

                    model.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("Manifest_Departure").Url;
                    return this.View("Report/ViewReport", model);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.RedirectToAction("Index", manifest);
        }

        /// <summary>
        /// Reports the information.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <param name="manifest">The manifest.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "MANIFDEP-PRINTREP")]
        public ActionResult ReportInformation(long boardingID, ManifestDepartureVO manifest)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();
            var reportName = string.Empty;
            AirplaneTypeDto airplaneTypeDto = new AirplaneTypeDto();

            try
            {
                //informacion de Modelo de Avion
                var itinerary = this.itineraryBusiness.FindFlightById(manifest.Sequence, manifest.AirlineCode, manifest.FlightNumber, manifest.ItineraryKey);

                if (itinerary != null && itinerary.Airplane != null)
                {
                    airplaneTypeDto = this.airplaneTypeBusiness.FindAirplaneTypeById(itinerary.Airplane.AirplaneModel);
                }

                if(airplaneTypeDto != null)
                {
                    reportName = airplaneTypeDto.CompartmentTypeCode;
                }
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName(reportName);
                SetSiteMapValues(manifest);

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath)
                    && !string.IsNullOrEmpty(boardingID.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        { 
                            new Microsoft.Reporting.WebForms.ReportParameter("BoardingID", boardingID.ToString(), false)
                        });

                    model.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("Manifest_Departure").Url;
                    return this.View("Report/ViewReport", model);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.RedirectToAction("Index", manifest);
        }

        /// <summary>
        /// Sets the site map values.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        private static void SetSiteMapValues(ManifestDepartureVO manifest)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Manifest_Departure");
                if (node != null)
                {
                    node.RouteValues["Sequence"] = manifest.Sequence;
                    node.RouteValues["AirlineCode"] = manifest.AirlineCode;
                    node.RouteValues["FlightNumber"] = manifest.FlightNumber;
                    node.RouteValues["ItineraryKey"] = manifest.ItineraryKey;
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
                Trace.TraceError(exception.Message, exception);
            }
        }

        /// <summary>
        /// Saves the information.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>.
        /// </returns>
        private bool Save(ManifestDepartureVO manifest)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    string arrivalAirport = string.Empty;
                    string departureAirport = string.Empty;

                    departureAirport = manifest.DepartureStationCode;
                    arrivalAirport = manifest.ArrivalStationCode;

                    manifest.ArrivalStationCode = arrivalAirport.Substring(0, 3);
                    manifest.ArrivalStationName = arrivalAirport.Substring(2);

                    ManifestDepartureDto manifestDto = new ManifestDepartureDto();
                    manifestDto = Mapper.Map<ManifestDepartureDto>(manifest);

                    if(manifest.DepartureStationCode == "MEX"){
                        manifestDto.AdditionalDepartureInformation = new AdditionalDepartureInformationDto();
                        manifestDto.AdditionalDepartureInformation = Mapper.Map<AdditionalDepartureInformationDto>(manifest);
                    }

                    //Informacion de Carga  Boarding
                    foreach(var item in manifestDto.ManifestDepartureBoardings)
                    {
                        var boardingInfo = manifest.ManifestDepartureBoardings.Where(c => c.Position == item.Position).Select(c => c.ManifestDepartureBoardingInformations).ToList();
                        var boardingDetailInfo = manifest.ManifestDepartureBoardings.Where(c => c.Position == item.Position).Select(c => c.ManifestDepartureBoardingDetails).ToList();

                        if (boardingInfo != null)
                        {
                            item.ManifestDepartureBoardingInformationDtos = Mapper.Map<IList<ManifestDepartureBoardingInformationDto>>(boardingInfo[0]);
                        }

                        if (boardingDetailInfo != null)
                        {
                            item.ManifestDepartureBoardingDetailDtos = Mapper.Map<IList<ManifestDepartureBoardingDetailDto>>(boardingDetailInfo[0]);
                        }
                    }                    

                    return this.manifestDepartureBusiness.SaveManifestDeparture(manifestDto);
                }

                this.TempData["ErrorMessage"] = Resource.FormValidationError;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = exception.ToString();
            }

            return false;
        }

        /// <summary>
        /// Closes the manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        private bool Close(ManifestDepartureVO manifest)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    string arrivalAirport = string.Empty;
                    string departureAirport = string.Empty;

                    departureAirport = manifest.DepartureStationCode;
                    arrivalAirport = manifest.ArrivalStationCode;

                    manifest.ArrivalStationCode = arrivalAirport.Substring(0, 3);
                    manifest.ArrivalStationName = arrivalAirport.Substring(2);

                    ManifestDepartureDto manifestDto = Mapper.Map<ManifestDepartureDto>(manifest);
                    if (manifest.DepartureStationCode == "MEX")
                    {
                        manifestDto.AdditionalDepartureInformation = new AdditionalDepartureInformationDto();
                        manifestDto.AdditionalDepartureInformation = Mapper.Map<AdditionalDepartureInformationDto>(manifest);
                    }

                    return this.manifestDepartureBusiness.CloseManifest(manifestDto);
                }

                this.TempData["ErrorMessage"] = Resource.FormValidationError;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = exception.ToString();
            }

            return false;
        }

        /// <summary>
        /// Opens the manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        private bool Open(ManifestDepartureVO manifest)
        {
            try
            {
                ManifestDepartureDto manifestDto = Mapper.Map<ManifestDepartureDto>(manifest);
                return this.manifestDepartureBusiness.OpenManifest(manifestDto);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = exception.ToString();
            }

            return false;
        }
    }
}