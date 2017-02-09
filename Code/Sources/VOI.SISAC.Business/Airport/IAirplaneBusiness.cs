//------------------------------------------------------------------------
// <copyright file="IAirplaneBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// Airplane Business interface
    /// </summary>
    public interface IAirplaneBusiness
    {
        /// <summary>
        /// Gets all Airplanes.
        /// </summary>
        /// <returns>All Airplanes.</returns>
        IList<AirplaneDto> GetAllAirplane();

        /// <summary>
        /// Finds the airplane by its identifier.
        /// </summary>
        /// <param name="id">The Airplane identifier.</param>
        /// <returns>Airplane Entity.</returns>
        AirplaneDto FindAirplaneById(string id);

        /// <summary>
        /// Adds a new Airplane.
        /// </summary>
        /// <param name="airplaneDto">The entity</param>
        /// <returns><c>true</c> if was added <c>false</c> otherwise.</returns>
        bool AddAirplane(AirplaneDto airplaneDto);

        /// <summary>
        /// Deletes the Airplane.
        /// </summary>
        /// <param name="airplaneDto">The entity.</param>
        /// <returns><c>true</c> if was deleted <c>false</c> otherwise.</returns>
        bool DeleteAirplane(AirplaneDto airplaneDto);

        /// <summary>
        /// Updates the Airplane.
        /// </summary>
        /// <param name="airplaneDto">The entity.</param>
        /// <returns><c>true</c> if was updated <c>false</c> otherwise.</returns>
        bool UpdateAirplane(AirplaneDto airplaneDto);

        /// <summary>
        /// Gets the all Airplane actives.
        /// </summary>
        /// <returns>List of actives Airplane.</returns>
        IList<AirplaneDto> GetActivesAirplane();
    }
}
