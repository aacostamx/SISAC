//------------------------------------------------------------------------
// <copyright file="ItineraryMailDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace VOI.SISAC.Business.Dto.Itineraries
{
    /// <summary>
    /// ItineraryMailDto class
    /// </summary>
    public class ItineraryMailDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItineraryMailDto"/> class.
        /// </summary>
        public ItineraryMailDto()
        {
            ItineraryGroups = new List<ItineraryGroupMailDto>();
        }

        /// <summary>
        /// Gets or sets the number lines.
        /// </summary>
        /// <value>
        /// The number lines.
        /// </value>
        public int numberLines { get; set; }

        /// <summary>
        /// Gets or sets the flights file.
        /// </summary>
        /// <value>
        /// The flights file.
        /// </value>
        public int flightsFile { get; set; }

        /// <summary>
        /// Gets or sets the flights processed.
        /// </summary>
        /// <value>
        /// The flights processed.
        /// </value>
        public int flightsProcessed { get; set; }

        /// <summary>
        /// Gets or sets the total errors.
        /// </summary>
        /// <value>
        /// The total errors.
        /// </value>
        public int TotalErrors { get; set; }

        /// <summary>
        /// Gets or sets the itinerary groups.
        /// </summary>
        /// <value>
        /// The itinerary groups.
        /// </value>
        public List<ItineraryGroupMailDto> ItineraryGroups { get; set; }
    }

    /// <summary>
    /// ItineraryGroupMailDto class
    /// </summary>
    public class ItineraryGroupMailDto
    {
        /// <summary>
        /// Gets or sets the itinerary date.
        /// </summary>
        /// <value>
        /// The itinerary date.
        /// </value>
        public DateTime ItineraryDate { get; set; }

        /// <summary>
        /// Gets or sets the total group flights.
        /// </summary>
        /// <value>
        /// The total group flights.
        /// </value>
        public int TotalGroupFlights { get; set; }

        /// <summary>
        /// Gets or sets the total group.
        /// </summary>
        /// <value>
        /// The total group.
        /// </value>
        public int TotalGroupProcess { get; set; }

        /// <summary>
        /// Gets or sets the total group errors.
        /// </summary>
        /// <value>
        /// The total group errors.
        /// </value>
        public int TotalGroupErrors { get; set; }
    }
}
