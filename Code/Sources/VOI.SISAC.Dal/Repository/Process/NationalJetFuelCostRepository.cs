//------------------------------------------------------------------------
// <copyright file="NationalJetFuelCostRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// NationalJetFuelCostRepository Class
    /// </summary>
    public class NationalJetFuelCostRepository : Repository<NationalJetFuelCost>, INationalJetFuelCostRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelCostRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalJetFuelCostRepository(IDbFactory factory)
            : base(factory) { }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="provisionId">The provision identifier.</param>
        /// <param name="periodCode">The period code.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public NationalJetFuelCost FindById(long provisionId, string periodCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the costs by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns></returns>
        public IList<NationalJetFuelCost> GetCostsByPeriod(string periodCode)
        {
            return this.DbContext.NationalJetFuelCost.Where(c => c.PeriodCode == periodCode).ToList();
        }

        /// <summary>
        /// Gets the costs by period.
        /// </summary>
        /// <param name="beginDate">The begin date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IList<NationalJetFuelCost> GetCostsByPeriod(DateTime beginDate, DateTime endDate)
        {
            List<string> periodCode = this.DbContext.JetFuelProcess
                .Where(c => c.StartDatePeriod >= beginDate && c.EndDatePeriod <= endDate)
                .Select(c => c.PeriodCode)
                .ToList();

            return this.DbContext.NationalJetFuelCost
                .Where(c => periodCode.Contains(c.PeriodCode))
                .ToList();
        }

        /// <summary>
        /// Sets the policy by period.
        /// </summary>
        /// <param name="beginDate">The begin date.</param>
        /// <param name="endDate">The end date.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetPolicyByPeriod(DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the policy by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetPolicyByPeriod(string periodCode)
        {
            throw new NotImplementedException();
        }
    }
}
