//------------------------------------------------------------------------
// <copyright file="MovementTypeVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Movement Type View Object
    /// </summary>
    public class MovementTypeVO
    {
        /// <summary>
        /// Gets or sets the movement type code.
        /// </summary>
        /// <value>
        /// The movement type code.
        /// </value>
        public string MovementTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the movement description.
        /// </summary>
        /// <value>
        /// The movement description.
        /// </value>
        public string MovementDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MovementTypeVO"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}