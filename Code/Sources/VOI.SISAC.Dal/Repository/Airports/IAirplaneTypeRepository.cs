//------------------------------------------------------------------------
// <copyright file="IAirplaneTypeRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Interface for specific operation for AirplaneType
    /// </summary>
    public interface IAirplaneTypeRepository : IRepository<AirplaneType>
    {
        /// <summary>
        /// Finds the airplane type by its identifier.
        /// </summary>
        /// <param name="id">The Airplane Type's identifier.</param>
        /// <returns>The Airplane Type specified.</returns>
        AirplaneType FindById(string id);

        /// <summary>
        /// Gets the actives airplane types.
        /// </summary>
        /// <returns>List of actives Airplane Types.</returns>
        IList<AirplaneType> GetActiveAirplaneType();
    }
}
