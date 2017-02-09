//------------------------------------------------------------------------
// <copyright file="NationalFuelContractConceptRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------


namespace VOI.SISAC.Dal.Repository.Finance
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// International Fuel Contract Concept Repository
    /// </summary>
    public class NationalFuelContractConceptRepository : Repository<NationalFuelContractConcept>, INationalFuelContractConceptRepository
    {
        /// <summary>
        /// DB Factory
        /// </summary>
        /// <param name="factory"></param>
        public NationalFuelContractConceptRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region NationalFuelContractConceptRepository members

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="FuelContractConcept">The fuel contract concept.</param>
        /// <returns>
        /// National Fuel Contract Concept
        /// </returns>
        public NationalFuelContractConcept FindById(NationalFuelContractConcept FuelContractConcept)
        {
            var nationalFuelContractConcept = this.DbContext.NationalFuelContractConcepts
                .Where(c => c.NationalFuelContractConceptId == FuelContractConcept.NationalFuelContractConceptId)
                .Include(c => c.NationalFuelContract)
                .FirstOrDefault();
            return nationalFuelContractConcept;
        }

        /// <summary>
        /// Get all fuel concepts
        /// </summary>
        /// <returns>
        /// List of National Fuel Contract Concept
        /// </returns>
        public IList<NationalFuelContractConcept> GetFuelContractConcepts()
        {
            return this.DbContext.NationalFuelContractConcepts.ToList();
        }
        #endregion
    }
}
