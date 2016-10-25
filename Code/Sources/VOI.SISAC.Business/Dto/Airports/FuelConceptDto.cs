//------------------------------------------------------------------------
// <copyright file="FuelConceptDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Data Transfer Object for FuelConcept
    /// </summary>
    public class FuelConceptDto
    {
        /// <summary>
        /// Gets or sets the Fuel Concept.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Fuel Concept.
        /// </value>
        public long FuelConceptID { get; set; }

        /// <summary>
        /// Gets or sets the name of the FuelConceptName.
        /// </summary>
        /// <value>
        /// The description of the FuelConceptName.
        /// </value>
        public string FuelConceptName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FuelConcept"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}
