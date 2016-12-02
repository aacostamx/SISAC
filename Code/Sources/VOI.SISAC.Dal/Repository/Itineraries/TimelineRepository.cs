//------------------------------------------------------------------------
// <copyright file="TimelineRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
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
        /// Gets the timeline by equipment number.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        public List<Timeline> GetTimelineByEquipmentNumber(Timeline flight)
        {
            var timeline = new List<Timeline>();

            try
            {
                timeline = this.DbContext.Timeline
                    .Include(c => c.Itinerary)
                    .Include(c => c.TimelineMovements.Select(d => d.MovementType))
                    .Include(c => c.TimelineMovements.Select(d => d.OperationType))
                    .Include(c => c.TimelineMovements.Select(d => d.Provider))
                    .Where(c => c.Itinerary.EquipmentNumber == flight.Itinerary.EquipmentNumber)
                    .OrderBy(or => or.Itinerary.DepartureDate)
                    .Take(20)
                    .ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return timeline;
        }

        /// <summary>
        /// Gets the timeline by flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        public Timeline GetTimelineByFlight(Timeline flight)
        {
            var timeline = new Timeline();

            try
            {
                timeline = this.DbContext.Timeline
                    .Include(c => c.Itinerary)
                    .Include(c => c.TimelineMovements)
                    .Where(c => c.Sequence == flight.Sequence &&
                    c.AirlineCode == flight.AirlineCode &&
                    c.FlightNumber == flight.FlightNumber &&
                    c.ItineraryKey == flight.ItineraryKey)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return timeline;
        }

        /// <summary>
        /// Timelines the start process.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public bool TimelineStartProcess(DateTime? startDate, DateTime? endDate)
        {
            var sucess = false;

            try
            {
                sucess = this.DbContext.AutomaticTimeline(startDate, endDate);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return sucess;
        }
    }
}
