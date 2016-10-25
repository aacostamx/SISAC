//------------------------------------------------------------------------
// <copyright file="OperationTypeBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Business.ExceptionBusiness;
    using VOI.SISAC.Business.Resources;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Catalog;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Class Operation Type Business
    /// </summary>
    public class OperationTypeBusiness : IOperationTypeBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The country repository
        /// </summary>
        private readonly IOperationTypeRepository operationTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationTypeBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="operationTypeRepository">The operation type repository.</param>
        public OperationTypeBusiness(IUnitOfWork unitOfWork, IOperationTypeRepository operationTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.operationTypeRepository = operationTypeRepository;
        }

        #region IOperationTypeBusiness Members

        /// <summary>
        /// Gets the type of all operation.
        /// </summary>
        /// <returns>
        /// IList OperationTypeDto
        /// </returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        public IList<Dto.Catalogs.OperationTypeDto> GetAllOperationType()
        {
            try
            {
                IList<OperationType> operationTypes = this.operationTypeRepository.GetAll().ToList();
                IList<OperationTypeDto> operationTypesDto = new List<OperationTypeDto>();

                operationTypesDto = Mapper.Map<IList<OperationType>, IList<OperationTypeDto>>(operationTypes);
                return operationTypesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        #endregion
    }
}
