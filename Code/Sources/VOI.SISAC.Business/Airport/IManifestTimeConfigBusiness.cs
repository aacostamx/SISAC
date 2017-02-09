//------------------------------------------------------------------------
// <copyright file="IManifestTimeConfigBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System.Collections.Generic;
    using Dto.Airports;

    /// <summary>
    /// Contract for ManifestTimeConfig methods
    /// </summary>
    public interface IManifestTimeConfigBusiness
    {
        /// <summary>
        /// Gets all ManifestTimeConfig.
        /// </summary>
        /// <returns>List of ManifestTimeConfigs.</returns>
        IList<ManifestTimeConfigDto> GetAllManifestTimeConfigs();

        /// <summary>
        /// Finds the ManifestTimeConfigDto by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity ManifestTimeConfigDto.</returns>
        ManifestTimeConfigDto FindManifestTimeConfigById(long id);

        /// <summary>
        /// Adds the ManifestTimeConfigDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddManifestTimeConfig(ManifestTimeConfigDto entity);

        /// <summary>
        /// Deletes the ManifestTimeConfigDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteManifestTimeConfig(ManifestTimeConfigDto entity);

        /// <summary>
        /// Physical Delete
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>Boolean</returns>
        bool PhysicalDeleteManifestTimeConfig(ManifestTimeConfigDto entity);

        /// <summary>
        /// Updates the ManifestTimeConfigDto.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateManifestTimeConfig(ManifestTimeConfigDto entity);

        /// <summary>
        /// Gets the Actives ManifestTimeConfigs.
        /// </summary>
        /// <returns>ManifestTimeConfigs Actives.</returns>
        IList<ManifestTimeConfigDto> GetActivesManifestTimeConfigs();
    }
}
