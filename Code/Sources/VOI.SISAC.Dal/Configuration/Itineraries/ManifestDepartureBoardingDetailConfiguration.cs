//--------------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingDetailConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Itineraries;
    
    /// <summary>
    /// ManifestDepartureBoardingDetailConfiguration
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{VOI.SISAC.Entities.Itineraries.ManifestDepartureBoardingDetail}" />
    public class ManifestDepartureBoardingDetailConfiguration : EntityTypeConfiguration<ManifestDepartureBoardingDetail>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestDepartureBoardingDetailConfiguration"/> class.
        /// </summary>
        public ManifestDepartureBoardingDetailConfiguration()
        {
            this.HasKey<long>(e => e.BoardingDetailID);

            this.Property(e => e.LuggageKgs)
                .HasPrecision(13, 5)
                .IsRequired()
                .HasColumnName("LuggageKgs");

            this.Property(e => e.ChargeKgs)
                .HasPrecision(13, 5)
                .IsRequired()
                .HasColumnName("ChargeKgs");

            this.Property(e => e.Remarks)
                .IsUnicode(false);

            this.Property(e => e.RampResponsible)
                .IsUnicode(false);            
        }
        
    }
}
