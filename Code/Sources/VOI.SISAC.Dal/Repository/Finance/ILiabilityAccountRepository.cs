//------------------------------------------------------------------------
// <copyright file="ILiabilityAccountRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Interface for the specific operations in LiabilityAccount
    /// </summary>
    public interface ILiabilityAccountRepository : IRepository<LiabilityAccount>
    {
        /// <summary>
        /// Finds the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Liability Account</returns>
        LiabilityAccount FindById(string id);

        /// <summary>
        /// Gets the active entity.
        /// </summary>
        /// <returns>ICollection Liability Account</returns>
        ICollection<LiabilityAccount> GetActiveLiabilityAccounts();

        /// <summary>
        /// Validated if the liability accounts exist.
        /// </summary>
        /// <param name="liabilityAccountNumbers">The liability account numbers to validated.</param>
        /// <returns>Returns a list with the Liability Account Numbers that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfLiabilityAccountExist(IList<string> liabilityAccountNumbers);
    }
}
