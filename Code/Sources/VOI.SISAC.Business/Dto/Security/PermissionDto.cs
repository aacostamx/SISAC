//-----------------------------------------------------------------------
// <copyright file="PermissionDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// PermissionDto
    /// </summary>
    public class PermissionDto
    {
        /// <summary>
        /// Gets or sets Permission Code
        /// </summary>
        public string PermissionCode { get; set; }
        
        /// <summary>
        /// Gets or sets Permission Name
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected { get; set; }

        ///// <summary>
        ///// Gets or sets Module Permissions
        ///// </summary>
        //public ICollection<ModulePermission> ModulePermissions { get; set; }
    }
}
