//------------------------------------------------------------------------
// <copyright file="DelayRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VOI.SISAC.Dal.Infrastructure;
using VOI.SISAC.Entities.Airport;

namespace VOI.SISAC.Dal.Repository.Airports
{
    /// <summary>
    /// Delay Repository
    /// </summary>
    public class DelayRepository : Repository<Delay>, IDelayRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        public DelayRepository(IDbFactory factory)
            : base(factory)
        {

        }

        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>Airport Entity.</returns>
        public Delay FindById(string id)
        {
            var Delay = this.DbContext.Delay
                .Where(c => c.DelayCode == id)
                .Include(c => c.FunctionalArea)
                .FirstOrDefault();
            return Delay;
        }

        /// <summary>
        /// Gets the Actives delay.
        /// </summary>
        /// <returns>delay marked as Actives.</returns>
        public IList<Delay> GetActivesDelays()
        {
            return this.DbContext.Delay
                .Where(c => c.Status)
                .Include(c => c.FunctionalArea)
                .ToList();
        }
    }
}
