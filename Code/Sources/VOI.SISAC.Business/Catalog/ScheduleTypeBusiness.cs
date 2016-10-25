//------------------------------------------------------------------------
// <copyright file="ScheduleTypeBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Catalog;
    using Entities.Catalog;
    using ExceptionBusiness;
    using Resources;
    using System;
    using VOI.SISAC.Business.Dto.Catalogs;
    using System.Collections.Generic;

    /// <summary>
    /// ScheduleTypeBusiness Class
    /// </summary>
    public class ScheduleTypeBusiness : IScheduleTypeBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The schedule repository
        /// </summary>
        private readonly IScheduleTypeRepository scheduleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleTypeBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="scheduleRepository">The schedule repository.</param>
        public ScheduleTypeBusiness(IUnitOfWork unitOfWork, IScheduleTypeRepository scheduleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.scheduleRepository = scheduleRepository;
        }

        /// <summary>
        /// Adds the type of the schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException">
        /// 10
        /// or
        /// </exception>
        public bool AddScheduleType(ScheduleTypeDto schedule)
        {
            bool added = false;
            if (this.IsScheduleTypeDuplicated(schedule))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
            {
                var entity = new ScheduleType()
                {
                    ScheduleTypeCode = schedule.ScheduleTypeCode,
                    ScheduleTypeName = schedule.ScheduleTypeName,
                    Status = true
                };

                this.scheduleRepository.Add(entity);
                this.unitOfWork.Commit();
                added = true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
            return added;
        }

        /// <summary>
        /// Determines whether [is schedule type duplicated] [the specified schedule].
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns></returns>
        private bool IsScheduleTypeDuplicated(ScheduleTypeDto schedule)
        {
            var entity = this.scheduleRepository.FindScheduleType(new ScheduleType(schedule.ScheduleTypeCode));
            return entity != null ? true : false;
        }

        /// <summary>
        /// Deletes the type of the schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool DeleteScheduleType(ScheduleTypeDto schedule)
        {
            bool deleted = false;
            try
            {
                var entity = this.scheduleRepository.FindScheduleType(new ScheduleType(schedule.ScheduleTypeCode));
                if (entity == null) return deleted;
                entity.Status = false;
                this.scheduleRepository.Update(entity);
                this.unitOfWork.Commit();
                deleted = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }

            return deleted;
        }

        /// <summary>
        /// Edits the type of the schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public bool EditScheduleType(ScheduleTypeDto schedule)
        {
            bool edit = false;
            try
            {
                var entity = this.scheduleRepository.FindScheduleType(new ScheduleType(schedule.ScheduleTypeCode));
                if (entity == null) return edit;
                entity.ScheduleTypeName = schedule.ScheduleTypeName;
                this.scheduleRepository.Update(entity);
                this.unitOfWork.Commit();
                edit = true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
            return edit;
        }

        /// <summary>
        /// Finds the type of the schedule.
        /// </summary>
        /// <param name="schedule">The schedule.</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public ScheduleTypeDto FindScheduleType(ScheduleTypeDto schedule)
        {
            var scheduleDto = new ScheduleTypeDto();
            try
            {
                var entity = this.scheduleRepository.FindScheduleType(new ScheduleType(schedule.ScheduleTypeCode));
                scheduleDto = Mapper.Map<ScheduleTypeDto>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
            return scheduleDto;
        }

        /// <summary>
        /// Gets the actives schedule types.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public IList<ScheduleTypeDto> GetActivesScheduleTypes()
        {
            IList<ScheduleTypeDto> actives = new List<ScheduleTypeDto>();
            try
            {
                var entity = this.scheduleRepository.GetActivesScheduleTypes();
                actives = Mapper.Map<IList<ScheduleTypeDto>>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
            return actives;
        }
    }
}
