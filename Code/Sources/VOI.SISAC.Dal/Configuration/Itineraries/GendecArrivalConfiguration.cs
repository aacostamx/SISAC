//------------------------------------------------------------------------
// <copyright file="GendecArrivalConfiguration.cs" company="AACOSTA">
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
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Gendec Arrival Configuration
    /// </summary>
    public class GendecArrivalConfiguration : EntityTypeConfiguration<GendecArrival>
    {
        /// <summary>
        /// GendecDeparture Configuration Object
        /// </summary>
        public GendecArrivalConfiguration()
        {
            this.ToTable("GendecArrival", "Itinerary");

            this.HasKey(c => new
            {
                c.Sequence,
                c.AirlineCode,
                c.FlightNumber,
                c.Itinerarykey
            });


            this.Property(c => c.Sequence)
                .IsRequired()
                .HasColumnName("Sequence");

            this.Property(c => c.AirlineCode)
                .IsRequired()
                .HasColumnName("AirlineCode");

            this.Property(c => c.FlightNumber)
                .IsRequired()
                .HasColumnName("FlightNumber");

            this.Property(c => c.Itinerarykey)
                .IsRequired()
                .HasColumnName("Itinerarykey");

            this.Property(c => c.TotalPax)
                .HasColumnName("TotalPax");

            this.Property(c => c.TotalCrew)
                .HasColumnName("TotalCrew");

            this.Property(c => c.BlockTime)
                .HasColumnName("BlockTime");

            this.Property(c => c.ManifestNumber)
                .HasColumnName("ManifestNumber");

            this.Property(c => c.GateNumber)
                .HasColumnName("GateNumber");

            this.Property(c => c.ActualTimeOfArrival)
                .HasColumnName("ActualTimeOfArrival");

            this.Property(c => c.ArrivalPlace)
                .HasColumnName("ArrivalPlace");

            this.Property(c => c.Disembanking)
                .HasColumnName("Disembanking");

            this.Property(c => c.FlightArrivalDescription)
                .HasColumnName("FlightArrivalDescription");

            this.Property(c => c.Member)
                .HasColumnName("Member");

            this.Property(c => c.AuthorizedAgent)
                .HasColumnName("AuthorizedAgent");

            this.Property(c => c.Remarks)
                .HasColumnName("Remarks");

            this.Property(c => c.Closed)
                .HasColumnName("Closed");
        }
    }
}