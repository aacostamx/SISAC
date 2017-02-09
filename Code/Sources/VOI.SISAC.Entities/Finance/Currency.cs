//------------------------------------------------------------------------
// <copyright file="Currency.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Currency entity
    /// </summary>
    public partial class Currency
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        public Currency()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
            this.NationalFuelRates = new HashSet<NationalFuelRate>();
        }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency's code.
        /// </value>
        public string CurrencyCode { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the currency.
        /// </summary>
        /// <value>
        /// The description of the currency.
        /// </value>
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Currency"/> is active.
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
        /// Gets or sets Exchange Rates.
        /// </summary>
        public virtual ICollection<ExchangeRates> ExchangeRates { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public virtual ICollection<ReconciliationTolerance> ReconciliationTolerances { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public virtual ICollection<NationalFuelContract> NationalFuelContracts { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public virtual ICollection<NationalFuelRate> NationalFuelRates { get; set; }
    }
}
