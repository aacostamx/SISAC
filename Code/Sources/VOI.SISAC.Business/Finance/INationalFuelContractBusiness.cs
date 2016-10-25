//----------------------------------------------------------------------------
// <copyright file="INationalFuelContractBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------
namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Interface National Fuel Contract Business
    /// </summary>
    public interface INationalFuelContractBusiness
    {
        /// <summary>
        /// Finds the national fuel contract by identifier.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// The national fuel contract.
        /// </returns>
        NationalFuelContractDto FindNationalFuelContractById(NationalFuelContractDto contract);

        /// <summary>
        /// Adds the National Fuel Contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        bool AddNationalFuelContract(NationalFuelContractDto contract);

        /// <summary>
        /// Deletes the National Fuel Contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// true if was deleted else false
        /// </returns>
        bool DeleteNationalFuelContract(NationalFuelContractDto contract);

        /// <summary>
        /// Updates the National Fuel Contract.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns>
        /// true if was updated else false
        /// </returns>
        bool UpdateNationalFuelContract(NationalFuelContractDto contract);

        /// <summary>
        /// Gets the national fuel contracts paginated.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>List of national fuel contract paginated.</returns>
        IList<NationalFuelContractDto> GetNationalFuelContractsPaginated(int pageSize, int pageNumber);

        /// <summary>
        /// Gets the national fuel contracts paginated.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>
        /// List of national fuel contract paginated.
        /// </returns>
        IList<NationalFuelContractDto> SearchNationalFuelContractsPaginated(NationalFuelContractDto parameters, int pageSize, int pageNumber);

        /// <summary>
        /// Count the records by the parameters given.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Retunrs contracts count.
        /// </returns>
        int CountSearchContracts(NationalFuelContractDto parameters);

        /// <summary>
        /// Count the total effectives contracts.
        /// </summary>
        /// <returns>Retunrs the total of effectives contracts.</returns>
        int CountEffectivesContracts();

        /// <summary>
        /// Inactivate the national fuel contract.
        /// </summary>
        /// <param name="effectiveDate">The effective date.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
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
            DateTime? endDateContract);


        /// <summary>
        /// Get Contracts by Airline and Airport
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        IList<NationalFuelContractDto> GetContractsByAirlineAirport(NationalFuelContractDto contract);
    }
}
