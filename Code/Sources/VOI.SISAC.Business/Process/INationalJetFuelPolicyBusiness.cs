//------------------------------------------------------------------------
// <copyright file="INationalJetFuelPolicyBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using Dto.Process;
    using System.Collections.Generic;

    /// <summary>
    /// Interface Jet Fuel Policy Business
    /// </summary>
    public interface INationalJetFuelPolicyBusiness
    {
        /// <summary>
        /// Finds the national jet fuel policy by PolicyResultID.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        NationalJetFuelPolicyDto FindNationalJetFuelPolicy(NationalJetFuelPolicyDto policy);

        /// <summary>
        /// Creates the national policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        bool CreateNationalPolicy(NationalJetFuelPolicyDto policy);

        /// <summary>
        /// Deletes the national policy by PolicyResultID
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        bool DeleteNationalPolicy(NationalJetFuelPolicyDto policy);

        /// <summary>
        /// Updates the national policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        bool UpdateNationalPolicy(NationalJetFuelPolicyDto policy);

        /// <summary>
        /// Gets all national policies.
        /// </summary>
        /// <returns></returns>
        IList<NationalJetFuelPolicyDto> GetAllNationalPolicies();

        /// <summary>
        /// Gets the unsuccessful national policies by NationalPolicyId
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        IList<NationalJetFuelPolicyDto> GetUnsuccessfulNationalPolicies(NationalJetFuelPolicyDto policy);

        /// <summary>
        /// Gets the national policies by control - NationalPolicyId
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        IList<NationalJetFuelPolicyDto> GetNationalPoliciesByCTRL(NationalJetFuelPolicyDto policy);

        /// <summary>
        /// Updates the national policies response - PolicyId and IDREG (SAP REGISTER)
        /// </summary>
        /// <param name="policies">The policies.</param>
        /// <returns></returns>
        bool UpdateNationalPoliciesResponse(List<NationalJetFuelPolicyDto> policies);

    }
}
