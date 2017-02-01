//------------------------------------------------------------------------
// <copyright file="IJetFuelPolicyRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Operations for Jet Fuel Policy Control Repository
    /// </summary>
    public interface IJetFuelPolicyRepository : IRepository<JetFuelPolicy>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="PolicyResultId">The policy result identifier.</param>
        /// <returns>
        /// The policies.
        /// </returns>
        JetFuelPolicy FindById(long PolicyResultId);

        /// <summary>
        /// Gets the policies by policy control identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of <see cref="JetFuelPolicyDto"/>.</returns>
        IList<JetFuelPolicy> GetPoliciesByPolicyControlId(long id);

        /// <summary>
        /// Finds the policy by policy control identifier.
        /// </summary>
        /// <param name="policyId">The policy control identifier.</param>
        /// <param name="documentNumber">The document number.</param>
        /// <returns>
        /// The policy.
        /// </returns>
        JetFuelPolicy FindByPolicyId(long policyId, string recordId);
    }
}
