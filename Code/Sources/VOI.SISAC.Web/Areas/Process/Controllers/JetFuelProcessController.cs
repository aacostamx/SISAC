//------------------------------------------------------------------------
// <copyright file="JetFuelProcessController.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Process.Controllers
{
    using AutoMapper;
    using Business.Common;
    using Business.Dto.Process;
    using Business.Process;
    using Models.Enums;
    using Models.VO.Process;
    using Newtonsoft.Json;
    using Resources;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Mvc;
    using VOI.SISAC.Business.Dto.Enums;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Web.Helpers;
    using Web.Controllers;

    /// <summary>
    /// Jet Fuel Process Controller
    /// </summary>
    [CustomAuthorize]
    public class JetFuelProcessController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(JetFuelProcessController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Module Name
        /// </summary>
        private readonly string moduleName = Resource.JetFuelProcess;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness comboboxGeneric;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IJetFuelProcessBusiness jetFuelProcessBusiness;

        /// <summary>
        /// JetFuelProcessController Constructor
        /// </summary>
        /// <param name="comboboxGeneric"></param>
        /// <param name="jetFuelProcessBusiness"></param>
        public JetFuelProcessController(
            IGenericCatalogBusiness comboboxGeneric,
            IJetFuelProcessBusiness jetFuelProcessBusiness)
        {
            this.userInfo = string.Format(
            LogMessages.UserInfo,
            Environment.UserDomainName,
            Environment.UserName,
            Environment.MachineName);
            this.comboboxGeneric = comboboxGeneric;
            this.jetFuelProcessBusiness = jetFuelProcessBusiness;
        }



        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "INTFUELPRO-IDX")]
        public ActionResult Index()
        {
            try
            {
                LoadCurrentPeriodCombox();
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return this.View();
        }

        /// <summary>
        /// Create Period
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "INTFUELPRO-ADD")]
        public ActionResult Create()
        {
            try
            {

            }
            catch (BusinessException)
            {

            }
            return this.View();
        }

        /// <summary>
        /// Start Process 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "INTFUELPRO-START")]
        public bool StartProcess(JetFuelProcessVO jetFuelProcess)
        {
            bool start = false;
            JetFuelProcessDto processDto = new JetFuelProcessDto();

            if (string.IsNullOrEmpty(jetFuelProcess.PeriodCode))
            {
                return start;
            }

            try
            {
                jetFuelProcess.TypeProcess = jetFuelProcess.ProcessPending == true ? TypeProcessVO.JetFuelProcessPending : TypeProcessVO.JetFuelProcessAll;
                jetFuelProcess.ProcessedByUserName = this.User.Identity.Name;
                processDto = Mapper.Map<JetFuelProcessDto>(jetFuelProcess);
                start = this.jetFuelProcessBusiness.StartJetFuelProcess(processDto);
            }
            catch (BusinessException ex)
            {
                this.LoadCurrentPeriodCombox();
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
            return start;
        }

        /// <summary>
        /// Loads the catalogs.
        /// </summary>
        private void LoadCurrentPeriodCombox()
        {
            try
            {
                this.ViewBag.Process = this.comboboxGeneric.GetJetFuelProcessCatalog();
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }
        }

        /// <summary>
        /// Get Fuel Process
        /// </summary>
        /// <param name="periodCode"></param>
        /// <returns></returns>
        public ContentResult GetFuelProcess(string periodCode)
        {
            string json = string.Empty;
            ContentResult result = new ContentResult();
            JetFuelProcessDto process = new JetFuelProcessDto();

            if (periodCode == null)
            {
                return result;
            }

            try
            {
                process = this.jetFuelProcessBusiness.FindJetFuelProcess(new JetFuelProcessDto(periodCode));
                CheckClosedProvision(process);

                json = JsonConvert.SerializeObject(process, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                result = Content(json);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
            }

            return result;
        }

        /// <summary>
        /// Check Closed Provision
        /// </summary>
        /// <param name="process"></param>
        private static void CheckClosedProvision(JetFuelProcessDto process)
        {
            var provisions = process.JetFuelProvisions;

            foreach (var item in provisions)
            {
                var closed = string.IsNullOrEmpty(item.PolicyID.ToString()) ? false : true;

                if (closed)
                {
                    process.ClosedProvision = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Revert Jet Fuel Process
        /// </summary>
        /// <param name="processVO"></param>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "INTFUELPRO-REVERT")]
        public bool RevertProcess(JetFuelProcessVO processVO)
        {
            bool revert = false;

            if (processVO.PeriodCode == null)
            {
                return revert;
            }

            try
            {
                JetFuelProcessDto process = new JetFuelProcessDto(processVO.PeriodCode);
                process.TypeProcess = TypeProcessDto.JetFuelProcessRevert;
                revert = this.jetFuelProcessBusiness.RevertJetFuelProcess(process);
            }
            catch (BusinessException ex)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Logger.Error(ex.Message, ex);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.moduleName, this.userInfo));
                Trace.TraceError(ex.Message, ex);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(ex.Number);
                revert = false;
            }
            return revert;
        }
    }
}