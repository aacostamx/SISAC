//-----------------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceControlRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// National Jet Fuel Invoice Control Repository
    /// </summary>
    public class NationalJetFuelInvoiceControlRepository : Repository<NationalJetFuelInvoiceControl>, INationalJetFuelInvoiceControlRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceControlRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalJetFuelInvoiceControlRepository(IDbFactory factory) : base(factory) { }

        /// <summary>
        /// Finds the national jet fuel invoice control.
        /// </summary>
        /// <param name="invoiceCTRL">The invoice control.</param>
        /// <returns></returns>
        public NationalJetFuelInvoiceControl FindNationalJetFuelInvoiceControl(NationalJetFuelInvoiceControl invoiceCTRL)
        {
            var invoice = this.DbContext.NationalJetFuelInvoiceControl
                .Include(c => c.CalculationStatus)
                .Include(c => c.ConfirmationStatus)
                .Include(c => c.StatusProcess)
                .Include(c => c.RemittanceStatus)
                .Include(c => c.DocumentStatus).FirstOrDefault
                (c => c.RemittanceID == invoiceCTRL.RemittanceID &&
                c.MonthYear == invoiceCTRL.MonthYear &&
                c.Period == invoiceCTRL.Period);

            return invoice;
        }

        /// <summary>
        /// Validates the remittance identifier.
        /// </summary>
        /// <param name="remittanceInfo">The remittance information.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <returns></returns>
        public IList<RemittanceIDValidate> ValidateRemittanceID(DataTable remittanceInfo, string airlineCode)
        {
            IList<RemittanceIDValidate> remittanceID = this.DbContext.ValidateAndSaveRemittanceID(remittanceInfo, airlineCode);

            return remittanceID;
        }

        /// <summary>
        /// Validates the manual reconcile.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        public IList<RemittanceIDValidate> ValidateManualReconcile(DataTable reconcileInfo)
        {
            IList<RemittanceIDValidate> remittanceID = this.DbContext.UploadManualReconcile(reconcileInfo);

            return remittanceID;
        }

        /// <summary>
        /// Validates the nonconformity.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        public IList<RemittanceIDValidate> ValidateNonconformity(DataTable reconcileInfo)
        {
            IList<RemittanceIDValidate> remittanceID = this.DbContext.UploadNonconformity(reconcileInfo);

            return remittanceID;
        }

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <param name="take">The number of elements to get.</param>
        /// <param name="skip">Indicates in which element start to count .</param>
        /// <returns>
        /// The entity's records paginated.
        /// </returns>
        public IList<NationalJetFuelInvoiceControl> GetPaginatedData(int take, int skip)
        {
            List<NationalJetFuelInvoiceControl> invoices = this.DbContext.NationalJetFuelInvoiceControl
                .Include(c => c.NationalJetFuelInvoiceDetails)
                .Include(c => c.RemittanceStatus)
                .Include(c => c.DocumentStatus)
                .OrderBy(c => c.RemittanceID)
                .Skip(skip)
                .Take(take)
                .ToList();

            return invoices;
        }

        /// <summary>
        /// Creates the policy.
        /// </summary>
        /// <param name="invoiceControl">The invoice control.</param>
        /// <returns>
        /// Greater than zero. The process was completed successfuly.
        /// Less or equal than zero. An error in the process.
        /// </returns>
        public int CreatePolicyDocument(NationalJetFuelInvoiceControl invoiceControl)
        {
            int result = this.DbContext.SaveNationalJetFuelInvoicePolicy(
                invoiceControl.RemittanceID,
                invoiceControl.MonthYear,
                (DateTime)invoiceControl.DateValue,
                (DateTime)invoiceControl.DatePosting,
                (DateTime)invoiceControl.DateBaseline,
                invoiceControl.Period);

            return result;
        }

        /// <summary>
        /// Deleteds the invoice policy.
        /// </summary>
        /// <param name="invoiceCTRL">The invoice control.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int DeletedInvoicePolicy(NationalJetFuelInvoiceControl invoiceCTRL)
        {
            var deleted = 0;

            try
            {
                deleted = this.DbContext.DeleteNationalInvoice(invoiceCTRL.RemittanceID, invoiceCTRL.MonthYear, invoiceCTRL.Period);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return deleted;
        }

        /// <summary>
        /// Validates the invoice policy before create it.
        /// </summary>
        /// <param name="invoiceControl">The invoice control.</param>
        /// <returns>
        /// -1: An error during the call of the store procedure.
        /// 0: No errors found.
        /// 1: Error. The policy has been created.
        /// 2: Error caused because there are errors in the remittense.
        /// </returns>
        public int ValidateInvoicePolicy(NationalJetFuelInvoiceControl invoiceControl)
        {
            int result = this.DbContext.VerifyNationalJetFuelInvoicePolicy(invoiceControl.RemittanceID, invoiceControl.MonthYear, invoiceControl.Period);
            return result;
        }

        /// <summary>
        /// Gets the national jet fuel reconcile control.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public IList<NationalJetFuelInvoiceControl> GetTopNationalJetFuelReconcileControl(string date)
        {
            var invoices = new List<NationalJetFuelInvoiceControl>();

            try
            {
                invoices = this.DbContext.NationalJetFuelInvoiceControl
                    .Take(30)
                    .Include(c => c.NationalJetFuelInvoiceDetails)
                    .Include(c => c.CalculationStatus)
                    .Include(c => c.ConfirmationStatus)
                    .Include(c => c.StatusProcess)
                    .Include(c => c.RemittanceStatus)
                    .Include(c => c.DocumentStatus)
                    .OrderBy(c => c.RemittanceID)
                    .ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return invoices;
        }

        /// <summary>
        /// Finds the national reconcile process no tracking.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        public NationalJetFuelInvoiceControl FindNationalReconcileProcessNoTracking(NationalJetFuelInvoiceControl process)
        {
            var reconcileProcess = new NationalJetFuelInvoiceControl();

            reconcileProcess = this.DbContext.NationalJetFuelInvoiceControl.AsNoTracking()
                .Where(c => c.RemittanceID == process.RemittanceID &&
                c.MonthYear == process.MonthYear &&
                c.Period == process.Period)
                .FirstOrDefault();

            return reconcileProcess;
        }

        /// <summary>
        /// Starts the national reconcile process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        public int StartNationalReconcileProcess(NationalJetFuelInvoiceControl process)
        {
            var sucess = 0;
            try
            {
                sucess = this.DbContext.JetFuelReconciliation(process);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return sucess;
        }

        /// <summary>
        /// Reverts the reconcile process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int RevertReconcileProcess(NationalJetFuelInvoiceControl process)
        {
            var sucess = 0;
            try
            {
                sucess = this.DbContext.JetFuelReconciliation(process);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return sucess;
        }

        /// <summary>
        /// Reverts the manual reconcile process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        public int RevertManualReconcileProcess(NationalJetFuelInvoiceControl process)
        {
            var sucess = 0;
            try
            {
                sucess = this.DbContext.JetFuelRevertManualReconcile(process);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return sucess;
        }

        /// <summary>
        /// Reverts the nonconformity process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        public int RevertNonconformityProcess(NationalJetFuelInvoiceControl process)
        {
            var sucess = 0;
            try
            {
                sucess = this.DbContext.JetFuelRevertNonconformity(process);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return sucess;
        }
    }
}
