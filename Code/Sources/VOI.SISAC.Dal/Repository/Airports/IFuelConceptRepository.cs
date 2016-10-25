//------------------------------------------------------------------------
// <copyright file="IFuelConceptRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// FuelConcept Repository Interface
    /// </summary>
    public interface IFuelConceptRepository : IRepository<FuelConcept>
    {
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>FuelConcept Entity.</returns>
        FuelConcept FindById(long id);


        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="fuelConceptName">Name of the fuel concept.</param>
        /// <returns>FuelConcept.</returns>
        FuelConcept FindByName(string fuelConceptName);

        /// <summary>
        /// Gets the actives AccountingAccounts.
        /// </summary>
        /// <returns>FuelConcept marked as Actives.</returns>
        IList<FuelConcept> GetActivesFuelConcepts();

        /// <summary>
        /// Validate if the Fuel Concept exist.
        /// </summary>
        /// <param name="fuelConcept">The fuel concept to validate.</param>
        /// <returns>Returns a list with the Fuel Concept that do not exist.</returns>
        IList<string> ValidatedIfFuelConceptExist(IList<string> fuelConcepts);
    }
}
