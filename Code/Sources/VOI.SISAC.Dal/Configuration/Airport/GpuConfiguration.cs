//------------------------------------------------------------------------
// <copyright file="GpuConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using Entities.Airport;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// Class
    /// </summary>
    public class GpuConfiguration : EntityTypeConfiguration<Gpu>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GpuConfiguration()
        {
            Property(e => e.GpuCode)
            .IsUnicode(false);

            Property(e => e.GpuName)
            .IsUnicode(false);

            Property(e => e.StationCode)
            .IsUnicode(false);
        }
    }
}
