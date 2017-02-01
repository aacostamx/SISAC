//------------------------------------------------------------------------
// <copyright file="ExchangeRatesRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using Entities.Finance;
    using Infrastructure;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// ExchangeRatesRepository Repository Class
    /// </summary>
    public class ExchangeRatesRepository : Repository<ExchangeRates>, IExchangeRatesRepository
    {
        /// <summary>
        /// ExchangeRatesRepository constructor
        /// </summary>
        /// <param name="factory"></param>
        public ExchangeRatesRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Find By Id
        /// </summary>
        /// <param name="exchanges"></param>
        /// <returns></returns>
        public ExchangeRates FindById(ExchangeRates exchanges)
        {
            var actives = this.DbContext.ExchangeRates.Where(
                    c => c.Year == exchanges.Year &&
                    c.Month == exchanges.Month &&
                    c.CurrencyCode == exchanges.CurrencyCode)
                .Include(p => p.Currency)
                .FirstOrDefault();
            return actives;
        }

        /// <summary>
        /// Find By Id As No Tracking
        /// </summary>
        /// <param name="exchanges"></param>
        /// <returns></returns>
        public ExchangeRates FindByIdNoTracking(ExchangeRates exchanges)
        {
            var actives = this.DbContext.ExchangeRates.AsNoTracking().Where(
                    c => c.Year == exchanges.Year &&
                    c.Month == exchanges.Month &&
                    c.CurrencyCode == exchanges.CurrencyCode)
                .Include(p => p.Currency)
                .FirstOrDefault();
            return actives;
        }

        /// <summary>
        /// Get Actives Exchanges Rates
        /// </summary>
        /// <returns></returns>
        public IList<ExchangeRates> GetActivesExchangeRates()
        {
            var actives = this.DbContext.ExchangeRates
                .Where(c => c.Status)
                .Include(p => p.Currency)
                .ToList();
            return actives;
        }
    }
}
