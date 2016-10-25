//------------------------------------------------------------------------
// <copyright file="TaxDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Finances
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entities.Finance;

    /// <summary>
    /// Data Transfer Object for Tax
    /// </summary>
    public class TaxDto
    {
        /// <summary>
        /// Gets or sets the Tax number.
        /// </summary>
        /// <value>
        /// The currency's number.
        /// </value>
        public string TaxCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the Tax.
        /// </summary>
        /// <value>
        /// The desciption of the Tax.
        /// </value>
        public string TaxName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Tax"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}
