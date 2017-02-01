//------------------------------------------------------------------------
// <copyright file="JetFuelProvisionRepository.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Process;

    /// <summary>
    /// Operations for jet fuel provision repository
    /// </summary>
    public class JetFuelProvisionRepository : Repository<JetFuelProvision>, IJetFuelProvisionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JetFuelProvisionRepository"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public JetFuelProvisionRepository(IDbFactory factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="provisionId">The provision identifier.</param>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// The matching provision.
        /// </returns>
        public JetFuelProvision FindById(long provisionId, string periodCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the provisions by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>
        /// List of provisions.
        /// </returns>
        public IList<JetFuelProvision> GetProvisionsByPeriod(string periodCode)
        {
            return this.DbContext.JetFuelProvisions.Where(c => c.PeriodCode == periodCode).ToList();
        }

        /// <summary>
        /// Gets the provisions by period.
        /// </summary>
        /// <param name="beginDate">The begin date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// List of provisions.
        /// </returns>
        public IList<JetFuelProvision> GetProvisionsByPeriod(DateTime beginDate, DateTime endDate)
        {
            List<string> periodCode = this.DbContext.JetFuelProcess
                .Where(c => c.StartDatePeriod >= beginDate && c.EndDatePeriod <= endDate)
                .Select(c => c.PeriodCode)
                .ToList();

            return this.DbContext.JetFuelProvisions
                .Where(c => periodCode.Contains(c.PeriodCode))
                .ToList();
        }

        /// <summary>
        /// Sets the policy by period.
        /// </summary>
        /// <param name="beginDate">The begin date.</param>
        /// <param name="endDate">The end date.</param>
        public void SetPolicyByPeriod(DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the policy by period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        public void SetPolicyByPeriod(string periodCode)
        {
            throw new NotImplementedException();
        }
    }
}
