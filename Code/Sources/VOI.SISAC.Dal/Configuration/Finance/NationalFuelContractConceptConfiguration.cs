//--------------------------------------------------------------------
// <copyright file="NationalFuelContractConceptConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// International FuelContract Concept Configuration
    /// </summary>
    public class NationalFuelContractConceptConfiguration : EntityTypeConfiguration<NationalFuelContractConcept>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelContractConceptConfiguration"/> class.
        /// </summary>
        public NationalFuelContractConceptConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("NationalFuelContractConcept", "Finance");

            // Primary key
            this.HasKey(c => new 
            { 
                c.EffectiveDate, 
                c.AirlineCode,
                c.StationCode, 
                c.ServiceCode, 
                c.ProviderNumberPrimary, 
                c.NationalFuelContractConceptId 
            });

            // Properties
            this.Property(e => e.NationalFuelContractConceptId)
                .HasColumnName("NationalFuelContractConceptID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            
            this.Property(e => e.EffectiveDate)
                .HasColumnName("EffectiveDate")
                .IsRequired();

            this.Property(e => e.AirlineCode)
                .HasColumnName("AirlineCode")
                .HasMaxLength(2)
                .IsRequired()
                .IsUnicode(false);

            this.Property(e => e.StationCode)
                .HasColumnName("StationCode")
                .HasMaxLength(3)
                .IsRequired()
                .IsUnicode(false);

            this.Property(e => e.ServiceCode)
                .HasColumnName("ServiceCode")
                .HasMaxLength(12)
                .IsRequired()
                .IsUnicode(false);

            this.Property(e => e.ProviderNumberPrimary)
                .HasColumnName("ProviderNumberPrimary")
                .HasMaxLength(8)
                .IsRequired()
                .IsUnicode(false);

            this.Property(e => e.FuelConceptID)
                .HasColumnName("FuelConceptID")
                .IsRequired();

            this.Property(e => e.FuelConceptTypeCode)
                .HasColumnName("FuelConceptTypeCode")
                .IsRequired()
                .IsUnicode(false);

            this.Property(e => e.ChargeFactorTypeID)
                .HasColumnName("ChargeFactorTypeID")
                .IsRequired();
            
            this.Property(e => e.ProviderNumber)
                .HasColumnName("ProviderNumber")
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);

            // Relationships
            this.HasRequired(c => c.Provider)
                .WithMany(s => s.NationalFuelContractConcepts)
                .HasForeignKey(fk => fk.ProviderNumber);

            this.HasRequired(c => c.ChargeFactorType)
                .WithMany(s => s.NationalFuelContractConcepts)
                .HasForeignKey(fk => fk.ChargeFactorTypeID);

            this.HasRequired(c => c.FuelConcept)
                .WithMany(s => s.NationalFuelContractConcepts)
                .HasForeignKey(fk => fk.FuelConceptID);

            this.HasRequired(c => c.FuelConceptType)
                .WithMany(s => s.NationalFuelContractConcepts)
                .HasForeignKey(fk => fk.FuelConceptTypeCode);
        }
    }
}
