//------------------------------------------------------------------------
// <copyright file="ProviderConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProviderConfiguration"/> class.
    /// </summary>
    public class ProviderConfiguration : EntityTypeConfiguration<Provider>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProviderConfiguration"/> class.
        /// </summary>
        public ProviderConfiguration()
        {
            this.ToTable("Provider", "Finance");

            this.HasKey<string>(c => c.ProviderNumber);

            this.Property(c => c.ProviderNumber)
                        .HasMaxLength(10)
                        .HasColumnName("ProviderNumber");

            this.Property(c => c.ProviderName)
                        .HasMaxLength(150)
                        .HasColumnName("ProviderName");

            this.Property(c => c.ProviderShortName)
                .HasMaxLength(15)
                .HasColumnName("ProviderShortName");

            this.Property(c => c.Status)
                .HasColumnName("Status");

            this.HasMany(e => e.AirportServiceContracts)
                .WithRequired(e => e.Provider)
                .WillCascadeOnDelete(false);

            this.HasMany(e => e.InternationalFuelContracts)
                .WithRequired(e => e.Provider)
                .HasForeignKey(e => e.ProviderNumberPrimary)
                .WillCascadeOnDelete(false);

            this.HasMany(e => e.InternationalFuelContractConcepts)
                .WithRequired(e => e.Provider)
                .WillCascadeOnDelete(false);


            //Configuracion JetFuelTicket JETFUEL
            this.HasMany(e => e.JetFuelTicketJetFuels)
                .WithRequired(e => e.JetFuelProvider)
                .HasForeignKey(e => e.JetFuelProviderNumber)
                .WillCascadeOnDelete(false);

            //Configuracion JetFuelTicket INTOPLANE
            this.HasMany(e => e.JetFuelTicketIntoPlanes)
                .WithRequired(e => e.IntoPlaneProvider)
                .HasForeignKey(e => e.IntoPlaneProviderNumber)
                .WillCascadeOnDelete(false);

            this.HasMany(e => e.NationalJetFuelProviders)
                .WithRequired(e => e.JetFuelProvider)
                .HasForeignKey(e => e.JetFuelProviderNumber)
                .WillCascadeOnDelete(false);

            this.HasMany(e => e.NationalJetFuelIntoPlanes)
                .WithRequired(e => e.IntoPlaneProvider)
                .HasForeignKey(e => e.IntoPlaneProviderNumber)
                .WillCascadeOnDelete(false);
        }
    }
}
