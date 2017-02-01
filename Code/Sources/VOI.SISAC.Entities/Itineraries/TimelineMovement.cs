//------------------------------------------------------------------------
// <copyright file="TimelineMovement.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Itineraries
{
    using Catalog;
    using Finance;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// TimelineMovement Entity
    /// </summary>
    [Table("Itinerary.TimelineMovement")]
    public partial class TimelineMovement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Itinerary"/> class.
        /// </summary>
        public TimelineMovement() { }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long ID { get; set; }

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
        [Required]
        [StringLength(3)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        [Required]
        [StringLength(5)]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        [Required]
        [StringLength(8)]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the operation type identifier.
        /// </summary>
        /// <value>
        /// The operation type identifier.
        /// </value>
        public int OperationTypeID { get; set; }

        /// <summary>
        /// Gets or sets the movement type code.
        /// </summary>
        /// <value>
        /// The movement type code.
        /// </value>
        [Required]
        [StringLength(5)]
        public string MovementTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the movement date.
        /// </summary>
        /// <value>
        /// The movement date.
        /// </value>
        public DateTime MovementDate { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        [StringLength(50)]
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        [StringLength(10)]
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the remaining fuel.
        /// </summary>
        /// <value>
        /// The remaining fuel.
        /// </value>
        public decimal? RemainingFuel { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        [StringLength(250)]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the type of the movement.
        /// </summary>
        /// <value>
        /// The type of the movement.
        /// </value>
        public virtual MovementType MovementType { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public virtual OperationType OperationType { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public virtual Provider Provider { get; set; }

        /// <summary>
        /// Gets or sets the timeline.
        /// </summary>
        /// <value>
        /// The timeline.
        /// </value>
        public virtual Timeline Timeline { get; set; }
    }
}