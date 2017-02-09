// <copyright file="InternationalFuelRateConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>

namespace VOI.SISAC.Dal.Configuration.Finance
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Gets or sets the International Fuel Rate Configuration
    /// </summary>
    public class InternationalFuelRateConfiguration : EntityTypeConfiguration<InternationalFuelRate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternationalFuelRateConfiguration"/> class.
        /// </summary>
        public InternationalFuelRateConfiguration()
        {
            // Define table´s name and name schema
            this.ToTable("InternationalFuelRate", "Finance");

            this.HasKey<long>(e => e.InternationalFuelRateID);

            this.Property(e => e.InternationalFuelRateID)
                .HasColumnName("InternationalFuelRateID");

            this.Property(e => e.InternationalFuelContractConceptID)
                .HasColumnName("InternationalFuelContractConceptID");

            this.Property(e => e.RateStartDate)
                .IsRequired()
                .HasColumnName("RateStartDate");

            this.Property(e => e.RateEndDate)
                .IsRequired()
                .HasColumnName("RateEndDate");

            this.Property(e => e.Rate)
                .HasPrecision(13, 5)
                .IsRequired()
                .HasColumnName("Rate");
        }
    }
}
