//------------------------------------------------------------------------
// <copyright file="ScheduleTypeConfiguration.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Configuration.Catalog
{
    using Entities.Catalog;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// ScheduleTypeConfiguration Class
    /// </summary>
    public class ScheduleTypeConfiguration : EntityTypeConfiguration<ScheduleType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleTypeConfiguration"/> class.
        /// </summary>
        public ScheduleTypeConfiguration()
        {
            Property(e => e.ScheduleTypeCode)
            .IsUnicode(false);

            Property(e => e.ScheduleTypeName)
            .IsUnicode(false);

            HasMany(e => e.AirportSchedules)
            .WithRequired(e => e.ScheduleType)
            .WillCascadeOnDelete(false);
        }
    }
}
