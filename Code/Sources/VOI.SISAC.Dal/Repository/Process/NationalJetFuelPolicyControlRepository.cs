//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyControlRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
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
    public class NationalJetFuelPolicyControlRepository : Repository<NationalJetFuelPolicyControl>, INationalJetFuelPolicyControlRepository
    {
        /// <summary>
        /// JetFuelProcessRepository constructor
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalJetFuelPolicyControlRepository(IDbFactory factory)
            : base(factory) { }

        /// <summary>
        /// Finds the national policy control.
        /// </summary>
        /// <param name="policyCtrl">The policy control.</param>
        /// <returns></returns>
        public NationalJetFuelPolicyControl FindNationalPolicyControl(NationalJetFuelPolicyControl policyCtrl)
        {
            var found = new NationalJetFuelPolicyControl();
            found = this.DbContext.NationalJetFuelPolicyControl.AsNoTracking().FirstOrDefault(c => c.NationalPolicyID == policyCtrl.NationalPolicyID);
            return found;
        }

        /// <summary>
        /// Creates the policy parameters.
        /// </summary>
        /// <param name="policyCtrl">The policy control.</param>
        /// <returns></returns>
        public long CreatePolicyParams(NationalJetFuelPolicyControl policyCtrl)
        {
            var policyID = 0L;

            try
            {
                policyID = this.DbContext.SaveNationalPolicyCost(policyCtrl);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return policyID;
        }

        /// <summary>
        /// Counts all policies.
        /// </summary>
        /// <returns></returns>
        public int CountPoliciesByYear()
        {
            var count = 0;
            var startDate = new DateTime((DateTime.Now.Year - 1), 11, 1);
            var endDate = new DateTime((DateTime.Now.Year + 1), 1, DateTime.DaysInMonth(DateTime.Now.Year + 1, 1));

            count = this.DbContext.NationalJetFuelPolicyControl
                .Where(c => DbFunctions.TruncateTime(c.CreationDate) >= startDate.Date &&
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
        public IList<NationalJetFuelPolicyControl> GetPagedPoliciesHistory(int pagesize, int pagenumber)
        {
            var skip = (pagenumber - 1) * pagesize;
            var startDate = new DateTime((DateTime.Now.Year - 1), 11, 1);
            var endDate = new DateTime((DateTime.Now.Year + 1), 1, DateTime.DaysInMonth(DateTime.Now.Year + 1, 1));

            IList<NationalJetFuelPolicyControl> policies = new List<NationalJetFuelPolicyControl>();

            policies = this.DbContext.NationalJetFuelPolicyControl
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
        public IList<NationalJetFuelPolicyControl> GetPagedPoliciesHistorySearch()
        {
            var policies = this.DbContext.NationalJetFuelPolicyControl.ToList();
            return policies;
        }

        /// <summary>
        /// Checks the policy currencies.
        /// </summary>
        /// <param name="policyCtrl">The policy control.</param>
        /// <returns></returns>
        public IList<CurrencyTypeChage> CheckPolicyCurrencies(NationalJetFuelPolicyControl policyCtrl)
        {
            var currencies = new List<CurrencyTypeChage>();

            try
            {
                //currencies = this.DbContext.VerifyNationalPolicyCurrency(policyCtrl);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return currencies;
        }


        /// <summary>
        /// Cancels the national jet fuel policy.
        /// </summary>
        /// <param name="policyID">The policy identifier.</param>
        /// <returns></returns>
        public bool CancelNationalJetFuelPolicy(long policyID)
        {
            var canceled = false;

            try
            {
                canceled = this.DbContext.CancelNationalJetFuelPolicy(policyID);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }

            return canceled;
        }
    }
}
