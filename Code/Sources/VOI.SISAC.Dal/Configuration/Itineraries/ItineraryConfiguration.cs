//------------------------------------------------------------------------
// <copyright file="ItineraryConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using Entities.Airport;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Itinerary Configuration Object
    /// </summary>
    public class ItineraryConfiguration : EntityTypeConfiguration<Itinerary>
    {
        /// <summary>
        /// Itinerary Configuration Object
        /// </summary>
        public ItineraryConfiguration()
        {
            this.ToTable("Itinerary", "Itinerary");

            this.HasKey(c => new
            {
                c.Sequence,
                c.AirlineCode,
                c.FlightNumber,
                c.ItineraryKey
            });

            this.Property(c => c.Sequence)
                .IsRequired()
                .HasColumnName("Sequence");

            this.Property(c => c.AirlineCode)
                .IsRequired()
                .HasColumnName("AirlineCode");

            this.Property(c => c.FlightNumber)
                .IsMaxLength()
                .IsRequired()
                .HasColumnName("FlightNumber");

            this.Property(c => c.ItineraryKey)
                .IsRequired()
                .HasColumnName("ItineraryKey");

            this.Property(c => c.DepartureDate)
                .IsRequired()
                .HasColumnName("DepartureDate");

            this.Property(c => c.DepartureStation)
                .IsRequired()
                .HasColumnName("DepartureStation");

            this.Property(c => c.ArrivalDate)
                .IsRequired()
                .HasColumnName("ArrivalDate");

            this.Property(c => c.ArrivalStation)
                .IsRequired()
                .HasColumnName("ArrivalStation");

            this.HasOptional<PassengerInformation>(s => s.PassengerInformation)
                .WithRequired(s => s.Itinerary);

            this.HasRequired<Airplane>(s => s.Airplane)
                .WithMany(s => s.Itinerary)
                .HasForeignKey(s => s.EquipmentNumber);

            this.HasOptional<GendecDeparture>(g => g.GendecDepartures)
                .WithRequired(g => g.Itinerary);

            this.HasOptional<GendecArrival>(i => i.GendecArrivals)
                .WithRequired(i => i.Itinerary);

            this.HasOptional(e => e.Timeline)
                .WithRequired(e => e.Itinerary);

            // Relationships
            this.HasMany(e => e.JetFuelTickets)
                .WithRequired(e => e.Itinerary)
                .HasForeignKey(e => new { e.Sequence, e.AirlineCode, e.FlightNumber, e.ItineraryKey })
                .WillCascadeOnDelete(true);
                
        }
    }
}