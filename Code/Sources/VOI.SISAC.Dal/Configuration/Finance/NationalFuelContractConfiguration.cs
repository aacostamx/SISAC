// <copyright file="NationalFuelContractConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// International Fuel Contract Configuration
    /// </summary>
    public class NationalFuelContractConfiguration : EntityTypeConfiguration<NationalFuelContract>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelContractConfiguration"/> class.
        /// </summary>
        public NationalFuelContractConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("NationalFuelContract", "Finance");

            // Primary key
            this.HasKey(c => new 
            { 
                c.EffectiveDate, 
                c.AirlineCode, 
                c.StationCode, 
                c.ServiceCode, 
                c.ProviderNumberPrimary 
            });

            // Properties
            this.Property(e => e.EffectiveDate)
                .IsRequired()
                .HasColumnName("EffectiveDate");

            this.Property(e => e.AirlineCode)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnName("AirlineCode");

            this.Property(e => e.StationCode)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("StationCode");

            this.Property(e => e.ServiceCode)
                .IsRequired()
                .HasMaxLength(12)
                .HasColumnName("ServiceCode");

            this.Property(e => e.ProviderNumberPrimary)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("ProviderNumberPrimary");

            this.Property(e => e.AircraftRegistCCFlag)
                .IsRequired()
                .HasColumnName("AircraftRegistCCFlag");

            this.Property(e => e.CostCenterNumber)
                .IsOptional()
                .HasMaxLength(8)
                .HasColumnName("CCNumber");

            this.Property(e => e.AccountingAccountNumber)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("AccountingAccountNumber");

            this.Property(e => e.LiabilityAccountNumber)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("LiabilityAccountNumber");

            this.Property(e => e.CurrencyCode)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("CurrencyCode");

            this.Property(e => e.FederalTaxCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("FederalTaxCode");

            this.Property(e => e.FederalTaxValue)
                .IsRequired()
                .HasPrecision(7, 2)
                .HasColumnName("FederalTaxValue");

            this.Property(e => e.FederalTaxFlag)
                .IsRequired()
                .HasColumnName("FederalTaxFlag");

            // Relationships
            // Contract has many Concepts
            this.HasMany(e => e.NationalFuelContractConcept)
                .WithRequired(e => e.NationalFuelContract)
                .HasForeignKey(e => new { e.EffectiveDate, e.AirlineCode, e.StationCode, e.ServiceCode, e.ProviderNumberPrimary })
                .WillCascadeOnDelete(true);

            this.RegisterPrimaryKeyRelationships();
            this.RegisterRequiredRelationships();
        }

        /// <summary>
        /// Registers the required relationships.
        /// </summary>
        private void RegisterRequiredRelationships()
        {
            this.HasRequired(c => c.Currency)
                .WithMany(s => s.NationalFuelContracts)
                .HasForeignKey(c => c.CurrencyCode);

            this.HasOptional(c => c.CostCenter)
                .WithMany(s => s.NationalFuelContracts)
                .HasForeignKey(c => c.CostCenterNumber);

            this.HasRequired(c => c.AccountingAccount)
                .WithMany(s => s.NationalFuelContracts)
                .HasForeignKey(c => c.AccountingAccountNumber);

            this.HasRequired(c => c.LiabilityAccount)
                .WithMany(s => s.NationalFuelContracts)
                .HasForeignKey(c => c.LiabilityAccountNumber);

            this.HasRequired(c => c.FederalTax)
                .WithMany(s => s.NationalFuelContracts)
                .HasForeignKey(c => c.FederalTaxCode);
        }

        /// <summary>
        /// Registers the primary key relationships.
        /// </summary>
        private void RegisterPrimaryKeyRelationships()
        {
            this.HasRequired(c => c.Airline)
                .WithMany(s => s.NationalFuelContracts)
                .HasForeignKey(c => c.AirlineCode);

            this.HasRequired(c => c.Airport)
                .WithMany(s => s.NationalFuelContracts)
                .HasForeignKey(c => c.StationCode);

            this.HasRequired(c => c.Service)
                .WithMany(s => s.NationalFuelContracts)
                .HasForeignKey(c => c.ServiceCode);

            this.HasRequired(c => c.Provider)
                .WithMany(s => s.NationalFuelContracts)
                .HasForeignKey(c => c.ProviderNumberPrimary);
        }
    }
}
