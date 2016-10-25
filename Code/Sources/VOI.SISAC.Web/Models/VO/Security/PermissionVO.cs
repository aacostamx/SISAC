//-----------------------------------------------------------------------
// <copyright file="PermissionVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// PermissionVO
    /// </summary>
    public class PermissionVO
    {
        /// <summary>
        /// Gets or sets Permission Code
        /// </summary>
        [Display(Name = "PermissionCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "PermissionMaxMinLong")]
        public string PermissionCode { get; set; }

        /// <summary>
        /// Gets or sets Permission Name
        /// </summary>
        [Display(Name = "PermissionName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax50")]
        public string PermissionName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected { get; set; }
    }
}