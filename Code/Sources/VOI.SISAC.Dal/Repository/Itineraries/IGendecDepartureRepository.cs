//------------------------------------------------------------------------
// <copyright file="IGendecDepartureRepository.cs" company="Volaris">
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
using VOI.SISAC.Entities.Airport;
using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Gendec Departure Interface
    /// </summary>
    public interface IGendecDepartureRepository : IRepository<GendecDeparture>
    {
        /// <summary>
        /// Get the Active Gendec for the Flight 
        /// </summary>
        /// <returns>GendecDeparture Entity</returns>
        GendecDeparture GetGendecDeparture(int sequence, string airlinecode, string flightnumber, string itinerarykey);

        /// <summary>
        /// Add Gendec with Crew
        /// </summary>
        /// <param name="gendecDeparture"></param>
        /// <param name="crews"></param>
        /// <returns><c>true</c> if was edited <c>false</c> otherwise.</returns>
        bool AddGendec(GendecDeparture gendecDeparture, IList<Crew> crews);        
    }
}
