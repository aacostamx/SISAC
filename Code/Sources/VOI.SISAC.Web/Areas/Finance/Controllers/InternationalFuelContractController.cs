//-----------------------------------------------------------------------------
// <copyright file="InternationalFuelContractController.cs" company="AACOSTA">
//     Copyright(c) AACOSTA - Todos los derechos reservados.
// </copyright>
//----------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using FileHelpers;
    using Models.Files;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Resources;
    using Business = VOI.SISAC.Business.Finance;
    using VOI.SISAC.Business.Finance;
    using FileHelpers.Events;
    using System.Net.Mime;

    /// <summary>
    /// InternationalFuelContractController
    /// </summary>
    [CustomAuthorize]
    public class InternationalFuelContractController : BaseController
    {
        #region Variables
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(InternationalFuelContractController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = "International Contract";//VOI.SISAC.Web.Resources.Resource.FunctionalAreaTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = "concatenar Key";//VOI.SISAC.Web.Resources.Resource.FunctionalAreaID;

        /// <summary>
        /// Interfaz privada del negocio solo de lectura
        /// </summary>        
        private readonly Business.IInternationalFuelContractBusiness internationalFuelContractBusiness;

        /// <summary>
        /// Rates business
        /// </summary>
        private readonly Business.IInternationalFuelRateBusiness internationalFuelRateBusiness;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness generic;


        /// <summary>
        /// The massive upload business
        /// </summary>
        private readonly IMassiveUploadBusiness massiveUploadBusiness;
        #endregion

        #region constructor
        /// <summary>
        /// InternationalFuelContractController
        /// </summary>
        /// <param name="internationalFuelContractBusiness"></param>
        /// <param name="generic"></param>
        /// <param name="internationalFuelRateBusiness"></param>
        public InternationalFuelContractController(Business.IInternationalFuelContractBusiness internationalFuelContractBusiness, 
                                                   IGenericCatalogBusiness generic, 
                                                   Business.IInternationalFuelRateBusiness internationalFuelRateBusiness,
                                                   IMassiveUploadBusiness massiveUploadBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.internationalFuelContractBusiness = internationalFuelContractBusiness;
            this.internationalFuelRateBusiness = internationalFuelRateBusiness;
            this.generic = generic;
            this.massiveUploadBusiness = massiveUploadBusiness;
        }
        #endregion

        #region index
        /// <summary>
        /// Vista principal
        /// </summary>
        /// <returns>Vista con todas los International Fuel Contracts</returns>
        [CustomAuthorize(Roles = "IFUELCON-IDX")]
        public ActionResult Index()
        {
            Trace.TraceInformation("Inicia Index Contracts");
            LoadCatalogs();
            Trace.TraceInformation("Termina de carga los catalogos");

            try
            {
                IList<InternationalFuelContractVO> internationalFuelContractVoList = new List<InternationalFuelContractVO>();

                Trace.TraceInformation("Inicia llamado al Bussines");
                internationalFuelContractVoList = Mapper.Map<IList<InternationalFuelContractDto>, IList<InternationalFuelContractVO>>(internationalFuelContractBusiness.GetActivesLastEffectiveDateInternationalFuelContracts());
                Trace.TraceInformation("Finaliza llamado al Bussines");

                return this.View(internationalFuelContractVoList);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View();
            }
        }
        #endregion

        #region search

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="statusSearch"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(InternationalFuelContractVO entity, string statusSearch)
        {
            LoadCatalogs();
            IList<InternationalFuelContractVO> internationalFuelContractVoList = new List<InternationalFuelContractVO>();
            try
            {
                InternationalFuelContractDto contractSearchParam = new InternationalFuelContractDto();
                contractSearchParam = Mapper.Map<InternationalFuelContractVO, InternationalFuelContractDto>(entity);
                internationalFuelContractVoList = Mapper.Map<IList<InternationalFuelContractDto>, IList<InternationalFuelContractVO>>(this.internationalFuelContractBusiness.GetAllSearchedInternationalFuelContracts(contractSearchParam, statusSearch));
                ViewBag.search = "search";
                //internationalFuelContractVoList = LoadDescriptionCatalogs(internationalFuelContractVoList);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View();
            }

            return this.View("Index", internationalFuelContractVoList);
        }
        #endregion

        #region Reset
        /// <summary>
        /// Reset
        /// </summary>
        /// <param name="RateStartDate"></param>
        /// <param name="RateEndDate"></param>
        /// <param name="entity"></param>
        /// <param name="StatusSearch"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Reset(string RateStartDate, string RateEndDate, InternationalFuelContractVO entity, string StatusSearch)
        {
            LoadCatalogs();
            IList<InternationalFuelContractVO> internationalFuelContractVoList = new List<InternationalFuelContractVO>();
            try
            {
                internationalFuelContractVoList = Mapper.Map<IList<InternationalFuelContractDto>, IList<InternationalFuelContractVO>>(internationalFuelContractBusiness.GetActivesLastEffectiveDateInternationalFuelContracts());
                ViewBag.search = "reset";
                //internationalFuelContractVoList = LoadDescriptionCatalogs(internationalFuelContractVoList);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View();
            }
            return this.View("Index", internationalFuelContractVoList);
        }
        #endregion

        #region Create
        /// <summary>
        /// Vista create
        /// </summary>
        /// <returns>Vista create</returns>
        [CustomAuthorize(Roles = "IFUELCON-ADD")]
        public ActionResult Create()
        {
            this.LoadCatalogs();
            this.LoadCostCenter("");
            return View();
        }

        /// <summary>
        /// Vista para insertar en el contratos POST
        /// </summary>
        /// <param name="entity">Contiene el objecto del formulario que será editado</param>
        /// <returns>regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InternationalFuelContractVO entity)
        {
            entity.Provider = null;
            this.LoadCatalogs();
            this.LoadCostCenter("");
            if (entity == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                //Si aplica matricula como CC se mandara CC en null, dado que no es requerido
                if (entity.AircraftRegistCCFlag == true)
                    entity.CCNumber = null;

                #region omitir estas validacion por regla de negocio
                int i = 0;
                ModelState["Provider.ProviderName"].Errors.Clear();
                ModelState["Provider.ProviderNumber"].Errors.Clear();
                ModelState["Provider.ProviderShortName"].Errors.Clear();
                foreach (var item in entity.InternationalFuelContractConcepts)
                {
                    ModelState["InternationalFuelContractConcepts[" + i.ToString() + "].AirlineCode"].Errors.Clear();
                    ModelState["InternationalFuelContractConcepts[" + i.ToString() + "].StationCode"].Errors.Clear();
                    ModelState["InternationalFuelContractConcepts[" + i.ToString() + "].ServiceCode"].Errors.Clear();
                    ModelState["InternationalFuelContractConcepts[" + i.ToString() + "].ProviderNumberPrimary"].Errors.Clear();

                    ModelState["InternationalFuelContractConcepts[" + i.ToString() + "].FuelConceptID"].Errors.Clear();
                    ModelState["InternationalFuelContractConcepts[" + i.ToString() + "].FuelConceptTypeCode"].Errors.Clear();
                    ModelState["InternationalFuelContractConcepts[" + i.ToString() + "].ChargeFactorTypeID"].Errors.Clear();
                    ModelState["InternationalFuelContractConcepts[" + i.ToString() + "].ProviderNumber"].Errors.Clear();

                    i++;
                }
                #endregion

                if (ModelState.IsValid)
                {
                    internationalFuelContractBusiness.AddInternationalFuelContract(Mapper.Map<InternationalFuelContractVO, InternationalFuelContractDto>(entity));
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
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

            return this.View(entity);
        }
        #endregion

        #region Edit

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumberPrimary">The provider number.</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "IFUELCON-UPD")]
        public ActionResult Edit(string effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumberPrimary, bool flag = true)
        {
            InternationalFuelContractVO contractVo = new InternationalFuelContractVO();
            InternationalFuelContractDto contractDto = new InternationalFuelContractDto();
            InternationalFuelContractDto entity = new InternationalFuelContractDto();
            DateTime date;
            if (DateTime.TryParse(effectiveDate, out date))
            {
                entity = new InternationalFuelContractDto()
                                                      {
                                                          EffectiveDate = date,
                                                          AirlineCode = airlineCode,
                                                          StationCode = stationCode,
                                                          ServiceCode = serviceCode,
                                                          ProviderNumberPrimary = providerNumberPrimary
                                                      };
            }
            //Valores Iniciales 
            this.LoadCatalogs();                       
            

            this.LoadCostCenter(entity.AirlineCode);

            if (entity == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (entity == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


                contractDto = this.internationalFuelContractBusiness.FindInternationalFuelContractById(entity);
                contractVo = Mapper.Map<InternationalFuelContractDto, InternationalFuelContractVO>(contractDto);
                if(contractVo != null)
                contractVo.EffectiveDate.ToLocalTime();

                if (contractVo == null)
                    return HttpNotFound();

            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.View(contractVo);
        }


        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InternationalFuelContractVO entity)
        {
            this.LoadCatalogs();
            this.LoadCostCenter(entity.AirlineCode);
            this.ViewBag.noneBlock = entity.AircraftRegistCCFlag ? "none" : "block";

            if (entity == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                //internationalFuelContractBusiness.UpdateInternationalFuelContract(Mapper.Map<InternationalFuelContractVO, InternationalFuelContractDto>(entity));
                //this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;
                //return RedirectToAction("Index");
                if (ModelState.IsValid)
                {
                    internationalFuelContractBusiness.UpdateInternationalFuelContract(Mapper.Map<InternationalFuelContractVO, InternationalFuelContractDto>(entity));
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;

                    return RedirectToAction("Index");
                }

                InternationalFuelContractVO internationalFuelContractVo = Mapper.Map<InternationalFuelContractDto, InternationalFuelContractVO>(internationalFuelContractBusiness.FindInternationalFuelContractById(Mapper.Map<InternationalFuelContractDto>(entity)));
                return View(internationalFuelContractVo);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View(entity);
            }
        }
        #endregion

        #region Detail

        /// <summary>
        /// Details
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="entity">entity.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "IFUELCON-VIE")]
        public ActionResult Details(string effectiveDate, InternationalFuelContractVO entity)
        {
            InternationalFuelContractVO contractVo = new InternationalFuelContractVO();
            InternationalFuelContractDto contractDto = new InternationalFuelContractDto();

            try
            {
                DateTime date;
                if (DateTime.TryParse(effectiveDate, out date))
                {
                    entity.EffectiveDate = date;
                    contractDto = internationalFuelContractBusiness.FindInternationalFuelContractById(Mapper.Map<InternationalFuelContractVO, InternationalFuelContractDto>(entity));
                    contractVo = Mapper.Map<InternationalFuelContractDto, InternationalFuelContractVO>(contractDto);
                }

                if (contractVo == null)
                    return HttpNotFound();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "ContractService", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "ContractService", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            if (contractVo != null)
                contractVo.EffectiveDate.ToLocalTime();

            return View(contractVo);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Vista Delete
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumberPrimary">The provider number.</param>
        /// <returns>Regresa la vista Delete precargada con la información a eliminar</returns>
        [CustomAuthorize(Roles = "IFUELCON-DEL")]
        public ActionResult Delete(string effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumberPrimary)
        {
            InternationalFuelContractVO entity = new InternationalFuelContractVO();
            DateTime date;
            if (DateTime.TryParse(effectiveDate, out date))
            {
                entity = new InternationalFuelContractVO()
                {
                    EffectiveDate = date,
                    AirlineCode = airlineCode,
                    StationCode = stationCode,
                    ServiceCode = serviceCode,
                    ProviderNumberPrimary = providerNumberPrimary
                };
            }

            if (entity == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (string.IsNullOrEmpty(entity.ToString()))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                InternationalFuelContractDto internationalFuelContractDto = this.internationalFuelContractBusiness.FindInternationalFuelContractById(Mapper.Map<InternationalFuelContractVO, InternationalFuelContractDto>(entity));
                InternationalFuelContractVO internationalFuelContractVo = Mapper.Map<InternationalFuelContractDto, InternationalFuelContractVO>(internationalFuelContractDto);
                if (internationalFuelContractVo == null)
                    return HttpNotFound();

                return View(internationalFuelContractVo);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message.ToString();

                System.Diagnostics.Trace.TraceError(ex.ToString());
                return this.View();
            }
        }

        /// <summary>
        /// Vista Delete POST
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="entity">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "IFUELCON-DEL")]
        public ActionResult DeleteConfirmed(string effectiveDate, InternationalFuelContractVO entity)
        {
            if (entity == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DateTime date;
            if (DateTime.TryParse(effectiveDate, out date))
            {
                entity.EffectiveDate = date;
            }

            try
            {
                InternationalFuelContractDto internationalFuelContractDto = this.internationalFuelContractBusiness.FindInternationalFuelContractById(Mapper.Map<InternationalFuelContractVO, InternationalFuelContractDto>(entity));
                if (this.internationalFuelContractBusiness.DeleteInternationalFuelContract(internationalFuelContractDto))
                {
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;
                    return RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }


            if (entity != null)
                entity.EffectiveDate.ToLocalTime();

            return this.View(entity);
        }
        #endregion

        #region Inactive

        /// <summary>
        /// Inactivates the contract.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <param name="contract">The contract.</param>
        /// <returns>Json Result</returns>
        [CustomAuthorize(Roles = "IFUELCON-INC")]
        public ActionResult InactivateContract(string endDate, string contract)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            InternationalFuelContractDto serviceContract = serializer.Deserialize<InternationalFuelContractDto>(contract);
            DateTime date = new DateTime();
            if (!DateTime.TryParse(endDate, out date))
            {
                Logger.Error(string.Format(LogMessages.ErrorParsingDate, "ContractService", this.userInfo));
                Trace.TraceError(string.Format(LogMessages.ErrorParsingDate, "ContractService", this.userInfo));
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(0);
                return this.PartialView("_AlertView");
            }

            try
            {
                this.internationalFuelContractBusiness.InactiveInternationalFuelContract(serviceContract, date);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessInactive;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorInactiveContractService, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorInactiveContractService, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.PartialView("_AlertView");
            }

            return this.Json("OK");
        }
        #endregion

        #region Otros
        /// <summary>
        /// Gets the code of airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <returns>Json Cost Center View Model</returns>
        [CustomAuthorize]
        public ActionResult GetAirlineCode(string airlineCode)
        {
            try
            {
                if (string.IsNullOrEmpty(airlineCode))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    IList<GenericCatalogDto> costCenterList = new List<GenericCatalogDto>();
                    costCenterList = this.generic.GetCostCenterCatalog(airlineCode, Resources.Resource.SelectItem);
                    return this.Json(costCenterList, JsonRequestBehavior.AllowGet);
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

            return View();
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
        [CustomAuthorize]
        private void LoadCatalogs()
        {
            this.ViewBag.Airline = this.generic.GetAirlineCatalog();
            this.ViewBag.Airport = this.generic.GetAirportsCatalog();

            //Servicios de Combustible
            IList<GenericCatalogDto> serviceList = new List<GenericCatalogDto>();
            IList<GenericCatalogDto> serviceListFilter = new List<GenericCatalogDto>();
            serviceList = this.generic.GetServiceCatalog();
            foreach (GenericCatalogDto item in serviceList)
            {
                if ((item.Id.Contains("-EXT")))
                {
                    serviceListFilter.Add(item);
                }
            }
            this.ViewBag.Service = serviceListFilter;

            this.ViewBag.Provider = this.generic.GetProviderCatalog();
            this.ViewBag.AccountingAccount = this.generic.GetAccountingAccountsCatalog();
            this.ViewBag.LiabilityAccount = this.generic.GetLiabilityAccountsCatalog();
            this.ViewBag.Currency = this.generic.GetCurrencyCatalog();
            this.ViewBag.OperationType = this.generic.GetOperationTypeCatalog();

            this.ViewBag.FuelConcept = this.generic.GetFuelConceptCatalog();
            this.ViewBag.FuelConceptType = this.generic.GetFuelConceptTypeCatalog();
            this.ViewBag.ChargeFactorType = this.generic.GetChargeFactorTypeCatalog();

            #region Lista de Status
            IList<GenericCatalogDto> status = new List<GenericCatalogDto>();
            status.Add(new GenericCatalogDto() { Id = "A", Description = Resources.Resource.Active });
            status.Add(new GenericCatalogDto() { Id = "I", Description = Resources.Resource.Inactive });
            this.ViewBag.Status = status;
            #endregion
        }

        /// <summary>
        /// Loads the cost center.
        /// </summary>
        [CustomAuthorize]
        private void LoadCostCenter(string airlineCode)
        {
            this.ViewBag.CostCenter = this.generic.GetCostCenterCatalog(airlineCode, Resources.Resource.SelectItem);
        }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <returns>Json Result</returns>
        public JsonResult GetValidationMessage()
        {
            string message = Resource.RequiredField;
            return this.Json(message, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Validation date for inactivate a contract.
        /// </summary>
        /// <returns>Action Result</returns>
        public ActionResult ValidateDate(string endDate, string contract)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            AirportServiceContractDto serviceContract = serializer.Deserialize<AirportServiceContractDto>(contract);
            DateTime date = new DateTime();
            if (!DateTime.TryParse(endDate, out date))
            {
                Logger.Error(string.Format(LogMessages.ErrorParsingDate, "ContractService", this.userInfo));
                Trace.TraceError(string.Format(LogMessages.ErrorParsingDate, "ContractService", this.userInfo));
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(0);
                return this.PartialView("_AlertView");
            }

            int result;
            result = DateTime.Compare(date, serviceContract.EffectiveDate);
            if (result >= 0)
            {
                return this.Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                this.ViewBag.ErrorMessage = string.Format(
                    FrontMessage.GetExceptionErrorMessage(12),
                    "Effective Date {" + serviceContract.EffectiveDate.ToShortDateString() + "}");
                return this.PartialView("_AlertView");
            }
        }

        #endregion

        #region COntratos activos

        private IList<InternationalFuelContractVO> GetContracts()
        {
            LoadCatalogs();
            IList<InternationalFuelContractVO> internationalFuelContractVoList = new List<InternationalFuelContractVO>();
            try
            {
                internationalFuelContractVoList = Mapper.Map<IList<InternationalFuelContractDto>, IList<InternationalFuelContractVO>>(internationalFuelContractBusiness.GetActivesLastEffectiveDateInternationalFuelContracts());
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return internationalFuelContractVoList;
            }
            return internationalFuelContractVoList;
        }

        #endregion

        #region carga masiva de contratos
        /// <summary>
        /// Read Text File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "IFUELCON-UPLF")]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            IList<string> errors = new List<string>();
            IList<InternationalFuelContractVO> contractList = GetContracts();

            try
            {
                if (file == null || !file.ContentType.Equals("text/plain") || file.ContentLength <= 0)
                {
                    this.ViewBag.ErrorMessage = "The file must to be a TXT file and have some content.";
                    return this.View("Index", contractList);
                }

                var engine = new DelimitedFileEngine<InternationalFuelContractFile>();
                engine.Options.IgnoreFirstLines = 1;
                InternationalFuelContractFile[] records;

                using (StreamReader sr = new StreamReader(file.InputStream, Encoding.Default))
                {
                    records = engine.ReadStream(sr);
                }

                IList<InternationalFuelContractFile> contracts = new List<InternationalFuelContractFile>(records);
                IList<InternationalFuelContractDto> contractsDto = Mapper.Map<List<InternationalFuelContractDto>>(contracts);
                InternationalFuelContractConceptDto conceptoDto = new InternationalFuelContractConceptDto();

                for (int i = 0; i < contracts.Count(); i++)
                {
                    contractsDto[i].InternationalFuelContractConcepts = new List<InternationalFuelContractConceptDto>();
                    conceptoDto = (Mapper.Map<InternationalFuelContractConceptDto>(contracts[i]));
                    conceptoDto.Provider = Mapper.Map<ProviderDto>(contracts[i]);
                    conceptoDto.FuelConcept = Mapper.Map<FuelConceptDto>(contracts[i]);
                    conceptoDto.FuelConceptType = Mapper.Map<FuelConceptTypeDto>(contracts[i]);
                    conceptoDto.ChargeFactorType = Mapper.Map<ChargeFactorTypeDto>(contracts[i]);
                    contractsDto[i].InternationalFuelContractConcepts.Add(conceptoDto);
                }

                //Verifica duplicidad por contrato
                GroupConceptsCount(contractsDto);

                //Asigna conceptos a respectivos contratos
                List<InternationalFuelContractDto> contractsDtoFinal = GroupContracts(contractsDto);

                //Validaciones de Negocio
                errors = internationalFuelContractBusiness.ValidateFuelContracts(contractsDtoFinal);


                //Sino hubo errores y guardo información
                if (errors.Count == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFile;
                    contractList = GetContracts();
                    return this.View("Index", contractList);
                }
                else
                {
                    this.ViewBag.ListErrorMessage = errors;
                    return this.View("Index", contractList);
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
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }
            return this.View("Index", contractList);
        }

        private static void GroupConceptsCount(IList<InternationalFuelContractDto> contractsDto)
        {
            var contractDistinct = (from ssi in contractsDto
                                    group ssi by new { ssi.EffectiveDate, ssi.AirlineCode, ssi.StationCode, ssi.ServiceCode, ssi.ProviderNumberPrimary } into g
                                    select new { EffectiveDate = g.Key.EffectiveDate, AirlineCode = g.Key.AirlineCode, StationCode = g.Key.StationCode, ServiceCode = g.Key.ServiceCode, ProviderNumberPrimary = g.Key.ProviderNumberPrimary }).ToList();

            var contractDistincFullRecord = (from ssi in contractsDto
                                             group ssi by new { ssi.EffectiveDate, ssi.AirlineCode, ssi.StationCode, ssi.ServiceCode, ssi.ProviderNumberPrimary, ssi.AircraftRegistCCFlag, ssi.CCNumber, ssi.AccountingAccountNumber, ssi.LiabilityAccountNumber, ssi.OperationType.OperationName, ssi.CurrencyCode } into g
                                             select new { EffectiveDate = g.Key.EffectiveDate, AirlineCode = g.Key.AirlineCode, StationCode = g.Key.StationCode, ServiceCode = g.Key.ServiceCode, ProviderNumberPrimary = g.Key.ProviderNumberPrimary, AircraftRegistCCFlag = g.Key.AircraftRegistCCFlag, CCNumber = g.Key.CCNumber, AccountingAccountNumber = g.Key.AccountingAccountNumber, LiabilityAccountNumber = g.Key.LiabilityAccountNumber, OperationName = g.Key.OperationName, CurrencyCode = g.Key.CurrencyCode }).ToList();

            if (contractDistinct.Count != contractDistincFullRecord.Count)
                throw new BusinessException(string.Empty, 5);
        }

        private static List<InternationalFuelContractDto> GroupContracts(IList<InternationalFuelContractDto> contractsDto)
        {
            List<InternationalFuelContractDto> contractsDtoFinal = new List<InternationalFuelContractDto>();
            InternationalFuelContractDto contractNew = new InternationalFuelContractDto();
            IList<InternationalFuelContractDto> contractConceptListNew = new List<InternationalFuelContractDto>();
            var contractDistinct = (from ssi in contractsDto
                                    group ssi by new { ssi.EffectiveDate, ssi.AirlineCode, ssi.StationCode, ssi.ServiceCode, ssi.ProviderNumberPrimary } into g
                                    select new { EffectiveDate = g.Key.EffectiveDate, AirlineCode = g.Key.AirlineCode, StationCode = g.Key.StationCode, ServiceCode = g.Key.ServiceCode, ProviderNumberPrimary = g.Key.ProviderNumberPrimary }).ToList();

            foreach (var item in contractDistinct)
            {
                contractNew = contractsDto.FirstOrDefault(c => c.EffectiveDate == item.EffectiveDate
                                                            && c.AirlineCode == item.AirlineCode
                                                            && c.StationCode == item.StationCode
                                                            && c.ServiceCode == item.ServiceCode
                                                            && c.ProviderNumberPrimary == item.ProviderNumberPrimary);

                contractConceptListNew = contractsDto.Where(c => c.EffectiveDate == item.EffectiveDate
                                                  && c.AirlineCode == item.AirlineCode
                                                  && c.StationCode == item.StationCode
                                                  && c.ServiceCode == item.ServiceCode
                                                  && c.ProviderNumberPrimary == item.ProviderNumberPrimary).ToList();

                IList<InternationalFuelContractConceptDto> conceptListNew = new List<InternationalFuelContractConceptDto>();
                foreach (InternationalFuelContractDto item2 in contractConceptListNew)
                {
                    if (item2.InternationalFuelContractConcepts.Count > 0)
                        conceptListNew.Add(item2.InternationalFuelContractConcepts.FirstOrDefault());
                }


                contractNew.InternationalFuelContractConcepts = null;
                contractNew.InternationalFuelContractConcepts = conceptListNew;

                contractsDtoFinal.Add(contractNew);
            }

            return contractsDtoFinal;
        }

        #endregion

        #region carga masiva de tarifas

        /// <summary>
        /// Read Text File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "IFUELCON-UPLF")]
        public ActionResult UploadFileRates(HttpPostedFileBase file)
        {
            //IList<string> errors = new List<string>();
            // Gets the active contracts to return them to the view
            IList<InternationalFuelContractVO> contractList = GetContracts();

            // Validates that the file has content
            if (file == null || file.ContentLength <= 0)
            {
                this.ViewBag.ErrorMessage = Resource.EmptyFileError;
                return this.View("Index", contractList);
            }

            // Validates that the field is a text plain type
            if (!file.ContentType.Equals("text/plain"))
            {
                this.ViewBag.ErrorMessage = Resource.FormatFileError;
                return this.View("Index", contractList);
            }

            try
            {
                DelimitedFileEngine<InternationalFuelRatesFile> engine = new DelimitedFileEngine<InternationalFuelRatesFile>();
                engine.AfterReadRecord += engine_AfterReadRecord;
                engine.Options.IgnoreFirstLines = 1;
                engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                InternationalFuelRatesFile[] records;

                using (StreamReader sr = new StreamReader(file.InputStream, Encoding.Default))
                {
                    records = engine.ReadStream(sr);
                }

                IList<string> errorResult = new List<string>();
                errorResult = this.FindErrors(engine);

                // Validates errors in the file
                if (errorResult != null && errorResult.Count > 0)
                {
                    this.ViewBag.ListErrorMessage = errorResult;
                    return this.View("Index", contractList);
                }

                IList<InternationalFuelRatesFile> rates = new List<InternationalFuelRatesFile>(records);
                IList<InternationalFuelRateFileDto> ratesDto = Mapper.Map<List<InternationalFuelRateFileDto>>(rates);
                errorResult = massiveUploadBusiness.InternationalFuelRatesAddRange(ratesDto);
                
                // Validates business errors
                if (errorResult == null || errorResult.Count == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFile;
                    return this.View("Index", contractList);
                }
                else
                {
                    this.ViewBag.ListErrorMessage = errorResult;
                    return this.View("Index", contractList);
                }
            }
            catch (Exception exception)
            {
                //airportContractVo = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContractVO>>(this.contractService.GetEffectiveContracts());
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
                return this.View("Index", contractList);
            }
        }

        static void engine_AfterReadRecord(EngineBase engine, AfterReadEventArgs<InternationalFuelRatesFile> e)
        {
            e.Record.LineNumber = engine.LineNumber;
        } 

        private IList<string> FindErrors(DelimitedFileEngine<InternationalFuelRatesFile> file)
        {
            IList<string> fileErrors = new List<string>();

            // Finds errors in the file
            foreach (var error in file.ErrorManager.Errors)
            {
                fileErrors.Add(Resource.ErrorValidationFile + error.LineNumber);
            }

            return fileErrors;
        }

        #endregion

        #region Download files methods
        /// <summary>
        /// Downloads the requiered file.
        /// </summary>
        /// <returns>The template for the upload file.</returns>
        public FileContentResult DownloadContractTemplate()
        {
            try
            {
                byte[] data;
                string pathFile = Server.MapPath(Resource.InternationalFuelContractFilePath);
                data = FileHelper.GetPlainTextFile(pathFile);
                return File(data, MediaTypeNames.Text.Plain, Resource.InternationalFuelContractFileName);
            }
            catch (IOException)
            {
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
                return null;
            }
        }

        /// <summary>
        /// Downloads the requiered file.
        /// </summary>
        /// <returns>The template for the upload file.</returns>
        public FileContentResult DownloadRatesTemplate()
        {
            try
            {
                byte[] data;
                string pathFile = Server.MapPath(Resource.InternationalFuelRateFilePath);
                data = FileHelper.GetPlainTextFile(pathFile);
                return File(data, MediaTypeNames.Text.Plain, Resource.InternationalFuelRateFileName);
            }
            catch (IOException)
            {
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
                return null;
            }
        }
        #endregion
    }
}