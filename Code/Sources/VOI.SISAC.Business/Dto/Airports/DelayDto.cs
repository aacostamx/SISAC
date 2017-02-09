//------------------------------------------------------------------------
// <copyright file="DelayDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using Catalogs;

    /// <summary>
    /// DelayDto Class
    /// </summary>
    public class DelayDto
    {
        /// <summary>
        /// Delay Code
        /// </summary>
        public string DelayCode { get; set; }

        /// <summary>
        /// Delay Name
        /// </summary>
        public string DelayName { get; set; }

        /// <summary>
        /// Functional Area
        /// </summary>
        public long FunctionalAreaID { get; set; }

        /// <summary>
        /// Under Control
        /// </summary>
        public bool UnderControl { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// FunctionalArea
        /// </summary>
        public virtual FunctionalAreaDto FunctionalArea { get; set; }

    }
}
