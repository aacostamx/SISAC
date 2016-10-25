//------------------------------------------------------------------------
// <copyright file="ItineraryRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Itinerary Repository
    /// </summary>
    public class ItineraryRepository : Repository<Itinerary>, IItineraryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItineraryRepository"/> class.
        /// </summary>
        /// <param name="factory">factory param</param>
        public ItineraryRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Add Range Itinerary
        /// </summary>
        /// <param name="itineraries"></param>
        public void AddRangeItinerary(IList<Itinerary> itineraries)
        {
            try
            {
                this.DbContext.Itineraries.AddRange(itineraries);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Advance Search Itinerary
        /// </summary>
        /// <returns></returns>
        public IList<Itinerary> AdvanceSearchItinerary()
        {
            var itineraries = new List<Itinerary>();
            try
            {
                //En caso que el algoritmo no funcione será necesario crear un SP
                //    OFFSET 10 ROWS FETCH NEXT 5 ROWS ONLY
                itineraries = (from items in DbContext.Itineraries
                               select items).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return itineraries;
        }

        /// <summary>
        /// Count All Fligths
        /// </summary>
        /// <returns></returns>
        public int CountAll()
        {
            int total = 0;
            try
            {
                total = this.DbContext.Itineraries.Count();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return total;
        }

        /// <summary>
        /// Count All Fligths
        /// </summary>
        /// <returns></returns>
        public int CountAllDay()
        {
            int total = 0;
            try
            {
                total = this.DbContext.Itineraries
                .Where(c => DbFunctions.TruncateTime(c.DepartureDate) ==
                    DbFunctions.TruncateTime(DateTime.Now)).Count();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return total;
        }

        /// <summary>
        /// Gets a Flight in base of three identifiers
        /// </summary>
        /// <param name="sequence">Sequence of flight</param>
        /// <param name="airlineCode">Airline Code</param>
        /// <param name="flightNumber">Flight Number of the airplane</param>
        /// <param name="itineraryKey">Itinerary identifier</param>
        /// <returns></returns>
        public Itinerary FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            Itinerary itinerary = this.DbContext.Itineraries.Where(c => c.Sequence == sequence
                                                                && c.AirlineCode == airlineCode
                                                                && c.FlightNumber == flightNumber
                                                                && c.ItineraryKey == itineraryKey)
                                          .Include(p => p.Airplane)
                                          .Include(p => p.GendecDepartures)
                                          .Include(p => p.GendecDepartures.Crews)
                                          .Include(p => p.GendecArrivals)
                                          .Include(p => p.GendecArrivals.Crews)
                                          .Include(p => p.PassengerInformation)
                                          .FirstOrDefault();

            //saber si es el ultimo sequence
            bool editableArrival = false;
            if (itinerary != null)
            {
                editableArrival = GetMaxAllFlightByKeyWithoutSequence(airlineCode, flightNumber, itineraryKey) == itinerary.Sequence ? true : false;

                itinerary.EditArrival = editableArrival;
                //Se apaga la funcionalidad de editar itinerario
                itinerary.EditArrival = false;
            }

            return itinerary;
        }

        /// <summary>
        /// Find By Params (AirlineCode, FlightNumber, ItineraryKey, DepartureStation)
        /// </summary>
        /// <param name="find"></param>
        /// <returns></returns>
        public Itinerary FindByParams(Itinerary find)
        {
            var itinerary = new Itinerary();
            try
            {
                itinerary = this.DbContext.Itineraries.AsNoTracking().Where(
                    c => c.AirlineCode == find.AirlineCode
                    && c.FlightNumber == find.FlightNumber
                    && c.ItineraryKey == find.ItineraryKey
                    && c.DepartureStation == find.DepartureStation)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return itinerary;
        }

        /// <summary>
        /// Gets all the flight by the actual day.
        /// </summary>
        /// <returns></returns>
        public IList<Itinerary> GetAllFlightByDay(string datenow)
        {
            var allFligths = new List<Itinerary>();
            try
            {
                allFligths = this.DbContext.Itineraries.Where(c => c.ItineraryKey == datenow).ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return allFligths;
        }

        /// <summary>
        /// GetAllFlightByKeyWithoutSequence
        /// </summary>
        /// <param name="airlineCode"></param>
        /// <param name="flightNumber"></param>
        /// <param name="itineraryKey"></param>
        /// <returns></returns>
        public int GetMaxAllFlightByKeyWithoutSequence(string airlineCode, string flightNumber, string itineraryKey)
        {
            List<Itinerary> itineraryList = this.DbContext.Itineraries.Where(c => c.AirlineCode == airlineCode
                                                      && c.FlightNumber == flightNumber
                                                      && c.ItineraryKey == itineraryKey).ToList();

            if (itineraryList.Count == 0)
                return 0;
            else
                return itineraryList.Max(c => c.Sequence);
        }

        /// <summary>
        /// Get only page items
        /// </summary>
        /// <param name="pagesize">page size</param>
        /// <param name="pagenumber">page number</param>
        /// <returns>itineraries order by departureDate</returns>
        public IList<Itinerary> paginationListbyDay(int pagesize, int pagenumber)
        {
            int skip = (pagenumber - 1) * pagesize;
            IList<Itinerary> itineraries = new List<Itinerary>();

            try
            {
                itineraries = this.DbContext.Itineraries.Where
                        (c => DbFunctions.TruncateTime(c.DepartureDate) == DbFunctions.TruncateTime(DateTime.Now))
                        .OrderBy(c => c.AirlineCode)
                        .ThenBy(c => c.FlightNumber)
                        .ThenBy(c => c.ItineraryKey)
                        .ThenBy(c => c.Sequence)
                        .ThenByDescending(c => c.DepartureDate)
                        .Skip(skip).Take(pagesize)
                        .ToList();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return itineraries;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public override void Update(Itinerary entity)
        {
            try
            {
                base.Update(entity);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
        }


        /// <summary>
        /// Gets the details of a flight with only the passenger information.
        /// </summary>
        /// <param name="sequence">The sequence of flight.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight Number of the airplane.</param>
        /// <param name="itineraryKey">The itinerary identifier.</param>
        /// <returns>
        /// The information of a flight with the passenger information object.
        /// </returns>
        public Itinerary GetItineraryWithManifestsInformation(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            Itinerary itinerary = this.DbContext.Itineraries
                .Include(p => p.PassengerInformation)
                .Include(p => p.ManifestDeparture)
                .Include(p => p.ManifestArrival)
                .FirstOrDefault(c => c.Sequence == sequence
                    && c.AirlineCode == airlineCode
                    && c.FlightNumber == flightNumber
                    && c.ItineraryKey == itineraryKey);

            return itinerary;
        }
    }
}
