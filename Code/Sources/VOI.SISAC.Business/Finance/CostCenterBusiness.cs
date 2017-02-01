//------------------------------------------------------------------------
// <copyright file="CostCenterBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Business.Finance
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Finances;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.MapConfiguration;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Finance;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Class Cost Center 
    /// </summary>
    public class CostCenterBusiness : ICostCenterBusiness
    {
        /// <summary>
        /// The cost center repository
        /// </summary>
        private readonly ICostCenterRepository costCenterRepository;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;
        
        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="CostCenterBusiness"/> class.
        /// </summary>
        /// <param name="costCenterRepository">The cost center repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public CostCenterBusiness(ICostCenterRepository costCenterRepository, IUnitOfWork unitOfWork)
        {
            this.costCenterRepository = costCenterRepository;
            this.unitOfWork = unitOfWork;
        }

        #endregion

        #region ICostCenterBusiness Members

        /// <summary>
        /// Gets all cost center.
        /// </summary>
        /// <returns>IList CostCenterDto</returns>
        public IList<CostCenterDto> GetAllCostCenter()
        {
            try
            {
                IList<CostCenter> costCenterList = this.costCenterRepository.GetAll().ToList();
                IList<CostCenterDto> costCenterDtoList = new List<CostCenterDto>();
                costCenterDtoList = Mapper.Map<IList<CostCenter>, IList<CostCenterDto>>(costCenterList);
                
                return costCenterDtoList;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Finds the cost centery by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Cost CenterDto</returns>
        /// <exception cref="System.NotImplementedException">Error to Find</exception>
        public CostCenterDto FindCostCenteryById(string id)
        {
            try
            {
                CostCenter costCenter = this.costCenterRepository.FindById(id);
                CostCenterDto costCenterDto = new CostCenterDto();

                costCenterDto.CCNumber = costCenter.CCNumber;
                costCenterDto.CCName = costCenter.CCName;
                costCenterDto.AirlineCode = costCenter.AirlineCode;

                return costCenterDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Adds the cost center.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if added else false</returns>
        /// <exception cref="BusinessException">Error to Add
        /// </exception>
        public bool AddCostCenter(CostCenterDto entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (this.IsCostCenterNumberDuplicate(entity.CCNumber))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }

            try
            {
                CostCenter costCenter = new CostCenter();
                entity.Status = true;
                costCenter = Mapper.Map<CostCenter>(entity);
                this.costCenterRepository.Add(costCenter);
                this.unitOfWork.Commit();

                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Deletes the cost center.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if deleted else false</returns>
        /// <exception cref="BusinessException">Error to Delete</exception>
        public bool DeleteCostCenter(CostCenterDto entity)
        {
            try
            {
                CostCenter costCenter = this.costCenterRepository.FindById(entity.CCNumber);

                if (costCenter != null)
                {
                    /*
                    * Si se quiere hacer un borrado permanente de la DB
                    * se debe de usar la siguiente instrucción:
                    * this.costCenterRepository.Delete(costCenter);
                    */

                    // Para borrado lógico
                    costCenter.Status = false;
                    this.costCenterRepository.Update(costCenter);
                    this.unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the cost center.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>True if updated else false</returns>
        /// <exception cref="BusinessException">Error to Update</exception>
        public bool UpdateCostCenter(CostCenterDto entity)
        {
            try
            {
                CostCenter costCenter = this.costCenterRepository.FindById(entity.CCNumber);
                costCenter.CCNumber = entity.CCNumber;
                costCenter.CCName = entity.CCName;
                costCenter.AirlineCode = entity.AirlineCode;
                this.costCenterRepository.Update(costCenter);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the actives cost center.
        /// </summary>
        /// <returns>IList CostCenterDto</returns>
        public IList<CostCenterDto> GetActivesCostCenter()
        {
            try
            {
                Trace.TraceInformation("Ida al GetActiveCostCenter");
                IList<CostCenter> costCenterEntity = this.costCenterRepository.GetActiveCostCenter().ToList();
                Trace.TraceInformation("Regreso del  GetActiveCostCenter");
                IList<CostCenterDto> costCenterDto = new List<CostCenterDto>();
                Trace.TraceInformation(" Ida al Mapeo de GetActiveCostCenter");
                costCenterDto = Mapper.Map<IList<CostCenter>, IList<CostCenterDto>>(costCenterEntity);
                Trace.TraceInformation(" Regreso del Mapeo de GetActiveCostCenter");
 
                return costCenterDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }           
        }

        /// <summary>
        /// Determines whether [is cost center number duplicate] [the specified ccnumber].
        /// </summary>
        /// <param name="ccnumber">The ccnumber.</param>
        /// <returns>true or false</returns>
        public bool IsCostCenterNumberDuplicate(string ccnumber)
        {
            CostCenter costCenter = this.costCenterRepository.FindById(ccnumber);
            return costCenter != null ? true : false;
        }

        #endregion
    }
}
