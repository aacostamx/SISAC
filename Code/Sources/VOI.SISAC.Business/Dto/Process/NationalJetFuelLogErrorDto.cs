//------------------------------------------------------------------------
// <copyright file="NationalJetFuelLogErrorDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------


namespace VOI.SISAC.Business.Dto.Process
{
    using System;

    /// <summary>
    /// Data transfer object for national jet fuel log error
    /// </summary>
    public class NationalJetFuelLogErrorDto
    {
        /// <summary>
        /// Gets or sets the log identifier.
        /// </summary>
        /// <value>
        /// The log identifier.
        /// </value>
        public long LogID { get; set; }

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
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the operation type identifier.
        /// </summary>
        /// <value>
        /// The operation type identifier.
        /// </value>
        public int? OperationTypeID { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel ticket identifier.
        /// </summary>
        /// <value>
        /// The national jet fuel ticket identifier.
        /// </value>
        public long? NationalJetFuelTicketID { get; set; }

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
        /// Gets or sets the fueling start date.
        /// </summary>
        /// <value>
        /// The fueling start date.
        /// </value>
        public DateTime? FuelingStartDate { get; set; }

        /// <summary>
        /// Gets or sets the fueling start time.
        /// </summary>
        /// <value>
        /// The fueling start time.
        /// </value>
        public TimeSpan? FuelingStartTime { get; set; }

        /// <summary>
        /// Gets or sets the fueling end date.
        /// </summary>
        /// <value>
        /// The fueling end date.
        /// </value>
        public DateTime? FuelingEndDate { get; set; }

        /// <summary>
        /// Gets or sets the fueling end time.
        /// </summary>
        /// <value>
        /// The fueling end time.
        /// </value>
        public TimeSpan? FuelingEndTime { get; set; }

        /// <summary>
        /// Gets or sets the apron position.
        /// </summary>
        /// <value>
        /// The apron position.
        /// </value>
        public string ApronPosition { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        /// <value>
        /// The ticket number.
        /// </value>
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the fueled qty LTS.
        /// </summary>
        /// <value>
        /// The fueled qty LTS.
        /// </value>
        public int? FueledQtyLts { get; set; }

        /// <summary>
        /// Gets or sets the remaining qty KGS.
        /// </summary>
        /// <value>
        /// The remaining qty KGS.
        /// </value>
        public int? RemainingQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the requested qty KGS.
        /// </summary>
        /// <value>
        /// The requested qty KGS.
        /// </value>
        public int? RequestedQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the fueled qty KGS.
        /// </summary>
        /// <value>
        /// The fueled qty KGS.
        /// </value>
        public int? FueledQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the density factor.
        /// </summary>
        /// <value>
        /// The density factor.
        /// </value>
        public decimal? DensityFactor { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contract concept identifier.
        /// </summary>
        /// <value>
        /// The national fuel contract concept identifier.
        /// </value>
        public long? NationalFuelContractConceptID { get; set; }

        /// <summary>
        /// Gets or sets the fuel concept identifier.
        /// </summary>
        /// <value>
        /// The fuel concept identifier.
        /// </value>
        public long? FuelConceptID { get; set; }

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
        public int? ChargeFactorTypeID { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the schedule type code.
        /// </summary>
        /// <value>
        /// The schedule type code.
        /// </value>
        public string ScheduleTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        public decimal? Rate { get; set; }
    }
}