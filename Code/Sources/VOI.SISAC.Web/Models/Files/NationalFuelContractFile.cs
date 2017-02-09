//------------------------------------------------------------------------
// <copyright file="NationalFuelContractFile.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Files
{
    using System;
    using FileHelpers;
    using Helpers;

    /// <summary>
    /// National Fuel Contract File
    /// </summary>
    [DelimitedRecord("\t")]
    public class NationalFuelContractFile
    {
        /// <summary>
        /// Effective Date
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime EffectiveDate;

        /// <summary>
        /// Airline Code
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string AirlineCode;

        /// <summary>
        /// Station Code
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string StationCode;

        /// <summary>
        /// Service Code
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ServiceCode;

        /// <summary>
        /// Provider Number Primary
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ProviderNumberPrimary;

        /// <summary>
        /// Cost Center Number
        /// </summary>
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CCNumber;

        /// <summary>
        /// Aircraft Registration Flag
        /// </summary>
        [FieldConverter(ConverterKind.Boolean)]
        public bool AircraftRegistCCFlag;

        /// <summary>
        /// Accounting Account Number
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        public string AccountingAccountNumber;

        /// <summary>
        /// Liability Account Number
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string LiabilityAccountNumber;

        /// <summary>
        /// Operation Name
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string OperationTypeName;

        /// <summary>
        /// Currency Code
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CurrencyCode;

        /// <summary>
        /// Gets or sets the federal tax code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The federal tax code.
        /// </value>      
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FederalTaxCode;

        /// <summary>
        /// Gets or sets the federal tax value.
        /// </summary>
        /// <value>
        /// The federal tax value.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public decimal? FederalTaxValue;

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Boolean)]
        public bool FederalTaxFlag;

        /// <summary>
        /// Fuel Concept Name
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FuelConceptName;

        /// <summary>
        /// Fuel Concept Type Name
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FuelConceptTypeName;

        /// <summary>
        /// Charge Factor Type Name
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ChargeFactorTypeName;

        /// <summary>
        /// Provider Number
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ProviderNumber;
    }
}