//------------------------------------------------------------------------
// <copyright file="RoleModulePermissionController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Security.Controllers
{
    using AutoMapper;
    using Business.Dto.Security;
    using Models.VO.Security;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Dynamic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Resources;
    using Web.Controllers;

    /// <summary>
    /// RoleModulePermissionController controller class
    /// </summary>
    [CustomAuthorize]
    public class RoleModulePermissionController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(RoleModulePermissionController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.RoleModulePermission;

        /// <summary>
        /// The airline business
        /// </summary>
        private readonly IRoleBusiness roleBusiness;

        /// <summary>
        /// The airline business
        /// </summary>
        private readonly IModulePermissionBusiness moduleBusiness;

        /// <summary>
        /// RoleModulePermissionController constructor
        /// </summary>
        /// <param name="roleBusiness"></param>
        /// <param name="moduleBusiness"></param>
        public RoleModulePermissionController(IRoleBusiness roleBusiness, IModulePermissionBusiness moduleBusiness)
        {
            this.userInfo = string.Format(
               LogMessages.UserInfo,
               System.Environment.UserDomainName,
               System.Environment.UserName,
               System.Environment.MachineName);
            this.roleBusiness = roleBusiness;
            this.moduleBusiness = moduleBusiness;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "ROLMODPER-UPD")]
        public ActionResult Create(RoleVO roleVO)
        {
            RoleDto roleDto = new RoleDto();
            RoleVO modulesVO = new RoleVO();

            try
            {
                if (roleVO != null)
                {
                    roleDto = this.roleBusiness.FindRoleById(roleVO.RoleCode);
                    modulesVO = Mapper.Map<RoleVO>(roleDto);
                    modulesVO.ModulePermissions = null;
                    modulesVO.ModulePermissions = Mapper.Map<IList<ModulePermissionVO>>
                        (this.moduleBusiness.GetAllModules());
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }

            return this.View(modulesVO);
        }

        /// <summary>
        /// Add formCollection
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "ROLMODPER-UPD")]
        public ActionResult EditRoleModules(RoleVO roles)
        {
            RoleDto roleDto = new RoleDto();
            try
            {
                if (roles != null)
                {
                    roleDto.RoleCode = roles.RoleCode;
                    roleDto.ModulePermissions = Mapper.Map<IList<ModulePermissionDto>>(roles.ModulePermissions);
                    this.roleBusiness.EditRoleModule(roleDto);
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;
                    return Json("Success");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                return Json("An Error Has occoured: " + ex.ToString());
            }
            return this.View();
        }

        /// <summary>
        /// Get Modules Permissions
        /// </summary>
        /// <param name="roleVO"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetModulesPermissions(RoleVO roleVO)
        {
            RoleDto roleDto = new RoleDto();
            bool edit = true;
            int userProfile = 0;
            dynamic jsonConvert = new ExpandoObject();
            string json = string.Empty;
            string value = string.Empty;
            IList<string> list = new List<string>();
            try
            {
                roleDto = this.roleBusiness.FindRoleByIdNoTracking(roleVO.RoleCode);
                userProfile = this.roleBusiness.CountUserProfileRoles(roleVO.RoleCode);
                edit = userProfile > 0 ? false : true;

                foreach (var item in roleDto.ModulePermissions)
                {
                    value = item.ModuleCode + "/" + item.PermissionCode;
                    list.Add(value);
                }

                jsonConvert.Edit = edit;
                jsonConvert.DataValue = list;

                json = JsonConvert.SerializeObject(jsonConvert, Formatting.None, new JsonSerializerSettings());

            }
            catch (Exception ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "ROLEMODULEPERMISSION", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "ROLEMODULEPERMISSION", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = ex.ToString();
            }

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}