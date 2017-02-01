//------------------------------------------------------------------------
// <copyright file="Itinerary.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Airport;

    /// <summary>
    /// Itinerary Entity
    /// </summary>
    [Table("Itinerary.Itinerary")]
    public partial class Itinerary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Itinerary"/> class.
        /// </summary>
        public Itinerary() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Itinerary"/> class.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        public Itinerary(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey)
        {
            this.Sequence = Sequence;
            this.AirlineCode = AirlineCode;
            this.FlightNumber = FlightNumber;
            this.ItineraryKey = ItineraryKey;
        }

        /// <summary>
        /// Gets or sets the line.
        /// </summary>
        /// <value>
        /// The line.
        /// </value>
        [NotMapped]
        public int Line { get; set; }

        /// <summary>
        /// Data of the flight sequence
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int Sequence { get; set; }

        /// <summary>
        /// CarrierCode = Airline Code
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Flight Number of the Airplane
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Primary Key = Departure Date + CarrierCode + Flight Number + DepartureStation + DepartureArrival
        /// </summary>
        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets Equipment Number
        /// </summary>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets Departure Date
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Gets or sets Departure Airport
        /// </summary>
        public string DepartureStation { get; set; }

        /// <summary>
        /// Gets or sets Arrival Date
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets Arrival Airport
        /// </summary>
        public string ArrivalStation { get; set; }

        /// <summary>
        /// EditArrival 
        /// </summary>
        [NotMapped]
        public bool? EditArrival { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation type.
        /// </summary>
        /// <value>
        /// The name of the operation type.
        /// </value>
        [NotMapped]
        public string OperationTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Itinerary"/> is add.
        /// </summary>
        /// <value>
        ///   <c>true</c> if add; otherwise, <c>false</c>.
        /// </value>
        [NotMapped]
        public bool Add { get; set; }

        /// <summary>
        /// AirportServices collection
        /// </summary>
        public virtual ICollection<AirportService> AirportServices { get; set; }

        /// <summary>
        /// Gets or sets a list of Logs in itinerary
        /// </summary>
        //public virtual ICollection<ItineraryLog> itinerariesLogs { get; set; }

        /// <summary>
        /// JetFuelTickets collection
        /// </summary>
        public virtual ICollection<JetFuelTicket> JetFuelTickets { get; set; }

        /// <summary>
        /// Gets or sets the gendec departure.
        /// </summary>
        /// <value>
        /// The gendec departure.
        /// </value>
        public virtual GendecDeparture GendecDepartures { get; set; }

        /// <summary>
        /// Gets or sets the gendec arrival.
        /// </summary>
        /// <value>
        /// The gendec arrival.
        /// </value>
        public virtual GendecArrival GendecArrivals { get; set; }

        /// <summary>
        /// Gets or sets the passenger information.
        /// </summary>
        /// <value>
        /// The passenger information.
        /// </value>
        public virtual PassengerInformation PassengerInformation { get; set; }

        /// <summary>
        /// Gets or sets the airplane.
        /// </summary>
        /// <value>
        /// The airplane.
        /// </value>
        public virtual Airplane Airplane { get; set; }

        /////// <summary>
        /////// ItineraryLog
        /////// </summary>
        ////public virtual ICollection<ItineraryLog> ItineraryLogs { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure.
        /// </summary>
        /// <value>
        /// The manifest departure.
        /// </value>
        public virtual ManifestDeparture ManifestDeparture { get; set; }

        /// <summary>
        /// Gets or sets the manifest arrival.
        /// </summary>
        /// <value>
        /// The manifest arrival.
        /// </value>
        public virtual ManifestArrival ManifestArrival { get; set; }

        /// <summary>
        /// Gets or sets the timeline.
        /// </summary>
        /// <value>
        /// The timeline.
        /// </value>
        public virtual Timeline Timeline { get; set; }
    }
}