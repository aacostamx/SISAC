//------------------------------------------------------------------------
// <copyright file="JetFuelLogErrorController.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Process.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using log4net.Repository.Hierarchy;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using VOI.SISAC.Business.Dto.Process;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Process;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.VO.Process;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Jet fuel log error controller
    /// </summary>
    [CustomAuthorize]
    public class JetFuelLogErrorController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(JetFuelLogErrorController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// The log errors business
        /// </summary>
        private readonly IJetFuelLogErrorBusiness logErrors;

        /// <summary>
        /// The process
        /// </summary>
        private readonly IJetFuelProcessBusiness jetFuelProcess;

        /// <summary>
        /// The controller name
        /// </summary>
        private readonly string controllerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelLogErrorController" /> class.
        /// </summary>
        /// <param name="logErrors">The log errors.</param>
        /// <param name="jetFuelProcess">The jet fuel process.</param>
        public JetFuelLogErrorController(IJetFuelLogErrorBusiness logErrors, IJetFuelProcessBusiness jetFuelProcess)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);

            this.logErrors = logErrors;
            this.jetFuelProcess = jetFuelProcess;
            this.controllerName = "Jet Fuel Log Error";
        }

        /// <summary>
        /// Gets the get headers title.
        /// </summary>
        /// <value>
        /// The get headers title.
        /// </value>
        private List<string> GetHeadersTitles
        {
            get
            {
                List<string> headers = new List<string>();
                headers.Add("Log Id");
                headers.Add("Period code");
                headers.Add("Line number");
                headers.Add(Resource.Description);
                headers.Add(Resource.Sequence);
                headers.Add(Resource.AirlineCode);
                headers.Add(Resource.FlightNumber);
                headers.Add(Resource.ItineraryKey);
                headers.Add(Resource.EquipmentNumber);
                headers.Add(Resource.OperationTypeID);
                headers.Add(Resource.JetFuelTicketID);
                headers.Add(Resource.FuelingDate);
                headers.Add(Resource.TicketNumber);
                headers.Add(Resource.FueledQtyGals);
                headers.Add(Resource.ServiceCode);
                headers.Add(Resource.ProviderNumberPrimary);
                headers.Add("International Fuel Contract Concept Id");
                headers.Add("Fuel Concept Id");
                headers.Add(Resource.FuelConceptTypeCode);
                headers.Add(Resource.ChargeFactorTypeID);
                headers.Add(Resource.ProviderNumber);
                headers.Add(Resource.Rate);

                return headers;
            }
        }

        /// <summary>
        /// Controller for the main view.
        /// </summary>
        /// <returns>Action result</returns>
        [CustomAuthorize(Roles="INTFUELERR-EXP")]
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Exports the specified period code.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>File with the errors found.</returns>
        [HttpPost]
        [CustomAuthorize(Roles = "INTFUELERR-EXP")]
        public ActionResult Export(string periodCode)
        {
            if (string.IsNullOrWhiteSpace(periodCode))
            {
                Logger.Error(string.Format(LogMessages.ErrorNullObject, this.controllerName));
                Trace.TraceError(string.Format(LogMessages.ErrorCreate, this.controllerName, this.userInfo));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            try
            {
                List<JetFuelLogErrorDto> errors = new List<JetFuelLogErrorDto>();
                byte[] bytes;
                IWorkbook workBook = new XSSFWorkbook();
                ISheet sheet = workBook.CreateSheet(Resource.LogErrors);

                errors = this.logErrors.GetErrorsForPeriod(periodCode).ToList();

                if (errors == null || errors.Count < 1)
                {
                    this.TempData["OperationSuccess"] = Resource.NoErrorsForPeriod;
                    return this.View("Index");
                }

                workBook = SetHeaders(workBook, this.GetHeadersTitles);
                workBook = SetContent(workBook, errors);

                using (MemoryStream exportData = new MemoryStream())
                {
                    workBook.Write(exportData);
                    bytes = exportData.ToArray();
                }

                return this.File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "log.xlsx");
            }
            catch (BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(exception.Number);
                return this.View("Index");
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.CreationFileError, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.CreationFileError, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage(0);
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
                period = new PeriodVO();
            }
            
            period = this.GetDatesByPeriod(periodCode);
            return this.Json(period, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the work book.
        /// </summary>
        /// <param name="workBook">The work book.</param>
        /// <param name="headers">The headers.</param>
        /// <returns>
        /// The work book with the headers.
        /// </returns>
        private static IWorkbook SetHeaders(IWorkbook workBook, List<string> headers)
        {
            ISheet sheet = workBook.GetSheetAt(0);
            IRow row = sheet.CreateRow(0);
            int column = 0;
            foreach (string item in headers)
            {
                row.CreateCell(column++).SetCellValue(item);
            }

            return workBook;
        }

        /// <summary>
        /// Creates the work book.
        /// </summary>
        /// <param name="workBook">The work book.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>
        /// The work book with the content.
        /// </returns>
        private static IWorkbook SetContent(IWorkbook workBook, List<JetFuelLogErrorDto> errors)
        {
            ISheet sheet = workBook.GetSheetAt(0);
            int rowIndex = 1;

            foreach (JetFuelLogErrorDto item in errors)
            {
                IRow row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(item.LogId);
                row.CreateCell(1).SetCellValue(item.PeriodCode);
                row.CreateCell(2).SetCellValue(item.LineNumber);
                row.CreateCell(3).SetCellValue(item.Description);
                row.CreateCell(4).SetCellValue(item.ItinerarySequence);
                row.CreateCell(5).SetCellValue(item.ItineraryAirlineCode);
                row.CreateCell(6).SetCellValue(item.ItineraryFlightNumber);
                row.CreateCell(7).SetCellValue(item.ItineraryKey);
                row.CreateCell(8).SetCellValue(item.AirplaneEquipmentNumber);
                row.CreateCell(9).SetCellValue(item.OperationTypeId == null ? string.Empty : item.OperationTypeId.Value.ToString());
                row.CreateCell(10).SetCellValue(item.JetFuelTicketId == null ? string.Empty : item.JetFuelTicketId.Value.ToString());
                row.CreateCell(11).SetCellValue(item.FuelingDate == null ? string.Empty : item.FuelingDate.Value.ToString());
                row.CreateCell(12).SetCellValue(item.TicketNumber);
                row.CreateCell(13).SetCellValue(item.FueledQuantityGallon == null ? string.Empty : item.FueledQuantityGallon.Value.ToString());
                row.CreateCell(14).SetCellValue(item.ServiceCode);
                row.CreateCell(15).SetCellValue(item.ProviderNumberPrimary);
                row.CreateCell(16).SetCellValue(item.InternationalFuelContractConceptId == null ? string.Empty : item.InternationalFuelContractConceptId.Value.ToString());
                row.CreateCell(17).SetCellValue(item.FuelConceptId == null ? string.Empty : item.FuelConceptId.Value.ToString());
                row.CreateCell(18).SetCellValue(item.FuelConceptTypeCode);
                row.CreateCell(19).SetCellValue(item.ChargeFactorTypeId == null ? string.Empty : item.ChargeFactorTypeId.Value.ToString());
                row.CreateCell(20).SetCellValue(item.ProviderNumber);
                row.CreateCell(21).SetCellValue(item.Rate == null ? string.Empty : item.Rate.Value.ToString());
                rowIndex++;
            }

            return workBook;
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
                periods = Mapper.Map<List<JetFuelProcessDto>, List<PeriodVO>>(this.jetFuelProcess.GetAllJetFuelProcesses().ToList());
                return  periods;
            }
            catch(BusinessException exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.controllerName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                return null;
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
                JetFuelProcessDto process = new JetFuelProcessDto
                {
                    PeriodCode = periodCode
                };

                period = Mapper.Map<JetFuelProcessDto, PeriodVO>(this.jetFuelProcess.FindJetFuelProcess(process));
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
    }
}