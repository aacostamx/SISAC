//------------------------------------------------------------------------
// <copyright file="ILiabilityAccountBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Contract for LiabilityAccount methods
    /// </summary>
    public interface ILiabilityAccountBusiness
    {
        /// <summary>
        /// Gets all LiabilityAccounts.
        /// </summary>
        /// <returns>List of LiabilityAccounts.</returns>
        IList<LiabilityAccountDto> GetAllLiabilityAccount();

        /// <summary>
        /// Finds the LiabilityAccounts by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity LiabilityAccount.</returns>
        LiabilityAccountDto FindLiabilityAccountById(string id);

        /// <summary>
        /// Adds the LiabilityAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddLiabilityAccount(LiabilityAccountDto entity);

        /// <summary>
        /// Deletes the LiabilityAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteLiabilityAccount(LiabilityAccountDto entity);

        /// <summary>
        /// Updates the LiabilityAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateLiabilityAccount(LiabilityAccountDto entity);

        /// <summary>
        /// Gets the Actives LiabilityAccount.
        /// </summary>
        /// <returns>LiabilityAccounts Actives.</returns>
        IList<LiabilityAccountDto> GetActivesLiabilityAccounts();
    }
}
