//------------------------------------------------------------------------
// <copyright file="IManifestTimeConfigRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Repository ManifestTimeConfig Interface
    /// </summary>
    public interface IManifestTimeConfigRepository : IRepository<ManifestTimeConfig>
    {
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>ManifestTimeConfig Entity.</returns>
        ManifestTimeConfig FindById(long id);

        /// <summary>
        /// Gets the actives ManifestTimeConfigs.
        /// </summary>
        /// <returns>ManifestTimeConfigs marked as Actives.</returns>
        IList<ManifestTimeConfig> GetActivesManifestTimeConfigs();

        /// <summary>
        /// Gets the time configuration.
        /// </summary>
        /// <returns>The time configuration for the airline.</returns>
        ManifestTimeConfig FindTimeConfigurationByAirline(string airlineCode);
    }
}
