//------------------------------------------------------------------------
// <copyright file="PageTicket.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Security
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// AccountingAccount
    /// </summary>
    public partial class PageReport
    {

        // <summary>
        /// Gets or sets the PageName.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The PageName.
        /// </value>
        public string PageName { get; set; }

        // <summary>
        /// Gets or sets the PathReport.
        /// </summary>
        /// <value>
        /// The PathReport.
        /// </value>
        public string PathReport { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccountingAccount"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}
