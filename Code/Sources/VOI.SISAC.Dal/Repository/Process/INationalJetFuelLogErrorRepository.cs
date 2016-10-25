//----------------------------------------------------------------------------
// <copyright file="INationalJetFuelLogErrorRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for jet fuel log error repository
    /// </summary>
    public interface INationalJetFuelLogErrorRepository : IRepository<NationalJetFuelLogError>
    {
        /// <summary>
        /// Gets the national log error by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns></returns>
        IList<NationalJetFuelLogError> GetLogErrorByPeriodCode(string periodCode);
    }
}
