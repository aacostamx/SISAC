//------------------------------------------------------------------------
// <copyright file="TimelineVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Timeline View Object
    /// </summary>
    public class TimelineVO
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineVO"/> class.
        /// </summary>
        public TimelineVO()
        {
            Itinerary = new ItineraryVO();
            TimelineMovements = new List<TimelineMovementVO>();
        }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        public long? Row { get; set; }

        /// <summary>
        /// Gets or sets the maximum row.
        /// </summary>
        /// <value>
        /// The maximum row.
        /// </value>
        public long? MaxRow { get; set; }

        /// <summary>
        /// Gets or sets the minimum row.
        /// </summary>
        /// <value>
        /// The minimum row.
        /// </value>
        public long? MinRow { get; set; }

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
        /// Gets or sets the station code.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the previous sequence.
        /// </summary>
        /// <value>
        /// The previous sequence.
        /// </value>
        public int? PreviousSequence { get; set; }

        /// <summary>
        /// Gets or sets the previous airline code.
        /// </summary>
        /// <value>
        /// The previous airline code.
        /// </value>
        public string PreviousAirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the previous flight number.
        /// </summary>
        /// <value>
        /// The previous flight number.
        /// </value>
        public string PreviousFlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the previous itinerary key.
        /// </summary>
        /// <value>
        /// The previous itinerary key.
        /// </value>
        public string PreviousItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the next sequence.
        /// </summary>
        /// <value>
        /// The next sequence.
        /// </value>
        public int? NextSequence { get; set; }

        /// <summary>
        /// Gets or sets the next airline code.
        /// </summary>
        /// <value>
        /// The next airline code.
        /// </value>
        public string NextAirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the next flight number.
        /// </summary>
        /// <value>
        /// The next flight number.
        /// </value>
        public string NextFlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the next itinerary key.
        /// </summary>
        /// <value>
        /// The next itinerary key.
        /// </value>
        public string NextItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [special case].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [special case]; otherwise, <c>false</c>.
        /// </value>
        public bool SpecialCase { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [next timeline].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [next timeline]; otherwise, <c>false</c>.
        /// </value>
        public bool NextTimeline { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [last timeline].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [last timeline]; otherwise, <c>false</c>.
        /// </value>
        public bool LastTimeline { get; set; }

        /// <summary>
        /// Gets or sets the current timeline.
        /// </summary>
        /// <value>
        /// The current timeline.
        /// </value>
        public bool CurrentTimeline { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public ItineraryVO Itinerary { get; set; }

        /// <summary>
        /// Gets or sets the timeline movement.
        /// </summary>
        /// <value>
        /// The timeline movement.
        /// </value>
        public IList<TimelineMovementVO> TimelineMovements { get; set; }
    }
}