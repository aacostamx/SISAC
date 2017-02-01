//------------------------------------------------------------------------
// <copyright file="ModulePermissionBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------


namespace VOI.SISAC.Business.Dto.Security
{
    using System.Collections.Generic;
    /// <summary>
    /// Module Permission Data Transfer object
    /// </summary>
    public class ModulePermissionDto
    {

        /// <summary>
        /// ModulePermissionDto
        /// </summary>
        public ModulePermissionDto()
        {

        }

        /// <summary>
        /// ModulePermissionDto
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <param name="permissionCode"></param>
        public ModulePermissionDto(string moduleCode, string permissionCode)
        {
            this.ModuleCode = moduleCode;
            this.PermissionCode = permissionCode;
        }

        /// <summary>
        /// Gets or sets Module Code
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// Gets or sets Permission Code
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// Gets or sets Module
        /// </summary>
        public ModuleDto Module { get; set; }

        /// <summary>
        /// Gets or sets Permission
        /// </summary>
        public PermissionDto Permission { get; set; }

        /////// <summary>
        /////// Gets or sets Roles
        /////// </summary>
        //public ICollection<RoleDto> Roles { get; set; }

        /// <summary>
        /// Gets or sets Users
        /// </summary>
        public ICollection<UserDto> Users { get; set; }
    }
}
