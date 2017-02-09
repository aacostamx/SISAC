//------------------------------------------------------------------------
// <copyright file="NationalFuelRateRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Operations for National fuel rates
    /// </summary>
    public class NationalFuelRateRepository : Repository<NationalFuelRate>, INationalFuelRateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelRateRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalFuelRateRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Find Contract by its primary parameters.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The National fuel rate.
        /// </returns>
        public NationalFuelRate FindById(long id)
        {
            return this.DbContext.NationalFuelRates
                .Include(c => c.Airport)
                .Include(c => c.Currency)
                .Include(c => c.FuelConceptType)
                .Include(c => c.Provider)
                .Include(c => c.ScheduleType)
                .Include(c => c.Service)
                .FirstOrDefault(c => c.NationalFuelRateId == id);
        }

        /// <summary>
        /// Gets the national fuel rates paginated.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>
        /// List of national fuel rates paginated.
        /// </returns>
        public IList<NationalFuelRate> GetNationalFuelRatesPagination(int skip, int take)
        {
            List<NationalFuelRate> rates = this.DbContext.NationalFuelRates
                .OrderBy(c => c.StationCode)
                .ThenBy(c => c.ServiceCode)
                .ThenBy(c => c.ProviderNumber)
                .ThenBy(c => c.ScheduleTypeCode)
                .ThenBy(c => c.FuelConceptTypeCode)
                .ThenBy(c => c.EffectiveEndDate)
                .Skip(skip)
                .Take(take)
                .Include(c => c.Airport)
                .Include(c => c.Service)
                .Include(c => c.Provider)
                .Include(c => c.FuelConceptType)
                .Include(c => c.ScheduleType)
                .Include(c => c.Currency)
                .ToList();

            return rates;
        }

        /// <summary>
        /// Adds a list of National Fuel Rates.
        /// </summary>
        /// <param name="rates">The rates.</param>
        public void AddRange(IList<NationalFuelRate> rates)
        {
            this.DbContext.NationalFuelRates.AddRange(rates);
        }

        /// <summary>
        /// Counts all registers for national fuel rates.
        /// </summary>
        /// <returns>
        /// Total of national fuel rates.
        /// </returns>
        public int CountAll()
        {
            return this.DbContext.NationalFuelRates.Count();
        }

        /// <summary>
        /// Gets the national fuel rates with theirs relationships.
        /// </summary>
        /// <returns>
        /// List of national fuel rates paginated.
        /// </returns>
        public IList<NationalFuelRate> GetAllWithRealationships()
        {
            List<NationalFuelRate> rates = this.DbContext.NationalFuelRates
                .Include(c => c.Airport)
                .Include(c => c.Currency)
                .Include(c => c.FuelConceptType)
                .Include(c => c.Provider)
                .Include(c => c.ScheduleType)
                .Include(c => c.Service)
                .ToList();
            return rates;
        }

        /// <summary>
        /// Deletes the range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        public void DeleteRange(DateTime startDate, DateTime endDate)
        {
            List<NationalFuelRate> rates = this.DbContext.NationalFuelRates
                .Where(c => c.EffectiveStartDate >= startDate && c.EffectiveEndDate <= endDate)
                .ToList();

            this.DbContext.NationalFuelRates.RemoveRange(rates);
        }

        /// <summary>
        /// Get rate's max end date for the airport.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="fuelConceptTypeCode">The fuel concept type code.</param>
        /// <param name="scheduleTypeCode">The schedule type code.</param>
        /// <returns>
        /// The rate's max end date for the airport.
        /// </returns>
        public DateTime GetRateMaxEndDate(string stationCode, string serviceCode, string providerNumber, string fuelConceptTypeCode, string scheduleTypeCode)
        {
            List<NationalFuelRate> rates = this.DbContext.NationalFuelRates
                .Where(c =>
                    c.StationCode == stationCode
                    && c.ServiceCode == serviceCode
                    && c.ProviderNumber == providerNumber
                    && c.FuelConceptTypeCode == fuelConceptTypeCode
                    && c.ScheduleTypeCode == scheduleTypeCode)
                .ToList();

            if (rates == null || rates.Count == 0)
            {
                return new DateTime();
            }

            DateTime maxEndDate = rates.Max(c => c.EffectiveEndDate);
            return maxEndDate;
        }

        /// <summary>
        /// Gets the national fuel rates by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>
        /// List of national fuel rates.
        /// </returns>
        public IList<NationalFuelRate> GetNationalFuelRatesByParameters(NationalFuelRate parameters, int skip, int take)
        {
            List<NationalFuelRate> rates = this.DbContext.NationalFuelRates
                .Where(c => 
                    c.EffectiveStartDate >= parameters.EffectiveStartDate && c.EffectiveEndDate <= parameters.EffectiveEndDate)
                .OrderBy(c => c.StationCode)
                .ThenBy(c => c.ServiceCode)
                .ThenBy(c => c.ProviderNumber)
                .ThenBy(c => c.ScheduleTypeCode)
                .ThenBy(c => c.FuelConceptTypeCode)
                .ThenBy(c => c.EffectiveEndDate)
                .Include(c => c.Airport)
                .Include(c => c.Service)
                .Include(c => c.Provider)
                .Include(c => c.Currency)
                .Include(c => c.FuelConceptType)
                .Include(c => c.ScheduleType)
                .Skip(skip)
                .Take(take)
                .ToList();

            return rates;
        }

        /// <summary>
        /// Counts the national fuel rates by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Total of records of national fuel rates by parameters.
        /// </returns>
        public int CountNationalFuelRatesByParameters(NationalFuelRate parameters)
        {
            int count = this.DbContext.NationalFuelRates
                .Where(c => 
                    c.EffectiveStartDate >= parameters.EffectiveStartDate && c.EffectiveEndDate <= parameters.EffectiveEndDate)
                .ToList().Count;

            return count;
        }
    }
}
