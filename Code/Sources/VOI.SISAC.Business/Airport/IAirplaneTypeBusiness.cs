//------------------------------------------------------------------------
// <copyright file="IAirplaneTypeBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// Airplane Business interface
    /// </summary>
    public interface IAirplaneTypeBusiness
    {
        /// <summary>
        /// Gets all Airplane Types.
        /// </summary>
        /// <returns>All Airplane Types.</returns>
        IList<AirplaneTypeDto> GetAllAirplaneType();

        /// <summary>
        /// Finds the airplane type by its identifier.
        /// </summary>
        /// <param name="id">The Currency identifier.</param>
        /// <returns>Currency Entity.</returns>
        AirplaneTypeDto FindAirplaneTypeById(string id);

        /// <summary>
        /// Adds a new Airplane Type.
        /// </summary>
        /// <param name="airplaneTypeDto">The entity</param>
        /// <returns><c>true</c> if was added <c>false</c> otherwise.</returns>
        bool AddAirplaneType(AirplaneTypeDto airplaneTypeDto);

        /// <summary>
        /// Deletes the Airplane Type.
        /// </summary>
        /// <param name="airplaneTypeDto">The entity.</param>
        /// <returns><c>true</c> if was deleted <c>false</c> otherwise.</returns>
        bool DeleteAirplaneType(AirplaneTypeDto airplaneTypeDto);

        /// <summary>
        /// Updates the Airplane Type.
        /// </summary>
        /// <param name="airplaneTypeDto">The entity.</param>
        /// <returns><c>true</c> if was updated <c>false</c> otherwise.</returns>
        bool UpdateAirplaneType(AirplaneTypeDto airplaneTypeDto);

        /// <summary>
        /// Gets the all Airplane Type actives.
        /// </summary>
        /// <returns>List of actives Airplane Types.</returns>
        IList<AirplaneTypeDto> GetActivesAirplaneType();
    }
}
