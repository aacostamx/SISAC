//------------------------------------------------------------------------
// <copyright file="Currency.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VOI.SISAC.Entities.Catalog;
using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Airport class
    /// </summary>
    [Table("Airport.Airport")]
    public partial class Airport
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Airport"/> class.
        /// </summary>
        public Airport()
        {
            this.Gpu = new HashSet<Gpu>();
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
            this.AirportSchedules = new HashSet<AirportSchedule>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
            this.NationalFuelRates = new HashSet<NationalFuelRate>();
        }

        /// <summary>
        /// Station code
        /// </summary>
        [Key]
        [StringLength(3)]
        public string StationCode { get; set; }

        /// <summary>
        /// Station name
        /// </summary>
        [Required]
        [StringLength(100)]
        public string StationName { get; set; }

        /// <summary>
        /// Country Code
        /// </summary>
        [Required]
        [StringLength(3)]
        public string CountryCode { get; set; }
        /// <summary>
        /// Opening time
        /// </summary>
        public TimeSpan? OpeningTime { get; set; }
        /// <summary>
        /// Closing Time
        /// </summary>
        public TimeSpan? ClosingTime { get; set; }

        /// <summary>
        /// Airport Group Code
        /// </summary>
        [StringLength(8)]
        public string AirportGroupCode { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public virtual Country Country { get; set; }
        /// <summary>
        /// Gpu
        /// </summary>
        public virtual ICollection<Gpu> Gpu { get; set; }
        /// <summary>
        /// Airport Group
        /// </summary>
        public virtual AirportGroup AirportGroup { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContracts { get; set; }  

        /// <summary>
        /// Gets or sets the airport schedules.
        /// </summary>
        /// <value>The airport schedules.</value>
        public virtual ICollection<AirportSchedule> AirportSchedules { get; set; }

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
    }
}
