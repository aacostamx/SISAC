//------------------------------------------------------------------------
// <copyright file="IAirplaneRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Interface for specific operation for Airplane
    /// </summary>
    public interface IAirplaneRepository : IRepository<Airplane>
    {
        /// <summary>
        /// Finds the airplane by its identifier.
        /// </summary>
        /// <param name="id">The airplane's identifier.</param>
        /// <returns>The Airplane specified.</returns>
        Airplane FindById(string id);

        /// <summary>
        /// Gets the actives airplane types.
        /// </summary>
        /// <returns>List of actives Airplanes</returns>
        IList<Airplane> GetActiveAirplane();
    }
}
