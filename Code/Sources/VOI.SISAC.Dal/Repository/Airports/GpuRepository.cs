//------------------------------------------------------------------------
// <copyright file="GpuRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Gpu Repository
    /// </summary>
    public class GpuRepository : Repository<Gpu>, IGpuRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factor"></param>
        public GpuRepository(IDbFactory factor) : base(factor)
        {

        }

        /// <summary>
        /// Find by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Gpu FindById(string id)
        {
            var gpu = this.DbContext.Gpus.Where(g => g.GpuCode == id).FirstOrDefault();
            return gpu;
        }

        /// <summary>
        /// Get Actives Gpu
        /// </summary>
        /// <returns></returns>
        public IList<Gpu> GetActiveGpu()
        {
            return this.DbContext.Gpus.Where(g => g.Status).ToList();
        }

        /// <summary>
        /// Gets the Gpu by its station.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="onlyActives">if set to <c>true</c> bring only actives records.</param>
        /// <returns>
        /// The GPU's related to a station.
        /// </returns>
        public IList<Gpu> GetGpuByStation(string stationCode, bool onlyActives)
        {
            if (onlyActives)
            {
                return this.DbContext.Gpus.Where(g => g.StationCode == stationCode && g.Status).ToList();
            }
            else
            {
                return this.DbContext.Gpus.Where(g => g.StationCode == stationCode).ToList();
            }
        }
    }
}
