//------------------------------------------------------------------------
// <copyright file="ItineraryController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Itineraries.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web.Mvc;
    using AutoMapper;
    using Business.Common;
    using Business.Dto.Catalogs;
    using Business.Dto.Itineraries;
    using Helpers;
    using Models.Files;
    using Newtonsoft.Json;
    using Resources;
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
        /// Read Text File
        /// </summary>
        /// <param name="input">input file</param>
        /// <returns>true or false</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "ITINERARY-UPLF")]
        public ActionResult UploadFileItinerary(ItineraryUploadVO input)
        {
            IDictionary<string, string> colNamesLength = new Dictionary<string, string>();
            List<ItineraryFile> itinerariesFile = new List<ItineraryFile>();
            List<ItineraryFile> itinerariesFileError = new List<ItineraryFile>();
            IList<ItineraryDto> itinerariesDto = new List<ItineraryDto>();
            var errors = new List<string>();
            var lines = new List<string>();
            var columnsLength = new List<string>();
            bool record = false, dayBefore = false, dayAfter = false;
            int countErrors = 0;

            try
            {
                if (string.IsNullOrEmpty(input.AirlineCodeCombobox))
                {
                    this.ViewBag.ErrorMessage = Resource.SelectAirline;
                    return this.View("Index");
                }
                if (input.file == null || !input.file.ContentType.Equals("text/plain") || input.file.ContentLength <= 0)
                {
                    this.ViewBag.ErrorMessage = Resource.FileValidation;
                    return this.View("Index");
                }
                streamToList(input, lines);

                //Loop for each line on the txt file
                for (int i = 0; i < lines.Count; i++)
                {
                    //Validate flag
                    record = string.IsNullOrEmpty(lines[i]) ? false : record;

                    //If record flag is on, begins recording info
                    if (record)
                    {
                        getFileInfo(colNamesLength, itinerariesFile, lines, input.AirlineCodeCombobox, i);
                    }

                    //Search Delimiter and start flag
                    searchDelimite(ref colNamesLength, lines, ref columnsLength, ref record, i);
                }

                //Valida que exitan el día anterior y el día posterior a la selección en el archivo
                dayBefore = itinerariesFile.Exists(c => c.DepartureDate.Date == input.StartDate.AddDays(-1).Date);
                dayAfter = itinerariesFile.Exists(c => c.DepartureDate.Date == input.EndDate.AddDays(1).Date);

                if (dayBefore && dayAfter)
                {
                    //Get al itineraries between StartDate - 1 day and EndDate + 1 apply to DepartureDate
                    itinerariesFile = itinerariesFile.Where(c => c.DepartureDate.Date >= input.StartDate.AddDays(-1).Date && c.DepartureDate <= input.EndDate.AddDays(1).Date).ToList();

                    //Validate all file errors
                    countErrors = validateFileErrors(itinerariesFile, itinerariesFileError, errors, countErrors);

                    //Eliminando los que tienen error 
                    itinerariesFile.RemoveAll(c => itinerariesFileError.Contains(c));

                    //Informa errores
                    if (errors.Count > 0)
                    {
                        this.ViewBag.ListErrorMessage = errors;
                    }

                    //Elimina los duplicados agruparlo por vuelo, llave, salida y llegada (toma el último)
                    itinerariesFile = itinerariesFile.GroupBy(c => new { c.FLTNUM, c.ItineraryKey, c.DepartureStation, c.ArrivalStation }).Select(c => c.Last()).ToList();

                    //Elimina los duplicados agruparlo por vuelo, llave y salida (toma el último)
                    itinerariesFile = itinerariesFile.GroupBy(c => new { c.FLTNUM, c.ItineraryKey, c.DepartureStation }).Select(c => c.Last()).ToList();

                    //Validate columns 
                    itinerariesDto = Mapper.Map<List<ItineraryDto>>(itinerariesFile);
                    itineraryBusiness.AddOrUpdateItinerary(itinerariesDto);

                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFile;
                }
                else
                {
                    this.ViewBag.ErrorMessage = Resource.InvalidDates;
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.FindRecord, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.FindRecord, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }
            return this.View("Index");
        }

        /// <summary>
        /// Validate File Errors
        /// </summary>
        /// <param name="itinerariesFile"></param>
        /// <param name="itinerariesFileError"></param>
        /// <param name="errors"></param>
        /// <param name="countErrors"></param>
        /// <returns></returns>
        private int validateFileErrors(List<ItineraryFile> itinerariesFile, List<ItineraryFile> itinerariesFileError, List<string> errors, int countErrors)
        {
            //Validate actives records
            var validEquipmentNumbers = airplaneBusiness.GetActivesAirplane().Select(c => c.EquipmentNumber).ToList();
            var validStations = airportBusiness.GetActivesAirports().Select(c => c.StationCode).ToList();

            //string para informar errores
            string equipmentNumberText = Resource.EquipmentNumber;
            string departureAirportText = Resource.DepartureAirport;
            string arrivalAirportText = Resource.ArrivalAirport;
            string requiredText = Resource.RequiredField;
            string notFoundDBText = Resource.NotFoundDB;
            string lineText = Resource.Line;

            string fltNumText = Resource.FlightNumber;
            string airlineText = Resource.Airline;
            string departureDate = Resource.DepartureDate;
            string hourDeparture = Resource.DepartureTime;
            string hourArrival = Resource.ArriveTime;

            foreach (ItineraryFile item in itinerariesFile)
            {
                var exitsEquipment = validEquipmentNumbers.Contains(item.ACREGNUMBER);
                var exitsDepStation = validStations.Contains(item.DEP);
                var exitsDetStation = validStations.Contains(item.DST);

                //Validacion de campos no null or espacios
                Dictionary<int, string> fields;
                Dictionary<int, string> fieldsErrorMessenge;
                LoadDictionaries(equipmentNumberText, departureAirportText, arrivalAirportText, fltNumText, airlineText, departureDate, hourDeparture, hourArrival, item, out fields, out fieldsErrorMessenge);

                foreach (var stringItem in fields)
                {
                    if (string.IsNullOrEmpty(stringItem.Value))
                    {
                        errors.Add(fieldsErrorMessenge[stringItem.Key] + " " + requiredText + " " + lineText + item.Line);
                        countErrors++;
                    }
                }

                //Validacion de campos que deben estar en catalogos de la BD
                if (!exitsEquipment)
                {
                    errors.Add(equipmentNumberText + ": '" + item.ACREGNUMBER + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }
                if (!exitsDepStation)
                {
                    errors.Add(departureAirportText + ": '" + item.DEP + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }
                if (!exitsDetStation)
                {
                    errors.Add(arrivalAirportText + ": '" + item.DST + "' " + notFoundDBText + " " + lineText + item.Line);
                    countErrors++;
                }

                //Validacion de departure y arrival iguales
                if (item.DepartureStation == item.ArrivalStation)
                {
                    errors.Add(departureAirportText + "-" + arrivalAirportText + ": " + item.DepartureStation + "-" + item.ArrivalStation + " " + lineText + item.Line);
                    countErrors++;
                }

                //Validacion de vuelo con Z como ultimo caracter, se debe ignorar
                if (Right(item.FLTNUM, 1) == "Z")
                {
                    errors.Add(fltNumText + ": " + item.FLTNUM + " " + lineText + item.Line);
                    countErrors++;
                }

                //remove from collection
                if (countErrors > 0)
                {
                    itinerariesFileError.Add(item);
                }

                countErrors = 0;
            }

            return countErrors;
        }

        /// <summary>
        /// Rights the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        private static string Right(string value, int length)
        {
            if (String.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }

        /// <summary>
        /// Loads the dictionaries.
        /// </summary>
        /// <param name="equipmentNumberText">The equipment number text.</param>
        /// <param name="departureAirportText">The departure airport text.</param>
        /// <param name="arrivalAirportText">The arrival airport text.</param>
        /// <param name="fltNumText">The FLT number text.</param>
        /// <param name="airlineText">The airline text.</param>
        /// <param name="departureDate">The departure date.</param>
        /// <param name="hourDeparture">The hour departure.</param>
        /// <param name="hourArrival">The hour arrival.</param>
        /// <param name="item">The item.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="fieldsErrorMessenge">The fields error messenge.</param>
        private static void LoadDictionaries(string equipmentNumberText, string departureAirportText, string arrivalAirportText, string fltNumText, string airlineText, string departureDate, string hourDeparture, string hourArrival, ItineraryFile item, out Dictionary<int, string> fields, out Dictionary<int, string> fieldsErrorMessenge)
        {
            fields = new Dictionary<int, string>();
            fields.Add(1, item.FLTNUM);
            fields.Add(2, item.AirlineCode);
            fields.Add(3, item.ACREGNUMBER);
            fields.Add(4, item.FLTORGDATELT);
            fields.Add(5, item.DepartureStation);
            fields.Add(6, item.ArrivalStation);
            fields.Add(7, item.STDLT);
            fields.Add(8, item.STALT);

            fieldsErrorMessenge = new Dictionary<int, string>();
            fieldsErrorMessenge.Add(1, fltNumText);
            fieldsErrorMessenge.Add(2, airlineText);
            fieldsErrorMessenge.Add(3, equipmentNumberText);
            fieldsErrorMessenge.Add(4, departureDate);
            fieldsErrorMessenge.Add(5, departureAirportText);
            fieldsErrorMessenge.Add(6, arrivalAirportText);
            fieldsErrorMessenge.Add(7, hourDeparture);
            fieldsErrorMessenge.Add(8, hourArrival);
        }

        /// <summary>
        /// Stream File to List
        /// </summary>
        /// <param name="input"></param>
        /// <param name="lines"></param>
        private static void streamToList(ItineraryUploadVO input, List<string> lines)
        {
            using (StreamReader sr = new StreamReader(input.file.InputStream, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
            }
        }

        /// <summary>
        /// Get File Information from the TXT
        /// </summary>
        /// <param name="colNamesLength"></param>
        /// <param name="itinerariesFile"></param>
        /// <param name="lines"></param>
        /// <param name="airline"></param>
        /// <param name="i"></param>
        private static void getFileInfo(IDictionary<string, string> colNamesLength, List<ItineraryFile> itinerariesFile, List<string> lines, string airline, int i)
        {
            ItineraryFile fileRow = new ItineraryFile();

            fileRow.Line = i + 1;
            fileRow.AirlineCode = airline;

            var coords = colNamesLength.Where(c => c.Key == "FLTNUM").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.FLTNUM = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "ACREGNUMBER").Select(c => c.Value).FirstOrDefault().Trim();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.ACREGNUMBER = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "FLTORGDATELT").Select(c => c.Value).FirstOrDefault().Trim();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.FLTORGDATELT = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "DEP").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.DEP = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "DST").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.DST = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "SKDDST").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.SKDDST = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "STDLT").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.STDLT = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            coords = colNamesLength.Where(c => c.Key == "STALT").Select(c => c.Value).FirstOrDefault();
            if (!string.IsNullOrEmpty(coords))
            {
                var index = coords.Split(' ');
                fileRow.STALT = lines[i].Substring(Convert.ToInt32(index[0]), Convert.ToInt32(index[1])).Trim();
            }

            fileRow.DepartureStation = fileRow.DEP;
            fileRow.ArrivalStation = fileRow.DST;
            setDates(fileRow);

            itinerariesFile.Add(fileRow);
        }

        /// <summary>
        /// Set Arrival and Departure Dates
        /// </summary>
        /// <param name="fileRow"></param>
        private static void setDates(ItineraryFile fileRow)
        {
            DateTime date = new DateTime();
            if (DateTime.TryParse(fileRow.FLTORGDATELT, out date))
            {
                fileRow.DepartureDate = date;
                fileRow.ArrivalDate = date;
                TimeSpan time;
                if (!string.IsNullOrEmpty(fileRow.STDLT)
                    && fileRow.STDLT.Length > 2
                    && TimeSpan.TryParse(fileRow.STDLT.Insert(2, ":"), out time))
                {
                    fileRow.DepartureDate = fileRow.DepartureDate.Add(time);
                }

                if (!string.IsNullOrEmpty(fileRow.STALT)
                    && fileRow.STALT.Length > 2
                    && TimeSpan.TryParse(fileRow.STALT.Insert(2, ":"), out time))
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
        }

        /// <summary>
        /// Search Delimited row 
        /// </summary>
        /// <param name="colNamesLength"></param>
        /// <param name="lines"></param>
        /// <param name="columnsLength"></param>
        /// <param name="record"></param>
        /// <param name="i"></param>
        private static void searchDelimite(ref IDictionary<string, string> colNamesLength, List<string> lines, ref List<string> columnsLength, ref bool record, int i)
        {
            var delimiter = "---";

            if (lines[i].Contains(delimiter))
            {
                var lastHeader = lines[i - 1];
                var firstHeader = lines[i - 2];
                columnsLength = lines[i].Split(' ').ToList();
                if (columnsLength != null && columnsLength.Count > 0)
                {
                    record = true;
                    columnsLength.RemoveAt(0);
                    columnsLength.RemoveAt(columnsLength.Count - 1);
                    var initialPos = 0;
                    colNamesLength = new Dictionary<string, string>();
                    for (int j = 0; j < columnsLength.Count; j++)
                    {
                        var column = columnsLength[j].Length + 1;
                        var columnName = string.Empty;
                        columnName = firstHeader.Substring(initialPos, column).Trim();
                        columnName += lastHeader.Substring(initialPos, column).Trim();
                        columnName = columnName.Replace(" ", "");
                        var coords = initialPos.ToString() + " " + column.ToString();
                        colNamesLength.Add(columnName, coords);
                        initialPos += column;
                    }
                }
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