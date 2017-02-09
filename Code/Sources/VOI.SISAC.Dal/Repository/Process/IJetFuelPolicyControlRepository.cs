//------------------------------------------------------------------------
// <copyright file="IJetFuelPolicyControlRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Interface for Jet Fuel Policy Control Repository
    /// </summary>
    public interface IJetFuelPolicyControlRepository : IRepository<JetFuelPolicyControl>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="PolicyId">The policy identifier.</param>
        /// <returns>
        /// The policies.
        /// </returns>
        JetFuelPolicyControl FindById(long PolicyId);

        /// <summary>
        /// Creates the policy parameters.
        /// </summary>
        /// <param name="policyControl">The policy control.</param>
        long CreatePolicyParameters(JetFuelPolicyControl policyControl);

        /// <summary>
        /// Counts the policies by year.
        /// </summary>
        /// <returns></returns>
        int CountPoliciesByYear();

        /// <summary>
        /// Gets the paged policies history.
        /// </summary>
        /// <returns></returns>
        IList<JetFuelPolicyControl> GetPagedPoliciesHistory(int pagesize, int pagenumber);

        /// <summary>
        /// Gets the paged policies history search.
        /// </summary>
        /// <returns></returns>
        IList<JetFuelPolicyControl> GetPagedPoliciesHistorySearch();

        /// <summary>
        /// Creates the policy parameters.
        /// </summary>
        /// <param name="policyControl">The policy control.</param>
        /// <returns>List of currencies, if verify equals 0 the currency has not change type.</returns>
        IList<CurrencyTypeChage> VerifyPolicy(JetFuelPolicyControl policyControl);

        /// <summary>
        /// Cancels the jet fuel policy.
        /// </summary>
        /// <param name="policyID">The policy identifier.</param>
        /// <returns></returns>
        bool CancelJetFuelPolicy(long policyID);
    }
}
