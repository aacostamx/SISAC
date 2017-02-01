//------------------------------------------------------------------------
// <copyright file="ItineraryLog.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace VOI.SISAC.Entities.Itineraries
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Itinerary Log Entity
    /// </summary>
    [Table("Itinerary.ItineraryLog")]
    public partial class ItineraryLog
    {
        /// <summary>
        /// Data of the flight sequence
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        /// Primary Key = Departure Date in format yyyMMdd
        /// </summary>
        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets Equipment Number
        /// </summary>
        [Key]
        [Column(Order = 4)]
        [StringLength(12)]
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets Departure Date
        /// </summary>
        [Key]
        [Column(Order = 5)]
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Gets or sets Departure Airport
        /// </summary>
        [Key]
        [Column(Order = 6)]
        [StringLength(3)]
        public string DepartureStation { get; set; }

        /// <summary>
        /// Gets or sets Arrival Date
        /// </summary>
        [Key]
        [Column(Order = 7)]
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets Arrival Airport
        /// </summary>
        [Key]
        [Column(Order = 8)]
        [StringLength(3)]
        public string ArrivalStation { get; set; }

        /// <summary>
        /// End date of the movement in the itinerary
        /// </summary>
        [Key]
        [Column(Order = 9)]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Comment for the movement done
        /// </summary>
        [StringLength(250)]
        public string Remarks { get; set; }

        ///// <summary>
        ///// In base the itinerary extracts the information.
        ///// </summary>        
        //public virtual Itinerary Itinerary { get; set; }
    }
}
