//------------------------------------------------------------------------
// <copyright file="IAirplaneWeightMeasureTypeRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Contract for Airplane weight type
    /// </summary>
    public interface IAirplaneWeightMeasureTypeRepository : IRepository<AirplaneWeightMeasureType>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Airplane weight type.</returns>
        AirplaneWeightMeasureType FindById(int id);
    }
}
