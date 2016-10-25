//------------------------------------------------------------------------
// <copyright file="ProfileDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Data Transfer Objet for Profiles
    /// </summary>
    public class ProfileDto
    {

        /// <summary>
        /// Gets or sets ProfileID
        /// </summary>
        public string ProfileCode { get; set; }

        /// <summary>
        /// Gets or sets Profile Name
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets Profile Roles
        /// </summary>
        public ICollection<ProfileRoleDto> ProfileRoles { get; set; }
    }
}
