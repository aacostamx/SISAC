//------------------------------------------------------------------------
// <copyright file="IAirportScheduleBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// Interface IAirportScheduleBusiness
    /// </summary>
    public interface IAirportScheduleBusiness
    {
        /// <summary>
        /// Finds the airport schedule by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>AirportScheduleDto.</returns>
        AirportScheduleDto FindAirportScheduleById(long id);
        /// <summary>
        /// Adds the airport schedule.
        /// </summary>
        /// <param name="waterDto">The water dto.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AddAirportSchedule(AirportScheduleDto airportScheduleDto);
        /// <summary>
        /// Deletes the airport schedule.
        /// </summary>
        /// <param name="waterDto">The water dto.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool DeleteAirportSchedule(AirportScheduleDto airportScheduleDto);
        /// <summary>
        /// Updates the airport schedule.
        /// </summary>
        /// <param name="waterDto">The water dto.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool UpdateAirportSchedule(AirportScheduleDto airportScheduleDto);
        /// <summary>
        /// Gets the actives airport schedules.
        /// </summary>
        /// <returns>IList&lt;AirportScheduleDto&gt;.</returns>
        IList<AirportScheduleDto> GetActivesAirportSchedules();
        /// <summary>
        /// Gets the airport schedules by station code.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="onlyActives">if set to <c>true</c> [only actives].</param>
        /// <returns>IList&lt;AirportScheduleDto&gt;.</returns>
        IList<AirportScheduleDto> GetAirportSchedulesByStationCode(string stationCode, bool onlyActives);
    }
}
