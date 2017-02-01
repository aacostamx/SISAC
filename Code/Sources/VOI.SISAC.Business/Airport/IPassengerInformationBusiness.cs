//------------------------------------------------------------------------
// <copyright file="IPassengerInformationBusiness.cs" company="Volaris">
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
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Passenger Information Business interface
    /// </summary>
    public interface IPassengerInformationBusiness
    {
        /// <summary>
        /// Get the passengers information of a flight.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="airlinecode"></param>
        /// <param name="flightnumber"></param>
        /// <param name="itineraykey"></param>
        /// <returns></returns>
        PassengerInformationDto FindById(int sequence, string airlinecode, string flightnumber, string itineraykey);

        /// <summary>
        /// Add a new Passenger Information
        /// </summary>
        /// <param name="passengerInformationDto"></param>
        /// <returns></returns>
        bool AddPassengerInformation(PassengerInformationDto passengerInformationDto);

        /// <summary>
        /// Update Passenger Information
        /// </summary>
        /// <param name="passengerInformationDto"></param>
        /// <returns></returns>
        bool UpdatePassengerInformation(PassengerInformationDto passengerInformationDto);

        /// <summary>
        /// Validates the passenger information.
        /// </summary>
        /// <param name="passengerInformationDto">The passenger information dto.</param>
        /// <param name="isMexicanAirport">if set to <c>true</c> set rules for national airports.</param>
        /// <returns>
        /// List of errors, if exist
        /// </returns>
        IList<string> ValidatePassengerInformation(PassengerInformationDto passengerInformationDto, bool isMexicanAirportDeparture, bool isInternationalAirportArrival);
    }
}
