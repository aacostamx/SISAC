//------------------------------------------------------------------------
// <copyright file="INationalJetFuelProcessBusiness.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Process
{
    using Dto.Process;
    using System.Collections.Generic;

    /// <summary>
    /// INationalJetFuelProcessBusiness interface
    /// </summary>
    public interface INationalJetFuelProcessBusiness
    {
        /// <summary>
        /// Get All NationalJetFuelProcesses
        /// </summary>
        /// <returns></returns>
        IList<NationalJetFuelProcessDto> GetAllNationalJetFuelProcesses();

        /// <summary>
        /// Find NationalJetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        NationalJetFuelProcessDto FindNationalJetFuelProcess(NationalJetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Add NationalJetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool AddNationalJetFuelProcess(NationalJetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Delete NationalJetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool DeleteNationalJetFuelProcess(NationalJetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Update NationalJetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool UpdateNationalJetFuelProcess(NationalJetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Exec NationalJetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        bool StartNationalJetFuelProcess(NationalJetFuelProcessDto jetFuelProcess);

        /// <summary>
        /// Get Current Period
        /// </summary>
        /// <returns></returns>
        IList<NationalJetFuelProcessDto> GetCurrentPeriod();

        /// <summary>
        /// Closes the period.
        /// </summary>
        /// <param name="processDto">The process data transfer object.</param>
        /// <returns><c>true</c> if the operation was correct, otherwise <c>flase</c>.</returns>
        bool ClosePeriod(NationalJetFuelProcessDto processDto);

        /// <summary>
        /// Opens the period.
        /// </summary>
        /// <param name="processDto">The process data transfer object.</param>
        /// <returns><c>true</c> if the operation was correct, otherwise <c>flase</c>.</returns>
        bool OpenPeriod(NationalJetFuelProcessDto processDto);

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
        bool RevertNationalJetFuelProcess(NationalJetFuelProcessDto processDto);
    }
}
