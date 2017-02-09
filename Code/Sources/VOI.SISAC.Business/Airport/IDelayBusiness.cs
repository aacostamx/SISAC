//------------------------------------------------------------------------
// <copyright file="IDelayBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System.Collections.Generic;
    using Dto.Airports;

    /// <summary>
    /// Contract for delay methods
    /// </summary>
    public interface IDelayBusiness
    {
        /// <summary>
        /// Gets all delay.
        /// </summary>
        /// <returns>List of delays.</returns>
        IList<DelayDto> GetAllDelays();

        /// <summary>
        /// Finds the DelayDto by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity DelayDto.</returns>
        DelayDto FindDelayById(string id);

        /// <summary>
        /// Adds the DelayDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddDelay(DelayDto entity);

        /// <summary>
        /// Deletes the DelayDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteDelay(DelayDto entity);

        /// <summary>
        /// Physical Delete
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Boolean</returns>
        bool PhysicalDeleteDelay(DelayDto entity);

        /// <summary>
        /// Updates the DelayDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateDelay(DelayDto entity);

        /// <summary>
        /// Gets the Actives delays.
        /// </summary>
        /// <returns>delays Actives.</returns>
        IList<DelayDto> GetActivesDelays();
    }
}
