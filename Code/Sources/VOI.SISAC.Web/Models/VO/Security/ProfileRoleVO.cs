//------------------------------------------------------------------------
// <copyright file="ProfileRoleVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System.Collections.Generic;

    /// <summary>
    /// ProfileRoleVO class
    /// </summary>
    public class ProfileRoleVO
    {
        /// <summary>
        /// Gets or sets ProfileID
        /// </summary>
        public string ProfileCode { get; set; }
        /// <summary>
        /// Gets or sets RoleID
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// Gets or sets Profile
        /// </summary>
        public ProfileVO Profile { get; set; }
        /// <summary>
        /// Gets or sets Role
        /// </summary>
        public RoleVO Role { get; set; }

        /// <summary>
        /// Gets or sets User Profile Roles
        /// </summary>
        public ICollection<UserProfileRoleVO> UserProfileRoles { get; set; }
    }
}