//-----------------------------------------------------------------------
// <copyright file="RoleDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// RoleDto
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// Gets or sets RoleID
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// Gets or sets Role Name
        /// </summary>
        public string RoleName { get; set; }
        
        /// <summary>
        /// Gets the Property if the rol is asigned into a Profile
        /// </summary>
        public bool IsChecked { get; set; }
        ///// <summary>
        ///// Gets or sets Module Permissions
        ///// </summary>
        //public ICollection<ModulePermission> ModulePermissions { get; set; }
        /// <summary>
        /// Gets or sets Profile Roles
        /// </summary>
        public ICollection<ProfileRoleDto> ProfileRoles { get; set; }

        /// <summary>
        /// Gets or sets Module Permissions
        /// </summary>
        public ICollection<ModulePermissionDto> ModulePermissions { get; set; }
    }
}
