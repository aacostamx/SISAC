//------------------------------------------------------------------------
// <copyright file="ITimelineMovementBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using Dto.Itineraries;


    /// <summary>
    /// ITimelineMovementBusiness interface
    /// </summary>
    public interface ITimelineMovementBusiness
    {
        /// <summary>
        /// Adds the timeline movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        bool AddTimelineMovement(TimelineMovementDto movement);

        /// <summary>
        /// Updates the timeline movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        bool UpdateTimelineMovement(TimelineMovementDto movement);

        /// <summary>
        /// Deletes the timeline movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        bool DeleteTimelineMovement(TimelineMovementDto movement);

        /// <summary>
        /// Gets the timeline movement.
        /// </summary>
        /// <param name="movementDto">The movement dto.</param>
        /// <returns></returns>
        TimelineMovementDto GetTimelineMovement(TimelineMovementDto movementDto);
    }
}
