//------------------------------------------------------------------------
// <copyright file="DrinkingWaterConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// DrinkingWater Configurations
    /// </summary>
    public class DrinkingWaterConfiguration : EntityTypeConfiguration<DrinkingWater>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrinkingWaterConfiguration"/> class.
        /// </summary>
        public DrinkingWaterConfiguration()
        {
            // Table name, Schema
            this.ToTable("DrinkingWater", "Airport");

            // Primary key
            this.HasKey<long>(c => c.DrinkingWaterId);

            // Columns configurations
            this.Property(c => c.DrinkingWaterId)
                .IsRequired()
                .HasColumnName("DrinkingWaterId");

            this.Property(c => c.EquipmentNumber)
                .HasMaxLength(8)
                .IsRequired()
                .HasColumnName("EquipmentNumber");

            this.Property(c => c.DrinkingWaterName)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("DrinkingWaterName");

            this.Property(c => c.Value)
                .IsRequired()
                .HasPrecision(10, 2)
                .HasColumnName("Value");
            
            this.Property(c => c.Status)
                .IsRequired()
                .HasColumnName("Status");

            // Relationships
            this.HasRequired<Airplane>(s => s.Airplane)
                .WithMany(s => s.DrinkingWaters)
                .HasForeignKey(s => s.EquipmentNumber);
        }
    }
}
