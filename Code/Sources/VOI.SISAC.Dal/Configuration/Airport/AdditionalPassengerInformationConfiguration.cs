//------------------------------------------------------------------------
// <copyright file="AdditionalPassengerInformationConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Data base configuration for Additional Passenger Information
    /// </summary>
    public class AdditionalPassengerInformationConfiguration : EntityTypeConfiguration<AdditionalPassengerInformation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalPassengerInformationConfiguration"/> class.
        /// </summary>
        public AdditionalPassengerInformationConfiguration()
        {
            this.ToTable("AdditionalPassengerInformation", "Airport");

            // Primary key
            this.HasKey(t => new
            {
                t.Sequence,
                t.AirlineCode,
                t.FlightNumber,
                t.ItineraryKey
            });

            // Configuration for the properties            
            this.PrimaryKeyConfiguration();

            // Properties configuration
            this.Property(c => c.AdultInternational)
                .HasColumnName("AdultInternational")
                .IsRequired();

            this.Property(c => c.AdultNational)
                .HasColumnName("AdultNational")
                .IsRequired();

            this.Property(c => c.MinorInternational)
                .HasColumnName("MinorInternational")
                .IsRequired();

            this.Property(c => c.MinorNational)
                .HasColumnName("MinorNational")
                .IsRequired();

            this.Property(c => c.DiplomaticInternational)
                .HasColumnName("DiplomaticInternational")
                .IsRequired();

            this.Property(c => c.DiplomaticNational)
                .HasColumnName("DiplomaticNational")
                .IsRequired();

            this.Property(c => c.CommissionInternational)
                .HasColumnName("CommissionInternational")
                .IsRequired();

            this.Property(c => c.CommissionNational)
                .HasColumnName("CommissionNational")
                .IsRequired();

            this.Property(c => c.InfantInternational)
                .HasColumnName("InfantInternational")
                .IsRequired();

            this.Property(c => c.InfantNational)
                .HasColumnName("InfantNational")
                .IsRequired();

            this.Property(c => c.TransitoryInternational)
                .HasColumnName("TransitoryInternational")
                .IsRequired();

            this.Property(c => c.TransitoryNational)
                .HasColumnName("TransitoryNational")
                .IsRequired();

            this.Property(c => c.ConnectionInternational)
                .HasColumnName("ConnectionInternational")
                .IsRequired();

            this.Property(c => c.ConnectionNational)
                .HasColumnName("ConnectionNational")
                .IsRequired();

            this.Property(c => c.OtherInternational)
                .HasColumnName("OtherInternational")
                .IsRequired();

            this.Property(c => c.OtherNational)
                .HasColumnName("OtherNational")
                .IsRequired();

            this.Property(c => c.PaxDni)
                .HasColumnName("PaxDni")
                .IsRequired();

            this.HasRequired(c => c.PassengerInformation)
                .WithOptional(f => f.AdditionalPassengerInformation)
                .WillCascadeOnDelete(true);
        }

        /// <summary>
        /// Configuration for the Primary keys.
        /// </summary>
        private void PrimaryKeyConfiguration()
        {
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
        }
    }
}
