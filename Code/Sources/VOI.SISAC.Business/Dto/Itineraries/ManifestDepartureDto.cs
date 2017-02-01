//------------------------------------------------------------------------
// <copyright file="ManifestDepartureDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Itineraries
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Airports;

    /// <summary>
    /// Manifest departure data transfer object
    /// </summary>
    public class ManifestDepartureDto
    {
        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline.
        /// </summary>
        /// <value>
        /// The airline.
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
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the nick name commander.
        /// </summary>
        /// <value>
        /// The nick name commander.
        /// </value>
        public string NickNameCommander { get; set; }

        /// <summary>
        /// Gets or sets the nick name first official.
        /// </summary>
        /// <value>
        /// The nick name first official.
        /// </value>
        public string NickNameFirstOfficial { get; set; }

        /// <summary>
        /// Gets or sets the nick name second official.
        /// </summary>
        /// <value>
        /// The nick name second official.
        /// </value>
        public string NickNameSecondOfficial { get; set; }

        /// <summary>
        /// Gets or sets the nick name third official.
        /// </summary>
        /// <value>
        /// The nick name third official.
        /// </value>
        public string NickNameThirdOfficial { get; set; }

        /// <summary>
        /// Gets or sets the nick name chief cabinet.
        /// </summary>
        /// <value>
        /// The nick name chief cabinet.
        /// </value>
        public string NickNameChiefCabinet { get; set; }

        /// <summary>
        /// Gets or sets the nick name first supercargo.
        /// </summary>
        /// <value>
        /// The nick name first supercargo.
        /// </value>
        public string NickNameFirstSupercargo { get; set; }

        /// <summary>
        /// Gets or sets the nick name second supercargo.
        /// </summary>
        /// <value>
        /// The nick name second supercargo.
        /// </value>
        public string NickNameSecondSupercargo { get; set; }

        /// <summary>
        /// Gets or sets the nick name third supercargo.
        /// </summary>
        /// <value>
        /// The nick name third supercargo.
        /// </value>
        public string NickNameThirdSupercargo { get; set; }

        /// <summary>
        /// Gets or sets the supercargo remarks.
        /// </summary>
        /// <value>
        /// The supercargo remarks.
        /// </value>
        public string SupercargoRemarks { get; set; }

        /// <summary>
        /// Gets or sets the departure station code.
        /// </summary>
        /// <value>
        /// The departure station code.
        /// </value>
        public string DepartureStationCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the departure station.
        /// </summary>
        /// <value>
        /// The name of the departure station.
        /// </value>
        public string DepartureStationName { get; set; }

        /// <summary>
        /// Gets or sets the scale station code.
        /// </summary>
        /// <value>
        /// The scale station code.
        /// </value>
        public string ScaleStationCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the scale station.
        /// </summary>
        /// <value>
        /// The name of the scale station.
        /// </value>
        public string ScaleStationName { get; set; }

        /// <summary>
        /// Gets or sets the arrival station code.
        /// </summary>
        /// <value>
        /// The arrival station code.
        /// </value>
        public string ArrivalStationCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the arrival station.
        /// </summary>
        /// <value>
        /// The name of the arrival station.
        /// </value>
        public string ArrivalStationName { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        public string ScheduledDepartureDate { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        public string ScheduledDepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        public string ActualDepartureDate { get; set; }

        /// <summary>
        /// Gets or sets the actual departure.
        /// </summary>
        /// <value>
        /// The actual departure.
        /// </value>
        public string ActualDepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the delay remarks.
        /// </summary>
        /// <value>
        /// The delay remarks.
        /// </value>
        public string DelayRemarks { get; set; }

        /// <summary>
        /// Gets or sets the inner trace.
        /// </summary>
        /// <value>
        /// The inner trace.
        /// </value>
        public int InnerSection { get; set; }

        /// <summary>
        /// Gets or sets the international.
        /// </summary>
        /// <value>
        /// The international.
        /// </value>
        public int International { get; set; }

        /// <summary>
        /// Gets or sets the international exempt.
        /// </summary>
        /// <value>
        /// The international exempt.
        /// </value>
        public int InternationalExempt { get; set; }

        /// <summary>
        /// Gets or sets the national exempt.
        /// </summary>
        /// <value>
        /// The national exempt.
        /// </value>
        public int NationalExempt { get; set; }

        /// <summary>
        /// Gets or sets the transit.
        /// </summary>
        /// <value>
        /// The transit.
        /// </value>
        public int Transit { get; set; }

        /// <summary>
        /// Gets or sets the infant.
        /// </summary>
        /// <value>
        /// The infant.
        /// </value>
        public int Infant { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel.
        /// </summary>
        /// <value>
        /// The jet fuel.
        /// </value>
        public decimal JetFuel { get; set; }

        /// <summary>
        /// Gets or sets the real takeoff weight.
        /// </summary>
        /// <value>
        /// The real takeoff weight.
        /// </value>
        public decimal RealTakeoffWeight { get; set; }

        /// <summary>
        /// Gets or sets the operating weight.
        /// </summary>
        /// <value>
        /// The operating weight.
        /// </value>
        public decimal OperatingWeight { get; set; }

        /// <summary>
        /// Gets or sets the safety margin.
        /// </summary>
        /// <value>
        /// The safety margin.
        /// </value>
        public decimal SafetyMargin { get; set; }

        /// <summary>
        /// Gets or sets the structural takeoff weight.
        /// </summary>
        /// <value>
        /// The structural takeoff weight.
        /// </value>
        public decimal StructuralTakeoffWeight { get; set; }

        /// <summary>
        /// Gets or sets the user identifier signature.
        /// </summary>
        /// <value>
        /// The user identifier signature.
        /// </value>
        public int UserIdSignature { get; set; }

        /// <summary>
        /// Gets or sets the user identifier authorize.
        /// </summary>
        /// <value>
        /// The user identifier authorize.
        /// </value>
        public int UserIdAuthorize { get; set; }

        /// <summary>
        /// Gets or sets the licence number signature.
        /// </summary>
        /// <value>
        /// The licence number signature.
        /// </value>
        public string LicenceNumberSignature { get; set; }

        /// <summary>
        /// Gets or sets the licence number authorize.
        /// </summary>
        /// <value>
        /// The licence number authorize.
        /// </value>
        public string LicenceNumberAuthorize { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the infant tickets.
        /// </summary>
        /// <value>
        /// The infant tickets.
        /// </value>
        public int InfantTickets { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ManifestDepartureDto"/> is closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if closed; otherwise, <c>false</c>.
        /// </value>
        public bool Closed { get; set; }

        /// <summary>
        /// Gets or sets the tolerance time.
        /// </summary>
        /// <value>
        /// The tolerance time.
        /// </value>
        public decimal ToleranceTime { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public ItineraryDto Itinerary { get; set; }
        
        /// <summary>
        /// Gets or sets the AdditionalDepartureInformation.
        /// </summary>
        /// <value>
        /// The Additional Departure Information.
        /// </value>
        public AdditionalDepartureInformationDto AdditionalDepartureInformation { get; set; }

        /// <summary>
        /// Gets or sets the delays.
        /// </summary>
        /// <value>
        /// The delays.
        /// </value>
        public IList<DelayDto> Delays { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boardings.
        /// </summary>
        /// <value>
        /// The manifest departure boardings.
        /// </value>
        public ICollection<ManifestDepartureBoardingDto> ManifestDepartureBoardings { get; set; }
    }
}
