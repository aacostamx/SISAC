//------------------------------------------------------------------------
// <copyright file="ModulePermissionController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Security.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Catalog;
    using VOI.SISAC.Web.Models.VO.Security;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Controller for Module-Permissions
    /// </summary>
    [CustomAuthorize]
    public class ModulePermissionController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ModulePermissionController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName;

        /// <summary>
        /// The module permission business
        /// </summary>
        private readonly IModulePermissionBusiness modulePermissionBusiness;

        /// <summary>
        /// The module business
        /// </summary>
        private readonly IModuleBusiness moduleBusiness;

        /// <summary>
        /// The generic catalog business
        /// </summary>
        private readonly IGenericCatalogBusiness genericCatalogBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulePermissionController" /> class.
        /// </summary>
        /// <param name="modulePermissionBusiness">The module permission business.</param>
        /// <param name="moduleBusiness">The module business.</param>
        /// <param name="genericCatalogBusiness">The generic catalog business.</param>
        public ModulePermissionController(
            IModulePermissionBusiness modulePermissionBusiness,
            IModuleBusiness moduleBusiness,
            IGenericCatalogBusiness genericCatalogBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.moduleName = "ModulePermission";
            this.modulePermissionBusiness = modulePermissionBusiness;
            this.moduleBusiness = moduleBusiness;
            this.genericCatalogBusiness = genericCatalogBusiness;
        }

        /// <summary>
        /// Action for insert permissions in module.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "MODPER-UPD")]
        public ActionResult Create(string moduleCode)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.SetModuleInformationInView(moduleCode);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return View();
        }

        /// <summary>
        /// Creates the specified collection.
        /// </summary>
        /// <param name="permissions">The permissions.</param>
        /// <returns>Action Result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "MODPER-UPD")]
        public ActionResult Create(AddPermissionsInModuleVO permissions)
        {
            if (permissions == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                List<PermissionDto> permissionDto = new List<PermissionDto>();
                if (permissions.Permission != null)
                {
                    foreach (PermissionVO item in permissions.Permission)
                    {
                        permissionDto.Add(new PermissionDto
                        {
                            PermissionCode = item.PermissionCode,
                            IsSelected = item.IsSelected
                        });
                    }
                }

                this.modulePermissionBusiness.UpdatesModuleAndPermissions(permissions.ModuleCode, permissionDto);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;
                this.SetModuleInformationInView(permissions.ModuleCode);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return View();
        }

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        /// <returns>
        /// Object with the permission.
        /// </returns>
        public JsonResult GetAllPermissions(string moduleCode)
        {
            if (string.IsNullOrWhiteSpace(moduleCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return null;
            }

            List<PermissionMarkedByModuleDto> permissions = new List<PermissionMarkedByModuleDto>();
            try
            {
                permissions = this.modulePermissionBusiness.GetAllPermissionByModule(moduleCode).ToList();
            }
            catch (BusinessException exception)
            {
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(permissions, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <returns>Object with the permission.</returns>
        public JsonResult GetWarningMessageConfiguration()
        {
            MessageVO message = new MessageVO();
            message.Confirm = Resource.Accept;
            message.Cancel = Resource.Cancel;
            message.Title = Resource.Warning;
            message.Text = Resource.WarningModulePermission;

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Sets the module information in view.
        /// </summary>
        /// <param name="moduleCode">The module code.</param>
        private void SetModuleInformationInView(string moduleCode)
        {
            ModuleDto module = this.moduleBusiness.FindModuleById(moduleCode);
            this.ViewBag.ModuleInfo = new ModuleDto();
            if (module != null)
            {
                this.ViewBag.ModuleInfo = module;
            }
        }
    }
}
