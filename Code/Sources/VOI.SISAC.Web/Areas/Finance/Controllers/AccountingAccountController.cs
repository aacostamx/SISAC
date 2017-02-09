//------------------------------------------------------------------------
// <copyright file="AccountingAccountController.cs" company="AACOSTA">
//     Copyright(c) AACOSTA - Todos los derechos reservados.
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
    using VOI.SISAC.Web.Areas.Finance.Controllers;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Resources;
    using Business = VOI.SISAC.Business.Finance;

    /// <summary>
    /// Controlador de la vista AccountingAccount
    /// </summary>
    [CustomAuthorize]
    public class AccountingAccountController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AccountingAccountController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.AccountingAccountTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = VOI.SISAC.Web.Resources.Resource.AccountingAccountNumber;

        /// <summary>
        /// Interfaz privada del negocio solo de lectura
        /// </summary>        
        private readonly Business.IAccountingAccountBusiness accountingAccountBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountingAccountController"/> class.
        /// </summary>
        /// <param name="accountingAccountBusiness">Interfaz del negocio AccountingAccount</param>
        public AccountingAccountController(Business.IAccountingAccountBusiness accountingAccountBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.accountingAccountBusiness = accountingAccountBusiness;
        }

        /// <summary>
        /// Vista principal
        /// </summary>
        /// <returns>Vista con todos las cuentas contables</returns>
        [CustomAuthorize(Roles = "ACCACC-IDX" )]
        public ActionResult Index()
        {
            System.Diagnostics.Trace.TraceInformation("Beginning AccountingAccount");
            IList<AccountingAccountVO> accountingAccountsVo = new List<AccountingAccountVO>();
            IList<AccountingAccountDto> accountingAccounts = new List<AccountingAccountDto>();
            try
            {
                accountingAccounts = accountingAccountBusiness.GetActivesAccountingAccounts();
                accountingAccountsVo = Mapper.Map<IList<AccountingAccountDto>, IList<AccountingAccountVO>>(accountingAccounts);
                return this.View(accountingAccountsVo);
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
        [CustomAuthorize(Roles = "ACCACC-ADD")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Vista para insertar en el catálogo cuentas contables POST
        /// </summary>
        /// <param name="accountingAccount">Contiene el objecto del formulario que será editado</param>
        /// <returns>regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ACCACC-ADD")]
        public ActionResult Create([Bind(Include = "AccountingAccountNumber,AccountingAccountName")] AccountingAccountVO accountingAccount)
        {
            if (accountingAccount == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    AccountingAccountDto accountingAccountDto = new AccountingAccountDto();
                    accountingAccountDto = Mapper.Map<AccountingAccountVO, AccountingAccountDto>(accountingAccount);
                    accountingAccountBusiness.AddAccountingAccount(accountingAccountDto);
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

            return this.View(accountingAccount);
        }

        /// <summary>
        /// Vista para editar registros del catálogo AccountingAccount
        /// </summary>
        /// <param name="id">Identificador del item del catálogo a editar</param>
        /// <returns>Regresa la vista Edit cargada con el item del modelo a editar</returns>
        [CustomAuthorize(Roles = "ACCACC-UPD")]
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

                AccountingAccountDto accountingAccount = accountingAccountBusiness.FindAccountingAccountById(id);
                AccountingAccountVO accountingAccountVo = Mapper.Map<AccountingAccountDto, AccountingAccountVO>(accountingAccount);
                if (accountingAccountVo == null)
                    return HttpNotFound();

                return this.View(accountingAccountVo);
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
        /// Vista para editar registro del catálogo AccountingAccount POST
        /// </summary>
        /// <param name="accountingAccount">Objecto de tipo cuenta contable que toma la información del formulario</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "ACCACC-UPD")]
        public ActionResult Edit([Bind(Include = "AccountingAccountNumber,AccountingAccountName")] AccountingAccountVO accountingAccount)
        {
            if (accountingAccount == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    AccountingAccountDto accountingAccountDto = Mapper.Map<AccountingAccountVO, AccountingAccountDto>(accountingAccount);
                    accountingAccountBusiness.UpdateAccountingAccount(accountingAccountDto);
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;

                    return RedirectToAction("Index");
                }

                return View(accountingAccount);
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
        [CustomAuthorize(Roles = "ACCACC-VIE")]
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            AccountingAccountDto accountingAccount = accountingAccountBusiness.FindAccountingAccountById(id);

            if (accountingAccount == null)
                return HttpNotFound();

            return View(accountingAccount);
        }

        /// <summary>
        /// Vista Delete
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa la vista Delete precargada con la información a eliminar</returns>
        [CustomAuthorize(Roles = "ACCACC-DEL")]
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

                AccountingAccountDto accountingAccount = accountingAccountBusiness.FindAccountingAccountById(id);
                AccountingAccountVO accountingAccountVO = Mapper.Map<AccountingAccountDto, AccountingAccountVO>(accountingAccount);
                if (accountingAccountVO == null)
                    return HttpNotFound();

                return View(accountingAccountVO);
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
        [CustomAuthorize(Roles = "ACCACC-DEL")]
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
                AccountingAccountDto accountingAccount = accountingAccountBusiness.FindAccountingAccountById(id);
                accountingAccountBusiness.DeleteAccountingAccount(accountingAccount);
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