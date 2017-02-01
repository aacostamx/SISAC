//------------------------------------------------------------------------
// <copyright file="AirportDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using Catalogs;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// AirportDto class
    /// </summary>
    public class AirportDto
    {
        /// <summary>
        /// Station code
        /// </summary>
        public string StationCode { get; set; }
        /// <summary>
        /// Station Name
        /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// Country Code
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// Opening Time
        /// </summary>
        public TimeSpan? OpeningTime { get; set; }
        /// <summary>
        /// Closing Time
        /// </summary>
        public TimeSpan? ClosingTime { get; set; }
        /// <summary>
        /// Airport Group
        /// </summary>
        public string AirportGroupCode { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public CountryDto Country { get; set; }
        /// <summary>
        /// Gpu collection
        /// </summary>
        public ICollection<GpuDto> Gpu { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public AirportDto()
        {
            Gpu = new List<GpuDto>();
        }
    }
}
