//------------------------------------------------------------------------
// <copyright file="IManifestDepartureRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    /// Interface for manifest departure
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.IRepository{VOI.SISAC.Entities.Itineraries.ManifestDeparture}" />
    public interface IManifestDepartureRepository : IRepository<ManifestDeparture>
    {
        /// <summary>
        /// Gets the manifest departure for itinerary.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>The manifest departure for the flight.</returns>
        ManifestDeparture GetManifestDepartureForItinerary(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Gets the delays for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>List of delays in the manifest departure.</returns>
        IList<Delay> GetDelaysForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Adds the specified manifest departure.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <param name="delays">The delays.</param>
        void Add(ManifestDeparture manifestDeparture, IList<Delay> delays);

        /// <summary>
        /// Updates the specified manifest departure.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <param name="delays">The delays.</param>
        void Update(ManifestDeparture manifestDeparture, IList<Delay> delays);

        /// <summary>
        /// Removes all delays.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        void RemoveAllDelaysFromManifest(ManifestDeparture manifestDeparture);
    }
}
