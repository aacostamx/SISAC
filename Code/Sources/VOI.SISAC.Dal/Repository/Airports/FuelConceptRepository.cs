//------------------------------------------------------------------------
// <copyright file="FuelConceptRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// FuelConcept Repository
    /// </summary>
    public class FuelConceptRepository : Repository<FuelConcept>, IFuelConceptRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuelConceptRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public FuelConceptRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region IFuelConceptRepository Members
        /// <summary>
        /// Finds by the entity's identifier.
        /// </summary>
        /// <param name="id">The entity's identifier.</param>
        /// <returns>FuelConcept Entity.</returns>
        public FuelConcept FindById(long id)
        {
            var fuelConcepts = this.DbContext.FuelConcepts.Where(c => c.FuelConceptID == id).FirstOrDefault();
            return fuelConcepts;
        }

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="fuelConceptName">Name of the fuel concept.</param>
        /// <returns>FuelConcept.</returns>
        public FuelConcept FindByName(string fuelConceptName)
        {
            var fuelConcepts = this.DbContext.FuelConcepts.Where(c => c.FuelConceptName == fuelConceptName).FirstOrDefault();
            return fuelConcepts;
        }

        /// <summary>
        /// Gets the Actives FuelConcepts.
        /// </summary>
        /// <returns>FuelConcepts marked as Actives.</returns>
        public IList<FuelConcept> GetActivesFuelConcepts()
        {
            return this.DbContext.FuelConcepts.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validate if the Fuel Concept exist.
        /// </summary>
        /// <param name="fuelConcept">The fuel concept to validate.</param>
        /// <returns>Returns a list with the Fuel Concept that do not exist.</returns>
        public IList<string> ValidatedIfFuelConceptExist(IList<string> fuelConcepts)
        {
            IList<string> notFound = new List<string>();
            IList<FuelConcept> fuelConceptList = this.DbContext.FuelConcepts.Where(c => c.Status).ToList();

            notFound = fuelConcepts.Except(fuelConceptList.Select(c => c.FuelConceptName)).ToList();
            return notFound;
        }
        #endregion
    }
}
