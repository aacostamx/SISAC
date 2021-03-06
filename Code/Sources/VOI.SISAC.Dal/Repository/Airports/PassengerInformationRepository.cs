﻿//------------------------------------------------------------------------
// <copyright file="PassengerInformationRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Passenger Information Repository
    /// </summary>
    public class PassengerInformationRepository : Repository<PassengerInformation>, IPassengerInformationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PassengerInformationRepository"/> class.
        /// </summary>
        /// <param name="factory">factory parameter</param>
        public PassengerInformationRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Gets the passenger information of a flight id
        /// </summary>
        /// <param name="sequence">flight sequence</param>
        /// <param name="airlinecode">airline code</param>
        /// <param name="flightnumber">flight number</param>
        /// <param name="itinerarykey">flight date</param>
        /// <returns></returns>
        public PassengerInformation FindById(int sequence, string airlinecode, string flightnumber, string itinerarykey)
        {
            var passengerInformation = this.DbContext.PassengerInformation
                .Include(c => c.Itinerary)
                .Include(c => c.AdditionalPassengerInformation)
                .Where(c => c.Sequence == sequence
                    && c.AirlineCode == airlinecode
                    && c.FlightNumber == flightnumber
                    && c.ItineraryKey == itinerarykey).FirstOrDefault();

            return passengerInformation;
        }
    }
}
