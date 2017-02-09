//------------------------------------------------------------------------
// <copyright file="InternationalFuelRateRepository.cs" company="AACOSTA">
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
    /// International Fuel Rate Repository
    /// </summary>
    public class InternationalFuelRateRepository : Repository<InternationalFuelRate>, IInternationalFuelRateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalFuelRateRepository"/> class.
        /// </summary>
        /// <param name="factory"></param>
        public InternationalFuelRateRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region InternationalFuelRateRepository Members
        /// <summary>
        /// Return Rate by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InternationalFuelRate FindById(long id)
        {
            var internationalFuelrate = this.DbContext.InternationalFuelRates
                .Where(c => c.InternationalFuelRateID == id)
                .Include(c => c.InternationalFuelContractConcept)
                .Include(c => c.InternationalFuelContractConcept.Provider)
                .Include(c => c.InternationalFuelContractConcept.FuelConcept)
                .FirstOrDefault();

            return internationalFuelrate;
        }

        /// <summary>
        /// Get all rates 
        /// </summary>
        /// <returns></returns>
        public IList<InternationalFuelRate> GetFuelRates()
        {
            return this.DbContext.InternationalFuelRates
                .Include(c => c.InternationalFuelContractConcept)
                .Include(c => c.InternationalFuelContractConcept.Provider)
                .Include(c => c.InternationalFuelContractConcept.FuelConcept)
                .ToList();
        }

        /// <summary>
        /// GEt a list of Rate in a period of time
        /// </summary>
        /// <param name="internationalFuelRate"></param>
        /// <returns></returns>
        public IList<InternationalFuelRate> GetAllSearchInternationalFuelRate(InternationalFuelRate internationalFuelRate)
        {
            return this.DbContext.InternationalFuelRates
                .Where(c => (c.RateStartDate >= internationalFuelRate.RateStartDate) &&
                (c.RateEndDate <= internationalFuelRate.RateEndDate))
                .Include(c => c.InternationalFuelContractConcept)
                .Include(c => c.InternationalFuelContractConcept.Provider)
                .Include(c => c.InternationalFuelContractConcept.FuelConcept)
                .ToList();
        }

        /// <summary>
        /// Delete a list of Fuel Rates
        /// </summary>
        /// <param name="internationalFuelRate"></param>
        /// <returns></returns>
        public IList<InternationalFuelRate> DeleteAllInternationalFuelRate(InternationalFuelRate internationalFuelRate)
        {
            return this.DbContext.InternationalFuelRates
                .RemoveRange(this.DbContext.InternationalFuelRates.Where(c =>
                (c.RateStartDate >= internationalFuelRate.RateStartDate) &&
                (c.RateEndDate <= internationalFuelRate.RateEndDate)))
                .ToList();
        }
        #endregion
    }
}
