//------------------------------------------------------------------------
// <copyright file="AirplaneWeightMeasureTypeConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Data base configuration for AirplaneWeightMeasureType
    /// </summary>
    public class AirplaneWeightMeasureTypeConfiguration : EntityTypeConfiguration<AirplaneWeightMeasureType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneWeightMeasureTypeConfiguration"/> class.
        /// </summary>
        public AirplaneWeightMeasureTypeConfiguration()
        {
            // Name of the table
            this.ToTable("AircraftWeightUomType", "Catalog");

            // Primary key
            this.HasKey(t => t.AirplaneWeightMeasureId);

            // Properties Configuration
            this.Property(t => t.AirplaneWeightMeasureName)
                .HasColumnName("AircraftWeightUomName")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(t => t.AirplaneWeightMeasureId)
                .HasColumnName("AircraftWeightUomID")
                .IsRequired();
        }
    }
}
