//------------------------------------------------------------------------
// <copyright file="CompartmentTypeRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// CompartmentType Repository
    /// </summary>
    public class CompartmentTypeRepository : Repository<CompartmentType>, ICompartmentTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompartmentTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CompartmentTypeRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Finds the compartment type by its identifier.
        /// </summary>
        /// <param name="id">The compartment's identifier.</param>
        /// <returns>
        /// The compartment specified.
        /// </returns>
        public CompartmentType FindById(string id)
        {
            return this.DbContext.CompartmentTypes.Find(id);
        }

        /// <summary>
        /// Gets the actives compartment types.
        /// </summary>
        /// <returns>
        /// List of actives compatments.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public System.Collections.Generic.IList<CompartmentType> GetActiveCompartmentType()
        {
            return this.DbContext.CompartmentTypes.Where(c => c.Status).ToList();
        }
    }
}
