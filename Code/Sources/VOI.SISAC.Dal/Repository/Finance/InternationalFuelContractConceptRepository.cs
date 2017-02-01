//------------------------------------------------------------------------
// <copyright file="InternationalFuelContractConceptRepository.cs" company="Volaris">
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
    public class InternationalFuelContractConceptRepository : Repository<InternationalFuelContractConcept>, IInternationalFuelContractConceptRepository
    {
        /// <summary>
        /// DB Factory
        /// </summary>
        /// <param name="factory"></param>
        public InternationalFuelContractConceptRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region InternationalFuelContractConceptRepository members
        /// <summary>
        /// Get Fuel Contract Concept by id
        /// </summary>
        /// <param name="FuelContractConcept"></param>
        /// <returns></returns>
        public InternationalFuelContractConcept FindById(InternationalFuelContractConcept FuelContractConcept)
        {
            var internationalFuelContractConcept = this.DbContext.InternationalFuelContractConcepts
                .Where(c => c.InternationalFuelContractConceptID == FuelContractConcept.InternationalFuelContractConceptID)
                .Include(c => c.InternationalFuelContract)
                .FirstOrDefault();
            return internationalFuelContractConcept;
        }

        /// <summary>
        /// Get all Fuel Contracts Concepts
        /// </summary>
        /// <returns></returns>
        public IList<InternationalFuelContractConcept> GetFuelContractsConcepts()
        {
            return this.DbContext.InternationalFuelContractConcepts
                //.Include(c => c.InternationalFuelContract)
                .ToList();
        }
        #endregion
    }
}
