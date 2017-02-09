//------------------------------------------------------------------------
// <copyright file="AirportGroupBusiness.cs" company="AACOSTA">
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
    using Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using Model = VOI.SISAC.Entities.Airport;
    using Entities.Airport;

    /// <summary>
    /// Class AirportGroupBusiness
    /// </summary>
    public class AirportGroupBusiness : IAirportGroupBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airportgroup repository
        /// </summary>
        private readonly IAirportGroupRepository airportgroupRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportGroupBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="airportgroupRepository">The airport repository.</param>
        public AirportGroupBusiness(IUnitOfWork unitOfWork, IAirportGroupRepository airportgroupRepository)
        {
            this.unitOfWork = unitOfWork;
            this.airportgroupRepository = airportgroupRepository;
        }

        /// <summary>
        /// Get all airport groups
        /// </summary>
        /// <returns>List of airport groups</returns>
        public IList<AirportGroupDto> GetAllAirportGroups()
        {
            try
            {
                IList<Model.AirportGroup> agroupModel = this.airportgroupRepository.GetAll();
                IList<AirportGroupDto> agroupsDTO = Mapper.Map<IList<Model.AirportGroup>, IList<AirportGroupDto>>(agroupModel);
                return agroupsDTO.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Find Airport Group by ID
        /// </summary>
        /// <param name="id">Airport Group ID</param>
        /// <returns>Aiport Group</returns>
        public AirportGroupDto FindAirportGroupById(string id)
        {
            try
            {
                Model.AirportGroup agroupModel = this.airportgroupRepository.FindById(id);
                AirportGroupDto agroupDTO = Mapper.Map<Model.AirportGroup, AirportGroupDto>(agroupModel);
                return agroupDTO;

            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Add new aiport group
        /// </summary>
        /// <param name="dto">Airport group object</param>
        /// <returns>boolean</returns>
        public bool AddAirportGroup(AirportGroupDto dto)
        {
            if (this.IsDuplicate(dto.AirportGroupCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
            {
                Model.AirportGroup agroupModel = Mapper.Map<AirportGroupDto, Model.AirportGroup>(dto);
                agroupModel.Status = true;
                this.airportgroupRepository.Add(agroupModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Logic delete
        /// </summary>
        /// <param name="dto">Airport group item</param>
        /// <returns>Boolean</returns>
        public bool DeleteAirportGroup(AirportGroupDto dto)
        {
            try
            {
                Model.AirportGroup agroupModel = this.airportgroupRepository.FindById(dto.AirportGroupCode);
                agroupModel.Status = false;
                this.airportgroupRepository.Update(agroupModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Physical delete
        /// </summary>
        /// <param name="dto">Airport Group</param>
        /// <returns>Boolean</returns>
        public bool PhysicalDeleteAirportGroup(AirportGroupDto dto)
        {
            try
            {
                Model.AirportGroup agroupModel = Mapper.Map<AirportGroupDto, Model.AirportGroup>(dto);
                this.airportgroupRepository.Delete(agroupModel);
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Update airport group
        /// </summary>
        /// <param name="dto">Airport group</param>
        /// <returns>rue</returns>
        public bool UpdateAirportGroup(AirportGroupDto dto)
        {
            try
            {
                Model.AirportGroup agroupModel = Mapper.Map<AirportGroupDto, Model.AirportGroup>(dto);
                agroupModel.Status = true;
                this.airportgroupRepository.Update(agroupModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Get Actives Airport Groups
        /// </summary>
        /// <returns>Return actives list of airport groups</returns>
        public IList<AirportGroupDto> GetActivesAirportGroups()
        {
            try
            {
                IList<Model.AirportGroup> agroupModel = this.airportgroupRepository.GetActivesAirportGroup();
                IList<AirportGroupDto> agroupsDto = Mapper.Map<IList<Model.AirportGroup>, IList<AirportGroupDto>>(agroupModel);
                return agroupsDto.OrderBy(c => c.AirportGroupCode).ToList();
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
            AirportGroup entity = this.airportgroupRepository.FindById(id);
            return entity != null ? true : false;
        }
    }
}