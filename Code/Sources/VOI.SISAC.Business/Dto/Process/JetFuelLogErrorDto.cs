//------------------------------------------------------------------------
// <copyright file="JetFuelLogErrorDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using System;

    /// <summary>
    /// Data transfer object for jet fuel log error
    /// </summary>
    public class JetFuelLogErrorDto
    {
        /// <summary>
        /// Gets or sets the log identifier.
        /// </summary>
        /// <value>
        /// The log identifier.
        /// </value>
        public long LogId { get; set; }

        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        public string PeriodCode { get; set; }

        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        public int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the itinerary sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int ItinerarySequence { get; set; }

        /// <summary>
        /// Gets or sets the itinerary airline code.
        /// </summary>
        /// <value>
        /// The itinerary airline code.
        /// </value>
        public string ItineraryAirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the itinerary flight number.
        /// </summary>
        /// <value>
        /// The itinerary flight number.
        /// </value>
        public string ItineraryFlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the airplane equipment number.
        /// </summary>
        /// <value>
        /// The airplane equipment number.
        /// </value>
        public string AirplaneEquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public int? OperationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel ticked identifier.
        /// </summary>
        /// <value>
        /// The jet fuel ticked identifier.
        /// </value>
        public long? JetFuelTicketId { get; set; }

        /// <summary>
        /// Gets or sets the fueling date.
        /// </summary>
        /// <value>
        /// The fueling date.
        /// </value>
        public DateTime? FuelingDate { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        /// <value>
        /// The ticket number.
        /// </value>
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the fueled quantity gallon.
        /// </summary>
        /// <value>
        /// The fueled quantity gallon.
        /// </value>
        public int? FueledQuantityGallon { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the provider number primary.
        /// </summary>
        /// <value>
        /// The provider number primary.
        /// </value>
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// Gets or sets the international fuel contract concept identifier.
        /// </summary>
        /// <value>
        /// The international fuel contract concept identifier.
        /// </value>
        public long? InternationalFuelContractConceptId { get; set; }

        /// <summary>
        /// Gets or sets the fuel concept identifier.
        /// </summary>
        /// <value>
        /// The fuel concept identifier.
        /// </value>
        public long? FuelConceptId { get; set; }

        /// <summary>
        /// Gets or sets the fuel concept type code.
        /// </summary>
        /// <value>
        /// The fuel concept type code.
        /// </value>
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the charge factor type identifier.
        /// </summary>
        /// <value>
        /// The charge factor type identifier.
        /// </value>
        public int? ChargeFactorTypeId { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        public decimal? Rate { get; set; }
    }
}
