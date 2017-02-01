//------------------------------------------------------------------------
// <copyright file="INationalFuelContractRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// National Fuel Contract Repository Interface
    /// </summary>
    public interface INationalFuelContractRepository : IRepository<NationalFuelContract>
    {
        /// <summary>
        /// Find Contract by its primary parameters.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <returns>
        /// National Fuel Contract
        /// </returns>
        NationalFuelContract FindByContract(
            DateTime effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber);

        /// <summary>
        /// Gets the effectives contracts pagination.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>List of national fuel contracts paginated.</returns>
        IList<NationalFuelContract> GetEffectivesContractsPagination(int pageSize, int pageNumber);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="fuelContracts">The fuel contracts.</param>
        void AddRange(IList<NationalFuelContract> fuelContracts);

        /// <summary>
        /// Counts all registers for national fuel contracts.
        /// </summary>
        /// <returns>Total of national contracts.</returns>
        int CountAll();

        /// <summary>
        /// Gets the effectives contracts pagination.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>List of national fuel contracts paginated.</returns>
        IList<NationalFuelContract> GetAllWithRealationships();

        /// <summary>
        /// Counts the contracs by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Number of contracts that match with the parameters.</returns>
        int CountContracsByParameters(NationalFuelContract parameters);

        /// <summary>
        /// Gets the contracs by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="take">The take.</param>
        /// <param name="skip">The skip.</param>
        /// <returns>
        /// Contracts that match with the parameters.
        /// </returns>
        IList<NationalFuelContract> GetContracsByParameters(NationalFuelContract parameters, int take, int skip);

        /// <summary>
        /// Gets the contracts by airline airport.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns></returns>
        IList<NationalFuelContract> GetContractsByAirlineAirport(NationalFuelContract contract);

        /// <summary>
        /// Gets max date for a contract.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <returns>
        /// The contract effective date.
        /// </returns>
        DateTime GetContractMaxDate(string airlineCode, string stationCode, string serviceCode, string providerNumber);
    }
}
