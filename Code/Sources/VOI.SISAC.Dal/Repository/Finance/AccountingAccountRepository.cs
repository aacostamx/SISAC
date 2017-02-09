//------------------------------------------------------------------------
// <copyright file="AccountingAccountRepository.cs" company="AACOSTA">
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
    /// AccountingAccount Repository
    /// </summary>
    public class AccountingAccountRepository : Repository<AccountingAccount>, IAccountingAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountingAccountRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AccountingAccountRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region IAccountingAccountRepository Members
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>AccountingAccount Entity.</returns>
        public AccountingAccount FindById(string id)
        {
            var accountingAccounts = this.DbContext.AccountingAccounts.Where(c => c.AccountingAccountNumber == id).FirstOrDefault();
            return accountingAccounts;
        }

        /// <summary>
        /// Gets the Actives AccountingAccounts.
        /// </summary>
        /// <returns>AccountingAccounts marked as Actives.</returns>
        public IList<AccountingAccount> GetActivesAccountingAccounts()
        {
            return this.DbContext.AccountingAccounts.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validated if the accounting accounts exist.
        /// </summary>
        /// <param name="accountingAccountNumbers">The accounting account numbers to validated.</param>
        /// <returns>Returns a list with the Accounting Account Numbers that do not exist.</returns>
        public IList<string> ValidatedIfAccountingAccountExist(IList<string> accountingAccountNumbers)
        {
            IList<string> notFound = new List<string>();
            IList<AccountingAccount> accountingAccountList = this.DbContext.AccountingAccounts.Where(c => c.Status).ToList();

            notFound = accountingAccountNumbers.Except(accountingAccountList.Select(c => c.AccountingAccountNumber)).ToList();
            return notFound;
        }
        #endregion
    }
}
