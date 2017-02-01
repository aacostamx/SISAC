//------------------------------------------------------------------------
// <copyright file="IReconciliationToleranceRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Catalog
{
    using Infrastructure;
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Catalog;

    /// <summary>
    /// Interface IReconciliationToleranceRepository
    /// </summary>
    public interface IReconciliationToleranceRepository : IRepository<ReconciliationTolerance>
    {
        /// <summary>
        /// Find By Id
        /// </summary>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        ReconciliationTolerance FindById(ReconciliationTolerance tolerance);

        /// <summary>
        /// Finds the by identifier no tracking.
        /// </summary>
        /// <param name="tolerance">The tolerance.</param>
        /// <returns></returns>
        ReconciliationTolerance FindByIdNoTracking(ReconciliationTolerance tolerance);

        /// <summary>
        /// Get Actives Exchange Rates 
        /// </summary>
        /// <returns></returns>
        IList<ReconciliationTolerance> GetActivesReconciliationTolerances();
    }
}
