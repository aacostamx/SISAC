//------------------------------------------------------------------------
// <copyright file="ICurrencyBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Contract for Currency operations
    /// </summary>
    public interface ICurrencyBusiness
    {
        /// <summary>
        /// Gets all Currencies.
        /// </summary>
        /// <returns>List of Airports.</returns>
        IList<CurrencyDto> GetAllCurrency();

        /// <summary>
        /// Finds the Currency by its identifier.
        /// </summary>
        /// <param name="id">The Currency identifier.</param>
        /// <returns>Currency Entity.</returns>
        CurrencyDto FindCurrencyById(string id);

        /// <summary>
        /// Adds a new Currency.
        /// </summary>
        /// <param name="currencyDto">The entity</param>
        /// <returns><c>true</c> if was added <c>false</c> otherwise.</returns>
        bool AddCurrency(CurrencyDto currencyDto);

        /// <summary>
        /// Deletes the Currency.
        /// </summary>
        /// <param name="currencyDto">The entity.</param>
        /// <returns><c>true</c> if was deleted <c>false</c> otherwise.</returns>
        bool DeleteCurrency(CurrencyDto currencyDto);

        /// <summary>
        /// Updates the Currency.
        /// </summary>
        /// <param name="currencyDto">The entity.</param>
        /// <returns><c>true</c> if was updated <c>false</c> otherwise.</returns>
        bool UpdateCurrency(CurrencyDto currencyDto);

        /// <summary>
        /// Gets the all Currencies actives.
        /// </summary>
        /// <returns>List of Currency actives.</returns>
        IList<CurrencyDto> GetActivesCurrency();
    }
}
