//------------------------------------------------------------------------
// <copyright file="InternationalFuelContractRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// International Fuel Contract Repository
    /// </summary>
    public class InternationalFuelContractRepository : Repository<InternationalFuelContract>, IInternationalFuelContractRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalFuelContractRepository"/> class. 
        /// </summary>
        /// <param name="factory">factory factory</param>
        /// <returns></returns>
        public InternationalFuelContractRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region InternationalFuelContractRepository Members
        /// <summary>
        /// Return Fuel Contracts by id
        /// </summary>
        /// <param name="fuelContracts">Fuel Contracts</param>
        /// <returns>International Fuel Contract</returns>
        public InternationalFuelContract FindById(InternationalFuelContract fuelContracts)
        {
            var internationalFuelContract = this.DbContext.InternationalFuelContracts
            .Where(c => c.EffectiveDate == fuelContracts.EffectiveDate
                && c.AirlineCode == fuelContracts.AirlineCode
                && c.StationCode == fuelContracts.StationCode
                && c.ServiceCode == fuelContracts.ServiceCode
                && c.ProviderNumberPrimary == fuelContracts.ProviderNumberPrimary)
                    .Include(c => c.Airline)
                    .Include(c => c.Airport)
                    .Include(c => c.Service)
                    .Include(c => c.AccountingAccount)
                    .Include(c => c.CostCenter)
                    .Include(c => c.Currency)
                    .Include(c => c.LiabilityAccount)
                    .Include(c => c.Provider)
                    .Include(c => c.OperationType)
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.InternationalFuelRate))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConcept))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.Provider))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConceptType))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.ChargeFactorType))
            .FirstOrDefault();
            return internationalFuelContract;
        }

        /// <summary>
        /// Find By Contract KeyExclude EffectiveDate
        /// </summary>
        /// <param name="fuelContracts">Fuel Contracts</param>
        /// <returns>List International Fuel Contract</returns>
        public IList<InternationalFuelContract> FindByContractKeyExcludeEffectiveDate(InternationalFuelContract fuelContracts)
        {
            return this.DbContext.InternationalFuelContracts
                .Where(c => c.AirlineCode == fuelContracts.AirlineCode
                && c.StationCode == fuelContracts.StationCode
                && c.ServiceCode == fuelContracts.ServiceCode
                && c.ProviderNumberPrimary == fuelContracts.ProviderNumberPrimary
                && c.Status)
            .Include(c => c.Airline)
            .Include(c => c.Airport)
            .Include(c => c.Service)
            .Include(c => c.AccountingAccount)
            .Include(c => c.CostCenter)
            .Include(c => c.Currency)
            .Include(c => c.LiabilityAccount)
            .Include(c => c.Provider)
            .Include(c => c.OperationType)
            .Include(c => c.InternationalFuelContractConcepts.Select(p => p.InternationalFuelRate))
            .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConcept))
            .Include(c => c.InternationalFuelContractConcepts.Select(p => p.Provider))
            .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConceptType))
            .Include(c => c.InternationalFuelContractConcepts.Select(p => p.ChargeFactorType))
            .ToList();
        }

        /// <summary>
        /// Search InternationalFuelContracts
        /// </summary>
        /// <param name="fuelContracts">Fuel Contracts</param>
        /// <param name="statusSearch">status Search</param>
        /// <returns>List International Fuel Contract</returns>
        public IList<InternationalFuelContract> SearchInternationalFuelContracts(InternationalFuelContract fuelContracts, string statusSearch)
        {
            DateTime effectiveDate = new DateTime(0001, 01, 01);
            bool status = false;

            if (statusSearch == "A")
            {
                status = true;
            }

            if (statusSearch == "I")
            {
                status = false;
            }

            if (statusSearch == "A" || statusSearch == "I")
            {
                return this.DbContext.InternationalFuelContracts
                    .Where(c => (c.EffectiveDate <= fuelContracts.EffectiveDate || fuelContracts.EffectiveDate == effectiveDate)
                    && (c.AirlineCode == fuelContracts.AirlineCode || string.IsNullOrEmpty(fuelContracts.AirlineCode))
                    && (c.StationCode == fuelContracts.StationCode || string.IsNullOrEmpty(fuelContracts.StationCode))
                    && (c.ProviderNumberPrimary == fuelContracts.ProviderNumberPrimary || string.IsNullOrEmpty(fuelContracts.ProviderNumberPrimary))
                    && (c.Status == status))
                    .Include(c => c.Airline)
                    .Include(c => c.Airport)
                    .Include(c => c.Service)
                    .Include(c => c.AccountingAccount)
                    .Include(c => c.CostCenter)
                    .Include(c => c.Currency)
                    .Include(c => c.LiabilityAccount)
                    .Include(c => c.Provider)
                    .Include(c => c.OperationType)
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.InternationalFuelRate))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConcept))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.Provider))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConceptType))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.ChargeFactorType))
                    .ToList();
            }
            else
            {
                return this.DbContext.InternationalFuelContracts
                    .Where(c => (c.EffectiveDate <= fuelContracts.EffectiveDate || fuelContracts.EffectiveDate == effectiveDate)
                    && (c.AirlineCode == fuelContracts.AirlineCode || string.IsNullOrEmpty(fuelContracts.AirlineCode))
                    && (c.StationCode == fuelContracts.StationCode || string.IsNullOrEmpty(fuelContracts.StationCode))
                    && (c.ProviderNumberPrimary == fuelContracts.ProviderNumberPrimary || string.IsNullOrEmpty(fuelContracts.ProviderNumberPrimary)))
                    .Include(c => c.Airline)
                    .Include(c => c.Airport)
                    .Include(c => c.Service)
                    .Include(c => c.AccountingAccount)
                    .Include(c => c.CostCenter)
                    .Include(c => c.Currency)
                    .Include(c => c.LiabilityAccount)
                    .Include(c => c.Provider)
                    .Include(c => c.OperationType)
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.InternationalFuelRate))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConcept))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.Provider))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConceptType))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.ChargeFactorType))
                    .ToList();
            }
        }

        /// <summary>
        /// Get all Inactives Fuel Contracts
        /// </summary>
        /// <returns>List International Fuel Contract</returns>
        public IList<InternationalFuelContract> GetInactivesFuelContracts()
        {
            return this.DbContext.InternationalFuelContracts.Where(c => c.Status == false)
                    .Include(c => c.Airline)
                    .Include(c => c.Airport)
                    .Include(c => c.Service)
                    .Include(c => c.AccountingAccount)
                    .Include(c => c.CostCenter)
                    .Include(c => c.Currency)
                    .Include(c => c.LiabilityAccount)
                    .Include(c => c.Provider)
                    .Include(c => c.OperationType)
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.InternationalFuelRate))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConcept))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.Provider))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConceptType))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.ChargeFactorType))
                .ToList();
        }

        /// <summary>
        /// Get all Actives Fuel Contracts
        /// </summary>
        /// <returns>List International Fuel Contract</returns>
        public IList<InternationalFuelContract> GetActivesFuelContracts(string airlineCode, string stationCode)
        {
            return this.DbContext.InternationalFuelContracts.Where(c => c.Status
            && c.AirlineCode == airlineCode
            && c.StationCode == stationCode)
                    .Include(c => c.Airline)
                    .Include(c => c.Airport)
                    .Include(c => c.Service)
                    .Include(c => c.AccountingAccount)
                    .Include(c => c.CostCenter)
                    .Include(c => c.Currency)
                    .Include(c => c.LiabilityAccount)
                    .Include(c => c.Provider)
                    .Include(c => c.OperationType)
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.InternationalFuelRate))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConcept))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.Provider))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConceptType))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.ChargeFactorType))
            .ToList();
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="fuelContracts">Fuel Contracts</param>
        public void DeleteEntity(InternationalFuelContract fuelContracts)
        {
            InternationalFuelContract objtblFee = this.DbContext.InternationalFuelContracts.First(c => c.EffectiveDate == fuelContracts.EffectiveDate
            && c.AirlineCode == fuelContracts.AirlineCode
            && c.StationCode == fuelContracts.StationCode
            && c.ServiceCode == fuelContracts.ServiceCode
            && c.ProviderNumberPrimary == fuelContracts.ProviderNumberPrimary);
            this.Delete(objtblFee);
        }

        /// <summary>
        /// Get All Contracts ByMaxDate
        /// </summary>
        /// <param name="fuelContracts">Fuel Contracts</param>
        /// <returns>Date Time</returns>
        public DateTime GetAllContractsByMaxDate(InternationalFuelContract fuelContracts)
        {
            DateTime maxDate = new DateTime();
            IList<InternationalFuelContract> contracts = this.DbContext.InternationalFuelContracts
               .Where(c =>
                   c.AirlineCode == fuelContracts.AirlineCode
                   && c.StationCode == fuelContracts.StationCode
                   && c.ServiceCode == fuelContracts.ServiceCode
                   && c.ProviderNumberPrimary == fuelContracts.ProviderNumberPrimary)
                    .Include(c => c.Airline)
                    .Include(c => c.Airport)
                    .Include(c => c.Service)
                    .Include(c => c.AccountingAccount)
                    .Include(c => c.CostCenter)
                    .Include(c => c.Currency)
                    .Include(c => c.LiabilityAccount)
                    .Include(c => c.Provider)
                    .Include(c => c.OperationType)
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.InternationalFuelRate))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConcept))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.Provider))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.FuelConceptType))
                    .Include(c => c.InternationalFuelContractConcepts.Select(p => p.ChargeFactorType))
                   .ToList();

            if (contracts.Count > 0)
            {
                maxDate = contracts.Max(c => c.EffectiveDate.Date);
            }

            return maxDate;
        }

        /// <summary>
        /// Adds the given list of fuel contracts Add Range.
        /// </summary>
        /// <param name="contracts">List of airport fuel contracts.</param>
        public void AddRange(IList<InternationalFuelContract> contracts)
        {
            this.DbContext.InternationalFuelContracts.AddRange(contracts);
        }
        #endregion
    }
}
