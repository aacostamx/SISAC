//------------------------------------------------------------------------
// <copyright file="DrinkingWaterRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;


    /// <summary>
    /// Class AirportScheduleRepository.
    /// </summary>
    public class AirportScheduleRepository : Repository<AirportSchedule>, IAirportScheduleRepository
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DrinkingWaterRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AirportScheduleRepository(IDbFactory factory) 
            : base(factory) 
        { 
        }
        #endregion

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>AirportSchedule.</returns>
        public AirportSchedule FindById(long id)
        {
            var airportSchedule = this.DbContext.AirportSchedules.FirstOrDefault(c => c.AirportScheduleID == id);
            return airportSchedule;
        }

        /// <summary>
        /// Gets the active airport schedule.
        /// </summary>
        /// <returns>IList&lt;AirportSchedule&gt;.</returns>
        public IList<AirportSchedule> GetActiveAirportSchedule()
        {
            return this.DbContext.AirportSchedules.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Gets the airport schedules by station code.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="onlyActives">if set to <c>true</c> [only actives].</param>
        /// <returns>IList&lt;AirportSchedule&gt;.</returns>
        public IList<AirportSchedule> GetAirportSchedulesByStationCode(string stationCode, bool onlyActives)
        {
            if (onlyActives)
            {
                return this.DbContext.AirportSchedules.Where(c => c.StationCode == stationCode && c.Status).ToList();
            }

            return this.DbContext.AirportSchedules.Where(c => c.StationCode == stationCode).ToList();
        }

        /// <summary>
        /// Gets the airport schedules by station code except identifier.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>IList&lt;AirportSchedule&gt;.</returns>
        public IList<AirportSchedule> GetAirportSchedulesByStationCodeExceptID(string stationCode, long id)
        {
            return this.DbContext.AirportSchedules.Where(c => c.StationCode == stationCode && c.Status && c.AirportScheduleID != id).ToList();
        }
    }
}
