//------------------------------------------------------------------------
// <copyright file="AirplaneTypeConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

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
    /// Airplane Type Configurations
    /// </summary>
    public class AirplaneTypeConfiguration : EntityTypeConfiguration<AirplaneType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneTypeConfiguration"/> class.
        /// </summary>
        public AirplaneTypeConfiguration()
        {
            // Table name, Schema
            this.ToTable("AirplaneType", "Airport");

            // Primary key
            this.HasKey<string>(c => c.AirplaneModel);

            // Columns configurations
            this.Property(c => c.AirplaneModel)
                .HasMaxLength(12)
                .IsRequired()
                .HasColumnName("AirplaneModel");

            this.Property(c => c.CompartmentTypeCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("CompartmentTypeCode");

            this.Property(c => c.MaximumTakeoffWeight)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("MaximumTakeoffWeight");

            this.Property(c => c.WeightInPound)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("WeightInPound");

            this.Property(c => c.WeightInTonnes)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("WeightInTonnes");

            this.Property(c => c.EmptyOperatingWeight)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("EmptyOperatingWeight");

            this.Property(c => c.FilmingMaximumWeight)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("FilmingMaximumWeight");

            this.Property(c => c.TakeoffWeightInTonnes)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("TakeoffWeightInTonnes");

            this.Property(c => c.GroupWeight)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("GroupWeight");

            this.Property(c => c.MaximumLandingWeight)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("MaximumLandingWeight");

            this.Property(c => c.MaximumZeroFuelWeight)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("MaximumZeroFuelWeight");

            this.Property(c => c.PassengerCapacity)
                .IsRequired()
                .HasColumnName("PassengerCapacity");

            this.Property(c => c.CrewCapacity)
                .IsRequired()
                .HasColumnName("CrewCapacity");

            this.Property(c => c.Magnitude)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("Magnitude");

            this.Property(c => c.FuelInLiters)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("FuelInLiters");

            this.Property(c => c.FuelInKg)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("FuelInKg");

            this.Property(c => c.FuelInGallon)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("FuelInGallon");

            this.Property(c => c.Status)
                .IsRequired()
                .HasColumnName("Status");

            // Relationships
            this.HasMany<Airplane>(s => s.Airplanes)
                .WithRequired(s => s.AirplaneType)
                .HasForeignKey(s => s.AirplaneModel);

            this.HasRequired(s => s.CompartmentType)
                .WithMany(s => s.AirplaneTypes)
                .HasForeignKey(s => s.CompartmentTypeCode);
        }
    }
}
