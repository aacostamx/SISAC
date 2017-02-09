//------------------------------------------------------------------------
// <copyright file="ProfileRoleDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using System.Collections.Generic;

    /// <summary>
    /// ProfileRole Data Object
    /// </summary>
    public class ProfileRoleDto
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
        public ProfileDto Profile { get; set; }
        /// <summary>
        /// Gets or sets Role
        /// </summary>
        public RoleDto Role { get; set; }

        /// <summary>
        /// Gets or sets User Profile Roles
        /// </summary>
        public ICollection<UserProfileRoleDto> UserProfileRoles { get; set; }
    }
}
