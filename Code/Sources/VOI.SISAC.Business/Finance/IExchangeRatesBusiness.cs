//------------------------------------------------------------------------
// <copyright file="IExchangeRatesBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using Dto.Finances;
    using System.Collections.Generic;

    /// <summary>
    /// IExchangeRatesBusiness Interface
    /// </summary>
    public interface IExchangeRatesBusiness
    {
        /// <summary>
        /// Get All Actives Exchange Rates
        /// </summary>
        /// <returns></returns>
        IList<ExchangeRatesDto> GetAllActivesExchangeRates();

        /// <summary>
        /// Find Exchange Rates by ID
        /// </summary>
        /// <param name="exchangeDto"></param>
        /// <returns></returns>
        ExchangeRatesDto FindExchangeRateById(ExchangeRatesDto exchangeDto);

        /// <summary>
        /// Add Exchange Rate
        /// </summary>
        /// <param name="exchangeDto"></param>
        /// <returns></returns>
        bool AddExchangeRate(ExchangeRatesDto exchangeDto);

        /// <summary>
        /// Delete Logic Exchange Rate
        /// </summary>
        /// <param name="exchangeDto"></param>
        /// <returns></returns>
        bool DeleteExchangeRate(ExchangeRatesDto exchangeDto);

        /// <summary>
        /// Update Exchange Rate
        /// </summary>
        /// <param name="exchangeDto"></param>
        /// <returns></returns>
        bool UpdateExchangeRate(ExchangeRatesDto exchangeDto);

        /// <summary>
        /// Get All Exchange Rates
        /// </summary>
        /// <returns></returns>
        IList<ExchangeRatesDto> GetAllExchangeRate();
    }
}
