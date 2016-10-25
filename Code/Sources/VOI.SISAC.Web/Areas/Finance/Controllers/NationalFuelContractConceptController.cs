//------------------------------------------------------------------------
// <copyright file="NationalFuelContractConceptController.cs" company="Volaris">
//     Copyright(c) Volaris - Todos los derechos reservados.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using Business.Common;
    using Business.Dto.Finances;
    using Business.ExceptionBusiness;
    using Business.Finance;
    using Helpers;
    using Models.VO.Finance;
    using MvcSiteMapProvider;
    using Resources;
    using Web.Controllers;

    /// <summary>
    /// International Fuel Contract Concept Controller
    /// </summary>
    [CustomAuthorize]
    public class NationalFuelContractConceptController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NationalFuelContractConceptController));

        /// <summary>
        /// Interface International Fuel 
        /// </summary>
        private readonly INationalFuelContractConceptBusiness conceptBusiness;

        /// <summary>
        /// The contract business
        /// </summary>
        private readonly INationalFuelContractBusiness contractBusiness;

        /// <summary>
        /// IGenericCatalogBusiness
        /// </summary>
        private readonly IGenericCatalogBusiness comboBoxes;

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = Resources.Resource.FuelConceptTitle;

        /// <summary>
        /// InternationalFuelContractConceptController
        /// </summary>
        /// <param name="conceptBusiness">The concept business.</param>
        /// <param name="contractBusiness">The contract business.</param>
        /// <param name="comboBoxes">The combo boxes.</param>
        public NationalFuelContractConceptController(
            INationalFuelContractConceptBusiness conceptBusiness,
            INationalFuelContractBusiness contractBusiness,
            IGenericCatalogBusiness comboBoxes)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.conceptBusiness = conceptBusiness;
            this.contractBusiness = contractBusiness;
            this.comboBoxes = comboBoxes;
        }

        /// <summary>
        /// Edit Fuel Concept Action
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "NATCONCEPT-UPD")]
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelContractConceptDto conceptDto = new NationalFuelContractConceptDto();
            NationalFuelContractConceptVO conceptVO = new NationalFuelContractConceptVO();
            this.LoadCombobox();

            try
            {
                conceptDto.NationalFuelContractConceptId = id;
                conceptDto = conceptBusiness.FindFuelConceptById(conceptDto);

                if (conceptDto == null)
                {
                    return this.HttpNotFound();
                }

                conceptVO = Mapper.Map<NationalFuelContractConceptVO>(conceptDto);
                SetSiteMapValues(conceptVO);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
                this.LoadCombobox();

            }
            return this.View(conceptVO);
        }

        /// <summary>
        /// Edit post
        /// </summary>
        /// <param name="conceptVO">The concept.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NATCONCEPT-UPD")]
        public ActionResult Edit(NationalFuelContractConceptVO conceptVO)
        {
            if (conceptVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelContractConceptDto dto = new NationalFuelContractConceptDto();
            try
            {
                dto = Mapper.Map<NationalFuelContractConceptDto>(conceptVO);
                conceptBusiness.UpdateFuelConcept(dto);
                this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                return this.RedirectToAction("Edit", "NationalFuelContract", new { effectiveDate = conceptVO.EffectiveDate.ToShortDateString(), airlineCode = conceptVO.AirlineCode, stationCode = conceptVO.StationCode, serviceCode = conceptVO.ServiceCode, providerNumberPrimary = conceptVO.ProviderNumberPrimary });

            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                string message = FrontMessage.GetExceptionErrorMessage(ex.Number);
                message = ex.Number == 10 ? string.Format(message, Resource.FuelConceptID) : message;
                this.ViewBag.ErrorMessage = message;
                this.LoadCombobox();
            }
            return this.View(conceptVO);
        }

        /// <summary>
        /// Delete fuel concept
        /// </summary>
        /// <returns>Action result</returns>
        [CustomAuthorize(Roles = "NATCONCEPT-DEL")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelContractConceptDto conceptDto = new NationalFuelContractConceptDto();
            NationalFuelContractConceptVO conceptVO = new NationalFuelContractConceptVO();

            try
            {
                conceptDto.NationalFuelContractConceptId = id;
                conceptDto = conceptBusiness.FindFuelConceptById(conceptDto);

                if (conceptDto == null)
                {
                    return this.HttpNotFound();
                }

                conceptVO = Mapper.Map<NationalFuelContractConceptVO>(conceptDto);
                SetSiteMapValues(conceptVO);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View(conceptVO);
        }

        /// <summary>
        /// Delete Confirmed
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "NATCONCEPT-DEL")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id < 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            NationalFuelContractConceptDto conceptDto = new NationalFuelContractConceptDto();
            NationalFuelContractConceptVO conceptVO = new NationalFuelContractConceptVO();
            try
            {
                conceptDto.NationalFuelContractConceptId = id;
                conceptDto = conceptBusiness.FindFuelConceptById(conceptDto);

                if (conceptDto == null)
                {
                    return this.HttpNotFound();
                }
                conceptVO = Mapper.Map<NationalFuelContractConceptVO>(conceptDto);
                this.conceptBusiness.DeleteFuelConcept(conceptDto);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;

                return this.RedirectToAction("Edit", "NationalFuelContract", new { effectiveDate = conceptVO.EffectiveDate.ToShortDateString(), airlineCode = conceptVO.AirlineCode, stationCode = conceptVO.StationCode, serviceCode = conceptVO.ServiceCode, providerNumberPrimary = conceptVO.ProviderNumberPrimary });
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(conceptVO);
        }

        /// <summary>
        /// Set Site Map Values: EffectiveDate, AirlineCode, StationCode, ServiceCode, ProviderNumberPrimary
        /// </summary>
        /// <param name="conceptVO"></param>
        private static void SetSiteMapValues(NationalFuelContractConceptVO conceptVO)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Fuel_National_Contracts");
                if (node != null)
                {
                    if (conceptVO != null)
                    {
                        node.RouteValues["EffectiveDate"] = conceptVO.EffectiveDate.ToShortDateString();
                        node.RouteValues["AirlineCode"] = conceptVO.AirlineCode;
                        node.RouteValues["StationCode"] = conceptVO.StationCode;
                        node.RouteValues["ServiceCode"] = conceptVO.ServiceCode;
                        node.RouteValues["ProviderNumberPrimary"] = conceptVO.ProviderNumberPrimary;
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception.Message, exception);
                Trace.TraceError(exception.Message, exception);
            }
        }

        /// <summary>
        /// Load comboboxes
        /// </summary>
        private void LoadCombobox()
        {
            this.ViewBag.FuelConcept = this.comboBoxes.GetFuelConceptCatalog();
            this.ViewBag.Provider = this.comboBoxes.GetProviderCatalog();
            this.ViewBag.FuelConceptType = this.comboBoxes.GetFuelConceptTypeCatalog();
            this.ViewBag.ChargeFactorType = this.comboBoxes.GetChargeFactorTypeCatalog().Where(c => c.Id.Equals("3")).ToList();
        }
    }
}