//------------------------------------------------------------------------
// <copyright file="INationalJetFuelPolicyControlBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using Dto.Process;
    using Entities.Process;
    using System.Collections.Generic;

    /// <summary>
    /// Interface National Jet Fuel Policy control Business
    /// </summary>
    public interface INationalJetFuelPolicyControlBusiness
    {
        /// <summary>
        /// Finds the national policy control.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        NationalJetFuelPolicyControlDto FindNationalPolicyCTRL(NationalJetFuelPolicyControlDto policyCTRL);

        /// <summary>
        /// Creates the national policy control.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        bool CreateNationalPolicyCTRL(NationalJetFuelPolicyControlDto policyCTRL);

        /// <summary>
        /// Deletes the national policy control.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        bool DeleteNationalPolicyCTRL(NationalJetFuelPolicyControlDto policyCTRL);

        /// <summary>
        /// Updates the national policy control.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        bool UpdateNationalPolicyCTRL(NationalJetFuelPolicyControlDto policyCTRL);

        /// <summary>
        /// Gets all national policies control.
        /// </summary>
        /// <returns></returns>
        IList<NationalJetFuelPolicyControlDto> GetAllNationalPoliciesCTRL();

        /// <summary>
        /// Creates the national policy.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        long CreateNationalPolicy(NationalJetFuelPolicyControlDto policyCTRL);

        /// <summary>
        /// Validates the type change for currency.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        IList<string> ValidateTypeChangeForCurrency(NationalJetFuelPolicyControlDto policyCTRL);

        /// <summary>
        /// Cancels the national jet fuel policy.
        /// </summary>
        /// <param name="policyCTRL">The policy control.</param>
        /// <returns></returns>
        bool CancelNationalJetFuelPolicy(NationalJetFuelPolicyControlDto policyCTRL);

        #region Policies History Methods

        /// <summary>
        /// Counts the policies by year.
        /// </summary>
        /// <returns></returns>
        int CountPoliciesByYear();

        /// <summary>
        /// Gets the paged policies history.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>List of national jet fuel policies paginated.</returns>
        IList<NationalJetFuelPolicyControlDto> GetPagedPoliciesHistory(int pageSize, int pageNumber);

        /// <summary>
        /// Counts all policies filtered by the parameters given.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The number of national jet fuel policies by parameters.</returns>
        int CountAllPoliciesSeach(NationalJetFuelPolicyHistoryDto parameters);

        /// <summary>
        /// Gets the paged policies filtered by the parameters given.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The national jet fuel policies by parameters.</returns>
        IList<NationalJetFuelPolicyControlDto> GetPagedPoliciesHistorySearch(NationalJetFuelPolicyHistoryDto parameters);
        #endregion
    }
}