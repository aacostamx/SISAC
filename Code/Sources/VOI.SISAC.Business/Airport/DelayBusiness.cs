//------------------------------------------------------------------------
// <copyright file="DelayBusiness.cs" company="AACOSTA">
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
    public class DelayBusiness : IDelayBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IDelayRepository DelayRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelayBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="DelayRepository">The Delay repository.</param>
        public DelayBusiness(IUnitOfWork unitOfWork, IDelayRepository DelayRepository)
        {
            this.unitOfWork = unitOfWork;
            this.DelayRepository = DelayRepository;
        }

        /// <summary>
        /// Gets all Delay.
        /// </summary>
        /// <returns>
        /// List of Delays.
        /// </returns>
        public IList<DelayDto> GetAllDelays()
        {
            try
            {
                IList<Delay> DelayModel = this.DelayRepository.GetAll();
                IList<DelayDto> DelayDTO = Mapper.Map<IList<Delay>, IList<DelayDto>>(DelayModel);
                return DelayDTO.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the Delay by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity Delay.
        /// </returns>
        public DelayDto FindDelayById(string id)
        {
            try
            {
                Delay DelayModel = this.DelayRepository.FindById(id);
                DelayDto DelayDTO = Mapper.Map<Delay, DelayDto>(DelayModel);
                return DelayDTO;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the Delay.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddDelay(DelayDto dto)
        {
            if (this.IsDuplicate(dto.DelayCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
            {
                Delay DelayModel = Mapper.Map<DelayDto, Delay>(dto);
                DelayModel.Status = true;
                this.DelayRepository.Add(DelayModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Deletes the Delay.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteDelay(DelayDto dto)
        {
            try
            {
                Delay DelayModel = this.DelayRepository.FindById(dto.DelayCode);
                DelayModel.Status = false;
                this.DelayRepository.Update(DelayModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Physical Delay delete
        /// </summary>
        /// <param name="dto">Delay</param>
        /// <returns>boolean</returns>
        public bool PhysicalDeleteDelay(DelayDto dto)
        {
            try
            {
                Delay DelayModel = Mapper.Map<DelayDto, Delay>(dto);
                this.DelayRepository.Delete(DelayModel);
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Updates the Delay.
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateDelay(DelayDto dto)
        {
            try
            {
                Delay DelayModel = Mapper.Map<DelayDto, Delay>(dto);
                DelayModel.Status = true;
                this.DelayRepository.Update(DelayModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets the actives Delays.
        /// </summary>
        /// <returns>Delays Actives.</returns>
        public IList<DelayDto> GetActivesDelays()
        {
            try
            {
                IList<Delay> DelayModel = this.DelayRepository.GetActivesDelays();
                IList<DelayDto> DelayDTO = Mapper.Map<IList<Delay>, IList<DelayDto>>(DelayModel);
                return DelayDTO;
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
            Delay entity = this.DelayRepository.FindById(id);
            return entity != null ? true : false;
        }
    }
}
