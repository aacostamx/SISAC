//------------------------------------------------------------------------
// <copyright file="ItineraryUploadVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.WinServices.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class ItineraryUploadVO
    /// </summary>
    public class ItineraryUploadVO
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
