//------------------------------------------------------------------------
// <copyright file="ITimelineMovement.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using Entities.Itineraries;
    using Infrastructure;

    /// <summary>
    /// Interface Timeline Movement
    /// </summary>
    public interface ITimelineMovementRepository : IRepository<TimelineMovement>
    {
        /// <summary>
        /// Gets the timeline movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        TimelineMovement GetTimelineMovement(TimelineMovement movement);

    }
}
