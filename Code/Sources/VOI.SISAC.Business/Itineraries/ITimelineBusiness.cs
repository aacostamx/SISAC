//------------------------------------------------------------------------
// <copyright file="ITimelineBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using Dto.Itineraries;
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
        IList<TimelineDto> GetTimelineByFlight(TimelineDto flight);

        /// <summary>
        /// Gets the full timeline.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        IList<TimelineDto> GetFullTimeline(TimelineDto flight);

        /// <summary>
        /// Adds the timeline movement.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        bool AddTimelineMovement(TimelineDto flight);

        /// <summary>
        /// Updates the timeline movement.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        bool UpdateTimelineMovement(TimelineDto flight);

        /// <summary>
        /// Deletes the timeline movement.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        bool DeleteTimelineMovement(TimelineDto flight);
    }
}
