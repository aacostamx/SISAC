// ***********************************************************************
// <copyright file="AirportSchedule.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
// ***********************************************************************


/// <summary>
/// The Airport namespace.
/// </summary>
namespace VOI.SISAC.Entities.Airport
{
    using System;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Class AirportSchedule.
    /// </summary>
    public partial class AirportSchedule
    {

        /// <summary>
        /// Gets or sets the airport schedule identifier.
        /// </summary>
        /// <value>The airport schedule identifier.</value>
        public long AirportScheduleID { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        /// <value>The station code.</value>
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the schedule type code.
        /// </summary>
        /// <value>The schedule type code.</value>
        public string ScheduleTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the start date schedule.
        /// </summary>
        /// <value>The start date schedule.</value>
        public TimeSpan StartTimeSchedule { get; set; }

        /// <summary>
        /// Gets or sets the end date schedule.
        /// </summary>
        /// <value>The end date schedule.</value>
        public TimeSpan EndTimeSchedule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AirportSchedule"/> is status.
        /// </summary>
        /// <value><c>true</c> if status; otherwise, <c>false</c>.</value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airport.
        /// </summary>
        /// <value>The airport.</value>
        public virtual Airport Airport { get; set; }

        /// <summary>
        /// Gets or sets the type of the schedule.
        /// </summary>
        /// <value>The type of the schedule.</value>
        public virtual ScheduleType ScheduleType { get; set; }
    }
}
