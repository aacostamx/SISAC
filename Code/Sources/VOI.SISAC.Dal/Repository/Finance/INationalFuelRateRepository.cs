//------------------------------------------------------------------------
// <copyright file="INationalFuelRateRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Interface for National fuel rate operations
    /// </summary>
    public interface INationalFuelRateRepository : IRepository<NationalFuelRate>
    {
        /// <summary>
        /// Find Contract by its primary parameters.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The National fuel rate.
        /// </returns>
        NationalFuelRate FindById(long id);

        /// <summary>
        /// Gets the national fuel rates paginated.
        /// </summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>
        /// List of national fuel rates paginated.
        /// </returns>
        IList<NationalFuelRate> GetNationalFuelRatesPagination(int skip, int take);

        /// <summary>
        /// Adds a list of National Fuel Rates.
        /// </summary>
        /// <param name="rates">The rates.</param>
        void AddRange(IList<NationalFuelRate> rates);

        /// <summary>
        /// Counts all registers for national fuel rates.
        /// </summary>
        /// <returns>Total of national fuel rates.</returns>
        int CountAll();

        /// <summary>
        /// Gets the national fuel rates with theirs relationships.
        /// </summary>
        /// <returns>List of national fuel rates paginated.</returns>
        IList<NationalFuelRate> GetAllWithRealationships();

        /// <summary>
        /// Deletes the range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        void DeleteRange(DateTime startDate, DateTime endDate);

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
        DateTime GetRateMaxEndDate(string stationCode, string serviceCode, string providerNumber, string fuelConceptTypeCode, string scheduleTypeCode);

        /// <summary>
        /// Gets the national fuel rates by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>
        /// List of national fuel rates.
        /// </returns>
        IList<NationalFuelRate> GetNationalFuelRatesByParameters(NationalFuelRate parameters, int skip, int take);

        /// <summary>
        /// Counts the national fuel rates by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Total of records of national fuel rates by parameters.
        /// </returns>
        int CountNationalFuelRatesByParameters(NationalFuelRate parameters);
    }
}
