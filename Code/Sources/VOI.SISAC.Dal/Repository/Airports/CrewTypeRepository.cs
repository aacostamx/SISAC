//------------------------------------------------------------------------
// <copyright file="CrewTypeRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Airport;

    /// <summary>
    /// Class CrewTypeRepository
    /// </summary>
    public class CrewTypeRepository : Repository<CrewType>, ICrewTypeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrewTypeRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CrewTypeRepository(IDbFactory factory)
            : base(factory)
        { 
        }

        /// <summary>
        /// Validateds if crew type exist.
        /// </summary>
        /// <param name="crewTypeID">The crew type identifier.</param>
        /// <returns></returns>
        public List<string> ValidatedIfCrewTypeExist(IList<string> crewTypeID)
        {
            List<string> notFound = new List<string>();
            List<CrewType> crews = this.DbContext.CrewTypes.ToList();

            notFound = crewTypeID.Except(crews.Select(c => c.CrewTypeID)).ToList();
            return notFound;
        }
    }
}
