//------------------------------------------------------------------------
// <copyright file="IFunctionalAreaRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// FunctionalArea Repository Interface
    /// </summary>
    public interface IFunctionalAreaRepository : IRepository<FunctionalArea>
    {
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>FunctionalArea Entity.</returns>
        FunctionalArea FindById(long id);

        /// <summary>
        /// Gets the actives FunctionalAreas.
        /// </summary>
        /// <returns>FunctionalAreas marked as Actives.</returns>
        IList<FunctionalArea> GetActivesFunctionalAreas();
    }
}
