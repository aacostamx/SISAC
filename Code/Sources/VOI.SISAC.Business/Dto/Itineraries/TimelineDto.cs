//------------------------------------------------------------------------
// <copyright file="TimelineDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// TimelineDto class
    /// </summary>
    public class TimelineDto
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineDto"/> class.
        /// </summary>
        public TimelineDto()
        {
            TimelineMovements = new List<TimelineMovementDto>();
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
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public ItineraryDto Itinerary { get; set; }

        /// <summary>
        /// Gets or sets the timeline movements.
        /// </summary>
        /// <value>
        /// The timeline movements.
        /// </value>
        public IList<TimelineMovementDto> TimelineMovements { get; set; }
    }
}
