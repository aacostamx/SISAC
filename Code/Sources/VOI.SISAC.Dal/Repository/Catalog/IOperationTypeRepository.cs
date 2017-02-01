//------------------------------------------------------------------------
// <copyright file="IOperationTypeRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Contract for Operation type
    /// </summary>
    public interface IOperationTypeRepository : IRepository<OperationType>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Operation type.</returns>
        OperationType FindById(int id);
    }
}
