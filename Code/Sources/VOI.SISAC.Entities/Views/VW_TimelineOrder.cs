//------------------------------------------------------------------------
// <copyright file="VW_TimelineOrder.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Views
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// VW_TimelineOrder class
    /// </summary>
    [Table("Itinerary.VW_TimelineOrder")]
    public partial class VW_TimelineOrder
    {
        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        public long? Row { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        [Key]
        [Column(Order = 0)]
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Key]
        [Column(Order = 1)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        [Key]
        [Column(Order = 2)]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        [Key]
        [Column(Order = 3)]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the departure date.
        /// </summary>
        /// <value>
        /// The departure date.
        /// </value>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Gets or sets the departure station.
        /// </summary>
        /// <value>
        /// The departure station.
        /// </value>
        public string DepartureStation { get; set; }

        /// <summary>
        /// Gets or sets the arrival date.
        /// </summary>
        /// <value>
        /// The arrival date.
        /// </value>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets the arrival station.
        /// </summary>
        /// <value>
        /// The arrival station.
        /// </value>
        public string ArrivalStation { get; set; }

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
        /// Gets or sets the next sequence.
        /// </summary>
        /// <value>
        /// The next sequence.
        /// </value>
        public int? NextSequence { get; set; }

        /// <summary>
        /// Gets or sets the next airline code.
        /// </summary>
        /// <value>
        /// The next airline code.
        /// </value>
        public string NextAirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the next flight number.
        /// </summary>
        /// <value>
        /// The next flight number.
        /// </value>
        public string NextFlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the next itinerary key.
        /// </summary>
        /// <value>
        /// The next itinerary key.
        /// </value>
        public string NextItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [special case].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [special case]; otherwise, <c>false</c>.
        /// </value>
        public bool SpecialCase { get; set; }
    }
}
