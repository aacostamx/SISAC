//------------------------------------------------------------------------
// <copyright file="ConfirmNationalPeriodController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Process.Controllers
{    
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Catalog;
    using VOI.SISAC.Web.Resources;
    using VOI.SISAC.Business.Process;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Web.Models.VO.Process;

    /// <summary>
    /// Confirm National Period Controller
    /// </summary>
    [CustomAuthorize]
    public class ConfirmNationalPeriodController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(JetFuelLogErrorController));

        /// <summary>
        /// The controller name
        /// </summary>
        private readonly string controllerName;

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// The national jet fuel process business
        /// </summary>
        private readonly INationalJetFuelProcessBusiness nationalJetFuelProcess;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmNationalPeriodController" /> class.
        /// </summary>
        /// <param name="nationalJetFuelProcess">The national jet fuel process.</param>
        public ConfirmNationalPeriodController(INationalJetFuelProcessBusiness nationalJetFuelProcess)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);
            this.nationalJetFuelProcess = nationalJetFuelProcess;
        }

        /// <summary>
        /// Main view.
        /// </summary>
        /// <returns>Action result.</returns>
        [CustomAuthorize(Roles = "CONNATPER-IDX")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Closes the specified period code.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "CONNATPER-CLOSE")]
        public ActionResult Close(string periodCode)
        {
            if (string.IsNullOrWhiteSpace(periodCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.controllerName, this.userInfo));
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(13);
                return this.View("Index");
            }

            try
            {
                NationalJetFuelProcessDto process = new NationalJetFuelProcessDto
                {
                    PeriodCode = periodCode,
                    ConfirmedByUserName = this.User.Identity.Name,
                    ConfirmationDate = DateTime.Now
                };
                this.nationalJetFuelProcess.ClosePeriod(process);
                this.TempData["OperationSuccess"] = string.Format(Resource.PeriodCloseSuccess, periodCode);
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View("Index");
            }
        }

        /// <summary>
        /// Opens the specified period code.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "CONNATPER-OPEN")]
        public ActionResult Open(string periodCode)
        {
            if (string.IsNullOrWhiteSpace(periodCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.controllerName, this.userInfo));
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(13);
                return this.View("Index");
            }

            try
            {
                NationalJetFuelProcessDto process = new NationalJetFuelProcessDto
                {
                    PeriodCode = periodCode,
                    ConfirmedByUserName = this.User.Identity.Name,
                    ConfirmationDate = DateTime.Now
                };
                this.nationalJetFuelProcess.OpenPeriod(process);
                this.TempData["OperationSuccess"] = string.Format(Resource.OpenPeriodSuccessfully, periodCode);
                return this.RedirectToAction("Index");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorUpdate, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorUpdate, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View("Index");
            }
        }

        /// <summary>
        /// Gets the periods.
        /// </summary>
        /// <returns>Json result.</returns>
        public JsonResult GetPeriods()
        {
            List<PeriodVO> periods = this.GetAllPeriods();
            return this.Json(periods, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the periods.
        /// </summary>
        /// <returns>Json result.</returns>
        public JsonResult GetDates(string periodCode)
        {
            PeriodVO period;
            if (periodCode == null)
            {
                return this.Json(new PeriodVO(), JsonRequestBehavior.AllowGet);
            }

            period = this.GetDatesByPeriod(periodCode);
            return this.Json(period, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the periods.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// String with the message.
        /// </returns>
        public string ValidatePeriod(string periodCode)
        {
            if (string.IsNullOrWhiteSpace(periodCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.controllerName, this.userInfo));
                return "Error";
            }

            try
            {
                return this.IsPeriodOnExecution(periodCode) ? "False" : "True";
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Finds if the periods has errors.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// String with the message.
        /// </returns>
        public string FindErrors(string periodCode)
        {
            if (string.IsNullOrWhiteSpace(periodCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.controllerName, this.userInfo));
                return "Error";
            }

            try
            {
                return this.ValidateIfPeriodHasErrors(periodCode) ? "True" : "False";
            }
            catch
            {
                return "Error";
            }
        }

        /// <summary>
        /// Gets all permissions.
        /// </summary>
        /// <returns>Object with the permission.</returns>
        public JsonResult GetMessagesConfiguration()
        {
            List<MessageVO> messages = new List<MessageVO>();
            messages.Add(new MessageVO
            {
                Confirm = Resource.Accept,
                Cancel = Resource.Cancel,
                Title = Resource.Warning,
                Text = Resource.ProcessRunningError
            });
            messages.Add(new MessageVO
            {
                Confirm = Resource.Yes,
                Cancel = Resource.No,
                Title = Resource.Warning,
                Text = Resource.ErrorsFoundWarning
            });
            messages.Add(new MessageVO
            {
                Confirm = Resource.Yes,
                Cancel = Resource.No,
                Title = Resource.Warning,
                Text = Resource.ConfirmAction
            });

            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets all periods.
        /// </summary>
        /// <returns>List of periods.</returns>
        private List<PeriodVO> GetAllPeriods()
        {
            try
            {
                List<PeriodVO> periods = new List<PeriodVO>();
                periods = Mapper.Map<List<NationalJetFuelProcessDto>, List<PeriodVO>>(this.nationalJetFuelProcess.GetAllNationalJetFuelProcesses().ToList());
                return periods;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return null;
            }
        }

        /// <summary>
        /// Gets all periods.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// List of periods.
        /// </returns>
        private bool IsPeriodOnExecution(string periodCode)
        {
            try
            {
                return this.nationalJetFuelProcess.IsProcessOnExcecution(periodCode);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                throw;
            }
        }

        /// <summary>
        /// Gets the dates by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// Dates of the period.
        /// </returns>
        private PeriodVO GetDatesByPeriod(string periodCode)
        {
            try
            {
                PeriodVO period = new PeriodVO();
                NationalJetFuelProcessDto process = new NationalJetFuelProcessDto
                {
                    PeriodCode = periodCode
                };

                period = Mapper.Map<NationalJetFuelProcessDto, PeriodVO>(this.nationalJetFuelProcess.FindNationalJetFuelProcess(process));
                return period;
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return null;
            }
        }

        /// <summary>
        /// Validate if the period has errors.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// <c>true</c> if the period has errors otherwise <c>false</c>.
        /// </returns>
        private bool ValidateIfPeriodHasErrors(string periodCode)
        {
            try
            {
                return this.nationalJetFuelProcess.ValidateIfPeriodHasErros(periodCode);
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                throw;
            }
        }
	}
}