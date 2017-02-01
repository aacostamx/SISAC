//------------------------------------------------------------------------
// <copyright file="INationalJetFuelPolicyRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Operations for Jet Fuel Policy Control Repository
    /// </summary>
    public interface INationalJetFuelPolicyRepository : IRepository<NationalJetFuelPolicy>
    {
        /// <summary>
        /// Finds the national policy by PolicyResultID (primary key) Its the same NationalJetFuelPolicyID
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        NationalJetFuelPolicy FindNationalPolicy(NationalJetFuelPolicy policy);

        /// <summary>
        /// Finds the national policy control.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        IList<NationalJetFuelPolicy> GetNationalPoliciesControl(NationalJetFuelPolicy policy);

        /// <summary>
        /// Finds the national policy sap.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        NationalJetFuelPolicy FindNationalPolicySAP(NationalJetFuelPolicy policy);
    }
}
