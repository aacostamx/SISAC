//------------------------------------------------------------------------
// <copyright file="GendecDepartureRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;
    using System.Data.Entity;

    /// <summary>
    /// Gendec Departure Repository
    /// </summary>
    public class GendecDepartureRepository : Repository<GendecDeparture>, IGendecDepartureRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GendecDepartureRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public GendecDepartureRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Get the Active Gendec for the Flight
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// GendecDeparture Entity
        /// </returns>
        public GendecDeparture GetGendecDeparture(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            GendecDeparture gendecDeparture = new GendecDeparture();
            gendecDeparture = this.DbContext.GendecDepartures.Where(c => c.Sequence == sequence
                                                           && c.AirlineCode == airlineCode
                                                           && c.FlightNumber == flightNumber
                                                           && c.Itinerarykey == itineraryKey)
                                                           .Include(x => x.Crews)
                                                           .Include(x => x.Itinerary)
                                                           .FirstOrDefault();

            return gendecDeparture;
        }

        /// <summary>
        /// Add Gendec with Crew
        /// </summary>
        /// <param name="gendecDeparture">Departure Document.</param>
        /// <param name="crews">The crews.</param>
        /// <returns>
        ///   <c>true</c> if was edited <c>false</c> otherwise.
        /// </returns>
        public bool AddGendec(GendecDeparture gendecDeparture, IList<Crew> crews)
        {
            gendecDeparture.Itinerary = null;
            this.DbContext.GendecDepartures.Attach(gendecDeparture);

            foreach (Crew crew in crews)
            {
                Crew crewEntity = this.DbContext.Crews.FirstOrDefault(c => c.CrewID == crew.CrewID);
                if (crewEntity != null)
                {
                    gendecDeparture.Crews.Add(crewEntity);
                }
            }

            this.DbContext.GendecDepartures.Add(gendecDeparture);
            return true;
        }
    }
}
