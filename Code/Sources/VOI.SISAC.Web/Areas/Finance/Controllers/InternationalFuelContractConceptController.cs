//------------------------------------------------------------------------
// <copyright file="InternationalFuelContractConceptController.cs" company="AACOSTA">
//     Copyright(c) AACOSTA - Todos los derechos reservados.
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
    using MvcSiteMapProvider;
    using Resources;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using Web.Controllers;

    /// <summary>
    /// International Fuel Contract Concept Controller
    /// </summary>
    public class InternationalFuelContractConceptController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(InternationalFuelContractConceptController));

        /// <summary>
        /// Interface International Fuel 
        /// </summary>
        private readonly IInternationalFuelContractConceptBusiness conceptBusiness;

        private readonly IInternationalFuelContractBusiness contractBusiness;

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
        /// <param name="conceptBusiness"></param>
        /// <param name="comboBoxes"></param>
        public InternationalFuelContractConceptController(IInternationalFuelContractConceptBusiness conceptBusiness, IGenericCatalogBusiness comboBoxes, IInternationalFuelContractBusiness contractBusiness)
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
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InternationalFuelContractConceptDto conceptDto = new InternationalFuelContractConceptDto();
            InternationalFuelContractConceptVO conceptVO = new InternationalFuelContractConceptVO();
            this.LoadCombobox();

            try
            {
                conceptDto.InternationalFuelContractConceptID = id;
                conceptDto = conceptBusiness.FindFuelConceptById(conceptDto);

                if (conceptDto == null)
                {
                    return this.HttpNotFound();
                }

                conceptVO = Mapper.Map<InternationalFuelContractConceptVO>(conceptDto);
                setSiteMapValues(conceptVO);
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
        /// <param name="conceptVO"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InternationalFuelContractConceptVO conceptVO)
        {
            if (conceptVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InternationalFuelContractConceptDto dto = new InternationalFuelContractConceptDto();
            try
            {
                dto = Mapper.Map<InternationalFuelContractConceptDto>(conceptVO);
                dto.InternationalFuelContract = null;
                dto.FuelConcept = null;
                conceptBusiness.UpdateFuelConcept(dto);
                this.TempData["OperationSuccess"] = Resource.SuccessEdit;
                return this.RedirectToAction("Edit", "InternationalFuelContract", new { effectiveDate = conceptVO.InternationalFuelContract.EffectiveDate.ToShortDateString(), airlineCode = conceptVO.InternationalFuelContract.AirlineCode, stationCode = conceptVO.InternationalFuelContract.StationCode, serviceCode = conceptVO.InternationalFuelContract.ServiceCode, providerNumberPrimary = conceptVO.InternationalFuelContract.ProviderNumberPrimary, AircraftRegistCCFlag = conceptVO.InternationalFuelContract.AircraftRegistCCFlag });

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
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InternationalFuelContractConceptDto conceptDto = new InternationalFuelContractConceptDto();
            InternationalFuelContractConceptVO conceptVO = new InternationalFuelContractConceptVO();

            try
            {
                conceptDto.InternationalFuelContractConceptID = id;
                conceptDto = conceptBusiness.FindFuelConceptById(conceptDto);

                if (conceptDto == null)
                {
                    return this.HttpNotFound();
                }

                conceptVO = Mapper.Map<InternationalFuelContractConceptVO>(conceptDto);
                setSiteMapValues(conceptVO);
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
        /// DeleteConfirmed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id < 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InternationalFuelContractConceptDto conceptDto = new InternationalFuelContractConceptDto();
            InternationalFuelContractConceptVO conceptVO = new InternationalFuelContractConceptVO();
            try
            {
                conceptDto.InternationalFuelContractConceptID = id;
                conceptDto = conceptBusiness.FindFuelConceptById(conceptDto);

                if (conceptDto == null)
                {
                    return this.HttpNotFound();
                }
                conceptVO = Mapper.Map<InternationalFuelContractConceptVO>(conceptDto);
                this.conceptBusiness.DeleteFuelConcept(conceptDto);
                this.TempData["OperationSuccess"] = Resource.SuccessDelete;

                return this.RedirectToAction("Edit", "InternationalFuelContract", new { effectiveDate = conceptVO.InternationalFuelContract.EffectiveDate.ToShortDateString(), airlineCode = conceptVO.InternationalFuelContract.AirlineCode, stationCode = conceptVO.InternationalFuelContract.StationCode, serviceCode = conceptVO.InternationalFuelContract.ServiceCode, providerNumberPrimary = conceptVO.InternationalFuelContract.ProviderNumberPrimary, AircraftRegistCCFlag = conceptVO.InternationalFuelContract.AircraftRegistCCFlag });
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
        /// Load comboboxes
        /// </summary>
        private void LoadCombobox()
        {
            this.ViewBag.FuelConcept = this.comboBoxes.GetFuelConceptCatalog();
            this.ViewBag.Provider = this.comboBoxes.GetProviderCatalog();
            this.ViewBag.FuelConceptType = this.comboBoxes.GetFuelConceptTypeCatalog();
            this.ViewBag.ChargeFactorType = this.comboBoxes.GetChargeFactorTypeCatalog();
        }

        /// <summary>
        /// Set Site Map Values: EffectiveDate, AirlineCode, StationCode, ServiceCode, ProviderNumberPrimary
        /// </summary>
        /// <param name="conceptVO"></param>
        private static void setSiteMapValues(InternationalFuelContractConceptVO conceptVO)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Fuel_Contracts");
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
    }
}