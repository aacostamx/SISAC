//------------------------------------------------------------------------
// <copyright file="AirplaneConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Airplane Configurations
    /// </summary>
    public class AirplaneConfiguration : EntityTypeConfiguration<Airplane>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneConfiguration"/> class.
        /// </summary>
        public AirplaneConfiguration()
        {
            // Table name, Schema
            this.ToTable("Airplane", "Airport");

            // Primary key
            this.HasKey<string>(c => c.EquipmentNumber);

            // Columns configurations
            this.Property(c => c.EquipmentNumber)
                .HasMaxLength(8)
                .IsRequired()
                .HasColumnName("EquipmentNumber");

            this.Property(c => c.AirplaneModel)
                .HasMaxLength(12)
                .IsRequired()
                .HasColumnName("AirplaneModel");

            this.Property(c => c.AirlineCode)
                .HasMaxLength(2)
                .IsRequired()
                .HasColumnName("AirlineCode");

            this.Property(c => c.SerialNumber)
                .HasMaxLength(20)
                .IsOptional()
                .HasColumnName("SerialNumber");

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

            this.Property(c => c.Status)
                .IsRequired()
                .HasColumnName("Status");

            // Relationships
            this.HasRequired<AirplaneType>(s => s.AirplaneType)
                .WithMany(s => s.Airplanes)
                .HasForeignKey(s => s.AirplaneModel);

            this.HasMany<DrinkingWater>(s => s.DrinkingWaters)
                .WithRequired(s => s.Airplane)
                .HasForeignKey(s=>s.EquipmentNumber);

            this.HasRequired<Airline>(s => s.Airline)
                .WithMany(s => s.Airplanes)
                .HasForeignKey(s => s.AirlineCode);
        }
    }
}
