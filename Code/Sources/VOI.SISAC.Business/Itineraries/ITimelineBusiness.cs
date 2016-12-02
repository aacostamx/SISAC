//------------------------------------------------------------------------
// <copyright file="ITimelineBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using Dto.Itineraries;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// ITimelineBusiness
    /// </summary>
    public interface ITimelineBusiness
    {
        /// <summary>
        /// Gets the timeline by flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        TimelineDto GetTimelineByFlight(TimelineDto flight);

        /// <summary>
        /// Gets the full timeline.
        /// </summary>
        /// <returns></returns>
        IList<TimelineDto> GetFullTimeline();

        /// <summary>
        /// Gets the timeline by equipment number.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        IList<TimelineDto> GetTimelineByEquipmentNumber(TimelineDto flight);

        /// <summary>
        /// Timelines the start procress.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        bool TimelineStartProcress(DateTime? startDate, DateTime? endDate);
    }
}
