//------------------------------------------------------------------------
// <copyright file="NationalFuelRateFile.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Files
{
    using System;
    using FileHelpers;
    using Helpers;

    /// <summary>
    /// National Fuel Rate File
    /// </summary>
    [DelimitedRecord("\t")]
    public class NationalFuelRateFile
    {
        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string StationCode;

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ServiceCode;

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ProviderNumber;

        /// <summary>
        /// Gets or sets the effective start date.
        /// </summary>
        /// <value>
        /// The effective start date.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime EffectiveStartDate;

        /// <summary>
        /// Gets or sets the effective end date.
        /// </summary>
        /// <value>
        /// The effective end date.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(typeof(ConvertDate))]
        public DateTime EffectiveEndDate;

        /// <summary>
        /// Gets or sets the type of the schedule.
        /// </summary>
        /// <value>
        /// The type of the schedule.
        /// </value>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string ScheduleTypeName;

        /// <summary>
        /// Gets or sets the type of the fuel concept.
        /// </summary>
        /// <value>
        /// The type of the fuel concept.
        /// </value>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string FuelConceptTypeName;

        /// <summary>
        /// Gets or sets the rate value.
        /// </summary>
        /// <value>
        /// The rate value.
        /// </value>
        [FieldNotEmpty]
        [FieldConverter(ConverterKind.Decimal)]
        public decimal RateValue;

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        [FieldNotEmpty]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(typeof(UpperStringHelper))]
        public string CurrencyCode;
    }
}