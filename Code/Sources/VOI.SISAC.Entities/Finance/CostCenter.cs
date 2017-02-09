//------------------------------------------------------------------------
// <copyright file="CostCenter.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class CostCenter
    /// </summary>
    public partial class CostCenter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CostCenter"/> class.
        /// </summary>
        public CostCenter()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
            this.InternationalFuelContract = new HashSet<InternationalFuelContract>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
        }

        /// <summary>
        /// Gets or sets the cc number.
        /// </summary>
        /// <value>
        /// The cc number.
        /// </value>
        public string CCNumber { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the cc.
        /// </summary>
        /// <value>
        /// The name of the cc.
        /// </value>
        public string CCName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CostCenter"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airlines.
        /// </summary>
        /// <value>
        /// The airlines.
        /// </value>
        public virtual Airline Airline { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContracts { get; set; }

        /// <summary>
        /// Gets or sets International Fuel Contract
        /// </summary>
        public virtual ICollection<InternationalFuelContract> InternationalFuelContract { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public virtual ICollection<NationalFuelContract> NationalFuelContracts { get; set; }
    }
}