//------------------------------------------------------------------------
// <copyright file="IJetFuelPolicyBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Process;

    /// <summary>
    /// Interface Jet Fuel Policy Business
    /// </summary>
    public interface IJetFuelPolicyBusiness
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="policyResultId">The policy result identifier.</param>
        /// <returns>
        /// The policies.
        /// </returns>
        JetFuelPolicyDto FindById(long policyResultId);

        /// <summary>
        /// Creates the specified policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>True if success otherwise false.</returns>
        bool Create(JetFuelPolicyDto policy);

        /// <summary>
        /// Deletes the specified policy.
        /// </summary>
        /// <param name="policyResultId">The policy identifier.</param>
        /// <returns>True if success otherwise false.</returns>
        bool Delete(long policyResultId);

        /// <summary>
        /// Updates the specified policy.
        /// </summary>
        /// <param name="policy">The policy.</param>
        /// <returns>True if success otherwise false.</returns>
        bool Update(JetFuelPolicyDto policy);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List of <see cref="JetFuelPolicyDto"/>.</returns>
        IList<JetFuelPolicyDto> GetAll();

        /// <summary>
        /// Gets Unsuccessful
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// List of <see cref="JetFuelPolicyDto" />.
        /// </returns>
        IList<JetFuelPolicyDto> GetUnsuccessful(long id);

        /// <summary>
        /// Gets the policies by policy control identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>List of <see cref="JetFuelPolicyDto"/>.</returns>
        IList<JetFuelPolicyDto> GetPoliciesByPolicyControlId(long id);

        /// <summary>
        /// Updates the policies data from the service response.
        /// </summary>
        /// <param name="policy">The policies.</param>
        /// <returns>
        /// True if success otherwise false.
        /// </returns>
        bool UpdatePoliciesFromResponse(List<JetFuelPolicyDto> policies);
    }
}
