//------------------------------------------------------------------------
// <copyright file="AirlineConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class Configuration Airline
    /// </summary>
    public class AirlineConfiguration : EntityTypeConfiguration<Airline>
    {
        /// <summary>
        /// The schema name
        /// </summary>
        private string schemaName = "Airport";

        /// <summary>
        /// Initializes a new instance of the <see cref="AirlineConfiguration"/> class.
        /// </summary>
        public AirlineConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("Airline", this.schemaName);

            // Define Primary key
            this.HasKey<string>(a => a.AirlineCode);

            // Define table´s properties
            this.Property(a => a.AirlineCode)
                .HasMaxLength(2)
                .IsRequired()
                .HasColumnName("AirlineCode");

            this.Property(a => a.AirlineName)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnName("AirlineName");

            this.Property(a => a.AirlineShortName)
                .HasMaxLength(3)
                .IsRequired()
                .HasColumnName("AirlineShortName");

            this.Property(a => a.Status)
                .HasColumnName("Status");

            this.Property(a => a.CompanyCode)
                .HasMaxLength(10)
                .IsRequired()
                .HasColumnName("CompanyCode");

            this.Property(a => a.Division)
                .HasMaxLength(10)
                .IsRequired()
                .HasColumnName("Division");

            this.Property(a => a.BusinessName)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("BusinessName");

            this.HasMany(a => a.Airplanes)
                .WithRequired(b => b.Airline);
        }
    }
}
