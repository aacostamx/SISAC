//------------------------------------------------------------------------
// <copyright file="ItineraryFile.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Services.Models.Files
{
    using System;

    /// <summary>
    /// ItineraryFile.cs
    /// </summary>
    public class ItineraryFile
    {
        /// <summary>
        /// Text File Line
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Flight number
        /// </summary>
        public string FLTNUM { get; set; }

        /// <summary>
        /// Aircraft Registration 
        /// </summary>
        public string ACREGNUMBER { get; set; }

        /// <summary>
        /// Filght Origin Date Local Time
        /// </summary>
        public string FLTORGDATELT { get; set; }

        /// <summary>
        /// Departure Station
        /// </summary>
        public string DEP { get; set; }

        /// <summary>
        /// Destination Station or Arrival Station
        /// </summary>
        public string DST { get; set; }

        /// <summary>
        /// Scheduled Destination or Arrival
        /// </summary>
        public string SKDDST { get; set; }

        /// <summary>
        /// Scheduled Time Departure Local Time
        /// </summary>
        public string STDLT { get; set; }

        /// <summary>
        /// Scheduled Time Arrival Local Time
        /// </summary>
        public string STALT { get; set; }

        /// <summary>
        /// Gets or sets the flttype.
        /// </summary>
        /// <value>
        /// The flttype.
        /// </value>
        public string FLTTYPE { get; set; }

        /// <summary>
        /// Airline Code
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Departure Date
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Arrival Date
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Itinerary Key - Only Date From DepartureDate YYYYMMdd - Example: 20151214
        /// </summary>
        public string ItineraryKey { get; set; }
    }
}