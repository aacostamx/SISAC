//------------------------------------------------------------------------
// <copyright file="ItinerarySearchVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;

    /// <summary>
    /// ItinerarySearchVO class
    /// </summary>
    public class ItinerarySearchVO
    {
        /// <summary>
        /// Page size
        /// </summary>
        public int Pagesize { get; set; }

        /// <summary>
        /// Page number
        /// </summary>
        public int Pagenumber { get; set; }

        /// <summary>
        /// Airline Code
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Flight Number
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Equipment Number
        /// </summary>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Departure Station
        /// </summary>
        public string DepartureStation { get; set; }

        /// <summary>
        /// Arrival Station
        /// </summary>
        public string ArrivalStation { get; set; }

        /// <summary>
        /// Localization Station
        /// </summary>
        public string LocalizationStation { get; set; }

        /// <summary>
        /// Start Departure Date
        /// </summary>
        public DateTime? DepartureDate { get; set; }

        /// <summary>
        /// End Departure Date
        /// </summary>
        public DateTime? ArrivalDate { get; set; }
    }
}