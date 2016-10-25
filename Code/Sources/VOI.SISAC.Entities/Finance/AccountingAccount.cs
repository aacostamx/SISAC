//------------------------------------------------------------------------
// <copyright file="AccountingAccount.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Accounting Account
    /// </summary>
    public partial class AccountingAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountingAccount"/> class.
        /// </summary>
        public AccountingAccount()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
        }

        /// <summary>
        /// Gets or sets the AccountingAccountNumber.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Accounting Account Number.
        /// </value>
        public string AccountingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the AccountingAccountName.
        /// </summary>
        /// <value>
        /// The Accounting Account Name.
        /// </value>
        public string AccountingAccountName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AccountingAccount"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContracts { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public virtual ICollection<NationalFuelContract> NationalFuelContracts { get; set; }
    }
}
