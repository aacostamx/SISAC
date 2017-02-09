//------------------------------------------------------------------------
// <copyright file="FunctionalAreaController.cs" company="AACOSTA">
//     Copyright(c) AACOSTA - Todos los derechos reservados.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Catalog.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Catalog;
    using VOI.SISAC.Web.Resources;
    using Business = VOI.SISAC.Business.Catalog;

    /// <summary>
    /// Controlador de la vista FunctionalArea
    /// </summary>
    [CustomAuthorize]
    public class FunctionalAreaController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(FunctionalAreaController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.FunctionalAreaTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = VOI.SISAC.Web.Resources.Resource.FunctionalAreaID;

        /// <summary>
        /// Interfaz privada del negocio solo de lectura
        /// </summary>        
        private readonly Business.IFunctionalAreaBusiness functionalAreaBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionalAreaController"/> class.
        /// </summary>
        /// <param name="functionalAreaBusiness">Interfaz del negocio FunctionalArea</param>
        public FunctionalAreaController(Business.IFunctionalAreaBusiness functionalAreaBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.functionalAreaBusiness = functionalAreaBusiness;
        }

        /// <summary>
        /// Vista principal
        /// </summary>
        /// <returns>Vista con todas las areas funcionales</returns>
        [CustomAuthorize(Roles = "FUNCAREA-IDX")]
        public ActionResult Index()
        {
            System.Diagnostics.Trace.TraceInformation("Beginning FunctionalArea");
            try
            {
                IList<FunctionalAreaVO> functionalAreasVo = Mapper.Map<IList<FunctionalAreaDto>, IList<FunctionalAreaVO>>(functionalAreaBusiness.GetActivesFunctionalAreas());
                return this.View(functionalAreasVo);
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
        [CustomAuthorize(Roles = "FUNCAREA-ADD")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Vista para insertar en el catálogo areas funcionales POST
        /// </summary>
        /// <param name="functionalArea">Contiene el objecto del formulario que será editado</param>
        /// <returns>regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "FUNCAREA-ADD")]
        public ActionResult Create(FunctionalAreaVO functionalArea)
        {
            if (functionalArea == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    functionalAreaBusiness.AddFunctionalArea(Mapper.Map<FunctionalAreaVO, FunctionalAreaDto>(functionalArea));
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

            return this.View(functionalArea);
        }

        /// <summary>
        /// Vista para editar registros del catálogo FunctionalArea
        /// </summary>
        /// <param name="id">Identificador del item del catálogo a editar</param>
        /// <returns>Regresa la vista Edit cargada con el item del modelo a editar</returns>
        [CustomAuthorize(Roles = "FUNCAREA-UPD")]
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

                FunctionalAreaVO functionalAreaVo = Mapper.Map<FunctionalAreaDto, FunctionalAreaVO>(functionalAreaBusiness.FindFunctionalAreaById(id));
                if (functionalAreaVo == null)
                    return HttpNotFound();

                return this.View(functionalAreaVo);
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
        /// Vista para editar registro del catálogo FunctionalArea POST
        /// </summary>
        /// <param name="functionalArea">Objecto de tipo cuenta contable que toma la información del formulario</param>
        /// <returns>Regresa a la vista principal</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "FUNCAREA-UPD")]
        public ActionResult Edit(FunctionalAreaVO functionalArea)
        {
            if (functionalArea == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    functionalAreaBusiness.UpdateFunctionalArea(Mapper.Map<FunctionalAreaVO, FunctionalAreaDto>(functionalArea));
                    this.TempData["OperationSuccess"] = Resources.Resource.SuccessEdit;

                    return RedirectToAction("Index");
                }

                return View(functionalArea);
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
        /// Vista Delete
        /// </summary>
        /// <param name="id">Identificador del item del modelo a eliminar</param>
        /// <returns>Regresa la vista Delete precargada con la información a eliminar</returns>
        [CustomAuthorize(Roles = "FUNCAREA-DEL")]
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

                FunctionalAreaVO functionalAreaVo = Mapper.Map<FunctionalAreaDto, FunctionalAreaVO>(functionalAreaBusiness.FindFunctionalAreaById(id));
                if (functionalAreaVo == null)
                    return HttpNotFound();

                return View(functionalAreaVo);
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
        [CustomAuthorize(Roles = "FUNCAREA-DEL")]
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
                FunctionalAreaDto functionalArea = functionalAreaBusiness.FindFunctionalAreaById(id);
                functionalAreaBusiness.DeleteFunctionalArea(functionalArea);
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