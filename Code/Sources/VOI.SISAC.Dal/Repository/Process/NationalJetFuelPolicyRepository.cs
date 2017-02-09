//------------------------------------------------------------------------
// <copyright file="NationalJetFuelPolicyRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Operation for Jet Fuel Policy Repository
    /// </summary>
    public class NationalJetFuelPolicyRepository : Repository<NationalJetFuelPolicy>, INationalJetFuelPolicyRepository
    {
        /// <summary>
        /// JetFuelProcessRepository constructor
        /// </summary>
        /// <param name="factory">The factory.</param>
        public NationalJetFuelPolicyRepository(IDbFactory factory)
            : base(factory) { }

        /// <summary>
        /// Finds the national policy by PolicyResultID (primary key) Its the same NationalJetFuelPolicyID
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        public NationalJetFuelPolicy FindNationalPolicy(NationalJetFuelPolicy policy)
        {
            var foundPolicy = new NationalJetFuelPolicy();
            foundPolicy = this.DbContext.NationalJetFuelPolicy.FirstOrDefault(c => c.PolicyResultID == policy.PolicyResultID);
            return foundPolicy;
        }

        /// <summary>
        /// Finds the national policy control.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        public IList<NationalJetFuelPolicy> GetNationalPoliciesControl(NationalJetFuelPolicy policy)
        {
            var policiesControl = new List<NationalJetFuelPolicy>();
            policiesControl = this.DbContext.NationalJetFuelPolicy.Where(c => c.NationalPolicyID == policy.NationalPolicyID).ToList();
            return policiesControl;
        }

        /// <summary>
        /// Finds the national policy sap.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        public NationalJetFuelPolicy FindNationalPolicySAP(NationalJetFuelPolicy policy)
        {
            NationalJetFuelPolicy policySAP = new NationalJetFuelPolicy();
            policySAP = this.DbContext.NationalJetFuelPolicy.FirstOrDefault(c => c.NationalPolicyID == policy.NationalPolicyID && c.IDREG == policy.IDREG);
            return policySAP;
        }
    }
}
