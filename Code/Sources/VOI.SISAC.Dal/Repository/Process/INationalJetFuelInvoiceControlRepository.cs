//------------------------------------------------------------------------
// <copyright file="INationalJetFuelInvoiceControlRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Interface for National Jet Fuel Invoice Control Repository
    /// </summary>
    public interface INationalJetFuelInvoiceControlRepository : IRepository<NationalJetFuelInvoiceControl>
    {
        /// <summary>
        /// Finds the national jet fuel invoice control.
        /// </summary>
        /// <param name="invoiceCTRL">The invoice control.</param>
        /// <returns></returns>
        NationalJetFuelInvoiceControl FindNationalJetFuelInvoiceControl(NationalJetFuelInvoiceControl invoiceCTRL);

        /// <summary>
        /// Validates the remittance identifier.
        /// </summary>
        /// <param name="remittanceInfo">The remittance information.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <returns></returns>
        IList<RemittanceIDValidate> ValidateRemittanceID(DataTable remittanceInfo, string airlineCode);

        /// <summary>
        /// Validates the manual reconcile.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        IList<RemittanceIDValidate> ValidateManualReconcile(DataTable reconcileInfo);

        /// <summary>
        /// Validates the nonconformity.
        /// </summary>
        /// <param name="reconcileInfo">The reconcile information.</param>
        /// <returns></returns>
        IList<RemittanceIDValidate> ValidateNonconformity(DataTable reconcileInfo);

        ///// <summary>
        ///// Counts the invoices in remittens.
        ///// </summary>
        ///// <param name="remittenceId">The remittence identifier.</param>
        ///// <returns>Number of invoice in the remittence.</returns>
        //int CountInvoicesInRemittence(string remittenceId);

        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <param name="take">The number of elements to get.</param>
        /// <param name="skip">Indicates in which element start to count .</param>
        /// <returns>
        /// The entity's records paginated.
        /// </returns>
        IList<NationalJetFuelInvoiceControl> GetPaginatedData(int take, int skip);

        /// <summary>
        /// Creates the policy.
        /// </summary>
        /// <param name="invoiceControl">The invoice control.</param>
        /// <returns>
        /// Greater than zero. The process was completed successfuly.
        /// Less or equal than zero. An error in the process.
        /// </returns>
        int CreatePolicyDocument(NationalJetFuelInvoiceControl invoiceControl);

        /// <summary>
        /// Deleteds the invoice policy.
        /// </summary>
        /// <param name="invoiceCTRL">The invoice control.</param>
        /// <returns></returns>
        int DeletedInvoicePolicy(NationalJetFuelInvoiceControl invoiceCTRL);

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
        int ValidateInvoicePolicy(NationalJetFuelInvoiceControl invoiceControl);

        /// <summary>
        /// Gets the national jet fuel reconcile control.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        IList<NationalJetFuelInvoiceControl> GetTopNationalJetFuelReconcileControl(string date);

        /// <summary>
        /// Finds the national jet fuel process no tracking.
        /// </summary>
        /// <param name="nationalJetFuelInvoiceControl">The national jet fuel invoice control.</param>
        /// <returns></returns>
        NationalJetFuelInvoiceControl FindNationalReconcileProcessNoTracking(NationalJetFuelInvoiceControl nationalJetFuelInvoiceControl);

        /// <summary>
        /// Starts the national reconcile process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        int StartNationalReconcileProcess(NationalJetFuelInvoiceControl process);

        /// <summary>
        /// Reverts the reconcile process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        int RevertReconcileProcess(NationalJetFuelInvoiceControl process);

        /// <summary>
        /// Reverts the manual reconcile process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        int RevertManualReconcileProcess(NationalJetFuelInvoiceControl process);

        /// <summary>
        /// Reverts the nonconformity process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns></returns>
        int RevertNonconformityProcess(NationalJetFuelInvoiceControl process);
    }
}
