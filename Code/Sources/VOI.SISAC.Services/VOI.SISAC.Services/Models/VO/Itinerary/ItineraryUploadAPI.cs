//------------------------------------------------------------------------
// <copyright file="ItineraryUploadVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Services.Models.VO.Itinerary
{
    using Files;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ItineraryUploadVO
    /// </summary>
    public class ItineraryUploadAPI
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItineraryUploadAPI"/> class.
        /// </summary>
        public ItineraryUploadAPI()
        {
            StartDate = new DateTime();
            EndDate = new DateTime();
            itineraries = new List<ItineraryFile>();
            readServerFile = true;
            email = true;
            errors = new List<string>();
            sucess = false;
        }

        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the itineraries.
        /// </summary>
        /// <value>
        /// The itineraries.
        /// </value>
        public List<ItineraryFile> itineraries { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [read server file].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [read server file]; otherwise, <c>false</c>.
        /// </value>
        public bool readServerFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [send mail].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [send mail]; otherwise, <c>false</c>.
        /// </value>
        public bool email { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<string> errors { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ItineraryUploadAPI"/> is sucess.
        /// </summary>
        /// <value>
        ///   <c>true</c> if sucess; otherwise, <c>false</c>.
        /// </value>
        public bool sucess { get; set; }
    }
}