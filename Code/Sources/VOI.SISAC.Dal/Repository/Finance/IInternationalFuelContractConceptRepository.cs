//------------------------------------------------------------------------
// <copyright file="IInternationalFuelContractConceptRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    public interface IInternationalFuelContractConceptRepository : IRepository<InternationalFuelContractConcept>
    {
        /// <summary>
        /// Get the fuel concepts by id
        /// </summary>
        /// <param name="FuelContractConcept"></param>
        /// <returns></returns>
        InternationalFuelContractConcept FindById(InternationalFuelContractConcept FuelContractConcept);

        /// <summary>
        /// Get all fuel concepts
        /// </summary>
        /// <returns></returns>
        IList<InternationalFuelContractConcept> GetFuelContractsConcepts();
    }
}
