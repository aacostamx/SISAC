//------------------------------------------------------------------------
// <copyright file="NationalFuelContractController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using AutoMapper;
    using Newtonsoft.Json;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Finance;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// National Fuel Contract Controller
    /// </summary>
    [CustomAuthorize]
    public class NationalFuelContractController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NationalFuelContractController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = "National Contract";

        /// <summary>
        /// Interface for the logic
        /// </summary>        
        private readonly INationalFuelContractBusiness nationalFuelContractBusiness;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelContractController"/> class.
        /// </summary>
        /// <param name="nationalFuelContractBusiness">The national fuel contract business.</param>
        /// <param name="generic">The generic.</param>
        public NationalFuelContractController(
            INationalFuelContractBusiness nationalFuelContractBusiness,
            IGenericCatalogBusiness generic)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.nationalFuelContractBusiness = nationalFuelContractBusiness;
            this.generic = generic;
        }
        #endregion

        /// <summary>
        /// Main view.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NATFUELCON-IDX")]
        public ActionResult Index()
        {
            this.LoadCatalogsSearch();
            return this.View();
        }

        /// <summary>
        /// Search view.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "NATFUELCON-SEARCH")]
        public ActionResult Search(NationalFuelContractSearchVO parameters)
        {
            return this.View(parameters);
        }

        /// <summary>
        /// Get contract data
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>
        /// Content result
        /// </returns>
        public ContentResult GetContractData(NationalFuelContractSearchVO search)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            IList<NationalFuelContractDto> contracts = new List<NationalFuelContractDto>();
            IList<NationalFuelContractVO> contractsVO = new List<NationalFuelContractVO>();

            try
            {
                totalRows = this.nationalFuelContractBusiness.CountEffectivesContracts();
                contracts = this.nationalFuelContractBusiness.GetNationalFuelContractsPaginated(search.PageSize, search.PageNumber);
                contractsVO = Mapper.Map<IList<NationalFuelContractVO>>(contracts);
                jsonConvert.total = totalRows;
                jsonConvert.rows = contractsVO;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                result = this.Content(json);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return result;
        }

        /// <summary>
        /// Get contract data
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>
        /// Content result
        /// </returns>
        public ContentResult SearchContractData(NationalFuelContractSearchVO search)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            IList<NationalFuelContractDto> contracts = new List<NationalFuelContractDto>();
            IList<NationalFuelContractVO> contractsVO = new List<NationalFuelContractVO>();

            try
            {
                NationalFuelContractDto parameters = Mapper.Map<NationalFuelContractDto>(search);
                totalRows = this.nationalFuelContractBusiness.CountSearchContracts(parameters);
                contracts = this.nationalFuelContractBusiness.SearchNationalFuelContractsPaginated(parameters, search.PageSize, search.PageNumber);
                contractsVO = Mapper.Map<IList<NationalFuelContractVO>>(contracts);
                jsonConvert.total = totalRows;
                jsonConvert.rows = contractsVO;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                result = this.Content(json);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return result;
        }

        /// <summary>
        /// View create.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NATFUELCON-ADD")]
        public ActionResult Create()
        {
            this.LoadCatalogs();
            NationalFuelContractVO contract = new NationalFuelContractVO();
            contract.NationalFuelContractConcept = new List<NationalFuelContractConceptVO>();
            return this.View();
        }

        /// <summary>
        /// View create.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NATFUELCON-ADD")]
        public ActionResult Create(NationalFuelContractVO contract)
        {
            if (contract == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelContractDto nationalContractDto = new NationalFuelContractDto();
            try
            {
                if (this.ModelState.IsValid)
                {
                    nationalContractDto = Mapper.Map<NationalFuelContractVO, NationalFuelContractDto>(contract);
                    this.nationalFuelContractBusiness.AddNationalFuelContract(nationalContractDto);
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
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);

                this.ViewBag.ErrorMessage = message;
                this.LoadCatalogs();
            }

            return this.View(contract);
        }

        /// <summary>
        /// View edit.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumberPrimary">The provider number primary.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "NATFUELCON-UPD")]
        public ActionResult Edit(
            string effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumberPrimary)
        {
            DateTime date;
            NationalFuelContractVO contractVo = new NationalFuelContractVO();
            NationalFuelContractDto result = new NationalFuelContractDto();
            NationalFuelContractDto contract = new NationalFuelContractDto();

            if (DateTime.TryParse(effectiveDate, out date))
            {
                contract = new NationalFuelContractDto()
                {
                    EffectiveDate = date,
                    AirlineCode = airlineCode,
                    StationCode = stationCode,
                    ServiceCode = serviceCode,
                    ProviderNumberPrimary = providerNumberPrimary
                };
            }
            else
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                result = this.nationalFuelContractBusiness.FindNationalFuelContractById(contract);
                contractVo = Mapper.Map<NationalFuelContractDto, NationalFuelContractVO>(result);
                if (contractVo != null)
                {
                    contractVo.EffectiveDate.ToLocalTime();
                }

                if (contractVo == null)
                {
                    return this.HttpNotFound();
                }

                this.LoadCatalogs();
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
        /// View edit.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NATFUELCON-UPD")]
        public ActionResult Edit(NationalFuelContractVO contract)
        {
            if (contract == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelContractDto nationalContractDto = new NationalFuelContractDto();
            try
            {
                if (this.ModelState.IsValid)
                {
                    nationalContractDto = Mapper.Map<NationalFuelContractVO, NationalFuelContractDto>(contract);
                    this.nationalFuelContractBusiness.UpdateNationalFuelContract(nationalContractDto);
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;
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
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);

                this.ViewBag.ErrorMessage = message;
                this.LoadCatalogs();
            }

            return this.View(contract);
        }

        /// <summary>
        /// View details.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumberPrimary">The provider number primary.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "NATFUELCON-DEL")]
        public ActionResult Delete(
            string effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumberPrimary)
        {
            DateTime date;
            NationalFuelContractVO contractVo = new NationalFuelContractVO();
            NationalFuelContractDto result = new NationalFuelContractDto();
            NationalFuelContractDto contract = new NationalFuelContractDto();

            if (DateTime.TryParse(effectiveDate, out date))
            {
                contract = new NationalFuelContractDto()
                {
                    EffectiveDate = date,
                    AirlineCode = airlineCode,
                    StationCode = stationCode,
                    ServiceCode = serviceCode,
                    ProviderNumberPrimary = providerNumberPrimary
                };
            }
            else
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                result = this.nationalFuelContractBusiness.FindNationalFuelContractById(contract);
                contractVo = Mapper.Map<NationalFuelContractDto, NationalFuelContractVO>(result);
                if (contractVo != null)
                {
                    contractVo.EffectiveDate.ToLocalTime();
                }

                if (contractVo == null)
                {
                    return this.HttpNotFound();
                }
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
        /// View delete.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NATFUELCON-DEL")]
        public ActionResult Delete(NationalFuelContractVO contract)
        {
            if (contract == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelContractDto nationalContractDto = new NationalFuelContractDto();
            try
            {
                nationalContractDto = Mapper.Map<NationalFuelContractVO, NationalFuelContractDto>(contract);
                this.nationalFuelContractBusiness.DeleteNationalFuelContract(nationalContractDto);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                this.ViewBag.ErrorMessage = message;
            }

            return this.View(contract);
        }

        /// <summary>
        /// View details.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumberPrimary">The provider number primary.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "NATFUELCON-VIE")]
        public ActionResult Detail(
            string effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumberPrimary)
        {
            DateTime date;
            NationalFuelContractVO contractVo = new NationalFuelContractVO();
            NationalFuelContractDto result = new NationalFuelContractDto();
            NationalFuelContractDto contract = new NationalFuelContractDto();

            if (DateTime.TryParse(effectiveDate, out date))
            {
                contract = new NationalFuelContractDto()
                {
                    EffectiveDate = date,
                    AirlineCode = airlineCode,
                    StationCode = stationCode,
                    ServiceCode = serviceCode,
                    ProviderNumberPrimary = providerNumberPrimary
                };
            }
            else
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                result = this.nationalFuelContractBusiness.FindNationalFuelContractById(contract);
                contractVo = Mapper.Map<NationalFuelContractDto, NationalFuelContractVO>(result);
                if (contractVo != null)
                {
                    contractVo.EffectiveDate.ToLocalTime();
                }

                if (contractVo == null)
                {
                    return this.HttpNotFound();
                }
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
        /// Inactivates the contract.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <param name="contract">The contract.</param>
        /// <returns>Json Result</returns>
        [CustomAuthorize(Roles = "NATFUELCON-INC")]
        public ActionResult InactivateContract(string endDate, string contract)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            NationalFuelContractDto nationalContract = serializer.Deserialize<NationalFuelContractDto>(contract);
            DateTime date = new DateTime();
            if (!DateTime.TryParse(endDate, out date))
            {
                Logger.Error(string.Format(LogMessages.ErrorParsingDate, this.catalogName, this.userInfo));
                Trace.TraceError(string.Format(LogMessages.ErrorParsingDate, this.catalogName, this.userInfo));
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(0);
                return this.PartialView("_AlertView");
            }

            try
            {
                this.nationalFuelContractBusiness.InactivateAirportSerciveContract(
                nationalContract.EffectiveDate,
                nationalContract.AirlineCode,
                nationalContract.StationCode,
                nationalContract.ServiceCode,
                nationalContract.ProviderNumberPrimary,
                date);
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

        /// <summary>
        /// Gets the airline code.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <returns>
        /// Action Result Cost Center View Model
        /// </returns>
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
                    costCenterList = this.generic.GetCostCenterCatalog(airlineCode, null);
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

            return this.View();
        }

        /// <summary>
        /// Validation date for inactivate a contract.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// Action Result
        /// </returns>
        public ActionResult ValidateDate(string endDate, string contract)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            NationalFuelContractDto nationalContract = serializer.Deserialize<NationalFuelContractDto>(contract);
            DateTime date = new DateTime();

            // Parse the endDate to a valid date
            if (!DateTime.TryParse(endDate, out date))
            {
                Logger.Error(string.Format(LogMessages.ErrorParsingDate, this.catalogName, this.userInfo));
                Trace.TraceError(string.Format(LogMessages.ErrorParsingDate, this.catalogName, this.userInfo));
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(0);
                return this.PartialView("_AlertView");
            }

            int result;

            // Validates the date to be greater than the effective date
            result = DateTime.Compare(date, nationalContract.EffectiveDate);
            if (result >= 0)
            {
                return this.Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                this.ViewBag.ErrorMessage = string.Format(
                    FrontMessage.GetExceptionErrorMessage(12),
                    "Effective Date {" + nationalContract.EffectiveDate.ToShortDateString() + "}");
                return this.PartialView("_AlertView");
            }
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
        /// Loads the catalogs.
        /// </summary>
        private void LoadCatalogs()
        {
            try
            {
                this.ViewBag.Airline = this.generic.GetAirlineCatalog();
                this.ViewBag.Airport = this.generic.GetAirportsCatalog();
                this.ViewBag.Service = this.generic.GetServiceCatalog().Where(c => c.Id.EndsWith("CM")).ToList();
                this.ViewBag.Provider = this.generic.GetProviderCatalog();
                this.ViewBag.CostCenter = this.generic.GetCostCenterCatalog();
                this.ViewBag.AccountingAccount = this.generic.GetAccountingAccountsCatalog();
                this.ViewBag.LiabilityAccount = this.generic.GetLiabilityAccountsCatalog();
                this.ViewBag.Tax = this.generic.GetTaxesCatalog();
                this.ViewBag.Currency = this.generic.GetCurrencyCatalog();
                this.ViewBag.OperationType = this.generic.GetOperationTypeCatalog();

                this.ViewBag.FuelConcept = this.generic.GetFuelConceptCatalog();
                this.ViewBag.FuelConceptType = this.generic.GetFuelConceptTypeCatalog();
                this.ViewBag.ChargeFactorType = this.generic.GetChargeFactorTypeCatalog().Where(c => c.Id.Equals("3")).ToList();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
        private void LoadCatalogsSearch()
        {
            try
            {
                this.ViewBag.Airline = this.generic.GetAirlineCatalog();
                this.ViewBag.Airport = this.generic.GetAirportsCatalog();
                this.ViewBag.Service = this.generic.GetServiceCatalog().Where(c => c.Id.EndsWith("CM")).ToList();
                this.ViewBag.Provider = this.generic.GetProviderCatalog();
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
        }
    }
}