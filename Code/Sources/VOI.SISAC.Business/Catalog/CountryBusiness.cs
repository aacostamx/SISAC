//------------------------------------------------------------------------
// <copyright file="CountryBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.MapConfiguration;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Catalog;
    using VOI.SISAC.Entities.Catalog;
    using Dal = VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Class of Country
    /// </summary>
    public class CountryBusiness : ICountryBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The country repository
        /// </summary>
        private readonly ICountryRepository countryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="countryRepository">The country repository.</param>
        public CountryBusiness(IUnitOfWork unitOfWork, ICountryRepository countryRepository)
        {
            this.unitOfWork = unitOfWork;
            this.countryRepository = countryRepository;
        }

        #region ICountryBusiness Members

        /// <summary>
        /// Gets all country.
        /// </summary>
        /// <returns> a List EntityCountry</returns>
        public IList<CountryDto> GetAllCountry()
        {
            try
            {
                IList<Country> countryEntity = this.countryRepository.GetAll().ToList();
                IList<CountryDto> countryDto = new List<CountryDto>();

                countryDto = Mapper.Map<IList<Country>, IList<CountryDto>>(countryEntity);
                return countryDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the country by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity Country</returns>
        /// <exception cref="System.NotImplementedException">Business Exception</exception>
        public CountryDto FindCountryById(string id)
        {
            try
            {
                Country country = this.countryRepository.FindById(id);
                CountryDto countryDto = new CountryDto();
                countryDto = Mapper.Map<Country, CountryDto>(country);

                return countryDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds the country.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if added else false</returns>
        /// <exception cref="System.NotImplementedException">Business Exception</exception>
        public bool AddCountry(CountryDto entity)
        {
            Country country = new Country();

            if (entity == null)
            {
                return false;
            }

            if (this.IsCountryCodeDuplicate(entity.CountryCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                entity.Status = true;
                country = Mapper.Map<Country>(entity);
                this.countryRepository.Add(country);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the country.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if deleted else false</returns>
        /// <exception cref="System.NotImplementedException">Business Exception</exception>
        public bool DeleteCountry(CountryDto entity)
        {
            try
            {
                Dal.Country country = this.countryRepository.FindById(entity.CountryCode);

                if (country != null)
                {
                    /*
                    * Si se quiere hacer un borrado permanente de la DB
                    * se debe de usar la siguiente instrucción:
                    * this.countryRepository.Delete(country);
                    */

                    // Para borrado lógico
                    country.Status = false;
                    this.countryRepository.Update(country);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the country.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if updated else false</returns>
        /// <exception cref="System.NotImplementedException">Business Exception</exception>
        public bool UpdateCountry(CountryDto entity)
        {
            try
            {
                Country country = this.countryRepository.FindById(entity.CountryCode);
                country.CountryName = entity.CountryName;
                country.CountryCodeShort = entity.CountryCodeShort;
                this.countryRepository.Update(country);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the actives country.
        /// </summary>
        /// <returns>A List EntityCountry</returns>
        /// <exception cref="System.NotImplementedException">Business Exception</exception>
        public IList<CountryDto> GetActivesCountry()
        {
            try
            {
                IList<Country> countryEntity = this.countryRepository.GetActiveCountry().ToList();
                IList<CountryDto> countryDto = new List<CountryDto>();
                countryDto = Mapper.Map<IList<Country>, IList<CountryDto>>(countryEntity);

                return countryDto.OrderBy(c => c.CountryCode).ToList();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Determines whether [is country code duplicate] [the specified country code].
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>true or false</returns>
        public bool IsCountryCodeDuplicate(string countryCode)
        {
            Country country = this.countryRepository.FindById(countryCode);
            return country != null ? true : false;
        }

        #endregion
    }
}
