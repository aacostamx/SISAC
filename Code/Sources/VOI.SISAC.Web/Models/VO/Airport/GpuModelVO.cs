//------------------------------------------------------------------------
// <copyright file="GpuModelVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.Collections.Generic;

    /// <summary>
    /// GpuModel class
    /// </summary>
    public class GpuModelVO
    {
        /// <summary>
        /// Gpu
        /// </summary>
        public GpuVO Gpu { get; set; }
        /// <summary>
        /// Airports
        /// </summary>
        public IList<AirportVO> Airports { get; set; }
    }
}