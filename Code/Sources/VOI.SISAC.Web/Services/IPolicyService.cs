//------------------------------------------------------------------------
// <copyright file="IPolicyService.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Services
{
    using System.Collections.Generic;
    using Models.VO.Process;

    /// <summary>
    /// Policy Service
    /// </summary>
    public interface IPolicyService
    {
        /// <summary>
        /// Sends the jet fuel policy to the service.
        /// </summary>
        /// <param name="policyId">The policy identifier.</param>
        /// <param name="information">The information.</param>
        /// <returns>The response from the service.</returns>
        IList<JetFuelPolicyVO> SendPolicyToService(long policyId, ref string information, ref string status);

        /// <summary>
        /// Sends the national jet fuel policy to the service.
        /// </summary>
        /// <param name="nationalPolicyId">The national policy identifier.</param>
        /// <param name="information">The information.</param>
        /// <returns>The response from the service.</returns>
        IList<NationalJetFuelPolicyVO> SendNationalPolicyToService(long nationalPolicyId, ref string information, ref string status);


        /// <summary>
        /// Sends the national invoice policy to service.
        /// </summary>
        /// <param name="remittanceId">The remittance identifier.</param>
        /// <param name="monthYear">The month year.</param>
        /// <param name="period">The period.</param>
        /// <param name="information">The information.</param>
        /// <returns></returns>
        IList<NationalJetFuelInvoicePolicyVO> SendNationalInvoicePolicyToService(string remittanceId, string monthYear, string period, ref string information);
    }
}
