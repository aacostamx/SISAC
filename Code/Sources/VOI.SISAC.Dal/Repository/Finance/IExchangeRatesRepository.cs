//------------------------------------------------------------------------
// <copyright file="IExchangeRatesRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------


namespace VOI.SISAC.Dal.Repository.Finance
{
    using Entities.Finance;
    using Infrastructure;
    using System.Collections.Generic;

    /// <summary>
    /// Interface IExchangeRatesRepository
    /// </summary>
    public interface IExchangeRatesRepository : IRepository<ExchangeRates>
    {
        /// <summary>
        /// Find By Id
        /// </summary>
        /// <param name="exchanges"></param>
        /// <returns></returns>
        ExchangeRates FindById(ExchangeRates exchanges);

        /// <summary>
        /// Find By Id No Tracking
        /// </summary>
        /// <param name="exchanges"></param>
        /// <returns></returns>
        ExchangeRates FindByIdNoTracking(ExchangeRates exchanges);

        /// <summary>
        /// Get Actives Exchange Rates 
        /// </summary>
        /// <returns></returns>
        IList<ExchangeRates> GetActivesExchangeRates();
    }
}
