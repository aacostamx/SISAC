//------------------------------------------------------------------------
// <copyright file="LiabilityAccountController.cs" company="Volaris">
//     Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Resources;
    using Business = VOI.SISAC.Business.Finance;

    /// <summary>
    /// Controlador de la vista LiabilityAccount
    /// </summary>
    [CustomAuthorize]
    public class LiabilityAccountController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(LiabilityAccountController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.LiabilityAccountTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = VOI.SISAC.Web.Resources.Resource.LiabilityAccountNumber;

        /// <summary>
        /// Interfaz privada del negocio solo de lectura
        /// </summary>
        private readonly Business.ILiabilityAccountBusiness liabilityAccountBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiabilityAccountController"/> class.
        /// </summary>
        /// <param name="liabilityAccountBusiness">Interfaz del negocio LiabilityAccount</param>
        public LiabilityAccountController(Business.ILiabilityAccountBusiness liabilityAccountBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.liabilityAccountBusiness = liabilityAccountBusiness;
        }

        /// <summary>
        /// Vista principal
        /// </summary>
        /// <returns>Vista con todos las cuentas de pasivo</returns>
        [CustomAuthorize(Roles = "LIAACC-IDX")]
        public ActionResult Index()
        {
            System.Diagnostics.Trace.TraceInformation("Beginning LiabilityAccount");
            try
            {
                IList<LiabilityAccountVO> liabilityAccountVo = Mapper.Map<IList<LiabilityAccountDto>, IList<LiabilityAccountVO>>(liabilityAccountBusiness.GetActivesLiabilityAccounts());
                return this.View(liabilityAccountVo);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View();
            }
        }

        /// <summary>
        /// Vista create
        /// </summary>
        /// <returns>Vista create</returns>
        [CustomAuthorize(Roles = "LIAACC-ADD")]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Vista para insertar en el catálogo cuenta de pasivo POST
        /// </summary>
        /// <param name="liabilityAccount">Contiene el objecto del formulario que será editado</param>
        /// <returns>regresa a la vista principal</returns> 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "LIAACC-ADD")]
        public ActionResult Create([Bind(Include = "LiabilityAccountNumber,LiabilityAccountName")] LiabilityAccountVO liabilityAccount)
        {
            if (liabilityAccount == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    LiabilityAccountDto liabilityAccountDto = Mapper.Map<LiabilityAccountVO, LiabilityAccountDto>(liabilityAccount);
                    liabilityAccountBusiness.AddLiabilityAccount(liabilityAccountDto);
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
                    message = string.Format(message, primaryKey);
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(liabilityAccount);
        }

        /// <summary>
        /// Vista para editar registros del catálogo LiabilityAccount
        /// </summary>
        /// <param name="id">Identificador del item del catálogo a editar</param>
        /// <returns>Regresa la vista Edit cargada con el item del modelo a editar</returns>
        [CustomAuthorize(Roles = "LIAACC-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (string.IsNullOrEmpty(id))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                LiabilityAccountVO liabilityAccountVo = Mapper.Map<LiabilityAccountDto, LiabilityAccountVO>(liabilityAccountBusiness.FindLiabilityAccountById(id));
                if (liabilityAccountVo == null)
                    return HttpNotFound();

                return this.View(liabilityAccountVo);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return this.View();
            }
        }

        /// <summary>
        /// Vista para editar registro del catálogo LiabilityAccount POST
        /// </summary>
        /// <param name="liabilityAccount">Objecto de tipo cuenta de pasivo que toma la información del formulario</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "LIAACC-UPD")]
        public ActionResult Edit([Bind(Include = "LiabilityAccountNumber,LiabilityAccountName")] LiabilityAccountVO liabilityAccount)
        {
            if (liabilityAccount == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    liabilityAccountBusiness.UpdateLiabilityAccount(Mapper.Map<LiabilityAccountVO, LiabilityAccountDto>(liabilityAccount));
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;

                    return RedirectToAction("Index");
                }

                return this.View(liabilityAccount);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);

                return this.View();
            }
        }

        /// <summary>
        /// Vista del detalle del registro
        /// </summary>
        /// <param name="id">Identificador del item del modelo a mostrar</param>
        /// <returns>Regresa la vista Details con el item del modelo</returns>
        [CustomAuthorize(Roles = "LIAACC-VIE")]
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            LiabilityAccountDto liabilityAccount = liabilityAccountBusiness.FindLiabilityAccountById(id);

            if (liabilityAccount == null)
                return HttpNotFound();

            return this.View(liabilityAccount);
        }

        /// <summary>
        /// Vista Delete
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa la vista Delete precargada con la información a eliminar</returns>
        [CustomAuthorize(Roles = "LIAACC-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (string.IsNullOrEmpty(id))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                LiabilityAccountVO liabilityAccountVo = Mapper.Map<LiabilityAccountDto, LiabilityAccountVO>(liabilityAccountBusiness.FindLiabilityAccountById(id));
                if (liabilityAccountVo == null)
                    return HttpNotFound();

                return this.View(liabilityAccountVo);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message.ToString();

                System.Diagnostics.Trace.TraceError(ex.ToString());
                return this.View();
            }
        }

        /// <summary>
        /// Vista Delete POST
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "LIAACC-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                LiabilityAccountDto liabilityAccount = liabilityAccountBusiness.FindLiabilityAccountById(id);
                liabilityAccountBusiness.DeleteLiabilityAccount(liabilityAccount);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;

                return RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);

                return this.View();
            }
        }
    }
}