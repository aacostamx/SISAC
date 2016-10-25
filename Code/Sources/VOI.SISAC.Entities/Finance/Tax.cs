//------------------------------------------------------------------------
// <copyright file="Tax.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Get or sets Tax Properties
    /// </summary>
    public partial class Tax
    {
        /// <summary>
        /// Initializes a new instance of the Tax class.
        /// </summary>
        public Tax()
        {
            this.AirportServiceContractFederalTaxes = new HashSet<AirportServiceContract>();
            this.AirportServiceContractLocalTaxes = new HashSet<AirportServiceContract>();
            this.AirportServiceContractStateTaxes = new HashSet<AirportServiceContract>();
            this.AirportServiceContractAirportTaxes = new HashSet<AirportServiceContract>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
        }

        /// <summary>
        /// Gets or sets TaxCode PrimaryKey
        /// </summary>
        public string TaxCode { get; set; }
        
        /// <summary>
        /// Gets or sets Tax Name
        /// </summary>
        public string TaxName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the status is enabled.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts for federal taxes.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContractFederalTaxes { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContractLocalTaxes { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContractStateTaxes { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContractAirportTaxes { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public virtual ICollection<NationalFuelContract> NationalFuelContracts { get; set; }
    }
}
