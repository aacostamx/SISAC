//------------------------------------------------------------------------
// <copyright file="IGpuBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Gpu
{
    using System.Collections.Generic;
    using Dto.Airports;

    /// <summary>
    /// Contract for Gpu methods
    /// </summary>
    public interface IGpuBusiness
    {
        /// <summary>
        /// Gets all Gpu.
        /// </summary>
        /// <returns>List of Gpus.</returns>
        IList<GpuDto> GetAllGpu();

        /// <summary>
        /// Finds the GpuDto by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity GpuDto.</returns>
        GpuDto FindGpuById(string id);

        /// <summary>
        /// Adds the GpuDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddGpu(GpuDto entity);

        /// <summary>
        /// Deletes the GpuDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteGpu(GpuDto entity);

        /// <summary>
        /// Updates the GpuDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateGpu(GpuDto entity);

        /// <summary>
        /// Gets the Actives Gpus.
        /// </summary>
        /// <returns>Gpus Actives.</returns>
        IList<GpuDto> GetActivesGpus();

        /// <summary>
        /// Gets the Gpu by its station.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="onlyActives">if set to <c>true</c> bring only actives records.</param>
        /// <returns>
        /// The GPU's related to a station.
        /// </returns>
        IList<GpuDto> GetGpuByStation(string stationCode, bool onlyActives);
    }
}
