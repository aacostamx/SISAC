//------------------------------------------------------------------------
// <copyright file="IJetFuelProvisionBusiness.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Process;

    /// <summary>
    /// Jet fuel provision interface
    /// </summary>
    public interface IJetFuelProvisionBusiness
    {
        /// <summary>
        /// Advances the search.
        /// </summary>
        /// <returns>A list of provision depending on the parameters given.</returns>
        IList<JetFuelProvisionDto> AdvanceSearch(JetFuelCalculationResultParametersDto parameters);

        /// <summary>
        /// Gets the group report.
        /// </summary>
        /// <returns></returns>
        IList<JetFuelProvisionDto> GetGroupReport();

        /// <summary>
        /// Gets the detailed report.
        /// </summary>
        /// <returns></returns>
        IList<JetFuelProvisionDto> GetDetailedReport();

        /// <summary>
        /// Creates the policy.
        /// </summary>
        /// <returns></returns>
        IList<JetFuelProvisionDto> CreatePolicy();
    }
}
