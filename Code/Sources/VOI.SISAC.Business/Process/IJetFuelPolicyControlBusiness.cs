//------------------------------------------------------------------------
// <copyright file="IJetFuelPolicyControlBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Process;

    /// <summary>
    /// Interface Jet Fuel Policy control Business
    /// </summary>
    public interface IJetFuelPolicyControlBusiness
    {
        /// <summary>
        /// Finds by identifier.
        /// </summary>
        /// <param name="policyId">The policy identifier.</param>
        /// <returns>
        /// The policy.
        /// </returns>
        JetFuelPolicyControlDto FindById(long policyId);

        /// <summary>
        /// Creates the specified control policy.
        /// </summary>
        /// <param name="policyControl">The control policy.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        bool Create(JetFuelPolicyControlDto policyControl);

        /// <summary>
        /// Deletes the specified control policy.
        /// </summary>
        /// <param name="policyControlId">The policy identifier.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        bool Delete(long policyControlId);

        /// <summary>
        /// Updates the specified policy control.
        /// </summary>
        /// <param name="policyControl">The policy.</param>
        /// <returns>True if success otherwise false.</returns>
        bool Update(JetFuelPolicyControlDto policyControl);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of <see cref="JetFuelPolicyControlDto"/>.</returns>
        IList<JetFuelPolicyControlDto> GetAll();

        /// <summary>
        /// Creates the specified control policy.
        /// </summary>
        /// <param name="policyControl">The control policy.</param>
        /// <returns>
        /// The policy identifier asigned. If the number is negative, the insertion faild.
        /// </returns>
        long CreatePolicy(JetFuelPolicyControlDto policyControl);

        /// <summary>
        /// Counts the policies by year.
        /// </summary>
        /// <returns></returns>
        int CountPoliciesByYear();

        /// <summary>
        /// Gets the paged policies history.
        /// </summary>
        /// <param name="Pagesize">The pagesize.</param>
        /// <param name="Pagenumber">The pagenumber.</param>
        /// <returns></returns>
        IList<JetFuelPolicyControlDto> GetPagedPoliciesHistory(int Pagesize, int Pagenumber);

        /// <summary>
        /// Counts all policies seach.
        /// </summary>
        /// <param name="policies">The policies.</param>
        /// <returns></returns>
        int CountAllPoliciesSeach(JetFuelPoliciesHistoryDto policies);

        /// <summary>
        /// Gets the paged policies history search.
        /// </summary>
        /// <param name="policies">The policies.</param>
        /// <returns></returns>
        IList<JetFuelPolicyControlDto> GetPagedPoliciesHistorySearch(JetFuelPoliciesHistoryDto policies);

        /// <summary>
        /// Validates the type change for currency.
        /// </summary>
        /// <param name="policyControl">The policy control.</param>
        /// <returns>List of Currencies that mmiss its change type.</returns>
        IList<string> ValidateTypeChangeForCurrency(JetFuelPolicyControlDto policyControl);

        /// <summary>
        /// Cancels the jet fuel policy.
        /// </summary>
        /// <param name="policyID">The policy identifier.</param>
        /// <returns></returns>
        bool CancelJetFuelPolicy(long policyID);
    }
}