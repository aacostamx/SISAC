//------------------------------------------------------------------------
// <copyright file="AirplaneWeightMeasureTypeVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    /// <summary>
    /// Class AirplaneWeightMeasure Type VO
    /// </summary>
    public class AirplaneWeightMeasureTypeVO
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