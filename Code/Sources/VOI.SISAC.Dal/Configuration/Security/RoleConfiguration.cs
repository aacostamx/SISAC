//------------------------------------------------------------------------
// <copyright file="RoleConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Database configuration for Profile Role.
    /// </summary>
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleConfiguration"/> class.
        /// </summary>
        public RoleConfiguration()
        {
            // Properties
            this.Property(e => e.RoleCode)
                .IsUnicode(false);

            this.Property(e => e.RoleName)
                .IsUnicode(false);

            // Table & Column Mappings
            this.HasMany(e => e.ProfileRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);
        }
    }
}
