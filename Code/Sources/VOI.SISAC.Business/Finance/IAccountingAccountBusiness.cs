//------------------------------------------------------------------------
// <copyright file="IAccountingAccountBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Contract for AccountingAccount methods
    /// </summary>
    public interface IAccountingAccountBusiness
    {
        /// <summary>
        /// Gets all AccountingAccounts.
        /// </summary>
        /// <returns>List of AccountingAccounts.</returns>
        IList<AccountingAccountDto> GetAllAccountingAccount();

        /// <summary>
        /// Finds the AccountingAccounts by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity AccountingAccount.</returns>
        AccountingAccountDto FindAccountingAccountById(string id);

        /// <summary>
        /// Adds the AccountingAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddAccountingAccount(AccountingAccountDto entity);

        /// <summary>
        /// Deletes the AccountingAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteAccountingAccount(AccountingAccountDto entity);

        /// <summary>
        /// Updates the AccountingAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateAccountingAccount(AccountingAccountDto entity);

        /// <summary>
        /// Gets the Actives AccountingAccount.
        /// </summary>
        /// <returns>AccountingAccounts Actives.</returns>
        IList<AccountingAccountDto> GetActivesAccountingAccounts();
    }
}
