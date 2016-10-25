//------------------------------------------------------------------------
// <copyright file="ProfileRoleController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Security.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Helpers;
    using Models.VO.Security;
    using Resources;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Security;
    using VOI.SISAC.Web.Models.VO.Catalog;
    using Web.Controllers;
    using System.Net;

    /// <summary>
    /// Profile role controller
    /// </summary>
    [CustomAuthorize]
    public class ProfileRoleController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ProfileRoleController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = "ProfilesRoles";

        /// <summary>
        /// The profile name
        /// </summary>
        private readonly string profileName;

        /// <summary>
        /// The profile business
        /// </summary>
        private readonly IProfileBusiness profileBusiness;

        /// <summary>
        /// The profile role business
        /// </summary>
        private readonly IProfileRoleBusiness profileRoleBusiness;

        /// <summary>
        /// The role business
        /// </summary>
        private readonly IRoleBusiness roleBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRoleController"/> class.
        /// </summary>
        /// <param name="profileRoleBusiness">The profile role business.</param>
        /// <param name="roleBusiness">The role business.</param>
        /// <param name="profileBusiness">The profile business.</param>
        public ProfileRoleController(
            IProfileRoleBusiness profileRoleBusiness,
            IRoleBusiness roleBusiness,
            IProfileBusiness profileBusiness)
        {
            this.userInfo = string.Format(
            LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.profileRoleBusiness = profileRoleBusiness;
            this.roleBusiness = roleBusiness;
            this.profileBusiness = profileBusiness;
        }

        /// <summary>
        /// Main view.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "PRFROL-UPD")]
        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.profileName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.profileName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                this.SetProfileInformationInView(id);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
            
            return this.View();
        }

        /// <summary>
        /// Edits the specified add profile roles vo.
        /// </summary>
        /// <param name="addProfileRolesVO">The add profile roles vo.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "PRFROL-UPD")]
        public ActionResult Edit(AddProfileRolesVO addProfileRolesVO)
        {
            if (addProfileRolesVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.profileName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.profileName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                List<RoleDto> rolesDto = new List<RoleDto>();
                if (addProfileRolesVO.Role != null)
                {
                    foreach (RoleVO item in addProfileRolesVO.Role)
                    {
                        rolesDto.Add(new RoleDto
                        {
                            RoleCode = item.RoleCode,
                            IsChecked = item.IsSelected
                        });
                    }
                }

                this.profileRoleBusiness.UpdatesProfileAndRoles(addProfileRolesVO.ProfileCode, rolesDto);
                this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                this.SetProfileInformationInView(addProfileRolesVO.ProfileCode);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }
            
            return this.View("Index");
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <param name="profileCode">The profile code.</param>
        /// <returns>Action result.</returns>
        public JsonResult GetAllRoles(string profileCode)
        {
            if (string.IsNullOrWhiteSpace(profileCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.profileName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.profileName));
                return null;
            }

            List<RolesMarkedByProfileDto> profiles = new List<RolesMarkedByProfileDto>();
            try
            {
                profiles = this.profileRoleBusiness.GetAllRolesByProfile(profileCode).ToList();
            }
            catch (BusinessException exception)
            {
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(profiles, JsonRequestBehavior.AllowGet);
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
        /// <param name="profileCode">The profile code.</param>
        private void SetProfileInformationInView(string profileCode)
        {
            ProfileDto profile = this.profileBusiness.FindProfileById(profileCode);
            this.ViewBag.Profile = new ProfileDto();
            if (profile != null)
            {
                this.ViewBag.Profile = profile;
            }
        }
    }
}