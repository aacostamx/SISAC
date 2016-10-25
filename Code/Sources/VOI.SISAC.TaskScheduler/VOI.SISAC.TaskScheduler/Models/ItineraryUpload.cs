//------------------------------------------------------------------------
// <copyright file="ItineraryUpload.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------


namespace VOI.SISAC.TaskScheduler.Models
{
    using System;
    using System.Collections.Generic;

    public class ItineraryUpload
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
