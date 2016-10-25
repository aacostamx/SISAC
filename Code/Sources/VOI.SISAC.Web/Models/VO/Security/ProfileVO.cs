//------------------------------------------------------------------------
// <copyright file="ProfileVO.cs" company="Volaris">
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
    /// View Object Profile
    /// </summary>
    public class ProfileVO
    {
        /// <summary>
        /// Gets or sets ProfileID
        /// </summary>
        [Display(Name = "ProfileCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        [StringLength(5, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax5")]
        public string ProfileCode { get; set; }

        /// <summary>
        /// Gets or sets Profile Name
        /// </summary>
        [Display(Name = "ProfileName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax100")]
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets Profile Roles
        /// </summary>
        public virtual ICollection<ProfileRoleVO> ProfileRoles { get; set; }
    }
}