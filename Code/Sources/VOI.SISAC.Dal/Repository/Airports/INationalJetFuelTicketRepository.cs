//------------------------------------------------------------------------
// <copyright file="INationalJetFuelTicketRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using Entities.Airport;
    using Entities.Itineraries;
    using Infrastructure;
    using System.Collections.Generic;

    /// <summary>
    /// INationalJetFuelTicketRepository Interface
    /// </summary>
    public interface INationalJetFuelTicketRepository : IRepository<NationalJetFuelTicket>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        NationalJetFuelTicket FindById(long id);

        /// <summary>
        /// Gets the national jet fuel tickets.
        /// </summary>
        /// <param name="ticket">The itinerary.</param>
        /// <returns></returns>
        List<NationalJetFuelTicket> GetNationalJetFuelTickets(NationalJetFuelTicket ticket);
    }
}
