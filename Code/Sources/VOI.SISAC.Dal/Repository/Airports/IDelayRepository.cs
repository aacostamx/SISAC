//------------------------------------------------------------------------
// <copyright file="IDelayRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Repository Delay Interface
    /// </summary>
    public interface IDelayRepository : IRepository<Delay>
    {
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>Delay Entity.</returns>
        Delay FindById(string id);

        /// <summary>
        /// Gets the actives Delays.
        /// </summary>
        /// <returns>Delays marked as Actives.</returns>
        IList<Delay> GetActivesDelays();
    }
}
