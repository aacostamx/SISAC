//------------------------------------------------------------------------
// <copyright file="CountryController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Catalog.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.Catalog;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Catalog;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Class controller of Country
    /// </summary>
    [CustomAuthorize]
    public class CountryController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(CountryController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = VOI.SISAC.Web.Resources.Resource.CountryTitle;

        /// <summary>
        /// The primary key
        /// </summary>
        private readonly string primaryKey = VOI.SISAC.Web.Resources.Resource.CountryCode;

        /// <summary>
        /// The country business
        /// </summary>
        private readonly ICountryBusiness countryBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryController"/> class.
        /// </summary>
        /// <param name="countryBusiness">The country business.</param>
        public CountryController(ICountryBusiness countryBusiness)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.countryBusiness = countryBusiness;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>View Country</returns>
        [CustomAuthorize(Roles = "COUNTRY-IDX")]
        public ActionResult Index()
        {
            IList<CountryDto> countriesDto = new List<CountryDto>();
            IList<CountryVO> countriesVo = new List<CountryVO>();
            try
            {
                countriesDto = this.countryBusiness.GetActivesCountry();
                countriesVo = Mapper.Map<IList<CountryDto>, IList<CountryVO>>(countriesDto);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(countriesVo);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>View Country</returns>
        [CustomAuthorize(Roles = "COUNTRY-ADD")]
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// Creates the specified country.
        /// </summary>
        /// <param name="countryVo">The country.</param>
        /// <returns>View CountryVo </returns>
        [CustomAuthorize(Roles = "COUNTRY-ADD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryVO countryVo)
        {
            CountryDto countryDto = new CountryDto();

            if (countryVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    countryDto = Mapper.Map<CountryVO, CountryDto>(countryVo);
                    this.countryBusiness.AddCountry(countryDto);
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

            return this.View(countryVo);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View Country </returns>
        [CustomAuthorize(Roles = "COUNTRY-UPD")]
        public ActionResult Edit(string id)
        {
            CountryVO countryVo = new CountryVO();
            
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.catalogName, this.userInfo));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                CountryDto countryDto = this.countryBusiness.FindCountryById(id);
                countryVo = Mapper.Map<CountryDto, CountryVO>(countryDto);
                if (countryDto == null)
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

            return this.View(countryVo);
        }

        /// <summary>
        /// Edits the specified country.
        /// </summary>
        /// <param name="countryVo">The country.</param>
        /// <returns>View CountryVo </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "COUNTRY-UPD")]
        public ActionResult Edit(CountryVO countryVo)
        {
            CountryDto countryDto = new CountryDto();
            if (countryVo == null)
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    countryDto = Mapper.Map<CountryVO, CountryDto>(countryVo);
                    this.countryBusiness.UpdateCountry(countryDto);
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

            return this.View(countryVo);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View Country </returns>
        [CustomAuthorize(Roles = "COUNTRY-DEL")]
        public ActionResult Delete(string id)
        {
            CountryVO countryVo = new CountryVO();
            if (string.IsNullOrEmpty(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                CountryDto countryDto = this.countryBusiness.FindCountryById(id);
                countryVo = Mapper.Map<CountryDto, CountryVO>(countryDto);
                if (countryDto == null)
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
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(countryVo);
        }

        /// <summary>
        /// Deletes the confirmed.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View Country </returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "COUNTRY-DEL")]
        public ActionResult DeleteConfirmed(string id)
        {
            CountryVO countryVO = new CountryVO();
            if (string.IsNullOrWhiteSpace(id))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.catalogName));
                Trace.TraceError(string.Format(LogMessages.ErrorNullObject, this.catalogName));

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                CountryDto countryDto = this.countryBusiness.FindCountryById(id);
                countryVO = Mapper.Map<CountryDto, CountryVO>(countryDto);
                this.countryBusiness.DeleteCountry(countryDto);
                this.TempData["OperationSuccess"] = Resources.Resource.SuccessDelete;

                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorDelete, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
            }

            return this.View(countryVO);
        }
    }
}