//------------------------------------------------------------------------
// <copyright file="ProfileController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Security.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Helpers;
    using Models.VO.Security;
    using Resources;
    using VOI.SISAC.Business.Dto.Security;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Security;
    using Web.Controllers;

    /// <summary>
    /// Controller for Profile Section
    /// </summary>
    [CustomAuthorize]
    public class ProfileController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ProfileController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.Profiles;

        /// <summary>
        /// The profile business
        /// </summary>
        private readonly IProfileBusiness profileBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileController" /> class.
        /// </summary>
        /// <param name="profileBusiness">The profile business.</param>
        public ProfileController(IProfileBusiness profileBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.profileBusiness = profileBusiness;
        }

        /// <summary>
        /// Main view
        /// </summary>
        /// <returns>Action Result.</returns>
        [CustomAuthorize(Roles = "PROFILE-IDX")]
        public ActionResult Index()
        {
            IList<ProfileVO> profileVO = new List<ProfileVO>();
            IList<ProfileDto> profileDto = new List<ProfileDto>();
            try
            {
                profileDto = this.profileBusiness.GetAllProfiles();
                profileVO = Mapper.Map<IList<ProfileDto>, IList<ProfileVO>>(profileDto);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(profileVO);
        }

        /// <summary>
        /// Create action for Profile
        /// </summary>
        /// <returns>Action Result.</returns>
        [CustomAuthorize(Roles = "PROFILE-ADD")]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Action for create a record in the Profile by POST
        /// </summary>
        /// <param name="profileVO">The profile vo.</param>
        /// <returns>
        /// Action Result.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "PROFILE-ADD")]
        public ActionResult Create(ProfileVO profileVO)
        {
            if (profileVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    ProfileDto profileDto = new ProfileDto();
                    profileDto = Mapper.Map<ProfileVO, ProfileDto>(profileVO);
                    this.profileBusiness.AddProfile(profileDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, "SERVICE", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "SERVICE", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 10)
                {
                    message = string.Format(message, VOI.SISAC.Web.Resources.Resource.ServiceCode);
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(profileVO);
        }

        /// <summary>
        /// Edit View
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Action Result.
        /// </returns>
        [CustomAuthorize(Roles = "PROFILE-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProfileDto profileDto = new ProfileDto();
            ProfileVO profileVO = new ProfileVO();
            try
            {
                profileDto = this.profileBusiness.FindProfileById(id);
                profileVO = Mapper.Map<ProfileDto, ProfileVO>(profileDto);
                if (profileVO == null)
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
            }

            return this.View(profileVO);
        }

        /// <summary>
        /// Edits the specified profile view object.
        /// </summary>
        /// <param name="profileVO">The profile view object.</param>
        /// <returns>Action Result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "PROFILE-UPD")]
        public ActionResult Edit(ProfileVO profileVO)
        {
            if (profileVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    ProfileDto profileDto = new ProfileDto();
                    profileDto = Mapper.Map<ProfileVO, ProfileDto>(profileVO);
                    this.profileBusiness.UpdateProfile(profileDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
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

            return this.View(profileVO);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "PROFILE-DEL")]
        public ActionResult Delete(string id)
        {
            ProfileVO profileVo = new ProfileVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                ProfileDto profileDto = this.profileBusiness.FindProfileById(id);
                profileVo = Mapper.Map<ProfileDto, ProfileVO>(profileDto);
                if (profileDto == null)
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
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(profileVo);
        }

        /// <summary>
        /// Deletes the confirm.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "PROFILE-DEL")]
        public ActionResult DeleteConfirm(string id)
        {
            ProfileVO profileVO = new ProfileVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                ProfileDto profileDto = this.profileBusiness.FindProfileById(id);
                profileVO = Mapper.Map<ProfileDto, ProfileVO>(profileDto);
                this.profileBusiness.DeleteProfile(id);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(profileVO);
        }
    }
}