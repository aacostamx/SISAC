//------------------------------------------------------------------------
// <copyright file="AirplaneTypeBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    public class AirplaneTypeBusiness : IAirplaneTypeBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airplane type repository
        /// </summary>
        private readonly IAirplaneTypeRepository airplaneTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneTypeBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="airplaneTypeRepository">The airplane type repository.</param>
        public AirplaneTypeBusiness(IUnitOfWork unitOfWork, IAirplaneTypeRepository airplaneTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.airplaneTypeRepository = airplaneTypeRepository;
        }

        /// <summary>
        /// Gets all Airplane Types.
        /// </summary>
        /// <returns>All Airport types.</returns>
        public IList<AirplaneTypeDto> GetAllAirplaneType()
        {
            try
            {
                IList<AirplaneType> airplaneTypeEntity = this.airplaneTypeRepository.GetAll();
                IList<AirplaneTypeDto> airplaneTypeDto = Mapper.Map<IList<AirplaneType>, IList<AirplaneTypeDto>>(airplaneTypeEntity);
                return airplaneTypeDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the Airplane Type by its identifier.
        /// </summary>
        /// <param name="id">The Currency identifier.</param>
        /// <returns>
        /// Currency Entity.
        /// </returns>
        public AirplaneTypeDto FindAirplaneTypeById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            try
            {
                AirplaneType airplaneTypeEntity = this.airplaneTypeRepository.FindById(id);
                AirplaneTypeDto airplaneTypeDto = Mapper.Map<AirplaneType, AirplaneTypeDto>(airplaneTypeEntity);
                return airplaneTypeDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds a new Airplane Type.
        /// </summary>
        /// <param name="airplaneTypeDto">The entity</param>
        /// <returns>
        ///   <c>true</c> if was added <c>false</c> otherwise.
        /// </returns>
        public bool AddAirplaneType(AirplaneTypeDto airplaneTypeDto)
        {
            if (airplaneTypeDto == null)
            {
                return false;
            }

            if (this.IsAirplaneModelDuplicate(airplaneTypeDto.AirplaneModel))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                AirplaneType airplaneType = new AirplaneType();
                airplaneType = Mapper.Map<AirplaneTypeDto, AirplaneType>(airplaneTypeDto);
                airplaneType.Status = true;
                this.airplaneTypeRepository.Add(airplaneType);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }        

        /// <summary>
        /// Deletes the Airplane Type.
        /// </summary>
        /// <param name="airplaneTypeDto">The entity.</param>
        /// <returns>
        ///   <c>true</c> if was deleted <c>false</c> otherwise.
        /// </returns>
        public bool DeleteAirplaneType(AirplaneTypeDto airplaneTypeDto)
        {
            if (airplaneTypeDto == null)
            {
                return false;
            }

            try
            {
                AirplaneType airplaneType = this.airplaneTypeRepository.FindById(airplaneTypeDto.AirplaneModel);

                // Para borrado lógico
                airplaneType.Status = false;
                this.airplaneTypeRepository.Update(airplaneType);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the Airplane Type.
        /// </summary>
        /// <param name="airplaneTypeDto">The entity.</param>
        /// <returns>
        ///   <c>true</c> if was updated <c>false</c> otherwise.
        /// </returns>
        public bool UpdateAirplaneType(AirplaneTypeDto airplaneTypeDto)
        {
            if (airplaneTypeDto == null)
            {
                return false;
            }

            try
            {
                AirplaneType airplaneType = this.airplaneTypeRepository.FindById(airplaneTypeDto.AirplaneModel);

                // Update the entity
                airplaneType.AirplaneModel = airplaneTypeDto.AirplaneModel;
                airplaneType.CompartmentTypeCode = airplaneTypeDto.CompartmentTypeCode;
                airplaneType.CrewCapacity = airplaneTypeDto.CrewCapacity;
                airplaneType.EmptyOperatingWeight = airplaneTypeDto.EmptyOperatingWeight;
                airplaneType.FilmingMaximumWeight = airplaneTypeDto.FilmingMaximumWeight;
                airplaneType.FuelInGallon = airplaneTypeDto.FuelInGallon;
                airplaneType.FuelInKg = airplaneTypeDto.FuelInKg;
                airplaneType.FuelInLiters = airplaneTypeDto.FuelInLiters;
                airplaneType.GroupWeight = airplaneTypeDto.GroupWeight;
                airplaneType.Magnitude = airplaneTypeDto.Magnitude;
                airplaneType.MaximumLandingWeight = airplaneTypeDto.MaximumLandingWeight;
                airplaneType.MaximumTakeoffWeight = airplaneTypeDto.MaximumTakeoffWeight;
                airplaneType.MaximumZeroFuelWeight = airplaneTypeDto.MaximumZeroFuelWeight;
                airplaneType.PassengerCapacity = airplaneTypeDto.PassengerCapacity;
                airplaneType.TakeoffWeightInTonnes = airplaneTypeDto.TakeoffWeightInTonnes;
                airplaneType.WeightInPound = airplaneTypeDto.WeightInPound;
                airplaneType.WeightInTonnes = airplaneTypeDto.WeightInTonnes;

                this.airplaneTypeRepository.Update(airplaneType);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the all Airplane Type actives.
        /// </summary>
        /// <returns>
        /// List of actives Airplane Types.
        /// </returns>
        public IList<AirplaneTypeDto> GetActivesAirplaneType()
        {
            try
            {
                IList<AirplaneType> airplaneTypeEntity = this.airplaneTypeRepository.GetActiveAirplaneType();
                IList<AirplaneTypeDto> airplaneTypeDto = Mapper.Map<IList<AirplaneType>, IList<AirplaneTypeDto>>(airplaneTypeEntity);
                return airplaneTypeDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Determines whether airplane model is duplicate.
        /// </summary>
        /// <param name="airplaneModel">The airplane model.</param>
        /// <returns><c>true</c> if duplicate, <c>false</c> otherwise.</returns>
        private bool IsAirplaneModelDuplicate(string airplaneModel)
        {
            AirplaneType airplaneType = this.airplaneTypeRepository.FindById(airplaneModel);
            return airplaneType != null ? true : false;
        }
    }
}
