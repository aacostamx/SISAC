//------------------------------------------------------------------------
// <copyright file="LiabilityAccountRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// LiabilityAccount Repository
    /// </summary>
    public class LiabilityAccountRepository : Repository<LiabilityAccount>, ILiabilityAccountRepository
    { 
        /// <summary>
        /// Initializes a new instance of the <see cref="LiabilityAccountRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public LiabilityAccountRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region ILiabilityAccountRepository Members
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>LiabilityAccount Entity.</returns>
        public LiabilityAccount FindById(string id)
        {
            var liabilityAccounts = this.DbContext.LiabilityAccounts.Where(c => c.LiabilityAccountNumber == id).FirstOrDefault();
            return liabilityAccounts;
        }

        /// <summary>
        /// Gets the Actives LiabilityAccounts.
        /// </summary>
        /// <returns>LiabilityAccounts marked as Actives.</returns>
        public IList<LiabilityAccount> GetActivesLiabilityAccounts()
        {
            return this.DbContext.LiabilityAccounts.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Gets the active entity.
        /// </summary>
        /// <returns>Collection of liability account.</returns>
        public ICollection<LiabilityAccount> GetActiveLiabilityAccounts()
        {
            return this.DbContext.LiabilityAccounts.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validated if the liability accounts exist.
        /// </summary>
        /// <param name="liabilityAccountNumbers">The liability account numbers to validated.</param>
        /// <returns>Returns a list with the Liability Account Numbers that do not exist.</returns>
        public IList<string> ValidatedIfLiabilityAccountExist(IList<string> liabilityAccountNumbers)
        {
            IList<string> notFound = new List<string>();
            IList<LiabilityAccount> liabilityAccountList = this.DbContext.LiabilityAccounts.Where(c => c.Status).ToList();

            notFound = liabilityAccountNumbers.Except(liabilityAccountList.Select(c => c.LiabilityAccountNumber)).ToList();
            return notFound;
        }
        #endregion
    }
}
