//------------------------------------------------------------------------
// <copyright file="IAirportRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Dal.Infrastructure;

    /// <summary>
    /// Airport Repository Interface
    /// </summary>
    public interface IAirportRepository : IRepository<Airport>
    {
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>Airport Entity.</returns>
        Airport FindById(string id);

        /// <summary>
        /// Gets the actives Airports.
        /// </summary>
        /// <returns>Airports marked as Actives.</returns>
        IList<Airport> GetActivesAirports();

        /// <summary>
        /// Validate if the airports exist.
        /// </summary>
        /// <param name="stationCodes">The station codes to validate.</param>
        /// <returns>Returns a list with the Station Codes that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfAirportExist(IList<string> stationCodes);

        /// <summary>
        /// Gets the name of the airport by station code.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <returns>Returns the airport name for the station code given.</returns>
        string GetAirportName(string stationCode);
    }
}
