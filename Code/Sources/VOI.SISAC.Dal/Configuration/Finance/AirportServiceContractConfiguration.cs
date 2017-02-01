//---------------------------------------------------------------------------------
// <copyright file="AirportServiceContractConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Data base configuration for AirportServiceContract
    /// </summary>
    public class AirportServiceContractConfiguration : EntityTypeConfiguration<AirportServiceContract>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirportServiceContractConfiguration"/> class.
        /// </summary>
        public AirportServiceContractConfiguration()
        {
            // Name of the table
            this.ToTable("AirportServiceContract", "Finance");

            // Primary key
            this.HasKey(t => new 
            { 
                t.EffectiveDate, 
                t.AirlineCode, 
                t.StationCode, 
                t.ServiceCode, 
                t.ProviderNumber, 
                t.CostCenterNumber 
            });

            // Configurations for the properties
            this.Property(c => c.EffectiveDate)
                .HasColumnName("EffectiveDate")
                .IsRequired();

            this.Property(c => c.AirlineCode)
                .HasColumnName("AirlineCode")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.StationCode)
                .HasColumnName("StationCode")
                .HasMaxLength(3)
                .IsRequired();

            this.Property(c => c.ServiceCode)
                .HasColumnName("ServiceCode")
                .HasMaxLength(12)
                .IsRequired();

            this.Property(c => c.ProviderNumber)
                .HasColumnName("ProviderNumber")
                .HasMaxLength(8)
                .IsRequired();

            this.Property(c => c.CostCenterNumber)
                .HasColumnName("CCNumber")
                .HasMaxLength(12)
                .IsRequired();

            this.Property(c => c.Status)
                .HasColumnName("Status")
                .IsRequired();

            this.Property(c => c.AccountingAccountNumber)
                .HasColumnName("AccountingAccountNumber")
                .HasMaxLength(8)
                .IsRequired();

            this.Property(c => c.LiabilityAccountNumber)
                .HasColumnName("LiabilityAccountNumber")
                .HasMaxLength(8)
                .IsRequired();

            this.Property(c => c.OperationTypeId)
                .HasColumnName("OperationTypeID")
                .IsRequired();

            this.Property(c => c.ServiceTypeCode)
                .HasColumnName("ServiceTypeCode")
                .HasMaxLength(1)
                .IsRequired();

            this.Property(c => c.AirportFeeCode)
                .HasColumnName("PortFeeCode")
                .HasMaxLength(8)
                .IsOptional();

            this.Property(c => c.AirportFeeFlag)
                .HasColumnName("PortFeeFlag")
                .IsOptional();

            this.Property(c => c.AirportFeeValue)
                .HasColumnName("PortFeeValue")
                .HasPrecision(7, 2)
                .IsOptional();

            this.Property(c => c.LocalTaxCode)
                .HasColumnName("LocalTaxCode")
                .HasMaxLength(8)
                .IsOptional();

            this.Property(c => c.LocalTaxFlag)
                .HasColumnName("LocalTaxFlag")
                .IsOptional();

            this.Property(c => c.LocalTaxValue)
                .HasColumnName("LocalTaxValue")
                .HasPrecision(7, 2)
                .IsOptional();

            this.Property(c => c.StateTaxCode)
                .HasColumnName("StateTaxCode")
                .HasMaxLength(8)
                .IsOptional();

            this.Property(c => c.StateTaxFlag)
                .HasColumnName("StateTaxFlag")
                .IsOptional();

            this.Property(c => c.StateTaxValue)
                .HasColumnName("StateTaxValue")
                .HasPrecision(7, 2)
                .IsOptional();

            this.Property(c => c.FederalTaxCode)
                .HasColumnName("FederalTaxCode")
                .HasMaxLength(8)
                .IsOptional();

            this.Property(c => c.FederalTaxFlag)
                .HasColumnName("FederalTaxFlag")
                .IsOptional();

            this.Property(c => c.FederalTaxValue)
                .HasColumnName("FederalTaxValue")
                .HasPrecision(7, 2)
                .IsOptional();

            this.Property(c => c.AirplanetWeightFlag)
                .HasColumnName("AircraftWeightFlag")
                .IsRequired();

            this.Property(c => c.AirplaneWeightCode)
                .HasColumnName("AircraftWeightCode")
                .HasMaxLength(5)
                .IsOptional();

            this.Property(c => c.AirplaneWeightUnit)
                .HasColumnName("AircraftWeightUomID")
                .IsOptional();

            this.Property(c => c.AirplaneWeightMultiplier)
                .HasColumnName("AircraftWeightMultiple")
                .IsOptional();

            this.Property(c => c.MultiRateFlag)
                .HasColumnName("MultiRateFlag");

            this.Property(c => c.Rate)
                .HasColumnName("Rate")
                .HasPrecision(18, 5)
                .IsOptional();

            this.Property(c => c.CurrencyCode)
                .HasColumnName("CurrencyCode")
                .HasMaxLength(3)
                .IsOptional();

            this.Property(c => c.ServiceRecordFlag)
                .HasColumnName("ServiceRecordFlag")
                .IsRequired();

            this.Property(c => c.CalculationTypeId)
                .HasColumnName("CalculationTypeID")
                .IsRequired();

            this.Property(c => c.EndDateContract)
                .HasColumnName("EndDateContract")
                .IsOptional();

            // Relationships
            this.RegisterRequiredRelationships();
            this.RegisterTaxRelationships();
            this.RegisterCatalogRelationships();   
        }

        /// <summary>
        /// Registers the required relationships.
        /// </summary>
        private void RegisterRequiredRelationships()
        {
            this.HasRequired(c => c.Airline)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.AirlineCode);

            this.HasRequired(c => c.Airport)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.StationCode);

            this.HasRequired(c => c.Service)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.ServiceCode);

            this.HasRequired(c => c.Provider)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.ProviderNumber);

            this.HasRequired(c => c.CostCenter)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.CostCenterNumber);

            this.HasRequired(c => c.AccountingAccount)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.AccountingAccountNumber);

            this.HasRequired(c => c.LiabilityAccount)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.LiabilityAccountNumber);
        }

        /// <summary>
        /// Registers the tax relationships.
        /// </summary>
        private void RegisterTaxRelationships()
        {
            this.HasOptional(c => c.AirportTax)
                .WithMany(s => s.AirportServiceContractAirportTaxes)
                .HasForeignKey(c => c.AirportFeeCode);

            this.HasOptional(c => c.FederalTax)
                .WithMany(s => s.AirportServiceContractFederalTaxes)
                .HasForeignKey(c => c.FederalTaxCode);

            this.HasOptional(c => c.StateTax)
                .WithMany(s => s.AirportServiceContractStateTaxes)
                .HasForeignKey(c => c.StateTaxCode);

            this.HasOptional(c => c.LocalTax)
                .WithMany(s => s.AirportServiceContractLocalTaxes)
                .HasForeignKey(c => c.LocalTaxCode);
        }

        /// <summary>
        /// Registers the catalog relationships.
        /// </summary>
        private void RegisterCatalogRelationships()
        {
            this.HasOptional(c => c.Currency)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.CurrencyCode);

            this.HasOptional(c => c.AirplaneWeightType)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.AirplaneWeightCode);

            this.HasOptional(c => c.AirplaneWeightMeasureType)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.AirplaneWeightUnit);

            this.HasRequired(c => c.OperationType)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.OperationTypeId);

            this.HasRequired(c => c.ServiceType)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.ServiceTypeCode);

            this.HasRequired(c => c.ServiceCalculationType)
                .WithMany(s => s.AirportServiceContracts)
                .HasForeignKey(c => c.CalculationTypeId);
        }
    }
}
