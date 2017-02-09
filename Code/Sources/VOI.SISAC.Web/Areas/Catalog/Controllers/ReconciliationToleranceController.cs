//------------------------------------------------------------------------
// <copyright file="ReconciliationToleranceController.cs" company="AACOSTA">
//     Copyright(c) AACOSTA - Todos los derechos reservados.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Catalog.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Catalog;
    using VOI.SISAC.Business.Common;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Catalog;
    using VOI.SISAC.Web.Resources;
    using Web.Controllers;

    /// <summary>
    /// ReconciliationToleranceController Class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    [CustomAuthorize]
    public class ReconciliationToleranceController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(ReconciliationToleranceController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string moduleName = Resource.ReconciliationToleranceTitle;

        /// <summary>
        /// Interface for Airport service contract operations
        /// </summary>
        private readonly IReconciliationToleranceBusiness toleranceBusiness;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness generic;

        /// <summary>
        /// Exchange Rates Controller Constructor
        /// </summary>
        /// <param name="toleranceBusiness"></param>
        /// <param name="generic"></param>
        public ReconciliationToleranceController(IReconciliationToleranceBusiness toleranceBusiness, IGenericCatalogBusiness generic)
        {
            this.userInfo = string.Format(
            LogMessages.UserInfo,
            Environment.UserDomainName,
            Environment.UserName,
            Environment.MachineName);
            this.toleranceBusiness = toleranceBusiness;
            this.generic = generic;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "RECTOLE-IDX")]
        public ActionResult Index()
        {
            IList<ReconciliationToleranceVO> tolerances = new List<ReconciliationToleranceVO>();
            try
            {
                var exchangeDto = this.toleranceBusiness.GetAllActivesReconciliationTolerances();
                tolerances = Mapper.Map<IList<ReconciliationToleranceVO>>(exchangeDto);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(tolerances);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "RECTOLE-ADD")]
        public ActionResult Create()
        {
            this.LoadCatalogs();
            return this.View();
        }

        /// <summary>
        /// Creates the specified tolerance vo.
        /// </summary>
        /// <param name="toleranceVO">The tolerance vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "RECTOLE-ADD")]
        public ActionResult Create(ReconciliationToleranceVO toleranceVO)
        {
            var toleranceDto = new ReconciliationToleranceDto();

            if (toleranceVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    toleranceDto = Mapper.Map<ReconciliationToleranceDto>(toleranceVO);
                    this.toleranceBusiness.AddReconciliationTolerance(toleranceDto);
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
                message = ex.Number == 10 ? string.Format(message, Resource.ReconciliationToleranceTitle) : message;
                this.ViewBag.ErrorMessage = message;
                this.LoadCatalogs();
            }

            return this.View(toleranceVO);
        }

        /// <summary>
        /// Edits the specified service code.
        /// </summary>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="toleranceTypeCode">The tolerance type code.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "RECTOLE-UPD")]
        public ActionResult Edit(string serviceCode, string currencyCode, string toleranceTypeCode)
        {
            var toleranceVO = new ReconciliationToleranceVO();

            if (string.IsNullOrEmpty(serviceCode) || string.IsNullOrEmpty(currencyCode) || string.IsNullOrEmpty(toleranceTypeCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                toleranceVO = Mapper.Map<ReconciliationToleranceVO>(this.toleranceBusiness.FindReconciliationToleranceById(new ReconciliationToleranceDto(serviceCode, currencyCode, toleranceTypeCode)));                
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

            return this.View(toleranceVO);
        }

        /// <summary>
        /// Edits the specified tolerance vo.
        /// </summary>
        /// <param name="toleranceVO">The tolerance vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "RECTOLE-UPD")]
        public ActionResult Edit(ReconciliationToleranceVO toleranceVO)
        {
            if (toleranceVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (this.ModelState.IsValid)
                {
                    var tolerance = Mapper.Map<ReconciliationToleranceDto>(toleranceVO);
                    this.toleranceBusiness.UpdateReconciliationTolerance(tolerance);
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

            return this.View(toleranceVO);
        }

        /// <summary>
        /// Deletes the specified service code.
        /// </summary>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <param name="toleranceTypeCode">The tolerance type code.</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "RECTOLE-DEL")]
        public ActionResult Delete(string serviceCode, string currencyCode, string toleranceTypeCode)
        {            
            var toleranceVO = new ReconciliationToleranceVO();

            if (string.IsNullOrEmpty(serviceCode) || string.IsNullOrEmpty(currencyCode) || string.IsNullOrEmpty(toleranceTypeCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                toleranceVO = Mapper.Map<ReconciliationToleranceVO>(this.toleranceBusiness.FindReconciliationToleranceById(new ReconciliationToleranceDto(serviceCode, currencyCode, toleranceTypeCode)));
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View("Delete", toleranceVO);
        }

        /// <summary>
        /// Deletes the specified tolerance vo.
        /// </summary>
        /// <param name="toleranceVO">The tolerance vo.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "RECTOLE-DEL")]
        public ActionResult Delete(ReconciliationToleranceVO toleranceVO)
        {
            var toleranceDto = new ReconciliationToleranceDto();

            if (toleranceVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.moduleName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                toleranceDto = this.toleranceBusiness.FindReconciliationToleranceById(new ReconciliationToleranceDto(toleranceVO.ServiceCode, toleranceVO.CurrencyCode, toleranceVO.ToleranceTypeCode));

                if (toleranceDto != null)
                {
                    this.toleranceBusiness.DeleteReconciliationTolerance(toleranceDto);
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

            return this.View("Delete", toleranceVO);
        }

        /// <summary>
        /// Load Catalogs
        /// </summary>
        private void LoadCatalogs()
        {
            try
            {
                this.ViewBag.Currency = this.generic.GetCurrencyCatalog();
                this.ViewBag.Service = this.generic.GetServiceCatalog();
                this.ViewBag.Tolerance = this.generic.GetToleranceTypeCatalog();
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