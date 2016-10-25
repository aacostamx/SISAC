//------------------------------------------------------------------------
// <copyright file="ItineraryBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using System;
    using System.Collections.Generic;
    using Dal.Infrastructure;
    using Dal.Repository.Itineraries;
    using Dto.Itineraries;
    using ExceptionBusiness;
    using Resources;


    /// <summary>
    /// TimelineBusiness class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Business.Itineraries.ITimelineBusiness" />
    public class TimelineBusiness : ITimelineBusiness
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The Itinerary repository
        /// </summary>
        private readonly ITimelineRepository timelineRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TimelineBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the timeline by flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<TimelineDto> GetTimelineByFlight(TimelineDto flight)
        {
            var timeline = new List<TimelineDto>();

            try
            {

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return timeline;
        }

        /// <summary>
        /// Gets the full timeline.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<TimelineDto> GetFullTimeline(TimelineDto flight)
        {
            var timeline = new List<TimelineDto>();

            try
            {

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return timeline;
        }

        /// <summary>
        /// Adds the timeline movement.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool AddTimelineMovement(TimelineDto flight)
        {
            var added = false;

            try
            {

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }

            return added;
        }

        /// <summary>
        /// Updates the timeline movement.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool UpdateTimelineMovement(TimelineDto flight)
        {
            var updated = false;

            try
            {

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }

            return updated;
        }

        /// <summary>
        /// Deletes the timeline movement.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool DeleteTimelineMovement(TimelineDto flight)
        {
            var deleted = false;

            try
            {

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }

            return deleted;
        }
    }
}