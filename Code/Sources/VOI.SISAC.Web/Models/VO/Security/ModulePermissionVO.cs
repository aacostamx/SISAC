//------------------------------------------------------------------------
// <copyright file="ModulePermissionVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System.Collections.Generic;

    public class ModulePermissionVO
    {
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
        public ModuleVO Module { get; set; }

        /// <summary>
        /// Gets or sets Permission
        /// </summary>
        public PermissionVO Permission { get; set; }

        ///// <summary>
        ///// Gets or sets Roles
        ///// </summary>
        ////public ICollection<RoleDto> Roles { get; set; }

        ///// <summary>
        ///// Gets or sets Users
        ///// </summary>
        //public ICollection<UserVO> Users { get; set; }
    }
}