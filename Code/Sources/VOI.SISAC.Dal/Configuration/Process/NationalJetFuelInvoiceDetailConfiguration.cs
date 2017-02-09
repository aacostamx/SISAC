//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceDetailConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using System.Data.Entity.ModelConfiguration;
    using Entities.Process;

    /// <summary>
    /// National Jet Fuel Invoice Control Configuration
    /// </summary>
    public class NationalJetFuelInvoiceDetailConfiguration : EntityTypeConfiguration<NationalJetFuelInvoiceDetail>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceDetailConfiguration"/> class.
        /// </summary>
        public NationalJetFuelInvoiceDetailConfiguration()
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

            this.Property(e => e.ServiceCode)
                .IsUnicode(false);

            this.Property(e => e.FederalTaxCode)
                .IsUnicode(false);

            this.Property(e => e.StationCode)
                .IsUnicode(false);

            this.Property(e => e.InvoiceNumber)
                .IsUnicode(false);

            this.Property(e => e.CustomerNumber)
                .IsUnicode(false);

            this.Property(e => e.CustomerName)
                .IsUnicode(false);

            this.Property(e => e.ElectronicInvoiceNumber)
                .IsUnicode(false);

            this.Property(e => e.ProductNumber)
                .IsUnicode(false);

            this.Property(e => e.ProductDescription)
                .IsUnicode(false);

            this.Property(e => e.TicketNumber)
                .IsUnicode(false);

            this.Property(e => e.OperationType)
                .IsUnicode(false);

            this.Property(e => e.FlightNumber)
                .IsUnicode(false);

            this.Property(e => e.EquipmentNumber)
                .IsUnicode(false);

            this.Property(e => e.AirplaneModel)
                .IsUnicode(false);

            this.Property(e => e.VolumeM3)
                .HasPrecision(18, 3);

            this.Property(e => e.RateType)
                .IsUnicode(false);

            this.Property(e => e.ErrorDescription)
                .IsUnicode(false);

            this.Property(e => e.Status)
                .IsUnicode(false);

            this.Property(e => e.ReconciliationStatus)
                .IsUnicode(false);
        }
    }
}
