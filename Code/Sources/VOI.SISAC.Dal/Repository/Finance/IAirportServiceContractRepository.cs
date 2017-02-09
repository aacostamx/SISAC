//------------------------------------------------------------------------
// <copyright file="IAirportServiceContractRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Interface for specific operations in ServiceContract
    /// </summary>
    public interface IAirportServiceContractRepository : IRepository<AirportServiceContract>
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
        AirportServiceContract FindById(
            DateTime effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber);

        /// <summary>
        /// Gets a list of the actives contracts.
        /// </summary>
        /// <returns>
        /// List of actives contracts.
        /// </returns>
        IList<AirportServiceContract> GetActivesContracts();

        /// <summary>
        /// Gets the contracts for airport and airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <returns>List of contracts for airline and airport.</returns>
        IList<AirportServiceContract> GetContractsForAirportAirline(string airlineCode, string airportCode);

        /// <summary>
        /// Gets the contracts by the parameters past in the object.
        /// </summary>
        /// <param name="parameters">The parameters to match in the query.</param>
        /// <returns>List of contracts that match with the specified parameters.</returns>
        IList<AirportServiceContract> GetContractsByParameters(AirportServiceContract parameters);

        /// <summary>
        /// Gets all contracts.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>List of contracts that match with the primary Key except Effective Date.</returns>
        DateTime GetAllContractsByMaxDate(string airlineCode, string stationCode, string serviceCode, string providerNumber, string costCenterNumber);

        /// <summary>
        /// Adds the given list of airport service contracs.
        /// </summary>
        /// <param name="contracts">List of airport service contracts.</param>
        void AddRange(IList<AirportServiceContract> contracts);

        ///// <summary>
        ///// Gets the server date time.
        ///// </summary>
        ///// <returns></returns>
        //DateTime GetServerDateTime();
    }
}