//------------------------------------------------------------------------
// <copyright file="AirportGroupRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Repository class from AirportGroupReposository
    /// </summary>
    public class AirportGroupRepository : Repository<AirportGroup>, IAirportGroupRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">Base param from constructor class</param>
        public AirportGroupRepository(IDbFactory factory) : base(factory)
        {

        }
        /// <summary>
        /// Find by Id method
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns></returns>
        public AirportGroup FindById(string id)
        {
            var airportgroup = this.DbContext.AirportGroups.Where(c => c.AirportGroupCode == id).FirstOrDefault();
            return airportgroup;
        }

        /// <summary>
        /// Get all actives Airport Groups
        /// </summary>
        /// <returns>List of Airports</returns>
        public IList<AirportGroup> GetActivesAirportGroup()
        {
            return this.DbContext.AirportGroups.Where(c => c.Status).ToList();
        }
    }
}
