//------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Itineraries
{
    using System.Collections.Generic;

    /// <summary>
    /// ManifestDepartureBoardingDto
    /// </summary>
    public class ManifestDepartureBoardingDto
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
        public ManifestDepartureDto ManifestDeparture { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding information dtos.
        /// </summary>
        /// <value>
        /// The manifest departure boarding information dtos.
        /// </value>
        public ICollection<ManifestDepartureBoardingInformationDto> ManifestDepartureBoardingInformationDtos { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure boarding detail dtos.
        /// </summary>
        /// <value>
        /// The manifest departure boarding detail dtos.
        /// </value>
        public ICollection<ManifestDepartureBoardingDetailDto> ManifestDepartureBoardingDetailDtos { get; set; }
    }
}
