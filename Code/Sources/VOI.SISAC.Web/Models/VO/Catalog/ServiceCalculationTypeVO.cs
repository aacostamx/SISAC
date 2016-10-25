//------------------------------------------------------------------------
// <copyright file="ServiceCalculationTypeVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    /// <summary>
    /// Class Service Calculation Type VO
    /// </summary>
    public class ServiceCalculationTypeVO
    {
        /// <summary>
        /// Gets or sets the calculation type identifier.
        /// </summary>
        /// <value>
        /// The calculation type identifier.
        /// </value>
        public int CalculationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the calculation type.
        /// </summary>
        /// <value>
        /// The name of the calculation type.
        /// </value>
        public string CalculationTypeName { get; set; }
    }
}