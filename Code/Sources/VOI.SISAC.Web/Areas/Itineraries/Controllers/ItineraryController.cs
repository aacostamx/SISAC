//------------------------------------------------------------------------
// <copyright file="ItineraryController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Itineraries.Controllers
{
    using AutoMapper;
    using Business.Common;
    using Business.Dto.Catalogs;
    using Business.Dto.Itineraries;
    using Helpers;
    using LumenWorks.Framework.IO.Csv;
    using Models.Enums;
    using Models.Files;
    using Newtonsoft.Json;
    using Resources;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Itineraries;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Models.VO.Itineraries;
    using Web.Controllers;

    /// <summary>
    /// Itinerary Controller
    /// </summary>
    [CustomAuthorize]
    public class ItineraryController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ItineraryController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = Resource.ItineraryTitle;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness Generic;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IAirplaneBusiness airplaneBusiness;

        /// <summary>
        /// Interface for Itinerary operations
        /// </summary>
        private readonly IItineraryBusiness itineraryBusiness;

        /// <summary>
        /// Interface for Airport Business
        /// </summary>
        private readonly IAirportBusiness airportBusiness;

        /// <summary>
        /// The module business
        /// </summary>
        private readonly IUserBusiness userBusiness;


        /// <summary>
        /// Initializes a new instance of the <see cref="ItineraryController"/> class.
        /// </summary>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        /// <param name="Generic">The generic.</param>
        /// <param name="airplaneBusiness">The airplane business.</param>
        /// <param name="airportBusiness">The airport business.</param>
        /// <param name="userBusiness">The user business.</param>
        public ItineraryController(
            IItineraryBusiness itineraryBusiness,
            IGenericCatalogBusiness Generic,
            IAirplaneBusiness airplaneBusiness,
            IAirportBusiness airportBusiness,
            IUserBusiness userBusiness)
        {
            this.userInfo = string.Format(
            LogMessages.UserInfo,
            Environment.UserDomainName,
            Environment.UserName,
            Environment.MachineName);
            this.itineraryBusiness = itineraryBusiness;
            this.Generic = Generic;
            this.airplaneBusiness = airplaneBusiness;
            this.airportBusiness = airportBusiness;
            this.userBusiness = userBusiness;
        }

        /// <summary>
        /// Get the list of Flights of actual day
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "ITINERARY-IDX")]
        public ActionResult Index()
        {
            Session["AirlineCodeSession"] = string.Empty;
            Session["FlightNumberSession"] = string.Empty;
            Session["EquipmentNumberSession"] = string.Empty;
            Session["DepartureStationSession"] = string.Empty;
            Session["ArrivalStationSession"] = string.Empty;
            Session["LocalizationStationSession"] = string.Empty;
            Session["StartDateSession"] = string.Empty;
            Session["EndDateSession"] = string.Empty;
            return this.View();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>View Create </returns>
        [CustomAuthorize(Roles = "ITINERARY-ADD")]
        public ActionResult Create()
        {
            this.LoadCatalogs();
            return this.View();
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="itineraryVO"></param>
        /// <param name="DepartureTime"></param>
        /// <param name="ArrivalTime"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ITINERARY-ADD")]
        public ActionResult Create(ItineraryVO itineraryVO, TimeSpan DepartureTime, TimeSpan ArrivalTime)
        {
            itineraryVO.DepartureDate = itineraryVO.DepartureDate.Add(DepartureTime);
            itineraryVO.ArrivalDate = itineraryVO.ArrivalDate.Add(ArrivalTime);

            if (itineraryVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                ModelState["ItineraryKey"].Errors.Clear();
                if (ModelState.IsValid)
                {
                    //validar aeropuertos distintos
                    if (itineraryVO.ArrivalStation == itineraryVO.DepartureStation)
                    {
                        this.ViewBag.ErrorMessage = Resources.Resource.DepartureAirportDistinctArrivalAirport;
                        this.LoadCatalogs();
                        return this.View(itineraryVO);
                    }

                    ItineraryDto itineraryDto = new ItineraryDto();
                    itineraryDto = Mapper.Map<ItineraryVO, ItineraryDto>(itineraryVO);
                    this.itineraryBusiness.AddFlightItinerary(itineraryDto);
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.LoadCatalogs();
                }
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 20)
                {
                    message = string.Format(message);
                }
                if (exception.Number == 21)
                {
                    message = string.Format(message);
                }

                this.ViewBag.ErrorMessage = message;
                this.LoadCatalogs();
            }

            return this.View(itineraryVO);
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="Sequence"></param>
        /// <param name="AirlineCode"></param>
        /// <param name="FlightNumber"></param>
        /// <param name="ItineraryKey"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "ITINERARY-UPD")]
        public ActionResult Edit(int Sequence,
                                 string AirlineCode,
                                 string FlightNumber,
                                 string ItineraryKey)
        {
            //Valores Iniciales 
            this.LoadCatalogs();
            this.ViewBag.EditArrival = false;

            if (string.IsNullOrEmpty(Sequence.ToString())
             || string.IsNullOrEmpty(AirlineCode)
             || string.IsNullOrEmpty(FlightNumber)
             || string.IsNullOrEmpty(ItineraryKey))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                //Inicializar combos
                ItineraryDto itineraryDto = new ItineraryDto();
                ItineraryVO itineraryVO = new ItineraryVO();
                itineraryDto = this.itineraryBusiness.FindFlightById(Sequence, AirlineCode, FlightNumber, ItineraryKey);
                itineraryVO = Mapper.Map<ItineraryVO>(itineraryDto);

                //Sino tiene permiso para editar aeropuerto
                if (NotContainsAirport(itineraryVO.DepartureStation))
                {
                    return RedirectToAction("Unauthorized", "Home", new { area = "" });
                }

                //Dejar o no editable campo ArrivalStation
                this.ViewBag.EditArrival = itineraryVO.EditArrival;

                if (itineraryVO == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                if (itineraryVO == null)
                    return HttpNotFound();

                return this.View(itineraryVO);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return this.View();
            }
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="itineraryVO"></param>
        /// <param name="DepartureTime"></param>
        /// <param name="ArrivalTime"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ITINERARY-UPD")]
        public ActionResult Edit(ItineraryVO itineraryVO, TimeSpan DepartureTime, TimeSpan ArrivalTime)
        {
            this.ViewBag.EditArrival = false;
            itineraryVO.DepartureDate = itineraryVO.DepartureDate.Add(DepartureTime);
            itineraryVO.ArrivalDate = itineraryVO.ArrivalDate.Add(ArrivalTime);

            if (itineraryVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                ItineraryDto itineraryDto = new ItineraryDto();
                itineraryDto = this.itineraryBusiness.FindFlightById(itineraryVO.Sequence, itineraryVO.AirlineCode, itineraryVO.FlightNumber, itineraryVO.ItineraryKey);

                //Dejar o no editable campo ArrivalStation
                this.ViewBag.EditArrival = itineraryDto.EditArrival;

                if (ModelState.IsValid)
                {
                    if (itineraryVO.EquipmentNumber != itineraryDto.EquipmentNumber)
                    {
                        this.itineraryBusiness.UpdateFlightItinerary(Mapper.Map<ItineraryVO, ItineraryDto>(itineraryVO), "UPDATE");
                        this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;

                        return this.RedirectToAction("Index");
                    }
                    else
                    {
                        return this.RedirectToAction("Index");
                    }

                }
                else
                {
                    this.LoadCatalogs();
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 22)
                {
                    message = string.Format(message);
                }

                this.ViewBag.ErrorMessage = message;
                this.LoadCatalogs();
            }

            return this.View(itineraryVO);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Sequence"></param>
        /// <param name="AirlineCode"></param>
        /// <param name="FlightNumber"></param>
        /// <param name="ItineraryKey"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "ITINERARY-DEL")]
        public ActionResult Delete(int Sequence,
                                string AirlineCode,
                                string FlightNumber,
                                string ItineraryKey)
        {
            ItineraryVO itineraryVO = new ItineraryVO();

            if (string.IsNullOrEmpty(Sequence.ToString())
             || string.IsNullOrEmpty(AirlineCode)
             || string.IsNullOrEmpty(FlightNumber)
             || string.IsNullOrEmpty(ItineraryKey))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (string.IsNullOrEmpty(Sequence.ToString())
                        || string.IsNullOrEmpty(AirlineCode)
                        || string.IsNullOrEmpty(FlightNumber)
                        || string.IsNullOrEmpty(ItineraryKey))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                itineraryVO = Mapper.Map<ItineraryDto, ItineraryVO>(this.itineraryBusiness.FindFlightById(Sequence, AirlineCode, FlightNumber, ItineraryKey));

                //Sino tiene permiso para editar aeropuerto
                if (NotContainsAirport(itineraryVO.DepartureStation))
                {
                    return RedirectToAction("Unauthorized", "Home", new { area = "" });
                }

                if (itineraryVO == null)
                    return HttpNotFound();
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return View(itineraryVO);
        }

        /// <summary>
        /// Vista Delete POST
        /// </summary>
        /// <param name="itineraryDelVO">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ITINERARY-DEL")]
        public ActionResult DeleteConfirmed(ItineraryVO itineraryDelVO)
        {
            ItineraryVO itinerarytVO = new ItineraryVO();
            if (itineraryDelVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                ItineraryDto itineraryDto = new ItineraryDto();
                itineraryDto = this.itineraryBusiness.FindFlightById(itineraryDelVO.Sequence, itineraryDelVO.AirlineCode, itineraryDelVO.FlightNumber, itineraryDelVO.ItineraryKey);
                if (itineraryDto == null)
                {
                    return this.HttpNotFound();
                }

                itinerarytVO = Mapper.Map<ItineraryDto, ItineraryVO>(itineraryDto);

                this.itineraryBusiness.DeleteFlightItinerary(itineraryDto, "DELETE");
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;

                return RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 23)
                {
                    message = string.Format(message);
                }

                this.ViewBag.ErrorMessage = message;
            }
            return this.View(itinerarytVO);
        }

        /// <summary>
        /// Get daily Itinerary Data
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ContentResult GetItineraryData(ItinerarySearchVO search)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            IList<ItineraryDto> itineraryList = new List<ItineraryDto>();
            ItinerarySearchDto searchDto = new ItinerarySearchDto();

            try
            {
                IList<string> searchParams = new List<string>
                {
                    search.AirlineCode,
                    search.FlightNumber,
                    search.EquipmentNumber,
                    search.DepartureStation,
                    search.ArrivalStation,
                    search.LocalizationStation,
                    search.DepartureDate.ToString(),
                    search.ArrivalDate.ToString()
                };
                IList<string> sessionParams = getSessionParams();

                // if search params are null or empty return only daily itinerary
                if (searchParams.All(c => string.IsNullOrEmpty(c))
                    && sessionParams.All(c => string.IsNullOrEmpty(c)))
                {
                    totalRows = this.itineraryBusiness.CountAllDay();
                    itineraryList = this.itineraryBusiness.paginationListbyDay(search.Pagesize, search.Pagenumber);
                    jsonConvert.total = totalRows;
                    jsonConvert.rows = itineraryList;
                    json = JsonConvert.SerializeObject(
                        jsonConvert,
                        Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                }
                else
                {
                    searchDto = Mapper.Map<ItinerarySearchDto>(search);

                    // else search by params
                    totalRows = this.itineraryBusiness.CountAdvanceSearchItinerary(searchDto);

                    if (totalRows > 0)
                    {
                        if (totalRows > 10)
                        {
                            Session["FlightNumberSession"] = search.FlightNumber;
                            Session["EquipmentNumberSession"] = search.EquipmentNumber;
                            Session["DepartureStationSession"] = search.DepartureStation;
                            Session["ArrivalStationSession"] = search.ArrivalStation;
                            Session["LocalizationStationSession"] = search.LocalizationStation;
                            Session["StartDateSession"] = search.DepartureDate.ToString();
                            Session["EndDateSession"] = search.ArrivalDate.ToString();
                        }

                        itineraryList = this.itineraryBusiness.AdvanceSearchItinerary(searchDto);
                        jsonConvert.total = totalRows;
                        jsonConvert.rows = itineraryList;
                        json = JsonConvert.SerializeObject(
                            jsonConvert,
                            Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
                    }
                }

                result = Content(json);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }

            return result;
        }

        /// <summary>
        /// Gets the itinerary data previous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        public ContentResult GetItineraryDataPrevious(ItinerarySearchVO search)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            IList<ItineraryDto> itineraryList = new List<ItineraryDto>();
            ItinerarySearchDto searchDto = new ItinerarySearchDto();

            try
            {
                searchDto = Mapper.Map<ItinerarySearchDto>(search);
                totalRows = this.itineraryBusiness.CountAdvanceSearchItineraryPrevious(searchDto);
                itineraryList = this.itineraryBusiness.AdvanceSearchItineraryPrevious(searchDto);
                for (int i = 0; i < itineraryList.Count; i++)
                {
                    itineraryList[i].Id = i;
                }
                jsonConvert.total = totalRows;
                jsonConvert.rows = itineraryList;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });

                result = this.Content(json);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }

            return result;
        }

        /// <summary>
        /// Get airlines actives
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AirlineComboBox()
        {
            IList<GenericCatalogDto> airlines = new List<GenericCatalogDto>();
            try
            {
                airlines = this.Generic.GetAirlineCatalog();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }

            return Json(airlines, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Airport Combobox
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult AirportComboBox()
        {
            IList<GenericCatalogDto> airports = new List<GenericCatalogDto>();
            try
            {
                airports = this.Generic.GetAirportsCatalog();
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }

            return Json(airports, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Uploads the file itinerary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "ITINERARY-UPLF")]
        public ActionResult UploadFileItinerary(ItineraryUploadVO input, HttpPostedFileBase file)
        {
            var extension = string.Empty;
            var url = string.Empty;
            var action = string.Empty;
            var restClient = new RestClient();
            var request = new RestRequest();
            var csvList = new List<string[]>();
            var json = string.Empty;

            try
            {
                extension = ConfigurationManager.AppSettings["Extension"];

                if (file == null || file.ContentLength <= 0 || !file.ContentType.Equals(extension))
                {
                    this.TempData["ErrorMessage"] = Resource.FileValidation;
                    return this.RedirectToAction("Index");
                }

                using (var csv = new CachedCsvReader(new StreamReader(file.InputStream, Encoding.Default), true))
                {
                    csvList = csv.ToList();
                }

                if (csvList != null && csvList.Count > 0)
                {
                    for (int i = 0; i < csvList.Count; i++)
                    {
                        var Jeppesen = new ItineraryFile();
                        Jeppesen.Line = i + 2;
                        Jeppesen.AirlineCode = csvList[i][0];
                        Jeppesen.FLTNUM = csvList[i][1];
                        Jeppesen.ACREGNUMBER = csvList[i][2];
                        Jeppesen.FLTORGDATELT = csvList[i][3];
                        Jeppesen.DEP = csvList[i][4];
                        Jeppesen.DST = csvList[i][10];
                        Jeppesen.SKDDST = csvList[i][9];
                        Jeppesen.STDLT = csvList[i][32];
                        Jeppesen.STALT = csvList[i][46];
                        Jeppesen.FLTTYPE = csvList[i][44];
                        setDates(Jeppesen);
                        input.itineraries.Add(Jeppesen);
                    }
                }

                if ((input.StartDate != null || input.StartDate != DateTime.MinValue) || (input.EndDate == null || input.EndDate == DateTime.MinValue))
                {
                    input.itineraries = input.itineraries.Where(c => c.DepartureDate.Date >= input.StartDate.Value.Date && c.DepartureDate < input.EndDate.Value.AddDays(1).Date).ToList();
                }

                if (input.itineraries.Count < 0)
                {
                    this.TempData["OperationSuccess"] = Resource.InvalidDates;
                }

                input.readServerFile = false;
                input.email = true;
                url = ConfigurationManager.AppSettings["Url"];
                action = ConfigurationManager.AppSettings["Action"];
                restClient = new RestClient(url);
                request = new RestRequest(action, Method.POST);
                json = JsonConvert.SerializeObject(input, Formatting.None, new JsonSerializerSettings());
                request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                var response = restClient.Execute(request);

                var sucess = response != null && response.StatusCode.ToString() == "OK" ? true : false;
                if (!sucess)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFile;
                }
                else
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFile;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.TempData["ErrorMessage"] = ex.ToString();
            }

            return this.RedirectToAction("Index");
        }


        /// <summary>
        /// Sets the dates.
        /// </summary>
        /// <param name="fileRow">The file row.</param>
        private void setDates(ItineraryFile fileRow)
        {
            var date = new DateTime();

            try
            {
                if (DateTime.TryParseExact(fileRow.FLTORGDATELT, "dd-MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    fileRow.DepartureDate = date;
                    fileRow.ArrivalDate = date;
                    TimeSpan time;
                    if (!string.IsNullOrEmpty(fileRow.STDLT)
                        && fileRow.STDLT.Length > 2
                        && TimeSpan.TryParse(fileRow.STDLT, out time))
                    {
                        fileRow.DepartureDate = fileRow.DepartureDate.Add(time);
                    }

                    if (!string.IsNullOrEmpty(fileRow.STALT)
                        && fileRow.STALT.Length > 2
                        && TimeSpan.TryParse(fileRow.STALT, out time))
                    {
                        fileRow.ArrivalDate = fileRow.ArrivalDate.Add(time);
                    }

                    if (fileRow.DepartureDate > fileRow.ArrivalDate)
                    {
                        fileRow.ArrivalDate = fileRow.ArrivalDate.AddDays(1);
                    }

                    if (fileRow.DepartureDate != DateTime.MinValue)
                    {
                        fileRow.ItineraryKey = fileRow.DepartureDate.ToString("yyyyMMdd");
                    }
                }
                else
                {
                    Logger.Error(string.Format("Invalid Date Format: {0} - " + Resource.Line + ": {1}", fileRow.FLTORGDATELT, fileRow.Line));
                    Trace.TraceError(string.Format("Invalid Date Format: {0} - " + Resource.Line + ": {1}", fileRow.FLTORGDATELT, fileRow.Line));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get Session Parameters
        /// </summary>
        /// <returns></returns>
        private List<string> getSessionParams()
        {
            try
            {
                return new List<string>
                {
                    Session["AirlineCodeSession"].ToString(),
                    Session["FlightNumberSession"].ToString(),
                    Session["EquipmentNumberSession"].ToString(),
                    Session["DepartureStationSession"].ToString(),
                    Session["ArrivalStationSession"].ToString(),
                    Session["LocalizationStationSession"].ToString(),
                    Session["StartDateSession"].ToString(),
                    Session["EndDateSession"].ToString()
                };
            }
            catch (Exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error("Variables de sessión expiraron favor de refrescar el navegador");
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError("Variables de sessión expiraron favor de refrescar el navegador");
                this.ViewBag.ErrorMessage = "Variables de sessión expiraron favor de refrescar el navegador";
                return new List<string>();
            }
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

        /// <summary>
        /// Loads the catalogs
        /// </summary>
        private void LoadCatalogs()
        {
            try
            {
                this.ViewBag.Airlines = this.Generic.GetAirlineCatalog();

                //Traer EquipmentNumbers
                IList<GenericCatalogDto> airplanesListFinal = new List<GenericCatalogDto>();
                IList<AirplaneDto> airplanesList = new List<AirplaneDto>();
                airplanesList = this.airplaneBusiness.GetActivesAirplane();

                airplanesListFinal = airplanesList.Select(item => new GenericCatalogDto()
                {
                    Id = item.EquipmentNumber,
                    Description = item.EquipmentNumber
                }).ToList<GenericCatalogDto>();

                this.ViewBag.EquipmentNumbers = airplanesListFinal;
                this.ViewBag.AirportDeparture = this.Generic.GetAirportsCatalog();
                this.ViewBag.AirportArrival = this.Generic.GetAirportsCatalog();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
        }
    }
}