//------------------------------------------------------------------------
// <copyright file="ServiceTypeRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Operations for Service type
    /// </summary>
    public class ServiceTypeRepository : Repository<ServiceType>, IServiceTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ServiceTypeRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Service type.
        /// </returns>
        public ServiceType FindById(string id)
        {
            return this.DbContext.ServiceTypes.FirstOrDefault(c => c.ServiceTypeCode == id);
        }
    }
}
