//------------------------------------------------------------------------
// <copyright file="IJetFuelLogErrorRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Interface for jet fuel log error repository
    /// </summary>
    public interface IJetFuelLogErrorRepository : IRepository<JetFuelLogError>
    {
        /// <summary>
        /// Gets the log errors by period code.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>The errors for the period.</returns>
        IList<JetFuelLogError> GetLogErrorByPeriodCode(string periodCode);
    }
}
