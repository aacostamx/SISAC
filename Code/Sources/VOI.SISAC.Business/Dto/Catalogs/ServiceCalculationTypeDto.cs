//------------------------------------------------------------------------
// <copyright file="ServiceCalculationTypeDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    /// <summary>
    /// Class ServiceCalculationTypeDto
    /// </summary>
    public class ServiceCalculationTypeDto
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
