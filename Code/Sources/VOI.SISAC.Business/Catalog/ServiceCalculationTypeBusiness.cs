//------------------------------------------------------------------------
// <copyright file="ServiceCalculationTypeBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    /// Class Service Calculation Type Business
    /// </summary>
    public class ServiceCalculationTypeBusiness : IServiceCalculationTypeBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The country repository
        /// </summary>
        private readonly IServiceCalculationTypeRepository serviceCalculationTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCalculationTypeBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="serviceCalculationTypeRepository">The service calculation type repository.</param>
        public ServiceCalculationTypeBusiness(IUnitOfWork unitOfWork, IServiceCalculationTypeRepository serviceCalculationTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.serviceCalculationTypeRepository = serviceCalculationTypeRepository;
        }

        #region IServiceCalculationTypeBusiness Members

        /// <summary>
        /// Gets the type of all service calculation.
        /// </summary>
        /// <returns>
        /// IList ServiceCalculationTypeDto
        /// </returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        public IList<Dto.Catalogs.ServiceCalculationTypeDto> GetAllServiceCalculationType()
        {
            try
            {
                IList<ServiceCalculationType> serviceCalculationTypes = this.serviceCalculationTypeRepository.GetAll().ToList();
                IList<ServiceCalculationTypeDto> serviceCalculationTypesDto = new List<ServiceCalculationTypeDto>();

                serviceCalculationTypesDto = Mapper.Map<IList<ServiceCalculationType>, IList<ServiceCalculationTypeDto>>(serviceCalculationTypes);
                return serviceCalculationTypesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        #endregion
    }
}
