//------------------------------------------------------------------------
// <copyright file="CurrencyConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Database configuration for Currency entity.
    /// </summary>
    public class CurrencyConfiguration : EntityTypeConfiguration<Currency>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyConfiguration"/> class.
        /// </summary>
        public CurrencyConfiguration()
        {
            // Name of the Table
            this.ToTable("Currency", "Finance");

            // Primary Key
            this.HasKey<string>(s => s.CurrencyCode);

            // Relations for the properties
            this.Property(c => c.CurrencyCode)
                .HasMaxLength(3)
                .HasColumnName("CurrencyCode");

            this.Property(c => c.CurrencyName)
                .IsRequired()
                .HasMaxLength(40)                
                .HasColumnName("CurrencyName");

            this.Property(c => c.Status)
                .HasColumnName("Status");

            // Relationships
            this.HasMany(c => c.AirportServiceContracts)
                .WithRequired(s => s.Currency)
                .HasForeignKey(c => c.CurrencyCode);

            this.HasMany(e => e.ExchangeRates)
                .WithRequired(e => e.Currency)
                .HasForeignKey(c => c.CurrencyCode)
                .WillCascadeOnDelete(false);
        }
    }
}