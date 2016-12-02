//------------------------------------------------------------------------
// <copyright file="IAircraftMovementMessageBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using VOI.SISAC.Business.Dto.Itineraries;

    /// <summary>
    /// Interface for the aircraft movement message.
    /// </summary>
    public interface IAircraftMovementMessageBusiness
    {
        /// <summary>
        /// Gets the information for the MTV message.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// Returns the information for the MVT message.
        /// </returns>
        AircraftMovementMessageDto GetAircraftMovementMessage(int sequence, string airlineCode, string flightNumber, string itineraryKey);
    }
}
