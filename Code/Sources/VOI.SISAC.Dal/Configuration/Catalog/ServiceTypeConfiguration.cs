//------------------------------------------------------------------------
// <copyright file="ServiceTypeConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Data base configuratios for Service type
    /// </summary>
    public class ServiceTypeConfiguration : EntityTypeConfiguration<ServiceType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTypeConfiguration"/> class.
        /// </summary>
        public ServiceTypeConfiguration()
        {
            // Name of the table
            this.ToTable("ServiceType", "Catalog");

            // Primary key
            this.HasKey(t => t.ServiceTypeCode);

            // Configurations for properties
            this.Property(t => t.ServiceTypeName)
                .HasColumnName("ServiceTypeName")
                .HasMaxLength(20)
                .IsRequired();

            this.Property(t => t.ServiceTypeCode)
                .HasColumnName("ServiceTypeCode")
                .HasMaxLength(1)
                .IsRequired();
        }
    }
}
