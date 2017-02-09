//------------------------------------------------------------------------
// <copyright file="IServiceCalculationTypeRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Contract for Service contract type repository
    /// </summary>
    public interface IServiceCalculationTypeRepository : IRepository<ServiceCalculationType>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Service calculation type</returns>
        ServiceCalculationType FindById(int id);
    }
}
