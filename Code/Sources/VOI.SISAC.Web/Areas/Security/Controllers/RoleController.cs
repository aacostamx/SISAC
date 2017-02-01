//------------------------------------------------------------------------
// <copyright file="RoleController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
	/// RoleController
    /// </summary>
    [CustomAuthorize]
	public class RoleController : BaseController
	{
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(RoleController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.RoleTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = Resource.RoleCode;

        /// <summary>
        /// The airline business
        /// </summary>
        private readonly IRoleBusiness roleBusiness;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleController"/> class.
        /// </summary>
        /// <param name="roleBusiness">The Role business.</param>
        public RoleController(IRoleBusiness roleBusiness)
        {
            this.userInfo = string.Format(
               LogMessages.UserInfo,
               System.Environment.UserDomainName,
               System.Environment.UserName,
               System.Environment.MachineName);
            this.roleBusiness = roleBusiness;
        }
        #endregion

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "ROLE-IDX")]
        public ActionResult Index()
        {
            Trace.TraceInformation("Iniciando Index Role");
            IList<RoleDto> roleDto = new List<RoleDto>();
            IList<RoleVO> roleVO = new List<RoleVO>();
            try
            {
                Trace.TraceInformation("Antes de Consulta en BD Role");
                roleDto = this.roleBusiness.GetRoles();
                roleVO = Mapper.Map<IList<RoleDto>, IList<RoleVO>>(roleDto);
                Trace.TraceInformation("Despues de Consulta en BD Role");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(roleVO);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "ROLE-ADD")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates the specified Role vo.
        /// </summary>
        /// <param name="roleVO">The Role vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ROLE-ADD")]
        public ActionResult Create(RoleVO roleVO)
        {
            RoleDto roleDto = new RoleDto();

            if (roleVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    roleDto = Mapper.Map<RoleVO, RoleDto>(roleVO);
                    this.roleBusiness.AddRole(roleDto);
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

            return this.View(roleVO);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="roleCode">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "ROLE-UPD")]
        public ActionResult Edit(string roleCode)
        {
            RoleVO roleVo = new RoleVO();
            if (string.IsNullOrEmpty(roleCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                RoleDto roleDto = this.roleBusiness.FindRoleById(roleCode);
                roleVo = Mapper.Map<RoleDto, RoleVO>(roleDto);

                if (roleDto == null)
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

            return this.View(roleVo);
        }

        /// <summary>
        /// Edits the specified Role vo.
        /// </summary>
        /// <param name="roleVO">The Role vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ROLE-UPD")]
        public ActionResult Edit(RoleVO roleVO)
        {
            RoleDto roleDto = new RoleDto();
            if (roleVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    roleDto = Mapper.Map<RoleVO, RoleDto>(roleVO);
                    this.roleBusiness.UpdateRole(roleDto);
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
            return this.View(roleVO);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="roleCode">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "ROLE-DEL")]
        public ActionResult Delete(string roleCode)
        {
            RoleVO roleVo = new RoleVO();
            if (string.IsNullOrEmpty(roleCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                RoleDto roleDto = this.roleBusiness.FindRoleById(roleCode);
                roleVo = Mapper.Map<RoleDto, RoleVO>(roleDto);
                if (roleDto == null)
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

            return this.View(roleVo);
        }

        /// <summary>
        /// Confirms the delete.
        /// </summary>
        /// <param name="roleCode">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ROLE-DEL")]
        public ActionResult DeleteConfirm(string roleCode)
        {
            RoleVO roleVO = new RoleVO();
            if (string.IsNullOrEmpty(roleCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                RoleDto roleDto = this.roleBusiness.FindRoleById(roleCode);
                roleVO = Mapper.Map<RoleDto, RoleVO>(roleDto);
                this.roleBusiness.DeleteRole(roleDto);
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

            return this.View(roleVO);
        }
	}
}