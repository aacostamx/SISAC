//------------------------------------------------------------------------
// <copyright file="ReconciliationToleranceBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Catalog;
    using VOI.SISAC.Entities.Catalog;
    using Dto.Finances;
    using Entities.Finance;
    using System.Linq;

    public class ReconciliationToleranceBusiness : IReconciliationToleranceBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Exchange Rates Repository
        /// </summary>
        private readonly IReconciliationToleranceRepository toleranceRepository;

        /// <summary>
        /// ExchangeRatesBusiness Controller
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="exchangeRepository"></param>
        public ReconciliationToleranceBusiness(IUnitOfWork unitOfWork, IReconciliationToleranceRepository toleranceRepository)
        {
            this.unitOfWork = unitOfWork;
            this.toleranceRepository = toleranceRepository;
        }

        /// <summary>
        /// Adds the reconciliation tolerance.
        /// </summary>
        /// <param name="toleranceDto">The tolerance dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException">
        /// 10
        /// or
        /// </exception>
        public bool AddReconciliationTolerance(ReconciliationToleranceDto toleranceDto)
        {
            bool added = false;
            if (toleranceDto == null)
            {
                return added;
            }
            else if (this.IsToleranceDuplicated(toleranceDto))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                var entity = new ReconciliationTolerance()
                {
                    ServiceCode = toleranceDto.ServiceCode,
                    CurrencyCode = toleranceDto.CurrencyCode,
                    ToleranceTypeCode = toleranceDto.ToleranceTypeCode,
                    ToleranceValue = toleranceDto.ToleranceValue,
                    Status = true
                };

                this.toleranceRepository.Add(entity);
                this.unitOfWork.Commit();
                added = true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
            return added;
        }

        /// <summary>
        /// Deletes the reconciliation tolerance.
        /// </summary>
        /// <param name="toleranceDto">The tolerance dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool DeleteReconciliationTolerance(ReconciliationToleranceDto toleranceDto)
        {
            bool deleted = false;

            if (toleranceDto == null)
            {
                return deleted;
            }

            try
            {
                var entity = this.toleranceRepository.FindByIdNoTracking(new ReconciliationTolerance(toleranceDto.ServiceCode, toleranceDto.CurrencyCode, toleranceDto.ToleranceTypeCode));

                if (entity == null)
                {
                    return deleted;
                }

                entity.Status = false;
                this.toleranceRepository.Update(entity);
                this.unitOfWork.Commit();
                deleted = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }

            return deleted;
        }

        /// <summary>
        /// Finds the exchange rate by identifier.
        /// </summary>
        /// <param name="toleranceDto">The tolerance dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public ReconciliationToleranceDto FindReconciliationToleranceById(ReconciliationToleranceDto toleranceDto)
        {
            ReconciliationToleranceDto exchange = new ReconciliationToleranceDto();

            if (toleranceDto == null)
            {
                return exchange;
            }

            try
            {
                var entity = this.toleranceRepository.FindByIdNoTracking(new ReconciliationTolerance(toleranceDto.ServiceCode, toleranceDto.CurrencyCode, toleranceDto.ToleranceTypeCode));
                exchange = Mapper.Map<ReconciliationToleranceDto>(entity);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }
            return exchange;
        }

        /// <summary>
        /// Gets all actives reconciliation tolerances.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<ReconciliationToleranceDto> GetAllActivesReconciliationTolerances()
        {
            IList<ReconciliationToleranceDto> actives = new List<ReconciliationToleranceDto>();
            try
            {
                var entity = this.toleranceRepository.GetActivesReconciliationTolerances().ToList();
                actives = Mapper.Map<IList<ReconciliationToleranceDto>>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return actives;
        }

        /// <summary>
        /// Gets all reconciliation tolerances.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<ReconciliationToleranceDto> GetAllReconciliationTolerances()
        {
            IList<ReconciliationToleranceDto> actives = new List<ReconciliationToleranceDto>();
            try
            {
                var entity = this.toleranceRepository.GetAll().ToList();
                actives = Mapper.Map<IList<ReconciliationToleranceDto>>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return actives;
        }

        /// <summary>
        /// Updates the reconciliation tolerance.
        /// </summary>
        /// <param name="toleranceDto">The tolerance dto.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool UpdateReconciliationTolerance(ReconciliationToleranceDto toleranceDto)
        {
            bool updated = false;

            if (toleranceDto == null)
            {
                return updated;
            }

            try
            {
                var entity = this.toleranceRepository.FindByIdNoTracking(new ReconciliationTolerance(toleranceDto.ServiceCode, toleranceDto.CurrencyCode, toleranceDto.ToleranceTypeCode));

                if (entity == null)
                {
                    return updated;
                }

                entity.ToleranceValue = toleranceDto.ToleranceValue;
                this.toleranceRepository.Update(entity);
                this.unitOfWork.Commit();
                updated = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }

            return updated;
        }

        /// <summary>
        /// Check if Exchange Rate is Duplicated
        /// </summary>
        /// <param name="tolaranceDto"></param>
        /// <returns></returns>
        private bool IsToleranceDuplicated(ReconciliationToleranceDto tolaranceDto)
        {
            var entity = this.toleranceRepository.FindByIdNoTracking(new ReconciliationTolerance(tolaranceDto.ServiceCode, tolaranceDto.CurrencyCode, tolaranceDto.ToleranceTypeCode));
            return entity != null ? true : false;
        }
    }
}
