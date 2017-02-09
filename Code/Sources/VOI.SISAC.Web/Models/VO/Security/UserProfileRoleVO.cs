//------------------------------------------------------------------------
// <copyright file="UserProfileRoleVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Class UserProfileRoleVO
    /// </summary>
    public class UserProfileRoleVO
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the profile code.
        /// </summary>
        /// <value>
        /// The profile code.
        /// </value>
        public string ProfileCode { get; set; }

        /// <summary>
        /// Gets or sets the role code.
        /// </summary>
        /// <value>
        /// The role code.
        /// </value>
        public string RoleCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Principal"/> is principal.
        /// </summary>
        /// <value>
        ///   <c>true</c> if principal; otherwise, <c>false</c>.
        /// </value>
        public bool Principal { get; set; }

        /// <summary>
        /// Gets or sets the profile roles.
        /// </summary>
        /// <value>
        /// The profile roles.
        /// </value>
        public ProfileRoleVO ProfileRoles { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public UserVO Users { get; set; }

        //[Display(Name = "NombreUsuario", ResourceType = typeof(VOI.SISAC.Web.Resources.Resource))]
        public string ProfileRoleCode
        {
            get
            {
                if (!String.IsNullOrEmpty(this.ProfileCode) && !String.IsNullOrEmpty(this.RoleCode))
                    return this.ProfileCode + this.RoleCode;
                else
                    return "";
            }
        }
    }
}