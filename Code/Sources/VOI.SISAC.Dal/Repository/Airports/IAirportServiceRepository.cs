//------------------------------------------------------------------------
// <copyright file="IAirportServiceRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------
namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using Entities.Airport;
    using Infrastructure;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Interface for Airport Service Repository
    /// </summary>
    public interface IAirportServiceRepository : IRepository<AirportService>
    {
        /// <summary>
        /// FindById AirportServiceID
        /// </summary>
        /// <param name="id">Airport Service ID</param>
        /// <returns>Airport Service</returns>
        AirportService FindById(long id);

        /// <summary>
        /// Add Range
        /// </summary>
        /// <param name="services">List of Airport Service</param>
        void AddRange(IList<AirportService> services);

        /// <summary>
        /// Gets the service in the contracts that applies for the Airline and Airport
        /// </summary>
        /// <param name="airlineCode">Airline Code</param>
        /// <param name="airportCode">Airport Code</param>
        /// <returns>Services in a contract by Airport and Airline</returns>
        IList<Service> GetDepartureServicesInContractByAirportAirline(string airlineCode, string airportCode);

        /// <summary>
        /// Gets the providers in contract by service code for airport airline.
        /// </summary>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="airportCode">The airport code.</param>
        /// <param name="serviceCode">The service code.</param>
        /// <returns>List of providers linked to the service.</returns>
        IList<Provider> GetProvidersInContractByServiceCodeForAirportAirline(string airlineCode, string airportCode, string serviceCode);

        /// <summary>
        /// Gets Departure Services by Itinerary
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// List of Departure Services by Itinerary
        /// </returns>
        IList<AirportService> GetDepartureServicesByItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Gets the service in the contracts that applies for the Airline and Airport
        /// </summary>
        /// <param name="airlineCode">Airline Code</param>
        /// <param name="airportCode">Airport Code</param>
        /// <returns>Services in a contract by Airport and Airline</returns>
        IList<Service> GetArrivalServicesInContractByAirportAirline(string airlineCode, string airportCode);

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
        IList<AirportService> GetArrivalServicesByItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey);

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
    }
}

//////           _      _
//////           (c\-.--/a)
//////            |q: p   /\_            _____
//////          __\(_/  ).'  '---._.---'`     '---.__
//////         /   (Y_)_/            /        : \-._ \
////// !!!!,,, \_))'-';             (       _/   \  '\\_
//////!!II!!!!!IIII,, \_             \     /      \_  '.\
////// !IIsndIIIII!!!!,,\     /_      \   |----.___ '-. \'.__
////// !!!IIIIIIIIIIIIIIII\   | '--._.-'  _)       \  |  `'--'
//////     '''!!!!IIIIIII/   .',, ((___.-'         / /
//////           '''!!!!/  _/!!!!IIIIIII!!!!!,,,,,;,;,,,.....
//////                 | /IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII
//////                 | \   ''IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII
//////                 \_,)     '''''!!!!IIIIIIIIIIIIIIII!!!!!!!!
//////                                   ''''''''''!!!!!!!!!!!!!!!