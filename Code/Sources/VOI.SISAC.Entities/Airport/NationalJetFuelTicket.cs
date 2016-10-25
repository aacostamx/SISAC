//------------------------------------------------------------------------
// <copyright file="NationalJetFuelTicket.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using Finance;
    using Itineraries;
    using Security;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// NationalJetFuelTicket Class
    /// </summary>
    [Table("Airport.NationalJetFuelTicket")]
    public partial class NationalJetFuelTicket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicket"/> class.
        /// </summary>
        public NationalJetFuelTicket() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicket"/> class.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        public NationalJetFuelTicket(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey)
        {
            this.Sequence = Sequence;
            this.AirlineCode = AirlineCode;
            this.FlightNumber = FlightNumber;
            this.ItineraryKey = ItineraryKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicket"/> class.
        /// </summary>
        /// <param name="Sequence">The sequence.</param>
        /// <param name="AirlineCode">The airline code.</param>
        /// <param name="FlightNumber">The flight number.</param>
        /// <param name="ItineraryKey">The itinerary key.</param>
        /// <param name="OperationTypeName">Name of the operation type.</param>
        public NationalJetFuelTicket(int Sequence, string AirlineCode, string FlightNumber, string ItineraryKey, string OperationTypeName)
        {
            this.Sequence = Sequence;
            this.AirlineCode = AirlineCode;
            this.FlightNumber = FlightNumber;
            this.ItineraryKey = ItineraryKey;
            this.OperationTypeName = OperationTypeName;
        }

        /// <summary>
        /// Gets or sets the national jet fuel ticket identifier.
        /// </summary>
        /// <value>
        /// The national jet fuel ticket identifier.
        /// </value>
        public long NationalJetFuelTicketID { get; set; }

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
        /// Gets or sets the name of the operation type.
        /// </summary>
        /// <value>
        /// The name of the operation type.
        /// </value>
        [Required]
        [StringLength(20)]
        public string OperationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [Required]
        [StringLength(12)]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the fueling date start.
        /// </summary>
        /// <value>
        /// The fueling date start.
        /// </value>
        public DateTime FuelingDateStart { get; set; }

        /// <summary>
        /// Gets or sets the fueling time start.
        /// </summary>
        /// <value>
        /// The fueling time start.
        /// </value>
        public TimeSpan FuelingTimeStart { get; set; }

        /// <summary>
        /// Gets or sets the fueling date end.
        /// </summary>
        /// <value>
        /// The fueling date end.
        /// </value>
        public DateTime FuelingDateEnd { get; set; }

        /// <summary>
        /// Gets or sets the fueling time end.
        /// </summary>
        /// <value>
        /// The fueling time end.
        /// </value>
        public TimeSpan FuelingTimeEnd { get; set; }

        /// <summary>
        /// Gets or sets the apron position.
        /// </summary>
        /// <value>
        /// The apron position.
        /// </value>
        [Required]
        [StringLength(8)]
        public string ApronPosition { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel provider number.
        /// </summary>
        /// <value>
        /// The jet fuel provider number.
        /// </value>
        [Required]
        [StringLength(10)]
        public string JetFuelProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the into plane provider number.
        /// </summary>
        /// <value>
        /// The into plane provider number.
        /// </value>
        [Required]
        [StringLength(10)]
        public string IntoPlaneProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        /// <value>
        /// The ticket number.
        /// </value>
        [Required]
        [StringLength(10)]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the fueled qty LTS.
        /// </summary>
        /// <value>
        /// The fueled qty LTS.
        /// </value>
        public int FueledQtyLts { get; set; }

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
        [Column(TypeName = "numeric")]
        public decimal? DensityFactor { get; set; }

        /// <summary>
        /// Gets or sets the aor user identifier.
        /// </summary>
        /// <value>
        /// The aor user identifier.
        /// </value>
        public int AorUserID { get; set; }

        /// <summary>
        /// Gets or sets the supplier responsible.
        /// </summary>
        /// <value>
        /// The supplier responsible.
        /// </value>
        [StringLength(150)]
        public string SupplierResponsible { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        [StringLength(250)]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public virtual Itinerary Itinerary { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel provider.
        /// </summary>
        /// <value>
        /// The jet fuel provider.
        /// </value>
        public virtual Provider JetFuelProvider { get; set; }

        /// <summary>
        /// Gets or sets the into plane provider.
        /// </summary>
        /// <value>
        /// The into plane provider.
        /// </value>
        public virtual Provider IntoPlaneProvider { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public virtual Service Service { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

    }
}
