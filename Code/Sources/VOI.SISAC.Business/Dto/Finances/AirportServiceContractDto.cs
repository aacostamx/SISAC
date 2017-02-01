//------------------------------------------------------------------------
// <copyright file="AirportServiceContractDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Finances
{
    using System;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// Contract entity
    /// </summary>
    public class AirportServiceContractDto
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
        /// Gets or sets the provider number.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the cost center number.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
        public string CostCenterNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AirportServiceContract"/> is active.
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
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The liability account number.
        /// </value>
        public string LiabilityAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public int OperationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the service.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public string ServiceTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the airport fee code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airport fee code.
        /// </value>
        public string AirportFeeCode { get; set; }

        /// <summary>
        /// Gets or sets the airport fee value.
        /// </summary>
        /// <value>
        /// The airport fee value.
        /// </value>
        public decimal? AirportFeeValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        public bool AirportFeeFlag { get; set; }

        /// <summary>
        /// Gets or sets the local tax code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The local tax code.
        /// </value>
        public string LocalTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the local tax value.
        /// </summary>
        /// <value>
        /// The local tax value.
        /// </value>
        public decimal? LocalTaxValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        public bool LocalTaxFlag { get; set; }

        /// <summary>
        /// Gets or sets the state tax code.
        /// </summary>
        /// <value>
        /// The state tax code.
        /// Foreign key.
        /// </value>
        public string StateTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the state tax value.
        /// </summary>
        /// <value>
        /// The state tax value.
        /// </value>
        public decimal? StateTaxValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        public bool StateTaxFlag { get; set; }

        /// <summary>
        /// Gets or sets the federal tax code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The federal tax code.
        /// </value>
        public string FederalTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the federal tax value.
        /// </summary>
        /// <value>
        /// The federal tax value.
        /// </value>
        public decimal? FederalTaxValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        public bool FederalTaxFlag { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value depends on the airplane's weight.
        /// </summary>
        /// <value>
        ///   <c>true</c> if depends on the airplane's weight otherwise, <c>false</c>.
        /// </value>
        public bool AirplanetWeightFlag { get; set; }

        /// <summary>
        /// Gets or sets the airplane weight description.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airplane weight description.
        /// </value>
        public string AirplaneWeightCode { get; set; }

        /// <summary>
        /// Gets or sets the airplane weight unit of measure.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airplane weight unit.
        /// </value>
        public int? AirplaneWeightUnit { get; set; }

        /// <summary>
        /// Gets or sets the airplane weight multiplier.
        /// </summary>
        /// <value>
        /// The airplane weight multiplier.
        /// </value>
        public long? AirplaneWeightMultiplier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the fee involves a 1:M relationship with the contract.
        /// </summary>
        /// <value>
        ///   <c>true</c> if involves a relationship otherwise, <c>false</c>.
        /// </value>
        public bool MultiRateFlag { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        public decimal? Rate { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether requires to capture the service to estimate it.
        /// </summary>
        /// <value>
        ///   <c>true</c> if requires the service otherwise, <c>false</c>.
        /// </value>
        public bool ServiceRecordFlag { get; set; }

        /// <summary>
        /// Gets or sets the calculation type identifier.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The calculation type identifier.
        /// </value>
        public int CalculationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the end date contract.
        /// </summary>
        /// <value>
        /// The end date contract.
        /// </value>
        public DateTime? EndDateContract { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public CurrencyDto Currency { get; set; }

        /// <summary>
        /// Gets or sets the airline.
        /// </summary>
        /// <value>
        /// The airline.
        /// </value>
        public AirlineDto Airline { get; set; }

        /// <summary>
        /// Gets or sets the airport.
        /// </summary>
        /// <value>
        /// The airport.
        /// </value>
        public AirportDto Airport { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public ServiceDto Service { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public ProviderDto Provider { get; set; }

        /// <summary>
        /// Gets or sets the cost center.
        /// </summary>
        /// <value>
        /// The cost center.
        /// </value>
        public CostCenterDto CostCenter { get; set; }

        /// <summary>
        /// Gets or sets the accounting account.
        /// </summary>
        /// <value>
        /// The accounting account.
        /// </value>
        public AccountingAccountDto AccountingAccount { get; set; }

        /// <summary>
        /// Gets or sets the liability account.
        /// </summary>
        /// <value>
        /// The liability account.
        /// </value>
        public LiabilityAccountDto LiabilityAccount { get; set; }

        /// <summary>
        /// Gets or sets the airport tax.
        /// </summary>
        /// <value>
        /// The airport tax.
        /// </value>
        public TaxDto AirportTax { get; set; }

        /// <summary>
        /// Gets or sets the local tax.
        /// </summary>
        /// <value>
        /// The local tax.
        /// </value>
        public TaxDto LocalTax { get; set; }

        /// <summary>
        /// Gets or sets the state tax.
        /// </summary>
        /// <value>
        /// The state tax.
        /// </value>
        public TaxDto StateTax { get; set; }

        /// <summary>
        /// Gets or sets the federal tax.
        /// </summary>
        /// <value>
        /// The federal tax.
        /// </value>
        public TaxDto FederalTax { get; set; }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public ServiceTypeDto ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the type of the service calculation.
        /// </summary>
        /// <value>
        /// The type of the service calculation.
        /// </value>
        public ServiceCalculationTypeDto ServiceCalculationType { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public OperationTypeDto OperationType { get; set; }

        /// <summary>
        /// Gets or sets the type of the airplane weight.
        /// </summary>
        /// <value>
        /// The type of the airplane weight.
        /// </value>
        public AirplaneWeightTypeDto AirplaneWeightType { get; set; }

        /// <summary>
        /// Gets or sets the type of the airplane weight measure.
        /// </summary>
        /// <value>
        /// The type of the airplane weight measure.
        /// </value>
        public AirplaneWeightMeasureTypeDto AirplaneWeightMeasureType { get; set; }
    }
}
