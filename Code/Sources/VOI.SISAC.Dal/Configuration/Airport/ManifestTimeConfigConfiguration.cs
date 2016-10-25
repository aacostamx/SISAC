//------------------------------------------------------------------------
// <copyright file="ManifestTimeConfigConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using Entities.Airport;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// ManifestTimeConfig class
    /// </summary>
    public class ManifestTimeConfigConfiguration : EntityTypeConfiguration<ManifestTimeConfig>
    {
        /// <summary>
        /// Constructor ManifestTimeConfigConfiguration
        /// </summary>
        public ManifestTimeConfigConfiguration()
        {
            Property(e => e.AirlineCode)
            .IsUnicode(false);

            Property(e => e.ArrivalMinutesMin)
            .HasPrecision(10, 2);

            Property(e => e.ArrivalMinutesMax)
            .HasPrecision(10, 2);

            Property(e => e.DepartureMinutesMin)
            .HasPrecision(10, 2);

            Property(e => e.DepartureMinutesMax)
            .HasPrecision(10, 2);
        }
    }
}
