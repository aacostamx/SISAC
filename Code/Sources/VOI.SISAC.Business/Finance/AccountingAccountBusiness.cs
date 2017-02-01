//------------------------------------------------------------------------
// <copyright file="AccountingAccountBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.MapConfiguration;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Accounting Account Business
    /// </summary>
    public class AccountingAccountBusiness : IAccountingAccountBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The accountingAccount repository
        /// </summary>
        private readonly IAccountingAccountRepository accountingAccountRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountingAccountBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="accountingAccountRepository">The accountingAccount repository.</param>
        public AccountingAccountBusiness(IUnitOfWork unitOfWork, IAccountingAccountRepository accountingAccountRepository)
        {
            this.unitOfWork = unitOfWork;
            this.accountingAccountRepository = accountingAccountRepository;
        }

        /// <summary>
        /// Gets all AccountingAccounts.
        /// </summary>
        /// <returns>
        /// List of AccountingAccounts.
        /// </returns>
        public IList<AccountingAccountDto> GetAllAccountingAccount()
        {
            try
            {
                IList<AccountingAccount> accountingAccounts = this.accountingAccountRepository.GetAll().ToList();
                IList<AccountingAccountDto> accountingAccount = new List<AccountingAccountDto>();

                accountingAccount = Mapper.Map<IList<AccountingAccount>, IList<AccountingAccountDto>>(accountingAccounts);
                return accountingAccount;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the AccountingAccount by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity AccountingAccount.
        /// </returns>
        public AccountingAccountDto FindAccountingAccountById(string id)
        {            
            try
            {
                AccountingAccount accountingAccounts = this.accountingAccountRepository.FindById(id);
                AccountingAccountDto accountingAccountsList = new AccountingAccountDto();
                accountingAccountsList = Mapper.Map<AccountingAccount, AccountingAccountDto>(accountingAccounts);

                return accountingAccountsList;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the AccountingAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddAccountingAccount(AccountingAccountDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (this.IsAccountingAccountNumberDuplicate(entity.AccountingAccountNumber))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                AccountingAccount accountingAccountModel = new AccountingAccount();
                entity.Status = true;
                accountingAccountModel = Mapper.Map<AccountingAccount>(entity);
                this.accountingAccountRepository.Add(accountingAccountModel);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Deletes the AccountingAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteAccountingAccount(AccountingAccountDto entity)
        {
            try
            {
                AccountingAccount accountingAccountDal = this.accountingAccountRepository.FindById(entity.AccountingAccountNumber);

                if (accountingAccountDal != null)
                {
                    /*
                     * Si se quiere hacer un borrado permanente de la DB
                     * se debe de usar la siguiente instrucción:
                     * this.accountingAccountRepository.Delete(accountingAccountDal);
                    */

                    // Para borrado lógico
                    accountingAccountDal.Status = false;
                    this.accountingAccountRepository.Update(accountingAccountDal);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Updates the AccountingAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateAccountingAccount(AccountingAccountDto entity)
        {
            try
            {
                if (entity != null)
                {
                    if (entity.AccountingAccountNumber != null)
                    {
                        AccountingAccount accountingAccountModel = this.accountingAccountRepository.FindById(entity.AccountingAccountNumber);

                        if (accountingAccountModel.AccountingAccountNumber != null)
                        {
                            accountingAccountModel.AccountingAccountName = entity.AccountingAccountName;
                            this.accountingAccountRepository.Update(accountingAccountModel);
                            this.unitOfWork.Commit();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets the actives AccountingAccounts.
        /// </summary>
        /// <returns>AccountingAccounts Actives.</returns>
        public IList<AccountingAccountDto> GetActivesAccountingAccounts()
        {
            try
            {
                IList<AccountingAccount> accountingAccounts = this.accountingAccountRepository.GetActivesAccountingAccounts().ToList();
                IList<AccountingAccountDto> accountingAccount = new List<AccountingAccountDto>();

                accountingAccount = Mapper.Map<IList<AccountingAccount>, IList<AccountingAccountDto>>(accountingAccounts);
                return accountingAccount;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Is AccountingAccountNumber Duplicate
        /// </summary>
        /// <param name="accountingAccountNumber">accounting Account Number</param>
        /// <returns>true or false</returns>
        private bool IsAccountingAccountNumberDuplicate(string accountingAccountNumber)
        {
            AccountingAccount encontrado = this.accountingAccountRepository.FindById(accountingAccountNumber);
            return encontrado != null ? true : false;
        }
    }
}
