//------------------------------------------------------------------------
// <copyright file="TimelineMovementRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using Entities.Itineraries;
    using System;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;

    /// <summary>
    /// Timeline Movement Repository
    /// </summary>
    public class TimelineMovementRepository : Repository<TimelineMovement>, ITimelineMovementRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineMovementRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public TimelineMovementRepository(IDbFactory factory) : base(factory) { }

        /// <summary>
        /// Gets the timeline movement.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <returns></returns>
        public TimelineMovement GetTimelineMovement(TimelineMovement move)
        {
            var movement = new TimelineMovement();

            try
            {
                movement = this.DbContext.TimelineMovement
                    .Include(c => c.MovementType)
                    .Include(c => c.OperationType)
                    .Include(c => c.Provider)
                    .Where(c => c.ID == move.ID)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return movement;
        }
    }
}
