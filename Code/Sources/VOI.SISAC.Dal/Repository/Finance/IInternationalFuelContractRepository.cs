//------------------------------------------------------------------------
// <copyright file="IInternationalFuelContractRepository.cs" company="Volaris">
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
    /// International Fuel Contract Repository Interface
    /// </summary>
    public interface IInternationalFuelContractRepository : IRepository<InternationalFuelContract>
    {
        /// <summary>
        /// Find By Id
        /// </summary>
        /// <param name="fuelContracts">fuel Contracts</param>
        /// <returns>International Fuel Contract</returns>
        InternationalFuelContract FindById(InternationalFuelContract fuelContracts);

        /// <summary>
        /// Find By ContractKeyExcludeEffectiveDate
        /// </summary>
        /// <param name="fuelContracts">fuel Contracts</param>
        /// <returns>List InternationalFuelContract</returns>
        IList<InternationalFuelContract> FindByContractKeyExcludeEffectiveDate(InternationalFuelContract fuelContracts);

        /// <summary>
        /// Search International Fuel Contracts
        /// </summary>
        /// <param name="fuelContracts">fuel Contracts</param>
        /// <param name="statusSearch">status Search</param>
        /// <returns>IList InternationalFuelContract</returns>
        IList<InternationalFuelContract> SearchInternationalFuelContracts(InternationalFuelContract fuelContracts, string statusSearch);

        /// <summary>
        /// Get all fuel contract
        /// </summary>
        /// <returns>list of InternationalFuelContract</returns>
        IList<InternationalFuelContract> GetInactivesFuelContracts();

        /// <summary>
        /// Get all Actives Fuel Contracts
        /// </summary>
        /// <returns>List International Fuel Contract</returns>
        IList<InternationalFuelContract> GetActivesFuelContracts(string airlineCode, string stationCode);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="fuelContracts">fuel Contracts</param>
        void DeleteEntity(InternationalFuelContract fuelContracts);

        /// <summary>
        /// Get All Contracts ByMaxDate
        /// </summary>
        /// <param name="fuelContracts">fuel Contracts</param>
        /// <returns>A DateTime</returns>
        DateTime GetAllContractsByMaxDate(InternationalFuelContract fuelContracts);

        /// <summary>
        /// Add Range
        /// </summary>
        /// <param name="fuelContracts">fuel Contracts</param>
        void AddRange(IList<InternationalFuelContract> fuelContracts);
    }
}
