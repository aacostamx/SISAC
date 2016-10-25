//------------------------------------------------------------------------
// <copyright file="IAccountingAccountRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// AccountingAccount Repository Interface
    /// </summary>
    public interface IAccountingAccountRepository : IRepository<AccountingAccount>
    {
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>AccountingAccount Entity.</returns>
        AccountingAccount FindById(string id);

        /// <summary>
        /// Gets the actives AccountingAccounts.
        /// </summary>
        /// <returns>AccountingAccount marked as Actives.</returns>
        IList<AccountingAccount> GetActivesAccountingAccounts();

        /// <summary>
        /// Validated if the accounting accounts exist.
        /// </summary>
        /// <param name="accountingAccountNumbers">The accounting account numbers to validated.</param>
        /// <returns>Returns a list with the Accounting Account Numbers that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfAccountingAccountExist(IList<string> accountingAccountNumbers);
    }
}
