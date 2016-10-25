//------------------------------------------------------------------------
// <copyright file="ModuleController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Security.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Security;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Class Module Controller 
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    [CustomAuthorize]
    public class ModuleController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ModuleController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.ModuleTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = Resource.ModuleCode;

        /// <summary>
        /// The airline business
        /// </summary>
        private readonly IModuleBusiness moduleBusiness;

        /// <summary>
        /// The menu business
        /// </summary>
        private readonly IMenuBusiness menuBusiness;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleController" /> class.
        /// </summary>
        /// <param name="moduleBusiness">The module business.</param>
        /// <param name="menuBusiness">The menu business.</param>
        public ModuleController(IModuleBusiness moduleBusiness, IMenuBusiness menuBusiness)
        {
            this.userInfo = string.Format(
               LogMessages.UserInfo,
               System.Environment.UserDomainName,
               System.Environment.UserName,
               System.Environment.MachineName);
            this.moduleBusiness = moduleBusiness;
            this.menuBusiness = menuBusiness;
        }
        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "MODULE-IDX")]
        public ActionResult Index()
        {
            Trace.TraceInformation("Iniciando Index Module");
            IList<ModuleDto> moduleDto = new List<ModuleDto>();
            IList<ModuleVO> moduleVO = new List<ModuleVO>();
            try
            {
                Trace.TraceInformation("Antes de Consulta en BD Module");
                moduleDto = this.moduleBusiness.GetModule();
                moduleVO = Mapper.Map<IList<ModuleDto>, IList<ModuleVO>>(moduleDto);
                Trace.TraceInformation("Despues de Consulta en BD Module");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(moduleVO);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "MODULE-ADD")]
        public ActionResult Create()
        {
            this.LoadCatalogs();
            return this.View();
        }

        /// <summary>
        /// Creates the specified module vo.
        /// </summary>
        /// <param name="moduleVO">The module vo.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "MODULE-ADD")]
        public ActionResult Create(ModuleVO moduleVO)
        {
            ModuleDto moduleDto = new ModuleDto();

            if (moduleVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.LoadCatalogs();
                if (ModelState.IsValid)
                {
                    moduleDto = Mapper.Map<ModuleVO, ModuleDto>(moduleVO);
                    this.moduleBusiness.AddModule(moduleDto);
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
                    message = string.Format(message, this.primaryKey);
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(moduleVO);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "MODULE-UPD")]
        public ActionResult Edit(string id)
        {
            ModuleVO moduleVo = new ModuleVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.LoadCatalogs();
                ModuleDto moduleDto = this.moduleBusiness.FindModuleById(id);
                moduleVo = Mapper.Map<ModuleDto, ModuleVO>(moduleDto);

                if (moduleDto == null)
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

            return this.View(moduleVo);
        }

        /// <summary>
        /// Edits the specified module vo.
        /// </summary>
        /// <param name="moduleVO">The module vo.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "MODULE-UPD")]
        public ActionResult Edit(ModuleVO moduleVO)
        {
            ModuleDto moduleDto = new ModuleDto();
            if (moduleVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.LoadCatalogs();
                if (ModelState.IsValid)
                {
                    moduleDto = Mapper.Map<ModuleVO, ModuleDto>(moduleVO);
                    this.moduleBusiness.UpdateModule(moduleDto);
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;

                    return this.RedirectToAction("Index");
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

            return this.View(moduleVO);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "MODULE-DEL")]
        public ActionResult Delete(string id)
        {
            ModuleVO moduleVo = new ModuleVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                ModuleDto moduleDto = this.moduleBusiness.FindModuleById(id);
                moduleVo = Mapper.Map<ModuleDto, ModuleVO>(moduleDto);
                if (moduleDto == null)
                {
                    return this.HttpNotFound();
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

            return this.View(moduleVo);
        }

        /// <summary>
        /// Deletes the confirm.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "MODULE-DEL")]
        public ActionResult DeleteConfirm(string id)
        {
            ModuleVO moduleVO = new ModuleVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                ModuleDto moduleDto = this.moduleBusiness.FindModuleById(id);
                moduleVO = Mapper.Map<ModuleDto, ModuleVO>(moduleDto);
                this.moduleBusiness.DeleteModule(moduleDto);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;

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

            return this.View(moduleVO);
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
        private void LoadCatalogs()
        {
            this.ViewBag.Menu = this.menuBusiness.GetAllMenu();
        }
    }
}