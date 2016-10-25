//------------------------------------------------------------------------
// <copyright file="InternationalFuelContractDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Finances
{
    using System;
    using System.Collections.Generic;
    using Airports;
    using Catalogs;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Data Object of International Fuel Contract
    /// </summary>
    public class InternationalFuelContractDto
    {
        /// <summary>
        /// Gets or sets the effective date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The effective date.
        /// </value>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the ProviderNumberPrimary
        /// Primary key.
        /// </summary>
        /// <value>
        /// the Provider Number Primary.
        /// </value>
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InternationalFuelContract"/> is active.
        /// </summary>
        /// <value>
        /// The  Aircraft Regist CCFlag.
        /// </value>
        public bool AircraftRegistCCFlag { get; set; }

        /// <summary>
        /// Gets or sets the cost center number.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
        public string CCNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InternationalFuelContract"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the accounting account number.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The accounting account number.
        /// </value>
        public string AccountingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the liability account number.
        /// </summary>
        /// <value>
        /// The liability account number.
        /// </value>
        public string LiabilityAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public int OperationTypeID { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the End Date Contract.
        /// </summary>
        /// <value>
        /// The EndDate Contract.
        /// </value>
        public DateTime? EndDateContract { get; set; }

        /// <summary>
        /// Gets or sets the Airline
        /// </summary>
        public AirlineDto Airline { get; set; }

        /// <summary>
        /// Gets or sets the Airport
        /// </summary>
        public AirportDto Airport { get; set; }

        /// <summary>
        /// Gets or sets the Service
        /// </summary>
        public ServiceDto Service { get; set; }

        /// <summary>
        /// Gets or sets the OperationType
        /// </summary>
        public OperationTypeDto OperationType { get; set; }

        /// <summary>
        /// Gets or sets the AccountingAccount
        /// </summary>
        public AccountingAccountDto AccountingAccount { get; set; }

        /// <summary>
        /// Gets or sets the CostCenter
        /// </summary>
        public CostCenterDto CostCenter { get; set; }

        /// <summary>
        /// Gets or sets the Currency
        /// </summary>
        public CurrencyDto Currency { get; set; }

        /// <summary>
        /// Gets or sets the LiabilityAccount
        /// </summary>
        public LiabilityAccountDto LiabilityAccount { get; set; }

        /// <summary>
        /// Gets or sets the Provider
        /// </summary>
        public ProviderDto Provider { get; set; }

        /// <summary>
        /// Gets or sets the Reference to table InternationalFuelContractConcept
        /// </summary>
        public ICollection<InternationalFuelContractConceptDto> InternationalFuelContractConcepts { get; set; }
    }
}
