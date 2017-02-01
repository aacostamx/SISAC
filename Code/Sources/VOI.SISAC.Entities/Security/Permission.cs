//------------------------------------------------------------------------
// <copyright file="Permission.cs" company="Volaris">
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
    /// Class Permission
    /// </summary>
    [Table("Security.Permission")]
    public partial class Permission
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        public Permission()
        {
            this.ModulePermissions = new List<ModulePermission>();
        }
        
        /// <summary>
        /// Gets or sets Permission Code
        /// </summary>
        [Key]
        [StringLength(10)]
        public string PermissionCode { get; set; }
        
        /// <summary>
        /// Gets or sets Permission Name
        /// </summary>
        [StringLength(50)]
        public string PermissionName { get; set; }
        
        /// <summary>
        /// Gets or sets Module Permissions
        /// </summary>
        public virtual ICollection<ModulePermission> ModulePermissions { get; set; }
    }
}
