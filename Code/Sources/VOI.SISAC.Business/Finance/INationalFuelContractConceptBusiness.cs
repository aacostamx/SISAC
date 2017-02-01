//------------------------------------------------------------------------
// <copyright file="INationalFuelContractConceptBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System.Collections.Generic;
    using Dto.Finances;

    /// <summary>
    /// Interface National Fuel Contract Concept Business
    /// </summary>
    public interface INationalFuelContractConceptBusiness
    {
        /// <summary>
        /// Gets all Fuel Concept.
        /// </summary>
        /// <returns>List of FuelConcepts.</returns>
        IList<NationalFuelContractConceptDto> GetAllFuelConcept();

        /// <summary>
        /// Finds the national Fuel Contract Concept by identifier.
        /// </summary>
        /// <param name="dto">The identifier.</param>
        /// <returns>Entity national Fuel Contract Concept.</returns>
        NationalFuelContractConceptDto FindFuelConceptById(NationalFuelContractConceptDto dto);

        /// <summary>
        /// Adds the National Fuel Contract Concept.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddFuelConcept(NationalFuelContractConceptDto dto);

        /// <summary>
        /// Deletes the national Fuel Contract Concept.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteFuelConcept(NationalFuelContractConceptDto dto);

        /// <summary>
        /// Updates the National Fuel Contract Concept.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateFuelConcept(NationalFuelContractConceptDto dto);
    }
}
