//------------------------------------------------------------------------
// <copyright file="ItineraryLogRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Itinerary Repository
    /// </summary>
    public class ItineraryLogRepository : Repository<ItineraryLog>, IItineraryLogRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItineraryRepository"/> class.
        /// </summary>
        /// <param name="factory">factory param</param>
        public ItineraryLogRepository(IDbFactory factory)
            : base(factory)
        { }

        public ItineraryLog FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            var itineraryLog = this.DbContext.ItineraryLogs.Where(c => c.Sequence == sequence
                                                                && c.AirlineCode == airlineCode
                                                                && c.FlightNumber == flightNumber
                                                                && c.ItineraryKey == itineraryKey).FirstOrDefault();
            return itineraryLog;
        }
        
    }
}