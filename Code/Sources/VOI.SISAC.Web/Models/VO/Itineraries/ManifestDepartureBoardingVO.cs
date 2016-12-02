//------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ManifestDepartureBoardingVO
    /// </summary>
    public class ManifestDepartureBoardingVO
    {
        /// <summary>
        /// Gets or sets the boarding identifier.
        /// </summary>
        /// <value>
        /// The boarding identifier.
        /// </value>
        public long BoardingID { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        [Display(Name = "Sequence", ResourceType = typeof(Resources.Resource))]
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>        
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        [Display(Name = "FlightNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        [Display(Name = "ItineraryKey", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        [Display(Name = "Position", ResourceType = typeof(Resources.Resource))]
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the station.
        /// </summary>
        /// <value>
        /// The station.
        /// </value>
        [Display(Name = "Airport", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string Station { get; set; }

        /// <summary>
        /// Gets or sets the passenger adult.
        /// </summary>
        /// <value>
        /// The passenger adult.
        /// </value>
        [Display(Name = "Adults", ResourceType = typeof(Resources.Resource))]
        public int? PassengerAdult { get; set; }

        /// <summary>
        /// Gets or sets the passenger minor.
        /// </summary>
        /// <value>
        /// The passenger minor.
        /// </value>
        [Display(Name = "Minor", ResourceType = typeof(Resources.Resource))]
        public int? PassengerMinor { get; set; }

        /// <summary>
        /// Gets or sets the passenger infant.
        /// </summary>
        /// <value>
        /// The passenger infant.
        /// </value>
        [Display(Name = "Infant", ResourceType = typeof(Resources.Resource))]
        public int? PassengerInfant { get; set; }

        /// <summary>
        /// Gets or sets the luggage quantity.
        /// </summary>
        /// <value>
        /// The luggage quantity.
        /// </value>
        [Display(Name = "LuggageQuantity", ResourceType = typeof(Resources.Resource))]
        public int? LuggageQuantity { get; set; }

        /// <summary>
        /// Gets or sets the luggage KGS.
        /// </summary>
        /// <value>
        /// The luggage KGS.
        /// </value>
        [Display(Name = "LuggageKgs", ResourceType = typeof(Resources.Resource))]
        public decimal? LuggageKgs { get; set; }

        /// <summary>
        /// Gets or sets the charge quantity.
        /// </summary>
        /// <value>
        /// The charge quantity.
        /// </value>
        [Display(Name = "ChargeQuantity", ResourceType = typeof(Resources.Resource))]
        public int? ChargeQuantity { get; set; }

        /// <summary>
        /// Gets or sets the charge KGS.
        /// </summary>
        /// <value>
        /// The charge KGS.
        /// </value>
        [Display(Name = "ChargeKgs", ResourceType = typeof(Resources.Resource))]
        public decimal? ChargeKgs { get; set; }

        /// <summary>
        /// Gets or sets the mail quantity.
        /// </summary>
        /// <value>
        /// The mail quantity.
        /// </value>
        [Display(Name = "MailQuantity", ResourceType = typeof(Resources.Resource))]
        public int? MailQuantity { get; set; }

        /// <summary>
        /// Gets or sets the mail KGS.
        /// </summary>
        /// <value>
        /// The mail KGS.
        /// </value>
        [Display(Name = "MailKgs", ResourceType = typeof(Resources.Resource))]
        public decimal? MailKgs { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure.
        /// </summary>
        /// <value>
        /// The manifest departure.
        /// </value>
        public ManifestDepartureVO ManifestDeparture { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding details.
        /// </summary>
        /// <value>
        /// The manifest departure boarding details.
        /// </value>
        public ICollection<ManifestDepartureBoardingDetailVO> ManifestDepartureBoardingDetails { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding informations.
        /// </summary>
        /// <value>
        /// The manifest departure boarding informations.
        /// </value>
        public ICollection<ManifestDepartureBoardingInformationVO> ManifestDepartureBoardingInformations { get; set; }
    }
}