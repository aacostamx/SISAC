//------------------------------------------------------------------------
// <copyright file="NationalFuelRateConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Configuration for National Fuel Rate
    /// </summary>
    public class NationalFuelRateConfiguration : EntityTypeConfiguration<NationalFuelRate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelRateConfiguration"/> class.
        /// </summary>
        public NationalFuelRateConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("NationalFuelRate", "Finance");

            // Primary key
            this.HasKey(c => c.NationalFuelRateId);

            // Properties
            this.Property(c => c.NationalFuelRateId)
                .HasColumnName("NationalRateID")
                .IsRequired();

            this.Property(e => e.StationCode)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("StationCode");

            this.Property(e => e.ServiceCode)
                .IsRequired()
                .HasMaxLength(12)
                .HasColumnName("ServiceCode");

            this.Property(e => e.ProviderNumber)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("ProviderNumber");

            this.Property(e => e.EffectiveStartDate)
                .IsRequired()
                .HasColumnName("EffectiveStartDate");

            this.Property(e => e.EffectiveEndDate)
                .IsRequired()
                .HasColumnName("EffectiveEndDate");

            this.Property(e => e.ScheduleTypeCode)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("ScheduleTypeCode");

            this.Property(e => e.FuelConceptTypeCode)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("FuelConceptTypeCode");

            this.Property(e => e.RateValue)
                .IsRequired()
                .HasPrecision(8, 6)
                .HasColumnName("RateValue");

            this.Property(e => e.CurrencyCode)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("CurrencyCode");

            // Relationships
            this.RegisterRequiredRelationships();
        }

        /// <summary>
        /// Registers the required relationships.
        /// </summary>
        private void RegisterRequiredRelationships()
        {
            this.HasRequired(c => c.Airport)
                .WithMany(s => s.NationalFuelRates)
                .HasForeignKey(c => c.StationCode);

            this.HasRequired(c => c.Currency)
                .WithMany(s => s.NationalFuelRates)
                .HasForeignKey(c => c.CurrencyCode);

            this.HasRequired(c => c.Service)
                .WithMany(s => s.NationalFuelRates)
                .HasForeignKey(c => c.ServiceCode);

            this.HasRequired(c => c.Provider)
                .WithMany(s => s.NationalFuelRates)
                .HasForeignKey(c => c.ProviderNumber);

            this.HasRequired(c => c.FuelConceptType)
                .WithMany(s => s.NationalFuelRates)
                .HasForeignKey(c => c.FuelConceptTypeCode);

            this.HasRequired(c => c.ScheduleType)
                .WithMany(s => s.NationalFuelRates)
                .HasForeignKey(c => c.ScheduleTypeCode);
        }
    }
}
