//-----------------------------------------------------------------------------------
// <copyright file="INationalJetFuelInvoicePolicyBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Process;

    /// <summary>
    /// Interface for National Jet Fuel Invoice Policy Business
    /// </summary>
    public interface INationalJetFuelInvoicePolicyBusiness
    {

        /// <summary>
        /// Gets the policies by remittance identifier.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <returns></returns>
        IList<NationalJetFuelInvoicePolicyDto> GetPoliciesByRemittanceId(string remittanceId, string monthYear, string period);
        
        /// <summary>
        /// Updates the national invoice policies response - PolicyId and IDREG (SAP REGISTER)
        /// </summary>
        /// <param name="invoicePolicies">The policies.</param>
        /// <returns><c>True</c> if success otherwise <c>false</c>.</returns>
        bool UpdateNationalInvoicePolicyResponse(List<NationalJetFuelInvoicePolicyDto> invoicePolicies);
    }
}
