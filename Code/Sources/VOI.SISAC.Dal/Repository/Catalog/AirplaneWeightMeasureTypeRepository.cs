//------------------------------------------------------------------------
// <copyright file="AirplaneWeightMeasureTypeRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Operations for Airplane weight type
    /// </summary>
    public class AirplaneWeightMeasureTypeRepository : Repository<AirplaneWeightMeasureType>, IAirplaneWeightMeasureTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneWeightMeasureTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AirplaneWeightMeasureTypeRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Airplane weight measure type.
        /// </returns>
        public AirplaneWeightMeasureType FindById(int id)
        {
            return this.DbContext.AirplaneWeightMeasureTypes.FirstOrDefault(c => c.AirplaneWeightMeasureId == id);
        }
    }
}