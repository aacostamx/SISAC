//------------------------------------------------------------------------
// <copyright file="ManifestDepartureDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace VOI.SISAC.Business.Dto.Itineraries
{
    using System;
    using System.Collections.Generic;

    public class AdditionalDepartureInformationDto
    {
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
        /// Gets or sets the pilot.
        /// </summary>
        /// <value>
        /// The pilot.
        /// </value>
        public int Pilot { get; set; }

        /// <summary>
        /// Gets or sets the surcharge.
        /// </summary>
        /// <value>
        /// The surcharge.
        /// </value>
        public int Surcharge { get; set; }

        /// <summary>
        /// Gets or sets the extra crew.
        /// </summary>
        /// <value>
        /// The extra crew.
        /// </value>
        public int ExtraCrew { get; set; }

        /// <summary>
        /// Gets or sets the type flight.
        /// </summary>
        /// <value>
        /// The type flight.
        /// </value>
        public string TypeFlight { get; set; }

        /// <summary>
        /// Gets or sets the slot allocated time.
        /// </summary>
        /// <value>
        /// The slot allocated time.
        /// </value>
        public TimeSpan? SlotAllocatedTime { get; set; }

        /// <summary>
        /// Gets or sets the slot coordinated time.
        /// </summary>
        /// <value>
        /// The slot coordinated time.
        /// </value>
        public TimeSpan? SlotCoordinatedTime { get; set; }

        /// <summary>
        /// Gets or sets the overnight end time.
        /// </summary>
        /// <value>
        /// The overnight end time.
        /// </value>
        public TimeSpan? OvernightEndTime { get; set; }

        /// <summary>
        /// Gets or sets the maneuver start time.
        /// </summary>
        /// <value>
        /// The maneuver start time.
        /// </value>
        public TimeSpan? ManeuverStartTime { get; set; }

        /// <summary>
        /// Gets or sets the position output time.
        /// </summary>
        /// <value>
        /// The position output time.
        /// </value>
        public TimeSpan? PositionOutputTime { get; set; }

        /// <summary>
        /// Gets or sets the delay description1.
        /// </summary>
        /// <value>
        /// The delay description1.
        /// </value>
        public string DelayDescription1 { get; set; }

        /// <summary>
        /// Gets or sets the delay description2.
        /// </summary>
        /// <value>
        /// The delay description2.
        /// </value>
        public string DelayDescription2 { get; set; }

        /// <summary>
        /// Gets or sets the delay description3.
        /// </summary>
        /// <value>
        /// The delay description3.
        /// </value>
        public string DelayDescription3 { get; set; }

        /// <summary>
        /// Gets or sets the manifest departure.
        /// </summary>
        /// <value>
        /// The manifest departure.
        /// </value>
        //public ManifestDepartureDto ManifestDeparture { get; set; }
    }
}
