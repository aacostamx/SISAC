//------------------------------------------------------------------------
// <copyright file="ICompartmentTypeBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// Contract  for CompartmentType 
    /// </summary>
    public interface ICompartmentTypeBusiness
    {
        /// <summary>
        /// Gets all compartment types.
        /// </summary>
        /// <returns>All compartments.</returns>
        IList<CompartmentTypeDto> GetAllCompartmentType();

        /// <summary>
        /// Finds the compartment types by its identifier.
        /// </summary>
        /// <param name="id">The CompartmentType identifier.</param>
        /// <returns>CompartmentType Entity.</returns>
        CompartmentTypeDto FindCompartmentById(string id);

        /// <summary>
        /// Gets the all actives compartment types.
        /// </summary>
        /// <returns>List of actives CompartmentTypes.</returns>
        IList<CompartmentTypeDto> GetActiveCompartmentType();
    }
}
