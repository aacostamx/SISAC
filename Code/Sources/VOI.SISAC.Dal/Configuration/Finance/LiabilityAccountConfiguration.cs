//------------------------------------------------------------------------
// <copyright file="LiabilityAccountConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Liability Account Configuration
    /// </summary>
    public class LiabilityAccountConfiguration : EntityTypeConfiguration<LiabilityAccount>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiabilityAccountConfiguration"/> class.
        /// </summary>
        public LiabilityAccountConfiguration()
        {
            // Name of the Table
            this.ToTable("LiabilityAccount", "Finance");

            // Primary Key
            this.HasKey<string>(s => s.LiabilityAccountNumber);

            // Relations for the properties
            this.Property(c => c.LiabilityAccountNumber)
                .HasMaxLength(8)
                .HasColumnName("LiabilityAccountNumber");

            this.Property(c => c.LiabilityAccountName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("LiabilityAccountName");

            this.Property(c => c.Status)
                .HasColumnName("Status");
        }
    }
}
