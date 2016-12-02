//------------------------------------------------------------------------
// <copyright file="PassengerInformationDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Business.Dto.Security;

    /// <summary>
    /// Data Object of Passenger Information
    /// </summary>
    public class PassengerInformationDto
    {
        /// <summary>
        /// Gets or sets the sequence.
        /// Foreing key.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// Foreing key.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// Foreing key.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// Foreing key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the adults in cabin a.
        /// </summary>
        /// <value>
        /// The adults in cabin a.
        /// </value>
        public int AdultsCabinA { get; set; }

        /// <summary>
        /// Gets or sets the adults in cabin b.
        /// </summary>
        /// <value>
        /// The adults in cabin b.
        /// </value>
        public int AdultsCabinB { get; set; }

        /// <summary>
        /// Gets or sets the teenage in cabin a.
        /// </summary>
        /// <value>
        /// The teenage in cabin a.
        /// </value>
        public int TeenageCabinA { get; set; }

        /// <summary>
        /// Gets or sets the teenage in cabin b.
        /// </summary>
        /// <value>
        /// The teenage in cabin b.
        /// </value>
        public int TeenageCabinB { get; set; }

        /// <summary>
        /// Gets or sets the children in cabin a.
        /// </summary>
        /// <value>
        /// The children in cabin a.
        /// </value>
        public int ChildrenCabinA { get; set; }

        /// <summary>
        /// Gets or sets the children in cabin b.
        /// </summary>
        /// <value>
        /// The children in cabin b.
        /// </value>
        public int ChildrenCabinB { get; set; }

        /// <summary>
        /// Gets or sets the local adults.
        /// </summary>
        /// <value>
        /// The local adults.
        /// </value>
        public int LocalAdults { get; set; }

        /// <summary>
        /// Gets or sets the local teenage.
        /// </summary>
        /// <value>
        /// The local teenage.
        /// </value>
        public int LocalTeenage { get; set; }

        /// <summary>
        /// Gets or sets the local children.
        /// </summary>
        /// <value>
        /// The local children.
        /// </value>
        public int LocalChildren { get; set; }

        /// <summary>
        /// Gets or sets the transitory adults.
        /// </summary>
        /// <value>
        /// The transitory adults.
        /// </value>
        public int TransitoryAdults { get; set; }

        /// <summary>
        /// Gets or sets the transitory teenage.
        /// </summary>
        /// <value>
        /// The transitory teenage.
        /// </value>
        public int TransitoryTeenage { get; set; }

        /// <summary>
        /// Gets or sets the transitory children.
        /// </summary>
        /// <value>
        /// The transitory children.
        /// </value>
        public int TransitoryChildren { get; set; }

        /// <summary>
        /// Gets or sets the connection adults.
        /// </summary>
        /// <value>
        /// The connection adults.
        /// </value>
        public int ConnectionAdults { get; set; }

        /// <summary>
        /// Gets or sets the connection teenage.
        /// </summary>
        /// <value>
        /// The connection teenage.
        /// </value>
        public int ConnectionTeenage { get; set; }

        /// <summary>
        /// Gets or sets the connection children.
        /// </summary>
        /// <value>
        /// The connection children.
        /// </value>
        public int ConnectionChildren { get; set; }

        /// <summary>
        /// Gets or sets the diplomatic.
        /// </summary>
        /// <value>
        /// The diplomatic.
        /// </value>
        public int Diplomatic { get; set; }

        /// <summary>
        /// Gets or sets the extra crew.
        /// </summary>
        /// <value>
        /// The extra crew.
        /// </value>
        public int ExtraCrew { get; set; }

        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        /// <value>
        /// The other.
        /// </value>
        public int Other { get; set; }

        /// <summary>
        /// Gets or sets the local baggage quantity.
        /// </summary>
        /// <value>
        /// The local baggage.
        /// </value>
        public int LocalBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the transitory baggage quantity.
        /// </summary>
        /// <value>
        /// The transitory baggage quantity.
        /// </value>
        public int TransitoryBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the connection baggage quantity.
        /// </summary>
        /// <value>
        /// The connection baggage quantity.
        /// </value>
        public int ConnectionBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the diplomatic baggage quantity.
        /// </summary>
        /// <value>
        /// The diplomatic baggage quantity.
        /// </value>
        public int DiplomaticBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the extra crew baggage quantity.
        /// </summary>
        /// <value>
        /// The extra crew baggage quantity.
        /// </value>
        public int ExtraCrewBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the other baggage quantity.
        /// </summary>
        /// <value>
        /// The other baggage quantity.
        /// </value>
        public int OtherBaggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the local baggage weight.
        /// </summary>
        /// <value>
        /// The local baggage weight.
        /// </value>
        public int LocalBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the transitory baggage weight.
        /// </summary>
        /// <value>
        /// The transitory baggage weight.
        /// </value>
        public int TransitoryBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the connection baggage weight.
        /// </summary>
        /// <value>
        /// The connection baggage weight.
        /// </value>
        public int ConnectionBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the diplomatic.
        /// </summary>
        /// <value>
        /// The diplomatic.
        /// </value>
        public int DiplomaticBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the extra crew baggage weight.
        /// </summary>
        /// <value>
        /// The extra crew baggage weight.
        /// </value>
        public int ExtraCrewBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the other baggage weight.
        /// </summary>
        /// <value>
        /// The other baggage weight.
        /// </value>
        public int OtherBaggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the observation.
        /// </summary>
        /// <value>
        /// The observation.
        /// </value>
        public string Observation { get; set; }

        /// <summary>
        /// Gets or sets the international TUA.
        /// </summary>
        /// <value>
        /// The international TUA.
        /// </value>
        public int InternationalTua { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// Foreing key.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the previous sequence.
        /// </summary>
        /// <value>
        /// The previous sequence.
        /// </value>
        public int? PreviousSequence { get; set; }

        /// <summary>
        /// Gets or sets the previous airline code.
        /// </summary>
        /// <value>
        /// The previous airline code.
        /// </value>
        public string PreviousAirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the previous flight number.
        /// </summary>
        /// <value>
        /// The previous flight number.
        /// </value>
        public string PreviousFlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the previous itinerary key.
        /// </summary>
        /// <value>
        /// The previous itinerary key.
        /// </value>
        public string PreviousItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public ItineraryDto Itinerary { get; set; }

        /// <summary>
        /// Gets or sets Users information
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public AdditionalPassengerInformationDto AdditonalInformation { get; set; }
    }
}
