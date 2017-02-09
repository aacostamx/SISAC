//------------------------------------------------------------------------
// <copyright file="IManifestArrivalBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Airports;
    using VOI.SISAC.Business.Dto.Itineraries;

    /// <summary>
    /// Interface for Manifest departure logic
    /// </summary>
    public interface IManifestArrivalBusiness
    {
        /// <summary>
        /// Gets the arrival manifest for flight.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>The arrival manifest for the flight.</returns>
        ManifestArrivalDto GetManifestArrivalForFlight(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Saves the arrival manifest.
        /// </summary>
        /// <param name="manifestArrival">The arrival manifest.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        bool SaveManifestArrival(ManifestArrivalDto manifestArrival);

        /// <summary>
        /// Closes the manifest.
        /// </summary>
        /// <param name="manifestArrival">The arrival manifest.</param>
        /// <returns><c>true</c> if the operation was success otherwise <c>false</c>.</returns>
        bool CloseManifest(ManifestArrivalDto manifestArrival);

        /// <summary>
        /// Opens the manifest.
        /// </summary>
        /// <param name="manifestArrival">The arrival manifest.</param>
        /// <returns><c>true</c> if the operation was success otherwise <c>false</c>.</returns>
        bool OpenManifest(ManifestArrivalDto manifestArrival);

        /// <summary>
        /// Gets the delays for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>List of delays in the arrival manifest.</returns>
        IList<DelayDto> GetDelaysForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey);
    }
}
