//------------------------------------------------------------------------
// <copyright file="MovementTypeDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Movement TypeDto Class
    /// </summary>
    public class MovementTypeDto
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
        /// Gets or sets a value indicating whether this <see cref="MovementTypeDto"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}
