//------------------------------------------------------------------------
// <copyright file="AdditionalDepartureInformationRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;
    using System.Linq;

    /// <summary>
    /// AdditionalDepartureInformationRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.Repository{VOI.SISAC.Entities.Itineraries.AdditionalDepartureInformation}" />
    /// <seealso cref="VOI.SISAC.Dal.Repository.Itineraries.IAdditionalDepartureInformationRepository" />
    public class AdditionalDepartureInformationRepository : Repository<AdditionalDepartureInformation>, IAdditionalDepartureInformationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalDepartureInformationRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AdditionalDepartureInformationRepository(IDbFactory factory) : base(factory)
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
        public AdditionalDepartureInformation FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            var additional = this.DbContext.AdditionalDepartureInformation.Where(c => c.Sequence == sequence
                                                                && c.AirlineCode == airlineCode
                                                                && c.FlightNumber == flightNumber
                                                                && c.ItineraryKey == itineraryKey)                                          
                                          .FirstOrDefault();
            return additional;
        }
    }
}
