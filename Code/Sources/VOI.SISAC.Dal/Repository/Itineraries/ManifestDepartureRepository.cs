//------------------------------------------------------------------------
// <copyright file="ManifestDepartureRepository.cs" company="AACOSTA">
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
    /// Operations for manifest departure
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.Repository{VOI.SISAC.Entities.Itineraries.ManifestDeparture}" />
    /// <seealso cref="VOI.SISAC.Dal.Repository.Itineraries.IManifestDepartureRepository" />
    public class ManifestDepartureRepository : Repository<ManifestDeparture>, IManifestDepartureRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestDepartureRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ManifestDepartureRepository(IDbFactory factory)
            : base(factory)
        { 
        }

        /// <summary>
        /// Gets the manifest departure for itinerary.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// The manifest departure for the flight.
        /// </returns>
        public ManifestDeparture GetManifestDepartureForItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            return this.DbContext.ManifestsDeparture
                .Include(c => c.Itinerary)
                .Include(c => c.Itinerary.PassengerInformation)
                .Include(c => c.AdditionalDepartureInformation)
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
        /// List of delays in the manifest departure.
        /// </returns>
        public IList<Delay> GetDelaysForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            IList<Delay> delay = this.DbContext.ManifestsDeparture
                .Include(c => c.Delays)
                .FirstOrDefault(c => c.Sequence == sequence && c.AirlineCode == airlineCode && c.FlightNumber == flightNumber && c.ItineraryKey == itineraryKey)
                .Delays;

            return delay;
        }

        /// <summary>
        /// Gets the boardings for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns></returns>
        //public IList<ManifestDepartureBoarding> GetBoardingsForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        //{
        //    return this.DbContext.ManifestDepartureBoardings
        //        .Include(c => c.ManifestDepartureBoardingInformations)
        //        .Include(c => c.ManifestDepartureBoardingDetails)
        //        .Where(c => c.Sequence == sequence && c.AirlineCode == airlineCode && c.FlightNumber == flightNumber && c.ItineraryKey == itineraryKey)
        //        .ToList();
        //}

        /// <summary>
        /// Adds the specified manifest departure.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <param name="delays">The delays.</param>
        public void Add(ManifestDeparture manifestDeparture, IList<Delay> delays)
        {
            manifestDeparture.Delays = new List<Delay>();
            foreach (Delay delay in delays)
            {
                Delay delayEntity = this.DbContext.Delay.FirstOrDefault(c => c.DelayCode == delay.DelayCode);
                if (delayEntity != null)
                {
                    manifestDeparture.Delays.Add(delayEntity);
                }
            }

            this.DbContext.ManifestsDeparture.Add(manifestDeparture);
        }

        /// <summary>
        /// Updates the specified manifest departure.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <param name="delays">The delays.</param>
        public void Update(ManifestDeparture manifestDeparture, IList<Delay> delays)
        {
            manifestDeparture.Delays = new List<Delay>();
            this.DbContext.ManifestsDeparture.Attach(manifestDeparture);
            foreach (Delay delay in delays)
            {
                Delay delayEntity = this.DbContext.Delay.FirstOrDefault(c => c.DelayCode == delay.DelayCode);
                if (delayEntity != null)
                {
                    manifestDeparture.Delays.Add(delayEntity);
                }
            }

            this.DbContext.Entry(manifestDeparture).State = EntityState.Modified;
        }        

        ///// <summary>
        ///// Removes all delays.
        ///// </summary>
        ///// <param name="manifestDeparture">The manifest departure.</param>
        public void RemoveAllDelaysFromManifest(ManifestDeparture manifestDeparture)
        {
            List<Delay> delaysInManifest = this.DbContext.ManifestsDeparture
                .Include(c => c.Delays)
                .FirstOrDefault(c => c.AirlineCode == manifestDeparture.AirlineCode
                    && c.Sequence == manifestDeparture.Sequence
                    && c.FlightNumber == manifestDeparture.FlightNumber
                    && c.ItineraryKey == manifestDeparture.ItineraryKey)
                .Delays
                .ToList();

            ManifestDeparture manifest = this.DbContext.ManifestsDeparture
                .Include(c => c.Delays)
                .FirstOrDefault(c => c.AirlineCode == manifestDeparture.AirlineCode
                    && c.Sequence == manifestDeparture.Sequence
                    && c.FlightNumber == manifestDeparture.FlightNumber
                    && c.ItineraryKey == manifestDeparture.ItineraryKey);

            foreach (Delay item in delaysInManifest)
            {
                Delay delay = this.DbContext.Delay.FirstOrDefault(c => c.DelayCode == item.DelayCode);
                manifest.Delays.Remove(item);
            }
        }        

        #region IManifestDepartureRepository Members



        #endregion
    }
}
