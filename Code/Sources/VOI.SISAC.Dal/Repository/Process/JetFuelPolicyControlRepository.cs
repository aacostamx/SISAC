//------------------------------------------------------------------------
// <copyright file="JetFuelPolicyControlRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using VOI.SISAC.Dal.Infrastructure;
    using VOI.SISAC.Entities.Finance;


    /// <summary>
    /// /JetFuelPolicyControlRepository Class
    /// </summary>
    /// <seealso cref="VOI.SISAC.Dal.Repository.Process.IJetFuelPolicyControlRepository" />
    public class JetFuelPolicyControlRepository : Repository<JetFuelPolicyControl>, IJetFuelPolicyControlRepository
    {
        /// <summary>
        /// JetFuelProcessRepository constructor
        /// </summary>
        /// <param name="factory">The factory.</param>
        public JetFuelPolicyControlRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="PolicyId">The policy identifier.</param>
        /// <returns>
        /// The policies.
        /// </returns>
        public JetFuelPolicyControl FindById(long PolicyId)
        {
            return this.DbContext.JetFuelControlPolicies.AsNoTracking()
                .FirstOrDefault(c => c.PolicyId == PolicyId);
        }

        /// <summary>
        /// Creates the policy parameters.
        /// </summary>
        /// <param name="policyControl">The policy control.</param>
        /// <returns>Policy identifier.</returns>
        public long CreatePolicyParameters(JetFuelPolicyControl policyControl)
        {
            var policyId = 0L;
            try
            {
                policyId = this.DbContext.SavePolicyProvisions(policyControl.StartDateReal,
            policyControl.EndDateReal,
            policyControl.StartDateComplementary,
            policyControl.EndDateComplementary,
            policyControl.HeaderText,
            policyControl.ItemText,
            policyControl.DateValue,
            policyControl.DatePosting,
            policyControl.DateBaseline,
            policyControl.AirlineCode,
            policyControl.ProviderCodes,
            policyControl.ServiceCodes,
            policyControl.AirportCodes,
            policyControl.SendByUserName);
                return policyId;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return policyId;
        }

        /// <summary>
        /// Counts the policies by year.
        /// </summary>
        /// <returns></returns>
        public int CountPoliciesByYear()
        {
            var count = 0;
            var startDate = new DateTime((DateTime.Now.Year - 1), 11, 1);
            var endDate = new DateTime((DateTime.Now.Year + 1), 1, DateTime.DaysInMonth(DateTime.Now.Year + 1, 1));

            count = this.DbContext.JetFuelControlPolicies.
                Where(c => DbFunctions.TruncateTime(c.CreationDate) >= startDate.Date &&
                DbFunctions.TruncateTime(c.CreationDate) <= endDate.Date)
                .Count();

            return count;
        }

        /// <summary>
        /// Gets the paged policies history.
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pagenumber"></param>
        /// <returns></returns>
        public IList<JetFuelPolicyControl> GetPagedPoliciesHistory(int pagesize, int pagenumber)
        {
            var skip = (pagenumber - 1) * pagesize;
            var startDate = new DateTime((DateTime.Now.Year - 1), 11, 1);
            var endDate = new DateTime((DateTime.Now.Year + 1), 1, DateTime.DaysInMonth(DateTime.Now.Year + 1, 1));

            IList<JetFuelPolicyControl> policies = new List<JetFuelPolicyControl>();

            policies = this.DbContext.JetFuelControlPolicies
                .Where(c => DbFunctions.TruncateTime(c.CreationDate) >= startDate.Date &&
                DbFunctions.TruncateTime(c.CreationDate) <= endDate.Date)
                .OrderByDescending(c => c.CreationDate)
                .Skip(skip)
                .Take(pagesize)
                .ToList();

            return policies;
        }

        /// <summary>
        /// Gets the paged policies history search.
        /// </summary>
        /// <returns></returns>
        public IList<JetFuelPolicyControl> GetPagedPoliciesHistorySearch()
        {
            var policies = this.DbContext.JetFuelControlPolicies.ToList();
            return policies;
        }

        /// <summary>
        /// Creates the policy parameters.
        /// </summary>
        /// <param name="policyControl">The policy control.</param>
        /// <returns>
        /// List of currencies, if verify equals 0 the currency has not change type.
        /// </returns>
        public IList<CurrencyTypeChage> VerifyPolicy(JetFuelPolicyControl policyControl)
        {
            IList<CurrencyTypeChage> currencies = this.DbContext.VerifyPolicyProvissions(policyControl.StartDateReal,
                policyControl.EndDateReal,
                policyControl.StartDateComplementary,
                policyControl.EndDateComplementary,
                policyControl.HeaderText,
                policyControl.ItemText,
                policyControl.DateValue,
                policyControl.DatePosting,
                policyControl.DateBaseline,
                policyControl.AirlineCode,
                policyControl.ProviderCodes,
                policyControl.ServiceCodes,
                policyControl.AirportCodes,
                policyControl.SendByUserName);

            return currencies;
        }

        /// <summary>
        /// Cancels the jet fuel policy.
        /// </summary>
        /// <param name="policyID">The policy identifier.</param>
        /// <returns></returns>
        public bool CancelJetFuelPolicy(long policyID)
        {
            var canceled = false;

            try
            {
                canceled = this.DbContext.CancelJetFuelPolicy(policyID);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return canceled;
        }
    }
}
