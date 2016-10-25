//------------------------------------------------------------------------
// <copyright file="Service.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Catalog;
    using VOI.SISAC.Entities.Finance;
    
    /// <summary>
    /// Service class definition
    /// </summary>
    public partial class Service
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
            this.JetFuelTickets = new HashSet<JetFuelTicket>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
            this.NationalFuelRates = new HashSet<NationalFuelRate>();
        }

        /// <summary>
        /// Gets or sets the name of the Service Code
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the Service Name
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is enabled.
        /// </summary>
        public bool Status
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContracts { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel tickets.
        /// </summary>
        /// <value>
        /// The jet fuel tickets.
        /// </value>
        public virtual ICollection<JetFuelTicket> JetFuelTickets { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public virtual ICollection<NationalFuelContract> NationalFuelContracts { get; set; }

        /// <summary>
        /// Gets or sets the national fuel rates.
        /// </summary>
        /// <value>
        /// The national fuel rates.
        /// </value>
        public virtual ICollection<NationalFuelRate> NationalFuelRates { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public virtual ICollection<ReconciliationTolerance> ReconciliationTolerances { get; set; }
    }
}
