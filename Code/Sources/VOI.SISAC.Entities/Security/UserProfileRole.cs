//------------------------------------------------------------------------
// <copyright file="UserProfileRole.cs" company="AACOSTA">
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
    /// Class User Profile Role
    /// </summary>
    [Table("Security.UserProfileRole")]
    public partial class UserProfileRole
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the profile code.
        /// </summary>
        /// <value>
        /// The profile code.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string ProfileCode { get; set; }

        /// <summary>
        /// Gets or sets the role code.
        /// </summary>
        /// <value>
        /// The role code.
        /// </value>
        [Key]
        [Column(Order = 2)]
        [StringLength(5)]
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
        public virtual ProfileRole ProfileRoles { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual User Users { get; set; }
    }
}
