//------------------------------------------------------------------------
// <copyright file="AirportScheduleConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Catalog;


    /// <summary>
    /// Class AirportScheduleConfiguration.
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{VOI.SISAC.Entities.Airport.DrinkingWater}" />
    public class AirportScheduleConfiguration : EntityTypeConfiguration<AirportSchedule>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportScheduleConfiguration"/> class.
        /// </summary>
        public AirportScheduleConfiguration()
        {
            // Table name, Schema
            this.ToTable("AirportSchedule", "Airport");

            // Primary key
            this.HasKey<long>(c => c.AirportScheduleID);

            // Columns configurations
            this.Property(c => c.AirportScheduleID)
                .IsRequired()
                .HasColumnName("AirportScheduleID");

            this.Property(c => c.StationCode)
                .HasMaxLength(3)
                .IsRequired()
                .HasColumnName("StationCode");

            this.Property(c => c.ScheduleTypeCode)
                .HasMaxLength(3)
                .IsRequired()
                .HasColumnName("ScheduleTypeCode");

            this.Property(c => c.StartTimeSchedule)
                .IsRequired()
                .HasColumnName("StartTimeSchedule");

            this.Property(c => c.EndTimeSchedule)
                .IsRequired()
                .HasColumnName("EndTimeSchedule");
            
            this.Property(c => c.Status)
                .IsRequired()
                .HasColumnName("Status");

            // Relationships
            this.HasRequired<Airport>(s => s.Airport)
                .WithMany(s => s.AirportSchedules)
                .HasForeignKey(s => s.StationCode);

            this.HasRequired<ScheduleType>(s => s.ScheduleType)
                .WithMany(s => s.AirportSchedules)
                .HasForeignKey(s => s.ScheduleTypeCode);
        }
    }
}
