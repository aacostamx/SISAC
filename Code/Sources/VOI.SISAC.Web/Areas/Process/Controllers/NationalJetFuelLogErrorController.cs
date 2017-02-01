//------------------------------------------------------------------------
// <copyright file="NationalJetFuelLogErrorController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    /// National Jet fuel log error controller
    /// </summary>
    [CustomAuthorize]
    public class NationalJetFuelLogErrorController : BaseController
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
        private readonly INationalJetFuelLogErrorBusiness logErrors;

        /// <summary>
        /// The process
        /// </summary>
        private readonly INationalJetFuelProcessBusiness jetFuelProcess;

        /// <summary>
        /// The controller name
        /// </summary>
        private readonly string controllerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelLogErrorController" /> class.
        /// </summary>
        /// <param name="logErrors">The log errors.</param>
        /// <param name="jetFuelProcess">The jet fuel process.</param>
        public NationalJetFuelLogErrorController(INationalJetFuelLogErrorBusiness logErrors, INationalJetFuelProcessBusiness jetFuelProcess)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                Environment.UserDomainName,
                Environment.UserName,
                Environment.MachineName);

            this.logErrors = logErrors;
            this.jetFuelProcess = jetFuelProcess;
            this.controllerName = "National Jet Fuel Log Error";
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
                headers.Add("National Jet Fuel Ticket ID");
                headers.Add("Fueling Start Date");
                headers.Add("Fueling End Date");
                headers.Add(Resource.TicketNumber);
                headers.Add(Resource.ApronPosition);
                headers.Add(Resource.FueledQtyLts);
                headers.Add(Resource.ServiceCode);
                headers.Add(Resource.ProviderNumberPrimary);
                headers.Add("National Fuel Contract Concept Id");
                headers.Add("Fuel Concept Id");
                headers.Add(Resource.FuelConceptTypeCode);
                headers.Add(Resource.ChargeFactorTypeID);
                headers.Add(Resource.ProviderNumber);
                headers.Add(Resource.ScheduleTypeCode);
                headers.Add(Resource.Rate);

                return headers;
            }
        }

        /// <summary>
        /// Controller for the main view.
        /// </summary>
        /// <returns>Action result</returns>
        [CustomAuthorize(Roles = "NATFUELERR-EXP")]
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
        [CustomAuthorize(Roles = "NATFUELERR-EXP")]
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
                List<NationalJetFuelLogErrorDto> errors = new List<NationalJetFuelLogErrorDto>();
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
        private static IWorkbook SetContent(IWorkbook workBook, List<NationalJetFuelLogErrorDto> errors)
        {
            ISheet sheet = workBook.GetSheetAt(0);
            int rowIndex = 1;

            foreach (NationalJetFuelLogErrorDto item in errors)
            {
                IRow row = sheet.CreateRow(rowIndex);
                row.CreateCell(0).SetCellValue(item.LogID);
                row.CreateCell(1).SetCellValue(item.PeriodCode);
                row.CreateCell(2).SetCellValue(item.LineNumber);
                row.CreateCell(3).SetCellValue(item.Description);
                row.CreateCell(4).SetCellValue(item.Sequence);
                row.CreateCell(5).SetCellValue(item.AirlineCode);
                row.CreateCell(6).SetCellValue(item.FlightNumber);
                row.CreateCell(7).SetCellValue(item.ItineraryKey);
                row.CreateCell(8).SetCellValue(item.EquipmentNumber);
                row.CreateCell(9).SetCellValue(item.OperationTypeID == null ? string.Empty : item.OperationTypeID.Value.ToString());
                row.CreateCell(10).SetCellValue(item.NationalJetFuelTicketID == null ? string.Empty : item.NationalJetFuelTicketID.Value.ToString());
                row.CreateCell(11).SetCellValue(item.FuelingStartDate == null ? string.Empty : item.FuelingStartDate.Value.ToString());
                row.CreateCell(12).SetCellValue(item.FuelingEndDate == null ? string.Empty : item.FuelingEndDate.Value.ToString());
                row.CreateCell(13).SetCellValue(item.TicketNumber);
                row.CreateCell(14).SetCellValue(item.ApronPosition);
                row.CreateCell(15).SetCellValue(item.FueledQtyLts == null ? string.Empty : item.FueledQtyLts.Value.ToString());
                row.CreateCell(16).SetCellValue(item.ServiceCode);
                row.CreateCell(17).SetCellValue(item.ProviderNumberPrimary);
                row.CreateCell(18).SetCellValue(item.NationalFuelContractConceptID == null ? string.Empty : item.NationalFuelContractConceptID.Value.ToString());
                row.CreateCell(19).SetCellValue(item.FuelConceptID == null ? string.Empty : item.FuelConceptID.Value.ToString());
                row.CreateCell(20).SetCellValue(item.FuelConceptTypeCode);
                row.CreateCell(21).SetCellValue(item.ChargeFactorTypeID == null ? string.Empty : item.ChargeFactorTypeID.Value.ToString());
                row.CreateCell(22).SetCellValue(item.ProviderNumber);
                row.CreateCell(23).SetCellValue(item.ScheduleTypeCode);
                row.CreateCell(24).SetCellValue(item.Rate == null ? string.Empty : item.Rate.Value.ToString());
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
                periods = Mapper.Map<List<NationalJetFuelProcessDto>, List<PeriodVO>>(this.jetFuelProcess.GetAllNationalJetFuelProcesses().ToList());
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
                NationalJetFuelProcessDto process = new NationalJetFuelProcessDto
                {
                    PeriodCode = periodCode
                };

                period = Mapper.Map<NationalJetFuelProcessDto, PeriodVO>(this.jetFuelProcess.FindNationalJetFuelProcess(process));
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