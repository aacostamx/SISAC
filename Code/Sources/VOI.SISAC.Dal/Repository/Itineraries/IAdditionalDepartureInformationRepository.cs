//------------------------------------------------------------------------
// <copyright file="IAdditionalDepartureInformationRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;


    /// <summary>
    /// IAdditionalDepartureInformationRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.IRepository{VOI.SISAC.Entities.Itineraries.AdditionalDepartureInformation}" />
    public interface IAdditionalDepartureInformationRepository : IRepository<AdditionalDepartureInformation>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns></returns>
        AdditionalDepartureInformation FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey);
    }
}
