//------------------------------------------------------------------------
// <copyright file="AirportServiceContractController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Mime;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using AutoMapper;
    using FileHelpers;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Finance;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.Files;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Controller for Airport service contract
    /// </summary>
    [CustomAuthorize]
    public class AirportServiceContractController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AirportServiceContractController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.AirportServiceContractName;

        /// <summary>
        /// Interface for Airport service contract operations
        /// </summary>
        private readonly IAirportServiceContractBusiness contractService;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

        /// <summary>
        /// The massive upload business
        /// </summary>
        private readonly IMassiveUploadBusiness massiveUploadBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportServiceContractController" /> class.
        /// </summary>
        /// <param name="contractService">The contract service.</param>
        /// <param name="generic">The generic catalogs service.</param>
        /// <param name="massiveUploadBusiness">The massive upload business.</param>
        public AirportServiceContractController(
            IAirportServiceContractBusiness contractService,
            IGenericCatalogBusiness generic,
            IMassiveUploadBusiness massiveUploadBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.contractService = contractService;
            this.generic = generic;
            this.massiveUploadBusiness = massiveUploadBusiness;
        }

        /// <summary>
        /// Main view.
        /// </summary>
        /// <returns>View with the contracts records.</returns>
        [CustomAuthorize(Roles = "AIRPSERVCN-IDX")]
        public ActionResult Index()
        {
            Trace.TraceInformation("Cargando Index de AirportServiceContract");
            IList<AirportServiceContractDto> contracts = new List<AirportServiceContractDto>();
            IList<AirportServiceContractVO> contractsVo = new List<AirportServiceContractVO>();
            try
            {
                Trace.TraceInformation("Ida a la base de datos de AirportServiceContract");
                contracts = this.contractService.GetEffectiveContracts();
                Trace.TraceInformation("Regreso de la base de datos de AirportServiceContract");
                contractsVo = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContractVO>>(contracts);
                Trace.TraceInformation("Realizando Mapeos AirportServiceContract");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(contractsVo);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>View Create </returns>
        [CustomAuthorize(Roles = "AIRPSERVCN-ADD")]
        public ActionResult Create()
        {
            this.LoadCatalogs();
            return this.View();
        }

        /// <summary>
        /// Creates the specified airport service contract vo.
        /// </summary>
        /// <param name="airportServiceContractVO">The airport service contract vo.</param>
        /// <returns>View airportServiceContractVO</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPSERVCN-ADD")]
        public ActionResult Create(AirportServiceContractVO airportServiceContractVO)
        {
            AirportServiceContractDto airportServiceContractDto = new AirportServiceContractDto();
            if (airportServiceContractVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    airportServiceContractDto = Mapper.Map<AirportServiceContractVO, AirportServiceContractDto>(airportServiceContractVO);
                    this.contractService.AddAirportSerciveContract(airportServiceContractDto);
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

            return this.View(airportServiceContractVO);
        }

        /// <summary>
        /// Gets the airline code.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="selectItem">The select item.</param>
        /// <returns>Action Result Cost Center View Model</returns>
        [CustomAuthorize]
        public ActionResult GetAirlineCode(string airlineCode, string selectItem)
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
                    costCenterList = this.generic.GetCostCenterCatalog(airlineCode, selectItem);
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
        /// Edits the specified effective date.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>View contractVo</returns>
        [CustomAuthorize(Roles = "AIRPSERVCN-UPD")]
        public ActionResult Edit(
            string effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber)
        {
            AirportServiceContractVO contractVo = new AirportServiceContractVO();
            AirportServiceContractDto contractDto = new AirportServiceContractDto();
            try
            {
                this.LoadCatalogs();
                DateTime date;
                if (DateTime.TryParse(effectiveDate, out date))
                {
                    contractDto = this.contractService.FindById(date, airlineCode, stationCode, serviceCode, providerNumber, costCenterNumber);
                    contractVo = Mapper.Map<AirportServiceContractDto, AirportServiceContractVO>(contractDto);
                    contractVo.EffectiveDate.ToLocalTime();
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                this.LoadCatalogs();
            }

            return this.View(contractVo);
        }

        /// <summary>
        /// Edits the specified airport service contract vo.
        /// </summary>
        /// <param name="airportServiceContractVO">The airport service contract vo.</param>
        /// <returns>View airportServiceContractVO</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPSERVCN-UPD")]
        public ActionResult Edit(AirportServiceContractVO airportServiceContractVO)
        {
            if (airportServiceContractVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    AirportServiceContractDto airportServiceContractDto = Mapper.Map<AirportServiceContractVO, AirportServiceContractDto>(airportServiceContractVO);
                    this.contractService.UpdateAirportSerciveContract(airportServiceContractDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.LoadCatalogs();
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                this.LoadCatalogs();
            }

            return this.View(airportServiceContractVO);
        }

        /// <summary>
        /// Rates the specified effective date.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>View AirportServiceContractVO</returns>
        [CustomAuthorize(Roles = "AIRPSERVCN-RATE")]
        public ActionResult Rate(
            string effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber)
        {
            AirportServiceContractVO contractVo = new AirportServiceContractVO();
            AirportServiceContractDto contractDto = new AirportServiceContractDto();
            try
            {
                this.LoadCatalogs();
                DateTime date;
                if (DateTime.TryParse(effectiveDate, out date))
                {
                    contractDto = this.contractService.FindById(date, airlineCode, stationCode, serviceCode, providerNumber, costCenterNumber);
                    contractVo = Mapper.Map<AirportServiceContractDto, AirportServiceContractVO>(contractDto);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                this.LoadCatalogs();
            }

            return this.View(contractVo);
        }

        /// <summary>
        /// Rates the specified airport service contract vo.
        /// </summary>
        /// <param name="airportServiceContractVO">The airport service contract vo.</param>
        /// <returns>View airportServiceContractVO</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPSERVCN-RATE")]
        public ActionResult Rate(AirportServiceContractVO airportServiceContractVO)
        {
            AirportServiceContractDto airportServiceContractDto = new AirportServiceContractDto();
            if (airportServiceContractVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    airportServiceContractDto = Mapper.Map<AirportServiceContractVO, AirportServiceContractDto>(airportServiceContractVO);
                    this.contractService.AddAirportSerciveContract(airportServiceContractDto);
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

            return this.View("Rate", airportServiceContractVO);
        }

        /// <summary>
        /// Deletes the specified effective date.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>View contractVo</returns>
        [CustomAuthorize(Roles = "AIRPSERVCN-DEL")]
        public ActionResult Delete(
            string effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber)
        {
            AirportServiceContractVO contractVo = new AirportServiceContractVO();
            AirportServiceContractDto contractDto = new AirportServiceContractDto();
            try
            {
                DateTime date;
                if (DateTime.TryParse(effectiveDate, out date))
                {
                    contractDto = this.contractService.FindById(date, airlineCode, stationCode, serviceCode, providerNumber, costCenterNumber);
                    contractVo = Mapper.Map<AirportServiceContractDto, AirportServiceContractVO>(contractDto);
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

            return this.View("Delete", contractVo);
        }

        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airportServiceContractVO">The airport service contract vo.</param>
        /// <returns>View Index</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRPSERVCN-DEL")]
        public ActionResult DeleteConfirmed(string effectiveDate, AirportServiceContractVO airportServiceContractVO)
        {
            if (airportServiceContractVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DateTime date;
            if (DateTime.TryParse(effectiveDate, out date))
            {
                airportServiceContractVO.EffectiveDate = date;
            }

            try
            {
                AirportServiceContractDto airportServiceContractDto = this.contractService.FindById(
                    airportServiceContractVO.EffectiveDate,
                    airportServiceContractVO.AirlineCode,
                    airportServiceContractVO.StationCode,
                    airportServiceContractVO.ServiceCode,
                    airportServiceContractVO.ProviderNumber,
                    airportServiceContractVO.CostCenterNumber);

                AirportServiceContractVO airportServiceVO = Mapper.Map<AirportServiceContractDto, AirportServiceContractVO>(airportServiceContractDto);
                if (this.contractService.DeleteAirportSerciveContract(airportServiceContractDto))
                {
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            if (airportServiceContractVO != null)
                airportServiceContractVO.EffectiveDate.ToLocalTime();

            return this.View(airportServiceContractVO);
        }

        /// <summary>
        /// Details of the specified contract.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>View contractVo</returns>
        [CustomAuthorize(Roles = "AIRPSERVCN-VIE")]
        public ActionResult Details(
            string effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber)
        {
            AirportServiceContractVO contractVo = new AirportServiceContractVO();
            AirportServiceContractDto contractDto = new AirportServiceContractDto();
            try
            {
                DateTime date;
                if (DateTime.TryParse(effectiveDate, out date))
                {
                    contractDto = this.contractService.FindById(date, airlineCode, stationCode, serviceCode, providerNumber, costCenterNumber);
                    contractVo = Mapper.Map<AirportServiceContractDto, AirportServiceContractVO>(contractDto);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "ContractService", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "ContractService", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View("Details", contractVo);
        }

        /// <summary>
        /// Inactivates the contract.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        /// <param name="contract">The contract.</param>
        /// <returns>Json Result</returns>
        [CustomAuthorize(Roles = "AIRPSERVCN-INC")]
        public ActionResult InactivateContract(string endDate, string contract)
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

            try
            {
                this.contractService.InactivateAirportSerciveContract(
                serviceContract.EffectiveDate,
                serviceContract.AirlineCode,
                serviceContract.StationCode,
                serviceContract.ServiceCode,
                serviceContract.ProviderNumber,
                serviceContract.CostCenterNumber,
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
        /// Downloads the requiered file.
        /// </summary>
        /// <returns>The template for the upload file.</returns>
        public FileContentResult Download()
        {
            try
            {
                byte[] data;
                string pathFile = Server.MapPath(Resource.InternationalServiceContractFilePath);
                data = FileHelper.GetPlainTextFile(pathFile);
                return File(data, MediaTypeNames.Text.Plain, Resource.InternationalServiceContractFileName);
            }
            catch (IOException)
            {
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
                return null;
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
            AirportServiceContractDto serviceContract = serializer.Deserialize<AirportServiceContractDto>(contract);
            DateTime date = new DateTime();

            // Parse the endDate to a valid date
            if (!DateTime.TryParse(endDate, out date))
            {
                Logger.Error(string.Format(LogMessages.ErrorParsingDate, "ContractService", this.userInfo));
                Trace.TraceError(string.Format(LogMessages.ErrorParsingDate, "ContractService", this.userInfo));
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(0);
                return this.PartialView("_AlertView");
            }

            int result;

            // Validates the date to be greater than the effective date
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

        /// <summary>
        /// Shows the retrieve contracts.
        /// </summary>
        /// <returns>The view with modal configurations.</returns>
        public ActionResult ShowRetrieveContracts()
        {
            this.LoadCatalogsContract();
            return this.PartialView("Partial/RetrieveContractModal");
        }

        /// <summary>
        /// Retrieves the contracts.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// Action Result.
        /// </returns>
        [CustomAuthorize(Roles = "AIRPSERVCN-RETRICONTR")]
        public ActionResult RetrieveContracs(ContractParametersVO contract)
        {
            try
            {
                IList<AirportServiceContractDto> contracts;
                IList<AirportServiceContractVO> contractsVo;
                AirportServiceContractDto parameter = Mapper.Map<ContractParametersVO, AirportServiceContractDto>(contract);
                contracts = this.contractService.GetContractsByParameters(parameter);
                contractsVo = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContractVO>>(contracts);
                this.ViewBag.AdvanceSearchSubtitle = "Advance search";
                return this.View("Index", contractsVo);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View("Index", new List<AirportServiceContractVO>());
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Action Result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "AIRPSERVCN-UPLF")]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            // Gets the active contracts to return them to the view
            IList<AirportServiceContractVO> airportContractVo = new List<AirportServiceContractVO>();

            // Validates that the file has content
            if (file == null || file.ContentLength <= 0)
            {
                airportContractVo = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContractVO>>(this.contractService.GetEffectiveContracts());
                this.ViewBag.ErrorMessage = Resource.EmptyFileError;
                return this.View("Index", airportContractVo);
            }

            // Validates that the field is a text plain type
            if (!file.ContentType.Equals("text/plain"))
            {
                airportContractVo = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContractVO>>(this.contractService.GetEffectiveContracts());
                this.ViewBag.ErrorMessage = Resource.FormatFileError;
                return this.View("Index", airportContractVo);
            }

            try
            {
                DelimitedFileEngine<AirportServiceContractFile> engine = new DelimitedFileEngine<AirportServiceContractFile>();
                engine.Options.IgnoreFirstLines = 1;
                engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                AirportServiceContractFile[] records;

                using (StreamReader sr = new StreamReader(file.InputStream, Encoding.Default))
                {
                    records = engine.ReadStream(sr);
                }

                IList<string> errorResult = new List<string>();
                errorResult = this.FindErrors(engine);

                // Validates errors in the file
                if (errorResult != null && errorResult.Count > 0)
                {
                    airportContractVo = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContractVO>>(this.contractService.GetEffectiveContracts());
                    this.ViewBag.ListErrorMessage = errorResult;
                    return this.View("Index", airportContractVo);
                }

                List<AirportServiceContractFile> listContractFile = new List<AirportServiceContractFile>(records);
                List<AirportServiceContractDto> listContract = Mapper.Map<List<AirportServiceContractFile>, List<AirportServiceContractDto>>(listContractFile);
                errorResult = this.massiveUploadBusiness.AirportServiceContractAddRange(listContract);

                // Validates business errors
                if (errorResult == null || errorResult.Count == 0)
                {
                    airportContractVo = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContractVO>>(this.contractService.GetEffectiveContracts());
                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFile;
                    return this.View("Index", airportContractVo);
                }
                else
                {
                    airportContractVo = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContractVO>>(this.contractService.GetEffectiveContracts());
                    this.ViewBag.ListErrorMessage = errorResult;
                    return this.View("Index", airportContractVo);
                }
            }
            catch (Exception exception)
            {
                airportContractVo = Mapper.Map<IList<AirportServiceContractDto>, IList<AirportServiceContractVO>>(this.contractService.GetEffectiveContracts());
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
                return this.View("Index", airportContractVo);
            }
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
                this.ViewBag.Service = this.generic.GetServiceCatalog();
                this.ViewBag.Provider = this.generic.GetProviderCatalog();
                this.ViewBag.CostCenter = this.generic.GetCostCenterCatalog();
                this.ViewBag.AccountingAccount = this.generic.GetAccountingAccountsCatalog();
                this.ViewBag.LiabilityAccount = this.generic.GetLiabilityAccountsCatalog();
                this.ViewBag.Tax = this.generic.GetTaxesCatalog();
                this.ViewBag.Currency = this.generic.GetCurrencyCatalog();
                this.ViewBag.AirplaneWeightMeasureType = this.generic.GetAirplaneWeightMeasureTypeCatalog();
                this.ViewBag.AirplaneWeightType = this.generic.GetAirplaneWeightTypeCatalog();
                this.ViewBag.OperationType = this.generic.GetOperationTypeCatalog();
                this.ViewBag.ServiceCalculationType = this.generic.GetServiceCalculationTypeCatalog();
                this.ViewBag.ServiceType = this.generic.GetServiceTypeCatalog();
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
        private void LoadCatalogsContract()
        {
            try
            {
                this.ViewBag.Airline = this.generic.GetAirlineCatalog();
                this.ViewBag.Airport = this.generic.GetAirportsCatalog();
                this.ViewBag.Service = this.generic.GetServiceCatalog();
                this.ViewBag.Provider = this.generic.GetProviderCatalog();
                this.ViewBag.CostCenter = this.generic.GetCostCenterCatalog();
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
        /// Finds the errors.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        /// Find errors in the given file.
        /// </returns>
        private IList<string> FindErrors(DelimitedFileEngine<AirportServiceContractFile> file)
        {
            IList<string> fileErrors = new List<string>();

            // Finds errors in the file
            foreach (var error in file.ErrorManager.Errors)
            {
                fileErrors.Add(Resource.ErrorValidationFile + error.LineNumber);
            }

            return fileErrors;
        }
    }
}