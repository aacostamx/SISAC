//------------------------------------------------------------------------
// <copyright file="ManifestTimeConfigDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace VOI.SISAC.Business.Dto.Airports
{
    using Entities.Airport;

    /// <summary>
    /// ManifestTimeConfigDto class
    /// </summary>
    public class ManifestTimeConfigDto
    {
        /// <summary>
        /// Manifest Time
        /// </summary>
        public long ManifestTimeConfigID { get; set; }

        /// <summary>
        /// Airline Code
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Arrive
        /// </summary>
        public decimal ArrivalMinutesMin { get; set; }

        /// <summary>
        /// Arrive
        /// </summary>
        public decimal ArrivalMinutesMax { get; set; }

        /// <summary>
        /// Departure
        /// </summary>
        public decimal DepartureMinutesMin { get; set; }

        /// <summary>
        /// Departure
        /// </summary>
        public decimal DepartureMinutesMax { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

    }
}
