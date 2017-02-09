//------------------------------------------------------------------------
// <copyright file="IInternationalFuelRateBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System.Collections.Generic;
    using Entities.Finance;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// International Fuel Rate Business Interface
    /// </summary>
    public interface IInternationalFuelRateBusiness
    {
        /// <summary>
        /// Get all Fuel Rates
        /// </summary>
        /// <returns>List of Fuel Rate</returns>
        IList<InternationalFuelRateDto> GetAllInternationalFuelRates();

        /// <summary>
        /// Gets Fuel Rate by identifiers
        /// </summary>
        /// <param name="id">id parameter</param>
        /// <returns>List of Fuel Rate by id</returns>
        InternationalFuelRateDto FindInternationalFuelRateById(long id);

        /// <summary>
        /// Delete a Fuel Rate
        /// </summary>
        /// <param name="internationalFuelRatedto">InternationalFuelRate Object</param>
        /// <returns>boolean object</returns>
        bool DeleteInternationalFuelRate(InternationalFuelRateDto internationalFuelRatedto);

        /// <summary>
        /// Update information of Fuel Rates.
        /// </summary>
        /// <param name="internationalFuelRatedto">InternationalFuelRate Object</param>
        /// <returns>boolean object</returns>
        bool UpdateInternationalFuelRate(InternationalFuelRateDto internationalFuelRatedto);

        /// <summary>
        /// Get the rate in base of Dates Parameters
        /// </summary>
        /// <param name="internationalFuelRatedto">InternationalFuelRate Object</param>
        /// <returns>A list of Fuel Rates</returns>
        IList<InternationalFuelRateDto> GetAllSearchInternationalFuelRates(InternationalFuelRateDto internationalFuelRatedto);

        /// <summary>
        /// Delete a range of Fuel Rates
        /// </summary>
        /// <param name="intFuelRates">InternationalFuelRate Object</param>
        /// <returns>list of Fuel Rates</returns>
        IList<InternationalFuelRateDto> DeleteRangeofFuelRates(InternationalFuelRateDto intFuelRates);

        ///// <summary>
        ///// This Method validates data of the contracts and concepts before the insert of Fuel Rates.
        ///// </summary>
        ///// <param name="internationalFuelRateFileDto">InternationalFuelRate File Object</param>
        ///// <returns>List of Errors</returns>
        //IList<string> ValidateInternationalFuelRates(IList<InternationalFuelRateFileDto> internationalFuelRateFileDto);

        /// <summary>
        /// Get active contracts
        /// </summary>
        /// <param name="fileFuelRatesDto">fileFuelRates object</param>
        /// <returns>List of contracts</returns>
        IList<InternationalFuelContractDto> GetContracts(IList<InternationalFuelRateFileDto> fileFuelRatesDto);
    }
}
