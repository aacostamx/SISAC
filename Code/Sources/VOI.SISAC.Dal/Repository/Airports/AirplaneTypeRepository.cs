//------------------------------------------------------------------------
// <copyright file="AirportRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Repository for Ariplane Type
    /// </summary>
    public class AirplaneTypeRepository : Repository<AirplaneType>, IAirplaneTypeRepository
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AirplaneTypeRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion

        /// <summary>
        /// Finds the airplane type by its identifier.
        /// </summary>
        /// <param name="id">The Airplane Type's identifier.</param>
        /// <returns>The Airplane Type specified.</returns>
        public AirplaneType FindById(string id)
        {
            var airplaneType = this.DbContext.AirplaneTypes.FirstOrDefault(c => c.AirplaneModel == id);
            return airplaneType;
        }

        /// <summary>
        /// Gets the actives airplane types.
        /// </summary>
        /// <returns>List of actives Airplane Types.</returns>
        public IList<AirplaneType> GetActiveAirplaneType()
        {
            return this.DbContext.AirplaneTypes
                                               
                                               .Where(c => c.Status)
                                               .Include(c => c.Airplanes)
                                               .Include(c => c.CompartmentType)
                                               .ToList();
        }
    }
}
