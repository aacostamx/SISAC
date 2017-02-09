//------------------------------------------------------------------------
// <copyright file="IAirportServiceBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System.Collections.Generic;
    using Dto.Airports;
    using VOI.SISAC.Business.Dto.Catalogs;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Interface Airport Service Business
    /// </summary>
    public interface IAirportServiceBusiness
    {
        /// <summary>
        /// Find By Id
        /// </summary>
        /// <param name="id">The ID</param>
        /// <returns>Airport Service Data Transfer Object.</returns>
        AirportServiceDto FindById(long id);

        /// <summary>
        /// Add Range of Airport Services
        /// </summary>
        /// <param name="services">List of Airport Service Data transfer object.</param>
        void AddRange(IList<AirportServiceDto> services);

        /// <summary>
        /// Add Airport Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        bool AddService(AirportServiceDto service);

        /// <summary>
        /// Gets the services in contract by airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <returns>
        /// List of Services in the contract for the Airport and Airline.
        /// </returns>
        List<GenericCatalogDto> GetDepartureServicesInContractByAirportAirline(string airlineCode, string airportCode);

        /// <summary>
        /// Gets the services in contract by airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <returns>
        /// List of Services in the contract for the Airport and Airline.
        /// </returns>
        List<GenericCatalogDto> GetArrivalServicesInContractByAirportAirline(string airlineCode, string airportCode);

        /// <summary>
        /// Gets the providers in contract by service code for airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <returns>List of providers linked to the service.</returns>
        IList<GenericCatalogDto> GetProvidersInContractByServiceCodeForAirportAirline(string airlineCode, string airportCode, string serviceCode);

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
        IList<AirportServiceDto> GetDepartureServicesByItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey);

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
        IList<AirportServiceDto> GetArrivalServiceByItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Deletes an Airport Service record.
        /// </summary>
        /// <param name="service">Airport Service Data transfer object.</param>
        /// <returns><c>true</c> if the update is successful <c>false</c> otherwise.</returns>
        bool DeleteAirportService(AirportServiceDto service);

        /// <summary>
        /// Updates an Airport Service Data transfer object.
        /// </summary>
        /// <param name="service">Airport Service Data transfer object.</param>
        /// <returns><c>true</c> if the update is successful <c>false</c> otherwise.</returns>
        bool UpdateAirportService(AirportServiceDto service);

        /// <summary>
        /// Gets the drinking water by equipment number.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns>List of drinking waters.</returns>
        IList<GenericCatalogDto> GetDrinkingWaterByEquipmentNumber(string equipmentNumber);

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
        int GetServicesCount(int sequence, string airlineCode, string flightNumber, string itineraryKey, string operationType);

        /// <summary>
        /// Gets the count departure services in contract by airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <returns></returns>
        int GetCountDepartureServicesInContractByAirportAirline(string airlineCode, string airportCode);

        /// <summary>
        /// Gets the count arrival services in contract by airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <returns></returns>
        int GetCountArrivalServicesInContractByAirportAirline(string airlineCode, string airportCode);
    }
}