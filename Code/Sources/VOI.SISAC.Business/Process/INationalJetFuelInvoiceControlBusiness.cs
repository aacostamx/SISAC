//------------------------------------------------------------------------
// <copyright file="INationalJetFuelInvoiceControlBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System.Collections.Generic;
    using System.Data;
    using VOI.SISAC.Business.Dto.Process;

    /// <summary>
    /// Interface for National Jet Fuel Invoice Control Business
    /// </summary>
    public interface INationalJetFuelInvoiceControlBusiness
    {
        /// <summary>
        /// Gets the national invoice information paginated.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of <see cref="NationalJetFuelInvoiceControlDto"/>.</returns>
        IList<NationalJetFuelInvoiceControlDto> GetNationalInvoicePaginated(int pageNumber, int pageSize);

        /// <summary>
        /// Gets the invoice control.
        /// </summary>
        /// <param name="invoiceKey">The invoice key.</param>
        /// <returns></returns>
        NationalJetFuelInvoiceControlDto GetInvoiceControl(NationalJetFuelInvoiceControlDto invoiceKey);

        /// <summary>
        /// Counts the national jet fuel invoice records.
        /// </summary>
        /// <returns>The total number of national jet fuel invoices.</returns>
        int CountNationalInvoiceRecords();

        /// <summary>
        /// Validates the remittance identifier.
        /// </summary>
        /// <param name="remittanceInfo">The remittance information.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <returns></returns>
        IList<string> ValidateRemittanceID(DataTable remittanceInfo, string airlineCode);

        /// <summary>
        /// Validates the manual reconcile.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        IList<string> ValidateManualReconcile(DataTable reconcileInfo);

        /// <summary>
        /// Validates the nonconformity.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        IList<string> ValidateNonconformity(DataTable reconcileInfo);

        /// <summary>
        /// Deletes the national invoice policy.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <returns></returns>
        int DeletedNationalInvoicePolicy(NationalJetFuelInvoiceControlDto invoice);

        /// <summary>
        /// Validates the invoice policy.
        /// </summary>
        /// <param name="invoiceControl">The invoice control.</param>
        /// <returns>
        /// -1: An error during the call of the store procedure.
        /// 0: No errors found.
        /// 1: Error. The policy has been created.
        /// 2: Error caused because there are errors in the remittance.
        /// </returns>
        int ValidateInvoicePolicy(NationalJetFuelInvoiceControlDto invoiceControl);

        /// <summary>
        /// Creates the policies for remittance.
        /// </summary>
        /// <param name="invoiceControl">The invoice control.</param>
        /// <returns>
        /// Greater than zero. The process was completed successfuly.
        /// Less or equal than zero. An error in the process.
        /// </returns>
        int CreatePoliciesForRemittance(NationalJetFuelInvoiceControlDto invoiceControl);

        /// <summary>
        /// Gets the national jet fuel reconcile control.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        IList<NationalJetFuelInvoiceControlDto> GetTopNationalJetFuelReconcileControl(string date);

        /// <summary>
        /// Starts the national reconcile process.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        int StartNationalReconcileProcess(NationalJetFuelInvoiceControlDto processDto);

        /// <summary>
        /// Reverts the reconcile process.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        int RevertReconcileProcess(NationalJetFuelInvoiceControlDto processDto);

        /// <summary>
        /// Reverts the manual reconcile process.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        int RevertManualReconcileProcess(NationalJetFuelInvoiceControlDto processDto);

        /// <summary>
        /// Reverts the nonconformity process.
        /// </summary>
        /// <param name="processDto">The process dto.</param>
        /// <returns></returns>
        int RevertNonconformityProcess(NationalJetFuelInvoiceControlDto processDto);

        /// <summary>
        /// Closes the nonconformity.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        bool CloseNonconformity(NationalJetFuelInvoiceControlDto process);

        /// <summary>
        /// Opens the nonconformity.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        bool OpenNonconformity(NationalJetFuelInvoiceControlDto process);
    }
}
