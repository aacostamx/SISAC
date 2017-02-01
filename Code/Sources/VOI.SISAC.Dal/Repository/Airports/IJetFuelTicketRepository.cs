//------------------------------------------------------------------------
// <copyright file="IJetFuelTicketRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;
    /// <summary>
    /// Interface JetFuelTicketRepository
    /// </summary>
    public interface IJetFuelTicketRepository : IRepository<JetFuelTicket>
    {
        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        JetFuelTicket FindById(long id);

        /// <summary>
        /// GetJetFuelTickets
        /// </summary>
        /// <param name="itinerary">itinerary</param>
        /// <param name="operationTypeName">operationTypeName</param>
        /// <returns>List of JetFuelTicket</returns>
        IList<JetFuelTicket> GetJetFuelTickets(Itinerary itinerary, string operationTypeName);
    }
}
