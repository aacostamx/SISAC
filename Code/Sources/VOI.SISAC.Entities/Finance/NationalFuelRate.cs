//------------------------------------------------------------------------
// <copyright file="NationalFuelRate.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// National fuel rate entity
    /// </summary>
    public class NationalFuelRate
    {
        /// <summary>
        /// Gets or sets the national fuel rate identifier.
        /// </summary>
        /// <value>
        /// The national fuel rate identifier.
        /// </value>
        public long NationalFuelRateId { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the effective start date.
        /// </summary>
        /// <value>
        /// The effective start date.
        /// </value>
        public DateTime EffectiveStartDate { get; set; }

        /// <summary>
        /// Gets or sets the effective end date.
        /// </summary>
        /// <value>
        /// The effective end date.
        /// </value>
        public DateTime EffectiveEndDate { get; set; }

        /// <summary>
        /// Gets or sets the schedule type code.
        /// </summary>
        /// <value>
        /// The schedule type code.
        /// </value>
        public string ScheduleTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the fuel concept type code.
        /// </summary>
        /// <value>
        /// The fuel concept type code.
        /// </value>
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the rate value.
        /// </summary>
        /// <value>
        /// The rate value.
        /// </value>
        public decimal RateValue { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the airport.
        /// </summary>
        /// <value>
        /// The airport.
        /// </value>
        public Airport Airport { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public Service Service { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public Provider Provider { get; set; }

        /// <summary>
        /// Gets or sets the type of the schedule.
        /// </summary>
        /// <value>
        /// The type of the schedule.
        /// </value>
        public ScheduleType ScheduleType { get; set; }

        /// <summary>
        /// Gets or sets the type of the fuel concept.
        /// </summary>
        /// <value>
        /// The type of the fuel concept.
        /// </value>
        public FuelConceptType FuelConceptType { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public Currency Currency { get; set; }
    }
}
