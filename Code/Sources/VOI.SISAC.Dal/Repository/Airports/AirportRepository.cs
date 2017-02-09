//------------------------------------------------------------------------
// <copyright file="AirportRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Airport Repository
    /// </summary>
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirportRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AirportRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region IAirportRepository Members
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>Airport Entity.</returns>
        public Airport FindById(string id)
        {
            var airports = this.DbContext.Airports
                .Where(c => c.StationCode == id)
                .Include(c => c.Country)
                .FirstOrDefault();

            return airports;
        }

        /// <summary>
        /// Gets the Actives airports.
        /// </summary>
        /// <returns>Airports marked as Actives.</returns>
        public IList<Airport> GetActivesAirports()
        {
            return this.DbContext.Airports.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validate if the airports exist.
        /// </summary>
        /// <param name="stationCodes">The station codes to validate.</param>
        /// <returns>Returns a list with the Station Codes that do not exist.</returns>
        public IList<string> ValidatedIfAirportExist(IList<string> stationCodes)
        {
            IList<string> notFound = new List<string>();
            IList<Airport> airportList = this.DbContext.Airports.Where(c => c.Status).ToList();

            notFound = stationCodes.Except(airportList.Select(c => c.StationCode)).ToList();
            return notFound;
        }

        /// <summary>
        /// Gets the name of the airport by station code.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <returns>
        /// Returns the airport name for the station code given.
        /// </returns>
        public string GetAirportName(string stationCode)
        {
            Airport airport = this.DbContext.Airports.FirstOrDefault(c => c.StationCode == stationCode);
            return airport != null ? airport.StationName : string.Empty;
        }
        #endregion
    }
}
