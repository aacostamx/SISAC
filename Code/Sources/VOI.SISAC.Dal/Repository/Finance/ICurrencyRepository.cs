//------------------------------------------------------------------------
// <copyright file="ICurrencyRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;


    /// <summary>
    /// Interface for the specific operations in Currency
    /// </summary>
    public interface ICurrencyRepository : IRepository<Currency>
    {
        /// <summary>
        /// Finds the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Currency FindById(string id);

        /// <summary>
        /// Gets the active entity.
        /// </summary>
        /// <returns></returns>
        IList<Currency> GetActiveCurrency();

        /// <summary>
        /// Validate if the currencies exist.
        /// </summary>
        /// <param name="currencyCodes">The currency codes to validate.</param>
        /// <returns>Returns a list with the Currency Codes that do not exist, if all of them exist returns NULL.</returns>
        IList<string> ValidatedIfCurrencyExist(IList<string> currencyCodes);
    }
}
