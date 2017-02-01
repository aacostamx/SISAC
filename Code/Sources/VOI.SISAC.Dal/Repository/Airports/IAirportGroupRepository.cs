//------------------------------------------------------------------------
// <copyright file="IAirportGroupRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using DALModel = VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Dal.Infrastructure;

    public interface IAirportGroupRepository : IRepository<DALModel.AirportGroup>
    {
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>AirportGroup Entity.</returns>
        DALModel.AirportGroup FindById(string id);

        /// <summary>
        /// Gets the actives groups of sirports
        /// </summary>
        /// <returns>AirportGroup marked as Actives.</returns>
        IList<DALModel.AirportGroup> GetActivesAirportGroup();
    }
}
