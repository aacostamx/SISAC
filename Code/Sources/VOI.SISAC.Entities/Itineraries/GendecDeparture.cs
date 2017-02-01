//------------------------------------------------------------------------
// <copyright file="GendecDeparture.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Itineraries
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Airport;

    /// <summary>
    /// Gendec Departure Entity
    /// </summary>
    [Table("Itinerary.GendecDeparture")]
    public partial class GendecDeparture
    {
        /// <summary>
        /// Sequence of Flight
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Airline Code of the Flight
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Flight Number
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Date of Flight
        /// </summary>
        public string Itinerarykey { get; set; }

        /// <summary>
        /// Total Pax
        /// </summary>
        public int TotalPax { get; set; }

        /// <summary>
        /// Total number of the crew members
        /// </summary>
        public int TotalCrew { get; set; }

        /// <summary>
        /// Block Time
        /// </summary>
        public TimeSpan? BlockTime { get; set; }

        /// <summary>
        /// Manifest Number
        /// </summary>
        public string ManifestNumber { get; set; }

        /// <summary>
        /// Gate Number
        /// </summary>
        public string GateNumber { get; set; }

        /// <summary>
        /// Departure Date
        /// </summary>
        public TimeSpan ActualTimeOfDeparture { get; set; }

        /// <summary>
        /// Airport departure
        /// </summary>
        public string DeparturePlace { get; set; }

        /// <summary>
        /// Time of landing
        /// </summary>
        public TimeSpan? Embarking { get; set; }

        /// <summary>
        /// Flight Departure Description
        /// </summary>
        public string FlightDepartureDescription { get; set; }

        /// <summary>
        /// Crew Member
        /// </summary>
        public int Member { get; set; }

        /// <summary>
        /// Authorized Agent
        /// </summary>
        public string AuthorizedAgent { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Statof of the Gendec
        /// </summary>
        public bool Closed { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public virtual Itinerary Itinerary { get; set; }

        /// <summary>
        /// Crew Collection
        /// </summary>
        public virtual ICollection<Crew> Crews { get; set; }
    }
}