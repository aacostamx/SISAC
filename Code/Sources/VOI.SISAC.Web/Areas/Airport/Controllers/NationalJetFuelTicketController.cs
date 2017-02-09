//------------------------------------------------------------------------
// <copyright file="NationalJetFuelTicketController.cs" company="AACOSTA">
//     Copyright(c) AACOSTA - Todos los derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using AutoMapper;
    using Business.Airport;
    using Business.Common;
    using Business.Dto.Airports;
    using Business.Dto.Catalogs;
    using Business.Dto.Finances;
    using Business.Dto.Security;
    using Business.ExceptionBusiness;
    using Business.Finance;
    using Business.Itineraries;
    using Business.Security;
    using Helpers;
    using Microsoft.Reporting.WebForms;
    using Models.VO.Airport;
    using Models.VO.Itineraries;
    using MvcSiteMapProvider;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Web.Controllers;

    /// <summary>
    /// NationalJetFuelTicketController Class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    [CustomAuthorize]
    public class NationalJetFuelTicketController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NationalJetFuelTicketController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// The module name
        /// </summary>
        private readonly string moduleName = Resource.NationalJetFuelTicketTitle;

        /// <summary>
        /// The NTL jet fuel ticket business
        /// </summary>
        private readonly INationalJetFuelTicketBusiness ntlJetFuelTicketBusiness;

        /// <summary>
        /// The page report business
        /// </summary>
        private readonly IPageReportBusiness pageReportBusiness;

        /// <summary>
        /// The itinerary business
        /// </summary>
        private readonly IItineraryBusiness itineraryBusiness;

        /// <summary>
        /// The NTL fuel contract business
        /// </summary>
        private readonly INationalFuelContractBusiness ntlFuelContractBusiness;

        /// <summary>
        /// The generic
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

        /// <summary>
        /// The Users
        /// </summary>
        private readonly IUserBusiness user;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicketController"/> class.
        /// </summary>
        /// <param name="ntlJetFuelTicketBusiness">The NTL jet fuel ticket business.</param>
        /// <param name="generic">The generic.</param>
        /// <param name="pageReportBusiness">The page report business.</param>
        /// <param name="itineraryBusiness">The itinerary business.</param>
        /// <param name="ntlFuelContractBusiness">The NTL fuel contract business.</param>
        /// <param name="user">The user business.</param>
        public NationalJetFuelTicketController(INationalJetFuelTicketBusiness ntlJetFuelTicketBusiness,
            IGenericCatalogBusiness generic, IPageReportBusiness pageReportBusiness, IItineraryBusiness itineraryBusiness,
             INationalFuelContractBusiness ntlFuelContractBusiness, IUserBusiness user)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.ntlJetFuelTicketBusiness = ntlJetFuelTicketBusiness;
            this.ntlFuelContractBusiness = ntlFuelContractBusiness;
            this.pageReportBusiness = pageReportBusiness;
            this.itineraryBusiness = itineraryBusiness;
            this.generic = generic;
            this.user = user;
        }

        /// <summary>
        /// Indexes the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLFUELTIC-IDX")]
        public ActionResult Index(NationalJetFuelTicketVO ticket)
        {
            var itinerary = new ItineraryVO();
            var ticketVO = new List<NationalJetFuelTicketVO>();
            var ticketDto = new NationalJetFuelTicketDto(ticket.Sequence, ticket.AirlineCode, ticket.FlightNumber, ticket.ItineraryKey, ticket.OperationTypeName);

            try
            {
                ticketVO = Mapper.Map<List<NationalJetFuelTicketVO>>(this.ntlJetFuelTicketBusiness.GetNationalJetFuelTickets(ticketDto));
                itinerary = this.GetItinerary(ticket);

                if (this.NotContainsAirport(itinerary.DepartureStation))
                    return this.RedirectToAction("Unauthorized", "Home", new { area = "" });

                SaveItineraryOnViewBag(itinerary);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(ticketVO);
        }

        /// <summary>
        /// Creates the specified sequence.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="OperationTypeName">Name of the operation type.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLFUELTIC-ADD")]
        public ActionResult Create(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey, string OperationTypeName)
        {
            var ticket = new NationalJetFuelTicketVO(Sequence, AirlineCode, FlightNumber, ItineraryKey, OperationTypeName);
            var itinerary = new ItineraryVO();

            try
            {
                itinerary = this.GetItinerary(ticket);
                this.SaveItineraryOnViewBag(itinerary);
                this.LoadComboBoxContainers(itinerary);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                this.ViewBag.ErrorMessage = message;
                this.LoadComboBoxContainers(itinerary);
            }

            return this.View(ticket);
        }

        /// <summary>
        /// Creates the specified jet fuel ticket vo.
        /// </summary>
        /// <param name="jetFuelTicketVO">The jet fuel ticket vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NTLFUELTIC-ADD")]
        public ActionResult Create(NationalJetFuelTicketVO jetFuelTicketVO)
        {
            var itineraryVO = new ItineraryVO();

            try
            {
                this.BadRequest(jetFuelTicketVO);

                if (ModelState.IsValid)
                {
                    SetTime(jetFuelTicketVO);
                    this.ntlJetFuelTicketBusiness.AddNationalJetFuelTicket(Mapper.Map<NationalJetFuelTicketDto>(jetFuelTicketVO));
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;

                    return this.RedirectToAction("Index", new
                    {
                        Sequence = jetFuelTicketVO.Sequence,
                        AirlineCode = jetFuelTicketVO.AirlineCode,
                        FlightNumber = jetFuelTicketVO.FlightNumber,
                        ItineraryKey = jetFuelTicketVO.ItineraryKey,
                        OperationTypeName = jetFuelTicketVO.OperationTypeName
                    });
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                message = ex.Number == 10 ? string.Format(message, Resource.NationalJetFuelTicketTitle) : message;
                this.ViewBag.ErrorMessage = message;
            }

            itineraryVO = this.GetItinerary(jetFuelTicketVO);
            SaveItineraryOnViewBag(itineraryVO);
            LoadComboBoxContainers(itineraryVO);

            return this.View(jetFuelTicketVO);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLFUELTIC-UPD")]
        public ActionResult Edit(long id)
        {

            var ticket = new NationalJetFuelTicketVO();
            var itinerary = new ItineraryVO();

            try
            {
                ticket = Mapper.Map<NationalJetFuelTicketVO>(this.ntlJetFuelTicketBusiness.FindNationalJetFuelTicket(id));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.BeginUpdate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.BeginUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            itinerary = this.GetItinerary(ticket);
            SetSiteMapValues(ticket);
            SaveItineraryOnViewBag(itinerary);
            LoadComboBoxContainers(itinerary);

            return this.View(ticket);
        }

        /// <summary>
        /// Edits the specified jet fuel ticket vo.
        /// </summary>
        /// <param name="jetFuelTicketVO">The jet fuel ticket vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NTLFUELTIC-UPD")]
        public ActionResult Edit(NationalJetFuelTicketVO jetFuelTicketVO)
        {
            var itinerary = new ItineraryVO();

            try
            {
                this.BadRequest(jetFuelTicketVO);

                if (ModelState.IsValid)
                {
                    SetTime(jetFuelTicketVO);
                    this.ntlJetFuelTicketBusiness.UpdateNationalJetFuelTicket(Mapper.Map<NationalJetFuelTicketDto>(jetFuelTicketVO));
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;

                    return this.RedirectToAction("Index", new
                    {
                        Sequence = jetFuelTicketVO.Sequence,
                        AirlineCode = jetFuelTicketVO.AirlineCode,
                        FlightNumber = jetFuelTicketVO.FlightNumber,
                        ItineraryKey = jetFuelTicketVO.ItineraryKey,
                        OperationTypeName = jetFuelTicketVO.OperationTypeName
                    });
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.BeginUpdate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.BeginUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            itinerary = this.GetItinerary(jetFuelTicketVO);
            SetSiteMapValues(jetFuelTicketVO);
            SaveItineraryOnViewBag(itinerary);
            LoadComboBoxContainers(itinerary);

            return this.View(jetFuelTicketVO);
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLFUELTIC-VIE")]
        public ActionResult Details(long id)
        {
            var ticket = new NationalJetFuelTicketVO();
            var itinerary = new ItineraryVO();

            try
            {
                ticket = Mapper.Map<NationalJetFuelTicketVO>(this.ntlJetFuelTicketBusiness.FindNationalJetFuelTicket(id));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            itinerary = this.GetItinerary(ticket);
            SaveItineraryOnViewBag(itinerary);
            SetSiteMapValues(ticket);

            return this.View(ticket);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLFUELTIC-DEL")]
        public ActionResult Delete(long id)
        {
            var ticket = new NationalJetFuelTicketVO();
            var itinerary = new ItineraryVO();

            try
            {
                ticket = Mapper.Map<NationalJetFuelTicketVO>(this.ntlJetFuelTicketBusiness.FindNationalJetFuelTicket(id));
                this.BadRequest(ticket);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            itinerary = this.GetItinerary(ticket);
            SaveItineraryOnViewBag(itinerary);
            SetSiteMapValues(ticket);

            return View(ticket);
        }

        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        [CustomAuthorize(Roles = "NTLFUELTIC-DEL")]
        public ActionResult DeleteConfirmed(long id)
        {
            NationalJetFuelTicketVO ticket = new NationalJetFuelTicketVO();
            ItineraryVO itinerary = new ItineraryVO();

            try
            {
                ticket = Mapper.Map<NationalJetFuelTicketVO>(this.ntlJetFuelTicketBusiness.FindNationalJetFuelTicket(id));
                this.BadRequest(ticket);

                if (ModelState.IsValid)
                {
                    this.ntlJetFuelTicketBusiness.DeleteNationalJetFuelTicket(Mapper.Map<NationalJetFuelTicketDto>(ticket));
                    this.TempData["OperationSuccess"] = Resource.SuccessDelete;

                    return this.RedirectToAction("Index", new
                    {
                        Sequence = ticket.Sequence,
                        AirlineCode = ticket.AirlineCode,
                        FlightNumber = ticket.FlightNumber,
                        ItineraryKey = ticket.ItineraryKey,
                        OperationTypeName = ticket.OperationTypeName
                    });
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.BeginDelete, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.BeginDelete, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            itinerary = this.GetItinerary(ticket);
            SaveItineraryOnViewBag(itinerary);
            SetSiteMapValues(ticket);
            return this.View(ticket);
        }

        /// <summary>
        /// Shows the report.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NTLFUELTIC-PRINTREP")]
        public ActionResult ShowReport(long id)
        {
            string reportPath = string.Empty;
            var pageReport = new PageReportDto();
            var ntlJetFuelTicketVO = new NationalJetFuelTicketVO();
            var ntlJetFuelTicketDto = new NationalJetFuelTicketDto();

            try
            {
                pageReport = this.pageReportBusiness.GetPageReportByPageName("NationalJetFuelTicket");
                ntlJetFuelTicketDto = this.ntlJetFuelTicketBusiness.FindNationalJetFuelTicket(id);
                NationalJetFuelTicketVO ticket = Mapper.Map<NationalJetFuelTicketVO>(ntlJetFuelTicketDto);
                SetSiteMapValues(ticket);
                reportPath = pageReport != null ? pageReport.PathReport : string.Empty;

                if (!string.IsNullOrEmpty(reportPath) && id > 0)
                {
                    List<ReportParameter> reportParameters = new List<ReportParameter>() { new ReportParameter("NationalJetFuelTicketID", id.ToString(), false) };
                    ReportingServiceViewModel reportModel = new ReportingServiceViewModel(reportPath, reportParameters);
                    reportModel.PageSourceUrl = SiteMaps.Current.FindSiteMapNodeFromKey("NationalJetFuelTicket").Url;
                    return View("Report/ViewReport", reportModel);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            ntlJetFuelTicketVO = Mapper.Map<NationalJetFuelTicketVO>(ntlJetFuelTicketDto);
            return this.RedirectToAction("Index", ntlJetFuelTicketVO);
        }

        /// <summary>
        /// Gets the itinerary.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        private ItineraryVO GetItinerary(NationalJetFuelTicketVO ticket)
        {
            var itineraryVO = new ItineraryVO();

            try
            {
                itineraryVO = Mapper.Map<ItineraryVO>(this.itineraryBusiness.FindFlightById(ticket.Sequence, ticket.AirlineCode, ticket.FlightNumber, ticket.ItineraryKey));
                itineraryVO.OperationTypeName = "SALIDA";
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }

            return itineraryVO;
        }

        /// <summary>
        /// Saves the itinerary on view bag.
        /// </summary>
        /// <param name="itinerary">The query string.</param>
        private void SaveItineraryOnViewBag(ItineraryVO itinerary)
        {
            try
            {
                this.ViewBag.ItineraryVO = itinerary;
                this.ViewBag.OperationTypeName = itinerary.OperationTypeName;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Sets the site map values.
        /// </summary>
        /// <param name="ticketVO">The ticket vo.</param>
        private static void SetSiteMapValues(NationalJetFuelTicketVO ticketVO)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("NationalJetFuelTicket");
                if (node != null)
                {
                    node.RouteValues["Sequence"] = ticketVO.Sequence;
                    node.RouteValues["AirlineCode"] = ticketVO.AirlineCode;
                    node.RouteValues["FlightNumber"] = ticketVO.FlightNumber;
                    node.RouteValues["ItineraryKey"] = ticketVO.ItineraryKey;
                    node.RouteValues["OperationTypeName"] = ticketVO.OperationTypeName;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Loads the ComboBox containers initialize.
        /// </summary>
        private void SetEmptyComboBoxContainers()
        {
            try
            {
                var serviceListFilter = new List<GenericCatalogDto>();
                var providerListFinal = new List<GenericCatalogDto>();
                var userListFinal = new List<GenericCatalogDto>();

                this.ViewBag.Service = serviceListFilter;
                this.ViewBag.ProviderJetFuel = providerListFinal;
                this.ViewBag.ProviderIntoPlane = providerListFinal;
                this.ViewBag.userAOR = userListFinal;

            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Loads the ComboBox containers.
        /// </summary>
        /// <param name="itinerary">The itinerary.</param>
        private void LoadComboBoxContainers(ItineraryVO itinerary)
        {
            string airport = string.Empty;
            var userAOR = new List<GenericCatalogDto>();
            var providers = new List<GenericCatalogDto>();
            var contractProviders = new List<GenericCatalogDto>();
            var serviceList = new List<GenericCatalogDto>();
            var contracts = new List<NationalFuelContractDto>();
            var supplyConcepts = new List<NationalFuelContractConceptDto>();
            var contractSupply = new List<GenericCatalogDto>();
            var contractDto = new NationalFuelContractDto() { AirlineCode = itinerary.AirlineCode, StationCode = itinerary.DepartureStation };

            try
            {
                if (itinerary != null)
                {
                    serviceList = this.generic.GetServiceCatalog().ToList();
                    serviceList.RemoveAll(c => !c.Id.Equals("CM"));
                    this.ViewBag.Service = serviceList;

                    contracts = this.ntlFuelContractBusiness.GetContractsByAirlineAirport(contractDto).ToList();
                    providers = this.generic.GetProviderCatalog().ToList();
                    contractProviders = providers.Where(pro => contracts.All(cont => pro.Id.Equals(cont.ProviderNumberPrimary))).ToList();
                    this.ViewBag.ProviderJetFuel = contracts.Count > 0 ? contractProviders : new List<GenericCatalogDto>();

                    supplyConcepts = contracts.SelectMany(sup => sup.NationalFuelContractConcept.Where(concept => concept.FuelConceptTypeCode.Equals("SUMIN"))).ToList();
                    contractSupply = providers.Where(cont => supplyConcepts.Any(sup => cont.Id.Equals(sup.ProviderNumber))).ToList();
                    this.ViewBag.ProviderSupply = supplyConcepts.Count > 0 ? contractSupply : new List<GenericCatalogDto>();

                    airport = itinerary.OperationTypeName.Equals("SALIDA") ? itinerary.DepartureStation : itinerary.ArrivalStation;
                    userAOR = this.generic.GetUserCatalog(airport, "AOR").ToList();
                    this.ViewBag.userAOR = userAOR;
                }

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                SetEmptyComboBoxContainers();
            }
        }

        /// <summary>
        /// Bads the request.
        /// </summary>
        /// <param name="jetFuelTicketVO">The jet fuel ticket vo.</param>
        /// <returns></returns>
        private HttpStatusCodeResult BadRequest(NationalJetFuelTicketVO jetFuelTicketVO)
        {
            if (jetFuelTicketVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Sets the time.
        /// </summary>
        /// <param name="jetFuelTicketVO">The jet fuel ticket vo.</param>
        private static void SetTime(NationalJetFuelTicketVO jetFuelTicketVO)
        {
            jetFuelTicketVO.FuelingTimeStart = jetFuelTicketVO.FuelingDateStart.TimeOfDay;
            jetFuelTicketVO.FuelingTimeEnd = jetFuelTicketVO.FuelingDateEnd.TimeOfDay;
        }

        /// <summary>
        /// Nots the contains airport.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <returns></returns>
        private bool NotContainsAirport(string stationCode)
        {
            var userDto = new UserDto();
            var airports = new List<string>();
            var contains = false;

            try
            {
                userDto = this.user.GetUserByUserName(this.User.Identity.Name);

                if (userDto.UserAirports != null)
                    airports = userDto.UserAirports.Select(c => c.StationCode).ToList();

                if (!airports.Contains(stationCode))
                    contains = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }

            return contains;
        }
    }
}