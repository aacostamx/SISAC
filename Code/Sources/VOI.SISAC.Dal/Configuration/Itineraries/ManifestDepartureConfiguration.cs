//------------------------------------------------------------------------
// <copyright file="ManifestDepartureConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Manifest departure configuration
    /// </summary>
    public class ManifestDepartureConfiguration : EntityTypeConfiguration<ManifestDeparture>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestDepartureConfiguration"/> class.
        /// </summary>
        public ManifestDepartureConfiguration()
        {
            // Table name and schema
            this.ToTable("ManifestDeparture", "Itinerary");

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

            this.Property(c => c.CreationDate)
                .HasColumnName("CreationDate")
                .IsRequired();

            this.Property(c => c.NickNameCommander)
                .HasColumnName("NickNameCommander")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(c => c.NickNameFirstOfficial)
                .HasColumnName("NickNameFirstOfficial")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(c => c.NickNameSecondOfficial)
                .HasColumnName("NickNameSecondOfficial")
                .HasMaxLength(50)
                .IsOptional();

            this.Property(c => c.NickNameThirdOfficial)
                .HasColumnName("NickNameThirdOfficial")
                .HasMaxLength(50)
                .IsOptional();

            this.Property(c => c.NickNameChiefCabinet)
                .HasColumnName("NickNameChiefCabinet")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(c => c.NickNameFirstSupercargo)
                .HasColumnName("NickNameFirstSupercargo")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(c => c.NickNameSecondSupercargo)
                .HasColumnName("NickNameSecondSupercargo")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(c => c.NickNameThirdSupercargo)
                .HasColumnName("NickNameThirdSupercargo")
                .HasMaxLength(50)
                .IsOptional();

            this.Property(c => c.SupercargoRemarks)
                .HasColumnName("SupercargoRemarks")
                .HasMaxLength(150)
                .IsOptional();

            this.Property(c => c.DepartureStation)
                .HasColumnName("DepartureStation")
                .HasMaxLength(3)
                .IsOptional();

            this.Property(c => c.ScaleStation)
                .HasColumnName("ScaleStation")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.ArrivalStation)
                .HasColumnName("ArrivalStation")
                .HasMaxLength(3)
                .IsOptional();

            this.Property(c => c.ActualDepartureDate)
                .HasColumnName("ActualDepartureDate")
                .IsRequired();

            this.Property(c => c.DelayRemarks)
                .HasColumnName("DelayRemarks")
                .HasMaxLength(150)
                .IsOptional();

            this.Property(c => c.InnerSection)
                .HasColumnName("InnerSection")
                .IsRequired();

            this.Property(c => c.International)
                .HasColumnName("International")
                .IsRequired();

            this.Property(c => c.InternationalExempt)
                .HasColumnName("InternationalExempt")
                .IsRequired();

            this.Property(c => c.NationalExempt)
                .HasColumnName("NationalExempt")
                .IsRequired();

            this.Property(c => c.Transit)
                .HasColumnName("Transit")
                .IsRequired();

            this.Property(c => c.Infant)
                .HasColumnName("Infant")
                .IsRequired();

            this.Property(c => c.JetFuel)
                .HasColumnName("JetFuel")
                .HasPrecision(18, 5)
                .IsRequired();

            this.Property(c => c.RealTakeoffWeight)
                .HasColumnName("RealTakeoffWeight")
                .HasPrecision(18, 5)
                .IsRequired();

            this.Property(c => c.OperatingWeight)
                .HasColumnName("OperatingWeight")
                .HasPrecision(18, 5)
                .IsRequired();

            this.Property(c => c.SafetyMargin)
                .HasColumnName("SafetyMargin")
                .HasPrecision(18, 5)
                .IsRequired();

            this.Property(c => c.StructuralTakeoffWeight)
                .HasColumnName("StructuralTakeoffWeight")
                .HasPrecision(18, 5)
                .IsRequired();

            this.Property(c => c.UserSignatureId)
                .HasColumnName("UserIDSignature")
                .IsRequired();

            this.Property(c => c.LicenceNumberSignature)
                .HasColumnName("LicenceNumberSignature")
                .HasMaxLength(20)
                .IsRequired();

            this.Property(c => c.UserAuthorizeId)
                .HasColumnName("UserIDAuthorize")
                .IsRequired();

            this.Property(c => c.LicenceNumberAuthorize)
                .HasColumnName("LicenceNumberAuthorize")
                .HasMaxLength(20)
                .IsRequired();

            this.Property(c => c.Position)
                .HasColumnName("Position")
                .HasMaxLength(50)
                .IsRequired();

            this.Property(c => c.InfantsTickets)
                .HasColumnName("InfantsTickets")
                .IsRequired();

            this.Property(c => c.Remarks)
                .HasColumnName("Remarks")
                .HasMaxLength(150)
                .IsOptional();

            this.Property(c => c.Status)
                .HasColumnName("Status")
                .HasMaxLength(5)
                .IsRequired();

            this.Property(c => c.Closed)
                .HasColumnName("Closed")
                .IsRequired();

            this.HasRequired<Itinerary>(s => s.Itinerary)
                .WithOptional(s => s.ManifestDeparture);

            this.HasMany<Delay>(e => e.Delays)
               .WithMany(e => e.ManifestDepartures)
               .Map(m => m.ToTable("ManifestDepartureDelay", "Itinerary")
               .MapRightKey("DelayCode")
               .MapLeftKey(new[] 
               { 
                   "Sequence", 
                   "AirlineCode", 
                   "FlightNumber", 
                   "ItineraryKey" 
               }));

            this.HasMany(e => e.ManifestDepartureBoardings)
                .WithRequired(e => e.ManifestDeparture)
                .HasForeignKey(e => new { e.Sequence, e.AirlineCode, e.FlightNumber, e.ItineraryKey })
                .WillCascadeOnDelete(true);  
        }
    }
}
