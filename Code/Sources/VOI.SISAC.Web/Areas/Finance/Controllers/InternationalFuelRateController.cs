//-----------------------------------------------------------------------------
// <copyright file="InternationalFuelRateController.cs" company="AACOSTA">
//     Copyright(c) AACOSTA - Todos los derechos reservados.
// </copyright>
//----------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Finance.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Finance;
    using VOI.SISAC.Web.Resources;
    using Business = VOI.SISAC.Business.Finance;
    using System.Linq;
    using System.Web;
    using FileHelpers;
    using System.IO;
    using Models.Files;
    using System.Text;
    using MvcSiteMapProvider;

    /// <summary>
    /// International Ful Rate Controller
    /// </summary>
    [CustomAuthorize]
    public class InternationalFuelRateController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(InternationalFuelRateController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = "INTERNATIONAL FUEL RATE";

        /// <summary>
        /// Interface Implementación of International Fuel Rate
        /// </summary>
        private readonly Business.IInternationalFuelRateBusiness internationalFuelRateBusiness;

        /// <summary>
        /// Interfaz privada del negocio solo de lectura
        /// </summary>        
        private readonly Business.IInternationalFuelContractBusiness internationalFuelContractBusiness;

        /// <summary>
        ///  Initializes a new instance of the <see cref="InternationalFuelRateController"/> class.
        /// </summary>
        /// <param name="internationalFuelRateBusiness"></param>
        /// <param name="internationalFuelContractBusiness"></param>
        public InternationalFuelRateController(Business.IInternationalFuelRateBusiness internationalFuelRateBusiness, Business.IInternationalFuelContractBusiness internationalFuelContractBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.internationalFuelRateBusiness = internationalFuelRateBusiness;
            this.internationalFuelContractBusiness = internationalFuelContractBusiness;
        }

        /// <summary>
        /// Principal View
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="entity">Identificador del item del modelo a eliminar</param>
        /// <param name="search">search</param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "IFUELRATE-IDX")]
        public ActionResult Index(string effectiveDate, InternationalFuelContractVO entity, string search)
        {
            System.Diagnostics.Trace.TraceInformation("Beginning InternationalFuelRate");
            InternationalFuelContractConceptVO internationalFuelContractConceptVO = new InternationalFuelContractConceptVO();
            IList<InternationalFuelRateVO> internationalFuelRateVO = new List<InternationalFuelRateVO>();
            IList<InternationalFuelRateDto> IntFuelRateDto = new List<InternationalFuelRateDto>();
            InternationalFuelContractDto contract = new InternationalFuelContractDto();
            InternationalFuelContractVO contractVO = new InternationalFuelContractVO();
                
            try
            {
                DateTime date;
                if (DateTime.TryParse(effectiveDate, out date))
                {
                    entity.EffectiveDate = date;
                }

                contract = this.internationalFuelContractBusiness.FindInternationalFuelContractById(Mapper.Map<InternationalFuelContractDto>(entity));
                contractVO = (Mapper.Map<InternationalFuelContractVO>(contract));
                ViewBag.search = search;
                if (contractVO != null && contractVO.InternationalFuelContractConcepts != null)
                {
                    foreach (var item in contractVO.InternationalFuelContractConcepts)
                    {
                        if (item.InternationalFuelContract != null)
                        {
                            internationalFuelContractConceptVO = item;

                            if (internationalFuelContractConceptVO.InternationalFuelRate.Count > 0)
                            {
                                DateTime maxFecha = internationalFuelContractConceptVO.InternationalFuelRate.Max(e => e.RateStartDate);
                                Int64 internationalfuelrateId = internationalFuelContractConceptVO.InternationalFuelRate.Where(e => e.RateStartDate == maxFecha).Select(e => e.InternationalFuelRateID).FirstOrDefault();
                                IntFuelRateDto.Add(this.internationalFuelRateBusiness.FindInternationalFuelRateById(internationalfuelrateId));
                            }
                        }
                    }
                    internationalFuelRateVO = Mapper.Map<IList<InternationalFuelRateDto>, IList<InternationalFuelRateVO>>(IntFuelRateDto);
                    foreach (var item in contractVO.InternationalFuelContractConcepts)
                    {
                        item.InternationalFuelRate = internationalFuelRateVO;
                    }
                }


                if (contractVO == null)
                {
                    Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                    Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                
            }

            if (contractVO != null)
                contractVO.EffectiveDate.ToLocalTime();

            return this.View(contractVO);
        }

        /// <summary>
        /// Search Rates in base of Dates Parameters
        /// </summary>
        /// <param name="RateStartDate"></param>
        /// <param name="RateEndDate"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(string RateStartDate, string RateEndDate, InternationalFuelContractVO entity)
        {
            IList<InternationalFuelRateVO> internationalFuelRateVO = new List<InternationalFuelRateVO>();
            InternationalFuelContractConceptVO internationalFuelContractConceptVO = new InternationalFuelContractConceptVO();
            IList<InternationalFuelRateDto> IntFuelRateDto = new List<InternationalFuelRateDto>();
            InternationalFuelContractDto contract = new InternationalFuelContractDto();
            InternationalFuelContractVO contractVO = new InternationalFuelContractVO();
            contract = this.internationalFuelContractBusiness.FindInternationalFuelContractById(Mapper.Map<InternationalFuelContractDto>(entity));
            contractVO = (Mapper.Map<InternationalFuelContractVO>(contract));
            try
            {
                //Set MapSite Values
                if (contractVO != null)
                {
                    setSiteMapValues(contractVO);
                }

                if (contractVO.InternationalFuelContractConcepts != null)
                {
                    foreach (var item in contractVO.InternationalFuelContractConcepts)
                    {
                        if (item.InternationalFuelContract != null)
                        {
                            internationalFuelContractConceptVO = item;

                            if (internationalFuelContractConceptVO.InternationalFuelRate.Count > 0)
                            {
                                foreach (var itemRates in internationalFuelContractConceptVO.InternationalFuelRate)
                                {
                                    if (!string.IsNullOrWhiteSpace(RateStartDate) && !string.IsNullOrWhiteSpace(RateEndDate))
                                    {
                                        DateTime InitialDate = Convert.ToDateTime(RateStartDate);
                                        DateTime EndDate = Convert.ToDateTime(RateEndDate);
                                        IList<Int64> internationalfuelrateidList = internationalFuelContractConceptVO.InternationalFuelRate.Where(e => (e.RateStartDate >= InitialDate)
                                                                                                                                    && (e.RateEndDate <= EndDate)
                                                                                                                                    && (e.InternationalFuelRateID == itemRates.InternationalFuelRateID)).Select(e => e.InternationalFuelRateID).ToList();
                                        foreach (Int64 item2 in internationalfuelrateidList)
                                        {
                                            IntFuelRateDto.Add(this.internationalFuelRateBusiness.FindInternationalFuelRateById(item2));
                                        }
                                    }
                                }

                            }
                        }
                    }
                    internationalFuelRateVO = Mapper.Map<IList<InternationalFuelRateDto>, IList<InternationalFuelRateVO>>(IntFuelRateDto);

                    foreach (var item in contractVO.InternationalFuelContractConcepts)
                    {
                        item.InternationalFuelRate = internationalFuelRateVO;
                    }
                }
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
            return this.View("Index", contractVO);
        }

        /// <summary>
        /// Delete all rates in base a Dates Parameters
        /// </summary>
        /// <param name="RateStartDate"></param>
        /// <param name="RateEndDate"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "IFUELRATE-DEL")]
        public ActionResult DeleteAll(string RateStartDate, string RateEndDate, InternationalFuelContractVO entity)
        {
            IList<InternationalFuelRateVO> internationalFuelRateVO = new List<InternationalFuelRateVO>();
            InternationalFuelContractConceptVO internationalFuelContractConceptVO = new InternationalFuelContractConceptVO();
            IList<InternationalFuelRateDto> IntFuelRateDto = new List<InternationalFuelRateDto>();
            InternationalFuelContractDto contract = new InternationalFuelContractDto();
            InternationalFuelContractVO contractVO = new InternationalFuelContractVO();
            contract = this.internationalFuelContractBusiness.FindInternationalFuelContractById(Mapper.Map<InternationalFuelContractDto>(entity));
            contractVO = (Mapper.Map<InternationalFuelContractVO>(contract));

            try
            {
                //Set MapSite Values
                if (contractVO != null)
                {
                    setSiteMapValues(contractVO);
                }

                if (contractVO.InternationalFuelContractConcepts != null)
                {
                    foreach (var item in contractVO.InternationalFuelContractConcepts)
                    {
                        if (item.InternationalFuelContract != null)
                        {
                            internationalFuelContractConceptVO = item;

                            if (internationalFuelContractConceptVO.InternationalFuelRate.Count > 0)
                            {
                                foreach (var itemRates in internationalFuelContractConceptVO.InternationalFuelRate)
                                {
                                    if (!string.IsNullOrWhiteSpace(RateStartDate) && !string.IsNullOrWhiteSpace(RateEndDate))
                                    {
                                        DateTime InitialDate = Convert.ToDateTime(RateStartDate);
                                        DateTime EndDate = Convert.ToDateTime(RateEndDate);
                                        IList<Int64> internationalfuelrateidList = internationalFuelContractConceptVO.InternationalFuelRate.Where(e => (e.RateStartDate >= InitialDate)
                                                                                                                                  && (e.RateEndDate <= EndDate)
                                                                                                                                  && (e.InternationalFuelRateID == itemRates.InternationalFuelRateID)).Select(e => e.InternationalFuelRateID).ToList();
                                        foreach (Int64 item2 in internationalfuelrateidList)
                                        {
                                            this.internationalFuelRateBusiness.DeleteInternationalFuelRate(this.internationalFuelRateBusiness.FindInternationalFuelRateById(item2));
                                        }
                                        this.TempData["OperationSuccess"] = Resource.SuccessDelete;
                                    }
                                }
                            }
                        }
                    }
                }

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
            contract = this.internationalFuelContractBusiness.FindInternationalFuelContractById(Mapper.Map<InternationalFuelContractDto>(entity));
            contractVO = (Mapper.Map<InternationalFuelContractVO>(contract));
            return this.View("Index", contractVO);
        }

        /// <summary>
        /// Edit a International Fuel Rate
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "IFUELRATE-UPD")]
        public ActionResult Edit(long id)
        {
            if (id == 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InternationalFuelRateDto internationalFuelRateDto = new InternationalFuelRateDto();
            InternationalFuelRateVO intFuelRateVO = new InternationalFuelRateVO();
            try
            {
                internationalFuelRateDto = this.internationalFuelRateBusiness.FindInternationalFuelRateById(id);
                intFuelRateVO = Mapper.Map<InternationalFuelRateDto, InternationalFuelRateVO>(internationalFuelRateDto);
                if (intFuelRateVO == null)
                {
                    return this.HttpNotFound();
                }
                setSiteMapValues(intFuelRateVO);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
            }
            return this.View(intFuelRateVO);
        }

        /// <summary>
        /// Set Site Map Values: EffectiveDate, AirlineCode, StationCode, ServiceCode, ProviderNumberPrimary
        /// </summary>
        /// <param name="intFuelRateVO"></param>
        private static void setSiteMapValues(InternationalFuelRateVO intFuelRateVO)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Fuel_Rates");
                if (node != null)
                {
                    if (intFuelRateVO.InternationalFuelContractConcept != null)
                    {
                        node.RouteValues["EffectiveDate"] = intFuelRateVO.InternationalFuelContractConcept.EffectiveDate;
                        node.RouteValues["AirlineCode"] = intFuelRateVO.InternationalFuelContractConcept.AirlineCode;
                        node.RouteValues["StationCode"] = intFuelRateVO.InternationalFuelContractConcept.StationCode;
                        node.RouteValues["ServiceCode"] = intFuelRateVO.InternationalFuelContractConcept.ServiceCode;
                        node.RouteValues["ProviderNumberPrimary"] = intFuelRateVO.InternationalFuelContractConcept.ProviderNumberPrimary;
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
        /// Set Site Map Values: EffectiveDate, AirlineCode, StationCode, ServiceCode, ProviderNumberPrimary
        /// </summary>
        /// <param name="intFuelRateVO"></param>
        private static void setSiteMapValues(InternationalFuelContractVO intFuelRateVO)
        {
            try
            {
                var node = SiteMaps.Current.FindSiteMapNodeFromKey("Fuel_Rates");
                if (node != null)
                {
                    if (intFuelRateVO != null)
                    {
                        node.RouteValues["EffectiveDate"] = intFuelRateVO.EffectiveDate;
                        node.RouteValues["AirlineCode"] = intFuelRateVO.AirlineCode;
                        node.RouteValues["StationCode"] = intFuelRateVO.StationCode;
                        node.RouteValues["ServiceCode"] = intFuelRateVO.ServiceCode;
                        node.RouteValues["ProviderNumberPrimary"] = intFuelRateVO.ProviderNumberPrimary;
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
        /// 
        /// </summary>
        /// <param name="internationalFuelRateVO"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "IFUELRATE-UPD")]
        public ActionResult Edit(InternationalFuelRateVO internationalFuelRateVO)
        {
            if (internationalFuelRateVO == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                InternationalFuelRateDto internationalFuelRateDto = new InternationalFuelRateDto();
                internationalFuelRateDto = Mapper.Map<InternationalFuelRateVO, InternationalFuelRateDto>(internationalFuelRateVO);
                this.internationalFuelRateBusiness.UpdateInternationalFuelRate(internationalFuelRateDto);
                this.TempData["OperationSuccess"] = Resource.SuccessEdit;

                return this.RedirectToAction("Index", new
                {
                    effectiveDate = internationalFuelRateVO.InternationalFuelContractConcept.EffectiveDate,
                    airlineCode = internationalFuelRateVO.InternationalFuelContractConcept.AirlineCode,
                    stationCode = internationalFuelRateVO.InternationalFuelContractConcept.StationCode,
                    serviceCode = internationalFuelRateVO.InternationalFuelContractConcept.ServiceCode,
                    providerNumberPrimary = internationalFuelRateVO.InternationalFuelContractConcept.ProviderNumberPrimary
                });
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.catalogName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(internationalFuelRateVO);
        }

        /// <summary>
        /// Action for delete a rate individually
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorize(Roles = "IFUELRATE-DEL")]
        public ActionResult Delete(long id)
        {
            if (id == 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "INTERNATIONALFUELRATE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "INTERNATIONALFUELRATE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InternationalFuelRateDto internationalFuelRateDto = new InternationalFuelRateDto();
            InternationalFuelRateVO internationalFuelRateVO = new InternationalFuelRateVO();
            try
            {
                internationalFuelRateDto = this.internationalFuelRateBusiness.FindInternationalFuelRateById(id);
                internationalFuelRateVO = Mapper.Map<InternationalFuelRateDto, InternationalFuelRateVO>(internationalFuelRateDto);
                if (internationalFuelRateVO == null)
                {
                    return this.HttpNotFound();
                }
                setSiteMapValues(internationalFuelRateVO);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, "INTERNATIONALFUELRATE", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, "INTERNATIONALFUELRATE", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(internationalFuelRateVO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "IFUELRATE-DEL")]
        public ActionResult DeleteConfirmed(long id)
        {
            if (id == 0)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, "INTERNATIONALFUELRATE"));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, "INTERNATIONALFUELRATE"));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InternationalFuelRateDto internationalFuelRateDto = new InternationalFuelRateDto();
            InternationalFuelRateVO internationalFuelRateVO = new InternationalFuelRateVO();

            try
            {
                internationalFuelRateDto = this.internationalFuelRateBusiness.FindInternationalFuelRateById(id);
                this.internationalFuelRateBusiness.DeleteInternationalFuelRate(internationalFuelRateDto);
                internationalFuelRateVO = Mapper.Map<InternationalFuelRateDto, InternationalFuelRateVO>(internationalFuelRateDto);
                this.TempData["OperationSuccess"] = VOI.SISAC.Web.Resources.Resource.SuccessDelete;
                return this.RedirectToAction("Index", new
                {
                    effectiveDate = internationalFuelRateVO.InternationalFuelContractConcept.EffectiveDate,
                    airlineCode = internationalFuelRateVO.InternationalFuelContractConcept.AirlineCode,
                    stationCode = internationalFuelRateVO.InternationalFuelContractConcept.StationCode,
                    serviceCode = internationalFuelRateVO.InternationalFuelContractConcept.ServiceCode,
                    providerNumberPrimary = internationalFuelRateVO.InternationalFuelContractConcept.ProviderNumberPrimary
                });
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, "INTERNATIONALFUELRATE", this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, "INTERNATIONALFUELRATE", this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return this.View(internationalFuelRateVO);
        }

    }
}