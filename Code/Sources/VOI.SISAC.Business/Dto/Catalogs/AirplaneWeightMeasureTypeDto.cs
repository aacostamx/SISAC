//------------------------------------------------------------------------
// <copyright file="AirplaneWeightMeasureTypeDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    /// <summary>
    /// Class AirplaneWeightMeasureTypeDto
    /// </summary>
    public class AirplaneWeightMeasureTypeDto
    {
        /// <summary>
        /// Gets or sets the airplane weight measure identifier.
        /// </summary>
        /// <value>
        /// The airplane weight measure identifier.
        /// </value>
        public int AirplaneWeightMeasureId { get; set; }

        /// <summary>
        /// Gets or sets the name of the airplane weight measure.
        /// </summary>
        /// <value>
        /// The name of the airplane weight measure.
        /// </value>
        public string AirplaneWeightMeasureName { get; set; }
    }
}
