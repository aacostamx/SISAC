//------------------------------------------------------------------------
// <copyright file="ManifestTimeConfigBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using AutoMapper;
    using ExceptionBusiness;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using Entities.Airport;
    using Resources;

    /// <summary>
    /// class AirporBusiness than implement IAirportBusiness
    /// </summary>
    public class ManifestTimeConfigBusiness : IManifestTimeConfigBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IManifestTimeConfigRepository ManifestTimeConfigRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestTimeConfigBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="ManifestTimeConfigRepository">The ManifestTimeConfig repository.</param>
        public ManifestTimeConfigBusiness(IUnitOfWork unitOfWork, IManifestTimeConfigRepository ManifestTimeConfigRepository)
        {
            this.unitOfWork = unitOfWork;
            this.ManifestTimeConfigRepository = ManifestTimeConfigRepository;
        }

        /// <summary>
        /// Gets all ManifestTimeConfig.
        /// </summary>
        /// <returns>
        /// List of ManifestTimeConfigs.
        /// </returns>
        public IList<ManifestTimeConfigDto> GetAllManifestTimeConfigs()
        {
            try
            {
                IList<ManifestTimeConfig> ManifestTimeConfigModel = this.ManifestTimeConfigRepository.GetAll();
                IList<ManifestTimeConfigDto> ManifestTimeConfigDTO = Mapper.Map<IList<ManifestTimeConfig>, IList<ManifestTimeConfigDto>>(ManifestTimeConfigModel);
                return ManifestTimeConfigDTO.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the ManifestTimeConfig by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity ManifestTimeConfig.
        /// </returns>
        public ManifestTimeConfigDto FindManifestTimeConfigById(long id)
        {
            try
            {
                ManifestTimeConfig ManifestTimeConfigModel = this.ManifestTimeConfigRepository.FindById(id);
                ManifestTimeConfigDto ManifestTimeConfigDTO = Mapper.Map<ManifestTimeConfig, ManifestTimeConfigDto>(ManifestTimeConfigModel);
                return ManifestTimeConfigDTO;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the ManifestTimeConfig.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddManifestTimeConfig(ManifestTimeConfigDto dto)
        {
            if (this.IsDuplicate(dto.ManifestTimeConfigID))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
            {
                ManifestTimeConfig ManifestTimeConfigModel = Mapper.Map<ManifestTimeConfigDto, ManifestTimeConfig>(dto);
                ManifestTimeConfigModel.Status = true;
                this.ManifestTimeConfigRepository.Add(ManifestTimeConfigModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Deletes the ManifestTimeConfig.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteManifestTimeConfig(ManifestTimeConfigDto dto)
        {
            try
            {
                ManifestTimeConfig ManifestTimeConfigModel = this.ManifestTimeConfigRepository.FindById(dto.ManifestTimeConfigID);
                ManifestTimeConfigModel.Status = false;
                this.ManifestTimeConfigRepository.Update(ManifestTimeConfigModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Physical ManifestTimeConfig delete
        /// </summary>
        /// <param name="dto">ManifestTimeConfig</param>
        /// <returns>boolean</returns>
        public bool PhysicalDeleteManifestTimeConfig(ManifestTimeConfigDto dto)
        {
            try
            {
                ManifestTimeConfig ManifestTimeConfigModel = Mapper.Map<ManifestTimeConfigDto, ManifestTimeConfig>(dto);
                this.ManifestTimeConfigRepository.Delete(ManifestTimeConfigModel);
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Updates the ManifestTimeConfig.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateManifestTimeConfig(ManifestTimeConfigDto dto)
        {
            try
            {
                ManifestTimeConfig ManifestTimeConfigModel = Mapper.Map<ManifestTimeConfigDto, ManifestTimeConfig>(dto);
                ManifestTimeConfigModel.Status = true;
                this.ManifestTimeConfigRepository.Update(ManifestTimeConfigModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets the actives ManifestTimeConfigs.
        /// </summary>
        /// <returns>ManifestTimeConfigs Actives.</returns>
        public IList<ManifestTimeConfigDto> GetActivesManifestTimeConfigs()
        {
            try
            {
                IList<ManifestTimeConfig> ManifestTimeConfigModel = this.ManifestTimeConfigRepository.GetActivesManifestTimeConfigs();
                IList<ManifestTimeConfigDto> ManifestTimeConfigDTO = Mapper.Map<IList<ManifestTimeConfig>, IList<ManifestTimeConfigDto>>(ManifestTimeConfigModel);
                return ManifestTimeConfigDTO;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Determines whether entity Code duplicate is.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsDuplicate(long id)
        {
            ManifestTimeConfig entity = this.ManifestTimeConfigRepository.FindById(id);
            return entity != null ? true : false;
        }
    }
}
