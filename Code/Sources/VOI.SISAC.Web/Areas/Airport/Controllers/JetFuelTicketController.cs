//------------------------------------------------------------------------
// <copyright file="JetFuelTicketController.cs" company="Volaris">
//     Copyright(c) Volaris - Todos los derechos reservados.
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
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Finance;
    using VOI.SISAC.Business.Itineraries;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Models.VO.Itineraries;
    using VOI.SISAC.Web.Resources;
    using Business = VOI.SISAC.Business.Airport;
    using MvcSiteMapProvider;
    using VOI.SISAC.Business.Airport;

    /// <summary>
    /// Controlador de JetFuelTicket
    /// </summary>
    [CustomAuthorize]
    public class JetFuelTicketController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(FuelConceptController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.JetFuelTicketTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = VOI.SISAC.Web.Resources.Resource.JetFuelTicketID;

        /// <summary>
        /// Interfaz privada del negocio solo de lectura
        /// </summary>        
        private readonly Business.IJetFuelTicketBusiness jetFuelTicketBusiness;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

        /// <summary>
        /// The International Fuel Contract
        /// </summary>
        private readonly IInternationalFuelContractBusiness internationalFuelContract;

        /// <summary>
        /// The Users
        /// </summary>
        private readonly IUserBusiness user;

        /// <summary>
        /// The Itinerary
        /// </summary>
        private readonly IItineraryBusiness itinerary;

        /// <summary>
        /// The Itinerary
        /// </summary>
        private readonly IPageReportBusiness pageReport;

        /// <summary>
        /// The airport business
        /// </summary>
        private readonly IAirportBusiness airportBusiness;


        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelTicketController" /> class.
        /// </summary>
        /// <param name="jetFuelTicketBusiness">The jet fuel ticket business.</param>
        /// <param name="generic">The generic.</param>
        /// <param name="internationalFuelContract">The international fuel contract.</param>
        /// <param name="user">The user.</param>
        /// <param name="itinerary">The itinerary.</param>
        /// <param name="pageReport">The page report.</param>
        /// <param name="airportBusiness">The airport business.</param>
        public JetFuelTicketController(Business.IJetFuelTicketBusiness jetFuelTicketBusiness,
                                       IGenericCatalogBusiness generic,
                                       IInternationalFuelContractBusiness internationalFuelContract,
                                       IUserBusiness user,
                                       IItineraryBusiness itinerary,
                                       IPageReportBusiness pageReport,
                                       IAirportBusiness airportBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.jetFuelTicketBusiness = jetFuelTicketBusiness;
            this.generic = generic;
            this.internationalFuelContract = internationalFuelContract;
            this.user = user;
            this.itinerary = itinerary;
            this.pageReport = pageReport;
            this.airportBusiness = airportBusiness;
        }

        /// <summary>
        /// Shows the specified report.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "JETFUELTIC-PRINTREP")]
        public ActionResult VerReporte(long id)
        {
            JetFuelTicketVO jetFuelTicketVO = new JetFuelTicketVO();
            try
            {
                string reportPath = "";
                PageReportDto pageReportDto = new PageReportDto();
                pageReportDto = this.pageReport.GetPageReportByPageName("JetFuelTicket");

                var code = jetFuelTicketBusiness.FindJetFuelTicketById(id);
                SetSiteMapValues(code);

                if (pageReportDto != null)
                {
                    reportPath = pageReportDto.PathReport;
                }

                if (!String.IsNullOrEmpty(reportPath) && !String.IsNullOrEmpty(id.ToString()))
                {
                    ReportingServiceViewModel model = new ReportingServiceViewModel(
                        reportPath,
                        new List<Microsoft.Reporting.WebForms.ReportParameter>()
                        { 
                            new Microsoft.Reporting.WebForms.ReportParameter("JetFuelTicketID",id.ToString(),false) 
                        });

                    model.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("JetFuel_Ticket").Url;
                    return View("Report/ViewReport", model);
                }
                else
                {
                    jetFuelTicketVO = Mapper.Map<JetFuelTicketDto, JetFuelTicketVO>(jetFuelTicketBusiness.FindJetFuelTicketById(id));
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

            return this.RedirectToAction("Index", new
            {
                Sequence = jetFuelTicketVO.Sequence,
                AirlineCode = jetFuelTicketVO.AirlineCode,
                FlightNumber = jetFuelTicketVO.FlightNumber,
                ItineraryKey = jetFuelTicketVO.ItineraryKey,
                OperationTypeName = jetFuelTicketVO.OperationTypeName
            });
        }

        /// <summary>
        /// Vista Principal
        /// </summary>
        /// <returns>Vista con todos los Tickets de Combustible</returns>
        [CustomAuthorize(Roles = "JETFUELTIC-IDX")]
        public ActionResult Index(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey, string OperationTypeName)
        {
            System.Diagnostics.Trace.TraceInformation("Beginning FuelConcept");
            IList<JetFuelTicketVO> jetFuelTicketVo = new List<JetFuelTicketVO>();
            SaveItineraryOnView(Sequence, AirlineCode, FlightNumber, ItineraryKey, OperationTypeName);
            ItineraryDto itineraryDto = new ItineraryDto();
            AirportDto airportDto = new AirportDto();

            try
            {
                itineraryDto = this.itinerary.FindFlightById(Sequence, AirlineCode, FlightNumber, ItineraryKey);

                jetFuelTicketVo = Mapper.Map<IList<JetFuelTicketDto>, IList<JetFuelTicketVO>>(this.jetFuelTicketBusiness.GetJetFuelTickets(itineraryDto, OperationTypeName));

                //Para mostrar partialView en index
                ItineraryVO itineraryVO = new ItineraryVO();

                itineraryVO = Mapper.Map<ItineraryVO>(itineraryDto);

                //Sino tiene permiso para editar aeropuerto
                if (NotContainsAirport(itineraryVO.DepartureStation))
                {
                    return RedirectToAction("Unauthorized", "Home", new { area = "" });
                }

                this.ViewBag.ItineraryVO = itineraryVO;


                // Se obtiene Código de Pais, solo  mostrará Gendec cuando el aeropuerto sea Internacional
                airportDto = this.airportBusiness.FindAirportById(itineraryVO.DepartureStation);


                if (airportDto.CountryCode != "MEX")
                {
                    return View(jetFuelTicketVo);
                }
                else
                {
                    this.TempData["OperationSuccess"] = "Jet Fuel Ticket solo se captura para vuelos a la salida Internacionales";
                    return RedirectToAction("Index", "Itinerary", new { area = "Itineraries" });
                }

            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);

                return this.View(jetFuelTicketVo);
            }
        }

        /// <summary>
        /// Vista create
        /// </summary>
        /// <returns>Vista create</returns>
        [CustomAuthorize(Roles = "JETFUELTIC-ADD")]
        public ActionResult Create(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey, string OperationTypeName)
        {
            JetFuelTicketVO jetFuelTicketVOinit = new JetFuelTicketVO();
            LoadCatalogsInit();
            SaveItineraryOnView(Sequence, AirlineCode, FlightNumber, ItineraryKey, OperationTypeName);

            try
            {
                //Inicializar combos
                ItineraryDto itineraryDto = new ItineraryDto();
                ItineraryVO itineraryVO = new ItineraryVO();
                itineraryDto = this.itinerary.FindFlightById(Sequence, AirlineCode, FlightNumber, ItineraryKey);
                itineraryVO = Mapper.Map<ItineraryVO>(itineraryDto);
                LoadCatalogs(itineraryVO, OperationTypeName);

                //JetFuelTicketDto jetFuelTicketDtoinit = new JetFuelTicketDto();
                jetFuelTicketVOinit = new JetFuelTicketVO()
                {
                    Sequence = itineraryDto.Sequence,
                    AirlineCode = itineraryDto.AirlineCode,
                    FlightNumber = itineraryDto.FlightNumber,
                    ItineraryKey = itineraryDto.ItineraryKey,
                    OperationTypeName = OperationTypeName,
                    FuelingDate = DateTime.Today
                };

                //Inicializar Informacion de Itinerary
                jetFuelTicketVOinit.Itinerary = itineraryVO;

                //jetFuelTicketVOinit = Mapper.Map<JetFuelTicketDto, JetFuelTicketVO>(jetFuelTicketDtoinit);

                //if (itineraryVO == null)
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //if (jetFuelTicketVOinit == null)
                //    return HttpNotFound();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.View(jetFuelTicketVOinit);
        }

        /// <summary>
        /// Create a new JetFuelTicket
        /// </summary>
        /// <param name="jetFuelTicketVO">Valores a guardar</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "JETFUELTIC-ADD")]
        public ActionResult Create(JetFuelTicketVO jetFuelTicketVO)
        {
            LoadCatalogsInit();
            if (jetFuelTicketVO != null)
                SaveItineraryOnView(jetFuelTicketVO.Sequence, jetFuelTicketVO.AirlineCode, jetFuelTicketVO.FlightNumber, jetFuelTicketVO.ItineraryKey, jetFuelTicketVO.OperationTypeName);

            try
            {
                //Inicializar combos
                ItineraryDto itineraryDto = new ItineraryDto();
                ItineraryVO itineraryVO = new ItineraryVO();
                if (jetFuelTicketVO != null)
                    itineraryDto = this.itinerary.FindFlightById(jetFuelTicketVO.Sequence, jetFuelTicketVO.AirlineCode, jetFuelTicketVO.FlightNumber, jetFuelTicketVO.ItineraryKey);

                itineraryVO = Mapper.Map<ItineraryVO>(itineraryDto);
                LoadCatalogs(itineraryVO, jetFuelTicketVO.OperationTypeName);

                if (jetFuelTicketVO == null)
                {
                    Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                    Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (ModelState.IsValid)
                {
                    jetFuelTicketVO.Itinerary = null;
                    JetFuelTicketDto jetFuelTicketDto = Mapper.Map<JetFuelTicketVO, JetFuelTicketDto>(jetFuelTicketVO);
                    jetFuelTicketBusiness.AddJetFuelTicket(jetFuelTicketDto);
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessCreate;

                    return this.RedirectToAction("Index", new
                    {
                        Sequence = jetFuelTicketVO.Sequence,
                        AirlineCode = jetFuelTicketVO.AirlineCode,
                        FlightNumber = jetFuelTicketVO.FlightNumber,
                        ItineraryKey = jetFuelTicketVO.ItineraryKey,
                        OperationTypeName = jetFuelTicketVO.OperationTypeName
                    });
                }
                else
                {
                    jetFuelTicketVO.Itinerary = itineraryVO;
                }
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
                    message = string.Format(message, primaryKey);
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(jetFuelTicketVO);
        }

        /// <summary>
        /// Vista para editar registros JetFuelTicket
        /// </summary>
        /// <param name="id">Identificador del item del catálogo a editar</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "JETFUELTIC-UPD")]
        public ActionResult Edit(long id, int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey, string OperationTypeName)
        {
            JetFuelTicketVO jetFuelTicketVO = new JetFuelTicketVO();
            LoadCatalogsInit();
            SaveItineraryOnView(Sequence, AirlineCode, FlightNumber, ItineraryKey, OperationTypeName);

            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                //Inicializar combos
                ItineraryDto itineraryDto = new ItineraryDto();
                ItineraryVO itineraryVO = new ItineraryVO();
                itineraryDto = this.itinerary.FindFlightById(Sequence, AirlineCode, FlightNumber, ItineraryKey);
                itineraryVO = Mapper.Map<ItineraryVO>(itineraryDto);
                LoadCatalogs(itineraryVO, OperationTypeName);

                if (string.IsNullOrEmpty(id.ToString()))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                jetFuelTicketVO = Mapper.Map<JetFuelTicketDto, JetFuelTicketVO>(jetFuelTicketBusiness.FindJetFuelTicketById(id));

                if (jetFuelTicketVO == null)
                    return HttpNotFound();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            jetFuelTicketVO.Itinerary = null;
            return this.View(jetFuelTicketVO);
        }

        /// <summary>
        /// Vista para editar registros JetFuelTicket
        /// </summary>
        /// <param name="jetFuelTicketVO">Objecto de tipo jet fuel ticket que toma la información del formulario</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "JETFUELTIC-UPD")]
        public ActionResult Edit(JetFuelTicketVO jetFuelTicketVO)
        {
            LoadCatalogsInit();
            if (jetFuelTicketVO != null)
            {
                SaveItineraryOnView(jetFuelTicketVO.Sequence, jetFuelTicketVO.AirlineCode, jetFuelTicketVO.FlightNumber, jetFuelTicketVO.ItineraryKey, jetFuelTicketVO.OperationTypeName);
            }

            if (jetFuelTicketVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                //Inicializar combos
                ItineraryDto itineraryDto = new ItineraryDto();
                ItineraryVO itineraryVO = new ItineraryVO();
                if (jetFuelTicketVO != null)
                    itineraryDto = this.itinerary.FindFlightById(jetFuelTicketVO.Sequence, jetFuelTicketVO.AirlineCode, jetFuelTicketVO.FlightNumber, jetFuelTicketVO.ItineraryKey);

                itineraryVO = Mapper.Map<ItineraryVO>(itineraryDto);
                LoadCatalogs(itineraryVO, jetFuelTicketVO.OperationTypeName);

                if (jetFuelTicketVO == null)
                {
                    Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                    Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (ModelState.IsValid)
                {
                    jetFuelTicketVO.Itinerary = null;
                    jetFuelTicketBusiness.UpdateJetFuelTicket(Mapper.Map<JetFuelTicketVO, JetFuelTicketDto>(jetFuelTicketVO));
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;

                    return this.RedirectToAction("Index", new
                    {
                        Sequence = jetFuelTicketVO.Sequence,
                        AirlineCode = jetFuelTicketVO.AirlineCode,
                        FlightNumber = jetFuelTicketVO.FlightNumber,
                        ItineraryKey = jetFuelTicketVO.ItineraryKey,
                        OperationTypeName = jetFuelTicketVO.OperationTypeName
                    });
                }
                else
                {
                    jetFuelTicketVO.Itinerary = itineraryVO;
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

            return View(jetFuelTicketVO);
        }


        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="OperationTypeName">Name of the operation type.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "JETFUELTIC-VIE")]
        public ActionResult Details(long id, int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey, string OperationTypeName)
        {
            JetFuelTicketVO jetFuelTicketVO = new JetFuelTicketVO();
            SaveItineraryOnView(Sequence, AirlineCode, FlightNumber, ItineraryKey, OperationTypeName);

            try
            {
                jetFuelTicketVO = Mapper.Map<JetFuelTicketDto, JetFuelTicketVO>(jetFuelTicketBusiness.FindJetFuelTicketById(id));
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.View(jetFuelTicketVO);
        }

        /// <summary>
        /// Vista Delete
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa la vista Delete precargada con la información a eliminar</returns>
        [CustomAuthorize(Roles = "JETFUELTIC-DEL")]
        public ActionResult Delete(long id)
        {
            LoadCatalogsInit();
            JetFuelTicketVO jetFuelTicketVO = new JetFuelTicketVO();
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                jetFuelTicketVO = Mapper.Map<JetFuelTicketDto, JetFuelTicketVO>(jetFuelTicketBusiness.FindJetFuelTicketById(id));

                //Inicializar combos
                ItineraryDto itineraryDto = new ItineraryDto();
                ItineraryVO itineraryVO = new ItineraryVO();
                if (jetFuelTicketVO != null)
                    itineraryDto = this.itinerary.FindFlightById(jetFuelTicketVO.Sequence, jetFuelTicketVO.AirlineCode, jetFuelTicketVO.FlightNumber, jetFuelTicketVO.ItineraryKey);

                itineraryVO = Mapper.Map<ItineraryVO>(itineraryDto);
                LoadCatalogs(itineraryVO, jetFuelTicketVO.OperationTypeName);

                SetSiteMapValues(jetFuelTicketVO);

                if (jetFuelTicketVO != null)
                    SaveItineraryOnView(jetFuelTicketVO.Sequence, jetFuelTicketVO.AirlineCode, jetFuelTicketVO.FlightNumber, jetFuelTicketVO.ItineraryKey, jetFuelTicketVO.OperationTypeName);

                if (jetFuelTicketVO == null)
                    return HttpNotFound();
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);

            }

            return View(jetFuelTicketVO);
        }

        /// <summary>
        /// Set Site Map Values: Sequence, AirlineCode, FlightNumber, ItineraryKey, OperationTypeName
        /// </summary>
        /// <param name="vo"></param>
        private static void SetSiteMapValues(JetFuelTicketVO vo)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("JetFuel_Ticket");
                if (node != null)
                {
                    node.RouteValues["Sequence"] = vo.Sequence;
                    node.RouteValues["AirlineCode"] = vo.AirlineCode;
                    node.RouteValues["FlightNumber"] = vo.FlightNumber;
                    node.RouteValues["ItineraryKey"] = vo.ItineraryKey;
                    node.RouteValues["OperationTypeName"] = vo.OperationTypeName;
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
                Trace.TraceError(exception.Message, exception);
            }
        }

        /// <summary>
        /// Vista Delete POST
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa a la vista principal</returns>        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "JETFUELTIC-DEL")]
        public ActionResult DeleteConfirmed(long id)
        {
            JetFuelTicketVO jetFuelTicketVO = new JetFuelTicketVO();
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                JetFuelTicketDto jetFuelTicketDto = new JetFuelTicketDto();
                jetFuelTicketDto = jetFuelTicketBusiness.FindJetFuelTicketById(id);
                if (jetFuelTicketDto == null)
                {
                    return this.HttpNotFound();
                }

                jetFuelTicketVO = Mapper.Map<JetFuelTicketDto, JetFuelTicketVO>(jetFuelTicketDto);

                if (jetFuelTicketVO != null)
                    SaveItineraryOnView(jetFuelTicketVO.Sequence, jetFuelTicketVO.AirlineCode, jetFuelTicketVO.FlightNumber, jetFuelTicketVO.ItineraryKey, jetFuelTicketVO.OperationTypeName);

                jetFuelTicketBusiness.DeleteJetFuelTicket(jetFuelTicketDto);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;

                return this.RedirectToAction("Index", new
                {
                    Sequence = jetFuelTicketVO.Sequence,
                    AirlineCode = jetFuelTicketVO.AirlineCode,
                    FlightNumber = jetFuelTicketVO.FlightNumber,
                    ItineraryKey = jetFuelTicketVO.ItineraryKey,
                    OperationTypeName = jetFuelTicketVO.OperationTypeName
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

            return this.View(jetFuelTicketVO);
        }

        #region Otros
        /// <summary>
        /// LoadCatalogsInit
        /// </summary>
        private void LoadCatalogsInit()
        {
            //Service 

            //Lista final con información requerida
            IList<GenericCatalogDto> serviceListFilter = new List<GenericCatalogDto>();

            //Asignamos a combo los servicios viables (combustible)
            this.ViewBag.Service = serviceListFilter;

            //Proveedores JET FUEL

            //Lista final con información requerida
            IList<GenericCatalogDto> providerListFinal = new List<GenericCatalogDto>();

            //Asignamos a combo los proveedores viables
            this.ViewBag.ProviderJetFuel = providerListFinal;


            //Proveedores INTO PLANE FUEL

            //Asignamos a combo los proveedores viables
            this.ViewBag.ProviderIntoPlane = providerListFinal;


            //Usuarios AOR

            //Lista final con información requerida
            IList<GenericCatalogDto> userListFinal = new List<GenericCatalogDto>();

            //Asignamos a combo los proveedores viables
            this.ViewBag.userAOR = userListFinal;
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
        private void LoadCatalogs(ItineraryVO itineraryVO, string operationTypeName)
        {
            string airport = "";


            if (itineraryVO != null)
                if (operationTypeName != "")//Validar que si haya valor de operationTypeName
                    if (operationTypeName == "LLEGADA")
                    {
                        airport = itineraryVO.ArrivalStation;
                    }
                    else
                    {
                        airport = itineraryVO.DepartureStation;
                    }

            //Servicios de Combustible           
            IList<GenericCatalogDto> serviceList = new List<GenericCatalogDto>();
            //Lista final con información requerida
            IList<GenericCatalogDto> serviceListFilter = new List<GenericCatalogDto>();
            serviceList = this.generic.GetServiceCatalog();
            foreach (GenericCatalogDto item in serviceList)
            {
                if ((item.Id.Contains("-EXT")))
                {
                    serviceListFilter.Add(item);
                }
            }
            //Asignamos a combo los servicios viables (combustible)
            this.ViewBag.Service = serviceListFilter;


            //Proveedores JET FUEL

            //Contratos compatibles con Airline y Airport
            IList<InternationalFuelContractDto> contratosByAirlineAirport = new List<InternationalFuelContractDto>();
            if (itineraryVO != null)
                contratosByAirlineAirport = this.internationalFuelContract.GetActivesFuelContracts(itineraryVO.AirlineCode, airport, operationTypeName);

            //Lista completa de proveedores
            IList<GenericCatalogDto> providerListComplete = new List<GenericCatalogDto>();

            //Lista final con información requerida
            IList<GenericCatalogDto> providerListFinal = new List<GenericCatalogDto>();

            //LLenamos lista completa
            providerListComplete = this.generic.GetProviderCatalog();

            //Buscamos los compatibles en lista completa
            GenericCatalogDto encontrado = new GenericCatalogDto();
            foreach (InternationalFuelContractDto item in contratosByAirlineAirport)
            {
                encontrado = providerListComplete.Where(x => x.Id == item.ProviderNumberPrimary).FirstOrDefault();

                if (encontrado != null)
                {
                    providerListFinal.Add(encontrado);
                }
                encontrado = null;
            }
            //Asignamos a combo los proveedores viables
            this.ViewBag.ProviderJetFuel = providerListFinal;


            //Proveedores INTO PLANE FUEL

            //Buscamos los compatibles en lista completa
            GenericCatalogDto repetido = new GenericCatalogDto();
            providerListFinal = new List<GenericCatalogDto>();
            foreach (InternationalFuelContractDto contrato in contratosByAirlineAirport)
            {
                foreach (InternationalFuelContractConceptDto concepto in contrato.InternationalFuelContractConcepts)
                {
                    if (concepto.FuelConceptTypeCode == "INTPL")
                    {
                        encontrado = providerListComplete.Where(x => x.Id == concepto.ProviderNumber).FirstOrDefault();
                        repetido = providerListFinal.Where(x => x.Id == encontrado.Id).FirstOrDefault();


                        if (encontrado != null && repetido == null)
                        {
                            providerListFinal.Add(encontrado);
                        }

                        encontrado = null;
                        repetido = null;
                    }
                }
            }
            //Asignamos a combo los proveedores viables
            this.ViewBag.ProviderIntoPlane = providerListFinal;


            //USUARIOS AOR
            IList<GenericCatalogDto> userListFinal = new List<GenericCatalogDto>();
            //Asignamos a combo los proveedores viables
            userListFinal = this.generic.GetUserCatalog(airport, "AOR");
            this.ViewBag.userAOR = userListFinal;

        }

        /// <summary>
        /// Saves the itinerary on view.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="OperationTypeName">Name of the operation type.</param>
        private void SaveItineraryOnView(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey, string OperationTypeName)
        {
            this.ViewBag.Sequence = Sequence.ToString();
            this.ViewBag.AirlineCode = AirlineCode;
            this.ViewBag.FlightNumber = FlightNumber;
            this.ViewBag.ItineraryKey = ItineraryKey;
            this.ViewBag.OperationTypeName = OperationTypeName;
        }

        /// <summary>
        /// Set Site Map Values: Sequence, AirlineCode, FlightNumber, ItineraryKey, OperationTypeName
        /// </summary>
        /// <param name="jetFuelTicket">service data transfer object.</param>
        private static void SetSiteMapValues(JetFuelTicketDto jetFuelTicket)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("JetFuel_Ticket");
                if (node != null)
                {
                    node.RouteValues["Sequence"] = jetFuelTicket.Sequence;
                    node.RouteValues["AirlineCode"] = jetFuelTicket.AirlineCode;
                    node.RouteValues["FlightNumber"] = jetFuelTicket.FlightNumber;
                    node.RouteValues["ItineraryKey"] = jetFuelTicket.ItineraryKey;
                    node.RouteValues["OperationTypeName"] = jetFuelTicket.OperationTypeName;
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
            userDto = this.user.GetUserByUserName(this.User.Identity.Name);
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

        #endregion
    }
}