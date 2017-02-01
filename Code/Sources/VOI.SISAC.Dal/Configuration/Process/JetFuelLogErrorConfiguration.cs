//------------------------------------------------------------------------
// <copyright file="JetFuelLogErrorConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Jet fuel log error configuration
    /// </summary>
    public class JetFuelLogErrorConfiguration : EntityTypeConfiguration<JetFuelLogError>
    {
        /// <summary>
        /// The schema name
        /// </summary>
        private string schemaName = "Process";

        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelLogErrorConfiguration"/> class.
        /// </summary>
        public JetFuelLogErrorConfiguration()
        {
            // Defines the table's name and schema
            this.ToTable("JetFuelLogError", this.schemaName);

            // Defines the primary key
            this.HasKey<long>(c => c.LogId);

            // Defines the columns' properties
            this.Property(c => c.LogId)
                .HasColumnName("LogID")
                .IsRequired();

            this.Property(c => c.PeriodCode)
                .HasColumnName("PeriodCode")
                .HasMaxLength(30)
                .IsRequired();

            this.Property(c => c.LineNumber)
                .HasColumnName("LineNumber")
                .IsRequired();

            this.Property(c => c.Description)
                .HasColumnName("Description")
                .HasMaxLength(100)
                .IsRequired();

            this.Property(c => c.ItinerarySequence)
                .HasColumnName("Sequence")
                .IsRequired();

            this.Property(c => c.ItineraryAirlineCode)
                .HasColumnName("AirlineCode")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.ItineraryFlightNumber)
                .HasColumnName("FlightNumber")
                .HasMaxLength(5)
                .IsRequired();

            this.Property(c => c.ItineraryKey)
                .HasColumnName("ItineraryKey")
                .HasMaxLength(8)
                .IsRequired();

            this.Property(c => c.AirplaneEquipmentNumber)
                .HasColumnName("EquipmentNumber")
                .HasMaxLength(12)
                .IsRequired();

            this.Property(c => c.OperationTypeId)
                .HasColumnName("OperationTypeID")
                .IsOptional();

            this.Property(c => c.JetFuelTicketId)
                .HasColumnName("JetFuelTicketID")
                .IsRequired();

            this.Property(c => c.FuelingDate)
                .HasColumnName("FuelingDate")
                .IsOptional();

            this.Property(c => c.TicketNumber)
                .HasColumnName("TicketNumber")
                .HasMaxLength(10)
                .IsOptional();

            this.Property(c => c.FueledQuantityGallon)
                .HasColumnName("FueledQtyGals")
                .IsOptional();

            this.Property(c => c.ServiceCode)
                .HasColumnName("ServiceCode")
                .HasMaxLength(12)
                .IsOptional();

            this.Property(c => c.ProviderNumberPrimary)
                .HasColumnName("ProviderNumberPrimary")
                .HasMaxLength(10)
                .IsOptional();

            this.Property(c => c.InternationalFuelContractConceptId)
                .HasColumnName("InternationalFuelContractConceptID")
                .IsOptional();

            this.Property(c => c.FuelConceptId)
                .HasColumnName("FuelConceptID")
                .IsOptional();

            this.Property(c => c.FuelConceptTypeCode)
                .HasColumnName("FuelConceptTypeCode")
                .HasMaxLength(5)
                .IsOptional();

            this.Property(c => c.ChargeFactorTypeId)
                .HasColumnName("ChargeFactorTypeID")
                .IsOptional();

            this.Property(c => c.ProviderNumber)
                .HasColumnName("ProviderNumber")
                .HasMaxLength(10)
                .IsOptional();

            this.Property(c => c.Rate)
                .HasColumnName("Rate")
                .HasPrecision(18, 5)
                .IsOptional();

            // Defines the relationship
            this.HasRequired(c => c.JetFuelProcess)
                .WithMany(e => e.JetFuelLogErrors)
                .HasForeignKey(c => c.PeriodCode);
        }
    }
}
