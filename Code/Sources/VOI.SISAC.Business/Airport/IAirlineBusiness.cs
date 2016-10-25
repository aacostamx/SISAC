//------------------------------------------------------------------------
// <copyright file="IAirlineBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Airports;
    
    /// <summary>
    /// Interface Airline Business
    /// </summary>
    public interface IAirlineBusiness
    {
        /// <summary>
        /// Gets all airline.
        /// </summary>
        /// <returns> List AirlineDto</returns>
        IList<AirlineDto> GetAllAirline();

        /// <summary>
        /// Finds the airline by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Airline Dto</returns>
        AirlineDto FindAirlineById(string id);

        /// <summary>
        /// Adds the airline.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true or false</returns>
        bool AddAirline(AirlineDto entity);

        /// <summary>
        /// Deletes the airline.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true or false</returns>
        bool DeleteAirline(AirlineDto entity);

        /// <summary>
        /// Updates the airline.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true or false</returns>
        bool UpdateAirline(AirlineDto entity);

        /// <summary>
        /// Gets the actives airline.
        /// </summary>
        /// <returns> List AirlineDto </returns>
        IList<AirlineDto> GetActivesAirline();
    }
}
