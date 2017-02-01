//------------------------------------------------------------------------
// <copyright file="JetFuelProvisionConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Jet fuel provision configuration
    /// </summary>
    public class JetFuelProvisionConfiguration : EntityTypeConfiguration<JetFuelProvision>
    {
        /// <summary>
        /// The schema name
        /// </summary>
        private string schemaName = "Process";

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelProvisionConfiguration"/> class.
        /// </summary>
        public JetFuelProvisionConfiguration()
        {
            // Defines the table
            this.ToTable("JetFuelProvision", this.schemaName);

            // Defines the primary key
            this.HasKey(c => new
            {
                c.ProvisionId,
                c.PeriodCode
            });

            // Defines the columns' properties
            this.Property(c => c.ProvisionId)
                .HasColumnName("ProvisionId")
                .IsRequired();

            this.Property(c => c.PeriodCode)
                .HasColumnName("PeriodCode")
                .HasMaxLength(30)
                .IsRequired();

            this.Property(c => c.Sequence)
                .HasColumnName("Sequence")
                .IsRequired();

            this.Property(c => c.AirlineCode)
                .HasColumnName("AirlineCode")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.FlightNumber)
                .HasColumnName("FlightNumber")
                .HasMaxLength(5)
                .IsRequired();

            this.Property(c => c.ItineraryKey)
                .HasColumnName("ItineraryKey")
                .HasMaxLength(8)
                .IsRequired();

            this.Property(c => c.EquipmentNumber)
                .HasColumnName("EquipmentNumber")
                .HasMaxLength(12)
                .IsRequired();

            this.Property(c => c.OperationTypeId)
                .HasColumnName("OperationTypeID")
                .IsRequired();

            this.Property(c => c.JetFuelTicketID)
                .HasColumnName("JetFuelTicketID")
                .IsRequired();

            this.Property(c => c.FuelingDate)
                .HasColumnName("FuelingDate")
                .IsRequired();

            this.Property(c => c.FuelingTime)
                .HasColumnName("FuelingTime")
                .IsRequired();

            this.Property(c => c.ApronPosition)
                .HasColumnName("ApronPosition")
                .HasMaxLength(8)
                .IsRequired();

            this.Property(c => c.TicketNumber)
                .HasColumnName("TicketNumber")
                .HasMaxLength(10)
                .IsRequired();

            this.Property(c => c.FueledQuantityGallon)
                .HasColumnName("FueledQtyGals")
                .IsRequired();

            this.Property(c => c.RemainingQuantityKiloGram)
                .HasColumnName("RemainingQry")
                .IsOptional();

            this.Property(c => c.RequestedQuantityKiloGram)
                .HasColumnName("RequestedQry")
                .IsOptional();

            this.Property(c => c.FueledQuantityKiloGram)
                .HasColumnName("FueledQry")
                .IsOptional();

            this.Property(c => c.DensityFactor)
                .HasColumnName("DensityFactor")
                .HasPrecision(8, 3)
                .IsOptional();

            this.Property(c => c.ServiceCode)
                .HasColumnName("ServiceCode")
                .HasMaxLength(12)
                .IsRequired();

            this.Property(c => c.ProviderNumberPrimary)
                .HasColumnName("ProviderNumberPrimary")
                .HasMaxLength(10)
                .IsRequired();

            this.Property(c => c.CurrencyCode)
                .HasColumnName("CurrencyCode")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.CostCenterNumber)
                .HasColumnName("CCNumber")
                .HasMaxLength(12)
                .IsRequired();

            this.Property(c => c.AccountingAccountNumber)
                .HasColumnName("AccountingAccountNumber")
                .HasMaxLength(8)
                .IsRequired();

            this.Property(c => c.LiabilityAccountNumber)
                .HasColumnName("LiabilityAccountNumber")
                .HasMaxLength(8)
                .IsRequired();

            this.Property(c => c.InternationalFuelContractConceptId)
                .HasColumnName("InternationalFuelContractConceptID")
                .IsRequired();

            this.Property(c => c.FuelConceptId)
                .HasColumnName("FuelConceptID")
                .IsRequired();

            this.Property(c => c.FuelConceptTypeCode)
                .HasColumnName("FuelConceptTypeCode")
                .HasMaxLength(5)
                .IsRequired();

            this.Property(c => c.ChargeFactorTypeId)
                .HasColumnName("ChargeFactorTypeID")
                .IsRequired();

            this.Property(c => c.ProviderNumber)
                .HasColumnName("ProviderNumber")
                .HasMaxLength(10)
                .IsRequired();

            this.Property(c => c.Rate)
                .HasColumnName("Rate")
                .HasPrecision(18, 5)
                .IsRequired();

            this.Property(c => c.ConceptAmount)
                .HasColumnName("ConceptAmount")
                .HasPrecision(18, 5)
                .IsRequired();

            this.Property(c => c.PolicyID)
                .HasColumnName("PolicyID")
                .IsOptional();

            // Defines the relationship
            this.HasRequired(c => c.JetFuelProcess)
                .WithMany(e => e.JetFuelProvisions)
                .HasForeignKey(c => c.PeriodCode);

            ////this.HasOptional(c => c.JetFuelPolicyControl)
            ////    .WithMany(e => e.JetFuelProvisions)
            ////    .HasForeignKey(c => c.PolicyID);
        }
    }
}
