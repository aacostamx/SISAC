//------------------------------------------------------------------------
// <copyright file="INationalJetFuelTicketBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using Dto.Airports;
    using Dto.Itineraries;
    using System.Collections.Generic;

    /// <summary>
    /// Interface
    /// </summary>
    public interface INationalJetFuelTicketBusiness
    {
        /// <summary>
        /// Gets the national jet fuel tickets.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        List<NationalJetFuelTicketDto> GetNationalJetFuelTickets(NationalJetFuelTicketDto ticket);

        /// <summary>
        /// Finds the national jet fuel ticket.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        NationalJetFuelTicketDto FindNationalJetFuelTicket(long id);

        /// <summary>
        /// Adds the national jet fuel ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        bool AddNationalJetFuelTicket(NationalJetFuelTicketDto ticket);

        /// <summary>
        /// Deletes the national jet fuel ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        bool DeleteNationalJetFuelTicket(NationalJetFuelTicketDto ticket);

        /// <summary>
        /// Updates the national jet fuel ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <returns></returns>
        bool UpdateNationalJetFuelTicket(NationalJetFuelTicketDto ticket);
    }
}
