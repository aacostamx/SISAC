//------------------------------------------------------------------------
// <copyright file="IGpuObservationRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using Configuration.Airport;
    using System.Collections.Generic;
    using Entities.Airport;
    using Infrastructure;

    /// <summary>
    /// Gpu Observation interface
    /// </summary>
    public interface IGpuObservationRepository : IRepository<GpuObservation>
    {
        /// <summary>
        /// Finds the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        GpuObservation FindById(string id);

        /// <summary>
        /// Gets the active entity.
        /// </summary>
        /// <returns></returns>
        IList<GpuObservation> GetActiveGpuObservation();
    }
}
