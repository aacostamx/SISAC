//-----------------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoicePolicyRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System.Linq;
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// National Jet Fuel Invoice Policy Repository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.Repository{VOI.SISAC.Entities.Process.NationalJetFuelInvoicePolicy}"/>
    /// <seealso cref="VOI.SISAC.Dal.Repository.Process.INationalJetFuelInvoicePolicyRepository"/>
    public class NationalJetFuelInvoicePolicyRepository : Repository<NationalJetFuelInvoicePolicy>, INationalJetFuelInvoicePolicyRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceControlRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalJetFuelInvoicePolicyRepository(IDbFactory factory)
            : base(factory) { }

        /// <summary>
        /// Finds the national jet fuel invoice policy.
        /// </summary>
        /// <param name="policyResultId"></param>
        /// <returns>
        /// The <see cref="NationalJetFuelInvoicePolicy" />.
        /// </returns>
        public NationalJetFuelInvoicePolicy FindNationalJetFuelInvoicePolicy(long policyResultId)
        {
            return this.DbContext.NationalJetFuelInvoicePolicy.FirstOrDefault(c => c.PolicyResultId == policyResultId);
        }


        /// <summary>
        /// Gets the invoice policies by remittance identifier.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <param name="monthYear"></param>
        /// <param name="period"></param>
        /// <returns>
        /// A collection of Invoice policy.
        /// </returns>
        public IList<NationalJetFuelInvoicePolicy> GetInvoicePoliciesByRemittanceId(string remittanceId, string monthYear, string period)
        {
            return this.DbContext.NationalJetFuelInvoicePolicy.Where(c => c.RemittanceId == remittanceId 
                                                                       && c.MonthYear == monthYear
                                                                       && c.Period == period).ToList();
        }

        /// <summary>
        /// Finds the invoice policy sap.
        /// </summary>
        /// <param name="invoicePolicy">The invoice policy.</param>
        /// <returns>
        /// The <see cref="NationalJetFuelInvoicePolicy"/>.
        /// </returns>
        public NationalJetFuelInvoicePolicy FindInvoicePolicySAP(NationalJetFuelInvoicePolicy invoicePolicy)
        {
            return this.DbContext.NationalJetFuelInvoicePolicy.FirstOrDefault(c => c.RemittanceId == invoicePolicy.RemittanceId
                                                                       && c.MonthYear == invoicePolicy.MonthYear
                                                                       && c.Period == invoicePolicy.Period
                                                                                    && c.IDREG == invoicePolicy.IDREG);
        }
    }
}
