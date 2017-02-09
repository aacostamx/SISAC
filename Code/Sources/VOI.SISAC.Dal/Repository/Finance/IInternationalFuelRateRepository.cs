//------------------------------------------------------------------------
// <copyright file="IInternationalFuelRateReporistory.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// International Fuel Rate Reporistory Interface
    /// </summary>
    public interface IInternationalFuelRateRepository : IRepository<InternationalFuelRate>
    {
        /// <summary>
        /// Find the Fuel Rate by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        InternationalFuelRate FindById(long id);

        /// <summary>
        /// Get the Actives Fuel Rates
        /// </summary>
        /// <returns></returns>
        IList<InternationalFuelRate> GetFuelRates();

        /// <summary>
        /// Search Rate in base Dates parameters
        /// </summary>
        /// <param name="internationalFuelRate"></param>
        /// <returns></returns>
        IList<InternationalFuelRate> GetAllSearchInternationalFuelRate(InternationalFuelRate internationalFuelRate);

        /// <summary>
        /// Delete a List of Fuel Rates 
        /// </summary>
        /// <param name="internationalFuelRate"></param>
        /// <returns></returns>
        IList<InternationalFuelRate> DeleteAllInternationalFuelRate(InternationalFuelRate internationalFuelRate);
    }
}
