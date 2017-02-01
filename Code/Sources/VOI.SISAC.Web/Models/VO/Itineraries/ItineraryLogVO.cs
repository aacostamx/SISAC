//------------------------------------------------------------------------
// <copyright file="ItineraryVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.Attributes;

    public class ItineraryLogVO
    {
        /// <summary>
        /// CarrierCode = Airline Code
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Flight Number of the Airplane
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Primary Key = Departure Date + CarrierCode + Flight Number + DepartureStation + DepartureArrival
        /// </summary>
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets Equipment Number
        /// </summary>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets Departure Date
        /// </summary>
        public DateTime? DepartureDate { get; set; }

        /// <summary>
        /// Gets or sets Departure Airport
        /// </summary>
        public string DepartureStation { get; set; }

        /// <summary>
        /// Gets or sets Arrival Date
        /// </summary>
        public DateTime? ArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets Arrival Airport
        /// </summary>
        public string ArrivalStation { get; set; }

        /// <summary>
        /// End date of the movement in the itinerary
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Comment for the movement done
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// In base the itinerary extracts the information.
        /// </summary>
        public virtual ItineraryVO itinerary { get; set; }
    }
}