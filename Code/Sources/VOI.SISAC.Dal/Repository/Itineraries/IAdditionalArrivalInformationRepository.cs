//------------------------------------------------------------------------
// <copyright file="IAdditionalArrivalInformationRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// IAdditionalArrivalInformationRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.IRepository{VOI.SISAC.Entities.Itineraries.AdditionalArrivalInformation}" />
    public interface IAdditionalArrivalInformationRepository : IRepository<AdditionalArrivalInformation>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns></returns>
        AdditionalArrivalInformation FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey);
    }
}
