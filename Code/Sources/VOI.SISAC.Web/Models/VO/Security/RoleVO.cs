//-----------------------------------------------------------------------
// <copyright file="RoleVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Role View Object
    /// </summary>
    public class RoleVO
    {
        /// <summary>
        /// Gets or sets role code
        /// </summary>
        [Display(Name = "RoleCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(5, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "RoleMaxMinLong")]
        public string RoleCode { get; set; }

        /// <summary>
        /// Gets or sets Role Name
        /// </summary>
        [Display(Name = "RoleName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax50")]
        public string RoleName { get; set; }

        /// <summary>
        /// Gets the Property if the rol is asigned into a Profile
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets Profile Roles
        /// </summary>
        public ICollection<ProfileRoleVO> ProfileRoles { get; set; }

        /// <summary>
        /// Gets or sets Module Permissions
        /// </summary>
        public ICollection<ModulePermissionVO> ModulePermissions { get; set; }
    }
}