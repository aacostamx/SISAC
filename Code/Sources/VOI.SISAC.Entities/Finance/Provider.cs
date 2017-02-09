//------------------------------------------------------------------------
// <copyright file="Provider.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using Itineraries;
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Provider Class Definition
    /// </summary>
    public partial class Provider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Provider"/> class.
        /// </summary>
        public Provider()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
            this.InternationalFuelContractConcepts = new HashSet<InternationalFuelContractConcept>();
            this.InternationalFuelContracts = new HashSet<InternationalFuelContract>();
            this.JetFuelTicketIntoPlanes = new HashSet<JetFuelTicket>();
            this.JetFuelTicketJetFuels = new HashSet<JetFuelTicket>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
            this.NationalFuelContractConcepts = new HashSet<NationalFuelContractConcept>();
            this.NationalFuelRates = new HashSet<NationalFuelRate>();
            this.TimelineMovements = new List<TimelineMovement>();
        }

        /// <summary>
        /// Gets or sets the Provider Number
        /// </summary>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets Provider Name
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets Provider Short Name
        /// </summary>
        public string ProviderShortName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the status is enabled.
        /// </summary>
        public bool Status { get; set; }        

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContracts { get; set; }

        /// <summary>
        /// Gets or sets the International Fuel Contracts.
        /// </summary>
        /// <value>
        /// The International Fuel Contracts.
        /// </value>    
        public virtual ICollection<InternationalFuelContract> InternationalFuelContracts { get; set; }

        /// <summary>
        /// Gets or sets a list of International Fuel Contract Concepts
        /// </summary>
        public virtual ICollection<InternationalFuelContractConcept> InternationalFuelContractConcepts { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel ticket jet fuels.
        /// </summary>
        /// <value>
        /// The jet fuel ticket jet fuels.
        /// </value>
        public virtual ICollection<JetFuelTicket> JetFuelTicketJetFuels { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel ticket into planes.
        /// </summary>
        /// <value>
        /// The jet fuel ticket into planes.
        /// </value>
        public virtual ICollection<JetFuelTicket> JetFuelTicketIntoPlanes { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public virtual ICollection<NationalFuelContract> NationalFuelContracts { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contract concepts.
        /// </summary>
        /// <value>
        /// The national fuel contract concepts.
        /// </value>
        public virtual ICollection<NationalFuelContractConcept> NationalFuelContractConcepts { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel providers.
        /// </summary>
        /// <value>
        /// The national jet fuel providers.
        /// </value>
        public virtual ICollection<NationalJetFuelTicket> NationalJetFuelProviders { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel into planes.
        /// </summary>
        /// <value>
        /// The national jet fuel into planes.
        /// </value>
        public virtual ICollection<NationalJetFuelTicket> NationalJetFuelIntoPlanes { get; set; }

        /// <summary>
        /// Gets or sets the national fuel rates.
        /// </summary>
        /// <value>
        /// The national fuel rates.
        /// </value>
        public virtual ICollection<NationalFuelRate> NationalFuelRates { get; set; }

        /// <summary>
        /// Gets or sets the timeline movement.
        /// </summary>
        /// <value>
        /// The timeline movement.
        /// </value>
        public virtual IList<TimelineMovement> TimelineMovements { get; set; }
    }
}