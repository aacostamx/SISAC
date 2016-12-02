//------------------------------------------------------------------------
// <copyright file="AdditionalArrivalInformationRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;
    using System.Linq;

    /// <summary>
    /// AdditionalArrivalInformationRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.Repository{VOI.SISAC.Entities.Itineraries.AdditionalArrivalInformation}" />
    /// <seealso cref="VOI.SISAC.Dal.Repository.Itineraries.IAdditionalArrivalInformationRepository" />
    public class AdditionalArrivalInformationRepository : Repository<AdditionalArrivalInformation>, IAdditionalArrivalInformationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalArrivalInformationRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AdditionalArrivalInformationRepository(IDbFactory factory)
            : base(factory)
        { 
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns></returns>
        public AdditionalArrivalInformation FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            var additional = this.DbContext.AdditionalArrivalInformation.Where(c => c.Sequence == sequence
                                                                && c.AirlineCode == airlineCode
                                                                && c.FlightNumber == flightNumber
                                                                && c.ItineraryKey == itineraryKey)
                                          .FirstOrDefault();
            return additional;
        }
    }
}
