//------------------------------------------------------------------------
// <copyright file="JetFuelPolicyRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Entities.Process;
    using Infrastructure;

    /// <summary>
    /// Operation for Jet Fuel Policy Repository
    /// </summary>
    public class JetFuelPolicyRepository : Repository<JetFuelPolicy>, IJetFuelPolicyRepository
    {
        /// <summary>
        /// JetFuelProcessRepository constructor
        /// </summary>
        /// <param name="factory">The factory.</param>
        public JetFuelPolicyRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="policyResultId">The policy Result Identifier</param>
        /// <returns>
        /// The policies.
        /// </returns>
        public JetFuelPolicy FindById(long policyResultId)
        {
            return this.DbContext.JetFuelPolicies.FirstOrDefault(c => c.PolicyResultId == policyResultId);
        }

        /// <summary>
        /// Gets the policies by policy control identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of <see cref="JetFuelPolicyDto"/>.</returns>
        public IList<JetFuelPolicy> GetPoliciesByPolicyControlId(long id)
        {
            return this.DbContext.JetFuelPolicies.Where(c => c.PolicyId == id).ToList();
        }

        /// <summary>
        /// Finds the by policy control identifier.
        /// </summary>
        /// <param name="policyId">The policy control identifier.</param>
        /// <param name="documentNumber">The document number.</param>
        /// <returns>
        /// The policy.
        /// </returns>
        public JetFuelPolicy FindByPolicyId(long policyId, string recordId)
        {
            return this.DbContext.JetFuelPolicies.FirstOrDefault(c => c.PolicyId == policyId && c.IDREG == recordId);
        }
    }
}
