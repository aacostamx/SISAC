//------------------------------------------------------------------------
// <copyright file="AirportGroupConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class
    /// </summary>
    public class AirportGroupConfiguration : EntityTypeConfiguration<AirportGroup>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AirportGroupConfiguration()
        {
            Property(e => e.AirportGroupCode)
            .IsUnicode(false);

            Property(e => e.AirportGroupName)
            .IsUnicode(false);

            HasMany(e => e.Airport)
            .WithOptional(e => e.AirportGroup)
            .HasForeignKey(e => e.AirportGroupCode);
        }
    }
}
