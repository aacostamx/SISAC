//------------------------------------------------------------------------
// <copyright file="ItineraryDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Itineraries
{
    using System;
    using System.Collections.Generic;
    using Airports;

    /// <summary>
    /// Data Object of Itinerary
    /// </summary>
    public class ItineraryDto
    {
        /// <summary>
        /// ItineraryDto Contructor
        /// </summary>
        public ItineraryDto()
        {
            DepartureTime = new TimeSpan(DepartureDate.Hour, DepartureDate.Minute,00);
            ArriveTime = new TimeSpan(ArrivalDate.Hour, ArrivalDate.Minute, 00);
        }

        /// <summary>
        /// Data of the flight sequence
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// CarrierCode = Airline Code
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Flight Number of the Airplane
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Primary Key = Departure Date + CarrierCode + Flight Number + DepartureStation + DepartureArrival
        /// </summary>
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets Equipment Number
        /// </summary>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets Departure Date
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Gets or sets Departure Airport
        /// </summary>
        public string DepartureStation { get; set; }

        /// <summary>
        /// Gets or sets Arrival Date
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Gets or sets Arrival Airport
        /// </summary>
        public string ArrivalStation { get; set; }

        /// <summary>
        /// Gets or sets DepartureTime
        /// </summary>
        public TimeSpan DepartureTime { get; set; }

        /// <summary>
        /// Gets or sets ArriveTime
        /// </summary>
        public TimeSpan ArriveTime { get; set; }

        /// <summary>
        /// EditArrival 
        /// </summary>
        public bool? EditArrival { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the gendec departure is close.
        /// </summary>
        /// <value>
        /// <c>true</c> if the gendec departure is close; otherwise, <c>false</c>.
        /// </value>
        public bool GendecDepartureIsClose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the gendec arrival is close.
        /// </summary>
        /// <value>
        /// <c>true</c> if the gendec arrival is close; otherwise, <c>false</c>.
        /// </value>
        public bool GendecArrivalIsClose { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation type.
        /// </summary>
        /// <value>
        /// The name of the operation type.
        /// </value>
        public string OperationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a list of Logs in itinerary
        /// </summary>
        public ICollection<ItineraryLogDto> itinerariesLogs { get; set; }

        /// <summary>
        /// AirportServices
        /// </summary>
        public ICollection<AirportServiceDto> AirportServices { get; set; }

        /// <summary>
        /// Gets or sets the airplane.
        /// </summary>
        /// <value>
        /// The airplane.
        /// </value>
        public AirplaneDto Airplane { get; set; }

        /// <summary>
        /// Gets or sets the passenger information.
        /// </summary>
        /// <value>
        /// The passenger information.
        /// </value>
        public PassengerInformationDto PassengerInformation { get; set; }

        /// <summary>
        /// JetFuelTickets collection
        /// </summary>
        public ICollection<JetFuelTicketDto> JetFuelTickets { get; set; }

        /// <summary>
        /// Gendec Departure collection
        /// </summary>
        public GendecDepartureDto GendecDepartures { get; set; }

        /// <summary>
        /// Gendec Arrival collection
        /// </summary>
        public GendecArrivalDto GendecArrivals {get; set; }
    }
}
