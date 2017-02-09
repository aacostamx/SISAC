//------------------------------------------------------------------------
// <copyright file="ManifestTimeConfigModelVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.Collections.Generic;
    /// <summary>
    /// ManifestTimeConfigModelVO
    /// </summary>
    public class ManifestTimeConfigModelVO
    {
        /// <summary>
        /// Manifest VO
        /// </summary>
        public ManifestTimeConfigVO ManifestVO { get; set; }

        /// <summary>
        /// Airline VO
        /// </summary>
        public IList<AirlineVO> AirlineVO { get; set; }
    }
}