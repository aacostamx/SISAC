//------------------------------------------------------------------------
// <copyright file="TimelineRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using System.Collections.Generic;
    using Entities.Itineraries;
    using Infrastructure;



    /// <summary>
    /// class Timeline Repository
    /// </summary>
    public class TimelineRepository : Repository<Timeline>, ITimelineRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public TimelineRepository(IDbFactory factory) : base(factory) { }

        /// <summary>
        /// Adds the timeline movement.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        public bool AddTimelineMovement(Timeline flight)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the timeline movement.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        public bool DeleteTimelineMovement(Timeline flight)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the full timeline.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        public IList<Timeline> GetFullTimeline(Timeline flight)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the timeline by flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        public IList<Timeline> GetTimelineByFlight(Timeline flight)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the timeline movement.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        public bool UpdateTimelineMovement(Timeline flight)
        {
            throw new NotImplementedException();
        }
    }
}
