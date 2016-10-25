//------------------------------------------------------------------------
// <copyright file="GpuDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    /// <summary>
    /// GpuDto
    /// </summary>
    public class GpuDto
    {
        /// <summary>
        /// Gpu Code
        /// </summary>
        public string GpuCode { get; set; }

        /// <summary>
        /// Gpu Name
        /// </summary>
        public string GpuName { get; set; }

        /// <summary>
        /// Station Code
        /// </summary>
        public string StationCode { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Airport
        /// </summary>
        public AirportDto Airport { get; set; }
    }
}
