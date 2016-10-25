//------------------------------------------------------------------------
// <copyright file="AccountingAccountDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    /// Data Transfer Object for AccountingAccount
    /// </summary>
    public class AccountingAccountDto
    {
        /// <summary>
        /// Gets or sets the AccountingAccountNumber.
        /// </summary>
        /// <value>
        /// The currency's number.
        /// </value>
        public string AccountingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the AccountingAccountName.
        /// </summary>
        /// <value>
        /// The description of the AccountingAccountName.
        /// </value>
        public string AccountingAccountName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Currency"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}
