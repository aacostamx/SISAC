//------------------------------------------------------------------------
// <copyright file="IManifestDepartureBusiness.cs" company="Volaris">
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
    public interface IManifestDepartureBusiness
    {
        /// <summary>
        /// Gets the manifest departure for flight.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>The manifest departure for the flight.</returns>
        ManifestDepartureDto GetManifestDepartureForFlight(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Saves the manifest departure.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <returns><c>true</c> if success otherwise <c>false</c>.</returns>
        bool SaveManifestDeparture(ManifestDepartureDto manifestDeparture);

        /// <summary>
        /// Closes the manifest.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <returns><c>true</c> if the operation was success otherwise <c>false</c>.</returns>
        bool CloseManifest(ManifestDepartureDto manifestDeparture);

        /// <summary>
        /// Opens the manifest.
        /// </summary>
        /// <param name="manifestDeparture">The manifest departure.</param>
        /// <returns><c>true</c> if the operation was success otherwise <c>false</c>.</returns>
        bool OpenManifest(ManifestDepartureDto manifestDeparture);

        /// <summary>
        /// Gets the delays for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>List of delays in the manifest departure.</returns>
        IList<DelayDto> GetDelaysForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Gets the boarding for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns></returns>
        IList<ManifestDepartureBoardingDto> GetBoardingForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Gets the boarding information for manifest.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <returns></returns>
        IList<ManifestDepartureBoardingInformationDto> GetBoardingInformationForManifest(long boardingID, string airplaneModel);

        /// <summary>
        /// Gets the boarding detail for manifest.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <returns></returns>
        IList<ManifestDepartureBoardingDetailDto> GetBoardingDetailForManifest(long boardingID, string airplaneModel);
    }
}
