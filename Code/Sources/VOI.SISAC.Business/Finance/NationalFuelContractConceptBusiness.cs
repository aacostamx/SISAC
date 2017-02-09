//------------------------------------------------------------------------
// <copyright file="NationalFuelContractConceptBusiness.cs" company="AACOSTA">
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
    /// National Fuel Contract Concept Business
    /// </summary>
    public class NationalFuelContractConceptBusiness : INationalFuelContractConceptBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The airport repository
        /// </summary>
        private readonly INationalFuelContractConceptRepository fuelConceptRepository;

        /// <summary>
        /// Constructor International Fuel Contract Concept Business
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="fuelConceptRepository">The fuel concept repository.</param>
        public NationalFuelContractConceptBusiness(IUnitOfWork unitOfWork, INationalFuelContractConceptRepository fuelConceptRepository)
        {
            this.unitOfWork = unitOfWork;
            this.fuelConceptRepository = fuelConceptRepository;
        }

        /// <summary>
        /// Add international fuel concept
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>
        /// true if was added else false
        /// </returns>
        public bool AddFuelConcept(NationalFuelContractConceptDto dto)
        {
            try
            {
                NationalFuelContractConcept entity = Mapper.Map<NationalFuelContractConceptDto, NationalFuelContractConcept>(dto);
                this.fuelConceptRepository.Add(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Delete International Fuel Concept
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>
        /// true if was deleted else false
        /// </returns>
        public bool DeleteFuelConcept(NationalFuelContractConceptDto dto)
        {
            try
            {
                NationalFuelContractConcept entity = Mapper.Map<NationalFuelContractConceptDto, NationalFuelContractConcept>(dto);
                entity = this.fuelConceptRepository.FindById(entity);
                this.fuelConceptRepository.Delete(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Find International Fuel Concept
        /// </summary>
        /// <param name="dto">The identifier.</param>
        /// <returns>
        /// Entity national Fuel Contract Concept.
        /// </returns>
        public NationalFuelContractConceptDto FindFuelConceptById(NationalFuelContractConceptDto dto)
        {
            try
            {
                NationalFuelContractConcept dtoToFind = Mapper.Map<NationalFuelContractConceptDto, NationalFuelContractConcept>(dto);
                NationalFuelContractConcept entity = this.fuelConceptRepository.FindById(dtoToFind);
                NationalFuelContractConceptDto dtoNew = Mapper.Map<NationalFuelContractConcept, NationalFuelContractConceptDto>(entity);
                return dtoNew;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Get All Fuel Concepts
        /// </summary>
        /// <returns>
        /// List of FuelConcepts.
        /// </returns>
        public IList<NationalFuelContractConceptDto> GetAllFuelConcept()
        {
            try
            {
                IList<NationalFuelContractConcept> entity = this.fuelConceptRepository.GetFuelContractConcepts();
                IList<NationalFuelContractConceptDto> contractDto = Mapper.Map<IList<NationalFuelContractConcept>, IList<NationalFuelContractConceptDto>>(entity);
                return contractDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Update Fuel Concept
        /// </summary>
        /// <param name="dto">The entity.</param>
        /// <returns>
        /// true if was updated else false
        /// </returns>
        public bool UpdateFuelConcept(NationalFuelContractConceptDto dto)
        {
            try
            {
                NationalFuelContractConcept entity = Mapper.Map<NationalFuelContractConceptDto, NationalFuelContractConcept>(dto);
                this.fuelConceptRepository.Update(entity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedUpdateRecord + Messages.SeeInnerException, exception);
            }
        }
    }
}
