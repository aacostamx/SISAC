//------------------------------------------------------------------------
// <copyright file="GendecArrivalRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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

    public class GendecArrivalRepository : Repository<GendecArrival>, IGendecArrivalRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GendecArrivalRepository"/> class.
        /// </summary>
        /// <param name="factory">factory param</param>
        public GendecArrivalRepository(IDbFactory factory)
            : base(factory)
        { }

        public GendecArrival GetGendecArrival(int sequence, string airlinecode, string flightnumber, string itinerarykey)
        {
            GendecArrival gendecArrival = new GendecArrival();
            gendecArrival = this.DbContext.GendecArrivals.Where(c => c.Sequence == sequence
                                                           && c.AirlineCode == airlinecode
                                                           && c.FlightNumber == flightnumber
                                                           && c.Itinerarykey == itinerarykey)
                                                           .Include(c => c.Crews)
                                                           .Include(c => c.Itinerary).FirstOrDefault();
            return gendecArrival;
        }

        public bool AddGendecArrival(GendecArrival gendecArrival, IList<Crew> crews)
        {
            gendecArrival.Itinerary = null;
            this.DbContext.GendecArrivals.Attach(gendecArrival);
            foreach (Crew crew in crews)
            {
                Crew crewEntity = this.DbContext.Crews.FirstOrDefault(c => c.CrewID == crew.CrewID);
                if (crewEntity != null)
                {
                    gendecArrival.Crews.Add(crewEntity);
                }
            }
            this.DbContext.GendecArrivals.Add(gendecArrival);
            return true;
        }

        public bool UpdateGendecArrival(GendecArrival gendecArrival, IList<Crew> crews)
        {
            this.DbContext.GendecArrivals.Attach(gendecArrival);
            foreach (Crew crew in crews)
            {
                Crew crewEntity = this.DbContext.Crews.FirstOrDefault(c => c.CrewID == crew.CrewID);
                if (crewEntity != null)
                {
                    gendecArrival.Crews.Add(crewEntity);
                }
            }
            this.DbContext.GendecArrivals.Add(gendecArrival);
            return true;
        }
    }
}
