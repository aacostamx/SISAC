//------------------------------------------------------------------------
// <copyright file="CountryConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Class Configuration Country
    /// </summary>
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryConfiguration"/> class.
        /// </summary>
        public CountryConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("Country", "Catalog");

            // Define Primary key
            this.HasKey<string>(c => c.CountryCode);

            // Define table´s properties
            this.Property(c => c.CountryCode)
                .HasMaxLength(3)
                .HasColumnName("CountryCode");

            this.Property(c => c.CountryName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("CountryName");

            this.Property(c => c.CountryCodeShort)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnName("CountryCodeShort");

            this.Property(c => c.Status)
                .HasColumnName("Status");            
        }
    }
}
