//------------------------------------------------------------------------
// <copyright file="AirportServiceRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Airport;
    using Infrastructure;
    using VOI.SISAC.Entities.Finance;
    using System.Data.Entity;
    /// <summary>
    /// Airport Service Repository.
    /// </summary>
    public class AirportServiceRepository : Repository<AirportService>, IAirportServiceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirportServiceRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AirportServiceRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Add Range.
        /// </summary>
        /// <param name="services">List of Airport Service.</param>
        public void AddRange(IList<AirportService> services)
        {
            this.DbContext.AirportServices.AddRange(services);
        }

        /// <summary>
        /// FindById AirportServiceID.
        /// </summary>
        /// <param name="id">Airport Service ID.</param>
        /// <returns>
        /// Airport Service.
        /// </returns>
        public AirportService FindById(long id)
        {
            AirportService service = this.DbContext.AirportServices
                .Where(c => c.AirportServiceID == id)
                .Include(c => c.Itinerary)
                .FirstOrDefault();
            return service;
        }

        /// <summary>
        /// Gets the service in the contracts that applies for the Airline and Airport.
        /// </summary>
        /// <param name="airlineCode">Airline Code.</param>
        /// <param name="airportCode">Airport Code.</param>
        /// <returns>
        /// Services in a contract by Airport and Airline.
        /// </returns>
        public IList<Service> GetDepartureServicesInContractByAirportAirline(string airlineCode, string airportCode)
        {
            List<string> serviceCodes = this.DbContext.AirportServiceContracts
                .Where(c =>
                    c.AirlineCode == airlineCode
                    && c.StationCode == airportCode
                    && c.Status
                    && (c.OperationTypeId == 1 || c.OperationTypeId == 3)
                    && c.ServiceRecordFlag)
                .Select(C => C.ServiceCode)
                .ToList();

            List<Service> servicesInContract = this.DbContext.Services
                .Where(c => serviceCodes.Contains(c.ServiceCode))
                .ToList();

            return servicesInContract;
        }

        /// <summary>
        /// Gets the providers in contract by service code for airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <returns>List of providers linked to the service.</returns>
        public IList<Provider> GetProvidersInContractByServiceCodeForAirportAirline(string airlineCode, string airportCode, string serviceCode)
        {
            List<string> providerNumbers = this.DbContext.AirportServiceContracts
                .Where(c => c.AirlineCode == airlineCode && c.StationCode == airportCode && c.Status && c.ServiceCode == serviceCode)
                .Select(C => C.ProviderNumber)
                .ToList();

            List<Provider> providersByServices = this.DbContext.Providers
                .Where(c => providerNumbers.Contains(c.ProviderNumber))
                .ToList();

            return providersByServices;
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
        public IList<AirportService> GetDepartureServicesByItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            return this.DbContext.AirportServices.Where(c =>
                c.Sequence == sequence
                && c.AirlineCode == airlineCode
                && c.FlightNumber == flightNumber
                && c.ItineraryKey == itineraryKey
                && c.OperationTypeName == "SALIDA")
                .Include(c => c.Service)
                .Include(c => c.Provider)
                .Include(c => c.Itinerary)
                .Include(c => c.GpuObservation)
                .Include(c => c.Gpu)
                .Include(c => c.DrinkingWater)
                .ToList();
        }

        /// <summary>
        /// Gets the service in the contracts that applies for the Airline and Airport
        /// </summary>
        /// <param name="airlineCode">Airline Code</param>
        /// <param name="airportCode">Airport Code</param>
        /// <returns>
        /// Services in a contract by Airport and Airline
        /// </returns>
        public IList<Service> GetArrivalServicesInContractByAirportAirline(string airlineCode, string airportCode)
        {
            List<string> serviceCodes = this.DbContext.AirportServiceContracts
                .Where(c =>
                    c.AirlineCode == airlineCode
                    && c.StationCode == airportCode
                    && c.Status
                    && (c.OperationTypeId == 2 || c.OperationTypeId == 3)
                    && c.ServiceRecordFlag)
                .Select(C => C.ServiceCode)
                .ToList();

            List<Service> servicesInContract = this.DbContext.Services
                .Where(c => serviceCodes.Contains(c.ServiceCode))
                .ToList();

            return servicesInContract;
        }

        /// <summary>
        /// Gets arrival services by Itinerary
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// List of arrival services by Itinerary
        /// </returns>
        public IList<AirportService> GetArrivalServicesByItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            return this.DbContext.AirportServices.Where(c =>
                c.Sequence == sequence
                && c.AirlineCode == airlineCode
                && c.FlightNumber == flightNumber
                && c.ItineraryKey == itineraryKey
                && c.OperationTypeName == "LLEGADA")
                .Include(c => c.Service)
                .Include(c => c.Provider)
                .Include(c => c.Itinerary)
                .Include(c => c.GpuObservation)
                .Include(c => c.Gpu)
                .Include(c => c.DrinkingWater)
                .ToList();
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
            return this.DbContext.AirportServices.Where(c =>
                c.Sequence == sequence
                && c.AirlineCode == airlineCode
                && c.FlightNumber == flightNumber
                && c.ItineraryKey == itineraryKey
                && c.OperationTypeName == operationType).Count();
        }
    }
}
