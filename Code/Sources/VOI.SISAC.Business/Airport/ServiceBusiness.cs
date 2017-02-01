//------------------------------------------------------------------------
// <copyright file="ServiceBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Dal.Repository.Airports;
    using VOI.SISAC.Entities.Airport;
    using AutoMapper;
    using MapConfiguration;
    using ExceptionBusiness;
    using Resources;

    /// <summary>
    /// Services Business Logic
    /// </summary>
    public class ServiceBusiness : IServiceBusiness
    {
        /// <summary>
        /// Unit of Work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The provider repository
        /// </summary>
        private readonly IServiceRepository serviceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusiness"/> class.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="serviceRepository"></param>
        public ServiceBusiness(IUnitOfWork unitOfWork, IServiceRepository serviceRepository)
        {
            this.unitOfWork = unitOfWork;
            this.serviceRepository = serviceRepository;
        }

        /// <summary>
        /// Get all Services
        /// </summary>
        /// <returns></returns>
        public IList<ServiceDto> GetAllService()
        {
            try
            {
                IList<Service> serviceModel = this.serviceRepository.GetAll().ToList();
                IList<ServiceDto> serviceEntity = new List<ServiceDto>();
                serviceEntity = Mapper.Map<IList<Service>, IList<ServiceDto>>(serviceModel);
                return serviceEntity.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Get the Service by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceDto FindServiceById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            try
            {
                Service service = this.serviceRepository.FindById(id);
                ServiceDto ServiceEntity = new ServiceDto();
                ServiceEntity = Mapper.Map<Service,ServiceDto>(service);
                return ServiceEntity;
            }
            catch(Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Add new service
        /// </summary>
        /// <param name="serviceDto"></param>
        /// <returns></returns>
        public bool AddService(ServiceDto serviceDto)
        {
            if (serviceDto == null)
            {
                return false;
            }
            if (this.IsServiceCodeDuplicate(serviceDto.ServiceCode))
            {
                throw new BusinessException(Messages.FailedInsertRecord, Messages.DuplicatePrimaryKey, 10);
            }
            try
            {
                if (serviceDto != null)
                {
                    Service serviceModel = new Service();
                    serviceModel = Mapper.Map<ServiceDto, Service>(serviceDto);
                    serviceModel.Status = true;
                    this.serviceRepository.Add(serviceModel);
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
                throw new BusinessException(ex.Message.ToString());
            } 
        }

        /// <summary>
        /// Delete a service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public bool DeleteService(ServiceDto service)
        {
            if (service == null)
            {
                return false;
            }
            try
            {
                ServiceDto serviceDto = new ServiceDto();
                Service serviceModel = this.serviceRepository.FindById(service.ServiceCode);
                serviceModel.Status = false;
                this.serviceRepository.Update(serviceModel);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Update a Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public bool UpdateService(ServiceDto service)
        {
            try
                {
                    Service serviceModel = this.serviceRepository.FindById(service.ServiceCode);
                    serviceModel.ServiceCode = service.ServiceCode;
                    serviceModel.ServiceName = service.ServiceName;
                    serviceModel.Status = service.Status;
                    this.serviceRepository.Update(serviceModel);
                    this.unitOfWork.Commit();
                    return true;
                }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get the active Services
        /// </summary>
        /// <returns></returns>
        public IList<ServiceDto> GetActivesService()
        {
            try
            {
                IList<Service> serviceModel = this.serviceRepository.GetServices().ToList();
                IList<ServiceDto> serviceDto = new List<ServiceDto>();
                serviceDto = Mapper.Map<IList<Service>, IList<ServiceDto>>(serviceModel);
                return serviceDto.ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, ex);
            }
        }

        /// <summary>
        /// Determines whether Services Code duplicate is.
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <returns></returns>
        private bool IsServiceCodeDuplicate(string serviceCode)
        {
            Service service = this.serviceRepository.FindById(serviceCode);
            return service != null ? true : false;
        }
    }
}
