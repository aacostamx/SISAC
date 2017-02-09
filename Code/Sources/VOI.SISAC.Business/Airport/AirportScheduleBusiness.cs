//------------------------------------------------------------------------
// <copyright file="AirportScheduleBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;

    public class AirportScheduleBusiness : IAirportScheduleBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;


        /// <summary>
        /// The airport schedule repository
        /// </summary>
        private readonly IAirportScheduleRepository airportScheduleRepository;


        /// <summary>
        /// Initializes a new instance of the <see cref="AirportScheduleBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="airportScheduleRepository">The airport schedule repository.</param>
        public AirportScheduleBusiness(IUnitOfWork unitOfWork, IAirportScheduleRepository airportScheduleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.airportScheduleRepository = airportScheduleRepository;
        }

        /// <summary>
        /// Finds the airport schedule by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>AirportScheduleDto.</returns>
        /// <exception cref="BusinessException"></exception>
        public AirportScheduleDto FindAirportScheduleById(long id)
        {
            if (id <= 0)
            {
                return null;
            }

            try
            {
                AirportSchedule schedule = this.airportScheduleRepository.FindById(id);
                AirportScheduleDto scheduleDto = Mapper.Map<AirportSchedule, AirportScheduleDto>(schedule);
                return scheduleDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds the airport schedule.
        /// </summary>
        /// <param name="airportScheduleDto">The airport schedule dto.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="BusinessException">
        /// 11
        /// or
        /// </exception>
        public bool AddAirportSchedule(AirportScheduleDto airportScheduleDto)
        {
            if (airportScheduleDto == null)
            {
                return false;
            }

            if (airportScheduleDto.EndTimeSchedule < airportScheduleDto.StartTimeSchedule)
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicateValue, 25);
            }

            if (this.IsValueDuplicatePerAirportAndScheduleTimeNew(airportScheduleDto))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicateValue, 11);
            }

            try
            {
                AirportSchedule schedule = Mapper.Map<AirportScheduleDto, AirportSchedule>(airportScheduleDto);
                schedule.Status = true;
                this.airportScheduleRepository.Add(schedule);
                this.unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }

            return false;
        }

        /// <summary>
        /// Deletes the airport schedule.
        /// </summary>
        /// <param name="airportScheduleDto">The airport schedule dto.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="BusinessException"></exception>
        public bool DeleteAirportSchedule(AirportScheduleDto airportScheduleDto)
        {
            if (airportScheduleDto == null)
            {
                return false;
            }

            try
            {
                AirportSchedule schedule = this.airportScheduleRepository.FindById(airportScheduleDto.AirportScheduleID);
                schedule.Status = false;
                this.airportScheduleRepository.Update(schedule);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the airport schedule.
        /// </summary>
        /// <param name="airportScheduleDto">The airport schedule dto.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="BusinessException">
        /// 11
        /// or
        /// </exception>
        public bool UpdateAirportSchedule(AirportScheduleDto airportScheduleDto)
        {
            if (airportScheduleDto == null)
            {
                return false;
            }

            if (airportScheduleDto.EndTimeSchedule < airportScheduleDto.StartTimeSchedule)
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicateValue, 25);
            }

            if (this.IsValueDuplicatePerAirportAndScheduleTimeUpd(airportScheduleDto))
            {
                throw new BusinessException(Messages.FailedUpdateRecord, Messages.DuplicateValue, 11);
            }

            try
            {
                AirportSchedule schedule = this.airportScheduleRepository.FindById(airportScheduleDto.AirportScheduleID);

                // Updating the enity
                schedule.ScheduleTypeCode = airportScheduleDto.ScheduleTypeCode;
                schedule.StartTimeSchedule = airportScheduleDto.StartTimeSchedule;
                schedule.EndTimeSchedule = airportScheduleDto.EndTimeSchedule;

                this.airportScheduleRepository.Update(schedule);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the actives airport schedules.
        /// </summary>
        /// <returns>IList&lt;AirportScheduleDto&gt;.</returns>
        /// <exception cref="BusinessException"></exception>
        public IList<AirportScheduleDto> GetActivesAirportSchedules()
        {
            try
            {
                IList<AirportSchedule> schedule = this.airportScheduleRepository.GetActiveAirportSchedule();
                IList<AirportScheduleDto> scheduleDto = Mapper.Map<IList<AirportSchedule>, IList<AirportScheduleDto>>(schedule);
                return scheduleDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the airport schedules by station code.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="onlyActives">if set to <c>true</c> [only actives].</param>
        /// <returns>IList&lt;AirportScheduleDto&gt;.</returns>
        /// <exception cref="BusinessException"></exception>
        public IList<AirportScheduleDto> GetAirportSchedulesByStationCode(string stationCode, bool onlyActives)
        {
            if (string.IsNullOrWhiteSpace(stationCode))
            {
                return null;
            }

            try
            {
                IList<AirportSchedule> schedule = this.airportScheduleRepository.GetAirportSchedulesByStationCode(stationCode, onlyActives);
                IList<AirportScheduleDto> scheduleDto = Mapper.Map<IList<AirportSchedule>, IList<AirportScheduleDto>>(schedule);
                return scheduleDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Determines whether [is value duplicate per airport and schedule time] [the specified schedule dto].
        /// </summary>
        /// <param name="scheduleDto">The schedule dto.</param>
        /// <returns><c>true</c> if [is value duplicate per airport and schedule time] [the specified schedule dto]; otherwise, <c>false</c>.</returns>
        private bool IsValueDuplicatePerAirportAndScheduleTimeNew(AirportScheduleDto scheduleDto)
        {
            IList<AirportSchedule> schedules = this.airportScheduleRepository
                .GetAirportSchedulesByStationCode(scheduleDto.StationCode, true);

            AirportSchedule scheduleTime = schedules.FirstOrDefault(c => (c.StartTimeSchedule <= scheduleDto.StartTimeSchedule && c.EndTimeSchedule >= scheduleDto.EndTimeSchedule) ||
                                                                         (c.StartTimeSchedule >= scheduleDto.StartTimeSchedule && c.EndTimeSchedule >= scheduleDto.EndTimeSchedule && c.StartTimeSchedule <= scheduleDto.EndTimeSchedule) ||
                                                                         (c.StartTimeSchedule <= scheduleDto.StartTimeSchedule && c.EndTimeSchedule <= scheduleDto.EndTimeSchedule && c.EndTimeSchedule >= scheduleDto.StartTimeSchedule));
            
            return scheduleTime != null ? true : false;

        }

        /// <summary>
        /// Determines whether [is value duplicate per airport and schedule time new] [the specified schedule dto].
        /// </summary>
        /// <param name="scheduleDto">The schedule dto.</param>
        /// <returns><c>true</c> if [is value duplicate per airport and schedule time new] [the specified schedule dto]; otherwise, <c>false</c>.</returns>
        private bool IsValueDuplicatePerAirportAndScheduleTimeUpd(AirportScheduleDto scheduleDto)
        {
            IList<AirportSchedule> schedules = this.airportScheduleRepository
                .GetAirportSchedulesByStationCodeExceptID(scheduleDto.StationCode, scheduleDto.AirportScheduleID);

            AirportSchedule scheduleTime = schedules.FirstOrDefault(c => (c.StartTimeSchedule <= scheduleDto.StartTimeSchedule && c.EndTimeSchedule >= scheduleDto.EndTimeSchedule) ||
                                                                         (c.StartTimeSchedule >= scheduleDto.StartTimeSchedule && c.EndTimeSchedule >= scheduleDto.EndTimeSchedule && c.StartTimeSchedule <= scheduleDto.EndTimeSchedule) ||
                                                                         (c.StartTimeSchedule <= scheduleDto.StartTimeSchedule && c.EndTimeSchedule <= scheduleDto.EndTimeSchedule && c.EndTimeSchedule >= scheduleDto.StartTimeSchedule));
                       
            return scheduleTime != null ? true : false;

        }
    }
}
