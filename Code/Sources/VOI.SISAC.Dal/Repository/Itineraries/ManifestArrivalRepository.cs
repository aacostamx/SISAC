//------------------------------------------------------------------------
// <copyright file="ManifestArrivalRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Operations for manifest arrival
    /// </summary>
    public class ManifestArrivalRepository : Repository<ManifestArrival>, IManifestArrivalRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestArrivalRepository" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ManifestArrivalRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Gets the manifest arrival for itinerary.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// The manifest arrival for the flight.
        /// </returns>
        public ManifestArrival GetManifestArrivalForItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            return this.DbContext.ManifestsArrival
                .Include(c => c.Itinerary)
                .Include(c => c.Itinerary.PassengerInformation)
                .Include(c => c.Itinerary.ManifestDeparture)
                .Include(c => c.AdditionalArrivalInformation)
                .FirstOrDefault(c => c.Sequence == sequence && c.AirlineCode == airlineCode && c.FlightNumber == flightNumber && c.ItineraryKey == itineraryKey);
        }

        /// <summary>
        /// Gets the delays for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// List of delays in the manifest arrival.
        /// </returns>
        public IList<Delay> GetDelaysForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            IList<Delay> delay = this.DbContext.ManifestsArrival
                .Include(c => c.Delays)
                .FirstOrDefault(c => c.Sequence == sequence && c.AirlineCode == airlineCode && c.FlightNumber == flightNumber && c.ItineraryKey == itineraryKey)
                .Delays;

            return delay;
        }

        /// <summary>
        /// Adds the specified manifest arrival.
        /// </summary>
        /// <param name="manifestArrival">The manifest arrival.</param>
        /// <param name="delays">The delays.</param>
        public void Add(ManifestArrival manifestArrival, IList<Delay> delays)
        {
            manifestArrival.Delays = new List<Delay>();
            foreach (Delay delay in delays)
            {
                Delay delayEntity = this.DbContext.Delay.FirstOrDefault(c => c.DelayCode == delay.DelayCode);
                if (delayEntity != null)
                {
                    manifestArrival.Delays.Add(delayEntity);
                }
            }

            this.DbContext.ManifestsArrival.Add(manifestArrival);
        }

        /// <summary>
        /// Updates the specified manifest arrival.
        /// </summary>
        /// <param name="manifestArrival">The manifest arrival.</param>
        /// <param name="delays">The delays.</param>
        public void Update(ManifestArrival manifestArrival, IList<Delay> delays)
        {
            manifestArrival.Delays = new List<Delay>();
            this.DbContext.ManifestsArrival.Attach(manifestArrival);
            foreach (Delay delay in delays)
            {
                Delay delayEntity = this.DbContext.Delay.FirstOrDefault(c => c.DelayCode == delay.DelayCode);
                if (delayEntity != null)
                {
                    manifestArrival.Delays.Add(delayEntity);
                }
            }

            this.DbContext.Entry(manifestArrival).State = EntityState.Modified;
        }

        /// <summary>
        /// Removes all delays.
        /// </summary>
        /// <param name="manifestArrival">The manifest arrival.</param>
        public void RemoveAllDelaysFromManifest(ManifestArrival manifestArrival)
        {
            List<Delay> delaysInManifest = this.DbContext.ManifestsArrival
                .Include(c => c.Delays)
                .FirstOrDefault(c => c.AirlineCode == manifestArrival.AirlineCode
                    && c.Sequence == manifestArrival.Sequence
                    && c.FlightNumber == manifestArrival.FlightNumber
                    && c.ItineraryKey == manifestArrival.ItineraryKey)
                .Delays
                .ToList();

            ManifestArrival manifest = this.DbContext.ManifestsArrival
                .Include(c => c.Delays)
                .FirstOrDefault(c => c.AirlineCode == manifestArrival.AirlineCode
                    && c.Sequence == manifestArrival.Sequence
                    && c.FlightNumber == manifestArrival.FlightNumber
                    && c.ItineraryKey == manifestArrival.ItineraryKey);

            foreach (Delay item in delaysInManifest)
            {
                Delay delay = this.DbContext.Delay.FirstOrDefault(c => c.DelayCode == item.DelayCode);
                manifest.Delays.Remove(item);
            }
        }
    }
}
