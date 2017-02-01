//------------------------------------------------------------------------
// <copyright file="ManifestTimeConfigRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using VOI.SISAC.Dal.Infrastructure;
using VOI.SISAC.Entities.Airport;

namespace VOI.SISAC.Dal.Repository.Airports
{
    public class ManifestTimeConfigRepository : Repository<ManifestTimeConfig>, IManifestTimeConfigRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public ManifestTimeConfigRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>Airport Entity.</returns>
        public ManifestTimeConfig FindById(long id)
        {
            var ManifestTimeConfig = this.DbContext.ManifestTimeConfig.Where(c => c.ManifestTimeConfigID == id).FirstOrDefault();
            return ManifestTimeConfig;
        }

        /// <summary>
        /// Gets the Actives ManifestTimeConfig.
        /// </summary>
        /// <returns>ManifestTimeConfig marked as Actives.</returns>
        public IList<ManifestTimeConfig> GetActivesManifestTimeConfigs()
        {
            return this.DbContext.ManifestTimeConfig.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Gets the time configuration.
        /// </summary>
        /// <returns>The time configuration for the airline.</returns>
        public ManifestTimeConfig FindTimeConfigurationByAirline(string airlineCode)
        {
            return this.DbContext.ManifestTimeConfig.FirstOrDefault(c => c.AirlineCode == airlineCode);
        }
    }
}
