//------------------------------------------------------------------------
// <copyright file="IMassiveUploadBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Massive Upload Interface
    /// </summary>
    public interface IMassiveUploadBusiness
    {
        /// <summary>
        /// Adds a list of Airports service contract.
        /// </summary>
        /// <param name="contracts">Contracts to insert.</param>
        /// <returns>
        /// A list with the possible errors found when try inserting the records.
        /// If there are no errors returns a empty list.
        /// </returns>
        IList<string> AirportServiceContractAddRange(IList<AirportServiceContractDto> contracts);

        /// <summary>
        /// Fuel contract add range.
        /// </summary>
        /// <param name="contracts">Contracts to insert.</param>
        /// <returns>List of Fuel Contracts.</returns>
        IList<string> InternationalFuelContractAddRange(IList<InternationalFuelContractDto> contracts);


        /// <summary>
        /// Fuel rates add range.
        /// </summary>
        /// <param name="rates">Rates to insert.</param>
        /// <returns>List of Error in the Fuel rates.</returns>
        IList<string> InternationalFuelRatesAddRange(IList<InternationalFuelRateFileDto> rates);

    }
}
