//------------------------------------------------------------------------
// <copyright file="IScheduleTypeBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System.Collections;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// IScheduleTypeBusiness Interface
    /// </summary>
    public interface IScheduleTypeBusiness
    {
        /// <summary>
        /// Adds the type of the schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns></returns>
        bool AddScheduleType(ScheduleTypeDto schedule);

        /// <summary>
        /// Edits the type of the schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns></returns>
        bool EditScheduleType(ScheduleTypeDto schedule);

        /// <summary>
        /// Deletes the type of the schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns></returns>
        bool DeleteScheduleType(ScheduleTypeDto schedule);

        /// <summary>
        /// Finds the type of the schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns></returns>
        ScheduleTypeDto FindScheduleType(ScheduleTypeDto schedule);

        /// <summary>
        /// Gets the actives schedule types.
        /// </summary>
        /// <returns></returns>
        IList<ScheduleTypeDto> GetActivesScheduleTypes();
    }
}
