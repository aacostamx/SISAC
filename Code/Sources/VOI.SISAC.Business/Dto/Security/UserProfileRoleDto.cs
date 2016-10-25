//------------------------------------------------------------------------
// <copyright file="UserProfileRoleDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Class UserProfileRoleDto
    /// </summary>
    public class UserProfileRoleDto
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
        /// Gets or sets a value indicating whether this <see cref="UserProfileRole"/> is principal.
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
        public ProfileRoleDto ProfileRoles { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public UserDto Users { get; set; }
    }
}
