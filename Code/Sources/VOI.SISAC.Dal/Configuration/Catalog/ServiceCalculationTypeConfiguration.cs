//------------------------------------------------------------------------
// <copyright file="ServiceCalculationTypeConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Data base configuration for ServiceCalculationTypeConfiguration
    /// </summary>
    public class ServiceCalculationTypeConfiguration : EntityTypeConfiguration<ServiceCalculationType>
    {
        public ServiceCalculationTypeConfiguration()
        {
            // Name of the table 
            this.ToTable("ServiceCalculationType", "Catalog");

            // Primary key
            this.HasKey(t => t.CalculationTypeId);

            // Properties configuration
            this.Property(t => t.CalculationTypeName)
                .HasColumnName("CalculationTypeName")
                .HasMaxLength(100)
                .IsRequired();

            this.Property(t => t.CalculationTypeId)
                .HasColumnName("CalculationTypeID")
                .IsRequired();
        }
    }
}
