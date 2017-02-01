//--------------------------------------------------------------------
// <copyright file="AirportServiceBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Airport;
    using AutoMapper;
    using Dal.Infrastructure;
    using Dal.Repository.Airports;
    using Dal.Repository.Finance;
    using Dto.Airports;
    using Entities.Airport;
    using ExceptionBusiness;
    using Resources;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// AirportServiceBusiness class
    /// </summary>
    public class AirportServiceBusiness : IAirportServiceBusiness
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The service repository
        /// </summary>
        private readonly IAirportServiceRepository serviceRepository;

        /// <summary>
        /// The contract repository
        /// </summary>
        private readonly IAirportServiceContractRepository contractRepository;

        /// <summary>
        /// The drinking water repository
        /// </summary>
        private readonly IDrinkingWaterRepository drinkingWaterRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirportServiceBusiness" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="serviceRepository">The service repository.</param>
        /// <param name="contractRepository">The contract repository.</param>
        /// <param name="drinkingWaterRepository">The drinking water repository.</param>
        public AirportServiceBusiness(
            IUnitOfWork unitOfWork,
            IAirportServiceRepository serviceRepository,
            IAirportServiceContractRepository contractRepository,
            IDrinkingWaterRepository drinkingWaterRepository)
        {
            this.unitOfWork = unitOfWork;
            this.serviceRepository = serviceRepository;
            this.contractRepository = contractRepository;
            this.drinkingWaterRepository = drinkingWaterRepository;
        }

        /// <summary>
        /// Add Range.
        /// </summary>
        /// <param name="services">List of Airport Service.</param>
        public void AddRange(IList<AirportServiceDto> services)
        {
            try
            {
                IList<AirportService> airportServices = new List<AirportService>();
                airportServices = Mapper.Map<IList<AirportService>>(services);
                this.serviceRepository.AddRange(airportServices);
                this.unitOfWork.Commit();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }
        
        /// <summary>
        /// Find By Id
        /// </summary>
        /// <param name="id">The ID</param>
        /// <returns>Airport Service Data Transfer Object.</returns>
        public AirportServiceDto FindById(long id)
        {
            AirportServiceDto service = new AirportServiceDto();
            try
            {
                AirportService entity = this.serviceRepository.FindById(id);
                service = Mapper.Map<AirportServiceDto>(entity);
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedFindRecord + Messages.SeeInnerException, exception);
            }

            return service;
        }

        /// <summary>
        /// Gets the services in contract by airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline.</param>
        /// <param name="airportCode">The airport.</param>
        /// <returns>List of Services in the contract for the Airport and Airline.</returns>
        public List<GenericCatalogDto> GetDepartureServicesInContractByAirportAirline(string airlineCode, string airportCode)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(airportCode))
            {
                return null;
            }

            List<GenericCatalogDto> services = new List<GenericCatalogDto>();
            try
            {
                List<Service> servicesInContract = this.serviceRepository.GetDepartureServicesInContractByAirportAirline(airlineCode, airportCode).ToList();
                foreach (Service service in servicesInContract)
                {
                    services.Add(new GenericCatalogDto
                    {
                        Id = service.ServiceCode,
                        Description = service.ServiceName + " - " + service.ServiceCode
                    });
                }

                return services.OrderBy(c => c.Description).ToList();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the services in contract by airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <returns>
        /// List of Services in the contract for the Airport and Airline.
        /// </returns>
        public List<GenericCatalogDto> GetArrivalServicesInContractByAirportAirline(string airlineCode, string airportCode)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(airportCode))
            {
                return null;
            }

            List<GenericCatalogDto> services = new List<GenericCatalogDto>();
            try
            {
                List<Service> servicesInContract = this.serviceRepository.GetArrivalServicesInContractByAirportAirline(airlineCode, airportCode).ToList();
                foreach (Service service in servicesInContract)
                {
                    services.Add(new GenericCatalogDto
                    {
                        Id = service.ServiceCode,
                        Description = service.ServiceName + " - " + service.ServiceCode
                    });
                }

                return services.OrderBy(c => c.Description).ToList();
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// GetCountDepartureServicesInContractByAirportAirline
        /// </summary>
        /// <param name="airlineCode"></param>
        /// <param name="airportCode"></param>
        /// <returns></returns>
        public int GetCountDepartureServicesInContractByAirportAirline(string airlineCode, string airportCode)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(airportCode))
            {
                return 0;
            }

            List<GenericCatalogDto> services = new List<GenericCatalogDto>();
            try
            {
                List<Service> servicesInContract = this.serviceRepository.GetDepartureServicesInContractByAirportAirline(airlineCode, airportCode).ToList();

                return servicesInContract.Count;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// GetCountArrivalServicesInContractByAirportAirline
        /// </summary>
        /// <param name="airlineCode"></param>
        /// <param name="airportCode"></param>
        /// <returns></returns>
        public int GetCountArrivalServicesInContractByAirportAirline(string airlineCode, string airportCode)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(airportCode))
            {
                return 0;
            }

            List<GenericCatalogDto> services = new List<GenericCatalogDto>();
            try
            {
                List<Service> servicesInContract = this.serviceRepository.GetArrivalServicesInContractByAirportAirline(airlineCode, airportCode).ToList();

                return servicesInContract.Count;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the providers in contract by service code for airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <returns>List of providers linked to the service.</returns>
        public IList<GenericCatalogDto> GetProvidersInContractByServiceCodeForAirportAirline(string airlineCode, string airportCode, string serviceCode)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(airportCode) || string.IsNullOrWhiteSpace(serviceCode))
            {
                return null;
            }

            List<GenericCatalogDto> providers = new List<GenericCatalogDto>();
            try
            {
                List<Provider> servicesInContract = this.serviceRepository.GetProvidersInContractByServiceCodeForAirportAirline(
                    airlineCode, 
                    airportCode,
                    serviceCode)
                    .ToList();


                foreach (Provider provider in servicesInContract)
                {
                    providers.Add(new GenericCatalogDto
                    {
                        Id = provider.ProviderNumber,
                        Description = provider.ProviderName
                    });
                }
                providers.OrderBy(c => c.Description);
                return providers;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets Departure Services by Itinerary.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// List of Departure Services by Itinerary.
        /// </returns>
        public IList<AirportServiceDto> GetDepartureServicesByItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(flightNumber) || string.IsNullOrWhiteSpace(itineraryKey))
            {
                return null;
            }

            List<AirportServiceDto> servicesDto = new List<AirportServiceDto>();
            try
            {
                List<AirportService> servicesEntity = this.serviceRepository.GetDepartureServicesByItinerary(
                    sequence,
                    airlineCode,
                    flightNumber,
                    itineraryKey)
                    .ToList();                

                if(servicesEntity != null)
                {
                    servicesDto = Mapper.Map<List<AirportService>, List<AirportServiceDto>>(servicesEntity);
                }

                return servicesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets Arrival Services by Itinerary.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// List of Arrival Services by Itinerary.
        /// </returns>
        public IList<AirportServiceDto> GetArrivalServiceByItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            if (string.IsNullOrWhiteSpace(airlineCode) || string.IsNullOrWhiteSpace(flightNumber) || string.IsNullOrWhiteSpace(itineraryKey))
            {
                return null;
            }

            List<AirportServiceDto> servicesDto = new List<AirportServiceDto>();
            try
            {
                List<AirportService> servicesEntity = this.serviceRepository.GetArrivalServicesByItinerary(
                    sequence,
                    airlineCode,
                    flightNumber,
                    itineraryKey)
                    .ToList();

                if (servicesEntity != null)
                {
                    servicesDto = Mapper.Map<List<AirportService>, List<AirportServiceDto>>(servicesEntity);
                }

                return servicesDto;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }

        }

        /// <summary>
        /// Deletes an Airport Service record.
        /// </summary>
        /// <param name="service">Airport Service Data transfer object.</param>
        /// <returns>
        ///   <c>true</c> if the delete is successful, <c>false</c> otherwise.
        /// </returns>
        public bool DeleteAirportService(AirportServiceDto service)
        {
            if (service == null)
            {
                return false;
            }

            try
            {
                AirportService serviceEntity = this.serviceRepository.FindById(service.AirportServiceID);
                this.serviceRepository.Delete(serviceEntity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Updates the airport service.
        /// </summary>
        /// <param name="service">The services.</param>
        /// <returns>
        ///   <c>true</c> if the update is successful, <c>false</c> otherwise.
        /// </returns>
        public bool UpdateAirportService(AirportServiceDto service)
        {
            if (service == null)
            {
                return false;
            }

            try
            {
                AirportService serviceEntity = this.serviceRepository.FindById(service.AirportServiceID);
                serviceEntity.ServiceCode = service.ServiceCode;
                serviceEntity.ProviderNumber = service.ProviderNumber;
                serviceEntity.ApronPosition = service.ApronPosition;                
                serviceEntity.QtyServices = service.QtyServices;
                serviceEntity.QtyHours = service.QtyHours;
                serviceEntity.StartDateService = service.StartDateService;
                serviceEntity.StartTimeService = service.StartTimeService;
                serviceEntity.EndDateService = service.EndDateService;
                serviceEntity.EndTimeService = service.EndTimeService;
                serviceEntity.DrinkingWaterID = service.DrinkingWaterID;
                serviceEntity.FuelBeforeLanding = service.FuelBeforeLanding;
                serviceEntity.FuelLoaded = service.FuelLoaded;
                serviceEntity.GpuCode = service.GpuCode;
                serviceEntity.GpuObservationCode = service.GpuObservationCode;
                serviceEntity.GpuEndMeter = service.GpuEndMeter;
                serviceEntity.GpuStartMeter = service.GpuStartMeter;
                serviceEntity.Remarks = service.Remarks;

                this.serviceRepository.Update(serviceEntity);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedDeleteRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the drinking water by equipment number.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns>List of drinking waters.</returns>
        public IList<GenericCatalogDto> GetDrinkingWaterByEquipmentNumber(string equipmentNumber)
        {
            if (string.IsNullOrWhiteSpace(equipmentNumber))
            {
                return null;
            }

            try
            {
                List<GenericCatalogDto> catalog = new List<GenericCatalogDto>();
                List<DrinkingWater> waters = new List<DrinkingWater>();
                waters = this.drinkingWaterRepository.GetDrinkingWatersByAirplaneId(equipmentNumber, true)
                    .OrderBy(c=> c.Value).ToList();
                foreach (DrinkingWater item  in waters)
                {
                    catalog.Add(new GenericCatalogDto
                    {
                        Id = item.DrinkingWaterId.ToString(),
                        Description = item.DrinkingWaterName
                    });
                }

                return catalog;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedRetrievedRecords + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Add Airport Services
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public bool AddService(AirportServiceDto service)
        {
            try
            {
                AirportService airportServices = new AirportService();
                airportServices = Mapper.Map<AirportService>(service);
                this.serviceRepository.Add(airportServices);
                this.unitOfWork.Commit();
                return true;
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }

        /// <summary>
        /// Gets the number of services.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="operationType">The operation type.</param>
        /// <returns>
        /// Service count.
        /// </returns>
        public int GetServicesCount(int sequence, string airlineCode, string flightNumber, string itineraryKey, string operationType)
        {
            try
            {
                return this.serviceRepository.GetServicesCount(sequence, airlineCode, flightNumber, itineraryKey, operationType);
                
            }
            catch (Exception exception)
            {
                throw new BusinessException(Messages.FailedInsertRecord + Messages.SeeInnerException, exception);
            }
        }
    }
}
