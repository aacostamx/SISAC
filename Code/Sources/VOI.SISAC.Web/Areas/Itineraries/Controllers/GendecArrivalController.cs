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
    using System.Threading.Tasks;
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
    /// Class Gendec Arrival
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
        /// The gendec departure business/
        /// </summary>
        private readonly IGendecDepartureBusiness gendecDepartureBusiness;

        /// <summary>
        /// The gendec arrival business/
        /// </summary>
        private IGendecArrivalBusiness gendecArrivalBusiness;

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
        /// The airport business
        /// </summary>
        private readonly IAirportBusiness airportBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="GendecArrivalController" /> class.
        /// </summary>
        /// <param name="gendecDepartureBusiness">The gendec departure business.</param>
        /// <param name="gendecArrivalBusiness">The gendec arrival business.</param>
        /// <param name="crewBusiness">The crew business.</param>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        /// <param name="genericCatalogBusiness">The generic catalog business.</param>
        /// <param name="userBusiness">The user business.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        /// <param name="airportBusiness">The airport business.</param>
        public GendecArrivalController(
            IGendecDepartureBusiness gendecDepartureBusiness,
            IGendecArrivalBusiness gendecArrivalBusiness,
            ICrewBusiness crewBusiness,
            IItineraryBusiness itineraryBusiness,
            IGenericCatalogBusiness genericCatalogBusiness,
            IUserBusiness userBusiness,
            IPageReportBusiness pageReportBusiness,
            IAirportBusiness airportBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.gendecDepartureBusiness = gendecDepartureBusiness;
            this.gendecArrivalBusiness = gendecArrivalBusiness;
            this.crewBusiness = crewBusiness;
            this.itineraryBusiness = itineraryBusiness;
            this.genericCatalogBusiness = genericCatalogBusiness;
            this.userBusiness = userBusiness;
            this.pageReportBusiness = pageReportBusiness;
            this.airportBusiness = airportBusiness;
        }

        /// <summary>
        /// Creates the specified sequence.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="DepartureStation">The departure station.</param>
        /// <param name="ArrivalStation">The arrival station.</param>
        /// <param name="EquipmentNumber">The equipment number.</param>
        /// <param name="DepartureDate">The departure date.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "GENDECARR-UPD")]
        public ActionResult Create(
            int Sequence, 
            string AirlineCode, 
            string FlightNumber, 
            string ItineraryKey,
            string DepartureStation, 
            string ArrivalStation,
            string EquipmentNumber, 
            string DepartureDate)
        {
            if (string.IsNullOrWhiteSpace(AirlineCode)
                || string.IsNullOrWhiteSpace(FlightNumber)
                || string.IsNullOrWhiteSpace(ItineraryKey))
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
                // Busca el aeropuerto para obtener el Código del Pais, solo  mostrará Gendec cuando el aeropuerto es Internacional
                airportDto = this.airportBusiness.FindAirportById(ArrivalStation);

                if (airportDto.CountryCode != "MEX")
                {
                    this.LoadCatalog(ArrivalStation);

                    //Sino tiene permiso para editar aeropuerto
                    if (NotContainsAirport(ArrivalStation))
                    {
                        return RedirectToAction("Unauthorized", "Home", new { area = "" });
                    }

                    // Vamos a la DB para obtener la información de Gendec y poder pintarla.
                    gendecArrivalDto = this.gendecArrivalBusiness.GetGendecArrival(Sequence, AirlineCode, FlightNumber, ItineraryKey);
                    if (gendecArrivalDto == null)
                    {
                        // Comienza proceso para obtener información de SISAC anterior
                        gendecArrivalVO = this.GetInformationManifiest(
                            Sequence,
                            AirlineCode,
                            FlightNumber,
                            ItineraryKey,
                            DepartureStation,
                            ArrivalStation,
                            EquipmentNumber,
                            DepartureDate);

                        // Obtenemos el itinerario
                        gendecArrivalVO.Itinerary = this.GetItinerary(Sequence, AirlineCode, FlightNumber, ItineraryKey);
                        return this.View(gendecArrivalVO);
                    }

                    gendecArrivalVO = Mapper.Map<GendecArrivalDto, GendecArrivalVO>(gendecArrivalDto);

                    // Obtenemos el itinerario
                    gendecArrivalVO.Itinerary = this.GetItinerary(Sequence, AirlineCode, FlightNumber, ItineraryKey);
                    this.ViewBag.DepartureDate = DepartureDate;
                    return this.View(gendecArrivalVO);
                }
                else
                {
                    this.TempData["OperationSuccess"] = "El Gendec solo se muestra para vuelos a la llegada Internacionales";
                    return RedirectToAction("Index", "Itinerary");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                System.Diagnostics.Trace.TraceError(exception.InnerException.ToString());
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View();
            }
        }

        /// <summary>
        /// Creates the specified gendec arrival vo.
        /// </summary>
        /// <param name="gendecArrivalVO">The gendec arrival vo.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "GENDECARR-UPD")]
        public ActionResult Create(GendecArrivalVO gendecArrivalVO)
        {
            IList<string> errors = new List<string>();
            GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();

            if (gendecArrivalVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.LoadCatalog(gendecArrivalVO.ArrivalStation);
                gendecArrivalVO = this.GetCrewsByID(gendecArrivalVO);
                gendecArrivalDto = Mapper.Map<GendecArrivalVO, GendecArrivalDto>(gendecArrivalVO);
                errors = this.gendecArrivalBusiness.ValidateGendecArrivalInformation(gendecArrivalDto);
                if (errors.Count == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                }
                else
                {
                    this.ViewBag.ListErrorMessage = errors;
                }


            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            // Una ves guardada la información, obtenemos para volver a mostrar 
            gendecArrivalDto = this.gendecArrivalBusiness.GetGendecArrival(gendecArrivalVO.Sequence, gendecArrivalVO.AirlineCode, gendecArrivalVO.FlightNumber, gendecArrivalVO.Itinerarykey);
            gendecArrivalVO = Mapper.Map<GendecArrivalDto, GendecArrivalVO>(gendecArrivalDto);
            
            // Obtenemos el itinerario
            gendecArrivalVO.Itinerary = this.GetItinerary(gendecArrivalVO.Sequence, gendecArrivalVO.AirlineCode, gendecArrivalVO.FlightNumber, gendecArrivalVO.Itinerarykey);
            return this.View(gendecArrivalVO);
        }

        /// <summary>
        /// Closes the gendec.
        /// </summary>
        /// <param name="gendecArrivalVO">The gendec arrival vo.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "GENDECARR-CLOSE")]
        public ActionResult CloseGendec(GendecArrivalVO gendecArrivalVO)
        {
            GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
            GendecArrivalDto gendecDepartDto = new GendecArrivalDto();

            if (gendecArrivalVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                gendecDepartDto = Mapper.Map<GendecArrivalVO, GendecArrivalDto>(gendecArrivalVO);
                this.gendecArrivalBusiness.CloseGendecArrival(gendecDepartDto);
            
                // Se obtiene lo que se actualizó
                gendecArrivalDto = this.gendecArrivalBusiness.GetGendecArrival(
                    gendecArrivalVO.Sequence, 
                    gendecArrivalVO.AirlineCode,
                    gendecArrivalVO.FlightNumber, 
                    gendecArrivalVO.Itinerarykey);
                gendecArrivalVO = Mapper.Map<GendecArrivalDto, GendecArrivalVO>(gendecArrivalDto);
                
                // Obtenemos el itinerario
                gendecArrivalVO.Itinerary = this.GetItinerary(gendecArrivalVO.Sequence, gendecArrivalVO.AirlineCode, gendecArrivalVO.FlightNumber, gendecArrivalVO.Itinerarykey);
                this.LoadCatalog(gendecArrivalVO.ArrivalStation);
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessInactive;
                return this.View("Create", gendecArrivalVO);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            LoadCatalog(gendecArrivalVO.ArrivalStation);
            return this.View(gendecArrivalVO);
        }

        /// <summary>
        /// Opens the gendec.
        /// </summary>
        /// <param name="gendecArrivalVO">The gendec arrival vo.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "GENDECARR-OPEN")]
        public ActionResult OpenGendec(GendecArrivalVO gendecArrivalVO)
        {
            GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
            GendecArrivalDto gendecDepartDto = new GendecArrivalDto();

            if (gendecArrivalVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                gendecDepartDto = Mapper.Map<GendecArrivalVO, GendecArrivalDto>(gendecArrivalVO);
                this.gendecArrivalBusiness.OpenGendecArrival(gendecDepartDto);

                // Se obtiene lo que se actualizó
                gendecArrivalDto = this.gendecArrivalBusiness.GetGendecArrival(
                    gendecArrivalVO.Sequence,
                    gendecArrivalVO.AirlineCode,
                    gendecArrivalVO.FlightNumber,
                    gendecArrivalVO.Itinerarykey);
                gendecArrivalVO = Mapper.Map<GendecArrivalDto, GendecArrivalVO>(gendecArrivalDto);

                // Obtenemos el itinerario
                gendecArrivalVO.Itinerary = this.GetItinerary(gendecArrivalVO.Sequence, gendecArrivalVO.AirlineCode, gendecArrivalVO.FlightNumber, gendecArrivalVO.Itinerarykey);
                this.LoadCatalog(gendecArrivalVO.ArrivalStation);
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessInactive;
                return this.View("Create", gendecArrivalVO);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            this.LoadCatalog(gendecArrivalVO.ArrivalStation);
            return this.View(gendecArrivalVO);
        }

        /// <summary>
        /// Gets the warning message configuration.
        /// </summary>
        /// <returns></returns>
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

            return Json(dictionary, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Prints the specified sequence.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="DepartureStation">The departure station.</param>
        /// <param name="ArrivalStation">The arrival station.</param>
        /// <param name="EquipmentNumber">The equipment number.</param>
        /// <param name="DepartureDate">The departure date.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "GENDECARR-PRINTREP")]
        public ActionResult Print(
            int Sequence, 
            string AirlineCode, 
            string FlightNumber, 
            string ItineraryKey,
            string DepartureStation,
            string ArrivalStation,
            string EquipmentNumber,
            string DepartureDate)
        {
            GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
            GendecArrivalVO gendecArrivalVO = new GendecArrivalVO();
            try
            {
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("GendecArrival");
                SetSiteMapValues(Sequence, AirlineCode, FlightNumber, ItineraryKey, DepartureStation, ArrivalStation, EquipmentNumber, DepartureDate);

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!string.IsNullOrEmpty(reportPath) 
                    && !string.IsNullOrEmpty(AirlineCode.ToString()) 
                    && !string.IsNullOrEmpty(FlightNumber.ToString()) 
                    && !string.IsNullOrEmpty(ItineraryKey.ToString()) 
                    && Sequence != 0)
                {
                    ReportingServiceViewModel reportServicelModel = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        {
                            new Microsoft.Reporting.WebForms.ReportParameter("Sequence", Sequence.ToString(),false),
                            new Microsoft.Reporting.WebForms.ReportParameter("AirlineCode", AirlineCode, false),
                            new Microsoft.Reporting.WebForms.ReportParameter("FlightNumber", FlightNumber, false),
                            new Microsoft.Reporting.WebForms.ReportParameter("ItineraryKey", ItineraryKey, false)
                        });

                    reportServicelModel.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("Gendec_Arrival").Url;
                    return View("Report/ViewReport", reportServicelModel);
                }
                else
                {
                    gendecArrivalDto = this.gendecArrivalBusiness.GetGendecArrival(Sequence, AirlineCode, FlightNumber, ItineraryKey);
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

            return this.RedirectToAction("Create", new
            {
                Sequence = gendecArrivalVO.Sequence,
                AirlineCode = gendecArrivalVO.AirlineCode,
                FlightNumber = gendecArrivalVO.FlightNumber,
                ItineraryKey = gendecArrivalVO.Itinerarykey
            });
        }

        /// <summary>
        /// Load ComboBox
        /// </summary>
        private void LoadCatalog(string ArrivalStation)
        {
            // USUARIOS ASC
            IList<GenericCatalogDto> userListFinal = new List<GenericCatalogDto>();
            
            // Asignamos a combo los proveedores viables
            userListFinal = this.genericCatalogBusiness.GetUserCatalog(ArrivalStation, "ASC");
            this.ViewBag.Users = userListFinal;
        }

        /// <summary>
        /// Gets the information manifiest.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="DepartureStation">The departure station.</param>
        /// <param name="ArrivalStation">The arrival station.</param>
        /// <param name="EquipmentNumber">The equipment number.</param>
        /// <param name="DepartureDate">The departure date.</param>
        /// <returns></returns>
        private GendecArrivalVO GetInformationManifiest(
            int Sequence, 
            string AirlineCode, 
            string FlightNumber,
            string ItineraryKey,
            string DepartureStation, 
            string ArrivalStation,
            string EquipmentNumber, 
            string DepartureDate)
        {
            CrewDto crewDto = new CrewDto();
            CrewVO crewVO = new CrewVO();
            GendecArrivalVO gendecArrivalVO = new GendecArrivalVO();
            gendecArrivalVO.Crews = new List<CrewVO>();

            WebServiceSisaRequestVO webServiceSisaRequestVO = new WebServiceSisaRequestVO();
            webServiceSisaRequestVO.DepartureStation = DepartureStation;
            webServiceSisaRequestVO.ArrivalStation = ArrivalStation;
            webServiceSisaRequestVO.FlightNumber = FlightNumber;
            webServiceSisaRequestVO.MaticulaAeronave = EquipmentNumber;
            webServiceSisaRequestVO.DepartureDate = Convert.ToDateTime(DepartureDate).ToString("yyyyMMdd");

            ResponseWebServiceSisaVO responseWebServiceSisaVO = ManifiestNationalSisa.ManifiestNationalGet(webServiceSisaRequestVO);

            if (responseWebServiceSisaVO.Result == "OK")
            {
                gendecArrivalVO.TotalPax = responseWebServiceSisaVO.Data.PassengerAdultNumber + responseWebServiceSisaVO.Data.PassengerChildrenNumber + responseWebServiceSisaVO.Data.PassengerInfantNumber;
                gendecArrivalVO.TotalCrew = responseWebServiceSisaVO.Data.CommandConf + responseWebServiceSisaVO.Data.StewarsConf;
                gendecArrivalVO.Sequence = Sequence;
                gendecArrivalVO.AirlineCode = AirlineCode;
                gendecArrivalVO.FlightNumber = FlightNumber;
                gendecArrivalVO.Itinerarykey = ItineraryKey;

                string[] crewArray = responseWebServiceSisaVO.Data.TripulacionesID.Split(',');
                string[] stewarsArray = responseWebServiceSisaVO.Data.SobrecargosID.Split(',');

                foreach (string crewNumberEmployee in crewArray)
                {
                    crewDto = this.crewBusiness.FindCrewByEmployeeNumber(crewNumberEmployee);
                    crewVO = Mapper.Map<CrewDto, CrewVO>(crewDto);

                    gendecArrivalVO.Crews.Add(crewVO);
                }

                foreach (string stewarNumberEmployee in stewarsArray)
                {
                    crewDto = this.crewBusiness.FindCrewByEmployeeNumber(stewarNumberEmployee);
                    crewVO = Mapper.Map<CrewDto, CrewVO>(crewDto);

                    gendecArrivalVO.Crews.Add(crewVO);
                }
            }

            gendecArrivalVO.Itinerary = GetItinerary(Sequence, AirlineCode, FlightNumber, ItineraryKey);
            return gendecArrivalVO;
        }

        /// <summary>
        /// Gets the itinerary.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <returns>Itinerary view object.</returns>
        private ItineraryVO GetItinerary(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey)
        {
            ItineraryDto itineraryDto = new ItineraryDto();
            ItineraryVO itineraryVO = new ItineraryVO();

            itineraryDto = this.itineraryBusiness.FindFlightById(Sequence, AirlineCode, FlightNumber, ItineraryKey);
            itineraryVO = Mapper.Map<ItineraryDto, ItineraryVO>(itineraryDto);

            return itineraryVO;
        }

        /// <summary>
        /// Gets the crews by identifier.
        /// </summary>
        /// <param name="gendecArrivalVO">The gendec arrival vo.</param>
        /// <returns>Gendec Arrival View Object.</returns>
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
        /// Sends the email.
        /// </summary>
        /// <param name="gendecArrivalVO">The gendec arrival vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "GENDECARR-OPEN")]
        public async Task<ActionResult> SendEmail(GendecArrivalVO gendecArrivalVO)
        {
            System.Diagnostics.Trace.TraceInformation("Beginning Sending Email");
            IList<string> errors = new List<string>();
            GendecArrivalVO gendecVO = new GendecArrivalVO();
            ItineraryDto itineraryDto;
            bool btnCerrar = false;

            GendecArrivalDto gendecArrivalDto = new GendecArrivalDto();
            ///gendecArrivalDto = this.gendecArrivalBusiness.GetGendecArrival(gendecArrivalVO.Sequence, gendecArrivalVO.AirlineCode, gendecArrivalVO.FlightNumber, gendecArrivalVO.Itinerarykey);
            gendecVO = Mapper.Map<GendecArrivalDto, GendecArrivalVO>(gendecArrivalDto);
            itineraryDto = this.itineraryBusiness.FindFlightById(gendecArrivalDto.Sequence, gendecArrivalDto.AirlineCode, gendecArrivalDto.FlightNumber, gendecArrivalDto.Itinerarykey);
            gendecArrivalVO.Itinerary = Mapper.Map<ItineraryDto, ItineraryVO>(itineraryDto);
            
            // LoadCatalog(gendecVO);
            errors = this.gendecArrivalBusiness.SendingEmail(gendecArrivalDto);
            if (errors.Count == 0)
            {
                errors.Add("El correo se ha enviado al jefe de estación para la reapertura del manifiesto de salida");
                this.ViewBag.ListErrorMessage = errors;
            }
            else
            {
                this.ViewBag.ListErrorMessage = errors;
            }

            this.ViewBag.btnCerrar = btnCerrar;
            return this.View("Create", gendecVO);
        }

        /// <summary>
        /// Set Site Map Values.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="DepartureStation">The departure station.</param>
        /// <param name="ArrivalStation">The arrival station.</param>
        /// <param name="EquipmentNumber">The equipment number.</param>
        /// <param name="DepartureDate">The departure date.</param>
        private static void SetSiteMapValues(
            int Sequence, 
            string AirlineCode, 
            string FlightNumber, 
            string ItineraryKey,
            string DepartureStation,
            string ArrivalStation,
            string EquipmentNumber,
            string DepartureDate)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Gendec_Arrival");
                if (node != null)
                {
                    node.RouteValues["Sequence"] = Sequence;
                    node.RouteValues["AirlineCode"] = AirlineCode;
                    node.RouteValues["FlightNumber"] = FlightNumber;
                    node.RouteValues["ItineraryKey"] = ItineraryKey;
                    node.RouteValues["ArrivalStation"] = ArrivalStation;
                    node.RouteValues["DepartureStation"] = DepartureStation;
                    node.RouteValues["EquipmentNumber"] = EquipmentNumber;
                    node.RouteValues["DepartureDate"] = DepartureDate;
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
                Trace.TraceError(exception.Message, exception);
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
    }
}