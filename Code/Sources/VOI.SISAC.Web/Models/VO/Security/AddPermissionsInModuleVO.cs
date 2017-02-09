//------------------------------------------------------------------------
// <copyright file="AddPermissionsInModuleVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System.Collections.Generic;

    /// <summary>
    /// View object add permissions in module
    /// </summary>
    public class AddPermissionsInModuleVO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddPermissionsInModuleVO"/> class.
        /// </summary>
        public AddPermissionsInModuleVO()
        {
            this.Permission = new List<PermissionVO>();
        }

        /// <summary>
        /// Gets or sets the module code.
        /// </summary>
        /// <value>
        /// The module code.
        /// </value>
        public string ModuleCode { get; set; }

        /// <summary>
        /// Gets or sets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public List<PermissionVO> Permission { get; set; }
    }
}