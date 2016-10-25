//------------------------------------------------------------------------
// <copyright file="AirportServiceContractRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Airport Service Contract Repository
    /// </summary>
    public class AirportServiceContractRepository : Repository<AirportServiceContract>, IAirportServiceContractRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirportServiceContractRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AirportServiceContractRepository(IDbFactory factory)
            : base(factory)
        {
        }

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
        public AirportServiceContract FindById(
            System.DateTime effectiveDate,
            string airlineCode,
            string stationCode,
            string serviceCode,
            string providerNumber,
            string costCenterNumber)
        {
            IList<AirportServiceContract> contracts = this.DbContext.AirportServiceContracts
                .Where(c =>
                    c.AirlineCode == airlineCode
                    && c.StationCode == stationCode
                    && c.ServiceCode == serviceCode
                    && c.ProviderNumber == providerNumber
                    && c.CostCenterNumber == costCenterNumber)
                .Include(p => p.AccountingAccount)
                .Include(p => p.Airline)
                .Include(p => p.AirplaneWeightType)
                .Include(p => p.AirplaneWeightMeasureType)
                .Include(p => p.Airport)
                .Include(p => p.CostCenter)
                .Include(p => p.Currency)
                .Include(p => p.LiabilityAccount)
                .Include(p => p.OperationType)
                .Include(p => p.Provider)
                .Include(p => p.Service)
                .Include(p => p.ServiceCalculationType)
                .Include(p => p.ServiceType)
                .Include(p => p.AirportTax)
                .Include(p => p.FederalTax)
                .Include(p => p.LocalTax)
                .Include(p => p.StateTax)
                .ToList();

            AirportServiceContract effectiveContract = contracts
                .FirstOrDefault(c =>
                        c.EffectiveDate.Date.Equals(effectiveDate.Date));

            return effectiveContract;
        }

        /// <summary>
        /// Gets a list of the actives contracts.
        /// </summary>
        /// <returns>
        /// List of actives contracts.
        /// </returns>
        public IList<AirportServiceContract> GetActivesContracts()
        {
            // TODO: it is not ready yet.
            return this.DbContext.AirportServiceContracts
                .Where(c => c.Status)
                .Include(a => a.Airline)
                .Include(a => a.Airport)
                .Include(a => a.Service)
                .Include(a => a.Provider)
                .Include(a => a.CostCenter)
                .Include(a => a.Currency)
                .Include(a => a.AccountingAccount)
                .Include(a => a.LiabilityAccount)
                .Include(a => a.AirplaneWeightMeasureType)
                .Include(a => a.AirplaneWeightType)
                .Include(a => a.OperationType)
                .Include(a => a.ServiceType)
                .Include(a => a.LocalTax)
                .Include(a => a.FederalTax)
                .Include(a => a.StateTax)
                .Include(a => a.AirportTax)
                .Include(a => a.ServiceCalculationType).ToList();
        }

        /// <summary>
        /// Gets the contracts for airport and airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <returns>
        /// List of contracts for airline and airport.
        /// </returns>
        public IList<AirportServiceContract> GetContractsForAirportAirline(string airlineCode, string airportCode)
        {
            IList<AirportServiceContract> contracts = this.DbContext.AirportServiceContracts.Where(c =>
                (c.AirlineCode == airportCode) &&
                (c.StationCode == airportCode))
                .Include(a => a.Airline)
                .Include(a => a.Airport)
                .Include(a => a.Service)
                .Include(a => a.Provider)
                .Include(a => a.CostCenter)
                .Include(a => a.Currency)
                .Include(a => a.AccountingAccount)
                .Include(a => a.LiabilityAccount)
                .Include(a => a.AirplaneWeightMeasureType)
                .Include(a => a.AirplaneWeightType)
                .Include(a => a.OperationType)
                .Include(a => a.ServiceType)
                .Include(a => a.LocalTax)
                .Include(a => a.FederalTax)
                .Include(a => a.StateTax)
                .Include(a => a.AirportTax)
                .Include(a => a.ServiceCalculationType)
                .ToList();

            return contracts;
        }

        /// <summary>
        /// Gets the contracts by the parameters past in the object.
        /// </summary>
        /// <param name="parameters">The parameters to match in the query.</param>
        /// <returns>
        /// List of contracts that match with the specified parameters.
        /// </returns>
        public IList<AirportServiceContract> GetContractsByParameters(AirportServiceContract parameters)
        {
            IQueryable<AirportServiceContract> query = this.DbContext.AirportServiceContracts;
            if (!string.IsNullOrEmpty(parameters.AirlineCode))
            {
                query = query.Where(c => c.AirlineCode.Contains(parameters.AirlineCode) || c.Airline.AirlineName.Contains(parameters.AirlineCode));
            }

            if (!string.IsNullOrEmpty(parameters.StationCode))
            {
                query = query.Where(c => c.StationCode.Contains(parameters.StationCode) || c.Airport.StationName.Contains(parameters.StationCode));
            }

            if (!string.IsNullOrEmpty(parameters.ProviderNumber))
            {
                query = query.Where(c => c.ProviderNumber.Contains(parameters.ProviderNumber) || c.Provider.ProviderName.Contains(parameters.ProviderNumber));
            }

            if (!string.IsNullOrEmpty(parameters.ServiceCode))
            {
                query = query.Where(c => c.ServiceCode.Contains(parameters.ServiceCode) || c.Service.ServiceName.Contains(parameters.ServiceCode));
            }

            if (!string.IsNullOrEmpty(parameters.CostCenterNumber))
            {
                query = query.Where(c => c.CostCenterNumber.Contains(parameters.CostCenterNumber) || c.CostCenter.CCName.Contains(parameters.CostCenterNumber));
            }

            if (parameters.EffectiveDate != null && parameters.EffectiveDate != default(DateTime))
            {
                query = query.Where(c => c.EffectiveDate <= parameters.EffectiveDate);
            }

            var result = query
                        .OrderBy(c => c.AirlineCode)
                        .ThenBy(c => c.StationCode)
                        .ThenBy(c => c.ServiceCode)
                        .ThenBy(c => c.ProviderNumber)
                        .ThenBy(c => c.CostCenterNumber)
                        .ThenBy(c => c.EffectiveDate)
                        .Include(a => a.Airline)
                        .Include(a => a.Airport)
                        .Include(a => a.Service)
                        .Include(a => a.Provider)
                        .Include(a => a.CostCenter)
                        .Include(a => a.Currency)
                        .Include(a => a.AccountingAccount)
                        .Include(a => a.LiabilityAccount)
                        .Include(a => a.AirplaneWeightMeasureType)
                        .Include(a => a.AirplaneWeightType)
                        .Include(a => a.OperationType)
                        .Include(a => a.ServiceType)
                        .Include(a => a.LocalTax)
                        .Include(a => a.FederalTax)
                        .Include(a => a.StateTax)
                        .Include(a => a.AirportTax)
                        .Include(a => a.ServiceCalculationType)
                        .ToList();
            return result;
        }

        /// <summary>
        /// Gets all contracts.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="stationCode">The station code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <param name="providerNumber">The provider number.</param>
        /// <param name="costCenterNumber">The cost center number.</param>
        /// <returns>
        /// List of contracts that match with the primary Key except Effective Date.
        /// </returns>
        public DateTime GetAllContractsByMaxDate(string airlineCode, string stationCode, string serviceCode, string providerNumber, string costCenterNumber)
        {
            DateTime maxDate = new DateTime();
            IList<AirportServiceContract> contracts = this.DbContext.AirportServiceContracts
               .Where(c =>
                   c.AirlineCode == airlineCode
                   && c.StationCode == stationCode
                   && c.ServiceCode == serviceCode
                   && c.ProviderNumber == providerNumber
                   && c.CostCenterNumber == costCenterNumber)
                   .Include(a => a.Airline)
                .Include(a => a.Airport)
                .Include(a => a.Service)
                .Include(a => a.Provider)
                .Include(a => a.CostCenter)
                .Include(a => a.Currency)
                .Include(a => a.AccountingAccount)
                .Include(a => a.LiabilityAccount)
                .Include(a => a.AirplaneWeightMeasureType)
                .Include(a => a.AirplaneWeightType)
                .Include(a => a.OperationType)
                .Include(a => a.ServiceType)
                .Include(a => a.LocalTax)
                .Include(a => a.FederalTax)
                .Include(a => a.StateTax)
                .Include(a => a.AirportTax)
                .Include(a => a.ServiceCalculationType).ToList();

            if (contracts.Count > 0)
            {
                maxDate = contracts.Max(c => c.EffectiveDate.Date);
            }

            return maxDate;
        }

        /// <summary>
        /// Adds the given list of airport service contracs.
        /// </summary>
        /// <param name="contracts">List of airport service contracts.</param>
        public void AddRange(IList<AirportServiceContract> contracts)
        {
            this.DbContext.AirportServiceContracts.AddRange(contracts);
        }


        /// <summary>
        /// Gets all entity's records.
        /// </summary>
        /// <returns>
        /// All the entity's records.
        /// </returns>
        public override IList<AirportServiceContract> GetAll()
        {
            return this.DbContext.AirportServiceContracts
                .Include(a => a.Airline)
                .Include(a => a.Airport)
                .Include(a => a.Service)
                .Include(a => a.Provider)
                .Include(a => a.CostCenter)
                .Include(a => a.Currency)
                .Include(a => a.AccountingAccount)
                .Include(a => a.LiabilityAccount)
                .Include(a => a.AirplaneWeightMeasureType)
                .Include(a => a.AirplaneWeightType)
                .Include(a => a.OperationType)
                .Include(a => a.ServiceType)
                .Include(a => a.LocalTax)
                .Include(a => a.FederalTax)
                .Include(a => a.StateTax)
                .Include(a => a.AirportTax)
                .Include(a => a.ServiceCalculationType).ToList();
        }
    }
}
