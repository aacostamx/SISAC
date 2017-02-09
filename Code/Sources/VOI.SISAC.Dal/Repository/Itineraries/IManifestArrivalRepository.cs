//------------------------------------------------------------------------
// <copyright file="IManifestArrivalRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// Interface for manifest arrival
    /// </summary>
    public interface IManifestArrivalRepository : IRepository<ManifestArrival>
    {
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
        ManifestArrival GetManifestArrivalForItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey);

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
        IList<Delay> GetDelaysForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Adds the specified manifest arrival.
        /// </summary>
        /// <param name="manifestArrival">The manifest arrival.</param>
        /// <param name="delays">The delays.</param>
        void Add(ManifestArrival manifestArrival, IList<Delay> delays);

        /// <summary>
        /// Updates the specified manifest arrival.
        /// </summary>
        /// <param name="manifestArrival">The manifest arrival.</param>
        /// <param name="delays">The delays.</param>
        void Update(ManifestArrival manifestArrival, IList<Delay> delays);

        /// <summary>
        /// Removes all delays.
        /// </summary>
        /// <param name="manifestArrival">The manifest arrival.</param>
        void RemoveAllDelaysFromManifest(ManifestArrival manifestArrival);
    }
}
