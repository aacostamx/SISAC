//------------------------------------------------------------------------
// <copyright file="ManifestDeparture.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Itineraries
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Manifest departure entity
    /// </summary>
    public class ManifestDeparture
    {
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
        /// Gets or sets the departure station.
        /// </summary>
        /// <value>
        /// The departure station.
        /// </value>
        public string DepartureStation { get; set; }

        /// <summary>
        /// Gets or sets the arrival station.
        /// </summary>
        /// <value>
        /// The arrival station.
        /// </value>
        public string ArrivalStation { get; set; }

        /// <summary>
        /// Gets or sets the scale station.
        /// </summary>
        /// <value>
        /// The scale station.
        /// </value>
        public string ScaleStation { get; set; }

        /// <summary>
        /// Gets or sets the actual departure date.
        /// </summary>
        /// <value>
        /// The actual departure date.
        /// </value>
        public DateTime ActualDepartureDate { get; set; }

        /// <summary>
        /// Gets or sets the delay remarks.
        /// </summary>
        /// <value>
        /// The delay remarks.
        /// </value>
        public string DelayRemarks { get; set; }

        /// <summary>
        /// Gets or sets the inner section.
        /// </summary>
        /// <value>
        /// The inner section.
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
        /// Gets or sets the user signature identifier.
        /// </summary>
        /// <value>
        /// The user signature identifier.
        /// </value>
        public int UserSignatureId { get; set; }

        /// <summary>
        /// Gets or sets the licence number signature.
        /// </summary>
        /// <value>
        /// The licence number signature.
        /// </value>
        public string LicenceNumberSignature { get; set; }

        /// <summary>
        /// Gets or sets the user authorize identifier.
        /// </summary>
        /// <value>
        /// The user authorize identifier.
        /// </value>
        public int UserAuthorizeId { get; set; }

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
        /// Gets or sets the infants tickets.
        /// </summary>
        /// <value>
        /// The infants tickets.
        /// </value>
        public int InfantsTickets { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ManifestDeparture"/> is closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if closed; otherwise, <c>false</c>.
        /// </value>
        public bool Closed { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public virtual Itinerary Itinerary { get; set; }

        /// <summary>
        /// Gets or sets the additional departure information.
        /// </summary>
        /// <value>
        /// The additional departure information.
        /// </value>
        public virtual AdditionalDepartureInformation AdditionalDepartureInformation { get; set; }

        /// <summary>
        /// Gets or sets the delays.
        /// </summary>
        /// <value>
        /// The delays.
        /// </value>
        public virtual IList<Delay> Delays { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boardings.
        /// </summary>
        /// <value>
        /// The manifest departure boardings.
        /// </value>
        public virtual ICollection<ManifestDepartureBoarding> ManifestDepartureBoardings { get; set; }
    }
}
