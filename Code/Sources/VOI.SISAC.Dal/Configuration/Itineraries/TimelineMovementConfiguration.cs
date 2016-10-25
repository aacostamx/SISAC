//------------------------------------------------------------------------
// <copyright file="TimelineMovementConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Itineraries;


    /// <summary>
    /// TimelineMovement Configuration
    /// </summary>
    public class TimelineMovementConfiguration : EntityTypeConfiguration<TimelineMovement>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineMovementConfiguration"/> class.
        /// </summary>
        public TimelineMovementConfiguration()
        {
            this.Property(e => e.AirlineCode)
            .IsUnicode(false);

            this.Property(e => e.FlightNumber)
            .IsUnicode(false);

            this.Property(e => e.ItineraryKey)
            .IsUnicode(false);

            this.Property(e => e.MovementTypeCode)
            .IsUnicode(false);

            this.Property(e => e.Position)
            .IsUnicode(false);

            this.Property(e => e.ProviderNumber)
            .IsUnicode(false);

            this.Property(e => e.RemainingFuel)
            .HasPrecision(18, 5);

            this.Property(e => e.Remarks)
            .IsUnicode(false);
        }
    }
}