//----------------------------------------------------------------------------
// <copyright file="IInternationalFuelContractBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------
namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Finances;

    /// <summary>
    /// Interface InternationalFuelContractBusiness
    /// </summary>
    public interface IInternationalFuelContractBusiness
    {                
        /// <summary>
        /// Gets all InternationalFuelContracts.
        /// </summary>
        /// <param name="entity">entity parameter</param>
        /// <param name="statusSearch">status Search</param>
        /// <returns>List of International Fuel Contracts.</returns>
        IList<InternationalFuelContractDto> GetAllSearchedInternationalFuelContracts(InternationalFuelContractDto entity, string statusSearch);

        /// <summary>
        /// Finds the InternationalFuelContract by identifier.
        /// </summary>
        /// <param name="entity">The identifier.</param>
        /// <returns>Entity InternationalFuelContract.</returns>
        InternationalFuelContractDto FindInternationalFuelContractById(InternationalFuelContractDto entity);

        /// <summary>
        /// Adds the InternationalFuelContract.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        bool AddInternationalFuelContract(InternationalFuelContractDto entity);

        /// <summary>
        /// Deletes the InternationalFuelContract.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        bool DeleteInternationalFuelContract(InternationalFuelContractDto entity);

        /// <summary>
        /// Inactive the InternationalFuelContract.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="endDateContract">The Date EndDate.</param>
        /// <returns>true if was deleted else false</returns>
        bool InactiveInternationalFuelContract(InternationalFuelContractDto entity, DateTime? endDateContract);

        /// <summary>
        /// Updates the InternationalFuelContract.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        bool UpdateInternationalFuelContract(InternationalFuelContractDto entity);

        /// <summary>
        /// Gets the Actives InternationalFuelContracts.
        /// </summary>
        /// <returns>AccountingAccounts Actives.</returns>
        IList<InternationalFuelContractDto> GetActivesLastEffectiveDateInternationalFuelContracts();

        /// <summary>
        /// Validate Fuel Contracts the InternationalFuelContract.
        /// </summary>
        /// <param name="entityList">The entity.</param>
        /// <returns>errors List<string> </string></returns>
        IList<string> ValidateFuelContracts(IList<InternationalFuelContractDto> entityList);

        /// <summary>
        /// GetActivesFuelContracts
        /// </summary>
        /// <returns></returns>
        IList<InternationalFuelContractDto> GetActivesFuelContracts(string airlineCode, string stationCode, string operationTypeName);
    }
}
