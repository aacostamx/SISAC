//------------------------------------------------------------------------
// <copyright file="IGpuObservationBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.GpuObservation
{
    using System.Collections.Generic;
    using Dto.Airports;

    /// <summary>
    /// Contract for GpuObservation methods
    /// </summary>
    public interface IGpuObservationBusiness
    {
        /// <summary>
        /// Gets all GpuObservation.
        /// </summary>
        /// <returns>List of GpuObservations.</returns>
        IList<GpuObservationDto> GetAllGpuObservation();

        /// <summary>
        /// Finds the GpuObservationDto by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity GpuObservationDto.</returns>
        GpuObservationDto FindGpuObservationById(string id);

        /// <summary>
        /// Adds the GpuObservationDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddGpuObservation(GpuObservationDto entity);

        /// <summary>
        /// Deletes the GpuObservationDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteGpuObservation(GpuObservationDto entity);

        /// <summary>
        /// Updates the GpuObservationDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateGpuObservation(GpuObservationDto entity);

        /// <summary>
        /// Gets the Actives GpuObservations.
        /// </summary>
        /// <returns>GpuObservations Actives.</returns>
        IList<GpuObservationDto> GetActivesGpuObservations();
    }
}
