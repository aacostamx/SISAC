//------------------------------------------------------------------------
// <copyright file="AirportConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Configuración airport
    /// </summary>
    public class AirportConfiguration : EntityTypeConfiguration<Airport>
    {
        /// <summary>
        /// Class AirportConfiguration
        /// </summary>
        public AirportConfiguration()
        {
            Property(e => e.StationCode)
            .IsUnicode(false);

            Property(e => e.StationName)
            .IsUnicode(false);

            Property(e => e.CountryCode)
            .IsUnicode(false);

            Property(e => e.AirportGroupCode)
            .IsUnicode(false);

            HasMany(e => e.Gpu)
            .WithRequired(e => e.Airport)
            .WillCascadeOnDelete(false);

            HasMany(e => e.AirportSchedules)
            .WithRequired(e => e.Airport)
            .WillCascadeOnDelete(false);
        }
    }
}
