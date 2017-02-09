//------------------------------------------------------------------------
// <copyright file="AdditionalDepartureInformationConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// AdditionalDepartureInformationConfiguration
    /// </summary>
    public class AdditionalDepartureInformationConfiguration : EntityTypeConfiguration<AdditionalDepartureInformation>
    {
        public AdditionalDepartureInformationConfiguration()
        {
            // Table name and schema
            this.ToTable("AdditionalDepartureInformation", "Itinerary");

            // Primary key
            this.HasKey(c => new
            {
                c.Sequence,
                c.AirlineCode,
                c.FlightNumber,
                c.ItineraryKey
            });

            // Properties configuration
            this.Property(c => c.Sequence)
                .HasColumnName("Sequence")
                .IsRequired();

            this.Property(c => c.AirlineCode)
                .HasColumnName("AirlineCode")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.FlightNumber)
                .HasColumnName("FlightNumber")
                .HasMaxLength(5)
                .IsRequired();

            this.Property(c => c.ItineraryKey)
                .HasColumnName("ItineraryKey")
                .HasMaxLength(8)
                .IsRequired();

            this.Property(c => c.Pilot)
                .HasColumnName("Pilot")
                .IsRequired();

            this.Property(c => c.Surcharge)
                .HasColumnName("Surcharge")
                .IsRequired();

            this.Property(c => c.ExtraCrew)
                .HasColumnName("ExtraCrew")
                .IsRequired();

            this.Property(c => c.TypeFlight)
                .HasColumnName("TypeFlight")
                .HasMaxLength(10)
                .IsOptional();

            this.Property(c => c.SlotAllocatedTime)
                .HasColumnName("SlotAllocatedTime")                
                .IsOptional();

            this.Property(c => c.SlotCoordinatedTime)
                .HasColumnName("SlotCoordinatedTime")                
                .IsOptional();

            this.Property(c => c.OvernightEndTime)
                .HasColumnName("OvernightEndTime")                
                .IsOptional();

            this.Property(c => c.ManeuverStartTime)
                .HasColumnName("ManeuverStartTime")                
                .IsOptional();

            this.Property(c => c.PositionOutputTime)
                .HasColumnName("PositionOutputTime")                
                .IsOptional();
    
            this.Property(c => c.DelayDescription1)
                .HasColumnName("DelayDescription1")
                .HasMaxLength(250)
                .IsOptional();

            this.Property(c => c.DelayDescription2)
                .HasColumnName("DelayDescription2")
                .HasMaxLength(250)
                .IsOptional();

            this.Property(c => c.DelayDescription3)
                .HasColumnName("DelayDescription3")
                .HasMaxLength(250)
                .IsOptional();

            this.HasRequired<ManifestDeparture>(s => s.ManifestDeparture)
                .WithOptional(s => s.AdditionalDepartureInformation);            

            //this.HasMany(e => e.ManifestDepartureBoardings)
            //    .WithRequired(e => e.ManifestDeparture)
            //    .HasForeignKey(e => new { e.Sequence, e.AirlineCode, e.FlightNumber, e.ItineraryKey })
            //    .WillCascadeOnDelete(true);  
        }
    }
}
