//------------------------------------------------------------------------
// <copyright file="GpuObservationConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using Entities.Airport;
    using System.Data.Entity;

    /// <summary>
    /// Class GpuObservationConfiguration
    /// </summary>
    public class GpuObservationConfiguration : EntityTypeConfiguration<GpuObservation>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GpuObservationConfiguration()
        {
                Property(e => e.GpuObservationCode)
                .IsUnicode(false);

                Property(e => e.GpuObservationCodeName)
                .IsUnicode(false);

        }
    }
}
