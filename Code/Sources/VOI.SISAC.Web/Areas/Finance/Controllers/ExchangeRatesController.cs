//------------------------------------------------------------------------
// <copyright file="ExchangeRatesController.cs" company="Volaris">
//     Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using AutoMapper;
    using Business.Common;
    using Business.Dto.Finances;
    using Business.ExceptionBusiness;
    using Business.Finance;
    using Helpers;
    using Models.VO.Finance;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using Web.Controllers;

    /// <summary>
    /// Exchange Rates Controller Class 
    /// </summary>
    [CustomAuthorize]
    public class ExchangeRatesController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ExchangeRatesController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = Resource.ExchangeRatesTitle;

        /// <summary>
        /// Interface for Airport service contract operations
        /// </summary>
        private readonly IExchangeRatesBusiness exchangeBusiness;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

        /// <summary>
        /// Exchange Rates Controller Constructor
        /// </summary>
        /// <param name="exchangeBusiness"></param>
        /// <param name="generic"></param>
        public ExchangeRatesController(IExchangeRatesBusiness exchangeBusiness, IGenericCatalogBusiness generic)
        {
            this.userInfo = string.Format(
            LogMessages.UserInfo,
            Environment.UserDomainName,
            Environment.UserName,
            Environment.MachineName);
            this.exchangeBusiness = exchangeBusiness;
            this.generic = generic;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "EXCHRATE-IDX")]
        public ActionResult Index()
        {
            IList<ExchangeRatesVO> exchange = new List<ExchangeRatesVO>();
            try
            {
                var exchangeDto = this.exchangeBusiness.GetAllActivesExchangeRates();
                exchange = Mapper.Map<IList<ExchangeRatesVO>>(exchangeDto);
                foreach (var item in exchange)
                {
                    if (item.Year > 0 && (item.Month >= 1 && item.Month <= 12))
                    {
                        item.ExchangeDate = new DateTime(item.Year, item.Month, 1);
                    }
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(exchange);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "EXCHRATE-ADD")]
        public ActionResult Create()
        {
            this.LoadCatalogs();
            return this.View();
        }

        /// <summary>
        /// Create Post
        /// </summary>
        /// <param name="exchangeVO"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "EXCHRATE-ADD")]
        public ActionResult Create(ExchangeRatesVO exchangeVO)
        {
            var exchangeDto = new ExchangeRatesDto();

            if (exchangeVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    exchangeVO.Year = exchangeVO.ExchangeDate.Year;
                    exchangeVO.Month = exchangeVO.ExchangeDate.Month;
                    exchangeDto = Mapper.Map<ExchangeRatesDto>(exchangeVO);
                    this.exchangeBusiness.AddExchangeRate(exchangeDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.LoadCatalogs();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                message = ex.Number == 10 ? string.Format(message, Resource.ExchangeRatesTitle) : message;
                this.ViewBag.ErrorMessage = message;
                this.LoadCatalogs();
            }

            return this.View(exchangeVO);
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "EXCHRATE-UPD")]
        public ActionResult Edit(int year, int month, string currencyCode)
        {
            var exchangeVO = new ExchangeRatesVO();

            if (year < 0 || month < 0 || string.IsNullOrEmpty(currencyCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                exchangeVO = Mapper.Map<ExchangeRatesVO>(exchangeBusiness.FindExchangeRateById(new ExchangeRatesDto(year, month, currencyCode)));
                exchangeVO.ExchangeDate = new DateTime(exchangeVO.Year, exchangeVO.Month, 1);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
                this.LoadCatalogs();
            }

            return this.View(exchangeVO);
        }

        /// <summary>
        /// Edit Post
        /// </summary>
        /// <param name="exchangeVO"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "EXCHRATE-UPD")]
        public ActionResult Edit(ExchangeRatesVO exchangeVO)
        {
            if (exchangeVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    var exchange = Mapper.Map<ExchangeRatesDto>(exchangeVO);
                    this.exchangeBusiness.UpdateExchangeRate(exchange);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return this.RedirectToAction("Index");
                }
                else
                {
                    this.LoadCatalogs();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
                this.LoadCatalogs();
            }

            return this.View(exchangeVO);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "EXCHRATE-DEL")]
        public ActionResult Delete(int year, int month, string currencyCode)
        {
            var exchangeVO = new ExchangeRatesVO();

            if (year < 0 || month < 0 || string.IsNullOrEmpty(currencyCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                exchangeVO = Mapper.Map<ExchangeRatesVO>(exchangeBusiness.FindExchangeRateById(new ExchangeRatesDto(year, month, currencyCode)));
                exchangeVO.ExchangeDate = new DateTime(exchangeVO.Year, exchangeVO.Month, 1);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View("Delete", exchangeVO);
        }

        /// <summary>
        /// Delete POST
        /// </summary>
        /// <param name="exchangeVO"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "EXCHRATE-DEL")]
        public ActionResult Delete(ExchangeRatesVO exchangeVO)
        {
            var exchangeDto = new ExchangeRatesDto();

            if (exchangeVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                exchangeDto = this.exchangeBusiness.FindExchangeRateById(new ExchangeRatesDto(exchangeVO.Year, exchangeVO.Month, exchangeVO.CurrencyCode));

                if (exchangeDto != null)
                {
                    this.exchangeBusiness.DeleteExchangeRate(exchangeDto);
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View("Delete", exchangeVO);
        }

        /// <summary>
        /// Load Catalogs
        /// </summary>
        private void LoadCatalogs()
        {
            try
            {
                this.ViewBag.Currency = this.generic.GetCurrencyCatalog();
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
        }
    }
}