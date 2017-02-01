//------------------------------------------------------------------------
// <copyright file="IAirportGroupBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// Contract for IAirportGroupBusiness methods
    /// </summary>
    public interface IAirportGroupBusiness
    {
        /// <summary>
        /// Gets all AirportGroupDto.
        /// </summary>
        /// <returns>List of AirportGroupDto.</returns>
        IList<AirportGroupDto> GetAllAirportGroups();

        /// <summary>
        /// Finds the airport by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity Airport.</returns>
        AirportGroupDto FindAirportGroupById(string id);

        /// <summary>
        /// Adds the airport.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddAirportGroup(AirportGroupDto entity);

        /// <summary>
        /// Deletes the airport.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteAirportGroup(AirportGroupDto entity);

        /// <summary>
        /// Physical Delete
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>boolean</returns>
        bool PhysicalDeleteAirportGroup(AirportGroupDto entity);

        /// <summary>
        /// Updates the airport.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateAirportGroup(AirportGroupDto entity);

        /// <summary>
        /// Gets the Actives AirportGroupDto.
        /// </summary>
        /// <returns>AirportGroupDto Actives.</returns>
        IList<AirportGroupDto> GetActivesAirportGroups();
    }
}
