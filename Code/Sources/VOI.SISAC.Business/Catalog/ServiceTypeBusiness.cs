//------------------------------------------------------------------------
// <copyright file="ServiceTypeBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

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
    /// Class Service Type Business 
    /// </summary>
    public class ServiceTypeBusiness : IServiceTypeBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The country repository
        /// </summary>
        private readonly IServiceTypeRepository serviceTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTypeBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="serviceTypeRepository">The service type repository.</param>
        public ServiceTypeBusiness(IUnitOfWork unitOfWork, IServiceTypeRepository serviceTypeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.serviceTypeRepository = serviceTypeRepository;
        }

        #region IServiceTypeBusiness Members

        /// <summary>
        /// Gets the type of all service.
        /// </summary>
        /// <returns>
        /// IList ServiceTypeDto
        /// </returns>
        /// <exception cref="BusinessException">Business Exception</exception>
        public IList<Dto.Catalogs.ServiceTypeDto> GetAllServiceType()
        {
            try
            {
                IList<ServiceType> servicesTypes = this.serviceTypeRepository.GetAll().ToList();
                IList<ServiceTypeDto> servicesTypesDto = new List<ServiceTypeDto>();

                servicesTypesDto = Mapper.Map<IList<ServiceType>, IList<ServiceTypeDto>>(servicesTypes);
                return servicesTypesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        #endregion
    }
}
