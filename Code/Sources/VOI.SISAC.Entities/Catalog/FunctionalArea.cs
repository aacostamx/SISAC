//------------------------------------------------------------------------
// <copyright file="FunctionalArea.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace VOI.SISAC.Entities.Catalog
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class FunctionalArea
    /// </summary>
    public partial class FunctionalArea
    {
        /// <summary>
        /// Gets or sets the Functional Area ID.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Functional Area ID.
        /// </value>
        public long FunctionalAreaID { get; set; }
        
        /// <summary>
        /// Gets or sets the FunctionalAreaName.
        /// </summary>
        /// <value>
        /// The FunctionalAreaName.
        /// </value>
        public string FunctionalAreaName { get; set; }
        
        /// <summary>
        /// Gets or sets the FunctionalAreaDescription.
        /// </summary>
        /// <value>
        /// The Functional Area Description.
        /// </value>
        public string FunctionalAreaDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FunctionalArea"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}
