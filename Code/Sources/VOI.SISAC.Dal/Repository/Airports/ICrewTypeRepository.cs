//------------------------------------------------------------------------
// <copyright file="ICrewTypeRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------

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
    /// Interface ICrewTypeRepository
    /// </summary>
    public interface ICrewTypeRepository : IRepository<CrewType>
    {
        /// <summary>
        /// Validateds if crew type exist.
        /// </summary>
        /// <param name="crewTypeID">The crew type identifier.</param>
        /// <returns></returns>
        List<string> ValidatedIfCrewTypeExist(IList<string> crewTypeID);
    }
}
