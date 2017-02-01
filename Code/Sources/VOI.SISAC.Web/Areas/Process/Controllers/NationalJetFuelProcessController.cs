//------------------------------------------------------------------------
// <copyright file="NationalJetFuelProcessController.cs" company="Volaris">
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
    using System.Web.Mvc;
    using Business.Dto.Enums;
    using Business.ExceptionBusiness;
    using Helpers;
    using Web.Controllers;

    /// <summary>
    /// Jet Fuel Process Controller
    /// </summary>
    [CustomAuthorize]
    public class NationalJetFuelProcessController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(NationalJetFuelProcessController));

        /// <summary>
        /// The machine name
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Module Name
        /// </summary>
        private readonly string moduleName = Resource.NationalJetFuelProcess;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly IGenericCatalogBusiness comboboxGeneric;

        /// <summary>
        /// The generic catalogs
        /// </summary>
        private readonly INationalJetFuelProcessBusiness ntlJetFuelProcessBusiness;

        /// <summary>
        /// NationalJetFuelProcessController Constructor
        /// </summary>
        /// <param name="comboboxGeneric"></param>
        /// <param name="ntlJetFuelProcessBusiness"></param>
        public NationalJetFuelProcessController(
            IGenericCatalogBusiness comboboxGeneric,
            INationalJetFuelProcessBusiness ntlJetFuelProcessBusiness)
        {
            this.userInfo = string.Format(
            LogMessages.UserInfo,
            Environment.UserDomainName,
            Environment.UserName,
            Environment.MachineName);
            this.comboboxGeneric = comboboxGeneric;
            this.ntlJetFuelProcessBusiness = ntlJetFuelProcessBusiness;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "NTLFUELPRO-IDX")]
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
        [CustomAuthorize(Roles = "NTLFUELPRO-ADD")]
        public ActionResult Create()
        {
            try
            {

            }
            catch (BusinessException ex)
            {
                Logger.Error(ex.Message, ex);
                Trace.TraceError(ex.Message, ex);
            }
            return this.View();
        }

        /// <summary>
        /// Start Process 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CustomAuthorize(Roles = "NTLFUELPRO-START")]
        public bool StartProcess(NationalJetFuelProcessVO jetFuelProcess)
        {
            bool start = false;
            NationalJetFuelProcessDto processDto = new NationalJetFuelProcessDto();

            if (string.IsNullOrEmpty(jetFuelProcess.PeriodCode))
            {
                return start;
            }

            try
            {
                jetFuelProcess.TypeProcess = jetFuelProcess.ProcessPending == true ? NationalTypeProcessVO.NationalJetFuelProcessPending : NationalTypeProcessVO.NationalJetFuelProcessAll;
                jetFuelProcess.ProcessedByUserName = this.User.Identity.Name;
                processDto = Mapper.Map<NationalJetFuelProcessDto>(jetFuelProcess);
                start = this.ntlJetFuelProcessBusiness.StartNationalJetFuelProcess(processDto);
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
                this.ViewBag.Process = this.comboboxGeneric.GetNationalJetFuelProcessCatalog();
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
            NationalJetFuelProcessDto process = new NationalJetFuelProcessDto();

            if (periodCode == null)
            {
                return result;
            }

            try
            {
                process = this.ntlJetFuelProcessBusiness.FindNationalJetFuelProcess(new NationalJetFuelProcessDto(periodCode));
                //CheckClosedCosts(process);

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
        private static void CheckClosedCosts(NationalJetFuelProcessDto process)
        {
            var costs = process.NationalJetFuelCosts;

            foreach (var item in costs)
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
        [CustomAuthorize(Roles = "NTLFUELPRO-REVERT")]
        public bool RevertProcess(NationalJetFuelProcessVO processVO)
        {
            bool revert = false;

            if (processVO.PeriodCode == null)
            {
                return revert;
            }

            try
            {
                NationalJetFuelProcessDto process = new NationalJetFuelProcessDto(processVO.PeriodCode);
                process.TypeProcess = NationalTypeProcessDto.NationalJetFuelProcessRevert;
                revert = this.ntlJetFuelProcessBusiness.RevertNationalJetFuelProcess(process);
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