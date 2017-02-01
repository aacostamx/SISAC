//------------------------------------------------------------------------
// <copyright file="FunctionalAreaDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Data Transfer Object for FunctionalArea
    /// </summary>
    public class FunctionalAreaDto
    {
        /// <summary>
        /// Gets or sets the FunctionalAreaID.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The FunctionalAreaID.
        /// </value>
        public long FunctionalAreaID { get; set; }

        /// <summary>
        /// Gets or sets the name of the FunctionalAreaName.
        /// </summary>
        /// <value>
        /// The description of the FunctionalAreaName.
        /// </value>
        public string FunctionalAreaName { get; set; }

        /// <summary>
        /// Gets or sets the name of the FunctionalAreaDescription.
        /// </summary>
        /// <value>
        /// The description of the FunctionalAreaDescription.
        /// </value>
        public string FunctionalAreaDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FunctionalArea"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}
