//------------------------------------------------------------------------
// <copyright file="Country.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class Country
    /// </summary>
    public partial class Country
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        public Country()
        {
            ////this.Airports = new HashSet<VOI.SISAC.Entities.Airport.Airport>();
        }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>
        /// The country code.
        /// </value>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the country code short.
        /// </summary>
        /// <value>
        /// The country code short.
        /// </value>
        public string CountryCodeShort { get; set; }

        /// <summary>
        /// Gets or sets the name of the country.
        /// </summary>
        /// <value>
        /// The name of the country.
        /// </value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Country"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airports.
        /// </summary>
        /// <value>
        /// The airports.
        /// </value>
        public virtual ICollection<Airport> Airports { get; set; }
    }
}
