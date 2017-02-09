//------------------------------------------------------------------------
// <copyright file="NationalJetFuelCostConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// NationalJetFuelCostConfiguration Class
    /// </summary>
    public class NationalJetFuelCostConfiguration : EntityTypeConfiguration<NationalJetFuelCost>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelCostConfiguration"/> class.
        /// </summary>
        public NationalJetFuelCostConfiguration()
        {
            this.Property(e => e.PeriodCode)
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

            this.Property(e => e.ConceptAmount)
            .HasPrecision(18, 5);

            this.Property(e => e.FederalTaxCode)
            .IsUnicode(false);

            this.Property(e => e.FederalTaxValue)
            .HasPrecision(7, 2);

            this.Property(e => e.FederalTaxAmount)
            .HasPrecision(8, 6);

            this.Property(e => e.CCNumber)
            .IsUnicode(false);

            this.Property(e => e.CurrencyCode)
            .IsUnicode(false);

            this.Property(e => e.AccountingAccountNumber)
            .IsUnicode(false);

            this.Property(e => e.LiabilityAccountNumber)
            .IsUnicode(false);

            this.Property(e => e.ReconciliationStatus)
            .IsUnicode(false);
        }
    }
}
