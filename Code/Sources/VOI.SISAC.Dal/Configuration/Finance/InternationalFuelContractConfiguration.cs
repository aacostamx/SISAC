// <copyright file="InternationalFuelContractConfiguration.cs" company="AACOSTA">
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
    public class InternationalFuelContractConfiguration : EntityTypeConfiguration<InternationalFuelContract>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalFuelContractConfiguration"/> class.
        /// </summary>
        public InternationalFuelContractConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("InternationalFuelContract", "Finance");
            
            this.Property(e => e.EffectiveDate)
                .IsRequired()
                .HasColumnName("EffectiveDate");

            this.Property(e => e.AirlineCode)
                .IsRequired()
                .HasColumnName("AirlineCode");

            this.Property(e => e.StationCode)
                .IsRequired()
                .HasColumnName("StationCode");

            this.Property(e => e.ServiceCode)
                .IsRequired()
                .HasColumnName("ServiceCode");

            this.Property(e => e.ProviderNumberPrimary)
                .IsRequired()
                .HasColumnName("ProviderNumberPrimary");

            this.Property(e => e.AircraftRegistCCFlag)
                .IsRequired()
                .HasColumnName("AircraftRegistCCFlag");

            this.Property(e => e.CCNumber)                
                .HasColumnName("CCNumber");

            this.Property(e => e.AccountingAccountNumber)
                .IsRequired()
                .HasColumnName("AccountingAccountNumber");

            this.Property(e => e.LiabilityAccountNumber)
                .IsRequired()
                .HasColumnName("LiabilityAccountNumber");

            this.Property(e => e.CurrencyCode)
                .IsRequired()
                .HasColumnName("CurrencyCode");

            // Relationships
            // Contract has many Concepts
            this.HasMany(e => e.InternationalFuelContractConcepts)
                .WithRequired(e => e.InternationalFuelContract)
                .HasForeignKey(e => new { e.EffectiveDate, e.AirlineCode, e.StationCode, e.ServiceCode, e.ProviderNumberPrimary })
                .WillCascadeOnDelete(true);      
        }
    }
}
