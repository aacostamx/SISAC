//----------------------------------------------------------------------------
// <copyright file="IJetFuelTicketBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace VOI.SISAC.Business.Airport
{
    using System;
    using System.Collections.Generic;
using VOI.SISAC.Business.Dto.Airports;
using VOI.SISAC.Business.Dto.Itineraries;

    /// <summary>
    /// Interface IJetFuelTicketBusiness
    /// </summary>
    public interface IJetFuelTicketBusiness
    {
        /// <summary>
        /// GetJetFuelTickets
        /// </summary>
        /// <param name="itineraryDto"></param>
        /// <param name="operationTypeName"></param>
        /// <returns></returns>
        IList<JetFuelTicketDto> GetJetFuelTickets(ItineraryDto itineraryDto, string operationTypeName);        

        /// <summary>
        /// FindJetFuelTicketById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        JetFuelTicketDto FindJetFuelTicketById(long id);
       
        /// <summary>
        /// AddJetFuelTicket
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool AddJetFuelTicket(JetFuelTicketDto entity);

        /// <summary>
        /// DeleteJetFuelTicket
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool DeleteJetFuelTicket(JetFuelTicketDto entity);

        /// <summary>
        /// UpdateJetFuelTicket
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateJetFuelTicket(JetFuelTicketDto entity);
    }
}
