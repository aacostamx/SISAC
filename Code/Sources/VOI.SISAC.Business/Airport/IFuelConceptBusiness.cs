//------------------------------------------------------------------------
// <copyright file="IFuelConceptBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// Contract for FuelConceptBusiness methods
    /// </summary>
    public interface IFuelConceptBusiness
    {
        /// <summary>
        /// Gets all FuelConcepts.
        /// </summary>
        /// <returns>List of FuelConcepts.</returns>
        IList<FuelConceptDto> GetAllFuelConcepts();

        /// <summary>
        /// Finds the FuelConcept by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Entity FuelConcept.</returns>
        FuelConceptDto FindFuelConceptById(long id);

        /// <summary>
        /// Adds the FuelConcept.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddFuelConcept(FuelConceptDto entity);

        /// <summary>
        /// Deletes the FuelConcept.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteFuelConcept(FuelConceptDto entity);

        /// <summary>
        /// Updates the FuelConcept.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateFuelConcept(FuelConceptDto entity);

        /// <summary>
        /// Gets the Actives FuelConcept.
        /// </summary>
        /// <returns>FuelConcepts Actives.</returns>
        IList<FuelConceptDto> GetActivesFuelConcepts();
    }
}
