// <copyright file="AirportServiceContractFile.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Files
{
    using System;
    using FileHelpers;
    using VOI.SISAC.Web.Helpers;

    /// <summary>
    /// 
    /// </summary>
    [DelimitedRecord("\t")]
    public class AirportServiceContractFile
    {
        /// <summary>
        /// The effective date
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime EffectiveDate;

        /// <summary>
        /// The airline code
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string AirlineCode;

        /// <summary>
        /// The station code
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string StationCode;

        /// <summary>
        /// The service code
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ServiceCode;

        /// <summary>
        /// Gets or sets the provider number.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ProviderNumber;

        /// <summary>
        /// Gets or sets the cost center number.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CostCenterNumber;

        /// <summary>
        /// Gets or sets the accounting account number.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The accounting account number.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string AccountingAccountNumber;

        /// <summary>
        /// Gets or sets the liability account number.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The liability account number.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string LiabilityAccountNumber;

        /// <summary>
        /// Gets or sets the type of the operation.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        [FieldNotEmpty]
        ////[FieldConverter(typeof(UpperStringHelper))]
        public string OperationName;

        /// <summary>
        /// Gets or sets the type of the service.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        [FieldNotEmpty]
        ////[FieldConverter(typeof(UpperStringHelper))]
        public string ServiceTypeName;

        /// <summary>
        /// Gets or sets the airport fee code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airport fee code.
        /// </value>
        [FieldConverter(typeof(UpperStringHelper))]
        public string AirportFeeCode;

        /// <summary>
        /// Gets or sets the airport fee value.
        /// </summary>
        /// <value>
        /// The airport fee value.
        /// </value>   
        [FieldConverter(ConverterKind.Decimal)]
        public decimal? AirportFeeValue;

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        [FieldConverter(ConverterKind.Boolean)]
        public bool AirportFeeFlag;

        /// <summary>
        /// Gets or sets the local tax code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The local tax code.
        /// </value>
        [FieldConverter(typeof(UpperStringHelper))]
        public string LocalTaxCode;

        /// <summary>
        /// Gets or sets the local tax value.
        /// </summary>
        /// <value>
        /// The local tax value.
        /// </value>
        [FieldConverter(ConverterKind.Decimal)]
        public decimal? LocalTaxValue;

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>      
        [FieldConverter(ConverterKind.Boolean)]
        public bool LocalTaxFlag;

        /// <summary>
        /// Gets or sets the state tax code.
        /// </summary>
        /// <value>
        /// The state tax code.
        /// Foreign key.
        /// </value>
        [FieldConverter(typeof(UpperStringHelper))]
        public string StateTaxCode;

        /// <summary>
        /// Gets or sets the state tax value.
        /// </summary>
        /// <value>
        /// The state tax value.
        /// </value>
        [FieldConverter(ConverterKind.Decimal)]
        public decimal? StateTaxValue;

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        [FieldConverter(ConverterKind.Boolean)]
        public bool StateTaxFlag;

        /// <summary>
        /// Gets or sets the federal tax code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The federal tax code.
        /// </value>      
        [FieldConverter(typeof(UpperStringHelper))]
        public string FederalTaxCode;

        /// <summary>
        /// Gets or sets the federal tax value.
        /// </summary>
        /// <value>
        /// The federal tax value.
        /// </value>
        [FieldConverter(ConverterKind.Decimal)]
        public decimal? FederalTaxValue;

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        [FieldConverter(ConverterKind.Boolean)]
        public bool FederalTaxFlag;

        /// <summary>
        /// Gets or sets a value indicating whether the value depends on the airplane's weight.
        /// </summary>
        /// <value>
        ///   <c>true</c> if depends on the airplane's weight otherwise, <c>false</c>.
        /// </value>
        [FieldConverter(ConverterKind.Boolean)]
        public bool AirplanetWeightFlag;

        /// <summary>
        /// Gets or sets the airplane weight description.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airplane weight description.
        /// </value>
        ////[FieldConverter(typeof(UpperStringHelper))]
        public string AirplaneWeightName;

        /// <summary>
        /// Gets or sets the airplane weight unit of measure.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airplane weight unit.
        /// </value>
        ////[FieldConverter(typeof(UpperStringHelper))]
        public string AirplaneWeightMeasureName;

        /// <summary>
        /// Gets or sets the airplane weight multiplier.
        /// </summary>
        /// <value>
        /// The airplane weight multiplier.
        /// </value>
        [FieldConverter(ConverterKind.Int64)]
        public long? AirplaneWeightMultiplier;

        /// <summary>
        /// Gets or sets a value indicating whether the fee involves a 1:M relationship with the contract.
        /// </summary>
        /// <value>
        ///   <c>true</c> if involves a relationship otherwise, <c>false</c>.
        /// </value>       
        [FieldConverter(ConverterKind.Boolean)]
        public bool MultiRateFlag;

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        //[FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public decimal? Rate;

        /// <summary>
        /// Gets or sets the currency code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        //[FieldNotEmpty]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CurrencyCode;

        /// <summary>
        /// Gets or sets a value indicating whether requires to capture the service to estimate it.
        /// </summary>
        /// <value>
        ///   <c>true</c> if requires the service otherwise, <c>false</c>.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Boolean)]
        public bool ServiceRecordFlag;

        /// <summary>
        /// Gets or sets the calculation type identifier.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The calculation type identifier.
        /// </value>
        [FieldNotEmpty]
        ////[FieldConverter(typeof(UpperStringHelper))]
        public string CalculationTypeName;

        //[FieldNotEmpty]
        public string ProcedureName;
    }
}