//-------------------------------------------------------------------------------------------
// <copyright file="UploadNationalJetFuelNonconformityController.cs" company="AACOSTA">
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
    using System.Net.Mime;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using FileHelpers;
    using FileHelpers.Events;
    using VOI.SISAC.Business.Process;
    using VOI.SISAC.Web.Controllers;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.Files;
    using VOI.SISAC.Web.Models.VO.Process;
    using VOI.SISAC.Web.Resources;

    /// <summary>
    /// Upload National JetFuel Nonconformity Controller
    /// </summary>
    /// <seealso cref="VOI.SISAC.Web.Controllers.BaseController" />
    public class UploadNationalJetFuelNonconformityController : BaseController
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(UploadNationalJetFuelNonconformityController));

        /// <summary>
        /// The user information
        /// </summary>
        private readonly string userInfo;

        /// <summary>
        /// Catalog name
        /// </summary>
        private readonly string catalogName = "Upload National Jet Fuel Nonconformity Controller";

        /// <summary>
        /// The national jet fuel invoice control
        /// </summary>
        private readonly INationalJetFuelInvoiceControlBusiness nationalJetFuelInvoiceControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadNationalJetFuelInvoiceController"/> class.
        /// </summary>
        /// <param name="nationalJetFuelInvoiceControl">The national jet fuel invoice control.</param>
        public UploadNationalJetFuelNonconformityController(INationalJetFuelInvoiceControlBusiness nationalJetFuelInvoiceControl)
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
                return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }
            if (string.IsNullOrEmpty(pMonthYear))
            {
                this.TempData["ErrorMessage"] = Resource.MonthYear + ' ' + Resource.RequiredField;
                return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }
            if (string.IsNullOrEmpty(pPeriod))
            {
                this.TempData["ErrorMessage"] = Resource.Period + ' ' + Resource.RequiredField;
                return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }

            // Validates that the file has content
            if (file == null || file.ContentLength <= 0)
            {
                this.TempData["ErrorMessage"] = Resource.EmptyFileError;
                return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }

            // Validates that the field is a text plain type
            if (!file.ContentType.Equals("text/plain"))
            {
                this.TempData["ErrorMessage"] = Resource.FormatFileError;
                return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }

            try
            {
                DelimitedFileEngine<NationalJetFuelNonconformityFile> engine = new DelimitedFileEngine<NationalJetFuelNonconformityFile>();
                engine.AfterReadRecord += EngineAfterReadRecord;
                engine.Options.IgnoreFirstLines = 1;
                engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                NationalJetFuelNonconformityFile[] records;

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
                    return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
                }

                IList<NationalJetFuelNonconformityFile> fileRecords = new List<NationalJetFuelNonconformityFile>(records);

                // Creacion de DataTable para mandar por SP               
                var table = new DataTable();
                ListFileToDataTable(fileRecords, table, pRemittanceID, pMonthYear, pPeriod, this.User.Identity.Name);

                // Se envia informacion de remesas a DB
                errorResult = this.nationalJetFuelInvoiceControl.ValidateNonconformity((DataTable)table);

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
                    return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
                }

                // Validates business errors
                if (errorResult == null || errorResult.Count == 0)
                {
                    this.TempData["OperationSuccess"] = Resource.SuccessfulLoadFileManual;
                    return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
                }
                else
                {
                    this.TempData["ListErrorMessage"] = errorResult;
                    return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
                }
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Logger.Error(exception.Message, exception);
                Trace.TraceError(string.Format(LogMessages.ErrorRetrieveInfo, this.catalogName, this.userInfo));
                Trace.TraceError(exception.Message, exception);
                this.TempData["ErrorMessage"] = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
                return this.RedirectToAction("NationalJetFuelNonconformityProcess", "NationalJetFuelReconcileInvoice", new NationalJetFuelReconcileControlVO { RemittanceID = pRemittanceID, MonthYear = pMonthYear, Period = pPeriod });
            }
        }

        /// <summary>
        /// Lists the file to data table.
        /// </summary>
        /// <param name="fileRecords">The file records.</param>
        /// <param name="table">The table.</param>
        private static void ListFileToDataTable(IList<NationalJetFuelNonconformityFile> fileRecords, DataTable table, string pRemittanceID, string pMonthYear, string pPeriod, string username)
        {
            table.Columns.Add("RemittanceID", typeof(string));
            table.Columns.Add("MonthYear", typeof(string));
            table.Columns.Add("Period", typeof(string));
            table.Columns.Add("Username", typeof(string));
            table.Columns.Add("ID", typeof(Int64));
            table.Columns.Add("AirlineCode", typeof(string));
            table.Columns.Add("ProviderNumber", typeof(string));
            table.Columns.Add("ServiceCode", typeof(string));
            table.Columns.Add("FederalTaxCode", typeof(string));
            table.Columns.Add("StationCode", typeof(string));
            table.Columns.Add("InvoiceNumber", typeof(string));
            table.Columns.Add("CustomerNumber", typeof(string));
            table.Columns.Add("CustomerName", typeof(string));
            table.Columns.Add("InvoiceDate", typeof(DateTime));
            table.Columns.Add("ElectronicInvoiceNumber", typeof(string));
            table.Columns.Add("ElectronicInvoiceDate", typeof(DateTime));
            table.Columns.Add("ProductNumber", typeof(string));
            table.Columns.Add("ProductDescription", typeof(string));
            table.Columns.Add("TicketNumber", typeof(string));
            table.Columns.Add("OperationType", typeof(string));
            table.Columns.Add("FlightNumber", typeof(string));
            table.Columns.Add("EquipmentNumber", typeof(string));
            table.Columns.Add("AirplaneModel", typeof(string));
            table.Columns.Add("StartDateTime", typeof(DateTime));
            table.Columns.Add("EndDateTime", typeof(DateTime));
            table.Columns.Add("StartMeter", typeof(decimal));
            table.Columns.Add("EndMeter", typeof(decimal));
            table.Columns.Add("VolumeM3", typeof(decimal));
            table.Columns.Add("RateType", typeof(string));
            table.Columns.Add("JetFuelAmount", typeof(decimal));
            table.Columns.Add("FreightAmount", typeof(decimal));
            table.Columns.Add("DiscountAmount", typeof(decimal));
            table.Columns.Add("FuelingAmount", typeof(decimal));
            table.Columns.Add("SubtotalAmount", typeof(decimal));
            table.Columns.Add("TaxAmount", typeof(decimal));
            table.Columns.Add("TotalAmount", typeof(decimal));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("ReconciliationStatus", typeof(string));
            table.Columns.Add("InvoiceCostVariance", typeof(decimal));

            foreach (var entity in fileRecords)
            {
                var row = table.NewRow();
                row["RemittanceID"] = pRemittanceID;
                row["MonthYear"] = pMonthYear;
                row["Period"] = pPeriod;
                row["Username"] = username;
                row["ID"] = entity.ID;
                row["AirlineCode"] = entity.AirlineCode;
                row["ProviderNumber"] = entity.ProviderNumber;
                row["ServiceCode"] = entity.ServiceCode;
                row["FederalTaxCode"] = entity.FederalTaxCode;
                row["StationCode"] = entity.StationCode;
                row["InvoiceNumber"] = entity.InvoiceNumber;
                row["CustomerNumber"] = entity.CustomerNumber;
                row["CustomerName"] = entity.CustomerName;
                row["InvoiceDate"] = entity.InvoiceDate;
                row["ElectronicInvoiceNumber"] = entity.ElectronicInvoiceNumber;
                row["ElectronicInvoiceDate"] = entity.ElectronicInvoiceDate;
                row["ProductNumber"] = entity.ProductNumber;
                row["ProductDescription"] = entity.ProductDescription;
                row["TicketNumber"] = entity.TicketNumber;
                row["OperationType"] = entity.OperationType;
                row["FlightNumber"] = entity.FlightNumber;
                row["EquipmentNumber"] = entity.EquipmentNumber;
                row["AirplaneModel"] = entity.AirplaneModel;
                row["StartDateTime"] = entity.StartDateTime;
                row["EndDateTime"] = entity.EndDateTime;
                row["StartMeter"] = entity.StartMeter;
                row["EndMeter"] = entity.EndMeter;
                row["VolumeM3"] = entity.VolumeM3;
                row["RateType"] = entity.RateType;
                row["JetFuelAmount"] = entity.JetFuelAmount;
                row["FreightAmount"] = entity.FreightAmount;
                row["DiscountAmount"] = entity.DiscountAmount;
                row["FuelingAmount"] = entity.FuelingAmount;
                row["SubtotalAmount"] = entity.SubtotalAmount;
                row["TaxAmount"] = entity.TaxAmount;
                row["TotalAmount"] = entity.TotalAmount;
                row["Status"] = entity.Status;
                row["ReconciliationStatus"] = entity.ReconciliationStatus != null ? (object)Convert.ToString(entity.ReconciliationStatus) : DBNull.Value;
                row["InvoiceCostVariance"] = entity.InvoiceCostVariance != null ? (object)Convert.ToDecimal(entity.InvoiceCostVariance) : DBNull.Value; 

                table.Rows.Add(row);
            }
        }

        /// <summary>
        /// Engines the after read record.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="e">The <see cref="AfterReadEventArgs{NationalJetFuelManualReconcileFile}"/> instance containing the event data.</param>
        private static void EngineAfterReadRecord(EngineBase engine, AfterReadEventArgs<NationalJetFuelNonconformityFile> e)
        {
            e.Record.LineNumber = engine.LineNumber;
        }

        /// <summary>
        /// Finds the errors.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private IList<string> FindErrors(DelimitedFileEngine<NationalJetFuelNonconformityFile> file)
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

        /// <summary>
        /// Downloads this instance.
        /// </summary>
        /// <returns></returns>
        public FileContentResult Download()
        {
            try
            {
                byte[] data;
                string pathFile = Server.MapPath(Resource.NonconformityFilePath);
                data = FileHelper.GetPlainTextFile(pathFile);
                return File(data, MediaTypeNames.Text.Plain, Resource.NonconformityFilePath);
            }
            catch (IOException)
            {
                this.ViewBag.ErrorMessage = FrontMessage.GetExceptionErrorMessage();
                return null;
            }
        }
	}
}