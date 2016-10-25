//------------------------------------------------------------------------
// <copyright file="FunctionalAreaRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// FunctionalArea Repository
    /// </summary>
    public class FunctionalAreaRepository : Repository<FunctionalArea>, IFunctionalAreaRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionalAreaRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public FunctionalAreaRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region IFunctionalAreaRepository Members
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>FunctionalArea Entity.</returns>
        public FunctionalArea FindById(long id)
        {
            var functionalArea = this.DbContext.FunctionalAreas.Where(c => c.FunctionalAreaID == id).FirstOrDefault();
            return functionalArea;
        }

        /// <summary>
        /// Gets the Actives FunctionalAreas.
        /// </summary>
        /// <returns>FunctionalAreas marked as Actives.</returns>
        public IList<FunctionalArea> GetActivesFunctionalAreas()
        {
            return this.DbContext.FunctionalAreas.Where(c => c.Status).ToList();
        }
        #endregion
    }
}
