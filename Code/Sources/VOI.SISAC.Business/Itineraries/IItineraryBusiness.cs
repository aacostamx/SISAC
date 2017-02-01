//------------------------------------------------------------------------
// <copyright file="IItineraryBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Itineraries;

    /// <summary>
    /// Contract for itinerary operations
    /// </summary>
    public interface IItineraryBusiness
    {
        /// <summary>
        /// Gets all flights by Day
        /// </summary>
        /// <returns></returns>
        IList<ItineraryDto> GetAllFlightsByDay();

        /// <summary>
        /// convert the data to accept in entity ItineraryDto
        /// </summary>
        /// <param name="itineraryFDto">Itinerary Data Object</param>
        /// <param name="airlineCode">Airline Code</param>
        /// <returns>list of error </returns>
        IList<string> ValidateItinerayFile(IList<ItineraryFileDto> itineraryFDto, string airlineCode);

        /// <summary>
        /// Gets the details of one flight in base of the following parameters
        /// </summary>
        /// <param name="sequence">sequence of flight</param>
        /// <param name="carrierCode">Airline Code</param>
        /// <param name="flightNumber">Flight Number of the Airplane</param>
        /// <param name="itinerarykey"></param>
        /// <returns></returns>
        ItineraryDto FindFlightById(int sequence, string carrierCode, string flightNumber, string itinerarykey);

        /// <summary>
        /// Add a Flight Itinerary manually
        /// </summary>
        /// <param name="itineraryDto">itinerary data object</param>
        /// <returns>True or False></returns>
        bool AddFlightItinerary(ItineraryDto itineraryDto);

        /// <summary>
        /// Delete a Flight Itinerary
        /// </summary>
        /// <param name="itineraryDto">itinerary data object</param>
        /// <param name="remarks">Coments for the Delete</param>
        /// <returns>True or False</returns>
        bool DeleteFlightItinerary(ItineraryDto itineraryDto, string remarks);

        /// <summary>
        /// Update a Flight Itinerary
        /// </summary>
        /// <param name="itineraryDto">itinerary data object</param>
        /// <param name="remarks">Comments for the Updated</param>
        /// <returns>True or False</returns>
        bool UpdateFlightItinerary(ItineraryDto itineraryDto, string remarks);

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
        /// <returns>itineraries order by departureDate</returns>
        IList<ItineraryDto> paginationListbyDay(int pagesize, int pagenumber);

        /// <summary>
        /// Advance Search Itinerary
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IList<ItineraryDto> AdvanceSearchItinerary(ItinerarySearchDto search);

        /// <summary>
        /// Advances the search itinerary previous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        IList<ItineraryDto> AdvanceSearchItineraryPrevious(ItinerarySearchDto search);

        /// <summary>
        /// Count Advance Search Itinerary
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        int CountAdvanceSearchItinerary(ItinerarySearchDto search);

        /// <summary>
        /// Counts the advance search itinerary previous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        int CountAdvanceSearchItineraryPrevious(ItinerarySearchDto search);

        /// <summary>
        /// Add or Update Itinerary
        /// </summary>
        /// <param name="itineraries"></param>
        /// <returns></returns>
        bool AddOrUpdateItinerary(IList<ItineraryDto> itineraries);

        /// <summary>
        /// Gets the details of a flight with only the passenger information.
        /// </summary>
        /// <param name="sequence">The sequence of flight.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight Number of the airplane.</param>
        /// <param name="itineraryKey">The itinerary identifier.</param>
        /// <returns>
        /// The information of a flight with the passenger information object.
        /// </returns>
        ItineraryDto FindItineraryWithPassengerInformation(int sequence, string airlineCode, string flightNumber, string itineraryKey);
    }
}
