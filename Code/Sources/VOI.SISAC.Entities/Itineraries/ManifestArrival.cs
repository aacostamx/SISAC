//------------------------------------------------------------------------
// <copyright file="ManifestArrival.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    public class ManifestArrival
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
        /// Gets or sets the last scale station.
        /// </summary>
        /// <value>
        /// The last scale station.
        /// </value>
        public string LastScaleStation { get; set; }

        /// <summary>
        /// Gets or sets the actual arrival date.
        /// </summary>
        /// <value>
        /// The actual departure date.
        /// </value>
        public DateTime ActualArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets the delay remarks.
        /// </summary>
        /// <value>
        /// The delay remarks.
        /// </value>
        public string DelayRemarks { get; set; }

        /// <summary>
        /// Gets or sets the adult passenger.
        /// </summary>
        /// <value>
        /// The adult passenger.
        /// </value>
        public int AdultPassenger { get; set; }

        /// <summary>
        /// Gets or sets the minor passenger.
        /// </summary>
        /// <value>
        /// The minor passenger.
        /// </value>
        public int MinorPassenger { get; set; }

        /// <summary>
        /// Gets or sets the infant passenger.
        /// </summary>
        /// <value>
        /// The infant passenger.
        /// </value>
        public int InfantPassenger { get; set; }

        /// <summary>
        /// Gets or sets the luggage quantity.
        /// </summary>
        /// <value>
        /// The luggage quantity.
        /// </value>
        public int LuggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the luggage weight.
        /// </summary>
        /// <value>
        /// The luggage weight.
        /// </value>
        public decimal LuggageWeight { get; set; }

        /// <summary>
        /// Gets or sets the charge quantity.
        /// </summary>
        /// <value>
        /// The charge quantity.
        /// </value>
        public int ChargeQuantity { get; set; }

        /// <summary>
        /// Gets or sets the charge weight.
        /// </summary>
        /// <value>
        /// The charge weight.
        /// </value>
        public decimal ChargeWeight { get; set; }

        /// <summary>
        /// Gets or sets the mail quantity.
        /// </summary>
        /// <value>
        /// The mail quantity.
        /// </value>
        public int MailQuantity { get; set; }

        /// <summary>
        /// Gets or sets the mail weight.
        /// </summary>
        /// <value>
        /// The mail weight.
        /// </value>
        public decimal MailWeight { get; set; }

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
        /// Gets or sets the jet fuel arrival.
        /// </summary>
        /// <value>
        /// The jet fuel arrival.
        /// </value>
        public decimal JetFuelArrival { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

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
        /// Gets or sets the delays.
        /// </summary>
        /// <value>
        /// The delays.
        /// </value>
        public virtual IList<Delay> Delays { get; set; }
    }
}
