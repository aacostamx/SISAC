//------------------------------------------------------------------------
// <copyright file="NationalJetFuelLogErrorConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// NationalJetFuelLogErrorConfiguration
    /// </summary>
    public class NationalJetFuelLogErrorConfiguration : EntityTypeConfiguration<NationalJetFuelLogError>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelLogErrorConfiguration"/> class.
        /// </summary>
        public NationalJetFuelLogErrorConfiguration()
        {
            this.Property(e => e.PeriodCode)
            .IsUnicode(false);

            this.Property(e => e.Description)
            .IsUnicode(false);

            this.Property(e => e.AirlineCode)
            .IsUnicode(false);

            this.Property(e => e.FlightNumber)
            .IsUnicode(false);

            this.Property(e => e.ItineraryKey)
            .IsUnicode(false);

            this.Property(e => e.EquipmentNumber)
            .IsUnicode(false);

            this.Property(e => e.ServiceCode)
            .IsUnicode(false);

            this.Property(e => e.ProviderNumberPrimary)
            .IsUnicode(false);

            this.Property(e => e.ApronPosition)
            .IsUnicode(false);

            this.Property(e => e.TicketNumber)
            .IsUnicode(false);

            this.Property(e => e.DensityFactor)
            .HasPrecision(8, 3);

            this.Property(e => e.FuelConceptTypeCode)
            .IsUnicode(false);

            this.Property(e => e.ProviderNumber)
            .IsUnicode(false);

            this.Property(e => e.ScheduleTypeCode)
            .IsUnicode(false);

            this.Property(e => e.Rate)
            .HasPrecision(18, 5);
        }
    }
}
