//------------------------------------------------------------------------
// <copyright file="GpuObservationRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using Infrastructure;
    using Entities.Airport;
    using System.Linq;

    /// <summary>
    /// Class GpuObservationRepository
    /// </summary>
    public class GpuObservationRepository : Repository<GpuObservation>, IGpuObservationRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factor"></param>
        public GpuObservationRepository(IDbFactory factor) : base(factor)
        {

        }

        /// <summary>
        /// Find by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GpuObservation FindById(string id)
        {
            var gpuObservation = this.DbContext.GpuObservations.Where(g => g.GpuObservationCode == id).FirstOrDefault();
            return gpuObservation;
        }

        /// <summary>
        /// Get Actives Gpu
        /// </summary>
        /// <returns></returns>
        public IList<GpuObservation> GetActiveGpuObservation()
        {
            return this.DbContext.GpuObservations.Where(g => g.Status).ToList();
        }
    }
}
