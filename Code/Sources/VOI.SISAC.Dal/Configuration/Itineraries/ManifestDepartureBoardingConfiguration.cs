//--------------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// ManifestDepartureBoardingConfiguration
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{VOI.SISAC.Entities.Itineraries.ManifestDepartureBoarding}" />
    public class ManifestDepartureBoardingConfiguration : EntityTypeConfiguration<ManifestDepartureBoarding>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ManifestDepartureBoardingConfiguration()
        {
            //this.HasKey<long>(e => e.BoardingID);

            Property(e => e.AirlineCode)
                .IsUnicode(false);

            Property(e => e.FlightNumber)
                .IsUnicode(false);

            Property(e => e.ItineraryKey)
                .IsUnicode(false);

            Property(e => e.Station)
                .IsUnicode(false);

            this.Property(e => e.LuggageKgs)
                .HasPrecision(13, 5)
                .IsRequired()
                .HasColumnName("LuggageKgs");

            this.Property(e => e.ChargeKgs)
                .HasPrecision(13, 5)
                .IsRequired()
                .HasColumnName("ChargeKgs");

            this.Property(e => e.MailKgs)
                .HasPrecision(13, 5)
                .IsRequired()
                .HasColumnName("MailKgs");

            this.HasMany(e => e.ManifestDepartureBoardingDetails)
                .WithRequired(e => e.ManifestDepartureBoarding)
                .HasForeignKey(e => e.BoardingID)
                .WillCascadeOnDelete(true);

            this.HasMany(e => e.ManifestDepartureBoardingInformations)
                .WithRequired(e => e.ManifestDepartureBoarding)
                .HasForeignKey(e => e.BoardingID)
                .WillCascadeOnDelete(true);
        }
    }
}
