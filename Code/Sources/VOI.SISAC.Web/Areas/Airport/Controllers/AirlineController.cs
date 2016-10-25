//------------------------------------------------------------------------
// <copyright file="AirlineController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Airport.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Class Controller Airline
    /// </summary>
    [CustomAuthorize]
    public class AirlineController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(AirlineController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resource.AirlineTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = Resource.AirlineCode;

        /// <summary>
        /// The airline business
        /// </summary>
        private readonly IAirlineBusiness airlineBusiness;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AirlineController"/> class.
        /// </summary>
        /// <param name="airlineBusiness">The airline business.</param>
        public AirlineController(IAirlineBusiness airlineBusiness)
        {
            this.userInfo = string.Format(
               LogMessages.UserInfo,
               System.Environment.UserDomainName,
               System.Environment.UserName,
               System.Environment.MachineName);
            this.airlineBusiness = airlineBusiness;
        }
        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>View of Get Airlines Actives</returns>
        [CustomAuthorize(Roles = "AIRLINE-IDX")]
        public ActionResult Index()
        {
            Trace.TraceInformation("Iniciando Index Aerolinea");
            IList<AirlineDto> airlinesDto = new List<AirlineDto>();
            IList<AirlineVO> airlinesVo = new List<AirlineVO>();
            try
            {
                Trace.TraceInformation("Antes de Consulta en BD Aerolinea");
                airlinesDto = this.airlineBusiness.GetActivesAirline();
                airlinesVo = Mapper.Map<IList<AirlineDto>, IList<AirlineVO>>(airlinesDto);
                Trace.TraceInformation("Despues de Consulta en BD Aerolinea");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(airlinesVo);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>View Airlines</returns>
        [CustomAuthorize(Roles = "AIRLINE-ADD")]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Creates the specified airline.
        /// </summary>
        /// <param name="airlineVo">The airline.</param>
        /// <returns>View AirlineVO</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRLINE-ADD")]
        public ActionResult Create(AirlineVO airlineVo)
        {
            AirlineDto airlineDto = new AirlineDto();

            if (airlineVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    airlineDto = Mapper.Map<AirlineVO, AirlineDto>(airlineVo);
                    this.airlineBusiness.AddAirline(airlineDto);
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

            return this.View(airlineVo);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View AirlineVO</returns>
        [CustomAuthorize(Roles = "AIRLINE-UPD")]
        public ActionResult Edit(string id)
        {
            AirlineVO airlineVo = new AirlineVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                AirlineDto airlineDto = this.airlineBusiness.FindAirlineById(id);
                airlineVo = Mapper.Map<AirlineDto, AirlineVO>(airlineDto);

                if (airlineDto == null)
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

            return this.View(airlineVo);
        }

        /// <summary>
        /// Edits the specified airline.
        /// </summary>
        /// <param name="airlineVo">The airline.</param>
        /// <returns>View AirlineVO</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRLINE-UPD")]
        public ActionResult Edit(AirlineVO airlineVo)
        {
            AirlineDto airlineDto = new AirlineDto();
            if (airlineVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    airlineDto = Mapper.Map<AirlineVO, AirlineDto>(airlineVo);
                    this.airlineBusiness.UpdateAirline(airlineDto);
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

            return this.View(airlineVo);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View airlineVO</returns>
        [CustomAuthorize(Roles = "AIRLINE-DEL")]
        public ActionResult Delete(string id)
        {
            AirlineVO airlineVo = new AirlineVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                AirlineDto airlineDto = this.airlineBusiness.FindAirlineById(id);
                airlineVo = Mapper.Map<AirlineDto, AirlineVO>(airlineDto);
                if (airlineDto == null)
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

            return this.View(airlineVo);
        }

        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View Airlines</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "AIRLINE-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            AirlineVO airlineVO = new AirlineVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                AirlineDto airlineDto = this.airlineBusiness.FindAirlineById(id);
                airlineVO = Mapper.Map<AirlineDto, AirlineVO>(airlineDto);
                this.airlineBusiness.DeleteAirline(airlineDto);
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

            return this.View(airlineVO);
        }
    }
}