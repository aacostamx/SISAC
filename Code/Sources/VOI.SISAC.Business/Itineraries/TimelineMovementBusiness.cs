//------------------------------------------------------------------------
// <copyright file="TimelineMovementBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Itineraries;
    using Dto.Itineraries;
    using Entities.Itineraries;
    using ExceptionBusiness;
    using Resources;
    using System;


    /// <summary>
    /// TimelineMovementBusiness class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Business.Itineraries.ITimelineMovementBusiness" />
    public class TimelineMovementBusiness : ITimelineMovementBusiness
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The timeline repository
        /// </summary>
        private readonly ITimelineMovementRepository movementRepository;

        /// <summary>
        /// The timeline repository
        /// </summary>
        private readonly ITimelineRepository timelineRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineMovementBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="movementRepository">The movement repository.</param>
        /// <param name="timelineRepository">The timeline repository.</param>
        public TimelineMovementBusiness(IUnitOfWork unitOfWork, ITimelineMovementRepository movementRepository,
            ITimelineRepository timelineRepository)
        {
            this.unitOfWork = unitOfWork;
            this.movementRepository = movementRepository;
            this.timelineRepository = timelineRepository;
        }

        /// <summary>
        /// Adds the timeline movement.
        /// </summary>
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        public bool AddTimelineMovement(TimelineMovementDto movement)
        {
            var added = false;

            try
            {
                this.movementRepository.Add(Mapper.Map<TimelineMovement>(movement));
                this.unitOfWork.Commit();
                added = true;
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
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        public bool UpdateTimelineMovement(TimelineMovementDto movement)
        {
            var updated = false;
            var movementDB = new TimelineMovement();

            try
            {
                movementDB = this.movementRepository.Find(c => c.ID == movement.ID, true);
                if (movementDB != null)
                {
                    movementDB.OperationTypeID = movement.OperationTypeID;
                    movementDB.MovementTypeCode = movement.MovementTypeCode;
                    movementDB.MovementDate = movement.MovementDate;
                    movementDB.Position = movement.Position;
                    movementDB.ProviderNumber = movement.ProviderNumber;
                    movementDB.RemainingFuel = movement.RemainingFuel;
                    movementDB.Remarks = movement.Remarks;

                    if (movement.MovementTypeCode == "SC")
                    {
                        var timeline = this.timelineRepository.Find(c => c.Sequence == movement.Sequence &&
                        c.AirlineCode == movement.AirlineCode &&
                        c.FlightNumber == movement.FlightNumber &&
                        c.ItineraryKey == movement.ItineraryKey);
                        timeline.SpecialCase = true;
                        this.timelineRepository.Update(timeline);
                    }

                    this.movementRepository.Update(movementDB);
                    this.unitOfWork.Commit();
                    updated = true;
                }
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
        /// <param name="movement">The movement.</param>
        /// <returns></returns>
        public bool DeleteTimelineMovement(TimelineMovementDto movement)
        {
            var deleted = false;
            var movementDB = new TimelineMovement();

            try
            {
                movementDB = this.movementRepository.Find(c => c.ID == movement.ID, true);

                if (movementDB != null)
                {
                    this.movementRepository.Delete(movementDB);
                    this.unitOfWork.Commit();
                    deleted = true;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }

            return deleted;
        }

        /// <summary>
        /// Gets the timeline movement.
        /// </summary>
        /// <param name="movement">The movement dto.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public TimelineMovementDto GetTimelineMovement(TimelineMovementDto movement)
        {
            var movementDto = new TimelineMovementDto();

            try
            {
                movementDto = Mapper.Map<TimelineMovementDto>(this.movementRepository.GetTimelineMovement(new TimelineMovement { ID = movement.ID }));
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }

            return movementDto;
        }
    }
}