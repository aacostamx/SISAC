//------------------------------------------------------------------------
// <copyright file="NationalJetFuelTicketRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using Entities.Airport;
    using Entities.Itineraries;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;


    /// <summary>
    /// NationalJetFuelTicketRepository Class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Repository.Airports.INationalJetFuelTicketRepository" />
    public class NationalJetFuelTicketRepository : Repository<NationalJetFuelTicket>, INationalJetFuelTicketRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelTicketRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalJetFuelTicketRepository(IDbFactory factory) : base(factory) { }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public NationalJetFuelTicket FindById(long id)
        {
            var nationalTicket = new NationalJetFuelTicket();

            try
            {
                nationalTicket = this.DbContext.NationalJetFuelTicket
                    .Where(c => c.NationalJetFuelTicketID == id)
                    .Include(c => c.Service)
                    .Include(c => c.JetFuelProvider)
                    .Include(c => c.IntoPlaneProvider)
                    .Include(c => c.User)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return nationalTicket;
        }

        /// <summary>
        /// Gets the national jet fuel tickets.
        /// </summary>
        /// <param name="ticket">The itinerary.</param>
        /// <returns></returns>
        public List<NationalJetFuelTicket> GetNationalJetFuelTickets(NationalJetFuelTicket ticket)
        {
            var nationalTickets = new List<NationalJetFuelTicket>();

            try
            {
                nationalTickets = this.DbContext.NationalJetFuelTicket
                    .Where(c => c.Sequence == ticket.Sequence &&
                    c.AirlineCode == ticket.AirlineCode &&
                    c.FlightNumber == ticket.FlightNumber &&
                    c.ItineraryKey == ticket.ItineraryKey &&
                    c.OperationTypeName == ticket.OperationTypeName)
                    .Include(c => c.Itinerary)
                    .Include(c => c.Service)
                    .Include(c => c.JetFuelProvider)
                    .Include(c => c.IntoPlaneProvider)
                    .Include(c => c.User)
                    .ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return nationalTickets;
        }
    }
}
