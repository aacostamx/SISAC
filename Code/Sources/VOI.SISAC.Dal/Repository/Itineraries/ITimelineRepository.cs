//------------------------------------------------------------------------
// <copyright file="ITimelineRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using Entities.Itineraries;
    using Infrastructure;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Interface ITimeline Repository
    /// </summary>
    public interface ITimelineRepository : IRepository<Timeline>
    {
        /// <summary>
        /// Gets the timeline by flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        Timeline GetTimelineByFlight(Timeline flight);

        /// <summary>
        /// Gets the timeline by equipment number.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        List<Timeline> GetTimelineByEquipmentNumber(Timeline flight);

        /// <summary>
        /// Timelines the start process.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        bool TimelineStartProcess(DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Gets the timeline paged.
        /// </summary>
        /// <param name="timeline">The timeline.</param>
        /// <returns></returns>
        List<Timeline> GetTimelinePaged(Timeline timeline);
    }
}
