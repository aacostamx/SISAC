//-----------------------------------------------------------------------------
// <copyright file="ManifestDepartureBoardingRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Itineraries
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Itineraries;

    /// <summary>
    /// ManifestDepartureBoardingRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.Repository{VOI.SISAC.Entities.Itineraries.ManifestDepartureBoarding}"/>
    /// <seealso cref="VOI.SISAC.Dal.Repository.Itineraries.IManifestDepartureBoardingRepository"/>
    public class ManifestDepartureBoardingRepository : Repository<ManifestDepartureBoarding>, IManifestDepartureBoardingRepository
    {
        #region Contructor        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ManifestDepartureBoardingRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ManifestDepartureBoardingRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion

        #region ManifestDepartureBoardingRepository Members

        /// <summary>
        /// Gets the boardings for manifest.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <returns></returns>
        public IList<ManifestDepartureBoarding> GetBoardingsForManifest(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            return this.DbContext.ManifestDepartureBoardings
                .Include(c => c.ManifestDepartureBoardingInformations)
                .Include(c => c.ManifestDepartureBoardingDetails)
                .Where(c => c.Sequence == sequence && c.AirlineCode == airlineCode && c.FlightNumber == flightNumber && c.ItineraryKey == itineraryKey)
                .ToList();
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        /// <param name="airlineCode">The airline code.</param>
        /// <param name="flightNumber">The flight number.</param>
        /// <param name="itineraryKey">The itinerary key.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public ManifestDepartureBoarding FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey, int position)
        {
            var manifestDepartureBoarding = this.DbContext.ManifestDepartureBoardings.Where(c => c.Position == position
                                                                                                && c.Sequence == sequence
                                                                                                && c.AirlineCode == airlineCode
                                                                                                && c.FlightNumber == flightNumber
                                                                                                && c.ItineraryKey == itineraryKey)
                                                                                                .Include(c => c.ManifestDepartureBoardingInformations)
                                                                                                .Include(c => c.ManifestDepartureBoardingDetails).FirstOrDefault();
            return manifestDepartureBoarding;
        }
        
        #endregion
    }
}
