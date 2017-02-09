//------------------------------------------------------------------------
// <copyright file="ItineraryUploadDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Itineraries
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ItineraryUploadDto class
    /// </summary>
    public class ItineraryUploadDto
    {
        /// <summary>
        /// Gets or sets the lines.
        /// </summary>
        /// <value>
        /// The lines.
        /// </value>
        public List<string> lines { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// AirlineCodeCombobox
        /// </summary>
        public string AirlineCodeCombobox { get; set; }
    }
}
