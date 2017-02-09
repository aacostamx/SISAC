//------------------------------------------------------------------------
// <copyright file="ItineraryBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    using Entities.Itineraries;
    using AutoMapper;
    using System.Linq;


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
        /// The timeline repository
        /// </summary>
        private readonly ITimelineRepository timelineRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="timelineRepository">The timeline repository.</param>
        public TimelineBusiness(IUnitOfWork unitOfWork, ITimelineRepository timelineRepository)
        {
            this.unitOfWork = unitOfWork;
            this.timelineRepository = timelineRepository;
        }

        /// <summary>
        /// Gets the timeline by flight.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public TimelineDto GetTimelineByFlight(TimelineDto flight)
        {
            var timeline = new TimelineDto();

            try
            {
                timeline = Mapper.Map<TimelineDto>(this.timelineRepository.GetTimelineByFlight(new Timeline()
                {
                    Sequence = flight.Sequence,
                    AirlineCode = flight.AirlineCode,
                    FlightNumber = flight.FlightNumber,
                    ItineraryKey = flight.ItineraryKey
                }));
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
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<TimelineDto> GetFullTimeline()
        {
            var timeline = new List<TimelineDto>();

            try
            {
                timeline = Mapper.Map<List<TimelineDto>>(this.timelineRepository.GetAll());
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return timeline;
        }

        /// <summary>
        /// Gets the timeline by equipment number.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<TimelineDto> GetTimelineByEquipmentNumber(TimelineDto flight)
        {
            var timeline = new List<TimelineDto>();

            try
            {
                timeline = Mapper.Map<List<TimelineDto>>(this.timelineRepository.GetTimelineByEquipmentNumber(Mapper.Map<Timeline>(flight)));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return timeline;
        }

        /// <summary>
        /// Timelines the start procress.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool TimelineStartProcress(DateTime? startDate, DateTime? endDate)
        {
            var sucess = false;

            try
            {
                sucess = this.timelineRepository.TimelineStartProcess(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return sucess;
        }

        /// <summary>
        /// Gets the timeline paged.
        /// </summary>
        /// <param name="flight">The flight.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<TimelineDto> GetTimelinePaged(TimelineDto flight)
        {
            var timelineDto = new List<TimelineDto>();

            try
            {
                timelineDto = Mapper.Map<List<TimelineDto>>(this.timelineRepository.GetTimelinePaged(new Timeline() { Sequence = flight.Sequence, AirlineCode = flight.AirlineCode, FlightNumber = flight.FlightNumber, ItineraryKey = flight.ItineraryKey }));

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return timelineDto;
        }
    }
}