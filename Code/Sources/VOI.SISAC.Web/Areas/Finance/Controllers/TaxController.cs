//------------------------------------------------------------------------
// <copyright file="TaxController.cs" company="Volaris">
//     Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System.Data;
    using System.Net;
    using System.Web.Mvc;
    using VOI.SISAC.Business.Dto.Finances;
    using Business = VOI.SISAC.Business.Finance;
    using VOI.SISAC.Business.ExceptionBusiness;
    using System;
    using Resources;
    using System.Collections;
    using System.Collections.Generic;
    using Models.VO.Finance;
    using AutoMapper;
    using Helpers;
    using System.Diagnostics;
    using Web.Controllers;

    /// <summary>
    /// Controlador de la vista Tax
    /// </summary>
    [CustomAuthorize]
    public class TaxController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(TaxController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Interfaz privada del negocio solo de lectura
        /// </summary>        
        private readonly Business.ITaxBusiness taxBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxController"/> class.
        /// </summary>
        /// <param name="taxBusiness">Interfaz del negocio Tax</param>
        public TaxController(Business.ITaxBusiness taxBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.taxBusiness = taxBusiness;
        }

        /// <summary>
        /// Vista principal
        /// </summary>
        /// <returns>Vista con todos los impuestos</returns>
        [CustomAuthorize(Roles = "TAX-IDX")]
        public ActionResult Index()
        {
            Trace.TraceInformation("Entrando a Tax");
            IList<TaxVO> taxesVo = new List<TaxVO>();
            IList<TaxDto> taxes = new List<TaxDto>();
            try
            {
                Trace.TraceInformation("Entrando consulta DB");
                taxes = this.taxBusiness.GetActivesTaxes();
                taxesVo = Mapper.Map<IList<TaxDto>, IList<TaxVO>>(taxes);
                Trace.TraceInformation("Termina consulta DB");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "TAX", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "TAX", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            Trace.TraceInformation("Devolviendo valor");
            return this.View(taxesVo);
        }

        /// <summary>
        /// Vista create
        /// </summary>
        /// <returns>Vista create</returns>
        [CustomAuthorize(Roles = "TAX-ADD")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Vista para insertar en el catálogo impuestos POST
        /// </summary>
        /// <param name="tax">Contiene el objecto del formulario que será editado</param>
        /// <returns>regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "TAX-ADD")]
        public ActionResult Create(TaxVO tax)
        {
            if (tax == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "TAX"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "TAX"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    TaxDto taxDto = new TaxDto();
                    taxDto = Mapper.Map<TaxVO, TaxDto>(tax);
                    this.taxBusiness.AddTax(taxDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessCreate;
                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorCreate, "TAX", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "TAX", this.userInfo));
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                if (ex.Number == 10)
                {
                    message = string.Format(message, VOI.SISAC.Web.Resources.Resource.TaxCode);
                }
                this.ViewBag.ErrorMessage = message;
            }
            return this.View(tax);
        }

        /// <summary>
        /// Vista para editar registros del catálogo Tax
        /// </summary>
        /// <param name="id">Identificador del item del catálogo a editar</param>
        /// <returns>Regresa la vista Edit cargada con el item del modelo a editar</returns>
        [CustomAuthorize(Roles = "TAX-UPD")]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "TAX"));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, "TAX", this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaxDto tax = new TaxDto();
            TaxVO taxVo = new TaxVO();
            try
            {
                tax = taxBusiness.FindTaxById(id);
                taxVo = Mapper.Map<TaxDto, TaxVO>(tax);
                if (taxVo == null)
                {
                    return this.HttpNotFound();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "TAX", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "TAX", this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }
            return this.View(taxVo);
        }

        /// <summary>
        /// Vista para editar registro del catálogo Tax POST
        /// </summary>
        /// <param name="tax">Objecto de tipo aeropuerto que toma la información del formulario</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "TAX-UPD")]
        public ActionResult Edit(TaxVO tax)
        {
            if (tax == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "TAX"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "TAX"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    TaxDto taxDto = new TaxDto();
                    taxDto = Mapper.Map<TaxVO, TaxDto>(tax);
                    taxBusiness.UpdateTax(taxDto);
                    this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                    return RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, "TAX", this.userInfo));
                Logger.Error(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(tax);
        }

        /// <summary>
        /// Vista Delete
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa la vista Delete precargada con la información a eliminar</returns>
        [CustomAuthorize(Roles = "TAX-DEL")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "TAX"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "TAX"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaxDto tax = new TaxDto();
            TaxVO taxVo = new TaxVO();
            try
            {
                tax = this.taxBusiness.FindTaxById(id);
                taxVo = Mapper.Map<TaxDto, TaxVO>(tax);
                if (taxVo == null)
                {
                    return HttpNotFound();
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "TAX", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "TAX", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return View(taxVo);
        }

        /// <summary>
        /// Vista Delete POST
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRLINE-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "TAX"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "TAX"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxDto tax = new TaxDto();
            TaxVO taxVo = new TaxVO();
            try
            {
                tax = this.taxBusiness.FindTaxById(id);
                this.taxBusiness.DeleteTax(tax);
                taxVo = Mapper.Map<TaxDto, TaxVO>(tax);
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessDelete;
                return this.RedirectToAction("Index");
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, "TAX", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, "TAX", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(taxVo);
        }
    }
}