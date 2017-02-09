//------------------------------------------------------------------------
// <copyright file="InternationalFuelContractConceptBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Finance;
    using Dto.Finances;
    using Entities.Finance;
    using ExceptionBusiness;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// International Fuel Contract Concept Business
    /// </summary>
    public class InternationalFuelContractConceptBusiness : IInternationalFuelContractConceptBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly IInternationalFuelContractConceptRepository fuelConceptRepository;

        /// <summary>
        /// Constructor International Fuel Contract Concept Business
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="fuelConceptRepository"></param>
        public InternationalFuelContractConceptBusiness(IUnitOfWork unitOfWork, IInternationalFuelContractConceptRepository fuelConceptRepository)
        {
            this.unitOfWork = unitOfWork;
            this.fuelConceptRepository = fuelConceptRepository;
        }

        /// <summary>
        /// Add international fuel concept
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool AddFuelConcept(InternationalFuelContractConceptDto dto)
        {
            try
            {
                InternationalFuelContractConcept entity = Mapper.Map<InternationalFuelContractConceptDto, InternationalFuelContractConcept>(dto);
                this.fuelConceptRepository.Add(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Delete International Fuel Concept
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool DeleteFuelConcept(InternationalFuelContractConceptDto dto)
        {
            try
            {
                InternationalFuelContractConcept entity = Mapper.Map<InternationalFuelContractConceptDto, InternationalFuelContractConcept>(dto);
                entity = this.fuelConceptRepository.FindById(entity);
                this.fuelConceptRepository.Delete(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Find International Fuel Concept
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public InternationalFuelContractConceptDto FindFuelConceptById(InternationalFuelContractConceptDto dto)
        {
            try
            {
                InternationalFuelContractConcept dtoToFind = Mapper.Map<InternationalFuelContractConceptDto, InternationalFuelContractConcept>(dto);
                InternationalFuelContractConcept entity = this.fuelConceptRepository.FindById(dtoToFind);
                InternationalFuelContractConceptDto dtoNew = Mapper.Map<InternationalFuelContractConcept, InternationalFuelContractConceptDto>(entity);
                return dtoNew;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Get All Fuel Concepts
        /// </summary>
        /// <returns></returns>
        public IList<InternationalFuelContractConceptDto> GetAllFuelConcept()
        {
            try
            {
                IList<InternationalFuelContractConcept> entity = this.fuelConceptRepository.GetFuelContractsConcepts();
                IList<InternationalFuelContractConceptDto> contractDto = Mapper.Map<IList<InternationalFuelContractConcept>, IList<InternationalFuelContractConceptDto>>(entity);
                return contractDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Update Fuel Concept
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool UpdateFuelConcept(InternationalFuelContractConceptDto dto)
        {
            try
            {
                InternationalFuelContractConcept entity = Mapper.Map<InternationalFuelContractConceptDto, InternationalFuelContractConcept>(dto);
                this.fuelConceptRepository.Update(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, ex);
            }
        }
    }
}
