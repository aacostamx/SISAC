//------------------------------------------------------------------------
// <copyright file="AirplaneRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Airplane repository
    /// </summary>
    public class AirplaneRepository : Repository<Airplane>, IAirplaneRepository
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AirplaneRepository(IDbFactory factory) 
            : base(factory) 
        { 
        }
        #endregion

        /// <summary>
        /// Finds the airplane by its identifier.
        /// </summary>
        /// <param name="id">The airplane's identifier.</param>
        /// <returns>The Airplane specified.</returns>
        public Airplane FindById(string id)
        {
            var airplan = this.DbContext.Airplanes.FirstOrDefault(c => c.EquipmentNumber == id);
            return airplan;
        }

        /// <summary>
        /// Gets the actives airplane types.
        /// </summary>
        /// <returns>List of actives Airplanes</returns>
        public IList<Airplane> GetActiveAirplane()
        {
            return this.DbContext.Airplanes.Where(c => c.Status).ToList();
        }
    }
}
