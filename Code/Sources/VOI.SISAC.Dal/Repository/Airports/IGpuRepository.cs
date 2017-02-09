//------------------------------------------------------------------------
// <copyright file="IGpuRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entities.Airport;
    using Infrastructure;

    /// <summary>
    /// Gpu repository
    /// </summary>
    public interface IGpuRepository : IRepository<Gpu>
    {
        /// <summary>
        /// Finds the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Gpu FindById(string id);

        /// <summary>
        /// Gets the active entity.
        /// </summary>
        /// <returns></returns>
        IList<Gpu> GetActiveGpu();

        /// <summary>
        /// Gets the Gpu by its station.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="onlyActives">if set to <c>true</c> bring only actives records.</param>
        /// <returns>
        /// The GPU's related to a station.
        /// </returns>
        IList<Gpu> GetGpuByStation(string stationCode, bool onlyActives);
    }
}
