//------------------------------------------------------------------------
// <copyright file="INationalJetFuelProcessRepository.cs" company="Volaris">
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
    /// INationalJetFuelProcessRepository Interface
    /// </summary>
    public interface INationalJetFuelProcessRepository : IRepository<NationalJetFuelProcess>
    {
        /// <summary>
        /// Start Jet Fuel Process
        /// </summary>
        bool StartNationalJetFuelProcess(NationalJetFuelProcess jetFuelProcess);

        /// <summary>
        /// Revert Jet Fuel Process
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool RevertNationalJetFuelProcess(NationalJetFuelProcess jetFuelProcess);

        /// <summary>
        /// Find Jet Fuel Process by Id
        /// </summary>
        /// <param name="periodCode"></param>
        /// <returns></returns>
        NationalJetFuelProcess FindNationalJetFuelProcess(string periodCode);

        /// <summary>
        /// Find NationalJetFuel Process No Tracking
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        NationalJetFuelProcess FindNationalJetFuelProcessNoTracking(NationalJetFuelProcess process);

        /// <summary>
        /// Get Current Period
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        IList<NationalJetFuelProcess> GetCurrentPeriod(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Counts the errors in period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>The number of errors in the period.</returns>
        int CountErrorsInPeriod(string periodCode);
    }
}
