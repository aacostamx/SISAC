//------------------------------------------------------------------------
// <copyright file="PassengerInformationConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;
    using Entities.Itineraries;
    using Entities.Security;

    /// <summary>
    /// Data base configuration for Passenger Information
    /// </summary>
    public class PassengerInformationConfiguration : EntityTypeConfiguration<PassengerInformation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassengerInformationConfiguration"/> class.
        /// </summary>
        public PassengerInformationConfiguration()
        {
            this.ToTable("PassengerInformation", "Airport");

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
            this.CabinConfiguration();
            this.PassengerTypesConfiguration();
            this.BaggageQuantityConfiguration();
            this.BaggageWeightConfiguration();

            this.Property(t => t.Observation)
                .HasColumnName("Observation")
                .HasMaxLength(100)
                .IsRequired();

            this.Property(t => t.InternationalTua)
                .HasColumnName("InternationalTua")
                .IsOptional();

            this.Property(t => t.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            this.Property(t => t.PreviousSequence)
                .HasColumnName("PreviousSequence")
                .IsOptional();

            this.Property(t => t.PreviousAirlineCode)
                .HasColumnName("PreviousAirlineCode")
                .HasMaxLength(3)
                .IsOptional();

            this.Property(t => t.PreviousFlightNumber)
                .HasColumnName("PreviousFlightNumber")
                .HasMaxLength(5)
                .IsOptional();

            this.Property(t => t.PreviousItineraryKey)
                .HasColumnName("PreviousItineraryKey")
                .HasMaxLength(8)
                .IsOptional();
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

        /// <summary>
        /// Configuration for the passengers in cabins.
        /// </summary>
        private void CabinConfiguration()
        {
            this.Property(c => c.AdultsCabinA)
                .HasColumnName("AdultsCabinA")
                .IsRequired();

            this.Property(c => c.AdultsCabinB)
                .HasColumnName("AdultsCabinB")
                .IsRequired();

            this.Property(c => c.TeenageCabinA)
                .HasColumnName("TeenageCabinA")
                .IsRequired();

            this.Property(c => c.TeenageCabinB)
                .HasColumnName("TeenageCabinB")
                .IsRequired();

            this.Property(c => c.ChildrenCabinA)
                .HasColumnName("ChildrenCabinA")
                .IsRequired();

            this.Property(c => c.ChildrenCabinB)
                .HasColumnName("ChildrenCabinB")
                .IsRequired();
        }

        /// <summary>
        /// Passengers the types configuration.
        /// </summary>
        private void PassengerTypesConfiguration()
        {
            this.Property(c => c.LocalAdults)
                .HasColumnName("LocalAdults")
                .IsRequired();

            this.Property(c => c.LocalTeenage)
                .HasColumnName("LocalTeenage")
                .IsRequired();

            this.Property(c => c.LocalChildren)
                .HasColumnName("LocalChildren")
                .IsRequired();

            this.Property(c => c.TransitoryAdults)
                .HasColumnName("TransitoryAdults")
                .IsRequired();

            this.Property(c => c.TransitoryTeenage)
                .HasColumnName("TransitoryTeenage")
                .IsRequired();

            this.Property(c => c.TransitoryChildren)
                .HasColumnName("TransitoryChildren")
                .IsRequired();

            this.Property(c => c.ConnectionAdults)
                .HasColumnName("ConnectionAdults")
                .IsRequired();

            this.Property(c => c.ConnectionTeenage)
                .HasColumnName("ConnectionTeenage")
                .IsRequired();

            this.Property(c => c.ConnectionChildren)
                .HasColumnName("ConnectionChildren")
                .IsRequired();

            this.Property(c => c.Diplomatic)
                .HasColumnName("Diplomatic")
                .IsRequired();

            this.Property(c => c.ExtraCrew)
                .HasColumnName("ExtraCrew")
                .IsRequired();

            this.Property(c => c.Other)
                .HasColumnName("Other")
                .IsRequired();
        }

        /// <summary>
        /// Configuration baggages quantity.
        /// </summary>
        private void BaggageQuantityConfiguration()
        {
            this.Property(c => c.LocalBaggageQuantity)
                .HasColumnName("LocalBaggageQuantity")
                .IsRequired();

            this.Property(c => c.TransitoryBaggageQuantity)
                .HasColumnName("TransitoryBaggageQuantity")
                .IsRequired();

            this.Property(c => c.ConnectionBaggageQuantity)
                .HasColumnName("ConnectionBaggageQuantity")
                .IsRequired();

            this.Property(c => c.DiplomaticBaggageQuantity)
                .HasColumnName("DiplomaticBaggageQuantity")
                .IsRequired();

            this.Property(c => c.ExtraCrewBaggageQuantity)
                .HasColumnName("ExtraCrewBaggageQuantity")
                .IsRequired();

            this.Property(c => c.OtherBaggageQuantity)
                .HasColumnName("OtherBaggageQuantity")
                .IsRequired();
        }

        /// <summary>
        /// Configuration Baggages weight.
        /// </summary>
        private void BaggageWeightConfiguration()
        {
            this.Property(c => c.LocalBaggageWeight)
                .HasColumnName("LocalBaggageWeight")
                .IsRequired();

            this.Property(c => c.TransitoryBaggageWeight)
                .HasColumnName("TransitoryBaggageWeight")
                .IsRequired();

            this.Property(c => c.ConnectionBaggageWeight)
                .HasColumnName("ConnectionBaggageWeight")
                .IsRequired();

            this.Property(c => c.DiplomaticBaggageWeight)
                .HasColumnName("DiplomaticBaggageWeight")
                .IsRequired();

            this.Property(c => c.ExtraCrewBaggageWeight)
                .HasColumnName("ExtraCrewBaggageWeight")
                .IsRequired();

            this.Property(c => c.OtherBaggageWeight)
                .HasColumnName("OtherBaggageWeight")
                .IsRequired();

            this.HasRequired<Itinerary>(s => s.Itinerary)
                .WithOptional(s => s.PassengerInformation);

            this.HasRequired<User>(t => t.User)
                .WithMany(f => f.PassengerInformation)
                .HasForeignKey(f => f.UserId);
        }
    }
}
