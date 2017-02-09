//------------------------------------------------------------------------
// <copyright file="AirlineRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class Airline Repository
    /// </summary>
    public class AirlineRepository : Repository<Airline>, IAirlineRepository
    {
        #region Contructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="AirlineRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public AirlineRepository(IDbFactory factory)
            : base(factory)
        {
        }
        #endregion

        #region IAirlineRepository Members

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return Airline</returns>
        public Airline FindById(string id)
        {
            var airline = this.DbContext.Airlines.Where(c => c.AirlineCode == id).FirstOrDefault();
            return airline; 
        }

        /// <summary>
        /// Gets the active airline.
        /// </summary>
        /// <returns>ICollection Airline</returns>
        public ICollection<Airline> GetActiveAirline()
        {
            return this.DbContext.Airlines.Where(c => c.Status).ToList();
        }

        /// <summary>
        /// Validate if the airlines exist.
        /// </summary>
        /// <param name="airlineCodes">The airline codes to validate.</param>
        /// <returns>Returns a list with the Airline Codes that do not exist.</returns>
        public IList<string> ValidatedIfAirlineExist(IList<string> airlineCodes)
        {
            IList<string> notFound = new List<string>();
            IList<Airline> airlineList = this.DbContext.Airlines.Where(c => c.Status).ToList();

            notFound = airlineCodes.Except(airlineList.Select(c => c.AirlineCode)).ToList();
            return notFound;
        }
        #endregion
    }
}
