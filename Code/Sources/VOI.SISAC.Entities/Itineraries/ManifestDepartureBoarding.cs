//------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoarding.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Itineraries
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ManifestDepartureBoarding
    /// </summary>
    [Table("Itinerary.ManifestDepartureBoarding")]
    public partial class ManifestDepartureBoarding
    {
        /// <summary>
        /// Gets or sets the boarding identifier.
        /// </summary>
        /// <value>
        /// The boarding identifier.
        /// </value>
        [Key]
        public long BoardingID { get; set; }

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
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the station.
        /// </summary>
        /// <value>
        /// The station.
        /// </value>
        [Required]
        [StringLength(3)]
        public string Station { get; set; }

        /// <summary>
        /// Gets or sets the passenger adult.
        /// </summary>
        /// <value>
        /// The passenger adult.
        /// </value>
        public int? PassengerAdult { get; set; }

        /// <summary>
        /// Gets or sets the passenger minor.
        /// </summary>
        /// <value>
        /// The passenger minor.
        /// </value>
        public int? PassengerMinor { get; set; }

        /// <summary>
        /// Gets or sets the passenger infant.
        /// </summary>
        /// <value>
        /// The passenger infant.
        /// </value>
        public int? PassengerInfant { get; set; }

        /// <summary>
        /// Gets or sets the luggage quantity.
        /// </summary>
        /// <value>
        /// The luggage quantity.
        /// </value>
        public int? LuggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the luggage KGS.
        /// </summary>
        /// <value>
        /// The luggage KGS.
        /// </value>
        public decimal? LuggageKgs { get; set; }

        /// <summary>
        /// Gets or sets the charge quantity.
        /// </summary>
        /// <value>
        /// The charge quantity.
        /// </value>
        public int? ChargeQuantity { get; set; }

        /// <summary>
        /// Gets or sets the charge KGS.
        /// </summary>
        /// <value>
        /// The charge KGS.
        /// </value>
        public decimal? ChargeKgs { get; set; }

        /// <summary>
        /// Gets or sets the mail quantity.
        /// </summary>
        /// <value>
        /// The mail quantity.
        /// </value>
        public int? MailQuantity { get; set; }

        /// <summary>
        /// Gets or sets the mail KGS.
        /// </summary>
        /// <value>
        /// The mail KGS.
        /// </value>
        public decimal? MailKgs { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure.
        /// </summary>
        /// <value>
        /// The manifest departure.
        /// </value>
        public virtual ManifestDeparture ManifestDeparture { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding informations.
        /// </summary>
        /// <value>
        /// The manifest departure boarding informations.
        /// </value>
        public virtual ICollection<ManifestDepartureBoardingInformation> ManifestDepartureBoardingInformations { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding details.
        /// </summary>
        /// <value>
        /// The manifest departure boarding details.
        /// </value>
        public virtual ICollection<ManifestDepartureBoardingDetail> ManifestDepartureBoardingDetails { get; set; }
    }
}
