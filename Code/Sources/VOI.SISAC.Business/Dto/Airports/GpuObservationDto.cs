//------------------------------------------------------------------------
// <copyright file="GpuObservationDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    /// <summary>
    /// Gpu Observation
    /// </summary>
    public class GpuObservationDto
    {
        /// <summary>
        /// Gpu Observation Code
        /// </summary>
        public string GpuObservationCode { get; set; }

        /// <summary>
        /// Gpu Observation Name
        /// </summary>
        public string GpuObservationCodeName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }
    }
}
