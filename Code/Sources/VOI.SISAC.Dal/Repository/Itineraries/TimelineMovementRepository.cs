//------------------------------------------------------------------------
// <copyright file="TimelineMovementRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using Entities.Itineraries;
    using VOI.SISAC.Dal.Infrastructure;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Linq.Expressions;

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


    }
}
