//------------------------------------------------------------------------
// <copyright file="CurrencyBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Entities.Finance;
    using VOI.SISAC.Business.Resources;

    /// <summary>
    /// Currency business logic
    /// </summary>
    public class CurrencyBusiness : ICurrencyBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly ICurrencyRepository currencyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="currencyRepository">The airport repository.</param>
        public CurrencyBusiness(IUnitOfWork unitOfWork, ICurrencyRepository currencyRepository)
        {
            this.unitOfWork = unitOfWork;
            this.currencyRepository = currencyRepository;
        }

        /// <summary>
        /// Gets all currencies.
        /// </summary>
        /// <returns>
        /// List of Currency.
        /// </returns>
        public IList<CurrencyDto> GetAllCurrency()
        {
            try
            {
                IList<Currency> currencyEntity = this.currencyRepository.GetAll().ToList();
                IList<CurrencyDto> currencyDto = new List<CurrencyDto>();
                currencyDto = Mapper.Map<IList<Currency>, IList<CurrencyDto>>(currencyEntity);
                return currencyDto.ToList();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the Currency by its identifier.
        /// </summary>
        /// <param name="id">Currency identifier.</param>
        /// <returns>
        /// Currency DTO.
        /// </returns>
        public CurrencyDto FindCurrencyById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            try
            {
                Currency currencyModel = this.currencyRepository.FindById(id);
                CurrencyDto currencyDto = new CurrencyDto();
                currencyDto = Mapper.Map<Currency, CurrencyDto>(currencyModel);
                return currencyDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds a new Currency.
        /// </summary>
        /// <param name="currencyDto">The entity to be added</param>
        /// <returns>
        ///   <c>true</c> if was added <c>false</c> otherwise.
        /// </returns>
        public bool AddCurrency(CurrencyDto currencyDto)
        {
            if (currencyDto == null)
            {
                return false;
            }

            if (this.IsCurrencyCodeDuplicated(currencyDto.CurrencyCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                Currency currency = new Currency()
                {
                    CurrencyCode = currencyDto.CurrencyCode,
                    CurrencyName = currencyDto.CurrencyName,
                    Status = true
                };

                this.currencyRepository.Add(currency);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the Currency.
        /// </summary>
        /// <param name="currencyDto">The entity to be deleted.</param>
        /// <returns><c>true</c> if was deleted <c>false</c> otherwise.</returns>
        public bool DeleteCurrency(CurrencyDto currencyDto)
        {
            if (currencyDto == null)
            {
                return false;
            }

            try
            {
                Currency currencyModel = this.currencyRepository.FindById(currencyDto.CurrencyCode);
                if (currencyModel == null)
                {
                    return false;
                }

                /*
                 * Si se quiere hacer un borrado permanente de la DB
                 * se debe de usar la siguiente instrucción:
                 * this.airportDal.Delete(airportDal);
                */

                // Logic delete
                currencyModel.Status = false;
                this.currencyRepository.Update(currencyModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the Currency.
        /// </summary>
        /// <param name="currencyDto">The entity.</param>
        /// <returns><c>true</c> if was updated <c>false</c> otherwise.</returns>
        public bool UpdateCurrency(CurrencyDto currencyDto)
        {
            if (currencyDto == null)
            {
                return false;
            }

            try
            {
                Currency currencyModel = this.currencyRepository.FindById(currencyDto.CurrencyCode);
                if (currencyModel == null)
                {
                    return false;
                }

                currencyModel.CurrencyName = currencyDto.CurrencyName;
                currencyModel.Status = currencyDto.Status;
                this.currencyRepository.Update(currencyModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the all active Currencies.
        /// </summary>
        /// <returns>List of Currency actives.</returns>
        public IList<CurrencyDto> GetActivesCurrency()
        {
            try
            {
                IList<Currency> currencyModel = this.currencyRepository.GetActiveCurrency().ToList();
                IList<CurrencyDto> currencyDto = new List<CurrencyDto>();
                currencyDto = Mapper.Map<IList<Currency>, IList<CurrencyDto>>(currencyModel);
                return currencyDto.ToList();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Validates if a duplicated currency code exists.
        /// </summary>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns>
        ///    <c>true</c> if exists <c>false</c>otherwise.
        /// </returns>
        private bool IsCurrencyCodeDuplicated(string currencyCode)
        {
            Currency currencies = this.currencyRepository.FindById(currencyCode);
            return currencies != null ? true : false;
        }
    }
}
