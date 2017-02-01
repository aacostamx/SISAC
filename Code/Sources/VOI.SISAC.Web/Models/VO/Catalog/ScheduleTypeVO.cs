//------------------------------------------------------------------------
// <copyright file="ScheduleTypeVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// ScheduleTypeVO Class
    /// </summary>
    public class ScheduleTypeVO
    {
        /// <summary>
        /// Gets or sets the schedule type code.
        /// </summary>
        /// <value>
        /// The schedule type code.
        /// </value>
        [Display(Name = "ScheduleTypeCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string ScheduleTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the schedule type.
        /// </summary>
        /// <value>
        /// The name of the schedule type.
        /// </value>
        [Display(Name = "ScheduleTypeName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string ScheduleTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ScheduleTypeVO"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}