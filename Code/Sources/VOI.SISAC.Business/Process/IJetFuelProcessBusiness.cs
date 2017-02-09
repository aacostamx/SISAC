//------------------------------------------------------------------------
// <copyright file="IJetFuelProcessBusiness.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using System;
    using System.Collections.Generic;
    using VOI.SISAC.Business.Dto.Process;

    /// <summary>
    /// IJetFuelProcessBusiness interface
    /// </summary>
    public interface IJetFuelProcessBusiness
    {
        /// <summary>
        /// Get All JetFuelProcesses
        /// </summary>
        /// <returns></returns>
        IList<JetFuelProcessDto> GetAllJetFuelProcesses();

        /// <summary>
        /// Find JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        JetFuelProcessDto FindJetFuelProcess(JetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Add JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool AddJetFuelProcess(JetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Delete JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool DeleteJetFuelProcess(JetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Update JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool UpdateJetFuelProcess(JetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Exec JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool StartJetFuelProcess(JetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Get Current Period
        /// </summary>
        /// <returns></returns>
        IList<JetFuelProcessDto> GetCurrentPeriod();

        /// <summary>
        /// Closes the period.
        /// </summary>
        /// <param name="processDto">The process data transfer object.</param>
        /// <returns><c>true</c> if the operation was correct, otherwise <c>flase</c>.</returns>
        bool ClosePeriod(JetFuelProcessDto processDto);

        /// <summary>
        /// Opens the period.
        /// </summary>
        /// <param name="processDto">The process data transfer object.</param>
        /// <returns><c>true</c> if the operation was correct, otherwise <c>flase</c>.</returns>
        bool OpenPeriod(JetFuelProcessDto processDto);

        /// <summary>
        /// Determines whether the process is running.
        /// </summary>
        /// <returns><c>true</c> if the process is running otherwise <c>false</c>.</returns>
        bool IsProcessOnExcecution(string periodCode);

        /// <summary>
        /// Validates if the period has erros.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns><c>true</c> if the period has errors otherwise <c>false</c>.</returns>
        bool ValidateIfPeriodHasErros(string periodCode);

        /// <summary>
        /// Revert Jet Fuel Process
        /// </summary>
        /// <param name="processDto"></param>
        /// <returns></returns>
        bool RevertJetFuelProcess(JetFuelProcessDto processDto);
    }
}
