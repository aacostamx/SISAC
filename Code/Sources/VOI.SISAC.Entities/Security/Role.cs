//------------------------------------------------------------------------
// <copyright file="Role.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    /// <summary>
    /// Class Role
    /// </summary>
    [Table("Security.Role")]
    public partial class Role
    {
        public Role()
        {
            this.ProfileRoles = new List<ProfileRole>();
            this.ModulePermissions = new List<ModulePermission>();
        }

        /// <summary>
        /// Gets or sets RoleID
        /// </summary>
        [Key]
        [StringLength(5)]
        public string RoleCode { get; set; }
        
        /// <summary>
        /// Gets or sets Role Name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }
        
        /// <summary>
        /// Gets or sets Profile Roles
        /// </summary>
        public virtual ICollection<ProfileRole> ProfileRoles { get; set; }
        
        /// <summary>
        /// Gets or sets Module Permissions
        /// </summary>
        public virtual ICollection<ModulePermission> ModulePermissions { get; set; }
    }
}
