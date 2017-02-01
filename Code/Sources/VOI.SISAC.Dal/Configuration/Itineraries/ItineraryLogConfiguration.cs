//------------------------------------------------------------------------
// <copyright file="ItineraryConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Itinerary Log Configuration Object
    /// </summary>
    public class ItineraryLogConfiguration : EntityTypeConfiguration<ItineraryLog>
    {
        /// <summary>
        /// Itinerary Log Configuration Object
        /// </summary>
        public ItineraryLogConfiguration()
        {
            this.ToTable("ItineraryLog", "Itinerary");

            this.Property(c => c.Sequence)
                .IsRequired()
                .HasColumnName("Sequence");

            this.Property(c => c.AirlineCode)
              .IsRequired()
              .HasColumnName("AirlineCode");

            this.Property(c => c.FlightNumber)
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

            this.Property(c => c.EndDate)
                .IsRequired()
                .HasColumnName("EndDate");

            this.Property(c => c.Remarks)
                .IsRequired()
                .HasColumnName("Remarks");
        }
    }
}
