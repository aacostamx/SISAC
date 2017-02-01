//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceControlConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using System.Data.Entity.ModelConfiguration;
    using Entities.Process;

    /// <summary>
    /// National Jet Fuel Invoice Control Configuration
    /// </summary>
    public class NationalJetFuelInvoiceControlConfiguration : EntityTypeConfiguration<NationalJetFuelInvoiceControl>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceControlConfiguration"/> class.
        /// </summary>
        public NationalJetFuelInvoiceControlConfiguration()
        {
            this.Property(e => e.RemittanceID)
            .IsUnicode(false);

            this.Property(e => e.MonthYear)
            .IsUnicode(false);

            this.Property(e => e.Period)
            .IsUnicode(false);

            this.Property(e => e.AirlineCode)
            .IsUnicode(false);

            this.Property(e => e.ProviderNumber)
            .IsUnicode(false);

            this.Property(e => e.JetFuelAmount)
            .HasPrecision(10, 2);

            this.Property(e => e.FreightAmount)
            .HasPrecision(10, 2);

            this.Property(e => e.DiscountAmount)
            .HasPrecision(10, 2);

            this.Property(e => e.FuelingAmount)
            .HasPrecision(10, 2);

            this.Property(e => e.SubtotalAmount)
            .HasPrecision(10, 2);

            this.Property(e => e.TaxAmount)
            .HasPrecision(10, 2);

            this.Property(e => e.TotalAmount)
            .HasPrecision(10, 2);

            this.Property(e => e.DocumentStatusCode)
            .IsUnicode(false);

            this.Property(e => e.Society)
            .IsUnicode(false);

            this.Property(e => e.ReconciliationStatusCode)
            .IsUnicode(false);

            this.Property(e => e.RemittanceStatusCode)
            .IsUnicode(false);

            this.Property(e => e.ReconciledSubtotalAmount)
            .HasPrecision(18, 5);

            this.Property(e => e.NonconformitySubtotalAmount)
            .HasPrecision(18, 5);

            this.Property(e => e.NonconformityReference)
            .IsUnicode(false);

            this.Property(e => e.StatusProcessCode)
            .IsUnicode(false);

            this.Property(e => e.ProcessProgress)
            .HasPrecision(5, 2);

            this.Property(e => e.CalculationStatusCode)
            .IsUnicode(false);

            this.Property(e => e.ConfirmationStatusCode)
            .IsUnicode(false);

            this.Property(e => e.NonconformityStatusCode)
                .IsUnicode(false);
            
            this.HasMany(e => e.NationalJetFuelInvoiceDetails)
            .WithRequired(e => e.NationalJetFuelInvoiceControl)
            .HasForeignKey(e => new { e.RemittanceID, e.MonthYear, e.Period })
            .WillCascadeOnDelete(false);

            this.HasMany(e => e.NationalJetFuelInvoicePolicies)
            .WithRequired(e => e.NationalJetFuelInvoiceControl)
            .HasForeignKey(e => new { e.RemittanceId, e.MonthYear, e.Period })
            .WillCascadeOnDelete(false);
        }

    }
}
