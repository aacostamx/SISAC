//------------------------------------------------------------------------
// <copyright file="IPassengerInformationRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Passenger Information Interface
    /// </summary>
    public interface IPassengerInformationRepository : IRepository<PassengerInformation>
    {
        /// <summary>
        /// Get a passenger information by flight
        /// </summary>
        /// <param name="sequence">sequence of the flight</param>
        /// <param name="airlinecode">airline code</param>
        /// <param name="flightnumber">flight number</param>
        /// <param name="itinerarykey">flight date</param>
        /// <returns></returns>
        PassengerInformation FindById(int sequence, string airlinecode, string flightnumber, string itinerarykey);
    }
}
