//------------------------------------------------------------------------
// <copyright file="CrewController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using AutoMapper;
    using Business.Dto.Paged;
    using FileHelpers;
    using Models.VO.Paged;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mime;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Catalog;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.Files;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Models.VO.Catalog;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Crew Controller
    /// </summary>
    [CustomAuthorize]
    public class CrewController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AirlineController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.CrewTitle;

        /// <summary>
        /// The crew business
        /// </summary>
        private readonly ICrewBusiness crewBusiness;

        /// <summary>
        /// The country business
        /// </summary>
        private readonly ICountryBusiness countryBusiness;

        /// <summary>
        /// The crew type business
        /// </summary>
        private readonly ICrewTypeBusiness crewTypeBusiness;

        /// <summary>
        /// The gender business
        /// </summary>
        private readonly IGenderBusiness genderBusiness;

        /// <summary>
        /// The status on board business
        /// </summary>
        public readonly IStatusOnBoardBusiness statusOnBoardBusiness;

        /// <summary>
        /// The massive upload crew business
        /// </summary>
        private readonly IMassiveUploadCrewBusiness massiveUploadCrewBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrewController" /> class.
        /// </summary>
        /// <param name="crewBusiness">The crew business.</param>
        /// <param name="countryBusiness">The country business.</param>
        /// <param name="crewTypeBusiness">The crew type business.</param>
        /// <param name="genderBusiness">The gender business.</param>
        /// <param name="statusOnBoardBusiness">The status on board business.</param>
        /// <param name="massiveUploadCrewBusiness">The massive upload crew business.</param>
        public CrewController(
            ICrewBusiness crewBusiness,
            ICountryBusiness countryBusiness,
            ICrewTypeBusiness crewTypeBusiness,
            IGenderBusiness genderBusiness,
            IStatusOnBoardBusiness statusOnBoardBusiness,
            IMassiveUploadCrewBusiness massiveUploadCrewBusiness)
        {
            this.userInfo = string.Format(
               LogMessages.UserInfo,
               System.Environment.UserDomainName,
               System.Environment.UserName,
               System.Environment.MachineName);
            this.crewBusiness = crewBusiness;
            this.countryBusiness = countryBusiness;
            this.crewTypeBusiness = crewTypeBusiness;
            this.genderBusiness = genderBusiness;
            this.statusOnBoardBusiness = statusOnBoardBusiness;
            this.massiveUploadCrewBusiness = massiveUploadCrewBusiness;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>View of Crew Active</returns>
        [CustomAuthorize(Roles = "CREW-IDX")]
        public ActionResult Index()
        {
            var crewVO = new List<CrewVO>();

            try
            {

            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(crewVO);
        }

        /// <summary>
        /// Gets the crew paged.
        /// </summary>
        /// <param name="crewPaged">The crew paged.</param>
        /// <returns></returns>
        public ContentResult GetCrewPaged(PagedVO crewPaged)
        {
            var total = 0;
            var json = string.Empty;
            var result = new ContentResult();
            var crewDto = new List<CrewDto>();
            var crewVO = new List<CrewVO>();
            var model = new { total = 0, rows = new List<CrewVO>() };

            try
            {
                crewDto = this.crewBusiness.GetCrewPaged(Mapper.Map<PagedDto>(crewPaged)).ToList();
                crewVO = Mapper.Map<List<CrewVO>>(crewDto);
                total = this.crewBusiness.TotalCrew();

                model = new
                {
                    total = total,
                    rows = crewVO,
                };

                json = JsonConvert.SerializeObject(model,Formatting.Indented, new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                result = this.Content(json);
            }
            catch (BusinessException ex)
            {
                Trace.TraceError(ex.Message, ex);
                Logger.Error(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>View Crew</returns>
        [CustomAuthorize(Roles = "CREW-ADD")]
        public ActionResult Create()
        {
            CrewModelVO crewViewModel = new CrewModelVO();
            crewViewModel.Countries = new List<CountryVO>();
            crewViewModel.CrewTypes = new List<CrewTypeVO>();
            crewViewModel.Genders = new List<GenderVO>();

            try
            {
                if (ModelState.IsValid)
                {
                    crewViewModel = this.LoadCatalogs();
                    if ((crewViewModel.Countries == null) || (crewViewModel.CrewTypes == null) || (crewViewModel.Genders == null))
                    {
                        this.HttpNotFound();
                    }
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(crewViewModel);
        }

        /// <summary>
        /// Creates the specified crew dto.
        /// </summary>
        /// <param name="crewViewModel">The crew dto.</param>
        /// <returns>View Crew</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "CREW-ADD")]
        public ActionResult Create(CrewModelVO crewViewModel)
        {
            CrewDto crewDto = new CrewDto();
            List<string> errorResult = new List<string>();
            if (crewViewModel == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    crewDto = Mapper.Map<CrewVO, CrewDto>(crewViewModel.CrewVO);

                    // Valida si Numero de Empleado, NickName o NickName Sabre ya existen en la base de datos
                    errorResult = this.crewBusiness.ValidateFields(crewDto);
                    if (errorResult != null && errorResult.Count > 0)
                    {
                        crewViewModel = this.LoadCatalogs(crewViewModel.CrewVO);
                        this.ViewBag.ListErrorMessage = errorResult;

                        return this.View("Create", crewViewModel);
                    }

                    this.crewBusiness.AddCrew(crewDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
                else
                {
                    crewViewModel = this.LoadCatalogs();
                    if ((crewViewModel.Countries == null) || (crewViewModel.CrewTypes == null) || (crewViewModel.Genders == null))
                    {
                        this.HttpNotFound();
                    }
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(crewViewModel);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View Crew</returns>
        [CustomAuthorize(Roles = "CREW-UPD")]
        public ActionResult Edit(long id)
        {
            CrewModelVO crewViewModel = new CrewModelVO();
            CrewDto crewDto = new CrewDto();

            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    crewDto = this.crewBusiness.FindCrewById(id);

                    crewViewModel = this.LoadCatalogs();
                    if ((crewViewModel.Countries == null) || (crewViewModel.CrewTypes == null) || (crewViewModel.Genders == null))
                    {
                        this.HttpNotFound();
                    }

                    crewViewModel.CrewVO = Mapper.Map<CrewDto, CrewVO>(crewDto);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }

            return this.View(crewViewModel);
        }

        /// <summary>
        /// Edits the specified crew view model.
        /// </summary>
        /// <param name="crewViewModel">The crew view model.</param>
        /// <returns>View Model Crew</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "CREW-UPD")]
        public ActionResult Edit(CrewModelVO crewViewModel)
        {
            CrewDto crewDto = new CrewDto();

            if (crewViewModel == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    crewDto = Mapper.Map<CrewVO, CrewDto>(crewViewModel.CrewVO);
                    this.crewBusiness.UpdateCrew(crewDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
                else
                {
                    crewViewModel = this.LoadCatalogs();
                    if ((crewViewModel.Countries == null) || (crewViewModel.CrewTypes == null) || (crewViewModel.Genders == null))
                    {
                        this.HttpNotFound();
                    }

                    crewViewModel.CrewVO = Mapper.Map<CrewDto, CrewVO>(crewDto);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(crewViewModel);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View Crew</returns>
        [CustomAuthorize(Roles = "CREW-DEL")]
        public ActionResult Delete(long id)
        {
            CrewDto crewDto = new CrewDto();
            CrewVO crewVo = new CrewVO();
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                crewDto = this.crewBusiness.FindCrewById(id);
                if (crewDto == null)
                {
                    return this.HttpNotFound();
                }
                else
                {
                    crewVo = Mapper.Map<CrewDto, CrewVO>(crewDto);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(crewVo);
        }

        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View Crew</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "CREW-DEL")]
        public ActionResult DeleteConfirmed(long id)
        {
            CrewDto crewDto = new CrewDto();
            CrewVO crewVo = new CrewVO();
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                crewDto = this.crewBusiness.FindCrewById(id);

                if (crewDto == null)
                {
                    return this.HttpNotFound();
                }

                this.crewBusiness.DeleteCrew(crewDto);
                crewVo = Mapper.Map<CrewDto, CrewVO>(crewDto);
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(crewVo);
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>View Index or upload File sucesfull</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "CREW-UPLF")]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            // Se obtienes los contratos de servicios activos para regresarlos a la vista nuevamente
            IList<CrewVO> crewVo = new List<CrewVO>();
            List<CrewDto> lstContract = new List<CrewDto>();

            // Validates that the file has content
            if (file == null || file.ContentLength <= 0)
            {
                crewVo = Mapper.Map<IList<CrewDto>, IList<CrewVO>>(this.crewBusiness.GetActivesCrew());
                this.ViewBag.ErrorMessage = Resource.EmptyFileError;
                return this.View("Index", crewVo);
            }

            // Validates that the field is a text plain type
            if (!file.ContentType.Equals("text/plain"))
            {
                crewVo = Mapper.Map<IList<CrewDto>, IList<CrewVO>>(this.crewBusiness.GetActivesCrew());
                this.ViewBag.ErrorMessage = Resource.FormatFileError;
                return this.View("Index", crewVo);
            }

            string validationError = Resource.FileErrors;
            try
            {
                DelimitedFileEngine<CrewFile> engine = new DelimitedFileEngine<CrewFile>();
                engine.Options.IgnoreFirstLines = 1;
                engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                CrewFile[] records;

                using (StreamReader sr = new StreamReader(file.InputStream, Encoding.Default))
                {
                    records = engine.ReadStream(sr);
                }

                IList<string> errorResult = new List<string>();
                errorResult = this.FindErrors(engine);

                // Validates errors in the file
                if (errorResult != null && errorResult.Count > 0)
                {
                    crewVo = Mapper.Map<IList<CrewDto>, IList<CrewVO>>(this.crewBusiness.GetActivesCrew());
                    this.ViewBag.ListErrorMessage = errorResult;
                    return this.View("Index", crewVo);
                }

                // Convierte el CrewFile[] a List<CrewFile>
                List<CrewFile> crewFiles = new List<CrewFile>(records);

                // Realiza el mapero de  List<CrewFile> a List<CrewDto> para mandarlo al Business
                lstContract = Mapper.Map<List<CrewFile>, List<CrewDto>>(crewFiles);

                // Realiza todas las validaciones de negocio en el archivo
                errorResult = this.massiveUploadCrewBusiness.CrewAddRange(lstContract);

                // Cuando la lista venga vacía, indica que los datos contenidos en el archivo se encontraron en los catálogos
                // y el arhivo debe cargarse exitosamente.
                if (errorResult == null || errorResult.Count == 0)
                {
                    // Se obtienes los contratos de servicios activos para regresarlos a la vista nuevamente
                    crewVo = Mapper.Map<IList<CrewDto>, IList<CrewVO>>(this.crewBusiness.GetActivesCrew());
                    this.ViewBag.ErrorMessage = Resource.SuccessfulLoadFile;

                    return this.View("Index", crewVo);
                }
                else
                {
                    crewVo = Mapper.Map<IList<CrewDto>, IList<CrewVO>>(this.crewBusiness.GetActivesCrew());
                    this.ViewBag.ListErrorMessage = errorResult;

                    return this.View("Index", crewVo);
                }
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.InnerException == null ? exception.Message : exception.InnerException.Message;

                return this.View("Index", crewVo);
            }
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
                string pathFile = Server.MapPath(Resource.CrewTemplateFilePath);
                data = FileHelper.GetPlainTextFile(pathFile);
                return File(data, MediaTypeNames.Text.Plain, Resource.CrewTemplateFileName);
            }
            catch (IOException)
            {
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
                return null;
            }
        }

        /// <summary>
        /// Gets the pilots.
        /// </summary>
        /// <returns>Object with the stewardess information.</returns>
        [HttpGet]
        public JsonResult GetStewardess()
        {
            IList<CrewDto> stewardess = new List<CrewDto>();
            try
            {
                stewardess = this.crewBusiness.GetActiveStewardess();
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return this.Json(stewardess, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the pilots.
        /// </summary>
        /// <returns>Objet with the pilots information.</returns>
        [HttpGet]
        public JsonResult GetPilots()
        {
            IList<CrewDto> pilots = new List<CrewDto>();
            try
            {
                pilots = this.crewBusiness.GetActivePilots();
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = exception.ToString();
            }

            return this.Json(pilots, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Finds the errors.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>List of string</returns>
        private IList<string> FindErrors(DelimitedFileEngine<CrewFile> file)
        {
            IList<string> fileErrors = new List<string>();

            // Finds errors in the file
            foreach (var error in file.ErrorManager.Errors)
            {
                fileErrors.Add(Resource.ErrorValidationFile + error.LineNumber);
            }

            return fileErrors;
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
        /// <param name="crewVO">The crew vo.</param>
        /// <returns> return CrewModelVO</returns>
        private CrewModelVO LoadCatalogs(CrewVO crewVO)
        {
            IList<CountryDto> countriesDto = new List<CountryDto>();
            IList<CrewTypeDto> crewTypesDto = new List<CrewTypeDto>();
            IList<GenderDto> gendersDto = new List<GenderDto>();
            IList<StatusOnBoardDto> statusOnBoardDto = new List<StatusOnBoardDto>();
            CrewModelVO crewViewModelResult = new CrewModelVO();

            crewTypesDto = this.crewTypeBusiness.GetActivesCrewType();
            countriesDto = this.countryBusiness.GetActivesCountry();
            gendersDto = this.genderBusiness.GetAllGender();
            statusOnBoardDto = this.statusOnBoardBusiness.GetAllStatusOnBoard();

            crewViewModelResult.Countries = Mapper.Map<IList<CountryDto>, IList<CountryVO>>(countriesDto);
            crewViewModelResult.CrewTypes = Mapper.Map<IList<CrewTypeDto>, IList<CrewTypeVO>>(crewTypesDto);
            crewViewModelResult.Genders = Mapper.Map<IList<GenderDto>, IList<GenderVO>>(gendersDto);
            crewViewModelResult.StatusOnBoards = Mapper.Map<IList<StatusOnBoardDto>, IList<StatusOnBoardVO>>(statusOnBoardDto);
            crewViewModelResult.CrewVO = crewVO;

            return crewViewModelResult;
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
        /// <returns>return CrewModelVO</returns>
        private CrewModelVO LoadCatalogs()
        {
            IList<CountryDto> countriesDto = new List<CountryDto>();
            IList<CrewTypeDto> crewTypesDto = new List<CrewTypeDto>();
            IList<GenderDto> gendersDto = new List<GenderDto>();
            IList<StatusOnBoardDto> statusOnBoardDto = new List<StatusOnBoardDto>();
            CrewModelVO crewViewModelResult = new CrewModelVO();

            crewTypesDto = this.crewTypeBusiness.GetActivesCrewType();
            countriesDto = this.countryBusiness.GetActivesCountry();
            gendersDto = this.genderBusiness.GetAllGender();
            statusOnBoardDto = this.statusOnBoardBusiness.GetAllStatusOnBoard();

            crewViewModelResult.Countries = Mapper.Map<IList<CountryDto>, IList<CountryVO>>(countriesDto);
            crewViewModelResult.CrewTypes = Mapper.Map<IList<CrewTypeDto>, IList<CrewTypeVO>>(crewTypesDto);
            crewViewModelResult.Genders = Mapper.Map<IList<GenderDto>, IList<GenderVO>>(gendersDto);
            crewViewModelResult.StatusOnBoards = Mapper.Map<IList<StatusOnBoardDto>, IList<StatusOnBoardVO>>(statusOnBoardDto);

            return crewViewModelResult;
        }
    }
}