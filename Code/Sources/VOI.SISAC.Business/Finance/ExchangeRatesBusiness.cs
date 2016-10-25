//------------------------------------------------------------------------
// <copyright file="ExchangeRatesBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Finance;
    using Dto.Finances;
    using Entities.Finance;
    using ExceptionBusiness;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    /// <summary>
    /// ExchangeRatesBusiness Class
    /// </summary>
    public class ExchangeRatesBusiness : IExchangeRatesBusiness
    {

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Exchange Rates Repository
        /// </summary>
        private readonly IExchangeRatesRepository exchangeRepository;

        /// <summary>
        /// ExchangeRatesBusiness Controller
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="exchangeRepository"></param>
        public ExchangeRatesBusiness(IUnitOfWork unitOfWork, IExchangeRatesRepository exchangeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.exchangeRepository = exchangeRepository;
        }

        /// <summary>
        /// Add Exchange Rate
        /// </summary>
        /// <param name="exchangeDto"></param>
        /// <returns></returns>
        public bool AddExchangeRate(ExchangeRatesDto exchangeDto)
        {
            bool added = false;
            if (exchangeDto == null)
            {
                return added;
            }
            else if(this.IsExchangeRateDuplicated(exchangeDto))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                var entity = new ExchangeRates()
                {
                    Year = exchangeDto.Year,
                    Month = exchangeDto.Month,
                    CurrencyCode = exchangeDto.CurrencyCode,
                    Rate = exchangeDto.Rate,
                    Status = true
                };

                this.exchangeRepository.Add(entity);
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
        /// Delete Exchange Rate
        /// </summary>
        /// <param name="exchangeDto"></param>
        /// <returns></returns>
        public bool DeleteExchangeRate(ExchangeRatesDto exchangeDto)
        {
            bool deleted = false;

            if (exchangeDto == null)
            {
                return deleted;
            }

            try
            {
                var entity = this.exchangeRepository.FindByIdNoTracking(new ExchangeRates(exchangeDto.Year, exchangeDto.Month, exchangeDto.CurrencyCode));

                if (entity == null)
                {
                    return deleted;
                }

                entity.Status = false;
                this.exchangeRepository.Update(entity);
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
        /// Find Exchange Rates By Id
        /// </summary>
        /// <param name="exchangeDto"></param>
        /// <returns></returns>
        public ExchangeRatesDto FindExchangeRateById(ExchangeRatesDto exchangeDto)
        {
            ExchangeRatesDto exchange = new ExchangeRatesDto();

            if(exchangeDto == null)
            {
                return exchange;
            }

            try
            {
                var entity = this.exchangeRepository
                    .FindByIdNoTracking(new ExchangeRates(exchangeDto.Year, exchangeDto.Month, exchangeDto.CurrencyCode));
                exchange = Mapper.Map<ExchangeRatesDto>(entity);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }
            return exchange;
        }

        /// <summary>
        /// Get All Actives Exchange Rates
        /// </summary>
        /// <returns></returns>
        public IList<ExchangeRatesDto> GetAllActivesExchangeRates()
        {
            IList<ExchangeRatesDto> actives = new List<ExchangeRatesDto>();
            try
            {
                var entity = this.exchangeRepository.GetActivesExchangeRates().ToList();
                actives = Mapper.Map<IList<ExchangeRatesDto>>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return actives;
        }

        /// <summary>
        /// Get All Exchange Rates
        /// </summary>
        /// <returns></returns>
        public IList<ExchangeRatesDto> GetAllExchangeRate()
        {
            IList<ExchangeRatesDto> actives = new List<ExchangeRatesDto>();
            try
            {
                var entity = this.exchangeRepository.GetAll().ToList();
                actives = Mapper.Map<IList<ExchangeRatesDto>>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return actives;
        }

        /// <summary>
        /// Update ExchangeRate
        /// </summary>
        /// <param name="exchangeDto"></param>
        /// <returns></returns>
        public bool UpdateExchangeRate(ExchangeRatesDto exchangeDto)
        {
            bool updated = false;

            if (exchangeDto == null)
            {
                return updated;
            }

            try
            {
                var entity = this.exchangeRepository.FindByIdNoTracking(new ExchangeRates(exchangeDto.Year, exchangeDto.Month, exchangeDto.CurrencyCode));

                if (entity == null)
                {
                    return updated;
                }

                entity.Rate = exchangeDto.Rate;
                this.exchangeRepository.Update(entity);
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
        /// <param name="exchangeDto"></param>
        /// <returns></returns>
        private bool IsExchangeRateDuplicated(ExchangeRatesDto exchangeDto)
        {
            var entity = this.exchangeRepository.FindByIdNoTracking(new ExchangeRates(exchangeDto.Year, exchangeDto.Month, exchangeDto.CurrencyCode));
            return entity != null ? true : false;
        }
    }
}
