//------------------------------------------------------------------------
// <copyright file="DrinkingWaterBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

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

    /// <summary>
    /// DrinkingWater business logic
    /// </summary>
    public class DrinkingWaterBusiness : IDrinkingWaterBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The drinking water repository
        /// </summary>
        private readonly IDrinkingWaterRepository drinkingWaterRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrinkingWaterBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="drinkingWaterRepository">The drinking water.</param>
        public DrinkingWaterBusiness(IUnitOfWork unitOfWork, IDrinkingWaterRepository drinkingWaterRepository)
        {
            this.unitOfWork = unitOfWork;
            this.drinkingWaterRepository = drinkingWaterRepository;
        }

        /// <summary>
        /// Finds the drinking water by its identifier.
        /// </summary>
        /// <param name="id">The drinking water identifier.</param>
        /// <returns>
        /// Drinking water DTO.
        /// </returns>
        public DrinkingWaterDto FindDrinkingWaterById(long id)
        {
            if (id <= 0)
            {
                return null;
            }

            try
            {
                DrinkingWater water = this.drinkingWaterRepository.FindById(id);
                DrinkingWaterDto waterDto = Mapper.Map<DrinkingWater, DrinkingWaterDto>(water);
                return waterDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds a new drinking water.
        /// </summary>
        /// <param name="waterDto">The new drinking water.</param>
        /// <returns>
        ///   <c>true</c> if was added <c>false</c> otherwise.
        /// </returns>
        public bool AddDrinkingWater(DrinkingWaterDto waterDto)
        {
            if (waterDto == null)
            {
                return false;
            }

            if (this.IsValueDuplicatePerAirplane(waterDto))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicateValue, 11);
            }

            try
            {
                DrinkingWater water = Mapper.Map<DrinkingWaterDto, DrinkingWater>(waterDto);
                water.Status = true;
                this.drinkingWaterRepository.Add(water);
                this.unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }

            return false;
        }

        /// <summary>
        /// Deletes the DrinkingWater.
        /// </summary>
        /// <param name="waterDto">The drinking water to delete.</param>
        /// <returns>
        ///   <c>true</c> if was deleted <c>false</c> otherwise.
        /// </returns>
        public bool DeleteDrinkingWater(DrinkingWaterDto waterDto)
        {
            if (waterDto == null)
            {
                return false;
            }

            try
            {
                DrinkingWater water = this.drinkingWaterRepository.FindById(waterDto.DrinkingWaterId);
                water.Status = false;
                this.drinkingWaterRepository.Update(water);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the drinking water.
        /// </summary>
        /// <param name="waterDto">The drinking water to edit.</param>
        /// <returns>
        ///   <c>true</c> if was updated <c>false</c> otherwise.
        /// </returns>
        public bool UpdateDrinkingWater(DrinkingWaterDto waterDto)
        {
            if (waterDto == null)
            {
                return false;
            }

            if (this.IsValueDuplicatePerAirplane(waterDto))
            {
                throw new BusinessException(Messages.FailedUpdateRecord, Messages.DuplicateValue, 11);
            }

            try
            {
                DrinkingWater water = this.drinkingWaterRepository.FindById(waterDto.DrinkingWaterId);

                // Updating the enity
                water.DrinkingWaterName = waterDto.DrinkingWaterName;
                water.Value = waterDto.Value;
                water.EquipmentNumber = waterDto.EquipmentNumber;

                this.drinkingWaterRepository.Update(water);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets all actives drinking water.
        /// </summary>
        /// <returns>List of actives drinking water.</returns>
        public IList<DrinkingWaterDto> GetActivesDrinkingWater()
        {
            try
            {
                IList<DrinkingWater> water = this.drinkingWaterRepository.GetActiveDrinkingWater();
                IList<DrinkingWaterDto> waterDto = Mapper.Map<IList<DrinkingWater>, IList<DrinkingWaterDto>>(water);
                return waterDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the all drinking water related in an airplane.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="onlyActives">if set to <c>true</c> gets the drinking waters actives, otherwise gets all drinking waters.</param>
        /// <returns>
        /// List of drinking waters related with an airplane.
        /// </returns>
        public IList<DrinkingWaterDto> GetDrinkingWatersByAirplaneId(string id, bool onlyActives)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            try
            {
                IList<DrinkingWater> water = this.drinkingWaterRepository.GetDrinkingWatersByAirplaneId(id, onlyActives);
                IList<DrinkingWaterDto> waterDto = Mapper.Map<IList<DrinkingWater>, IList<DrinkingWaterDto>>(water);
                return waterDto;
            }
            catch (Exception excception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, excception);
            }
        }

        /// <summary>
        /// Validates if a duplicated value is inserted in the same Airplane.
        /// </summary>
        /// <param name="waterDto">The water dto.</param>
        /// <returns>
        ///   <c>true</c> if duplicate, <c>false</c> other wise.
        /// </returns>
        private bool IsValueDuplicatePerAirplane(DrinkingWaterDto waterDto)
        {
            IList<DrinkingWater> waters = this.drinkingWaterRepository
                .GetDrinkingWatersByAirplaneId(waterDto.EquipmentNumber, false);

            DrinkingWater waterWithValue = waters.FirstOrDefault(c => c.Value == waterDto.Value);
            return waterWithValue != null ? true : false;
        }
    }
}
