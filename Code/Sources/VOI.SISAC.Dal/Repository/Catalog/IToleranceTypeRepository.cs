//-------------------------------------------------------------------
// <copyright file="IToleranceTypeRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{

    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// IToleranceTypeRepository class
    /// </summary>
    public interface IToleranceTypeRepository : IRepository<ToleranceType>
    {

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ToleranceType FindById(string toleranceTypeCode);


        /// <summary>
        /// Gets the actives tolerance types.
        /// </summary>
        /// <returns></returns>
        IList<ToleranceType> GetActivesToleranceTypes();
    }
}
