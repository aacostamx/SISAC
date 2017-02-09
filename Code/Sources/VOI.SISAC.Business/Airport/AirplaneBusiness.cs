//------------------------------------------------------------------------
// <copyright file="AirplaneBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Airplane business logic
    /// </summary>
    public class AirplaneBusiness : IAirplaneBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airplane type repository
        /// </summary>
        private readonly IAirplaneRepository airplaneRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="airplaneRepository">The airplane repository.</param>
        public AirplaneBusiness(IUnitOfWork unitOfWork, IAirplaneRepository airplaneRepository)
        {
            this.unitOfWork = unitOfWork;
            this.airplaneRepository = airplaneRepository;
        }

        /// <summary>
        /// Gets all Airplanes.
        /// </summary>
        /// <returns>All Airports.</returns>
        public IList<AirplaneDto> GetAllAirplane()
        {
            try
            {
                IList<Airplane> airplaneEntity = this.airplaneRepository.GetAll();
                IList<AirplaneDto> airplaneDto = new List<AirplaneDto>();
                airplaneDto = Mapper.Map<IList<Airplane>, IList<AirplaneDto>>(airplaneEntity);
                return airplaneDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the Airplane by its identifier.
        /// </summary>
        /// <param name="id">The Currency identifier.</param>
        /// <returns>
        /// Currency Entity.
        /// </returns>
        public AirplaneDto FindAirplaneById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            try
            {
                Airplane airplaneTypeEntity = this.airplaneRepository.FindById(id);
                AirplaneDto airplaneTypeDto = airplaneTypeDto = Mapper.Map<Airplane, AirplaneDto>(airplaneTypeEntity);
                return airplaneTypeDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds a new Airplane.
        /// </summary>
        /// <param name="airplaneDto">The entity</param>
        /// <returns>
        ///   <c>true</c> if was added <c>false</c> otherwise.
        /// </returns>
        public bool AddAirplane(AirplaneDto airplaneDto)
        {
            if (airplaneDto == null)
            {
                return false;
            }

            if (this.IsEquipmentNumberDuplicate(airplaneDto.EquipmentNumber))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                Airplane airplane = Mapper.Map<AirplaneDto, Airplane>(airplaneDto);
                airplane.Status = true;
                this.airplaneRepository.Add(airplane);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the Airplane.
        /// </summary>
        /// <param name="airplaneDto">The entity.</param>
        /// <returns>
        ///   <c>true</c> if was deleted <c>false</c> otherwise.
        /// </returns>
        public bool DeleteAirplane(AirplaneDto airplaneDto)
        {
            if (airplaneDto == null)
            {
                return false;
            }

            try
            {
                Airplane airplane = this.airplaneRepository.FindById(airplaneDto.EquipmentNumber);

                // Para borrado lógico
                airplane.Status = false;
                this.airplaneRepository.Update(airplane);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the Airplane.
        /// </summary>
        /// <param name="airplaneDto">The entity.</param>
        /// <returns>
        ///   <c>true</c> if was updated <c>false</c> otherwise.
        /// </returns>
        public bool UpdateAirplane(AirplaneDto airplaneDto)
        {
            if (airplaneDto == null)
            {
                return false;
            }

            try
            {
                Airplane airplane = this.airplaneRepository.FindById(airplaneDto.EquipmentNumber);

                // Update the entity
                airplane.AirplaneModel = airplaneDto.AirplaneModel;
                airplane.CrewCapacity = airplaneDto.CrewCapacity;
                airplane.EmptyOperatingWeight = airplaneDto.EmptyOperatingWeight;
                airplane.FilmingMaximumWeight = airplaneDto.FilmingMaximumWeight;
                airplane.GroupWeight = airplaneDto.GroupWeight;
                airplane.Magnitude = airplaneDto.Magnitude;
                airplane.MaximumLandingWeight = airplaneDto.MaximumLandingWeight;
                airplane.MaximumTakeoffWeight = airplaneDto.MaximumTakeoffWeight;
                airplane.MaximumZeroFuelWeight = airplaneDto.MaximumZeroFuelWeight;
                airplane.PassengerCapacity = airplaneDto.PassengerCapacity;
                airplane.TakeoffWeightInTonnes = airplaneDto.TakeoffWeightInTonnes;
                airplane.WeightInPound = airplaneDto.WeightInPound;
                airplane.WeightInTonnes = airplaneDto.WeightInTonnes;
                airplane.SerialNumber = airplaneDto.SerialNumber;
                airplane.AirlineCode = airplaneDto.AirlineCode;

                this.airplaneRepository.Update(airplane);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the all Airplane actives.
        /// </summary>
        /// <returns>
        /// List of actives Airplane.
        /// </returns>
        public IList<AirplaneDto> GetActivesAirplane()
        {
            try
            {
                IList<Airplane> airplaneEntity = this.airplaneRepository.GetActiveAirplane();
                IList<AirplaneDto> airplaneDto = Mapper.Map<IList<Airplane>, IList<AirplaneDto>>(airplaneEntity);
                return airplaneDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Determines whether equipment number duplicate is.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns><c>true</c> if duplicate, <c>false</c> otherwise.</returns>
        private bool IsEquipmentNumberDuplicate(string equipmentNumber)
        {
            Airplane airplane = this.airplaneRepository.FindById(equipmentNumber);
            return airplane != null ? true : false;
        }
    }
}
