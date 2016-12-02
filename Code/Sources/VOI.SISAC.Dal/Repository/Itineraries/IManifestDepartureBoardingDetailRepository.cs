//-------------------------------------------------------------------------------
// <copyright file="IManifestDepartureBoardingDetailRepository.cs" company="Volaris">
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
    /// IManifestDepartureBoardingDetail
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Infrastructure.IRepository{VOI.SISAC.Entities.Itineraries.ManifestDepartureBoardingDetail}" />
    public interface IManifestDepartureBoardingDetailRepository : IRepository<ManifestDepartureBoardingDetail>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ManifestDepartureBoardingDetail FindById(long id);

        IList<ManifestDepartureBoardingDetail> GetDetailByBoardingID(long boardingID, string airplaneModel);
    }
}
