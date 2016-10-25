//------------------------------------------------------------------------
// <copyright file="ScheduleTypeDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    /// <summary>
    /// ScheduleTypeDto Class
    /// </summary>
    public class ScheduleTypeDto
    {
        /// <summary>
        /// Gets or sets the schedule type code.
        /// </summary>
        /// <value>
        /// The schedule type code.
        /// </value>
        public string ScheduleTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the schedule type.
        /// </summary>
        /// <value>
        /// The name of the schedule type.
        /// </value>
        public string ScheduleTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ScheduleTypeDto"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleTypeDto"/> class.
        /// </summary>
        public ScheduleTypeDto()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleTypeDto"/> class.
        /// </summary>
        /// <param name="ScheduleTypeCode">The schedule type code.</param>
        public ScheduleTypeDto(string ScheduleTypeCode)
        {
            this.ScheduleTypeCode = ScheduleTypeCode;
        }
    }
}
