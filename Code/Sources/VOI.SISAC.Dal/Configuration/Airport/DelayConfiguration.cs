//------------------------------------------------------------------------
// <copyright file="DelayConfiguration.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Airport
{
    using System.Data.Entity.ModelConfiguration;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class
    /// </summary>
    public class DelayConfiguration : EntityTypeConfiguration<Delay>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DelayConfiguration()
        {
            Property(e => e.DelayCode)
            .IsUnicode(false);

            Property(e => e.DelayName)
            .IsUnicode(false);
        }
    }
}
