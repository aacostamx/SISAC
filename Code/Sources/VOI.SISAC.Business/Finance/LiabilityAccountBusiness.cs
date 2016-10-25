//------------------------------------------------------------------------
// <copyright file="LiabilityAccountBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    /// Liability Account Business
    /// </summary>
    public class LiabilityAccountBusiness : ILiabilityAccountBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The LiabilityAccount repository
        /// </summary>
        private readonly ILiabilityAccountRepository liabilityAccountRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LiabilityAccountBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="liabilityAccountRepository">The LiabilityAccount repository.</param>
        public LiabilityAccountBusiness(IUnitOfWork unitOfWork, ILiabilityAccountRepository liabilityAccountRepository)
        {
            this.unitOfWork = unitOfWork;
            this.liabilityAccountRepository = liabilityAccountRepository;
        }

        /// <summary>
        /// Gets all LiabilityAccounts.
        /// </summary>
        /// <returns>
        /// List of LiabilityAccounts.
        /// </returns>
        public IList<LiabilityAccountDto> GetAllLiabilityAccount()
        {
            try
            {
                IList<LiabilityAccount> liabilityAccounts = this.liabilityAccountRepository.GetAll().ToList();
                IList<LiabilityAccountDto> liabilityAccount = new List<LiabilityAccountDto>();

                liabilityAccount = Mapper.Map<IList<LiabilityAccount>, IList<LiabilityAccountDto>>(liabilityAccounts);
                return liabilityAccount;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the LiabilityAccount by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity LiabilityAccount.
        /// </returns>
        public LiabilityAccountDto FindLiabilityAccountById(string id)
        {            
            try
            {
                LiabilityAccount liabilityAccounts = this.liabilityAccountRepository.FindById(id);
                LiabilityAccountDto liabilityAccountsList = new LiabilityAccountDto();
                liabilityAccountsList = Mapper.Map<LiabilityAccount, LiabilityAccountDto>(liabilityAccounts);

                return liabilityAccountsList;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the LiabilityAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddLiabilityAccount(LiabilityAccountDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (this.IsLiabilityAccountNumberDuplicate(entity.LiabilityAccountNumber))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                LiabilityAccount liabilityAccountModel = new LiabilityAccount();
                entity.Status = true;
                liabilityAccountModel = Mapper.Map<LiabilityAccount>(entity);
                this.liabilityAccountRepository.Add(liabilityAccountModel);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }        

        /// <summary>
        /// Deletes the LiabilityAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteLiabilityAccount(LiabilityAccountDto entity)
        {
            try
            {
                LiabilityAccount liabilityAccountDal = this.liabilityAccountRepository.FindById(entity.LiabilityAccountNumber);

                if (liabilityAccountDal != null)
                {
                    /*
                     * Si se quiere hacer un borrado permanente de la DB
                     * se debe de usar la siguiente instrucción:
                     * this.accountingAccountRepository.Delete(accountingAccountDal);
                    */

                    // Para borrado lógico
                    liabilityAccountDal.Status = false;
                    this.liabilityAccountRepository.Update(liabilityAccountDal);
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
        /// Updates the LiabilityAccount.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateLiabilityAccount(LiabilityAccountDto entity)
        {
            try
            {
                if (entity != null)
                {
                    if (entity.LiabilityAccountNumber != null)
                    {
                        LiabilityAccount liabilityAccountModel = this.liabilityAccountRepository.FindById(entity.LiabilityAccountNumber);

                        if (liabilityAccountModel.LiabilityAccountNumber != null)
                        {
                            liabilityAccountModel.LiabilityAccountName = entity.LiabilityAccountName;
                            this.liabilityAccountRepository.Update(liabilityAccountModel);
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
        /// Gets the actives LiabilityAccounts.
        /// </summary>
        /// <returns>LiabilityAccounts Actives.</returns>
        public IList<LiabilityAccountDto> GetActivesLiabilityAccounts()
        {
            try
            {
                IList<LiabilityAccount> liabilityAccounts = this.liabilityAccountRepository.GetActiveLiabilityAccounts().ToList();
                IList<LiabilityAccountDto> liabilityAccount = new List<LiabilityAccountDto>();

                liabilityAccount = Mapper.Map<IList<LiabilityAccount>, IList<LiabilityAccountDto>>(liabilityAccounts);
                return liabilityAccount;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Is Liability Account Number Duplicate
        /// </summary>
        /// <param name="liabilityAccountNumber">string liabilityAccountNumber</param>
        /// <returns>true or false</returns>
        private bool IsLiabilityAccountNumberDuplicate(string liabilityAccountNumber)
        {
            LiabilityAccount encontrado = this.liabilityAccountRepository.FindById(liabilityAccountNumber);
            return encontrado != null ? true : false;
        }
    }
}
