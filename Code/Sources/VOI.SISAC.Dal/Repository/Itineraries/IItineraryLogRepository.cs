//------------------------------------------------------------------------
// <copyright file="IItineraryLogRepository.cs" company="Volaris">
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
    public interface IItineraryLogRepository : IRepository<ItineraryLog>
    {
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="airlineCode"></param>
        /// <param name="flightNumber"></param>
        /// <param name="itineraryKey"></param>
        /// <returns></returns>
        ItineraryLog FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey);
    }
}
