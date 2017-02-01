//-------------------------------------------------------------------------------------------
// <copyright file="UploadNationalJetFuelManualReconcileController.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

namespace VOI.SISAC.Web.Areas.Process.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using FileHelpers;
    using FileHelpers.Events;
    using VOI.SISAC.Business.Process;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Models.Files;
    using VOI.SISAC.Web.Models.VO.Process;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Upload NationalJetFuel Manual Reconcile Controller
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    public class UploadNationalJetFuelManualReconcileController : BaseController
    {
       
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(UploadNationalJetFuelInvoiceController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = "Upload National Jet Fuel Manual Reconcile";

        /// <summary>
        /// The national jet fuel invoice control
        /// </summary>
        private readonly INationalJetFuelInvoiceControlBusiness nationalJetFuelInvoiceControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadNationalJetFuelInvoiceController"/> class.
        /// </summary>
        /// <param name="nationalJetFuelInvoiceControl">The national jet fuel invoice control.</param>
        public UploadNationalJetFuelManualReconcileController(INationalJetFuelInvoiceControlBusiness nationalJetFuelInvoiceControl)
        {
            this.userInfo = string.Format(
                LogMessages.UserInfo,
                System.Environment.UserDomainName,
                System.Environment.UserName,
                System.Environment.MachineName);
            this.nationalJetFuelInvoiceControl = nationalJetFuelInvoiceControl;
        }

        /// <summary>
        /// Uploads the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string pRemittanceID, string pMonthYear, string pPeriod)
        {
            //Validar parametros de Remesa
            if (string.IsNullOrEmpty(pRemittanceID))
            {
                this.TempData["ErrorMessage"] = Resource.RemittanceId + ' ' + Resource.RequiredField;
                return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }
            if (string.IsNullOrEmpty(pMonthYear))
            {
                this.TempData["ErrorMessage"] = Resource.MonthYear + ' ' + Resource.RequiredField;
                return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }
            if (string.IsNullOrEmpty(pPeriod))
            {
                this.TempData["ErrorMessage"] = Resource.Period + ' ' + Resource.RequiredField;
                return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }

            // Validates that the file has content
            if (file == null || file.ContentLength <= 0)
            {
                this.TempData["ErrorMessage"] = Resource.EmptyFileError;
                return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }

            // Validates that the field is a text plain type
            if (!file.ContentType.Equals("text/plain"))
            {
                this.TempData["ErrorMessage"] = Resource.FormatFileError;
                return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }

            try
            {
                DelimitedFileEngine<NationalJetFuelManualReconcileFile> engine = new DelimitedFileEngine<NationalJetFuelManualReconcileFile>();
                engine.AfterReadRecord += EngineAfterReadRecord;
                engine.Options.IgnoreFirstLines = 1;
                engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                NationalJetFuelManualReconcileFile[] records;

                using (StreamReader sr = new StreamReader(file.InputStream, Encoding.Default))
                {
                    records = engine.ReadStream(sr);
                }

                IList<string> errorResult = new List<string>();
                errorResult = this.FindErrors(engine);

                // Validates errors in the file
                if (errorResult != null && errorResult.Count > 0)
                {
                    this.TempData["ListErrorMessage"] = errorResult;
                    return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
                }

                IList<NationalJetFuelManualReconcileFile> fileRecords = new List<NationalJetFuelManualReconcileFile>(records);

                // Creacion de DataTable para mandar por SP               
                var table = new DataTable();
                ListFileToDataTable(fileRecords, table, pRemittanceID, pMonthYear, pPeriod, this.User.Identity.Name);

                // Se envia informacion de remesas a DB
                errorResult = this.nationalJetFuelInvoiceControl.ValidateManualReconcile((DataTable)table);

                if (errorResult.Count > 0)
                {
                    List<string> message = new List<string>();
                    foreach (string item in errorResult)
                    {
                        if (item.Length < 22)
                        {
                            message.Add("Some IDs belong to another Remittance as the following: " + item);
                        }
                        else
                        {
                            message.Add(item);
                        }
                    }

                    this.TempData["ListErrorMessage"] = message;
                    return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
                }

                // Validates business errors
                if (errorResult == null || errorResult.Count == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFileManual;
                    return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
                }
                else
                {
                    this.TempData["ListErrorMessage"] = errorResult;
                    return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear , Period = pPeriod });
                }
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
                return this.RedirectToAction("NationalJetFuelReconcileManualProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }
        }

        /// <summary>
        /// Lists the file to data table.
        /// </summary>
        /// <param name="fileRecords">The file records.</param>
        /// <param name="table">The table.</param>
        private static void ListFileToDataTable(IList<NationalJetFuelManualReconcileFile> fileRecords, DataTable table, string pRemittanceID, string pMonthYear, string pPeriod, string username)
        {
            table.Columns.Add("RemittanceID", typeof(string));
            table.Columns.Add("MonthYear", typeof(string));
            table.Columns.Add("Period", typeof(string));
            table.Columns.Add("Username", typeof(string));
            table.Columns.Add("ID", typeof(Int64));
            table.Columns.Add("PeriodCode", typeof(string));
            table.Columns.Add("Sequence", typeof(int));
            table.Columns.Add("AirlineCode", typeof(string));
            table.Columns.Add("FlightNumber", typeof(string));
            table.Columns.Add("ItineraryKey", typeof(string));
            table.Columns.Add("EquipmentNumber", typeof(string));
            table.Columns.Add("DepartureStation", typeof(string));
            table.Columns.Add("ArrivalStation", typeof(string));
            table.Columns.Add("NationalJetFuelTicketID", typeof(Int64));
            table.Columns.Add("FuelingStartDate", typeof(DateTime));
            table.Columns.Add("FuelingEndDate", typeof(DateTime));
            table.Columns.Add("TicketNumber", typeof(string));
            table.Columns.Add("FueledQtyLts", typeof(int));
            table.Columns.Add("RemainingQtyKgs", typeof(int));
            table.Columns.Add("RequestedQtyKgs", typeof(int));
            table.Columns.Add("FueledQtyKgs", typeof(int));
            table.Columns.Add("DensityFactor", typeof(decimal));
            table.Columns.Add("PemexSubTotal", typeof(decimal));
            table.Columns.Add("SuministroSubTotal", typeof(decimal));
            table.Columns.Add("FleteSubTotal", typeof(decimal));
            table.Columns.Add("Iva", typeof(int));
            table.Columns.Add("PrecioSubTotal", typeof(decimal));
            table.Columns.Add("Total", typeof(decimal));

            foreach (var entity in fileRecords)
            {
                var row = table.NewRow();
                row["RemittanceID"] = pRemittanceID;
                row["MonthYear"] = pMonthYear;
                row["Period"] = pPeriod;
                row["Username"] = username;
                row["ID"] = entity.ID;
                row["PeriodCode"] = entity.PeriodCode;
                row["Sequence"] = entity.Sequence;
                row["AirlineCode"] = entity.AirlineCode;
                row["FlightNumber"] = entity.FlightNumber;
                row["ItineraryKey"] = entity.ItineraryKey;
                row["EquipmentNumber"] = entity.EquipmentNumber;
                row["DepartureStation"] = entity.DepartureStation;
                row["ArrivalStation"] = entity.ArrivalStation;
                row["NationalJetFuelTicketID"] = entity.NationalJetFuelTicketID;
                row["FuelingStartDate"] = entity.FuelingStartDate;
                row["FuelingEndDate"] = entity.FuelingEndDate;
                row["TicketNumber"] = entity.TicketNumber;
                row["FueledQtyLts"] = entity.FueledQtyLts;

                row["RemainingQtyKgs"] = entity.RemainingQtyKgs != null ? (object)Convert.ToInt32(entity.RemainingQtyKgs) : DBNull.Value;
                row["RequestedQtyKgs"] = entity.RequestedQtyKgs != null ? (object)Convert.ToInt32(entity.RequestedQtyKgs) : DBNull.Value;
                row["FueledQtyKgs"] = entity.FueledQtyKgs != null ? (object)Convert.ToInt32(entity.FueledQtyKgs) : DBNull.Value;
                row["DensityFactor"] = entity.DensityFactor != null ? (object)Convert.ToDecimal(entity.DensityFactor) : DBNull.Value; 
                
                row["PemexSubTotal"] = entity.PemexSubTotal;
                row["SuministroSubTotal"] = entity.SuministroSubTotal;
                row["FleteSubTotal"] = entity.FleteSubTotal;
                row["Iva"] = entity.Iva;
                row["PrecioSubTotal"] = entity.PrecioSubTotal;
                row["Total"] = entity.Total;
                table.Rows.Add(row);
            }
        }

        /// <summary>
        /// Engines the after read record.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="e">The <see cref="AfterReadEventArgs{NationalJetFuelManualReconcileFile}"/> instance containing the event data.</param>
        private static void EngineAfterReadRecord(EngineBase engine, AfterReadEventArgs<NationalJetFuelManualReconcileFile> e)
        {
            e.Record.LineNumber = engine.LineNumber;
        }

        /// <summary>
        /// Finds the errors.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private IList<string> FindErrors(DelimitedFileEngine<NationalJetFuelManualReconcileFile> file)
        {
            IList<string> fileErrors = new List<string>();
            string errorDetail;

            // Finds errors in the file
            foreach (var error in file.ErrorManager.Errors)
            {
                errorDetail = error.ExceptionInfo.InnerException != null ? error.ExceptionInfo.InnerException.Message : string.Empty;
                fileErrors.Add(Resource.ErrorValidationFile + error.LineNumber + ". " + errorDetail);
            }

            return fileErrors;
        }
    }
}