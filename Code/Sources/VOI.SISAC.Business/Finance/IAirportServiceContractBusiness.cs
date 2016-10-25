//------------------------------------------------------------------------
// <copyright file="IAirportServiceContractBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Contract for Airport service contract operations.
    /// </summary>
    public interface IAirportServiceContractBusiness
    {
        /// <summary>
        /// Finds the contract by its composite id.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>
        /// The specified contract.
        /// </returns>
        AirportServiceContractDto FindById(
            DateTime? effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber);

        /// <summary>
        /// Gets a list of the effective contracts.
        /// </summary>
        /// <returns>
        /// List of effective contracts.
        /// </returns>
        IList<AirportServiceContractDto> GetEffectiveContracts();

        /// <summary>
        /// Gets the contracts for airport and airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <returns>List of contracts for airline and airport.</returns>
        IList<AirportServiceContractDto> GetContractsForAirportAirline(string airlineCode, string airportCode);

        /// <summary>
        /// Gets the contracts by the parameters past in the object.
        /// </summary>
        /// <param name="parameters">The parameters to match in the query.</param>
        /// <returns>List of contracts that match with the specified parameters.</returns>
        IList<AirportServiceContractDto> GetContractsByParameters(AirportServiceContractDto parameters);

        /// <summary>
        /// Adds the airport sercive contract.
        /// </summary>
        /// <param name="airportServiceContractDto">The airport service contract dto.</param>
        /// <returns><c>true</c> if was added <c>false</c> otherwise.</returns>
        bool AddAirportSerciveContract(AirportServiceContractDto airportServiceContractDto);

        /// <summary>
        /// Updates the airport sercive contract.
        /// </summary>
        /// <param name="airportServiceContractDto">The airport service contract dto.</param>
        /// <returns><c>true</c> if was updated <c>false</c> otherwise.</returns>
        bool UpdateAirportSerciveContract(AirportServiceContractDto airportServiceContractDto);

        /// <summary>
        /// Deletes the airport sercive contract.
        /// </summary>
        /// <param name="airportServiceContractDto">The airport service contract dto.</param>
        /// <returns><c>true</c> if was deleted <c>false</c> otherwise.</returns>
        bool DeleteAirportSerciveContract(AirportServiceContractDto airportServiceContractDto);

        /// <summary>
        /// Inactivate the airport sercive contract.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <param name="endDateContract">The end date contract.</param>
        /// <returns>
        ///   <c>true</c> if inactivate successfully, <c>false</c> otherwise.
        /// </returns>
        bool InactivateAirportSerciveContract(
            DateTime? effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber,
            DateTime? endDateContract);
    }
}
