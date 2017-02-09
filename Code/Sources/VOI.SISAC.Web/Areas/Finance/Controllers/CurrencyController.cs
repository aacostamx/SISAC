//------------------------------------------------------------------------
// <copyright file="CurrencyController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{    
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Finance;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Controller for Currency section
    /// </summary>
    [CustomAuthorize]
    public class CurrencyController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(CurrencyController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Interface for the business operations
        /// </summary>
        private readonly ICurrencyBusiness currencyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyController"/> class.
        /// </summary>
        /// <param name="currencyService">The currency business.</param>
        public CurrencyController(ICurrencyBusiness currencyService)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.currencyService = currencyService;
        }

        /// <summary>
        /// Main view
        /// </summary>
        /// <returns>View with the currency records.</returns>
        [CustomAuthorize(Roles = "CURRENCY-IDX")]
        public ActionResult Index()
        {
            IList<CurrencyVO> currenciesVo = new List<CurrencyVO>();
            IList<CurrencyDto> currencies = new List<CurrencyDto>();
            try
            {
                currencies = this.currencyService.GetActivesCurrency();
                currenciesVo = Mapper.Map<IList<CurrencyDto>, IList<CurrencyVO>>(currencies);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "CURRENCY", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "CURRENCY", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(currenciesVo);
        }

        /// <summary>
        /// Create Action for Currency.
        /// </summary>
        /// <returns>The Create view.</returns>
        [CustomAuthorize(Roles = "CURRENCY-ADD")]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Action for insert a new record in currency catalog by POST
        /// </summary>
        /// <param name="currencyVo">Object that contains the form that will be inserted.</param>
        /// <returns>The Create view.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "CURRENCY-ADD")]
        public ActionResult Create(CurrencyVO currencyVo)
        {
            if (currencyVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "CURRENCY"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "CURRENCY"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    CurrencyDto currency = Mapper.Map<CurrencyVO, CurrencyDto>(currencyVo);
                    this.currencyService.AddCurrency(currency);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                // Sets information of the error.
                Logger.Error(string.Format(LogMessages.ErrorCreate, "CURRENCY", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "CURRENCY", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 10)
                {
                    message = string.Format(message, "Currency code");
                }

                this.ViewBag.ErrorMessage = message;
            }

            return this.View(currencyVo);
        }

        /// <summary>
        /// Action for edit a record in the catalog Currency.
        /// </summary>
        /// <param name="id">Id of the item that will be edited.</param>
        /// <returns>Returns the the Edit view with the item to be edited.</returns>
        [CustomAuthorize(Roles = "CURRENCY-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "CURRENCY"));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "CURRENCY", this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CurrencyVO currencyVo = new CurrencyVO();
            try
            {
                CurrencyDto currency = new CurrencyDto();
                currency = this.currencyService.FindCurrencyById(id);
                if (currencyVo == null)
                {
                    return this.HttpNotFound();
                }

                currencyVo = Mapper.Map<CurrencyDto, CurrencyVO>(currency);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "CURRENCY", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "CURRENCY", this.userInfo));
                Trace.TraceError(exception.Message, exception);
            }

            return this.View(currencyVo);
        }

        /// <summary>
        /// Action for edit a record in the catalog Currency by POST
        /// </summary>
        /// <param name="currencyVo">Object that contains the form that will be edited.</param>
        /// <returns>If the The Edit View.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "CURRENCY-UPD")]
        public ActionResult Edit(CurrencyVO currencyVo)
        {
            if (currencyVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "CURRENCY"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "CURRENCY"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    CurrencyDto currency = Mapper.Map<CurrencyVO, CurrencyDto>(currencyVo);
                    this.currencyService.UpdateCurrency(currency);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, "CURRENCY", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, "CURRENCY", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(currencyVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog Currency
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns the the Delete view with the item to be deleted.</returns>
        [CustomAuthorize(Roles = "CURRENCY-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "CURRENCY"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "CURRENCY"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CurrencyVO currencyVo = new CurrencyVO();
            try
            {
                CurrencyDto currency = this.currencyService.FindCurrencyById(id);
                if (currency == null)
                {
                    return this.HttpNotFound();
                }

                currencyVo = Mapper.Map<CurrencyDto, CurrencyVO>(currency);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "CURRENCY", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "CURRENCY", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(currencyVo);
        }

        /// <summary>
        /// Action for delete a record in the catalog Currency by POST
        /// </summary>
        /// <param name="id">Id of the item that will be deleted.</param>
        /// <returns>Returns to the Catalog view.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "CURRENCY-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "CURRENCY"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "CURRENCY"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CurrencyVO currencyVo = new CurrencyVO();
            try
            {
                CurrencyDto currency = this.currencyService.FindCurrencyById(id);
                this.currencyService.DeleteCurrency(currency);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;
                currencyVo = Mapper.Map<CurrencyDto, CurrencyVO>(currency);
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, "CURRENCY", this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, "CURRENCY", this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(currencyVo);
        }
    }
}
