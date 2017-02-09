//------------------------------------------------------------------------
// <copyright file="IJetFuelProcessRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    /// <summary>
    /// IJetFuelProcessRepository Interface
    /// </summary>
    public interface IJetFuelProcessRepository : IRepository<JetFuelProcess>
    {
        /// <summary>
        /// Start Jet Fuel Process
        /// </summary>
        bool StartJetFuelProcess(JetFuelProcess jetFuelProcess);

        /// <summary>
        /// Revert Jet Fuel Process
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool RevertJetFuelProcess(JetFuelProcess jetFuelProcess);

        /// <summary>
        /// Find Jet Fuel Process by Id
        /// </summary>
        /// <param name="periodCode"></param>
        /// <returns></returns>
        JetFuelProcess FindJetFuelProcess(string periodCode);

        /// <summary>
        /// Find JetFuel Process No Tracking
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        JetFuelProcess FindJetFuelProcessNoTracking(JetFuelProcess process);

        /// <summary>
        /// Get Current Period
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IList<JetFuelProcess> GetCurrentPeriod(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Counts the errors in period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>The number of errors in the period.</returns>
        int CountErrorsInPeriod(string periodCode);
    }
}
