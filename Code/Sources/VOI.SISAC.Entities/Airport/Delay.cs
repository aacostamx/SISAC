//------------------------------------------------------------------------
// <copyright file="Delay.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Catalog;
using VOI.SISAC.Entities.Itineraries;
    using System.Collections.Generic;

    /// <summary>
    /// Delay class
    /// </summary>
    [Table("Airport.Delay")]
    public partial class Delay
    {
        /// <summary>
        /// Delay Code
        /// </summary>
        [Key]
        [StringLength(5)]
        public string DelayCode { get; set; }

        /// <summary>
        /// Delay Name
        /// </summary>
        [Required]
        [StringLength(250)]
        public string DelayName { get; set; }

        /// <summary>
        /// FunctionalArea
        /// </summary>
        public long FunctionalAreaID { get; set; }

        /// <summary>
        /// Under Control
        /// </summary>
        public bool UnderControl { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Functional Area
        /// </summary>
        public virtual FunctionalArea FunctionalArea { get; set; }

        /// <summary>
        /// Gets or sets the manifest departures.
        /// </summary>
        /// <value>
        /// The manifest departures.
        /// </value>
        public virtual IList<ManifestDeparture> ManifestDepartures { get; set; }

        /// <summary>
        /// Gets or sets the manifest arrival.
        /// </summary>
        /// <value>
        /// The manifest arrival.
        /// </value>
        public virtual IList<ManifestArrival> ManifestArrivals { get; set; }
    }
}
