//------------------------------------------------------------------------
// <copyright file="ScheduleType.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// ScheduleType
    /// </summary>
    [Table("Catalog.ScheduleType")]
    public partial class ScheduleType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleType"/> class.
        /// </summary>
        public ScheduleType()
        {
            this.AirportSchedules = new HashSet<AirportSchedule>();
            this.NationalFuelRates = new HashSet<NationalFuelRate>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleType"/> class.
        /// </summary>
        /// <param name="ScheduleTypeCode">The schedule type code.</param>
        public ScheduleType(string ScheduleTypeCode)
        {
            this.ScheduleTypeCode = ScheduleTypeCode;
            this.AirportSchedules = new HashSet<AirportSchedule>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleType"/> class.
        /// </summary>
        /// <param name="ScheduleTypeCode">The schedule type code.</param>
        /// <param name="ScheduleTypeName">Name of the schedule type.</param>
        public ScheduleType(string ScheduleTypeCode, string ScheduleTypeName)
        {
            this.ScheduleTypeCode = ScheduleTypeCode;
            this.ScheduleTypeName = ScheduleTypeName;
            this.AirportSchedules = new HashSet<AirportSchedule>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleType"/> class.
        /// </summary>
        /// <param name="ScheduleTypeCode">The schedule type code.</param>
        /// <param name="ScheduleTypeName">Name of the schedule type.</param>
        /// <param name="Status">if set to <c>true</c> [status].</param>
        public ScheduleType(string ScheduleTypeCode, string ScheduleTypeName, bool Status)
        {
            this.ScheduleTypeCode = ScheduleTypeCode;
            this.ScheduleTypeName = ScheduleTypeName;
            this.Status = Status;
            this.AirportSchedules = new HashSet<AirportSchedule>();
        }

        /// <summary>
        /// Gets or sets the schedule type code.
        /// </summary>
        /// <value>
        /// The schedule type code.
        /// </value>
        [Key]
        [StringLength(3)]
        public string ScheduleTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the schedule type.
        /// </summary>
        /// <value>
        /// The name of the schedule type.
        /// </value>
        [Required]
        [StringLength(30)]
        public string ScheduleTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ScheduleType"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airport schedules.
        /// </summary>
        /// <value>The airport schedules.</value>
        public virtual ICollection<AirportSchedule> AirportSchedules { get; set; }

        /// <summary>
        /// Gets or sets the national fuel rates.
        /// </summary>
        /// <value>
        /// The national fuel rates.
        /// </value>
        public virtual ICollection<NationalFuelRate> NationalFuelRates { get; set; }
    }
}
