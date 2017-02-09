//------------------------------------------------------------------------
// <copyright file="TaxConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    /// Database configuration for Tax entity.
    /// </summary>
    public class TaxConfiguration : EntityTypeConfiguration<Tax>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxConfiguration"/> class.
        /// </summary>
        public TaxConfiguration()
        {
            // Name of the Table
            this.ToTable("Tax", "Finance");

            // Primary Key
            this.HasKey<string>(s => s.TaxCode);

            // Relations for the properties
            this.Property(c => c.TaxCode)
                .HasMaxLength(8)
                .HasColumnName("TaxCode");

            this.Property(c => c.TaxName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("TaxName");

            //this.Property(c => c.TaxValue)
            //    .IsRequired()
            //    .HasPrecision(7,2)
            //    .HasColumnName("TaxValue");

            this.Property(c => c.Status)
                .HasColumnName("Status");
        }
    }
}
