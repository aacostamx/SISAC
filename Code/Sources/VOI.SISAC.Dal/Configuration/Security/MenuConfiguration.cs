//------------------------------------------------------------------------
// <copyright file="MenuConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Security
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Security;

    public class MenuConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
        {
            this.ToTable("Menu", "Security");

            this.HasKey(c => c.MenuCode);

            // Properties
            this.Property(e => e.MenuCode)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("MenuCode");

            this.Property(e => e.MenuName)
               .IsRequired()
               .HasMaxLength(25)
               .HasColumnName("MenuName");

            this.Property(e => e.IsParent)
               .IsRequired()
               .HasColumnName("IsParent");

            this.Property(e => e.ParentCode)
               .IsRequired()
               .HasColumnName("ParentCode");

            // Table & Column Mappings
            this.HasMany(e => e.Module)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);
        }
    }
}
