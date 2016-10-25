//------------------------------------------------------------------------
// <copyright file="INationalJetFuelPolicyControlRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Interface for Jet Fuel Policy Control Repository
    /// </summary>
    public interface INationalJetFuelPolicyControlRepository : IRepository<NationalJetFuelPolicyControl>
    {
        /// <summary>
        /// Finds the national policy control.
        /// </summary>
        /// <param name="policyCtrl">The policy control.</param>
        /// <returns></returns>
        NationalJetFuelPolicyControl FindNationalPolicyControl(NationalJetFuelPolicyControl policyCtrl);

        /// <summary>
        /// Creates the policy parameters.
        /// </summary>
        /// <param name="policyCtrl">The policy control.</param>
        /// <returns></returns>
        long CreatePolicyParams(NationalJetFuelPolicyControl policyCtrl);

        /// <summary>
        /// Counts all policies.
        /// </summary>
        /// <returns></returns>
        int CountPoliciesByYear();

        /// <summary>
        /// Gets the paged policies history.
        /// </summary>
        /// <returns></returns>
        IList<NationalJetFuelPolicyControl> GetPagedPoliciesHistory(int pagesize, int pagenumber);

        /// <summary>
        /// Gets the paged policies history search.
        /// </summary>
        /// <returns></returns>
        IList<NationalJetFuelPolicyControl> GetPagedPoliciesHistorySearch();

        /// <summary>
        /// Checks the policy currencies.
        /// </summary>
        /// <param name="policyCtrl">The policy control.</param>
        /// <returns></returns>
        IList<CurrencyTypeChage> CheckPolicyCurrencies(NationalJetFuelPolicyControl policyCtrl);

        /// <summary>
        /// Cancels the national jet fuel policy.
        /// </summary>
        /// <param name="policyID">The policy identifier.</param>
        /// <returns></returns>
        bool CancelNationalJetFuelPolicy(long policyID);
    }
}
