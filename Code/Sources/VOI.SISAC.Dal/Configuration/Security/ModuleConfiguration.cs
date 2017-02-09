// <copyright file="ModuleConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>

namespace VOI.SISAC.Dal.Configuration.Security
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Security;

    /// <summary>
    /// Database configuration for Module.
    /// </summary>
    public class ModuleConfiguration : EntityTypeConfiguration<Module>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleConfiguration"/> class.
        /// </summary>
        public ModuleConfiguration()
        {
            this.ToTable("Module", "Security");

            this.HasKey(c => c.ModuleCode);

            // Properties
            this.Property(e => e.ModuleCode)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("ModuleCode");

            this.Property(e => e.ModuleName)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("ModuleName");

            this.Property(e => e.MenuCode)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("MenuCode");

            this.Property(e => e.ControllerName)
                .HasMaxLength(1500)
                .HasColumnName("ControllerName");

            // Table & Column Mappings
            this.HasMany(e => e.ModulePermissions)
                .WithRequired(e => e.Module)
                .WillCascadeOnDelete(true);
        }
    }
}
