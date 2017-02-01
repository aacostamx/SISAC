//------------------------------------------------------------------------
// <copyright file="FuelConceptBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.MapConfiguration;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Fuel Concept Business
    /// </summary>
    public class FuelConceptBusiness : IFuelConceptBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The FuelConcept repository
        /// </summary>
        private readonly IFuelConceptRepository fuelConceptRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuelConceptBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="fuelConceptRepository">The FuelConcept repository.</param>
        public FuelConceptBusiness(IUnitOfWork unitOfWork, IFuelConceptRepository fuelConceptRepository)
        {
            this.unitOfWork = unitOfWork;
            this.fuelConceptRepository = fuelConceptRepository;
        }

        /// <summary>
        /// Gets all FuelConcepts.
        /// </summary>
        /// <returns>
        /// List of FuelConcepts.
        /// </returns>
        public IList<FuelConceptDto> GetAllFuelConcepts()
        {
            try
            {
                IList<FuelConcept> fuelConcepts = this.fuelConceptRepository.GetAll().ToList();
                IList<FuelConceptDto> fuelConceptsDto = new List<FuelConceptDto>();

                fuelConceptsDto = Mapper.Map<IList<FuelConcept>, IList<FuelConceptDto>>(fuelConcepts);
                return fuelConceptsDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the FuelConcept by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity FuelConcept.
        /// </returns>
        public FuelConceptDto FindFuelConceptById(long id)
        {            
            try
            {
                FuelConcept fuelConcept = this.fuelConceptRepository.FindById(id);
                FuelConceptDto fuelConceptDto = new FuelConceptDto();
                fuelConceptDto = Mapper.Map<FuelConcept, FuelConceptDto>(fuelConcept);

                return fuelConceptDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the FuelConcept.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddFuelConcept(FuelConceptDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (this.IsFuelConceptIsDuplicate(entity.FuelConceptName))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                FuelConcept fuelConcept = new FuelConcept();
                entity.Status = true;
                fuelConcept = Mapper.Map<FuelConcept>(entity);
                this.fuelConceptRepository.Add(fuelConcept);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }        

        /// <summary>
        /// Deletes the FuelConcept.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteFuelConcept(FuelConceptDto entity)
        {
            try
            {
                FuelConcept fuelConcept = this.fuelConceptRepository.FindById(entity.FuelConceptID);

                if (fuelConcept != null)
                {
                    // Para borrado lógico
                    fuelConcept.Status = false;
                    this.fuelConceptRepository.Update(fuelConcept);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Updates the FuelConcept.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateFuelConcept(FuelConceptDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (this.IsFuelConceptIsDuplicate(entity.FuelConceptName))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                if (entity != null)
                {
                        FuelConcept fuelConcept = this.fuelConceptRepository.FindById(entity.FuelConceptID);

                            fuelConcept.FuelConceptName = entity.FuelConceptName;
                            this.fuelConceptRepository.Update(fuelConcept);
                            this.unitOfWork.Commit();
                            return true;                 
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets the actives FuelConcepts.
        /// </summary>
        /// <returns>FuelConcepts Actives.</returns>
        public IList<FuelConceptDto> GetActivesFuelConcepts()
        {
            try
            {
                IList<FuelConcept> fuelConcepts = this.fuelConceptRepository.GetActivesFuelConcepts().ToList();
                IList<FuelConceptDto> fuelConceptsDto = new List<FuelConceptDto>();

                fuelConceptsDto = Mapper.Map<IList<FuelConcept>, IList<FuelConceptDto>>(fuelConcepts);
                return fuelConceptsDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Is FuelConcept ID Duplicate
        /// </summary>
        /// <param name="fuelConceptName">The fuelConceptID.</param>
        /// <returns>true if was deleted else false</returns>
        private bool IsFuelConceptIsDuplicate(string fuelConceptName)
        {
            FuelConcept encontrado = this.fuelConceptRepository.FindByName(fuelConceptName);
            return encontrado != null ? true : false;
        }
    }
}
