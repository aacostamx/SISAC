//------------------------------------------------------------------------
// <copyright file="InternationalFuelRatesFile.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Files
{
    using FileHelpers;
    using System;
    using Helpers;

    /// <summary>
    /// InternationalFuelRatesFile
    /// </summary>
    [DelimitedRecord("\t")]
    public class InternationalFuelRatesFile
    {
        [FieldIgnored]
        public int LineNumber; 

        /// <summary>
        /// EffectiveDate
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        public DateTime EffectiveDate;

        /// <summary>
        /// AirlineCode
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        public string AirlineCode;

        /// <summary>
        /// StationCode
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        public string StationCode;

        /// <summary>
        /// ServiceCode
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        public string ServiceCode;

        /// <summary>
        /// ProviderNumberPrimary
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        public string ProviderNumberPrimary;

        /// <summary>
        /// FuelConceptTypeCode
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        public string FuelConceptName;

        /// <summary>
        /// ProviderNumber
        /// </summary>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        public string ProviderNumber;

        /// <summary>
        /// RateStartDate
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        public DateTime RateStartDate;

        /// <summary>
        /// RateEndDate
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        public DateTime RateEndDate;

        /// <summary>
        /// Rate
        /// </summary>
        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal, ".")]
        public Decimal Rate;
    }
}