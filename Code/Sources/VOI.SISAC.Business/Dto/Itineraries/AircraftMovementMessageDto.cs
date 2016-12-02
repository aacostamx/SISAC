//------------------------------------------------------------------------
// <copyright file="AircraftMovementMessageDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Itineraries
{
    using System.Collections.Generic;

    /// <summary>
    /// Aircraft movement message structure
    /// </summary>
    public class AircraftMovementMessageDto
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the departure information.
        /// </summary>
        /// <value>
        /// The departure information.
        /// </value>
        public string DepartureInformation { get; set; }

        /// <summary>
        /// Gets or sets the arrival information.
        /// </summary>
        /// <value>
        /// The arrival information.
        /// </value>
        public string ArrivalInformation { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel information.
        /// </summary>
        /// <value>
        /// The jet fuel information.
        /// </value>
        public string JetFuelInformation { get; set; }

        /// <summary>
        /// Gets or sets the delays information.
        /// </summary>
        /// <value>
        /// The delays information.
        /// </value>
        public IList<string> DelaysInformation { get; set; }

        /// <summary>
        /// Gets or sets the captains information.
        /// </summary>
        /// <value>
        /// The captains information.
        /// </value>
        public string CaptainsInformation { get; set; }

        /// <summary>
        /// Gets or sets the stewardess information.
        /// </summary>
        /// <value>
        /// The stewardess information.
        /// </value>
        public string StewardessInformation { get; set; }

        /// <summary>
        /// Gets or sets the charge information title.
        /// </summary>
        /// <value>
        /// The charge information title.
        /// </value>
        public string ChargeInformationTitle { get; set; }

        /// <summary>
        /// Gets or sets the charge information.
        /// </summary>
        /// <value>
        /// The charge information.
        /// </value>
        public IList<string> ChargeInformation { get; set; }
    }
}
