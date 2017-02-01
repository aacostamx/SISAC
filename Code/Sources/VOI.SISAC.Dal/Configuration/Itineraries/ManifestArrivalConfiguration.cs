//------------------------------------------------------------------------
// <copyright file="ManifestArrivalConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Manifest departure configuration
    /// </summary>
    public class ManifestArrivalConfiguration : EntityTypeConfiguration<ManifestArrival>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestArrivalConfiguration"/> class.
        /// </summary>
        public ManifestArrivalConfiguration()
        {
            // Table name and schema
            this.ToTable("ManifestArrival", "Itinerary");

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

            this.Property(c => c.LastScaleStation)
                .HasColumnName("LastScaleStation")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.ArrivalStation)
                .HasColumnName("ArrivalStation")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.ActualArrivalDate)
                .HasColumnName("ActualArrivalDate")
                .IsRequired();

            this.Property(c => c.DelayRemarks)
                .HasColumnName("DelayRemarks")
                .HasMaxLength(150)
                .IsOptional();

            this.Property(c => c.AdultPassenger)
                .HasColumnName("AdultPassenger")
                .IsOptional();

            this.Property(c => c.MinorPassenger)
                .HasColumnName("MinorPassenger")
                .IsOptional();

            this.Property(c => c.InfantPassenger)
                .HasColumnName("InfantPassenger")
                .IsOptional();

            this.Property(c => c.LuggageQuantity)
                .HasColumnName("LuggageQuantity")
                .IsOptional();

            this.Property(c => c.LuggageWeight)
                .HasColumnName("KgsLuggage")
                .HasPrecision(18, 5)
                .IsOptional();

            this.Property(c => c.ChargeQuantity)
                .HasColumnName("ChargeQuantity")
                .IsOptional();

            this.Property(c => c.ChargeWeight)
                .HasColumnName("KgsCharge")
                .HasPrecision(18, 5)
                .IsOptional();

            this.Property(c => c.MailQuantity)
                .HasColumnName("MailQuantity")
                .IsRequired();

            this.Property(c => c.MailWeight)
                .HasColumnName("KgsMail")
                .HasPrecision(18, 5)
                .IsOptional();

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

            this.Property(c => c.JetFuelArrival)
                .HasColumnName("JetFuelArrival")
                .HasPrecision(18, 5)
                .IsRequired();

            this.Property(c => c.Remarks)
                .HasColumnName("Remarks")
                .HasMaxLength(150)
                .IsOptional();

            this.Property(c => c.Closed)
                .HasColumnName("Closed")
                .IsRequired();

            this.HasRequired<Itinerary>(s => s.Itinerary)
                .WithOptional(s => s.ManifestArrival);

            this.HasMany<Delay>(e => e.Delays)
               .WithMany(e => e.ManifestArrivals)
               .Map(m => m.ToTable("ManifestArrivalDelay", "Itinerary")
               .MapRightKey("DelayCode")
               .MapLeftKey(new[] 
               { 
                   "Sequence", 
                   "AirlineCode", 
                   "FlightNumber", 
                   "ItineraryKey" 
               }));
        }
    }
}
