//------------------------------------------------------------------------
// <copyright file="ReconciliationToleranceRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using Infrastructure;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// ExchangeRatesRepository
    /// </summary>
    public class ReconciliationToleranceRepository : Repository<ReconciliationTolerance>, IReconciliationToleranceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReconciliationToleranceRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ReconciliationToleranceRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Find By Id
        /// </summary>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public ReconciliationTolerance FindById(ReconciliationTolerance tolerance)
        {
            var actives = this.DbContext.ReconciliationTolerances.Where(
                    c => c.ServiceCode == tolerance.ServiceCode &&
                    c.CurrencyCode == tolerance.CurrencyCode &&
                    c.ToleranceTypeCode == tolerance.ToleranceTypeCode)
                .Include(p => p.Service)
                .FirstOrDefault();
            return actives;
        }

        public ReconciliationTolerance FindByIdNoTracking(ReconciliationTolerance tolerance)
        {
            var actives = this.DbContext.ReconciliationTolerances.AsNoTracking().Where(
                    c => c.ServiceCode == tolerance.ServiceCode &&
                    c.CurrencyCode == tolerance.CurrencyCode &&
                    c.ToleranceTypeCode == tolerance.ToleranceTypeCode)
                .Include(p => p.Service)
                .Include(p => p.Currency)
                .Include(p => p.ToleranceType)
                .FirstOrDefault();
            return actives;
        }

        /// <summary>
        /// Get Actives Exchange Rates
        /// </summary>
        /// <returns></returns>
        public IList<ReconciliationTolerance> GetActivesReconciliationTolerances()
        {
            var actives = this.DbContext.ReconciliationTolerances
                .Where(c => c.Status)
                .Include(p => p.Service)
                .ToList();
            return actives;
        }
    }
}
