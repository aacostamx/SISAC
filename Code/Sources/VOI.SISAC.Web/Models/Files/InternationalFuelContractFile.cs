//------------------------------------------------------------------------
// <copyright file="CultureHelper.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Files
{
    using FileHelpers;
    using System;
    using Helpers;

    /// <summary>
    /// InternationalFuelContractFile
    /// </summary>
    [DelimitedRecord("\t")]
    public class InternationalFuelContractFile
    {
        /// <summary>
        /// EffectiveDate
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime EffectiveDate;

        /// <summary>
        /// AirlineCode
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string AirlineCode;

        /// <summary>
        /// StationCode
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string StationCode;

        /// <summary>
        /// ServiceCode
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ServiceCode;

        /// <summary>
        /// ProviderNumberPrimary
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ProviderNumberPrimary;

        /// <summary>
        /// CCNumber
        /// </summary>
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CCNumber;

        /// <summary>
        /// AircraftRegistCCFlag
        /// </summary>
        [FieldConverter(ConverterKind.Boolean)]
        public bool AircraftRegistCCFlag;

        /// <summary>
        /// AccountingAccountNumber
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        public string AccountingAccountNumber;

        /// <summary>
        /// LiabilityAccountNumber
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string LiabilityAccountNumber;

        /// <summary>
        /// OperationName
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string OperationName;

        /// <summary>
        /// CurrencyCode
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CurrencyCode;

        /// <summary>
        /// FuelConceptName
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FuelConceptName;

        /// <summary>
        /// FuelConceptTypeName
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FuelConceptTypeName;

        /// <summary>
        /// ChargeFactorTypeName
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ChargeFactorTypeName;

        /// <summary>
        /// ProviderNumber
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ProviderNumber;
    }
}