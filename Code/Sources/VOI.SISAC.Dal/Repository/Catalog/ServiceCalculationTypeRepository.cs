//------------------------------------------------------------------------
// <copyright file="ServiceCalculationTypeRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Operations for Service calculation type repository
    /// </summary>
    public class ServiceCalculationTypeRepository : Repository<ServiceCalculationType>, IServiceCalculationTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCalculationTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ServiceCalculationTypeRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Service calculation type
        /// </returns>
        public ServiceCalculationType FindById(int id)
        {
            return this.DbContext.ServiceCalculationTypes.FirstOrDefault(c => c.CalculationTypeId == id);
        }
    }
}
