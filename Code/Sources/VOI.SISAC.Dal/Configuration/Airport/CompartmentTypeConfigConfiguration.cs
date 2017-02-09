//------------------------------------------------------------------------
// <copyright file="CompartmentTypeConfigConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// CompartmentType Configurations
    /// </summary>
    public class CompartmentTypeConfigConfiguration : EntityTypeConfiguration<CompartmentTypeConfig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompartmentTypeConfigConfiguration"/> class.
        /// </summary>
        public CompartmentTypeConfigConfiguration()
        {
            // Table name, Schema
            this.ToTable("CompartmentTypeConfig", "Airport");

            // Primary key
            this.HasKey<int>(c => c.CompartmentTypeID);

            // Columns configurations
            this.Property(c => c.CompartmentTypeID)
                .IsRequired()
                .HasColumnName("CompartmentTypeID");

            this.Property(c => c.CompartmentTypeCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("CompartmentTypeCode");

            this.Property(c => c.CompartmentTypeName)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnName("CompartmentTypeName");

            this.Property(c => c.CompartmentTypeLevel)
                .IsRequired()
                .HasColumnName("CompartmentTypeLevel");

            this.Property(c => c.MaximumWeight)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("MaximumWeight");

            this.Property(c => c.Status)
                .IsRequired()
                .HasColumnName("Status");

            // Relationships
            this.HasRequired<CompartmentType>(s => s.CompartmentType)
                .WithMany(s => s.CompartmentTypeConfigs)
                .HasForeignKey(s => s.CompartmentTypeCode);
        }
    }
}
