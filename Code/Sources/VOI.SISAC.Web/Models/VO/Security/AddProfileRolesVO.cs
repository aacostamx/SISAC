//------------------------------------------------------------------------
// <copyright file="AddProfileRoles.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Add profile roles view object.
    /// </summary>
    public class AddProfileRolesVO
    {
        /// <summary>
        /// Gets or sets the profile code.
        /// </summary>
        /// <value>
        /// The profile code.
        /// </value>
        public string ProfileCode { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public IList<RoleVO> Role { get; set; }
    }
}