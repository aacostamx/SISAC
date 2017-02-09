//------------------------------------------------------------------------
// <copyright file="INationalJetFuelCostRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for jet fuel provision repository
    /// </summary>
    public interface INationalJetFuelCostRepository : IRepository<NationalJetFuelCost>
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="provisionId">The provision identifier.</param>
        /// <param name="periodCode">The period code.</param>
        /// <returns></returns>
        NationalJetFuelCost FindById(long provisionId, string periodCode);

        /// <summary>
        /// Gets the costs by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns></returns>
        IList<NationalJetFuelCost> GetCostsByPeriod(string periodCode);

        /// <summary>
        /// Gets the costs by period.
        /// </summary>
        /// <param name="beginDate">The begin date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        IList<NationalJetFuelCost> GetCostsByPeriod(DateTime beginDate, DateTime endDate);

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
