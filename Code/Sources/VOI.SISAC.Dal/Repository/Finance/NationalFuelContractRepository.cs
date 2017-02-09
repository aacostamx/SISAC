//------------------------------------------------------------------------
// <copyright file="NationalFuelContractRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    /// National Fuel Contract Repository
    /// </summary>
    public class NationalFuelContractRepository : Repository<NationalFuelContract>, INationalFuelContractRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelContractRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalFuelContractRepository(IDbFactory factory)
            : base(factory)
        {
        }

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
        public NationalFuelContract FindByContract(
            DateTime effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber)
        {
            NationalFuelContract contract = this.DbContext.NationalFuelContracts
            .Where(c => c.EffectiveDate == effectiveDate
                && c.AirlineCode == airlineCode
                && c.StationCode == stationCode
                && c.ServiceCode == serviceCode
                && c.ProviderNumberPrimary == providerNumber)
                    .Include(c => c.Airline)
                    .Include(c => c.Airport)
                    .Include(c => c.Service)
                    .Include(c => c.AccountingAccount)
                    .Include(c => c.CostCenter)
                    .Include(c => c.Currency)
                    .Include(c => c.LiabilityAccount)
                    .Include(c => c.Provider)
                    .Include(c => c.OperationType)
                    .Include(c => c.NationalFuelContractConcept)
                    .Include(c => c.NationalFuelContractConcept.Select(p => p.FuelConcept))
                    .Include(c => c.NationalFuelContractConcept.Select(p => p.Provider))
                    .Include(c => c.NationalFuelContractConcept.Select(p => p.FuelConceptType))
                    .Include(c => c.NationalFuelContractConcept.Select(p => p.ChargeFactorType))
            .FirstOrDefault();
            return contract;
        }

        /// <summary>
        /// Gets the effectives contracts pagination.
        /// </summary>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>
        /// List of national fuel contracts paginated.
        /// </returns>
        public IList<NationalFuelContract> GetEffectivesContractsPagination(int pageSize, int pageNumber)
        {
            List<NationalFuelContract> contracts = this.DbContext.NationalFuelContracts
                .OrderBy(c => c.EffectiveDate)
                .ThenBy(c => c.AirlineCode)
                .ThenBy(c => c.StationCode)
                .ThenBy(c => c.ServiceCode)
                .ThenBy(c => c.ProviderNumberPrimary)
                .Skip(pageSize)
                .Take(pageNumber)
                .ToList();

            return contracts;
        }

        /// <summary>
        /// Gets the effectives contracts pagination.
        /// </summary>
        /// <returns>
        ///     List of national fuel contracts paginated.
        /// </returns>
        public IList<NationalFuelContract> GetAllWithRealationships()
        {
            List<NationalFuelContract> contracts = this.DbContext.NationalFuelContracts
                .Include(c => c.AccountingAccount)
                .Include(c => c.Airline)
                .Include(c => c.Airport)
                .Include(c => c.CostCenter)
                .Include(c => c.Currency)
                .Include(c => c.FederalTax)
                .Include(c => c.LiabilityAccount)
                .Include(c => c.OperationType)
                .Include(c => c.Provider)
                .Include(c => c.Service)
                .ToList();

            return contracts;
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="fuelContracts">The fuel contracts.</param>
        public void AddRange(IList<NationalFuelContract> fuelContracts)
        {
            this.DbContext.NationalFuelContracts.AddRange(fuelContracts);
        }

        /// <summary>
        /// Counts all registers for national fuel contracts.
        /// </summary>
        /// <returns>
        /// Total of national contracts.
        /// </returns>
        public int CountAll()
        {
            return this.DbContext.NationalFuelContracts.Count();
        }

        /// <summary>
        /// Counts the contracs by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>
        /// Number of contracts that match with the parameters.
        /// </returns>
        public int CountContracsByParameters(NationalFuelContract parameters)
        {
            IQueryable<NationalFuelContract> query = this.DbContext.NationalFuelContracts;
            if (!string.IsNullOrEmpty(parameters.AirlineCode))
            {
                query = query.Where(c => c.AirlineCode.Contains(parameters.AirlineCode));
            }

            if (!string.IsNullOrEmpty(parameters.StationCode))
            {
                query = query.Where(c => c.StationCode.Contains(parameters.StationCode));
            }

            if (!string.IsNullOrEmpty(parameters.ProviderNumberPrimary))
            {
                query = query.Where(c => c.ProviderNumberPrimary.Contains(parameters.ProviderNumberPrimary));
            }

            if (!string.IsNullOrEmpty(parameters.ServiceCode))
            {
                query = query.Where(c => c.ServiceCode.Contains(parameters.ServiceCode));
            }

            if (parameters.EffectiveDate != null && parameters.EffectiveDate != default(DateTime))
            {
                query = query.Where(c => c.EffectiveDate <= parameters.EffectiveDate);
            }

            var result = query.ToList().Count;
            return result;
        }

        /// <summary>
        /// Gets the contracs by parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="take">The take.</param>
        /// <param name="skip">The skip.</param>
        /// <returns>
        /// Contracts that match with the parameters.
        /// </returns>
        public IList<NationalFuelContract> GetContracsByParameters(NationalFuelContract parameters, int take, int skip)
        {
            IQueryable<NationalFuelContract> query = this.DbContext.NationalFuelContracts;
            if (!string.IsNullOrEmpty(parameters.AirlineCode))
            {
                query = query.Where(c => c.AirlineCode.Contains(parameters.AirlineCode));
            }

            if (!string.IsNullOrEmpty(parameters.StationCode))
            {
                query = query.Where(c => c.StationCode.Contains(parameters.StationCode));
            }

            if (!string.IsNullOrEmpty(parameters.ProviderNumberPrimary))
            {
                query = query.Where(c => c.ProviderNumberPrimary.Contains(parameters.ProviderNumberPrimary));
            }

            if (!string.IsNullOrEmpty(parameters.ServiceCode))
            {
                query = query.Where(c => c.ServiceCode.Contains(parameters.ServiceCode));
            }

            if (parameters.EffectiveDate != null && parameters.EffectiveDate != default(DateTime))
            {
                query = query.Where(c => c.EffectiveDate <= parameters.EffectiveDate);
            }

            var result = query
                .OrderBy(c => c.EffectiveDate)
                .ThenBy(c => c.AirlineCode)
                .ThenBy(c => c.StationCode)
                .ThenBy(c => c.ServiceCode)
                .ThenBy(c => c.ProviderNumberPrimary)
                .Include(c => c.Airline)
                .Include(c => c.Airport)
                .Include(c => c.Service)
                .Include(c => c.Provider)
                .Include(c => c.AccountingAccount)
                .Include(c => c.LiabilityAccount)
                .Include(c => c.CostCenter)
                .Include(c => c.Currency)
                .Include(c => c.OperationType)
                .Include(c => c.FederalTax)
                .Skip(skip)
                .Take(take);
            return result.ToList();
        }

        /// <summary>
        /// Gets the contracts by airline airport.
        /// </summary>
        /// <param name="contract">The contract.</param>
        /// <returns></returns>
        public IList<NationalFuelContract> GetContractsByAirlineAirport(NationalFuelContract contract)
        {
            var contracts = new List<NationalFuelContract>();

            contracts = this.DbContext.NationalFuelContracts
                .Where(c => c.Status &&
                c.AirlineCode == contract.AirlineCode &&
                c.StationCode == contract.StationCode)
                .Include(c => c.NationalFuelContractConcept)
                .ToList();

            return contracts;
        }

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
        public DateTime GetContractMaxDate(string airlineCode, string stationCode, string serviceCode, string providerNumber)
        {
            DateTime maxDate = new DateTime();
            IList<NationalFuelContract> contracts = this.DbContext.NationalFuelContracts
               .Where(c =>
                   c.AirlineCode == airlineCode
                   && c.StationCode == stationCode
                   && c.ServiceCode == serviceCode
                   && c.ProviderNumberPrimary == providerNumber)
                .ToList();

            if (contracts.Count > 0)
            {
                maxDate = contracts.Max(c => c.EffectiveDate.Date);
            }

            return maxDate;
        }
    }
}
