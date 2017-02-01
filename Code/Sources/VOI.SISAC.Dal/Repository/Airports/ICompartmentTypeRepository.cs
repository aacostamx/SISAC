//------------------------------------------------------------------------
// <copyright file="ICompartmentTypeRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    public interface ICompartmentTypeRepository : IRepository<CompartmentType>
    {
        /// <summary>
        /// Finds the compartment type by its identifier.
        /// </summary>
        /// <param name="id">The compartment's identifier.</param>
        /// <returns>The compartment specified.</returns>
        CompartmentType FindById(string id);

        /// <summary>
        /// Gets the actives compartment types.
        /// </summary>
        /// <returns>List of actives compatments.</returns>
        IList<CompartmentType> GetActiveCompartmentType();
    }
}
