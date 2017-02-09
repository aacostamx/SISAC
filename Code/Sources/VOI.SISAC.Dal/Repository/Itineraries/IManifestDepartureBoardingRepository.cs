//-------------------------------------------------------------------------------
// <copyright file="IManifestDepartureBoardingRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// IManifestDepartureBoardingRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.IRepository{VOI.SISAC.Entities.Itineraries.ManifestDepartureBoarding}" />
    public interface IManifestDepartureBoardingRepository : IRepository<ManifestDepartureBoarding>
    {
        /// <summary>
        /// Gets the boardings for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns></returns>
        IList<ManifestDepartureBoarding> GetBoardingsForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey);

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        ManifestDepartureBoarding FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey, int position);
    }
}
