//------------------------------------------------------------------------
// <copyright file="INationalJetFuelInvoicePolicyRepository.cs" company="Volaris">
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
    /// Interface for National Jet Fuel Invoice Policy
    /// </summary>
    public interface INationalJetFuelInvoicePolicyRepository : IRepository<NationalJetFuelInvoicePolicy>
    {
        /// <summary>
        /// Finds the national jet fuel invoice policy.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <returns>The <see cref="NationalJetFuelInvoicePolicy"/>.</returns>
        NationalJetFuelInvoicePolicy FindNationalJetFuelInvoicePolicy(long policyResultId);


        /// <summary>
        /// Gets the invoice policies by remittance identifier.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        IList<NationalJetFuelInvoicePolicy> GetInvoicePoliciesByRemittanceId(string remittanceId, string monthYear, string period);

        /// <summary>
        /// Finds the invoice policy sap.
        /// </summary>
        /// <param name="invoicePolicy">The invoice policy.</param>
        /// <returns>
        /// The <see cref="NationalJetFuelInvoicePolicy" />.
        /// </returns>
        NationalJetFuelInvoicePolicy FindInvoicePolicySAP(NationalJetFuelInvoicePolicy invoicePolicy);
    }
}
