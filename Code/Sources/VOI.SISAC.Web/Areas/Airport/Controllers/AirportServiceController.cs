//------------------------------------------------------------------------
// <copyright file="AirportServiceController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using MvcSiteMapProvider;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Gpu;
    using VOI.SISAC.Business.GpuObservation;
    using VOI.SISAC.Business.Itineraries;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Resources;
    /// <summary>
    /// AirportServiceController class
    /// </summary>
    [CustomAuthorize]
    public class AirportServiceController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AirportServiceController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.AirportService;

        /// <summary>
        /// Interface for Airport service contract operations
        /// </summary>
        private readonly IAirportServiceBusiness serviceBusiness;

        /// <summary>
        /// Interface for Itinerary Business
        /// </summary>
        private readonly IItineraryBusiness itineraryBusiness;

        /// <summary>
        /// The gpu business
        /// </summary>
        private readonly IGpuBusiness gpuBusiness;

        /// <summary>
        /// The user business
        /// </summary>
        private readonly IUserBusiness userBusiness;

        /// <summary>
        /// The gpu observation business
        /// </summary>
        private readonly IGpuObservationBusiness gpuObservationBusiness;


        /// <summary>
        /// Initializes a new instance of the <see cref="AirportServiceController"/> class.
        /// </summary>
        /// <param name="serviceBusiness">The service business.</param>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        /// <param name="gpuBusiness">The gpu business.</param>
        /// <param name="gpuObservationBusiness">The gpu observation business.</param>
        /// <param name="userBusiness">The user business.</param>
        public AirportServiceController(
            IAirportServiceBusiness serviceBusiness,
            IItineraryBusiness itineraryBusiness,
            IGpuBusiness gpuBusiness,
            IGpuObservationBusiness gpuObservationBusiness,
            IUserBusiness userBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.serviceBusiness = serviceBusiness;
            this.itineraryBusiness = itineraryBusiness;
            this.gpuBusiness = gpuBusiness;
            this.gpuObservationBusiness = gpuObservationBusiness;
            this.userBusiness = userBusiness;
        }

        /// <summary>
        /// Index action
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="operationTypeName">Name of the operation type.</param>
        /// <returns>Action Result</returns>
        [CustomAuthorize(Roles = "AIRPSERV-IDX")]
        public ActionResult Index(
            int sequence,
            string airlineCode,
            string flightNumber,
            string itineraryKey,
            string operationTypeName)
        {
            int contractNumber = 0;
            this.ViewBag.ServicesContract = false;
            AirportServiceVO servicesVO = new AirportServiceVO();
            AirportServiceDto servicesDto = new AirportServiceDto();
            try
            {
                this.OperationTypeViewBag(operationTypeName);
                servicesDto.Itinerary = this.GetItinerary(sequence, airlineCode, flightNumber, itineraryKey);
                SetOperatioTypeName(operationTypeName, servicesDto);
                this.GetDepartureOrArrivalItinerary(sequence, airlineCode, flightNumber, itineraryKey, operationTypeName, servicesDto);
                this.OperationTypeViewBag(operationTypeName);
                servicesDto.Itinerary = this.GetItinerary(sequence, airlineCode, flightNumber, itineraryKey);
                SetOperatioTypeName(operationTypeName, servicesDto);
                this.GetDepartureOrArrivalItinerary(sequence, airlineCode, flightNumber, itineraryKey, operationTypeName, servicesDto);
                SetTime(servicesDto);

                if (operationTypeName == "SALIDA")
                {
                    contractNumber = this.serviceBusiness.GetCountDepartureServicesInContractByAirportAirline(airlineCode, servicesDto.Itinerary.DepartureStation);
                    //Sino tiene permiso para editar aeropuerto
                    if (NotContainsAirport(servicesDto.Itinerary.DepartureStation))
                    {
                        return RedirectToAction("Unauthorized", "Home", new { area = "" });
                    }
                }
                else
                {
                    contractNumber = this.serviceBusiness.GetCountArrivalServicesInContractByAirportAirline(airlineCode, servicesDto.Itinerary.ArrivalStation);
                    //Sino tiene permiso para editar aeropuerto
                    if (NotContainsAirport(servicesDto.Itinerary.ArrivalStation))
                    {
                        return RedirectToAction("Unauthorized", "Home", new { area = "" });
                    }
                }

                if (contractNumber > 0)
                {
                    this.ViewBag.ServicesContract = true;
                }

                servicesVO = Mapper.Map<AirportServiceVO>(servicesDto);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(servicesVO);
        }

        /// <summary>
        /// Create view
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="operationTypeName">Name of the operation type.</param>
        /// <returns>Action Result</returns>
        [CustomAuthorize(Roles = "AIRPSERV-ADD")]
        public ActionResult Create(
            int sequence,
            string airlineCode,
            string flightNumber,
            string itineraryKey,
            string operationTypeName)
        {
            AirportServiceVO servicesVO = new AirportServiceVO();
            AirportServiceDto servicesDto = new AirportServiceDto();
            try
            {
                this.OperationTypeViewBag(operationTypeName);
                servicesDto.Itinerary = this.GetItinerary(sequence, airlineCode, flightNumber, itineraryKey);
                this.LoadServiceInformation(
                    servicesDto.Itinerary.AirlineCode,
                    servicesDto.Itinerary.DepartureStation,
                    servicesDto.Itinerary.ArrivalStation,
                    operationTypeName);
                SetOperatioTypeName(operationTypeName, servicesDto);
                this.ViewBag.Providers = new List<GenericCatalogDto>();
                servicesVO = Mapper.Map<AirportServiceVO>(servicesDto);
                this.ViewBag.GpuObservations = this.GetGpuObservationsFromServer();
                servicesVO.StartDateService = DateTime.Now;
                servicesVO.EndDateService = DateTime.Now;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(servicesVO);
        }

        /// <summary>
        /// POST: Airport/AirportService/Create
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "AIRPSERV-ADD")]
        public ActionResult Create(AirportServiceVO model)
        {
            if (model == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                GetStationCodeByOperationType(model);
                model.QtyHours = CalculateTotalMinutes(model);
                model.Itinerary = null;
                AirportServiceDto service = Mapper.Map<AirportServiceDto>(model);
                this.serviceBusiness.AddService(service);
                this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                return this.RedirectToAction(
                "Index",
                        new
                        {
                            model.Sequence,
                            model.AirlineCode,
                            model.FlightNumber,
                            model.ItineraryKey,
                            model.OperationTypeName
                        });
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                message = ex.Number == 10 ? string.Format(message, Resource.StationCode) : message;
                this.ViewBag.ErrorMessage = message;
            }

            return this.View(model);
        }

        /// <summary>
        /// GET: Airport/AirportService/Edit/5
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result</returns>
        [CustomAuthorize(Roles = "AIRPSERV-UPD")]
        public ActionResult Edit(int id)
        {
            AirportServiceDto serviceDto = new AirportServiceDto();
            AirportServiceVO serviceVo = new AirportServiceVO();
            try
            {
                serviceDto = this.serviceBusiness.FindById((long)id);
                if (serviceDto == null)
                {
                    Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                    Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                SetSiteMapValues(serviceDto);

                serviceVo = Mapper.Map<AirportServiceDto, AirportServiceVO>(serviceDto);

                // Sets the services in the contract for the airline and airport
                this.LoadServiceInformation(
                    serviceDto.Itinerary.AirlineCode,
                    serviceDto.Itinerary.DepartureStation,
                    serviceDto.Itinerary.ArrivalStation,
                    serviceDto.OperationTypeName);

                // Sets the providers for the initial service
                this.ViewBag.Providers = this.LoadProviderInformation(
                    serviceDto.AirlineCode,
                    serviceDto.Itinerary.ArrivalStation,
                    serviceDto.Itinerary.DepartureStation,
                    serviceDto.ServiceCode,
                    serviceDto.OperationTypeName);

                // If the service is AGPO, sets the levels of drinking water for the equipment
                if (serviceDto.ServiceCode.Contains("AGPO"))
                {
                    this.ViewBag.Water = this.GetDrinkingWatersByEquipmentNumber(serviceDto.Itinerary.EquipmentNumber);
                }

                // If the service is GPU, sets the corresponding GPU for the airport
                if (serviceDto.ServiceCode.Contains("GPU"))
                {
                    this.ViewBag.Gpu = this.GetGpuByStation(
                        serviceDto.Itinerary.ArrivalStation,
                        serviceDto.Itinerary.DepartureStation,
                        serviceDto.OperationTypeName);

                    this.ViewBag.GpuObservations = this.GetGpuObservationsFromServer();
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.Services = new List<AirportServiceVO>();
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(serviceVo);
        }

        /// <summary>
        /// POST: Airport/AirportService/Edit/5
        /// </summary>
        /// <param name="airportService">The airport service.</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "AIRPSERV-UPD")]
        public ActionResult Edit(AirportServiceVO airportService)
        {
            if (airportService == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    airportService.QtyHours = CalculateTotalMinutes(airportService);
                    AirportServiceDto service = Mapper.Map<AirportServiceVO, AirportServiceDto>(airportService);
                    this.serviceBusiness.UpdateAirportService(service);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction(
                    "Index",
                    new
                    {
                        airportService.Sequence,
                        airportService.AirlineCode,
                        airportService.FlightNumber,
                        airportService.ItineraryKey,
                        airportService.OperationTypeName
                    });
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airportService);
        }

        /// <summary>
        /// GET: Airport/AirportService/Delete/
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result</returns>
        [CustomAuthorize(Roles = "AIRPSERV-DEL")]
        public ActionResult Delete(int id)
        {
            AirportServiceDto serviceDto = new AirportServiceDto();
            AirportServiceVO serviceVo = new AirportServiceVO();
            try
            {
                serviceDto = this.serviceBusiness.FindById((long)id);
                if (serviceDto == null)
                {
                    Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                    Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                SetSiteMapValues(serviceDto);
                serviceVo = Mapper.Map<AirportServiceDto, AirportServiceVO>(serviceDto);

                // Sets the services in the contract for the airline and airport
                this.LoadServiceInformation(
                    serviceDto.Itinerary.AirlineCode,
                    serviceDto.Itinerary.DepartureStation,
                    serviceDto.Itinerary.ArrivalStation,
                    serviceDto.OperationTypeName);

                // Sets the providers for the initial service
                this.ViewBag.Providers = this.LoadProviderInformation(
                    serviceDto.AirlineCode,
                    serviceDto.Itinerary.ArrivalStation,
                    serviceDto.Itinerary.DepartureStation,
                    serviceDto.ServiceCode,
                    serviceDto.OperationTypeName);

                // If the service is AGPO, sets the levels of drinking water for the equipment
                if (serviceDto.ServiceCode.Contains("AGPO"))
                {
                    this.ViewBag.Water = this.GetDrinkingWatersByEquipmentNumber(serviceDto.Itinerary.EquipmentNumber);
                }

                // If the service is GPU, sets the corresponding GPU for the airport
                if (serviceDto.ServiceCode.Contains("GPU"))
                {
                    this.ViewBag.Gpu = this.GetGpuByStation(
                        serviceDto.Itinerary.ArrivalStation,
                        serviceDto.Itinerary.DepartureStation,
                        serviceDto.OperationTypeName);

                    this.ViewBag.GpuObservations = this.GetGpuObservationsFromServer();
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.Services = new List<AirportServiceVO>();
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(serviceVo);
        }

        /// <summary>
        /// POST: Airport/AirportService/Delete/5
        /// </summary>
        /// <param name="airportService">The airport service.</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "AIRPSERV-DEL")]
        public ActionResult Delete(AirportServiceVO airportService)
        {
            if (airportService == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AirportServiceDto airportDto = new AirportServiceDto();
            try
            {
                airportDto = Mapper.Map<AirportServiceVO, AirportServiceDto>(airportService);
                this.serviceBusiness.DeleteAirportService(airportDto);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;
                return this.RedirectToAction(
                    "Index",
                    new
                    {
                        airportService.Sequence,
                        airportService.AirlineCode,
                        airportService.FlightNumber,
                        airportService.ItineraryKey,
                        airportService.OperationTypeName
                    });
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airportService);
        }

        /// <summary>
        /// Gets the providers by service.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="arrivalStation">The arrival station.</param>
        /// <param name="departureStation">The departure station.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="operationType">Type of the operation.</param>
        /// <returns>Action result</returns>
        public JsonResult GetProvidersByService(
            string airlineCode,
            string arrivalStation,
            string departureStation,
            string serviceCode,
            string operationType)
        {
            if (string.IsNullOrWhiteSpace(serviceCode)
                || string.IsNullOrWhiteSpace(airlineCode)
                || string.IsNullOrWhiteSpace(departureStation)
                || string.IsNullOrWhiteSpace(arrivalStation)
                || string.IsNullOrWhiteSpace(operationType))
            {
                return null;
            }

            IList<GenericCatalogDto> providers = new List<GenericCatalogDto>();
            providers = this.LoadProviderInformation(
                airlineCode,
                arrivalStation,
                departureStation,
                serviceCode,
                operationType);
            return this.Json(providers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the drinking waters by equipment number.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns>The drinking waters.</returns>
        public JsonResult GetDrinkingWaters(string equipmentNumber)
        {
            if (string.IsNullOrWhiteSpace(equipmentNumber))
            {
                return null;
            }

            IList<GenericCatalogDto> waters = this.GetDrinkingWatersByEquipmentNumber(equipmentNumber);
            return this.Json(waters, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the GPU's by its station.
        /// </summary>
        /// <param name="arriveStation">The arrive station.</param>
        /// <param name="departureStation">The departure station.</param>
        /// <param name="operationType">Type of the operation.</param>
        /// <returns>
        /// The drinking waters.
        /// </returns>
        public JsonResult GetGpu(string arriveStation, string departureStation, string operationType)
        {
            if (string.IsNullOrWhiteSpace(arriveStation)
                || string.IsNullOrWhiteSpace(departureStation)
                || string.IsNullOrWhiteSpace(operationType))
            {
                return null;
            }

            IList<GenericCatalogDto> catalog = this.GetGpuByStation(arriveStation, departureStation, operationType);
            return this.Json(catalog, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the GPU's by its station.
        /// </summary>
        /// <returns>
        /// The drinking waters.
        /// </returns>
        public JsonResult GetGpuObservation()
        {
            IList<GenericCatalogDto> catalog = this.GetGpuObservationsFromServer();
            return this.Json(catalog, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Set Site Map Values: Sequence, AirlineCode, FlightNumber, ItineraryKey, OperationTypeName
        /// </summary>
        /// <param name="serviceDto">service data transfer object.</param>
        private static void SetSiteMapValues(AirportServiceDto serviceDto)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Airport_Service");
                if (node != null)
                {
                    node.RouteValues["Sequence"] = serviceDto.Sequence;
                    node.RouteValues["AirlineCode"] = serviceDto.AirlineCode;
                    node.RouteValues["FlightNumber"] = serviceDto.FlightNumber;
                    node.RouteValues["ItineraryKey"] = serviceDto.ItineraryKey;
                    node.RouteValues["OperationTypeName"] = serviceDto.OperationTypeName;
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
                Trace.TraceError(exception.Message, exception);
            }
        }

        /// <summary>
        /// Get Station code by Operation Type Name
        /// </summary>
        /// <param name="model">The model.</param>
        private static void GetStationCodeByOperationType(AirportServiceVO model)
        {
            if (model.OperationTypeName == "SALIDA")
            {
                model.StationCode = model.Itinerary.DepartureStation;
            }
            else if (model.OperationTypeName == "LLEGADA")
            {
                model.StationCode = model.Itinerary.ArrivalStation;
            }
        }

        /// <summary>
        /// Calculate Total of Minutes
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Total of minutes.</returns>
        private static int CalculateTotalMinutes(AirportServiceVO model)
        {
            int totalMinutes = 0;
            try
            {
                DateTime startServices = model.StartDateService.Add(model.StartTimeService);
                DateTime endServices = model.EndDateService.Add(model.EndTimeService);
                totalMinutes = Convert.ToInt32((startServices - endServices).TotalMinutes);
                model.StartDateService = startServices;
                model.EndDateService = endServices;
            }
            catch (BusinessException ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }

            return Math.Abs(totalMinutes);
        }

        /// <summary>
        /// Set Time to DateTime
        /// </summary>
        /// <param name="servicesDto">The services data transfer object.</param>
        private static void SetTime(AirportServiceDto servicesDto)
        {
            for (int i = 0; i < servicesDto.ServicesAirport.Count; i++)
            {
                servicesDto.ServicesAirport[i].StartDateService.Add(
                    servicesDto.ServicesAirport[i].StartTimeService);

                servicesDto.ServicesAirport[i].EndDateService.Add(
                    servicesDto.ServicesAirport[i].EndTimeService);
            }
        }

        /// <summary>
        /// Set Operation Type Name
        /// </summary>
        /// <param name="operationTypeName">Name of the operation type.</param>
        /// <param name="servicesDto">The services data transfer object.</param>
        private static void SetOperatioTypeName(string operationTypeName, AirportServiceDto servicesDto)
        {
            if (operationTypeName == "SALIDA")
            {
                servicesDto.OperationTypeName = "SALIDA";
            }
            else if (operationTypeName == "LLEGADA")
            {
                servicesDto.OperationTypeName = "LLEGADA";
            }
        }

        /// <summary>
        /// Gets the drinking waters by equipment number.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns>The drinking waters.</returns>
        private IList<GenericCatalogDto> GetDrinkingWatersByEquipmentNumber(string equipmentNumber)
        {
            try
            {
                IList<GenericCatalogDto> waters = new List<GenericCatalogDto>();
                waters = this.serviceBusiness.GetDrinkingWaterByEquipmentNumber(equipmentNumber);
                return waters;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return new List<GenericCatalogDto>();
        }

        /// <summary>
        /// Gets the GPU's by its station.
        /// </summary>
        /// <param name="arriveStation">The arrive station.</param>
        /// <param name="departureStation">The departure station.</param>
        /// <param name="operationType">Type of the operation.</param>
        /// <returns>
        /// The drinking waters.
        /// </returns>
        private IList<GenericCatalogDto> GetGpuByStation(string arriveStation, string departureStation, string operationType)
        {
            try
            {
                IList<GenericCatalogDto> catalog = new List<GenericCatalogDto>();
                IList<GpuDto> gpu;
                switch (operationType)
                {
                    case "SALIDA":
                        gpu = this.gpuBusiness.GetGpuByStation(departureStation, true);
                        break;
                    case "LLEGADA":
                        gpu = this.gpuBusiness.GetGpuByStation(arriveStation, true);
                        break;
                    default:
                        gpu = new List<GpuDto>();
                        break;
                }

                foreach (GpuDto item in gpu)
                {
                    catalog.Add(new GenericCatalogDto
                    {
                        Id = item.GpuCode,
                        Description = item.GpuName
                    });
                }

                return catalog;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return new List<GenericCatalogDto>();
        }

        /// <summary>
        /// Get Departure Or Arrival Itinerary
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="operationTypeName">Name of the operation type.</param>
        /// <param name="servicesDto">The services data transfer object.</param>
        private void GetDepartureOrArrivalItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey, string operationTypeName, AirportServiceDto servicesDto)
        {
            if (operationTypeName == "SALIDA")
            {
                servicesDto.ServicesAirport = this.serviceBusiness.GetDepartureServicesByItinerary(
                    sequence, airlineCode, flightNumber, itineraryKey);
            }
            else if (operationTypeName == "LLEGADA")
            {
                servicesDto.ServicesAirport = this.serviceBusiness.GetArrivalServiceByItinerary(
                    sequence, airlineCode, flightNumber, itineraryKey);
            }
        }

        /// <summary>
        /// Load services information
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="departureStation">The departure station.</param>
        /// <param name="arrivalStation">The arrival station.</param>
        /// <param name="operationType">Type of the operation.</param>
        private void LoadServiceInformation(string airlineCode, string departureStation, string arrivalStation, string operationType)
        {
            if (operationType.Equals("SALIDA"))
            {
                this.ViewBag.Services = this.serviceBusiness.GetDepartureServicesInContractByAirportAirline(
                    airlineCode,
                    departureStation);
            }
            else
            {
                this.ViewBag.Services = this.serviceBusiness.GetArrivalServicesInContractByAirportAirline(
                    airlineCode,
                    arrivalStation);
            }
        }

        /// <summary>
        /// Load provider information
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="arrivalStation">The arrival station.</param>
        /// <param name="departureStation">The departure station.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="operationType">The operation type.</param>
        /// <returns>List of Generic Catalog for provider</returns>
        private IList<GenericCatalogDto> LoadProviderInformation(
            string airlineCode,
            string arrivalStation,
            string departureStation,
            string serviceCode,
            string operationType)
        {
            try
            {
                switch (operationType)
                {
                    case "SALIDA":
                        return this.serviceBusiness.GetProvidersInContractByServiceCodeForAirportAirline(
                        airlineCode,
                        departureStation,
                        serviceCode);
                    case "LLEGADA":
                        return this.serviceBusiness.GetProvidersInContractByServiceCodeForAirportAirline(
                        airlineCode,
                        arrivalStation,
                        serviceCode);
                    default:
                        return new List<GenericCatalogDto>();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.FindRecord, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.FindRecord, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return new List<GenericCatalogDto>();
        }

        /// <summary>
        /// Operation Type ViewBag
        /// </summary>
        /// <param name="operationTypeName">Name of the operation type.</param>
        private void OperationTypeViewBag(string operationTypeName)
        {
            if (operationTypeName == "SALIDA")
            {
                this.ViewBag.OperationTypeName = "SALIDA";
                return;
            }

            if (operationTypeName == "LLEGADA")
            {
                this.ViewBag.OperationTypeName = "LLEGADA";
                return;
            }
        }

        /// <summary>
        /// Get Itinerary by ID
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>Itinerary Data transfer object</returns>
        private ItineraryDto GetItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            return this.itineraryBusiness.FindFlightById(
                            sequence,
                            airlineCode,
                            flightNumber,
                            itineraryKey);
        }

        /// <summary>
        /// Services View Bag
        /// </summary>
        /// <param name="servicesDto">The services data transfer object.</param>
        private void ServicesViewBag(AirportServiceDto servicesDto)
        {
            if (servicesDto.OperationTypeName == "SALIDA")
            {
                this.ViewBag.Providers = this.LoadProviderInformation(
                    servicesDto.AirlineCode,
                    servicesDto.Itinerary.ArrivalStation,
                    servicesDto.Itinerary.DepartureStation,
                    servicesDto.ServiceCode,
                    servicesDto.OperationTypeName);
            }
            else
            {
                this.ViewBag.Providers = this.LoadProviderInformation(
                    servicesDto.AirlineCode,
                    servicesDto.Itinerary.ArrivalStation,
                    servicesDto.Itinerary.DepartureStation,
                    servicesDto.ServiceCode,
                    servicesDto.OperationTypeName);
            }
        }

        /// <summary>
        /// Gets the gpu observations from server.
        /// </summary>
        /// <returns>List of Generic Catalog for Observations</returns>
        private IList<GenericCatalogDto> GetGpuObservationsFromServer()
        {
            try
            {
                IList<GpuObservationDto> gpus = this.gpuObservationBusiness.GetActivesGpuObservations();
                if (gpus == null)
                {
                    this.ViewBag.GpuObservations = new List<GenericCatalogDto>();
                }
                else
                {
                    List<GenericCatalogDto> catalog = new List<GenericCatalogDto>();
                    foreach (GpuObservationDto item in gpus)
                    {
                        catalog.Add(new GenericCatalogDto
                        {
                            Id = item.GpuObservationCode,
                            Description = item.GpuObservationCodeName
                        });
                    }

                    return catalog;
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

            return new List<GenericCatalogDto>();
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
