//------------------------------------------------------------------------
// <copyright file="FunctionalAreaBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.MapConfiguration;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Catalog;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Functional Area Business
    /// </summary>
    public class FunctionalAreaBusiness : IFunctionalAreaBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The FunctionalArea repository
        /// </summary>
        private readonly IFunctionalAreaRepository functionalAreaRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionalAreaBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="functionalAreaRepository">The FunctionalArea repository.</param>
        public FunctionalAreaBusiness(IUnitOfWork unitOfWork, IFunctionalAreaRepository functionalAreaRepository)
        {
            this.unitOfWork = unitOfWork;
            this.functionalAreaRepository = functionalAreaRepository;
        }

        /// <summary>
        /// Gets all FunctionalAreas.
        /// </summary>
        /// <returns>
        /// List of FunctionalAreas.
        /// </returns>
        public IList<FunctionalAreaDto> GetAllFunctionalAreas()
        {
            try
            {
                IList<FunctionalArea> functionalAreas = this.functionalAreaRepository.GetAll().ToList();
                IList<FunctionalAreaDto> functionalAreasDto = new List<FunctionalAreaDto>();

                functionalAreasDto = Mapper.Map<IList<FunctionalArea>, IList<FunctionalAreaDto>>(functionalAreas);
                return functionalAreasDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Finds the FunctionalArea by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// Entity FunctionalArea.
        /// </returns>
        public FunctionalAreaDto FindFunctionalAreaById(long id)
        {            
            try
            {
                FunctionalArea functionalArea = this.functionalAreaRepository.FindById(id);
                FunctionalAreaDto functionalAreaDto = new FunctionalAreaDto();
                functionalAreaDto = Mapper.Map<FunctionalArea, FunctionalAreaDto>(functionalArea);

                return functionalAreaDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Adds the FunctionalArea.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was added else false</returns>
        public bool AddFunctionalArea(FunctionalAreaDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (this.IsFunctionalAreaIDDuplicate(entity.FunctionalAreaID))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                FunctionalArea functionalArea = new FunctionalArea();
                entity.Status = true;
                functionalArea = Mapper.Map<FunctionalArea>(entity);
                this.functionalAreaRepository.Add(functionalArea);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Deletes the FunctionalArea.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was deleted else false</returns>
        public bool DeleteFunctionalArea(FunctionalAreaDto entity)
        {
            try
            {
                FunctionalArea functionalArea = this.functionalAreaRepository.FindById(entity.FunctionalAreaID);

                if (functionalArea != null)
                {
                    //// Para borrado lógico
                    functionalArea.Status = false;
                    this.functionalAreaRepository.Update(functionalArea);
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
        /// Updates the FunctionalArea.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>true if was updated else false</returns>
        public bool UpdateFunctionalArea(FunctionalAreaDto entity)
        {
            try
            {
                if (entity != null)
                {
                    if (entity.FunctionalAreaID != null)
                    {
                        FunctionalArea functionalArea = this.functionalAreaRepository.FindById(entity.FunctionalAreaID);

                        if (functionalArea.FunctionalAreaID != null)
                        {
                            functionalArea.FunctionalAreaName = entity.FunctionalAreaName;
                            functionalArea.FunctionalAreaDescription = entity.FunctionalAreaDescription;
                            this.functionalAreaRepository.Update(functionalArea);
                            this.unitOfWork.Commit();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Gets the actives FunctionalAreas.
        /// </summary>
        /// <returns>FunctionalAreas Actives.</returns>
        public IList<FunctionalAreaDto> GetActivesFunctionalAreas()
        {
            try
            {
                IList<FunctionalArea> functionalAreas = this.functionalAreaRepository.GetActivesFunctionalAreas().ToList();
                IList<FunctionalAreaDto> functionalAreasDto = new List<FunctionalAreaDto>();

                functionalAreasDto = Mapper.Map<IList<FunctionalArea>, IList<FunctionalAreaDto>>(functionalAreas);
                return functionalAreasDto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Is FunctionalArea ID Duplicate
        /// </summary>
        /// <param name="functionalAreaID">functional Area ID</param>
        /// <returns>true or false</returns>
        private bool IsFunctionalAreaIDDuplicate(long functionalAreaID)
        {
            FunctionalArea encontrado = this.functionalAreaRepository.FindById(functionalAreaID);
            return encontrado != null ? true : false;
        }
    }
}
