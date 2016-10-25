//------------------------------------------------------------------------
// <copyright file="AirportController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Helpers;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Catalog;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Models.VO.Catalog;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Controlador de la vista Airport
    /// </summary>
    [CustomAuthorize]
    public class AirportController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AirportController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.AirportTitle;

        /// <summary>
        /// Interfaz privada del negocio solo de lectura
        /// </summary>
        private readonly IAirportBusiness airportBusiness;

        /// <summary>
        /// Interface country
        /// </summary>
        private readonly ICountryBusiness countryBusiness;

        /// <summary>
        /// Interface Airport Group
        /// </summary>
        private readonly IAirportGroupBusiness airportGroupBusiness;

        /// <summary>
        /// Controller
        /// </summary>
        /// <param name="airportBusiness">The airport business.</param>
        /// <param name="countryBusiness">The country business.</param>
        /// <param name="airportGroupBusiness">The airport group business.</param>
        public AirportController(
            IAirportBusiness airportBusiness,
            ICountryBusiness countryBusiness,
            IAirportGroupBusiness airportGroupBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.airportBusiness = airportBusiness;
            this.countryBusiness = countryBusiness;
            this.airportGroupBusiness = airportGroupBusiness;
        }

        /// <summary>
        /// Vista principal
        /// </summary>
        /// <returns>Vista con todos los aeropuertos</returns>
        [CustomAuthorize(Roles = "AIRPORT-IDX")]
        public ActionResult Index()
        {
            IList<AirportDto> airportDto = new List<AirportDto>();
            IList<AirportVO> airportVo = new List<AirportVO>();
            try
            {
                airportDto = airportBusiness.GetActivesAirports();
                airportVo = Mapper.Map<IList<AirportDto>, IList<AirportVO>>(airportDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(airportVo);
        }

        /// <summary>
        /// Vista create
        /// </summary>
        /// <returns>Vista create</returns>
        [CustomAuthorize(Roles = "AIRPORT-ADD")]
        public ActionResult Create()
        {
            AirportModelVO AirportViewModel = new AirportModelVO();
            IList<CountryDto> countriesDto = new List<CountryDto>();
            IList<AirportGroupDto> groupsDto = new List<AirportGroupDto>();

            try
            {
                if (ModelState.IsValid)
                {
                    countriesDto = countryBusiness.GetActivesCountry();
                    groupsDto = airportGroupBusiness.GetActivesAirportGroups();
                    AirportViewModel.Countries = Mapper.Map<IList<CountryDto>, IList<CountryVO>>(countriesDto);
                    AirportViewModel.AirportGroupCodes = Mapper.Map<IList<AirportGroupDto>, IList<AirportGroupVO>>(groupsDto);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(AirportViewModel);
        }

        /// <summary>
        /// Vista para insertar en el catálogo aeropuerto POST
        /// </summary>
        /// <param name="airport">Contiene el objecto del formulario que será editado</param>
        /// <returns>regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPORT-ADD")]
        public ActionResult Create(AirportModelVO airport)
        {
            if (airport == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            AirportDto airportDto = new AirportDto();
            try
            {
                if (ModelState.IsValid)
                {
                    airportDto = Mapper.Map<AirportVO, AirportDto>(airport.AirportVO);
                    airportBusiness.AddAirport(airportDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                message = ex.Number == 10 ? string.Format(message, Resource.StationCode) : message;
                this.ViewBag.ErrorMessage = message;
                airport.Countries = Mapper.Map<IList<CountryDto>, IList<CountryVO>>(countryBusiness.GetActivesCountry());
                airport.AirportGroupCodes = Mapper.Map<IList<AirportGroupDto>, IList<AirportGroupVO>>(airportGroupBusiness.GetActivesAirportGroups());
            }
            
            return this.View(airport);
        }

        /// <summary>
        /// Vista para editar registros del catálogo Airport
        /// </summary>
        /// <param name="id">Identificador del item del catálogo a editar</param>
        /// <returns>Regresa la vista Edit cargada con el item del modelo a editar</returns>
        [CustomAuthorize(Roles = "AIRPORT-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirportModelVO AirportViewModel = new AirportModelVO();
            IList<CountryDto> countriesDto = new List<CountryDto>();
            IList<AirportGroupDto> groupsDto = new List<AirportGroupDto>();
            AirportDto airportDto = new AirportDto();
            try
            {
                airportDto = airportBusiness.FindAirportById(id);
                if (airportDto == null)
                {
                    return this.HttpNotFound();
                }

                countriesDto = countryBusiness.GetActivesCountry();
                groupsDto = airportGroupBusiness.GetActivesAirportGroups();
                AirportViewModel.AirportVO = Mapper.Map<AirportDto, AirportVO>(airportDto);
                AirportViewModel.Countries = Mapper.Map<IList<CountryDto>, IList<CountryVO>>(countriesDto);
                AirportViewModel.AirportGroupCodes = Mapper.Map<IList<AirportGroupDto>, IList<AirportGroupVO>>(groupsDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }

            return this.View(AirportViewModel);
        }

        /// <summary>
        /// Vista para editar registro del catálogo Airport POST
        /// </summary>
        /// <param name="airport">Objecto de tipo aeropuerto que toma la información del formulario</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPORT-UPD")]
        public ActionResult Edit(AirportModelVO airport)
        {
            if (airport == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirportDto airportDto = new AirportDto();
            try
            {
                if (ModelState.IsValid)
                {
                    airportDto = Mapper.Map<AirportVO, AirportDto>(airport.AirportVO);
                    airportBusiness.UpdateAirport(airportDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
                airport.Countries = Mapper.Map<IList<CountryDto>, IList<CountryVO>>(countryBusiness.GetActivesCountry());
                airport.AirportGroupCodes = Mapper.Map<IList<AirportGroupDto>, IList<AirportGroupVO>>(airportGroupBusiness.GetActivesAirportGroups());
            }

            return this.View(airport);
        }

        /// <summary>
        /// Vista del detalle del registro
        /// </summary>
        /// <param name="id">Identificador del item del modelo a mostrar</param>
        /// <returns>Regresa la vista Details con el item del modelo</returns>
        [CustomAuthorize(Roles = "AIRPORT-VIE")]
        public ActionResult Details(string id)
        {
            AirportModelVO airportViewModel = new AirportModelVO();
            AirportDto airport = new AirportDto();
            try
            {
                airport = airportBusiness.FindAirportById(id);
                airportViewModel.AirportVO = Mapper.Map<AirportDto, AirportVO>(airport);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return View(airportViewModel.AirportVO);
        }

        /// <summary>
        /// Vista Delete
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa la vista Delete precargada con la información a eliminar</returns>
        [CustomAuthorize(Roles = "AIRPORT-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            AirportDto airportDto = new AirportDto();
            AirportVO airportVo = new AirportVO();
            try
            {
                airportDto = airportBusiness.FindAirportById(id);
                if (airportDto == null)
                {
                    return this.HttpNotFound();
                }

                airportVo = Mapper.Map<AirportDto, AirportVO>(airportDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);

            }

            return this.View(airportVo);
        }

        /// <summary>
        /// Vista Delete POST
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPORT-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirportDto airportDto = new AirportDto();
            AirportVO airportVo = new AirportVO();
            try
            {
                airportDto = airportBusiness.FindAirportById(id);
                if (airportDto == null)
                {
                    return this.HttpNotFound();
                }

                airportVo = Mapper.Map<AirportDto, AirportVO>(airportDto);
                this.airportBusiness.DeleteAirport(airportDto);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(airportVo);
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
    }
}