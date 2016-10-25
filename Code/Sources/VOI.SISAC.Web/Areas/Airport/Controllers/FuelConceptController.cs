//------------------------------------------------------------------------
// <copyright file="FuelConceptController.cs" company="Volaris">
//     Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Resources;
    using Business = VOI.SISAC.Business.Airport;

    /// <summary>
    /// Controlador de la vista FuelConcept
    /// </summary>
    [CustomAuthorize]
    public class FuelConceptController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(FuelConceptController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.FuelConceptTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = VOI.SISAC.Web.Resources.Resource.FuelConceptName;

        /// <summary>
        /// Interfaz privada del negocio solo de lectura
        /// </summary>        
        private readonly Business.IFuelConceptBusiness fuelConceptBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuelConceptController"/> class.
        /// </summary>
        /// <param name="fuelConceptBusiness">Interfaz del negocio FuelConcept</param>
        public FuelConceptController(Business.IFuelConceptBusiness fuelConceptBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.fuelConceptBusiness = fuelConceptBusiness;
        }

        /// <summary>
        /// Vista principal
        /// </summary>
        /// <returns>Vista con todas los conceptos de combustible</returns>
        [CustomAuthorize(Roles = "FUELCONCEP-IDX")]
        public ActionResult Index()
        {
            System.Diagnostics.Trace.TraceInformation("Beginning FuelConcept");
            try
            {
                IList<FuelConceptVO> fuelConceptsVo = Mapper.Map<IList<FuelConceptDto>, IList<FuelConceptVO>>(fuelConceptBusiness.GetActivesFuelConcepts());
                return this.View(fuelConceptsVo);
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
        [CustomAuthorize(Roles = "FUELCONCEP-ADD")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Vista para insertar en el catálogo conceptos de combustible POST
        /// </summary>
        /// <param name="fuelConcept">Contiene el objecto del formulario que será editado</param>
        /// <returns>regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "FUELCONCEP-ADD")]
        public ActionResult Create(FuelConceptVO fuelConcept)
        {
            if (fuelConcept == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    FuelConceptDto fuelConceptDto = Mapper.Map<FuelConceptVO, FuelConceptDto>(fuelConcept);
                    fuelConceptBusiness.AddFuelConcept(fuelConceptDto);
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

            return this.View(fuelConcept);
        }

        /// <summary>
        /// Vista para editar registros del catálogo FuelConcept
        /// </summary>
        /// <param name="id">Identificador del item del catálogo a editar</param>
        /// <returns>Regresa la vista Edit cargada con el item del modelo a editar</returns>
        [CustomAuthorize(Roles = "FUELCONCEP-UPD")]
        public ActionResult Edit(long id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                FuelConceptVO fuelConceptVo = Mapper.Map<FuelConceptDto, FuelConceptVO>(fuelConceptBusiness.FindFuelConceptById(id));
                if (fuelConceptVo == null)
                    return HttpNotFound();

                return this.View(fuelConceptVo);
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
        /// Vista para editar registro del catálogo FuelConcept POST
        /// </summary>
        /// <param name="fuelConcept">Objecto de tipo cuenta contable que toma la información del formulario</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "FUELCONCEP-UPD")]
        public ActionResult Edit(FuelConceptVO fuelConcept)
        {
            if (fuelConcept == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    fuelConceptBusiness.UpdateFuelConcept(Mapper.Map<FuelConceptVO, FuelConceptDto>(fuelConcept));
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;

                    return RedirectToAction("Index");
                }

            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                string message = FrontMessage.GetExceptionErrorMessage(exception.Number);
                if (exception.Number == 10)
                {
                    message = string.Format(message, primaryKey);
                }

                this.ViewBag.ErrorMessage = message;
            }

            return View(fuelConcept);
        }

        /// <summary>
        /// Vista Delete
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa la vista Delete precargada con la información a eliminar</returns>
        [CustomAuthorize(Roles = "FUELCONCEP-DEL")]
        public ActionResult Delete(long id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (string.IsNullOrEmpty(id.ToString()))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                FuelConceptVO fuelConceptVo = Mapper.Map<FuelConceptDto, FuelConceptVO>(fuelConceptBusiness.FindFuelConceptById(id));
                if (fuelConceptVo == null)
                    return HttpNotFound();

                return View(fuelConceptVo);
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
        [CustomAuthorize(Roles = "FUELCONCEP-DEL")]
        public ActionResult DeleteConfirmed(long id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                FuelConceptDto fuelConcept = fuelConceptBusiness.FindFuelConceptById(id);
                fuelConceptBusiness.DeleteFuelConcept(fuelConcept);
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