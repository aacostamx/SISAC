//------------------------------------------------------------------------
// <copyright file="CurrencyRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Specific operations for Currency
    /// </summary>
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AirportRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CurrencyRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion

        /// <summary>
        /// Finds the Currency by its identifier.
        /// </summary>
        /// <param name="id">Currency's identifier.</param>
        /// <returns>Instance of Currency.</returns>
        public Currency FindById(string id)
        {
            var currency = this.DbContext.Currencies.Where(c => c.CurrencyCode == id).FirstOrDefault();
            return currency;
        }

        /// <summary>
        /// Gets the active currency.
        /// </summary>
        /// <returns>List of the actives Currencies.</returns>
        public IList<Currency> GetActiveCurrency()
        {
            return this.DbContext.Currencies.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validate if the currencies exist.
        /// </summary>
        /// <param name="currencyCodes">The currency codes to validate.</param>
        /// <returns>Returns a list with the Currency Codes that do not exist, if all of them exist returns NULL.</returns>
        public IList<string> ValidatedIfCurrencyExist(IList<string> currencyCodes)
        {
            IList<string> notFound = new List<string>();
            IList<Currency> currencyList = this.DbContext.Currencies.Where(c => c.Status).ToList();

            notFound = currencyCodes.Except(currencyList.Select(c => c.CurrencyCode)).ToList();
            return notFound;
        }
    }
}
