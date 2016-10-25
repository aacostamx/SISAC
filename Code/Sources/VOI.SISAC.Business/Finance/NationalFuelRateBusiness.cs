//------------------------------------------------------------------------
// <copyright file="NationalFuelRateBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Linq;
    using AutoMapper;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Entities.Finance;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Dal.Repository.Catalog;
    using VOI.SISAC.Entities.Catalog;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Operations for National Fuel Rate
    /// </summary>
    public class NationalFuelRateBusiness : INationalFuelRateBusiness
    {
        /// <summary>
        /// The national rate
        /// </summary>
        private readonly INationalFuelRateRepository nationalRate;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IAirportRepository airportRepository;

        /// <summary>
        /// The service repository
        /// </summary>
        private readonly IServiceRepository serviceRepository;

        /// <summary>
        /// The provider repository
        /// </summary>
        private readonly IProviderRepository providerRepository;

        /// <summary>
        /// The currency repository
        /// </summary>
        private readonly ICurrencyRepository currencyRepository;

        /// <summary>
        /// The schedule type repository
        /// </summary>
        private readonly IScheduleTypeRepository scheduleTypeRepository;

        /// <summary>
        /// The fuel concept type repository
        /// </summary>
        private readonly IFuelConceptTypeRepository fuelConceptTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelRateBusiness" /> class.
        /// </summary>
        /// <param name="nationalRate">The national rate.</param>
        /// <param name="airportRepository">The airport repository.</param>
        /// <param name="serviceRepository">The service repository.</param>
        /// <param name="providerRepository">The provider repository.</param>
        /// <param name="currencyRepository">The currency repository.</param>
        /// <param name="scheduleTypeRepository">The schedule type repository.</param>
        /// <param name="fuelConceptTypeRepository">The fuel concept type repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public NationalFuelRateBusiness(
            INationalFuelRateRepository nationalRate,
            IAirportRepository airportRepository,
            IServiceRepository serviceRepository,
            IProviderRepository providerRepository,
            ICurrencyRepository currencyRepository,
            IScheduleTypeRepository scheduleTypeRepository,
            IFuelConceptTypeRepository fuelConceptTypeRepository,
            IUnitOfWork unitOfWork)
        {
            this.nationalRate = nationalRate;
            this.airportRepository = airportRepository;
            this.serviceRepository = serviceRepository;
            this.providerRepository = providerRepository;
            this.currencyRepository = currencyRepository;
            this.scheduleTypeRepository = scheduleTypeRepository;
            this.fuelConceptTypeRepository = fuelConceptTypeRepository;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Finds the national fuel rate by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The national fuel rate.
        /// </returns>
        public NationalFuelRateDto FindNationalFuelRateById(long id)
        {
            if (id <= 0)
            {
                return null;
            }

            try
            {
                NationalFuelRate rate = this.nationalRate.FindById(id);
                NationalFuelRateDto rateDto = Mapper.Map<NationalFuelRateDto>(rate);
                return rateDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds the National Fuel Rates.
        /// </summary>
        /// <param name="rates">The rates.</param>
        /// <returns>
        ///   <c>true</c> if success otherwise <c>false</c>.
        /// </returns>
        public IList<string> UploadNationalFuelRates(List<NationalFuelRateDto> rates)
        {
            // If there isn't any items
            if (rates == null || rates.Count == 0)
            {
                return new List<string> { "No items found" };
            }

            List<string> errors = new List<string>();

            try
            {
                // Validates that the Catalogs exist
                errors = this.ValidateCatalogs(rates).ToList();
                if (errors.Count > 0)
                {
                    return errors;
                }

                // Adds the specific ID for the fields that only have description
                errors = this.NationalFuelContractLoadCode(rates).ToList();
                if (errors.Count > 0)
                {
                    return errors;
                }

                errors = this.ValidateDates(rates);
                if (errors.Count > 0)
                {
                    return errors;
                }

                List<NationalFuelRate> contractEntities = Mapper.Map<List<NationalFuelRate>>(rates);
                this.nationalRate.AddRange(contractEntities);
                this.unitOfWork.Commit();
                return errors;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the National Fuel Rates.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// true if was deleted else false
        /// </returns>
        public bool DeleteNationalFuelRate(long id)
        {
            if (id <= 0)
            {
                return false;
            }

            try
            {
                NationalFuelRate rate = this.nationalRate.FindById(id);
                this.nationalRate.Delete(rate);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the National Fuel Rates in the period given.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// Number of records deleted. If equal to -1 means that an error exists.
        /// </returns>
        public int DeleteNationalFuelRate(DateTime startDate, DateTime endDate)
        {
            if (startDate == default(DateTime) || endDate == default(DateTime))
            {
                return -1;
            }

            if (startDate.Date > endDate.Date)
            {
                return -1;
            }

            try
            {
                NationalFuelRate rate = new NationalFuelRate()
                {
                    EffectiveStartDate = startDate,
                    EffectiveEndDate = endDate
                };
                int count = this.nationalRate.CountNationalFuelRatesByParameters(rate);
                this.nationalRate.DeleteRange(startDate, endDate);
                this.unitOfWork.Commit();
                return count;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the national fuel rates pagination.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>List of national fuel rates.</returns>
        public IList<NationalFuelRateDto> GetNationalFuelRatesPagination(int pageSize, int pageNumber)
        {
            int skip = (pageNumber - 1) * pageSize;
            try
            {
                List<NationalFuelRate> rates = this.nationalRate.GetNationalFuelRatesPagination(skip, pageSize).ToList();
                List<NationalFuelRateDto> ratesDto = Mapper.Map<List<NationalFuelRateDto>>(rates);

                return ratesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Counts all.
        /// </summary>
        /// <returns>
        /// Total of records.
        /// </returns>
        public int CountAll()
        {
            try
            {
                return this.nationalRate.CountAll();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the national fuel rate.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <returns>true if was deleted else false.</returns>
        public bool UpdateNationalFuelRate(NationalFuelRateDto rate)
        {
            if (rate == null)
            {
                return false;
            }

            try
            {
                NationalFuelRate entity = this.nationalRate.FindById(rate.NationalFuelRateId);
                entity.RateValue = rate.RateValue;
                this.nationalRate.Update(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Search the national fuel rates by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>
        /// List of national fuel rates.
        /// </returns>
        public IList<NationalFuelRateDto> SearchNationalFuelRateByParameters(NationalFuelRateDto parameters, int pageSize, int pageNumber)
        {
            int skip = (pageNumber - 1) * pageSize;
            try
            {
                NationalFuelRate entity = Mapper.Map<NationalFuelRate>(parameters);
                List<NationalFuelRate> rates = this.nationalRate.GetNationalFuelRatesByParameters(entity, skip, pageSize).ToList();
                List<NationalFuelRateDto> ratesDto = Mapper.Map<List<NationalFuelRateDto>>(rates);
                return ratesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Counts the national fuel rates by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Number of records.
        /// </returns>
        public int CountNationalFuelRateByParameters(NationalFuelRateDto parameters)
        {
            try
            {
                NationalFuelRate entity = Mapper.Map<NationalFuelRate>(parameters);
                return this.nationalRate.CountNationalFuelRatesByParameters(entity);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Validates catalogs existence.
        /// </summary>
        /// <param name="rates">The contracts.</param>
        /// <returns>List of errors.</returns>
        private List<string> ValidateCatalogs(List<NationalFuelRateDto> rates)
        {
            List<string> errors = new List<string>();

            // Validate Airport
            errors.AddRange(this.ValidateAirport(rates.Select(c => c.StationCode).ToList()));

            // Validate Service
            errors.AddRange(this.ValidateIfServiceIsNationalFuel(rates.Select(c => c.ServiceCode).ToList()));

            // Validate Provider
            errors.AddRange(this.ValidateProvider(rates.Select(c => c.ProviderNumber).ToList()));

            // Validate Currency
            errors.AddRange(this.ValidateCurrency(rates.Select(c => c.CurrencyCode).ToList()));

            return errors;
        }

        /// <summary>
        /// Validates if the airports exist.
        /// </summary>
        /// <param name="stationCodes">The station codes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private List<string> ValidateAirport(List<string> stationCodes)
        {
            List<string> errors = new List<string>();
            List<string> result = this.airportRepository.ValidatedIfAirportExist(stationCodes).ToList();
            if (result.Count > 0)
            {
                errors.Add("Airport code(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the services exist.
        /// </summary>
        /// <param name="serviceCodes">The service codes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private List<string> ValidateIfServiceIsNationalFuel(List<string> serviceCodes)
        {
            List<string> errors = new List<string>();
            List<string> codes = new List<string>() { "CM" };
            List<string> result = serviceCodes.Except(codes).ToList();
            if (result.Count > 0)
            {
                errors.Add("Service code(s) not found. The codes must be for natiocal fuel.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the providers exist.
        /// </summary>
        /// <param name="providerNumbers">The service codes.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private List<string> ValidateProvider(List<string> providerNumbers)
        {
            List<string> errors = new List<string>();
            List<string> result = this.providerRepository.ValidatedIfProviderExist(providerNumbers).ToList();
            if (result.Count > 0)
            {
                errors.Add("Provider number(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>
        /// Validates if the currencies exist.
        /// </summary>
        /// <param name="currencies">The currencies.</param>
        /// <returns>List of errors if exist otherwise NULL.</returns>
        private List<string> ValidateCurrency(List<string> currencies)
        {
            List<string> errors = new List<string>();
            List<string> result = this.currencyRepository.ValidatedIfCurrencyExist(currencies).ToList();
            if (result.Count > 0)
            {
                errors.Add("Currency code(s) not found.");
                errors.AddRange(result);
            }

            return errors;
        }

        /// <summary>         
        /// Loads the codes in the object national fuel rates.
        /// </summary>
        /// <param name="rates">The rates.</param>
        /// <returns>List of errors.</returns>
        private List<string> NationalFuelContractLoadCode(List<NationalFuelRateDto> rates)
        {
            List<ScheduleType> scheduleTypes = this.scheduleTypeRepository.GetActivesScheduleTypes().ToList();
            List<FuelConceptType> fuelConceptTypes = this.fuelConceptTypeRepository.GetAll().ToList();
            List<string> errors = new List<string>();

            foreach (var item in rates)
            {
                FuelConceptType fuel = fuelConceptTypes.FirstOrDefault(e => e.FuelConceptTypeName == item.FuelConceptTypeName);
                if (fuel != null)
                {
                    item.FuelConceptTypeCode = fuel.FuelConceptTypeCode;
                }
                else
                {
                    errors.Add("Fuel Concept Type not found: " + item.FuelConceptTypeName);
                }

                ScheduleType schedule = scheduleTypes.FirstOrDefault(e => e.ScheduleTypeName == item.ScheduleTypeName);
                if (schedule != null)
                {
                    item.ScheduleTypeCode = schedule.ScheduleTypeCode;
                }
                else
                {
                    errors.Add("Schedule Type not found: " + item.ScheduleTypeName);
                }
            }

            return errors;
        }

        /// <summary>
        /// Validates the dates.
        /// </summary>
        /// <param name="rates">The rates.</param>
        /// <returns>List of errors.</returns>
        private List<string> ValidateDates(List<NationalFuelRateDto> rates)
        {
            List<string> errors = new List<string>();
            List<string> datesString = new List<string>();

            int numberOfDays = 0;
            string conceptKey = string.Empty;
            string startDateText = "Start Date";
            string endDateText = "End Date";
            string effectiveDateText = "Airport current max date";

            // Verify if there is any duplicate rate in the collection
            foreach (NationalFuelRateDto item in rates)
            {
                // The airport's max end date
                DateTime endMaxDate = this.nationalRate.GetRateMaxEndDate(item.StationCode, item.ServiceCode, item.ProviderNumber, item.FuelConceptTypeCode, item.ScheduleTypeCode);

                // number of days per rate
                numberOfDays = item.EffectiveEndDate.Subtract(item.EffectiveStartDate).Days + 1;

                // key
                conceptKey = item.StationCode + " & " + item.ServiceCode + " & " + item.ProviderNumber + " & " + item.ScheduleTypeCode + " & " + item.FuelConceptTypeCode;

                // All days from the start date to the end date
                if (numberOfDays > 0)
                {
                    datesString.AddRange(Enumerable.Range(0, numberOfDays)
                                          .Select(i => item.EffectiveStartDate.AddDays(i).ToShortDateString() + conceptKey).ToList());
                }

                // Start date must be less than end date
                if (item.EffectiveStartDate.Date > item.EffectiveEndDate.Date)
                {
                    errors.Add(
                        endDateText + ": "
                        + item.EffectiveEndDate.ToShortDateString() + " must be greater than " 
                        + startDateText + ": " 
                        + item.EffectiveStartDate.ToShortDateString() + ". In Airport: "
                        + item.StationCode + ", Service: " 
                        + item.ServiceCode + ", Provider: "
                        + item.ProviderNumber + ", Concept: "
                        + item.FuelConceptTypeName + ", Schedule Type: "
                        + item.ScheduleTypeName + ".");
                }

                // Start date must be greater than airport's max end date
                if (item.EffectiveStartDate.Date <= endMaxDate.Date)
                {
                    errors.Add(
                        startDateText + ": " 
                        + item.EffectiveStartDate.ToShortDateString() + " must be greater than " 
                        + effectiveDateText + ": " 
                        + endMaxDate.ToShortDateString() + ". In Airport: "
                        + item.StationCode + ", Service: " 
                        + item.ServiceCode + ", Provider: "
                        + item.ProviderNumber + ", Concept: "
                        + item.FuelConceptTypeName + ", Schedule Type: "
                        + item.ScheduleTypeName + ".");
                }
            }

            // Duplicate dates or dates in the same period
            foreach (var item in datesString)
            {
                var datesListFound = datesString.Where(c => c.Contains(item)).ToList();
                if (datesListFound.Count > 1)
                {
                    errors.Add("Match Date: " + item.Substring(0, 10) + ", For: " + item.Substring(10));
                }
            }

            return errors;
        }
    }
}
