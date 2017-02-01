//--------------------------------------------------------------------
// <copyright file="IAirportBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// Contract for Airport methods
    /// </summary>
    public interface IAirportBusiness
    {
        /// <summary>
        /// Gets all airport.
        /// </summary>
        /// <returns>List of Airports.</returns>
        IList<AirportDto> GetAllAirport();

        /// <summary>
        /// Finds the AirportDto by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity AirportDto.</returns>
        AirportDto FindAirportById(string id);

        /// <summary>
        /// Adds the AirportDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddAirport(AirportDto entity);

        /// <summary>
        /// Deletes the AirportDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteAirport(AirportDto entity);

        /// <summary>
        /// Physical Delete
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Boolean</returns>
        bool PhysicalDeleteAirport(AirportDto entity);

        /// <summary>
        /// Updates the AirportDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateAirport(AirportDto entity);

        /// <summary>
        /// Gets the Actives airports.
        /// </summary>
        /// <returns>Airports Actives.</returns>
        IList<AirportDto> GetActivesAirports();
    }
}
