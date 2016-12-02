//-------------------------------------------------------------------------------
// <copyright file="IManifestDepartureBoardingInformationRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    /// IManifestDepartureBoardingInformationRepository
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.IRepository{VOI.SISAC.Entities.Itineraries.ManifestDepartureBoardingInformation}" />
    public interface IManifestDepartureBoardingInformationRepository : IRepository<ManifestDepartureBoardingInformation>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ManifestDepartureBoardingInformation FindById(long id);

        /// <summary>
        /// Gets the information by boarding identifier.
        /// </summary>
        /// <param name="boardingID">The boarding identifier.</param>
        /// <returns></returns>
        IList<ManifestDepartureBoardingInformation> GetInformationByBoardingID(long boardingID, string airplaneModel);
    }
}
