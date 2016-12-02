//------------------------------------------------------------------------
// <copyright file="ManifestArrivalController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Itineraries.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using MvcSiteMapProvider;
    using Newtonsoft.Json;
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
    /// Manifest Arrival Controller
    /// </summary>
    [CustomAuthorize]
    public class ManifestArrivalController : BaseController
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
        private readonly string moduleName = "ManifestArrival";

        /// <summary>
        /// The manifest arrival business
        /// </summary>
        private readonly IManifestArrivalBusiness manifestArrivalBusiness;

        /// <summary>
        /// The page report
        /// </summary>
        private readonly IPageReportBusiness pageReportBusiness;

        /// <summary>
        /// The generic business
        /// </summary>
        private readonly IGenericCatalogBusiness genericBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestArrivalController" /> class.
        /// </summary>
        /// <param name="manifestArrivalBusiness">The manifest arrival business.</param>
        /// <param name="genericBusiness">The generic business.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        public ManifestArrivalController(
            IManifestArrivalBusiness manifestArrivalBusiness,
            IGenericCatalogBusiness genericBusiness,
            IPageReportBusiness pageReportBusiness)
        {
            this.manifestArrivalBusiness = manifestArrivalBusiness;
            this.genericBusiness = genericBusiness;
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
        /// <param name="manifestArrival">The manifest arrival.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "MANIFARR-IDX")]
        public ActionResult Index(ManifestArrivalVO manifestArrival)
        {
            if (manifestArrival == null
                || string.IsNullOrWhiteSpace(manifestArrival.AirlineCode)
                || string.IsNullOrWhiteSpace(manifestArrival.FlightNumber)
                || string.IsNullOrWhiteSpace(manifestArrival.ItineraryKey))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ManifestArrivalVO manifestVo = new ManifestArrivalVO();
            try
            {
                ManifestArrivalDto manifestDto = new ManifestArrivalDto();
                manifestDto = this.manifestArrivalBusiness.GetManifestArrivalForFlight(
                    manifestArrival.Sequence,
                    manifestArrival.AirlineCode,
                    manifestArrival.FlightNumber,
                    manifestArrival.ItineraryKey);
                manifestVo = Mapper.Map<ManifestArrivalVO>(manifestDto);
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
                this.ViewBag.ErrorMessage = LogMessages.ErrorRetrieveInfo;
            }

            manifestVo.Itinerary = new ItineraryVO();
            manifestVo.Itinerary.AirlineCode = manifestArrival.AirlineCode;
            manifestVo.Itinerary.FlightNumber = manifestArrival.FlightNumber;
            manifestVo.Itinerary.ItineraryKey = manifestArrival.ItineraryKey;
            manifestVo.Itinerary.Sequence = manifestArrival.Sequence;
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
        [CustomAuthorize(Roles = "MANIFARR-UPD")]
        [CustomAuthorize(Roles = "MANIFARR-OPEN")]
        [CustomAuthorize(Roles = "MANIFARR-CLOSE")]
        public ActionResult PerformAction(ManifestArrivalVO manifest)
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
        public ContentResult GetDelaysForManifest(ManifestArrivalVO manifest)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            IList<DelayDto> delays = new List<DelayDto>();
            IList<DelayVO> delaysVO = new List<DelayVO>();

            try
            {
                delays = this.manifestArrivalBusiness.GetDelaysForManifest(manifest.Sequence, manifest.AirlineCode, manifest.FlightNumber, manifest.ItineraryKey);
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
                this.ViewBag.ErrorMessage = LogMessages.ErrorRetrieveInfo;
            }

            return result;
        }

        /// <summary>
        /// Shows the report.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "MANIFARR-PRINTREP")]
        public ActionResult ViewReport(ManifestArrivalVO manifest)
        {
            string reportPath = string.Empty;
            PageReportDto pageReportDto = new PageReportDto();

            try
            {
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("ManifestArrival");
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

                    model.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("Manifest_Arrival").Url;
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
                this.ViewBag.ErrorMessage = LogMessages.ErrorRetrieveInfo;
            }

            return this.Json(users, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sets the site map values.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        private static void SetSiteMapValues(ManifestArrivalVO manifest)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Manifest_Arrival");
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
        private bool Save(ManifestArrivalVO manifest)
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

                    ManifestArrivalDto manifestDto = Mapper.Map<ManifestArrivalDto>(manifest);
                    
                    if (manifest.ArrivalStationCode == "MEX")
                    {
                        manifestDto.AdditionalArrivalInformation = new AdditionalArrivalInformationDto();
                        manifestDto.AdditionalArrivalInformation = Mapper.Map<AdditionalArrivalInformationDto>(manifest);
                    }

                    return this.manifestArrivalBusiness.SaveManifestArrival(manifestDto);
                }

                this.TempData["ErrorMessage"] = Resource.FormValidationError;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = LogMessages.ErrorUpdate;
            }

            return false;
        }

        /// <summary>
        /// Closes the manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        private bool Close(ManifestArrivalVO manifest)
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

                    ManifestArrivalDto manifestDto = Mapper.Map<ManifestArrivalDto>(manifest);
                    return this.manifestArrivalBusiness.CloseManifest(manifestDto);
                }

                this.TempData["ErrorMessage"] = Resource.FormValidationError;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = LogMessages.ErrorUpdate;
            }

            return false;
        }

        /// <summary>
        /// Opens the manifest.
        /// </summary>
        /// <param name="manifest">The manifest.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        private bool Open(ManifestArrivalVO manifest)
        {
            try
            {
                ManifestArrivalDto manifestDto = Mapper.Map<ManifestArrivalDto>(manifest);
                return this.manifestArrivalBusiness.OpenManifest(manifestDto);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = LogMessages.ErrorUpdate;
            }

            return false;
        }
    }
}