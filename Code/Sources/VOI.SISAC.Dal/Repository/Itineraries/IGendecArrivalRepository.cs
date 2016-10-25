//------------------------------------------------------------------------
// <copyright file="IGendecArrivalRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Gendec Arrival Repository Interface
    /// </summary>
    public interface IGendecArrivalRepository : IRepository<GendecArrival>
    {
        /// <summary>
        /// Get the Active Gendec for the Flight 
        /// </summary>
        /// <returns></returns>
        GendecArrival GetGendecArrival(int sequence, string airlinecode, string flightnumber, string itinerarykey);

        bool AddGendecArrival(GendecArrival gendecArrival, IList<Crew> crews);

        bool UpdateGendecArrival(GendecArrival gendecArrival, IList<Crew> crews);
    }
}
