//------------------------------------------------------------------------
// <copyright file="GpuObservationBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.GpuObservation
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
    using Entities.Airport;


    /// <summary>
    /// class AirporBusiness than implement IGpuObservationBusiness
    /// </summary>
    public class GpuObservationBusiness : IGpuObservationBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The GpuObservation repository
        /// </summary>
        private readonly IGpuObservationRepository GpuObservationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpuObservationBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="GpuObservationRepository">The GpuObservation repository.</param>
        public GpuObservationBusiness(IUnitOfWork unitOfWork, IGpuObservationRepository GpuObservationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.GpuObservationRepository = GpuObservationRepository;
        }

        /// <summary>
        /// Gets all GpuObservation.
        /// </summary>
        /// <returns>
        /// List of GpuObservations.
        /// </returns>
        public IList<GpuObservationDto> GetAllGpuObservation()
        {
            try
            {
                IList<GpuObservation> GpuObservationModel = this.GpuObservationRepository.GetAll();
                IList<GpuObservationDto> GpuObservationDTO = Mapper.Map<IList<GpuObservation>, IList<GpuObservationDto>>(GpuObservationModel);
                return GpuObservationDTO.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the GpuObservation by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity GpuObservation.
        /// </returns>
        public GpuObservationDto FindGpuObservationById(string id)
        {
            try
            {
                GpuObservation GpuObservationModel = this.GpuObservationRepository.FindById(id);
                GpuObservationDto GpuObservationDTO = Mapper.Map<GpuObservation, GpuObservationDto>(GpuObservationModel);
                return GpuObservationDTO;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the GpuObservation.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddGpuObservation(GpuObservationDto dto)
        {
            if (this.IsDuplicate(dto.GpuObservationCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
            {
                GpuObservation GpuObservationModel = Mapper.Map<GpuObservationDto, GpuObservation>(dto);
                GpuObservationModel.Status = true;
                this.GpuObservationRepository.Add(GpuObservationModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Deletes the GpuObservation.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteGpuObservation(GpuObservationDto dto)
        {
            try
            {
                GpuObservation GpuObservationModel = this.GpuObservationRepository.FindById(dto.GpuObservationCode);
                GpuObservationModel.Status = false;
                this.GpuObservationRepository.Update(GpuObservationModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Physical GpuObservation delete
        /// </summary>
        /// <param name="dto">GpuObservation</param>
        /// <returns>boolean</returns>
        public bool PhysicalDeleteGpuObservation(GpuObservationDto dto)
        {
            try
            {
                GpuObservation GpuObservationModel = Mapper.Map<GpuObservationDto, GpuObservation>(dto);
                this.GpuObservationRepository.Delete(GpuObservationModel);
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Updates the GpuObservation.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateGpuObservation(GpuObservationDto dto)
        {
            try
            {
                GpuObservation GpuObservationModel = Mapper.Map<GpuObservationDto, GpuObservation>(dto);
                GpuObservationModel.Status = true;
                this.GpuObservationRepository.Update(GpuObservationModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets the actives GpuObservations.
        /// </summary>
        /// <returns>GpuObservations Actives.</returns>
        public IList<GpuObservationDto> GetActivesGpuObservations()
        {
            try
            {
                IList<GpuObservation> GpuObservationModel = this.GpuObservationRepository.GetActiveGpuObservation();
                IList<GpuObservationDto> GpuObservationDTO = Mapper.Map<IList<GpuObservation>, IList<GpuObservationDto>>(GpuObservationModel);
                return GpuObservationDTO;
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
            GpuObservation entity = this.GpuObservationRepository.FindById(id);
            return entity != null ? true : false;
        }
    }
}
