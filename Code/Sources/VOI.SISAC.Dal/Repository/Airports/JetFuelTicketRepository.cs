//------------------------------------------------------------------------
// <copyright file="JetFuelTicketRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------


namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// JetFuelTicketRepository
    /// </summary>
    public class JetFuelTicketRepository : Repository<JetFuelTicket>, IJetFuelTicketRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelTicketRepository"/> class. 
        /// </summary>
        /// <param name="factory">factory factory</param>
        public JetFuelTicketRepository(IDbFactory factory)
            : base(factory)
        {
        }

        #region InternationalFuelContractRepository Members

        /// <summary>
        /// FindById
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>A JetFuelTicket</returns>
        public JetFuelTicket FindById(long id)
        {
            var jetFuelTicket = this.DbContext.JetFuelTickets
                .Where(c => c.JetFuelTicketID == id)
                .Include(c => c.Service)
                .Include(c => c.JetFuelProvider)
                .Include(c => c.IntoPlaneProvider)
                .Include(c => c.User)
                .FirstOrDefault();
            return jetFuelTicket;
        }

        /// <summary>
        /// GetJetFuelTickets
        /// </summary>
        /// <param name="itinerary">itinerary</param>
        /// <param name="operationTypeName">operationTypeName</param>
        /// <returns>List of JetFuelTicket</returns>
        public IList<JetFuelTicket> GetJetFuelTickets(Itinerary itinerary,string operationTypeName)
        {             
            return this.DbContext.JetFuelTickets
                .Where(c => c.Sequence == itinerary.Sequence
                && c.AirlineCode == itinerary.AirlineCode
                && c.FlightNumber == itinerary.FlightNumber
                && c.ItineraryKey == itinerary.ItineraryKey
                && c.OperationTypeName == operationTypeName)
                .Include(c => c.Service)
                .Include(c => c.JetFuelProvider)
                .Include(c => c.IntoPlaneProvider)
                .Include(c => c.User)
                .ToList();
        }
        #endregion
    }
}
