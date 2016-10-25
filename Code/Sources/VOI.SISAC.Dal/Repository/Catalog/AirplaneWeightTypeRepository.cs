//------------------------------------------------------------------------
// <copyright file="AirplaneWeightTypeRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    public class AirplaneWeightTypeRepository : Repository<AirplaneWeightType>, IAirplaneWeightTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneWeightTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AirplaneWeightTypeRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Airplane weight type.
        /// </returns>
        public AirplaneWeightType FindById(string id)
        {
            return this.DbContext.AirplaneWeightTypes.FirstOrDefault(c => c.AirplaneWeightCode == id);
        }
    }
}
