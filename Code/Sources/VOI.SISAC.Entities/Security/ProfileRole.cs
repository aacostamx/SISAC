//------------------------------------------------------------------------
// <copyright file="ProfileRole.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Security
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class Profile Role
    /// </summary>
    [Table("Security.ProfileRole")]    
    public partial class ProfileRole
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProfileRole()
        {
            this.UserProfileRoles = new List<UserProfileRole>();
        }
       
        /// <summary>
        /// Gets or sets ProfileID
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string ProfileCode { get; set; }
        
        /// <summary>
        /// Gets or sets RoleID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string RoleCode { get; set; }
        
        /// <summary>
        /// Gets or sets Profile
        /// </summary>
        public virtual Profile Profile { get; set; }
        
        /// <summary>
        /// Gets or sets Role
        /// </summary>
        public virtual Role Role { get; set; }
        
        /// <summary>
        /// Gets or sets User Profile Roles
        /// </summary>
        public virtual ICollection<UserProfileRole> UserProfileRoles { get; set; }
    }
}
