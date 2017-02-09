//------------------------------------------------------------------------
// <copyright file="CompartmentTypeConfiguration.cs" company="AACOSTA">
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
    public class CompartmentTypeConfiguration : EntityTypeConfiguration<CompartmentType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompartmentTypeConfiguration"/> class.
        /// </summary>
        public CompartmentTypeConfiguration()
        {
            // Table name, Schema
            this.ToTable("CompartmentType", "Airport");

            // Primary key
            this.HasKey<string>(c => c.CompartmentTypeCode);

            // Columns configurations
            this.Property(c => c.CompartmentTypeCode)
                .HasMaxLength(8)
                .IsRequired()
                .HasColumnName("CompartmentTypeCode");

            this.Property(c => c.Status)
                .IsRequired()
                .HasColumnName("Status");

            // Relationships
            this.HasMany<AirplaneType>(s => s.AirplaneTypes)
                .WithRequired(s => s.CompartmentType)
                .HasForeignKey(s => s.CompartmentTypeCode);

            this.HasMany<CompartmentTypeConfig>(s => s.CompartmentTypeConfigs)
                .WithRequired(s => s.CompartmentType)
                .HasForeignKey(s => s.CompartmentTypeCode);

            this.HasMany<CompartmentTypeInformation>(s => s.CompartmentTypeInformations)
                .WithRequired(s => s.CompartmentType)
                .HasForeignKey(s => s.CompartmentTypeCode);
        }
    }
}
