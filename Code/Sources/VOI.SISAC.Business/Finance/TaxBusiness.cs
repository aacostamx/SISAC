//------------------------------------------------------------------------
// <copyright file="TaxBusiness.cs" company="Volaris">
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
    using Entities.Finance;
    using ExceptionBusiness;
    using MapConfiguration;
    using Resources;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using DAL = VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Business Interface Implementation
    /// </summary>
    public class TaxBusiness : ITaxBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The Tax repository
        /// </summary>
        private readonly ITaxRepository taxRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="taxRepository">The tax repository.</param>
        public TaxBusiness(IUnitOfWork unitOfWork, ITaxRepository taxRepository)
        {
            this.unitOfWork = unitOfWork;
            this.taxRepository = taxRepository;
        }

        /// <summary>
        /// Gets all Taxes.
        /// </summary>
        /// <returns>
        /// List of Taxes.
        /// </returns>
        public IList<TaxDto> GetAllTax()
        {
            try
            {
                IList<Tax> taxes = this.taxRepository.GetAll().ToList();
                IList<TaxDto> taxList = new List<TaxDto>();
                taxList = Mapper.Map<IList<Tax>, IList<TaxDto>>(taxes);
                return taxList.ToList();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the Tax by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity Tax.
        /// </returns>
        public TaxDto FindTaxById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            try
            {
                TaxDto taxList = new TaxDto();
                DAL.Tax tax = this.taxRepository.FindById(id);
                taxList = Mapper.Map<Tax, TaxDto>(tax);
                return taxList;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the Tax.
        /// </summary>
        /// <param name="taxDto">The entity tax.</param>
        /// <returns>true if was added else false</returns>
        public bool AddTax(TaxDto taxDto)
        {
            if (taxDto == null)
            {
                return false;
            }

            if (this.IsTaxCodeDuplicated(taxDto.TaxCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                DAL.Tax taxModel = new DAL.Tax();
                taxModel = Mapper.Map<TaxDto, Tax>(taxDto);
                taxModel.Status = true;
                this.taxRepository.Add(taxModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Deletes the Tax.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteTax(TaxDto entity)
        {
            try
            {
                DAL.Tax taxDelete = this.taxRepository.FindById(entity.TaxCode);
                taxDelete.Status = false;
                this.taxRepository.Update(taxDelete);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Updates the Tax.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateTax(TaxDto entity)
        {
            try
            {
                if (entity != null)
                {
                    if (entity.TaxCode != null)
                    {
                        DAL.Tax taxModel = this.taxRepository.FindById(entity.TaxCode);

                        if (taxModel.TaxCode != null)
                        {
                            taxModel.TaxName = entity.TaxName;
                            this.taxRepository.Update(taxModel);
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
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the actives Taxes.
        /// </summary>
        /// <returns>Taxes Actives.</returns>
        public IList<TaxDto> GetActivesTaxes()
        {
            try
            {
                IList<Tax> taxes = this.taxRepository.GetActivesTaxes().ToList();
                IList<TaxDto> taxList = new List<TaxDto>();

                taxList = Mapper.Map<IList<Tax>, IList<TaxDto>>(taxes);
                return taxList.ToList();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Validates if a duplicated tax code exists.
        /// </summary>
        /// <param name="taxCode">tax Code parameter</param>
        /// <returns>true is duplicated</returns>
        private bool IsTaxCodeDuplicated(string taxCode)
        {
            Tax taxes = this.taxRepository.FindById(taxCode);
            return taxes != null ? true : false;
        }
    }
}
