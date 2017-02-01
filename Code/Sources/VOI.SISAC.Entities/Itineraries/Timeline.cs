//------------------------------------------------------------------------
// <copyright file="Timeline.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Itineraries
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Paged;


    /// <summary>
    /// Timeline Entity
    /// </summary>
    [Table("Itinerary.Timeline")]
    public partial class Timeline
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Timeline"/> class.
        /// </summary>
        public Timeline()
        {
            TimelineMovements = new List<TimelineMovement>();
        }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        [NotMapped]
        public long? Row { get; set; }

        /// <summary>
        /// Gets or sets the maximum row.
        /// </summary>
        /// <value>
        /// The maximum row.
        /// </value>
        [NotMapped]
        public long? MaxRow { get; set; }

        /// <summary>
        /// Gets or sets the minimum row.
        /// </summary>
        /// <value>
        /// The minimum row.
        /// </value>
        [NotMapped]
        public long? MinRow { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ItineraryKey { get; set; }

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
        [StringLength(3)]
        public string PreviousAirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the previous flight number.
        /// </summary>
        /// <value>
        /// The previous flight number.
        /// </value>
        [StringLength(5)]
        public string PreviousFlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the previous itinerary key.
        /// </summary>
        /// <value>
        /// The previous itinerary key.
        /// </value>
        [StringLength(8)]
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
        [StringLength(3)]
        public string NextAirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the next flight number.
        /// </summary>
        /// <value>
        /// The next flight number.
        /// </value>
        [StringLength(5)]
        public string NextFlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the next itinerary key.
        /// </summary>
        /// <value>
        /// The next itinerary key.
        /// </value>
        [StringLength(8)]
        public string NextItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [special case].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [special case]; otherwise, <c>false</c>.
        /// </value>
        public bool SpecialCase { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public virtual Itinerary Itinerary { get; set; }

        /// <summary>
        /// Gets or sets the timeline movements.
        /// </summary>
        /// <value>
        /// The timeline movements.
        /// </value>
        public virtual IList<TimelineMovement> TimelineMovements { get; set; }
    }
}