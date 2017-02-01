//------------------------------------------------------------------------
// <copyright file="CostCenterController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Airport;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Finance;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Airport;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Controller Cost Center
    /// </summary>
    [CustomAuthorize]
    public class CostCenterController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(CostCenterController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.CostCenterTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = VOI.SISAC.Web.Resources.Resource.CCNumber;

        /// <summary>
        /// The cost center business
        /// </summary>
        private readonly ICostCenterBusiness costCenterBusiness;

        /// <summary>
        /// The airline business
        /// </summary>
        private readonly IAirlineBusiness airlineBusiness;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CostCenterController"/> class.
        /// </summary>
        /// <param name="costCenterBusiness">The cost center business.</param>
        /// <param name="airlineBusiness">The airline business.</param>
        public CostCenterController(ICostCenterBusiness costCenterBusiness, IAirlineBusiness airlineBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.costCenterBusiness = costCenterBusiness;
            this.airlineBusiness = airlineBusiness;
        }
        #endregion

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>View CostCenter Actives</returns>
        [CustomAuthorize(Roles = "COSTCENTER-IDX")]
        public ActionResult Index()
        {
            Trace.TraceInformation("Iniciando Index de Centro de Costos");
            IList<CostCenterVO> costCentersVo = new List<CostCenterVO>();

            try
            {
                Trace.TraceInformation("Ida a la base de datos de Centro de Costos");
                IList<CostCenterDto> costCentersDto = this.costCenterBusiness.GetActivesCostCenter();
                Trace.TraceInformation("Ida al Mapeo de Dto a VO de Centro de Costos");
                costCentersVo = Mapper.Map<IList<CostCenterDto>, IList<CostCenterVO>>(costCentersDto);
                Trace.TraceInformation("Regreso del Mapeo de Dto a VO de Centro de Costos");
                Trace.TraceInformation("Regreso de la base de datos de Centro de Costos. Ultimo");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(costCentersVo);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>View CostCenterViewModel</returns>
        [CustomAuthorize(Roles = "COSTCENTER-ADD")]
        public ActionResult Create()
        {
            CostCenterModelVO costCenterViewModel = new CostCenterModelVO();
            costCenterViewModel.Airlines = new List<AirlineVO>();

            try
            {
                if (ModelState.IsValid)
                {
                    IList<AirlineDto> airlineDto = this.airlineBusiness.GetActivesAirline();
                    costCenterViewModel.Airlines = Mapper.Map<IList<AirlineDto>, IList<AirlineVO>>(airlineDto);
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
            
            return this.View(costCenterViewModel);
        }

        /// <summary>
        /// Creates the specified cost center.
        /// </summary>
        /// <param name="costCenter">The cost center.</param>
        /// <returns> View CostCenterViewModel</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "COSTCENTER-ADD")]
        public ActionResult Create(CostCenterModelVO costCenter)
        {
            if (costCenter == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    AirlineDto airlineDto = this.airlineBusiness.FindAirlineById(costCenter.CostCenterVO.AirlineCode);
                    costCenter.CostCenterVO.CCNumber = airlineDto.AirlineShortName + "-" + costCenter.CostCenterVO.CCNumber;
                    CostCenterDto costCenterDto = Mapper.Map<CostCenterVO, CostCenterDto>(costCenter.CostCenterVO);
                    IList<AirlineDto> airlinesDto = this.airlineBusiness.GetActivesAirline();
                    costCenter.Airlines = Mapper.Map<IList<AirlineDto>, IList<AirlineVO>>(airlinesDto);
                    this.costCenterBusiness.AddCostCenter(costCenterDto);
                    this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessCreate;

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

            return this.View(costCenter);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View CostCenterViewModel</returns>
        [CustomAuthorize(Roles = "COSTCENTER-UPD")]
        public ActionResult Edit(string id)
        {
            CostCenterModelVO costCenterViewModel = new CostCenterModelVO();

            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                CostCenterDto costCenterDto = this.costCenterBusiness.FindCostCenteryById(id);
                if (costCenterDto == null)
                {
                    return this.HttpNotFound();
                }

                costCenterViewModel.CostCenterVO = Mapper.Map<CostCenterDto, CostCenterVO>(costCenterDto);
                IList<AirlineDto> airlinesDto = this.airlineBusiness.GetActivesAirline();
                costCenterViewModel.Airlines = Mapper.Map<IList<AirlineDto>, IList<AirlineVO>>(airlinesDto);
                AirlineDto airlineDto = this.airlineBusiness.FindAirlineById(costCenterViewModel.CostCenterVO.AirlineCode);
               
                if (airlineDto == null)
                {
                    return this.HttpNotFound();
                }

                costCenterViewModel.AirlineVo = Mapper.Map<AirlineDto, AirlineVO>(airlineDto);
                costCenterViewModel.AirLineName = costCenterViewModel.AirlineVo.AirlineCode + "-" + costCenterViewModel.AirlineVo.AirlineName;
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }

            return this.View(costCenterViewModel);
        }

        /// <summary>
        /// Edits the specified cost center.
        /// </summary>
        /// <param name="costCenter">The cost center.</param>
        /// <returns>View CostCenterViewModel</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "COSTCENTER-UPD")]
        public ActionResult Edit(CostCenterModelVO costCenter)
        {
            if (costCenter == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    CostCenterDto costCenterDto = Mapper.Map<CostCenterVO, CostCenterDto>(costCenter.CostCenterVO);
                    this.costCenterBusiness.UpdateCostCenter(costCenterDto);
                    this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessEdit;

                    return this.RedirectToAction("Index");
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(costCenter);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View CostCenterDto</returns>
        [CustomAuthorize(Roles = "COSTCENTER-DEL")]
        public ActionResult Delete(string id)
        {
            CostCenterModelVO costCenterViewModel = new CostCenterModelVO();

            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                CostCenterDto costCenterDto = this.costCenterBusiness.FindCostCenteryById(id);
                if (costCenterDto == null)
                {
                    return this.HttpNotFound();
                }

                costCenterViewModel.CostCenterVO = Mapper.Map<CostCenterDto, CostCenterVO>(costCenterDto);
                AirlineDto airlineDto = this.airlineBusiness.FindAirlineById(costCenterViewModel.CostCenterVO.AirlineCode);

                if (airlineDto == null)
                {
                    return this.HttpNotFound();
                }

                costCenterViewModel.AirlineVo = Mapper.Map<AirlineDto, AirlineVO>(airlineDto);
                costCenterViewModel.AirLineName = costCenterViewModel.AirlineVo.AirlineCode + "-" + costCenterViewModel.AirlineVo.AirlineName;                
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(costCenterViewModel);
        }

        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View CostCenter </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "COSTCENTER-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            CostCenterDto costCenter = new CostCenterDto();

            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                costCenter = this.costCenterBusiness.FindCostCenteryById(id);
                this.costCenterBusiness.DeleteCostCenter(costCenter);
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessDelete;
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

            return this.View(costCenter);
        }

        /// <summary>
        /// Gets the number airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <returns>Json Cost Center View Model</returns>
        public ActionResult GetNumberAirline(string airlineCode)
        {
            CostCenterModelVO costCenterViewModel = new CostCenterModelVO();

            try
            {
                if (string.IsNullOrEmpty(airlineCode))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    AirlineDto airlineDto = this.airlineBusiness.FindAirlineById(airlineCode);
                    costCenterViewModel.AirlineVo = Mapper.Map<AirlineDto, AirlineVO>(airlineDto);
                    return this.Json(costCenterViewModel.AirlineVo.AirlineShortName, JsonRequestBehavior.AllowGet);
                }
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(costCenterViewModel);
        }
    }
}