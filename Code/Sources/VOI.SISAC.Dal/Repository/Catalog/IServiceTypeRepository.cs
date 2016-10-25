//------------------------------------------------------------------------
// <copyright file="IServiceTypeRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Operations for service type
    /// </summary>
    public interface IServiceTypeRepository : IRepository<ServiceType>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Service type.</returns>
        ServiceType FindById(string id);
    }
}
