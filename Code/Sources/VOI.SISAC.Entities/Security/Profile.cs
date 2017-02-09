//------------------------------------------------------------------------
// <copyright file="ProfileRole.cs" company="AACOSTA">
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
    /// 
    /// </summary>
    [Table("Security.Profile")]
    public partial class Profile
    {
        /// <summary>
        /// Gets or sets ProfileID
        /// </summary>
        [Key]
        [StringLength(5)]
        public string ProfileCode { get; set; }

        /// <summary>
        /// Gets or sets Profile Name
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ProfileName { get; set; }
        
        ///// <summary>
        ///// Gets or sets Status
        ///// </summary>
        //public bool Status { get; set; }
        
        /// <summary>
        /// Gets or sets Profile Roles
        /// </summary>
        public virtual ICollection<ProfileRole> ProfileRoles { get; set; }
    }
}
