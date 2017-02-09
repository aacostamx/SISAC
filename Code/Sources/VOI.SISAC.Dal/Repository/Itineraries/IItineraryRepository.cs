//------------------------------------------------------------------------
// <copyright file="IItineraryRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Itinerary Interface
    /// </summary>
    public interface IItineraryRepository : IRepository<Itinerary>
    {
        /// <summary>
        /// Gets the details of a flight
        /// </summary>
        /// <param name="sequence">sequence of flight</param>
        /// <param name="airlineCode">Airline Code</param>
        /// <param name="flightNumber">Flight Number of the airplane</param>
        /// <param name="itineraryKey">itinerary identifier</param>
        /// <returns></returns>
        Itinerary FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Find By Params
        /// </summary>
        /// <param name="find"></param>
        /// <returns></returns>
        Itinerary FindByParams(Itinerary find);

        /// <summary>
        /// Get a List of flights
        /// </summary>
        /// <returns>a list of flights</returns>
        IList<Itinerary> GetAllFlightByDay(string datenow);

        /// <summary>
        /// GetAllFlightByKeyWithoutSequence
        /// </summary>
        /// <param name="airlineCode"></param>
        /// <param name="flightNumber"></param>
        /// <param name="itineraryKey"></param>
        /// <returns></returns>
        int GetMaxAllFlightByKeyWithoutSequence(string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Count All Fligths
        /// </summary>
        /// <returns></returns>
        int CountAll();

        /// <summary>
        /// Count All by Day
        /// </summary>
        /// <returns></returns>
        int CountAllDay();

        /// <summary>
        /// Get only page items
        /// </summary>
        /// <param name="pagesize">page size</param>
        /// <param name="pagenumber">page number</param>
        /// <returns></returns>
        IList<Itinerary> paginationListbyDay(int pagesize, int pagenumber);

        /// <summary>
        /// Advance Search Itinerary
        /// </summary>  
        /// <returns></returns>
        IList<Itinerary> AdvanceSearchItinerary();

        /// <summary>
        /// Add Range Itineary
        /// </summary>
        /// <param name="itineraries"></param>
        void AddRangeItinerary(IList<Itinerary> itineraries);

        /// <summary>
        /// Gets the details of a flight with the passenger information, manifests and general declarations.
        /// </summary>
        /// <param name="sequence">The sequence of flight.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight Number of the airplane.</param>
        /// <param name="itineraryKey">The itinerary identifier.</param>
        /// <returns>
        /// The information of a flight with the passenger information, manifests and general declarations.
        /// </returns>
        Itinerary GetItineraryWithDeclarationsAndPassengerInformation(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Determines whether the departure station is from Mexico.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        ///   <c>true</c> if the departure station is from Mexico otherwise <c>false</c>.
        /// </returns>
        bool IsDepartureStationFromMexico(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Determines whether the arrival station is from Mexico.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        ///   <c>true</c> if the arrival station is from Mexico otherwise <c>false</c>.
        /// </returns>
        bool IsArrivalStationFromMexico(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Gets the details of a flight with the departure manifest and its children and Jet fuel tickets.
        /// </summary>
        /// <param name="sequence">The sequence of flight.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight Number of the airplane.</param>
        /// <param name="itineraryKey">The itinerary identifier.</param>
        /// <returns>
        /// The information of a flight with the departure manifest and its children Jet fuel tickets.
        /// </returns>
        Itinerary GetItineraryInformationForMessageMVT(int sequence, string airlineCode, string flightNumber, string itineraryKey);
    }
}
