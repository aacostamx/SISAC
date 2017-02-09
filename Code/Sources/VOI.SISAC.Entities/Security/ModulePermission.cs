//------------------------------------------------------------------------
// <copyright file="ModulePermission.cs" company="AACOSTA">
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
    /// Class Module Permission
    /// </summary>
    [Table("Security.ModulePermission")]
    public partial class ModulePermission
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModulePermission"/> class.
        /// </summary>
        public ModulePermission()
        {
            this.Roles = new List<Role>();
            this.Users = new List<User>();
        }

        /// <summary>
        /// Gets or sets ModuleID
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ModuleCode { get; set; }
        
        /// <summary>
        /// Gets or sets Permission Code
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string PermissionCode { get; set; }
        
        /// <summary>
        /// Gets or sets Module
        /// </summary>
        public virtual Module Module { get; set; }
        
        /// <summary>
        /// Gets or sets Permission
        /// </summary>
        public virtual Permission Permission { get; set; }
        
        /// <summary>
        /// Gets or sets Roles
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }
        
        /// <summary>
        /// Gets or sets Users
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
