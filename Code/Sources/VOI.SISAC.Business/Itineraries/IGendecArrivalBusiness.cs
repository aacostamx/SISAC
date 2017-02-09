//------------------------------------------------------------------------
// <copyright file="IGendecArrivalBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Itineraries;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// General document Arrival Business Interface
    /// </summary>
    public interface IGendecArrivalBusiness
    {
        /// <summary>
        /// Obtain the Arrival Departure of a Departure Flight.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns>
        /// The information of the general document.
        /// </returns>
        GendecArrivalDto GetGendecArrival(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Validates the general document arrival information.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document arrival.</param>
        /// <returns>
        /// List of possible errors. If the list is empty the operation was success.
        /// </returns>
        IList<string> ValidateGendecArrivalInformation(GendecArrivalDto gendecArrivalDto);

        /// <summary>
        /// Adds the general document arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document arrival.</param>
        /// <returns>
        ///   <c>true</c> if success, otherwise <c>false</c>.
        /// </returns>
        bool AddGendecArrival(GendecArrivalDto gendecArrivalDto);

        /// <summary>
        /// Updates the general document document arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document document arrival.</param>
        /// <returns>
        ///   <c>true</c> if success, otherwise <c>false</c>.
        /// </returns>
        bool UpdateGendecArrival(GendecArrivalDto gendecArrivalDto);

        /// <summary>
        /// Closes the general document arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document arrival.</param>
        /// <returns>
        ///   <c>true</c> if success, otherwise <c>false</c>.
        /// </returns>
        bool CloseGendecArrival(GendecArrivalDto gendecArrivalDto);

        /// <summary>
        /// Opens the general document arrival.
        /// </summary>
        /// <param name="gendecArrivalDto">The general document arrival.</param>
        /// <returns><c>true</c> if success, otherwise <c>false</c>.</returns>
        bool OpenGendecArrival(GendecArrivalDto gendecArrivalDto);
    }
}
