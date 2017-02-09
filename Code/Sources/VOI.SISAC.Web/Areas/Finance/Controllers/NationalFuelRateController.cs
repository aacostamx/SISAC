//------------------------------------------------------------------------
// <copyright file="NationalFuelRateController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.IO;
    using System.Net;
    using System.Net.Mime;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using FileHelpers;
    using Newtonsoft.Json;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Finance;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.Files;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// National Fuel Rate Controller
    /// </summary>
    [CustomAuthorize]
    public class NationalFuelRateController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NationalFuelContractController));

        /// <summary>
        /// The national rate business
        /// </summary>
        private readonly INationalFuelRateBusiness nationalRateBusiness;

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = "National Contract";

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelRateController"/> class.
        /// </summary>
        /// <param name="nationalRateBusiness">The national rate business.</param>
        public NationalFuelRateController(INationalFuelRateBusiness nationalRateBusiness)
        {
            this.nationalRateBusiness = nationalRateBusiness;
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
        }

        /// <summary>
        /// Main view.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NATFUELRAT-IDX")]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Main view.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "NATFUELRAT-SEARCH")]
        public ActionResult Search(NationalFuelRateVO rate)
        {
            return this.View();
        }

        /// <summary>
        /// Gets the rate by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Content result.
        /// </returns>
        public ContentResult GetRateSearch(NationalFuelRateVO parameters)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            IList<NationalFuelRateDto> rates = new List<NationalFuelRateDto>();
            IList<NationalFuelRateVO> ratesVO = new List<NationalFuelRateVO>();

            try
            {
                DateTime endDate;
                DateTime startDate;
                NationalFuelRateDto ratesDto = new NationalFuelRateDto();
                if (DateTime.TryParse(parameters.StartDate, out startDate))
                {
                    if (DateTime.TryParse(parameters.EndDate, out endDate))
                    {
                        ratesDto.EffectiveStartDate = startDate;
                        ratesDto.EffectiveEndDate = endDate;
                    }
                }

                totalRows = this.nationalRateBusiness.CountNationalFuelRateByParameters(ratesDto);
                rates = this.nationalRateBusiness.SearchNationalFuelRateByParameters(ratesDto, parameters.PageSize, parameters.PageNumber);
                ratesVO = Mapper.Map<IList<NationalFuelRateVO>>(rates);
                jsonConvert.total = totalRows;
                jsonConvert.rows = ratesVO;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                result = this.Content(json);
            }
            catch (Exception exception)
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
        /// Gets the rate data.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Content result.
        /// </returns>
        public ContentResult GetRateData(NationalFuelRateVO parameters)
        {
            ContentResult result = new ContentResult();
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            int totalRows = 0;
            IList<NationalFuelRateDto> rates = new List<NationalFuelRateDto>();
            IList<NationalFuelRateVO> ratesVO = new List<NationalFuelRateVO>();

            try
            {
                totalRows = this.nationalRateBusiness.CountAll();
                rates = this.nationalRateBusiness.GetNationalFuelRatesPagination(parameters.PageSize, parameters.PageNumber);
                ratesVO = Mapper.Map<IList<NationalFuelRateVO>>(rates);
                jsonConvert.total = totalRows;
                jsonConvert.rows = ratesVO;
                json = JsonConvert.SerializeObject(
                    jsonConvert,
                    Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                result = this.Content(json);
            }
            catch (Exception exception)
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
        /// Uploads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NATFUELRAT-UPLF")]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            // IList<NationalFuelContractVO> contractList = GetContracts();
            IList<string> errors = new List<string>();

            // Validates that the file has content
            if (file == null || !file.ContentType.Equals("text/plain") || file.ContentLength <= 0)
            {
                this.ViewBag.ErrorMessage = Resource.ErrorFormatFile;
                return this.View("Index");
            }

            // Validates that the field is a text plain type
            if (!file.ContentType.Equals("text/plain"))
            {
                this.ViewBag.ErrorMessage = Resource.FormatFileError;
                return this.View("Index");
            }

            try
            {
                DelimitedFileEngine<NationalFuelRateFile> engine = new DelimitedFileEngine<NationalFuelRateFile>();
                engine.Options.IgnoreFirstLines = 1;
                engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                NationalFuelRateFile[] records;

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
                    return this.View("Index");
                }

                List<NationalFuelRateFile> fileRecords = new List<NationalFuelRateFile>(records);
                List<NationalFuelRateDto> ratesFromFile = Mapper.Map<List<NationalFuelRateDto>>(fileRecords);

                // Begins to validate the rates and if is success save the contracts in DB
                errors = this.nationalRateBusiness.UploadNationalFuelRates(ratesFromFile);

                if (errors.Count == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFile;
                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.TempData["ListErrorMessage"] = errors;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.FindRecord, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.FindRecord, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = exception.ToString();
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Edits the specified rate.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NATFUELRAT-UPD")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelRateVO rate = new NationalFuelRateVO();
            try
            {
                NationalFuelRateDto rateDto = this.nationalRateBusiness.FindNationalFuelRateById(id.Value);
                rate = Mapper.Map<NationalFuelRateVO>(rateDto);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(rate);
        }

        /// <summary>
        /// Edits the specified rate.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NATFUELRAT-UPD")]
        public ActionResult Edit(NationalFuelRateVO rate)
        {
            if (rate == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                NationalFuelRateDto rateDto = Mapper.Map<NationalFuelRateDto>(rate);
                this.nationalRateBusiness.UpdateNationalFuelRate(rateDto);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(rate);
        }

        /// <summary>
        /// Delete the specified rate.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [CustomAuthorize(Roles = "NATFUELRAT-DEL")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelRateVO rate = new NationalFuelRateVO();
            try
            {
                NationalFuelRateDto rateDto = new NationalFuelRateDto();
                rateDto = this.nationalRateBusiness.FindNationalFuelRateById(id.Value);
                rate = Mapper.Map<NationalFuelRateVO>(rateDto);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(rate);
        }

        /// <summary>
        /// Delete the rate.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NATFUELRAT-DEL")]
        public ActionResult Delete(NationalFuelRateVO rate)
        {
            if (rate == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelContractDto nationalContractDto = new NationalFuelContractDto();
            try
            {
                this.nationalRateBusiness.DeleteNationalFuelRate(rate.NationalFuelRateId);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                this.ViewBag.ErrorMessage = message;
            }

            return this.View(rate);
        }

        /// <summary>
        /// Delete the rate.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NATFUELRAT-DEL")]
        public ActionResult DeleteRange(NationalFuelRateVO rate)
        {
            if (rate == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (rate.EffectiveStartDate > rate.EffectiveEndDate)
            {
                this.ViewBag.ErrorMessage = Resource.StartDateGreaterThanEndError;
                return this.View("Index");
            }

            try
            {
                int i = this.nationalRateBusiness.DeleteNationalFuelRate(rate.EffectiveStartDate, rate.EffectiveEndDate);
                if (i > 0)
                {
                    this.TempData["OperationSuccess"] = string.Format(Resource.DeleteRangeSuccess, i);
                    return this.RedirectToAction("Index");
                }
                else if (i == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.DeleteRangeZeroDeleted;
                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.TempData["ErrorMessage"] = FrontMessage.GetExceptionErrorMessage();
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                this.TempData["ErrorMessage"] = message;
            }

            return this.RedirectToAction("Index");
        }

        /// <summary>
        /// Downloads the required file.
        /// </summary>
        /// <returns>
        /// The template for the upload file.
        /// </returns>
        public FileContentResult Download()
        {
            try
            {
                byte[] data;
                string pathFile = Server.MapPath(Resource.NationalFuelRateFilePath);
                data = FileHelper.GetPlainTextFile(pathFile);
                return this.File(data, MediaTypeNames.Text.Plain, Resource.NationalFuelRateFileName);
            }
            catch (IOException)
            {
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
                return null;
            }
        }

        /// <summary>
        /// Finds the errors.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        /// Find errors in the given file.
        /// </returns>
        private IList<string> FindErrors(DelimitedFileEngine<NationalFuelRateFile> file)
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