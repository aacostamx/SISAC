//------------------------------------------------------------------------
// <copyright file="IAirlineRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Interface IAirlineRepository
    /// </summary>
    public interface IAirlineRepository : IRepository<Airline>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return Airline</returns>
        Airline FindById(string id);

        /// <summary>
        /// Gets the active airline.
        /// </summary>
        /// <returns>ICollection Airline</returns>
        ICollection<Airline> GetActiveAirline();

        /// <summary>
        /// Validate if the airlines exist.
        /// </summary>
        /// <param name="airlineCodes">The airline codes to validate.</param>
        /// <returns>Returns a list with the Airline Codes that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfAirlineExist(IList<string> airlineCodes);
    }
}
