//------------------------------------------------------------------------
// <copyright file="IScheduleTypeRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using Entities.Catalog;
    using Infrastructure;
    using System.Collections.Generic;

    /// <summary>
    /// IScheduleTypeRepository Interface
    /// </summary>
    public interface IScheduleTypeRepository : IRepository<ScheduleType>
    {
        /// <summary>
        /// Finds the type of the schedule.
        /// </summary>
        /// <param name="code">Type of the schedule.</param>
        /// <returns></returns>
        ScheduleType FindScheduleType(ScheduleType code);

        /// <summary>
        /// Gets the actives schedule types.
        /// </summary>
        /// <returns></returns>
        IList<ScheduleType> GetActivesScheduleTypes();
    }
}
