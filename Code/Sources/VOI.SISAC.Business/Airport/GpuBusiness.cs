//------------------------------------------------------------------------
// <copyright file="GpuBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Gpu
{
    using AutoMapper;
    using Dal.Repository.Airports;
    using Dto.Airports;
    using ExceptionBusiness;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Resources;
    using VOI.SISAC.Dal.Infrastructure;


    /// <summary>
    /// class AirporBusiness than implement IGpuBusiness
    /// </summary>
    public class GpuBusiness : IGpuBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The Gpu repository
        /// </summary>
        private readonly IGpuRepository GpuRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpuBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="GpuRepository">The Gpu repository.</param>
        public GpuBusiness(IUnitOfWork unitOfWork, IGpuRepository GpuRepository)
        {
            this.unitOfWork = unitOfWork;
            this.GpuRepository = GpuRepository;
        }

        /// <summary>
        /// Gets all Gpu.
        /// </summary>
        /// <returns>
        /// List of Gpus.
        /// </returns>
        public IList<GpuDto> GetAllGpu()
        {
            try
            {
                IList<Entities.Airport.Gpu> GpuModel = this.GpuRepository.GetAll();
                IList<GpuDto> GpuDTO = Mapper.Map<IList<Entities.Airport.Gpu>, IList<GpuDto>>(GpuModel);
                return GpuDTO.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the Gpu by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity Gpu.
        /// </returns>
        public GpuDto FindGpuById(string id)
        {
            try
            {
                Entities.Airport.Gpu GpuModel = this.GpuRepository.FindById(id);
                GpuDto GpuDTO = Mapper.Map<Entities.Airport.Gpu, GpuDto>(GpuModel);
                return GpuDTO;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the Gpu.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddGpu(GpuDto dto)
        {
            if (this.IsDuplicate(dto.GpuCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
            {
                Entities.Airport.Gpu GpuModel = Mapper.Map<GpuDto, Entities.Airport.Gpu>(dto);
                GpuModel.Status = true;
                this.GpuRepository.Add(GpuModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Deletes the Gpu.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteGpu(GpuDto dto)
        {
            try
            {
                Entities.Airport.Gpu GpuModel = this.GpuRepository.FindById(dto.GpuCode);
                GpuModel.Status = false;
                this.GpuRepository.Update(GpuModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Physical Gpu delete
        /// </summary>
        /// <param name="dto">Gpu</param>
        /// <returns>boolean</returns>
        public bool PhysicalDeleteGpu(GpuDto dto)
        {
            try
            {
                Entities.Airport.Gpu GpuModel = Mapper.Map<GpuDto, Entities.Airport.Gpu>(dto);
                this.GpuRepository.Delete(GpuModel);
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Updates the Gpu.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateGpu(GpuDto dto)
        {
            try
            {
                Entities.Airport.Gpu GpuModel = Mapper.Map<GpuDto, Entities.Airport.Gpu>(dto);
                GpuModel.Status = true;
                this.GpuRepository.Update(GpuModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets the actives Gpus.
        /// </summary>
        /// <returns>Gpus Actives.</returns>
        public IList<GpuDto> GetActivesGpus()
        {
            try
            {
                IList<Entities.Airport.Gpu> GpuModel = this.GpuRepository.GetActiveGpu();
                IList<GpuDto> GpuDTO = Mapper.Map<IList<Entities.Airport.Gpu>, IList<GpuDto>>(GpuModel);
                return GpuDTO;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets the Gpu by its station.
        /// </summary>
        /// <param name="stationCode">The station code.</param>
        /// <param name="onlyActives">if set to <c>true</c> bring only actives records.</param>
        /// <returns>
        /// Gpus Actives.
        /// </returns>
        public IList<GpuDto> GetGpuByStation(string stationCode, bool onlyActives)
        {
            try
            {
                IList<Entities.Airport.Gpu> GpuModel = this.GpuRepository.GetGpuByStation(stationCode, onlyActives);
                IList<GpuDto> GpuDTO = Mapper.Map<IList<Entities.Airport.Gpu>, IList<GpuDto>>(GpuModel);
                return GpuDTO;
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
        private bool IsDuplicate(string id)
        {
            Entities.Airport.Gpu entity = this.GpuRepository.FindById(id);
            return entity != null ? true : false;
        }
    }
}
