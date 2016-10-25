//------------------------------------------------------------------------
// <copyright file="ItineraryUploadVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// ItineraryUploadVO
    /// </summary>
    public class ItineraryUploadVO
    {
        /// <summary>
        /// HttpPostedFileBase File
        /// </summary>
        public HttpPostedFileBase file { get; set; }

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