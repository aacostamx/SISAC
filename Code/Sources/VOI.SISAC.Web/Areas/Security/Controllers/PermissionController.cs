//------------------------------------------------------------------------
// <copyright file="PermissionController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Security.Controllers
{
	using AutoMapper;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Net;
	using System.Web.Mvc;
	using VOI.SISAC.Business.Dto.Security;
	using VOI.SISAC.Business.ExceptionBusiness;
	using VOI.SISAC.Business.Security;
	using VOI.SISAC.Web.Controllers;
	using VOI.SISAC.Web.Helpers;
	using VOI.SISAC.Web.Models.VO.Security;
	using VOI.SISAC.Web.Resources;

	/// <summary>
	/// PermissionController
    /// </summary>
    [CustomAuthorize]
	public class PermissionController : BaseController
	{
		/// <summary>
		/// The logger
		/// </summary>
		private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(PermissionController));

		/// <summary>
		/// The user information
		/// </summary>
		private readonly string userInfo;

		/// <summary>
		/// Catalog name
		/// </summary>
		private readonly string catalogName = Resource.PermissionTitle;

		/// <summary>
		/// The primary key
		/// </summary>
		private readonly string primaryKey = Resource.PermissionCode;

		/// <summary>
		/// The airline business
		/// </summary>
		private readonly IPermissionBusiness permissionBusiness;

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="PermissionController"/> class.
		/// </summary>
		/// <param name="permissionBusiness">The Permission business.</param>
		public PermissionController(IPermissionBusiness permissionBusiness)
		{
			this.userInfo = string.Format(
			   LogMessages.UserInfo,
			   System.Environment.UserDomainName,
			   System.Environment.UserName,
			   System.Environment.MachineName);
			this.permissionBusiness = permissionBusiness;
		}
		#endregion

		/// <summary>
		/// Index
		/// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "PERMISSION-IDX")]
		public ActionResult Index()
		{
			Trace.TraceInformation("Iniciando Index Permission");
			IList<PermissionDto> permissionDto = new List<PermissionDto>();
			IList<PermissionVO> permissionVO = new List<PermissionVO>();
			try
			{
				Trace.TraceInformation("Antes de Consulta en BD Permission");
				permissionDto = this.permissionBusiness.GetPermits();
				permissionVO = Mapper.Map<IList<PermissionDto>, IList<PermissionVO>>(permissionDto);
				Trace.TraceInformation("Despues de Consulta en BD Permission");
			}
			catch (BusinessException exception)
			{
				Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
				Logger.Error(exception.Message, exception);
				Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
				Trace.TraceError(exception.Message, exception);
				this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
			}

			return this.View(permissionVO);
		}

		/// <summary>
		/// Creates this instance.
		/// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "PERMISSION-ADD")]
		public ActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// Creates the specified Permission vo.
		/// </summary>
		/// <param name="permissionVO">The Permission vo.</param>
		/// <returns></returns>
		[HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "PERMISSION-ADD")]
		public ActionResult Create(PermissionVO permissionVO)
		{
			PermissionDto permissionDto = new PermissionDto();

			if (permissionVO == null)
			{
				Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
				Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			try
			{
				if (ModelState.IsValid)
				{
					permissionDto = Mapper.Map<PermissionVO, PermissionDto>(permissionVO);
					this.permissionBusiness.AddPermission(permissionDto);
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

			return this.View(permissionVO);
		}

		/// <summary>
		/// Edits the specified identifier.
		/// </summary>
		/// <param name="permissionCode">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "PERMISSION-UPD")]
		public ActionResult Edit(string permissionCode)
		{
			PermissionVO permissionVo = new PermissionVO();
			if (string.IsNullOrEmpty(permissionCode))
			{
				Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
				Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			try
			{
				PermissionDto permissionDto = this.permissionBusiness.FindPermissionById(permissionCode);
				permissionVo = Mapper.Map<PermissionDto, PermissionVO>(permissionDto);

				if (permissionDto == null)
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

			return this.View(permissionVo);
		}

		/// <summary>
		/// Edits the specified Permission vo.
		/// </summary>
		/// <param name="permissionVO">The Permission vo.</param>
		/// <returns></returns>
		[HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "PERMISSION-UPD")]
		public ActionResult Edit(PermissionVO permissionVO)
		{
			PermissionDto permissionDto = new PermissionDto();
			if (permissionVO == null)
			{
				Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
				Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			try
			{
				if (ModelState.IsValid)
				{
					permissionDto = Mapper.Map<PermissionVO, PermissionDto>(permissionVO);
					this.permissionBusiness.UpdatePermission(permissionDto);
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
			return this.View(permissionVO);
		}

		/// <summary>
		/// Deletes the specified identifier.
		/// </summary>
		/// <param name="permissionCode">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "PERMISSION-DEL")]
		public ActionResult Delete(string permissionCode)
		{
			PermissionVO permissionVo = new PermissionVO();
			if (string.IsNullOrEmpty(permissionCode))
			{
				Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
				Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			try
			{
				PermissionDto permissionDto = this.permissionBusiness.FindPermissionById(permissionCode);
				permissionVo = Mapper.Map<PermissionDto, PermissionVO>(permissionDto);
				if (permissionDto == null)
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

			return this.View(permissionVo);
		}

        // <summary>
        // Deletes the confirmed.
        // </summary>
        // <param name="permissionCode">The identifier.</param>
        // <returns></returns>
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string permissionCode)
        //{
        //    PermissionVO permissionVO = new PermissionVO();
        //    if (string.IsNullOrEmpty(permissionCode))
        //    {
        //        Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
        //        Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    try
        //    {
        //        PermissionDto permissionDto = this.permissionBusiness.FindPermissionById(permissionCode);
        //        permissionVO = Mapper.Map<PermissionDto, PermissionVO>(permissionDto);
        //        this.permissionBusiness.DeletePermission(permissionDto);
        //        this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;

        //        return this.RedirectToAction("Index");
        //    }
        //    catch (BusinessException ex)
        //    {
        //        Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
        //        Logger.Error(ex.Message, ex);
        //        Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
        //        Trace.TraceError(ex.Message, ex);
        //        this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
        //    }

        //    return this.View(permissionVO);
        //}

        /// <summary>
        /// Deletes the confirm.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "PERMISSION-DEL")]
        public ActionResult DeleteConfirm(string id)
        {
            PermissionVO permissionVO = new PermissionVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                PermissionDto permissionDto = this.permissionBusiness.FindPermissionById(id);
                permissionVO = Mapper.Map<PermissionDto, PermissionVO>(permissionDto);
                this.permissionBusiness.DeletePermission(permissionDto);
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

            return this.View(permissionVO);
        }
	}
}