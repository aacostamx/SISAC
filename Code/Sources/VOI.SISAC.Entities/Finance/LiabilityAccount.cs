//------------------------------------------------------------------------
// <copyright file="LiabilityAccount.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Liability Account
    /// </summary>
    public partial class LiabilityAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiabilityAccount"/> class.
        /// </summary>
        public LiabilityAccount()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
        }

        /// <summary>
        /// Gets or sets the LiabilityAccountNumber.
        /// </summary>
        /// <value>
        /// The LiabilityAccountNumber.
        /// </value>
        public string LiabilityAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the LiabilityAccountName.
        /// </summary>
        /// <value>
        /// The LiabilityAccountName.
        /// </value>
        public string LiabilityAccountName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LiabilityAccount"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airports service contracts.
        /// </summary>
        /// <value>
        /// The airports service contracts.
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
