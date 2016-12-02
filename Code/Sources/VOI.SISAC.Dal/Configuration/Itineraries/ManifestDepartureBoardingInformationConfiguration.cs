//--------------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingInformationConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Itineraries
{
    using System;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// ManifestDepartureBoardingInformationConfiguration
    /// </summary>
    /// <seealso cref="System.Data.Entity.ModelConfiguration.EntityTypeConfiguration{VOI.SISAC.Entities.Itineraries.ManifestDepartureBoardingInformation}" />
    public class ManifestDepartureBoardingInformationConfiguration : EntityTypeConfiguration<ManifestDepartureBoardingInformation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestDepartureBoardingInformationConfiguration"/> class.
        /// </summary>
        public ManifestDepartureBoardingInformationConfiguration()
        {
            this.HasKey<long>(e => e.BoardingInformationID);
        }
    }
}
