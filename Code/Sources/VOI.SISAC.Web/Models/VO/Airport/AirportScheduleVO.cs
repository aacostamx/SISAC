//------------------------------------------------------------------------
// <copyright file="AirportScheduleVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Class AirportScheduleVO.
    /// </summary>
    public class AirportScheduleVO
    {

        /// <summary>
        /// Gets or sets the airport schedule identifier.
        /// </summary>
        /// <value>The airport schedule identifier.</value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public long AirportScheduleID { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        /// <value>The station code.</value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax3")]
        [Display(Name = "StationCode", ResourceType = typeof(Resources.Resource))]
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the schedule type code.
        /// </summary>
        /// <value>The schedule type code.</value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax3")]
        [Display(Name = "ScheduleTypeCode", ResourceType = typeof(Resources.Resource))]
        public string ScheduleTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the start date schedule.
        /// </summary>
        /// <value>The start date schedule.</value>        
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        [Display(Name = "StartTimeSchedule", ResourceType = typeof(Resources.Resource))]
        public TimeSpan StartTimeSchedule { get; set; }

        /// <summary>
        /// Gets or sets the end date schedule.
        /// </summary>
        /// <value>The end date schedule.</value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}")]
        [Display(Name = "EndTimeSchedule", ResourceType = typeof(Resources.Resource))]
        public TimeSpan EndTimeSchedule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AirportSchedule"/> is status.
        /// </summary>
        /// <value><c>true</c> if status; otherwise, <c>false</c>.</value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airport.
        /// </summary>
        /// <value>The airport.</value>
        public AirportVO Airport { get; set; }

        /// <summary>
        /// Gets or sets the type of the schedule.
        /// </summary>
        /// <value>The type of the schedule.</value>
        //public ScheduleTypeVO ScheduleType { get; set; }
    }
}