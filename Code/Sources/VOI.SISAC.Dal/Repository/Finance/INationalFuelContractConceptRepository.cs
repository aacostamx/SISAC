//------------------------------------------------------------------------
// <copyright file="INationalFuelContractConceptRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// IInternational Fuel Contract Concept Repository Interface
    /// </summary>
    public interface INationalFuelContractConceptRepository : IRepository<NationalFuelContractConcept>
    {
        /// <summary>
        /// Get the fuel concepts by id
        /// </summary>
        /// <param name="FuelContractConcept">The fuel contract concept.</param>
        /// <returns>National Fuel Contract Concept</returns>
        NationalFuelContractConcept FindById(NationalFuelContractConcept FuelContractConcept);

        /// <summary>
        /// Get all fuel concepts
        /// </summary>
        /// <returns>List of National Fuel Contract Concept</returns>
        IList<NationalFuelContractConcept> GetFuelContractConcepts();
    }
}
