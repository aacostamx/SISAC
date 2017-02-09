//------------------------------------------------------------------------
// <copyright file="ModulePermissionBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    /// <summary>
    /// Data transfer object for permission marked by module.
    /// </summary>
    public class PermissionMarkedByModuleDto
    {
        /// <summary>
        /// Gets or sets the permission code.
        /// </summary>
        /// <value>
        /// The permission code.
        /// </value>
        public string PermissionCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the permission.
        /// </summary>
        /// <value>
        /// The name of the permission.
        /// </value>
        public string PermissionName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this permission is selected.
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
