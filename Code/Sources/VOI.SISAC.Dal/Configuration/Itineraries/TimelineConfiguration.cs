//------------------------------------------------------------------------
// <copyright file="TimelineConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Itineraries;


    /// <summary>
    /// Timeline Configuration
    /// </summary>
    public class TimelineConfiguration : EntityTypeConfiguration<Timeline>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineConfiguration"/> class.
        /// </summary>
        public TimelineConfiguration()
        {
            this.Property(e => e.AirlineCode)
            .IsUnicode(false);

            this.Property(e => e.FlightNumber)
            .IsUnicode(false);

            this.Property(e => e.ItineraryKey)
            .IsUnicode(false);

            this.Property(e => e.PreviousAirlineCode)
            .IsUnicode(false);

            this.Property(e => e.PreviousFlightNumber)
            .IsUnicode(false);

            this.Property(e => e.PreviousItineraryKey)
            .IsUnicode(false);

            this.Property(e => e.NextAirlineCode)
            .IsUnicode(false);

            this.Property(e => e.NextFlightNumber)
            .IsUnicode(false);

            this.Property(e => e.NextItineraryKey)
            .IsUnicode(false);

            this.HasMany(e => e.TimelineMovements)
            .WithRequired(e => e.Timeline)
            .HasForeignKey(e => new { e.Sequence, e.AirlineCode, e.FlightNumber, e.ItineraryKey })
            .WillCascadeOnDelete(true);
        }
    }
}