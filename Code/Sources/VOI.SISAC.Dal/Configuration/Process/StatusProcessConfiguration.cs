//------------------------------------------------------------------------
// <copyright file="StatusProcessConfiguration.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Process
{
    using Entities.Process;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// StatusProcessConfiguration Class
    /// </summary>
    public class StatusProcessConfiguration : EntityTypeConfiguration<StatusProcess>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public StatusProcessConfiguration()
        {
            Property(e => e.StatusProcessCode)
            .IsUnicode(false);

            Property(e => e.StatusProcessName)
            .IsUnicode(false);
        }
    }
}
