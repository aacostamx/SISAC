//------------------------------------------------------------------------
// <copyright file="IJetFuelProvisionRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Interface for jet fuel provision repository
    /// </summary>
    public interface IJetFuelProvisionRepository : IRepository<JetFuelProvision>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="provisionId">The provision identifier.</param>
        /// <param name="periodCode">The period code.</param>
        /// <returns>The matching provision.</returns>
        JetFuelProvision FindById(long provisionId, string periodCode);

        /// <summary>
        /// Gets the provisions by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>List of provisions.</returns>
        IList<JetFuelProvision> GetProvisionsByPeriod(string periodCode);

        /// <summary>
        /// Gets the provisions by period.
        /// </summary>
        /// <param name="beginDate">The begin date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// List of provisions.
        /// </returns>
        IList<JetFuelProvision> GetProvisionsByPeriod(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// Sets the policy by period.
        /// </summary>
        /// <param name="beginDate">The begin date.</param>
        /// <param name="endDate">The end date.</param>
        void SetPolicyByPeriod(DateTime beginDate, DateTime endDate);

        /// <summary>
        /// Sets the policy by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        void SetPolicyByPeriod(string periodCode);
    }
}
