//------------------------------------------------------------------------
// <copyright file="GendecArrivalDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace VOI.SISAC.Business.Dto.Itineraries
{
    using System;
    using System.Collections.Generic;
    using Airports;
    using Catalogs;
    using VOI.SISAC.Entities.Itineraries;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Gendec Arrvial Data Object
    /// </summary>
    public class GendecArrivalDto
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
        public TimeSpan ActualTimeOfArrival { get; set; }

        /// <summary>
        /// Airport departure
        /// </summary>
        public string ArrivalPlace { get; set; }

        /// <summary>
        /// Time of landing
        /// </summary>
        public TimeSpan? Disembanking { get; set; }

        /// <summary>
        /// Flight Departure Description
        /// </summary>
        public string FlightArrivalDescription { get; set; }

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
        /// 
        /// </summary>
        public ItineraryDto Itinerary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<CrewDto> Crews { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CrewDto Crew { get; set; }
    }
}
