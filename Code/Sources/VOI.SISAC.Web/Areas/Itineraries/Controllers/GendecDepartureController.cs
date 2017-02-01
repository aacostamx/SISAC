//------------------------------------------------------------------------
// <copyright file="GendecDepartureController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    using Business.Dto.Itineraries;
    using Models.VO.Airport;
    using Models.VO.Itineraries;
    using MvcSiteMapProvider;
    using Resources;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Itineraries;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Helpers;
    using Web.Controllers;

    /// <summary>
    /// Class Gendec Departure Controller
    /// </summary>
    [CustomAuthorize]
    public class GendecDepartureController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ItineraryController));

        /// <summary>
        /// The catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.GendecDepartureTitle;

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Interface for the gendec deaprture operations
        /// </summary>
        private readonly IGendecDepartureBusiness gendecDepartureBusiness;

        /// <summary>
        /// Crew Business
        /// </summary>
        private readonly ICrewBusiness crewBusiness;

        /// <summary>
        /// Itinerary Business
        /// </summary>
        private readonly IItineraryBusiness itineraryBusiness;

        /// <summary>
        /// Generic Catalog Business
        /// </summary>
        private readonly IGenericCatalogBusiness genericCatalogBusiness;

        /// <summary>
        /// User Business
        /// </summary>
        private readonly IUserBusiness userBusiness;

        /// <summary>
        /// The airport business
        /// </summary>
        private readonly IAirportBusiness airportBusiness;

        /// <summary>
        /// The page report business
        /// </summary>
        private readonly IPageReportBusiness pageReportBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="GendecDepartureController"/> class.
        /// </summary>
        /// <param name="genderDepartureBusiness">The gender departure business.</param>
        /// <param name="airplaneBusiness">The airplane business.</param>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        /// <param name="crewBusiness">The crew business.</param>
        /// <param name="genericCatalogBusiness">The generic catalog business.</param>
        /// <param name="userBusiness">The user business.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        /// <param name="airportBusiness">The airport business.</param>
        public GendecDepartureController(
            IGendecDepartureBusiness genderDepartureBusiness,
            IAirplaneBusiness airplaneBusiness,
            IItineraryBusiness itineraryBusiness,
            ICrewBusiness crewBusiness,
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
            this.gendecDepartureBusiness = genderDepartureBusiness;
            this.genericCatalogBusiness = genericCatalogBusiness;
            this.crewBusiness = crewBusiness;
            this.itineraryBusiness = itineraryBusiness;
            this.userBusiness = userBusiness;
            this.pageReportBusiness = pageReportBusiness;
            this.airportBusiness = airportBusiness;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sequence"></param>
        /// <param name="AirlineCode"></param>
        /// <param name="FlightNumber"></param>
        /// <param name="ItineraryKey"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "GENDECDEP-UPD")]
        public ActionResult Create(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey)
        {
            System.Diagnostics.Trace.TraceInformation("Beginning Gendec Departure");
            this.ViewBag.OperationTypeName = "SALIDA";
            GendecDepartureVO gendecDepartureVO = new GendecDepartureVO();
            GendecDepartureDto gendecDepartureDto = new GendecDepartureDto();
            AirportDto airportDto = new AirportDto();
            ItineraryVO itineraryVO = new ItineraryVO();

            if (string.IsNullOrWhiteSpace(AirlineCode)
                || string.IsNullOrWhiteSpace(FlightNumber)
                || string.IsNullOrWhiteSpace(ItineraryKey))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                // Obtenemos los datos completos del vuelo dentro de Itinerario
                itineraryVO = Mapper.Map<ItineraryDto, ItineraryVO>(this.itineraryBusiness.FindFlightById(Sequence, AirlineCode, FlightNumber, ItineraryKey));

                //Sino tiene permiso para editar aeropuerto
                if (NotContainsAirport(itineraryVO.DepartureStation))
                {
                    return RedirectToAction("Unauthorized", "Home", new { area = "" });
                }

                // Obtenemos el Genden Departure
                gendecDepartureDto = this.gendecDepartureBusiness.GetGendecDeparture(Sequence, AirlineCode, FlightNumber, ItineraryKey);

                // Realizamos el mapeo del Gendec Dto a VO
                gendecDepartureVO = Mapper.Map<GendecDepartureDto, GendecDepartureVO>(gendecDepartureDto);

                // Se obtiene Código de Pais, solo  mostrará Gendec cuando el aeropuerto sea Internacional
                airportDto = this.airportBusiness.FindAirportById(itineraryVO.DepartureStation);

                if (airportDto.CountryCode != "MEX")
                {
                    if (gendecDepartureVO == null)
                    {
                        gendecDepartureVO = new GendecDepartureVO();
                        gendecDepartureVO.Itinerary = itineraryVO;
                    }

                    this.LoadCatalog(gendecDepartureVO);

                    return View(gendecDepartureVO);
                }
                else
                {
                    this.TempData["OperationSuccess"] = "El Gendec solo se captura para vuelos a la salida Internacionales";
                    return RedirectToAction("Index", "Itinerary");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                System.Diagnostics.Trace.TraceError(ex.InnerException.ToString());
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);

                return this.View();
            }

        }

        /// <summary>
        /// Get Crew By ID
        /// </summary>
        /// <param name="CrewID"></param>
        /// <returns></returns>
        public ActionResult GetCrewsGendec(long CrewID)
        {
            CrewDto crewDto = new CrewDto();
            crewDto = this.crewBusiness.GetAllCrewByID(CrewID);
            return this.Json(crewDto, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Insert or Update Gendec
        /// </summary>
        /// <param name="gendecDepartureVO"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "GENDECDEP-UPD")]
        public ActionResult Edit(GendecDepartureVO gendecDepartureVO)
        {
            GendecDepartureDto gendecDepartureFound = new GendecDepartureDto();
            GendecDepartureVO gendecDepartureVOResult = new GendecDepartureVO();
            ItineraryVO itineraryVO = new ItineraryVO();

            if (gendecDepartureVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            gendecDepartureFound = this.gendecDepartureBusiness.GetGendecDeparture(gendecDepartureVO.Sequence, gendecDepartureVO.AirlineCode, gendecDepartureVO.FlightNumber, gendecDepartureVO.Itinerarykey);
            itineraryVO = Mapper.Map<ItineraryDto, ItineraryVO>(this.itineraryBusiness.FindFlightById(gendecDepartureVO.Sequence, gendecDepartureVO.AirlineCode, gendecDepartureVO.FlightNumber, gendecDepartureVO.Itinerarykey));
            // Si es igual a null es porque no existe un Gendec, por lo tanto debe insertar.
            if (gendecDepartureFound == null)
            {
                gendecDepartureVOResult = this.CreateGendec(gendecDepartureVO);
                if (gendecDepartureVOResult != null)
                {
                    gendecDepartureVOResult.Itinerary = itineraryVO;
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessCreate;
                }
            }
            else
            {
                gendecDepartureVOResult = this.EditGendec(gendecDepartureVO);
                if (gendecDepartureVOResult != null)
                {
                    gendecDepartureVOResult.Itinerary = itineraryVO;
                    gendecDepartureVOResult = this.GetCrewsByID(gendecDepartureVOResult);
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;
                }
            }
            LoadCatalog(gendecDepartureVOResult);
            return this.View(gendecDepartureVOResult);
        }

        /// <summary>
        /// Closes the gendec.
        /// </summary>
        /// <param name="gendecDepartureVO">The gendec departure vo.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "GENDECDEP-CLOSE")]
        public ActionResult CloseGendec(GendecDepartureVO gendecDepartureVO)
        {
            GendecDepartureDto gendecDepartureDto = new GendecDepartureDto();
            GendecDepartureDto gendecDepartDto = new GendecDepartureDto();
            GendecDepartureVO gendecDepartureVOSaved = new GendecDepartureVO();

            if (gendecDepartureVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                gendecDepartDto = Mapper.Map<GendecDepartureVO, GendecDepartureDto>(gendecDepartureVO);
                
                // Cierra el Gendec
                this.gendecDepartureBusiness.CloseGendecDeparture(gendecDepartDto);
                
                // Se obtiene lo que ya se guardo
                gendecDepartureDto = this.gendecDepartureBusiness.GetGendecDeparture(gendecDepartureVO.Sequence, gendecDepartureVO.AirlineCode,
                                                                gendecDepartureVO.FlightNumber, gendecDepartureVO.Itinerarykey);
                // Realiza el mapeo
                gendecDepartureVOSaved = Mapper.Map<GendecDepartureDto, GendecDepartureVO>(gendecDepartureDto);
                
                // Obtiene el Itinerario
                gendecDepartureVOSaved.Itinerary = this.GetItinerary(gendecDepartureVO.Sequence, gendecDepartureVO.AirlineCode,
                                                                gendecDepartureVO.FlightNumber, gendecDepartureVO.Itinerarykey);
                // Manda mensaje de Exito
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessInactive;
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            this.LoadCatalog(gendecDepartureVOSaved);
            return this.View(gendecDepartureVOSaved);
        }

        /// <summary>
        /// Delete Gendec Departure
        /// </summary>
        /// <param name="CrewID">The crew identifier.</param>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "GENDECDEP-UPD")]
        public ActionResult Delete(long CrewID, int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey)
        {
            if (CrewID < 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GendecDepartureDto gendecDepartureDto = new GendecDepartureDto();
            GendecDepartureVO gendecDepartureVO = new GendecDepartureVO();

            gendecDepartureDto = this.gendecDepartureBusiness.GetGendecDeparture(Sequence, AirlineCode, FlightNumber, ItineraryKey);
            gendecDepartureVO = Mapper.Map<GendecDepartureDto, GendecDepartureVO>(gendecDepartureDto);
            gendecDepartureVO.Crews = new List<CrewVO>();
            gendecDepartureVO = this.GetCrewForDelete(gendecDepartureVO, CrewID);
            return View(gendecDepartureVO);
        }

        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="CrewID">The crew identifier.</param>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "GENDECDEP-UPD")]
        public ActionResult DeleteConfirmed(long CrewID, int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey)
        {
            GendecDepartureDto gendecDepartureDto = new GendecDepartureDto();
            GendecDepartureVO gendecDepartureVO = new GendecDepartureVO();
            bool btnCerrar = false;

            if (CrewID < 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                gendecDepartureDto = this.gendecDepartureBusiness.GetGendecDeparture(Sequence, AirlineCode, FlightNumber, ItineraryKey);
                if (gendecDepartureDto == null)
                {
                    return this.HttpNotFound();
                }
                this.ViewBag.btnCerrar = btnCerrar;
                gendecDepartureVO = Mapper.Map<GendecDepartureDto, GendecDepartureVO>(this.gendecDepartureBusiness.DeleteGendecCrew(CrewID, gendecDepartureDto));
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);

            }
            this.LoadCatalog(gendecDepartureVO);
            return this.RedirectToAction("Create", new
            {
                Sequence = gendecDepartureVO.Sequence,
                AirlineCode = gendecDepartureVO.AirlineCode,
                FlightNumber = gendecDepartureVO.FlightNumber,
                ItineraryKey = gendecDepartureVO.Itinerarykey
            });
        }

        ///// <summary>
        ///// Sends the email.
        ///// </summary>
        ///// <param name="gendecDepartureVO">The gendec departure vo.</param>
        ///// <returns></returns>
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////[CustomAuthorize(Roles = "GENDECDEP-OPEN")]
        ////public async Task<ActionResult> SendEmail(GendecDepartureVO gendecDepartureVO)
        ////{
        ////    System.Diagnostics.Trace.TraceInformation("Beginning Sending Email");
        ////    IList<string> errors = new List<string>();
        ////    GendecDepartureVO gendecVO = new GendecDepartureVO();
        ////    ItineraryDto itineraryDto;
        ////    bool btnCerrar = false;

        ////    GendecDepartureDto gendecDepartureDto = new GendecDepartureDto();
        ////    gendecDepartureDto = this.gendecDepartureBusiness.GetGendecDeparture(gendecDepartureVO.Sequence, gendecDepartureVO.AirlineCode, gendecDepartureVO.FlightNumber, gendecDepartureVO.Itinerarykey);
        ////    gendecVO = Mapper.Map<GendecDepartureDto, GendecDepartureVO>(gendecDepartureDto);
        ////    itineraryDto = this.itineraryBusiness.FindFlightById(gendecDepartureVO.Sequence, gendecDepartureVO.AirlineCode, gendecDepartureVO.FlightNumber, gendecDepartureVO.Itinerarykey);
        ////    gendecDepartureVO.Itinerary = Mapper.Map<ItineraryDto, ItineraryVO>(itineraryDto);
        ////    LoadCatalog(gendecVO);
        ////    try
        ////    {
        ////        errors = this.gendecDepartureBusiness.SendingEmail(gendecDepartureDto);

        ////    }
        ////    catch (BusinessException ex)
        ////    {
        ////        Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
        ////        Logger.Error(ex.Message, ex);
        ////        Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
        ////        Trace.TraceError(ex.Message, ex);
        ////        this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
        ////        errors.Add(ex.InnerException.Message);

        ////    }
        ////    if (errors.Count == 0)
        ////    {
        ////        errors.Add("El correo se ha enviado al jefe de estación para la reapertura del manifiesto de salida");
        ////        this.ViewBag.ListErrorMessage = errors;
        ////    }
        ////    else
        ////    {
        ////        this.ViewBag.ListErrorMessage = errors;
        ////    }
        ////    this.ViewBag.btnCerrar = btnCerrar;
        ////    return this.View("Create", gendecVO);
        ////}

        /// <summary>
        /// Open the gendec.
        /// </summary>
        /// <param name="gendecDepartureVO">The gendec departure vo.</param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "GENDECDEP-OPEN")]
        public ActionResult OpenGendec(GendecDepartureVO gendecDepartureVO)
        {
            GendecDepartureDto gendecDepartureDto = new GendecDepartureDto();
            GendecDepartureDto gendecDepartDto = new GendecDepartureDto();
            GendecDepartureVO gendecDepartureVOSaved = new GendecDepartureVO();

            if (gendecDepartureVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                gendecDepartDto = Mapper.Map<GendecDepartureVO, GendecDepartureDto>(gendecDepartureVO);

                // Cierra el Gendec
                this.gendecDepartureBusiness.OpenGendecDepartureButton(gendecDepartDto);

                // Se obtiene lo que ya se guardo
                gendecDepartureDto = this.gendecDepartureBusiness.GetGendecDeparture(gendecDepartureVO.Sequence, gendecDepartureVO.AirlineCode,
                                                                gendecDepartureVO.FlightNumber, gendecDepartureVO.Itinerarykey);
                
                // Realiza el mapeo
                gendecDepartureVOSaved = Mapper.Map<GendecDepartureDto, GendecDepartureVO>(gendecDepartureDto);
                
                // Obtiene el Itinerario
                gendecDepartureVOSaved.Itinerary = this.GetItinerary(gendecDepartureVO.Sequence, gendecDepartureVO.AirlineCode,
                                                                gendecDepartureVO.FlightNumber, gendecDepartureVO.Itinerarykey);
                
                // Manda mensaje de Exito
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessActive;
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            LoadCatalog(gendecDepartureVOSaved);

            //return this.View(gendecDepartureVOSaved);
            return this.RedirectToAction("Create", new
            {
                Sequence = gendecDepartureVOSaved.Sequence,
                AirlineCode = gendecDepartureVOSaved.AirlineCode,
                FlightNumber = gendecDepartureVOSaved.FlightNumber,
                ItineraryKey = gendecDepartureVOSaved.Itinerarykey
            });
        }

        /// <summary>
        /// Print the Gendec Departure
        /// </summary>
        /// <param name="Sequence"></param>
        /// <param name="AirlineCode"></param>
        /// <param name="FlightNumber"></param>
        /// <param name="ItineraryKey"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "GENDECDEP-PRINTREP")]
        public ActionResult Print(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey)
        {
            GendecDepartureDto gendecDepartureDto = new GendecDepartureDto();
            GendecDepartureVO gendecDepartureVO = new GendecDepartureVO();
            try
            {
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReportBusiness.GetPageReportByPageName("GendecDeparture");
                SetSiteMapValues(Sequence, AirlineCode, FlightNumber, ItineraryKey);

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

                    reportServicelModel.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("Gendec_Departure").Url;
                    return View("Report/ViewReport", reportServicelModel);
                }
                else
                {
                    gendecDepartureDto = this.gendecDepartureBusiness.GetGendecDeparture(Sequence, AirlineCode, FlightNumber, ItineraryKey);
                    gendecDepartureVO = Mapper.Map<GendecDepartureDto, GendecDepartureVO>(gendecDepartureDto);
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
                Sequence = gendecDepartureVO.Sequence,
                AirlineCode = gendecDepartureVO.AirlineCode,
                FlightNumber = gendecDepartureVO.FlightNumber,
                ItineraryKey = gendecDepartureVO.Itinerarykey
            });
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
        /// Load comboboxes
        /// </summary>
        private void LoadCatalog(GendecDepartureVO gendecDepartureVO)
        {
            this.ViewBag.CrewType = this.genericCatalogBusiness.GetCrewCatalog();
            this.ViewBag.Crew = this.genericCatalogBusiness.GetCrewSobCatalog();

            // USUARIOS ASC
            IList<GenericCatalogDto> userListFinal = new List<GenericCatalogDto>();

            // Asignamos a combo los proveedores viables
            userListFinal = this.genericCatalogBusiness.GetUserCatalog(gendecDepartureVO.Itinerary.DepartureStation, "ASC");
            this.ViewBag.Users = userListFinal;
        }

        /// <summary>
        /// Create a Gendec
        /// </summary>
        /// <param name="gendecDepartureVO"></param>
        /// <returns></returns>
        private GendecDepartureVO CreateGendec(GendecDepartureVO gendecDepartureVO)
        {
            try
            {
                gendecDepartureVO = this.GetCrewsByID(gendecDepartureVO);
                GendecDepartureDto gendecDto = Mapper.Map<GendecDepartureVO, GendecDepartureDto>(gendecDepartureVO);
                this.gendecDepartureBusiness.AddGendec(gendecDto);
                return gendecDepartureVO;
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 10)
                {
                    message = string.Format(message, "Primary Key");
                }

                this.ViewBag.ErrorMessage = message;
            }
            this.TempData["OperationSuccess"] = Resource.SuccessCreate;
            return gendecDepartureVO;
        }

        /// <summary>
        /// Edit to Gendec
        /// </summary>
        /// <param name="gendecDepartureVO"></param>
        /// <returns></returns>
        private GendecDepartureVO EditGendec(GendecDepartureVO gendecDepartureVO)
        {
            GendecDepartureDto gendecDepartureDto = new GendecDepartureDto();
            try
            {
                gendecDepartureDto = Mapper.Map<GendecDepartureVO, GendecDepartureDto>(gendecDepartureVO);
                this.gendecDepartureBusiness.EditGendec(gendecDepartureDto);
                return gendecDepartureVO;

            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 10)
                {
                    message = string.Format(message, "Primary Key");
                }

                this.ViewBag.ErrorMessage = message;
            }
            return gendecDepartureVO;
        }

        /// <summary>
        /// Get Gendec By ID
        /// </summary>
        /// <param name="gendecDepartureVO"></param>
        /// <returns></returns>
        private GendecDepartureVO GetCrewsByID(GendecDepartureVO gendecDepartureVO)
        {
            CrewVO crewVO = new CrewVO();

            if (gendecDepartureVO.Crews != null)
            {
                List<CrewVO> crewVOS = gendecDepartureVO.Crews.ToList();
                gendecDepartureVO.Crews = new List<CrewVO>();
                foreach (CrewVO item in crewVOS)
                {
                    crewVO = Mapper.Map<CrewDto, CrewVO>(this.crewBusiness.GetAllCrewByID(item.CrewID));
                    gendecDepartureVO.Crews.Add(crewVO);
                }
            }
            return gendecDepartureVO;
        }

        /// <summary>
        /// Get Crew For Delete
        /// </summary>
        /// <param name="gendecDepartureVO"></param>
        /// <param name="crewID"></param>
        /// <returns></returns>
        private GendecDepartureVO GetCrewForDelete(GendecDepartureVO gendecDepartureVO, long crewID)
        {
            CrewVO crewVO = new CrewVO();
            if (gendecDepartureVO.Crews != null && crewID != null)
            {
                List<CrewVO> crewVOS = gendecDepartureVO.Crews.ToList();
                gendecDepartureVO.Crews = new List<CrewVO>();

                crewVO = Mapper.Map<CrewDto, CrewVO>(this.crewBusiness.FindCrewById(crewID));
                gendecDepartureVO.Crews.Add(crewVO);
            }

            return gendecDepartureVO;
        }

        /// <summary>
        /// Gets the itinerary.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <returns></returns>
        private ItineraryVO GetItinerary(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey)
        {
            ItineraryDto itineraryDto = new ItineraryDto();
            ItineraryVO itineraryVO = new ItineraryVO();

            itineraryDto = this.itineraryBusiness.FindFlightById(Sequence, AirlineCode, FlightNumber, ItineraryKey);
            itineraryVO = Mapper.Map<ItineraryDto, ItineraryVO>(itineraryDto);

            return itineraryVO;
        }

        /// <summary>
        /// Set Site Map Values: EffectiveDate, AirlineCode, StationCode, ServiceCode, ProviderNumberPrimary
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        private static void SetSiteMapValues(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Gendec_Departure");
                if (node != null)
                {
                    node.RouteValues["Sequence"] = sequence;
                    node.RouteValues["AirlineCode"] = airlineCode;
                    node.RouteValues["FlightNumber"] = flightNumber;
                    node.RouteValues["ItineraryKey"] = itineraryKey;
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