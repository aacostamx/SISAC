//------------------------------------------------------------------------
// <copyright file="IInternationalFuelContractConceptBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using Dto.Finances;
    using System.Collections.Generic;

    /// <summary>
    /// Interface International Fuel Contract Concept Business
    /// </summary>
    public interface IInternationalFuelContractConceptBusiness
    {
        /// <summary>
        /// Gets all FuelConcept.
        /// </summary>
        /// <returns>List of FuelConcepts.</returns>
        IList<InternationalFuelContractConceptDto> GetAllFuelConcept();

        /// <summary>
        /// Finds the InternationalFuelContractConceptDto by identifier.
        /// </summary>
        /// <param name="dto">The identifier.</param>
        /// <returns>Entity InternationalFuelContractConceptDto.</returns>
        InternationalFuelContractConceptDto FindFuelConceptById(InternationalFuelContractConceptDto dto);

        /// <summary>
        /// Adds the InternationalFuelContractConceptDto.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddFuelConcept(InternationalFuelContractConceptDto dto);

        /// <summary>
        /// Deletes the InternationalFuelContractConceptDto.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteFuelConcept(InternationalFuelContractConceptDto dto);

        /// <summary>
        /// Updates the InternationalFuelContractConceptDto.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateFuelConcept(InternationalFuelContractConceptDto dto);
    }
}
