//------------------------------------------------------------------------
// <copyright file="ModulePermissionBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOI.SISAC.Business.Dto.Security
{
    /// <summary>
    /// Data transfer object for role marked by profile.
    /// </summary>
    public class RolesMarkedByProfileDto
    {
        /// <summary>
        /// Gets or sets the role code.
        /// </summary>
        /// <value>
        /// The role code.
        /// </value>
        public string RoleCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this role is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if selected; otherwise, <c>false</c>.
        /// </value>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has relationship.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has relationship; otherwise, <c>false</c>.
        /// </value>
        public bool HasRelationship { get; set; }
    }
}
