//------------------------------------------------------------------------
// <copyright file="ScheduleTypeRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using System;
    using Entities.Catalog;
    using Infrastructure;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// ScheduleTypeRepository Class
    /// </summary>
    public class ScheduleTypeRepository : Repository<ScheduleType>, IScheduleTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ScheduleTypeRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Finds the type of the schedule.
        /// </summary>
        /// <param name="code">The schedule find.</param>
        /// <returns></returns>
        public ScheduleType FindScheduleType(ScheduleType code)
        {
            var scheduleDB = new ScheduleType();
            scheduleDB = this.DbContext.ScheduleType.AsNoTracking()
                            .Where(c => c.ScheduleTypeCode == code.ScheduleTypeCode)
                            .FirstOrDefault();
            return scheduleDB;

        }

        /// <summary>
        /// Gets the actives schedule types.
        /// </summary>
        /// <returns></returns>
        public IList<ScheduleType> GetActivesScheduleTypes()
        {
            var actives = new List<ScheduleType>();
            actives = this.DbContext.ScheduleType
                            .Where(c => c.Status)
                            .ToList();
            return actives;
        }
    }
}
