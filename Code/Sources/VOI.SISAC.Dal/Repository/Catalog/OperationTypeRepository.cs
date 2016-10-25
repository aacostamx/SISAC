//------------------------------------------------------------------------
// <copyright file="OperationTypeRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Operations for Operation type
    /// </summary>
    public class OperationTypeRepository : Repository<OperationType>, IOperationTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public OperationTypeRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Operation type.
        /// </returns>
        public OperationType FindById(int id)
        {
            return this.DbContext.OperationTypes.FirstOrDefault(c => c.OperationTypeId == id);
        }
    }
}
