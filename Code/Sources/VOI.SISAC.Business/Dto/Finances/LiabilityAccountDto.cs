//------------------------------------------------------------------------
// <copyright file="LiabilityAccountDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Finances
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Data Transfer Object for LiabilityAccount
    /// </summary>
    public class LiabilityAccountDto
    {
        /// <summary>
        /// Gets or sets the LiabilityAccountNumber.
        /// </summary>
        /// <value>
        /// The currency's number.
        /// </value>
        public string LiabilityAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the LiabilityAccountName.
        /// </summary>
        /// <value>
        /// The description of the LiabilityAccountName.
        /// </value>
        public string LiabilityAccountName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Currency"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}
