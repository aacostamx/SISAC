//------------------------------------------------------------------------
// <copyright file="PageReportDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Data Transfer Object for PageReport
    /// </summary>
    public class PageReportDto
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
