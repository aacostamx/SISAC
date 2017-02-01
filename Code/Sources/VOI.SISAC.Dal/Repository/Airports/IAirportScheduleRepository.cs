//------------------------------------------------------------------------
// <copyright file="IAirportScheduleRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Interface IAirportScheduleRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.IRepository{VOI.SISAC.Entities.Airport.AirportSchedule}"/>
    public interface IAirportScheduleRepository : IRepository<AirportSchedule>
    {

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>AirportSchedule.</returns>
        AirportSchedule FindById(long id);

        /// <summary>
        /// Gets the active airport schedule.
        /// </summary>
        /// <returns>IList&lt;AirportSchedule&gt;.</returns>
        IList<AirportSchedule> GetActiveAirportSchedule();

        /// <summary>
        /// Gets the airport schedules by station code.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="onlyActives">if set to <c>true</c> [only actives].</param>
        /// <returns>IList&lt;AirportSchedule&gt;.</returns>
        IList<AirportSchedule> GetAirportSchedulesByStationCode(string stationCode, bool onlyActives);
        /// <summary>
        /// Gets the airport schedules by station code except identifier.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>IList&lt;AirportSchedule&gt;.</returns>
        IList<AirportSchedule> GetAirportSchedulesByStationCodeExceptID(string stationCode, long id);
    }
}
