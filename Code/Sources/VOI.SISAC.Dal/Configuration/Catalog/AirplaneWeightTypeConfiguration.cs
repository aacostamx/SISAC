//------------------------------------------------------------------------
// <copyright file="AirplaneWeightTypeConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Data base configuration for AirplaneWeightType
    /// </summary>
    public class AirplaneWeightTypeConfiguration : EntityTypeConfiguration<AirplaneWeightType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneWeightTypeConfiguration"/> class.
        /// </summary>
        public AirplaneWeightTypeConfiguration()
        {
            // Name of the table 
            this.ToTable("AircraftWeightType", "Catalog");

            // Primary key
            this.HasKey(t => t.AirplaneWeightCode);

            // Properties configurations
            this.Property(t => t.AirplaneWeightName)
                .HasColumnName("AircraftWeightName")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.AirplaneWeightCode)
                .HasColumnName("AircraftWeightCode")
                .HasMaxLength(5)
                .IsRequired();
        }
    }
}
