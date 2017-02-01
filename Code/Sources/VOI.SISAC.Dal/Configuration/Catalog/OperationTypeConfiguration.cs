//------------------------------------------------------------------------
// <copyright file="OperationTypeConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Data base configuration for OperationTypeConfiguration
    /// </summary>
    public class OperationTypeConfiguration : EntityTypeConfiguration<OperationType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationTypeConfiguration"/> class.
        /// </summary>
        public OperationTypeConfiguration()
        {
            // Name of the table
            this.ToTable("OperationType", "Catalog");

            // Primary key
            this.HasKey(t => t.OperationTypeId);

            // Configurations for properties
            this.Property(t => t.OperationName)
                .HasColumnName("OperationName")
                .HasMaxLength(10)
                .IsRequired();

            this.Property(t => t.OperationTypeId)
                .HasColumnName("OperationTypeID")
                .IsRequired();
        }
    }
}
