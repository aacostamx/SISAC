using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOI.SISAC.Dal.Infrastructure;
using VOI.SISAC.Entities.Airport;

namespace VOI.SISAC.Dal.Repository.Airports
{
    public class AdditionalPassengerInformationRepository : Repository<AdditionalPassengerInformation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalPassengerInformationRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AdditionalPassengerInformationRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Finds the airplane type by its identifier.
        /// </summary>
        /// <param name="id">The Airplane Type's identifier.</param>
        /// <returns>The Airplane Type specified.</returns>
        public AdditionalPassengerInformation FindById(int sequence, string airlineCode, string flightNumber, string itineraryKey)
        {
            AdditionalPassengerInformation item = this.DbContext.AdditionalPassengerInformation
                .FirstOrDefault(c =>
                    c.Sequence == sequence &&
                    c.AirlineCode == airlineCode &&
                    c.FlightNumber == flightNumber &&
                    c.ItineraryKey == itineraryKey);
            return item;
        }
    }
}
