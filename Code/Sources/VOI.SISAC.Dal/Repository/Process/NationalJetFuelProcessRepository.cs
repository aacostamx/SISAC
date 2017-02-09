//------------------------------------------------------------------------
// <copyright file="NationalJetFuelProcessRepository.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// JetFuelProcessRepository
    /// </summary>
    public class NationalJetFuelProcessRepository : Repository<NationalJetFuelProcess>, INationalJetFuelProcessRepository
    {
        /// <summary>
        /// NationalJetFuelProcessRepository constructor
        /// </summary>
        /// <param name="factory"></param>
        public NationalJetFuelProcessRepository(IDbFactory factory)
            : base(factory) { }

        /// <summary>
        /// Find Jet Fuel Process by Id
        /// </summary>
        /// <param name="periodCode"></param>
        /// <returns></returns>
        public NationalJetFuelProcess FindNationalJetFuelProcess(string periodCode)
        {
            NationalJetFuelProcess jetFuelDB = new NationalJetFuelProcess();

            jetFuelDB = this.DbContext.NationalJetFuelProcess
                .Where(c => c.PeriodCode == periodCode)
                .Include(p => p.StatusProcess)
                .Include(p => p.CalculationStatus)
                .Include(p => p.ConfirmationStatus)
                .Include(p => p.NationalJetFuelCosts)
                .FirstOrDefault();

            return jetFuelDB;

        }

        /// <summary>
        /// Get Current Period
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IList<NationalJetFuelProcess> GetCurrentPeriod(DateTime startDate, DateTime endDate)
        {
            IList<NationalJetFuelProcess> currentPeriod = new List<NationalJetFuelProcess>();

            currentPeriod = this.DbContext.NationalJetFuelProcess
                .Where(c => DbFunctions.TruncateTime(c.StartDatePeriod) >= startDate.Date &&
                    DbFunctions.TruncateTime(c.EndDatePeriod) <= endDate.Date)
                .ToList();

            return currentPeriod;
        }

        /// <summary>
        /// Counts the errors in period.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <returns>The number of errors in the period.</returns>
        public int CountErrorsInPeriod(string periodCode)
        {
            NationalJetFuelProcess currentPeriod = new NationalJetFuelProcess();
            currentPeriod = this.DbContext.NationalJetFuelProcess.Where(c => c.PeriodCode == periodCode)
                .Include(i => i.NationalJetFuelLogErrors)
                .FirstOrDefault();
            return currentPeriod.NationalJetFuelLogErrors.Count;
        }

        /// <summary>
        /// Revert Jet Fuel Process
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        public bool RevertNationalJetFuelProcess(NationalJetFuelProcess jetFuelProcess)
        {
            var sucess = false;
            try
            {
                this.DbContext.CalculateNationalFuel(jetFuelProcess.PeriodCode, (int)jetFuelProcess.TypeProcess);
                sucess = true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return sucess;
        }

        /// <summary>
        /// Execute NationalJetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        public bool StartNationalJetFuelProcess(NationalJetFuelProcess jetFuelProcess)
        {
            var sucess = false;
            try
            {
                this.DbContext.CalculateNationalFuel(jetFuelProcess.PeriodCode, (int)jetFuelProcess.TypeProcess);
                sucess = true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return sucess;
        }

        /// <summary>
        /// Find Jet Fuel Process No Tracking
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public NationalJetFuelProcess FindNationalJetFuelProcessNoTracking(NationalJetFuelProcess process)
        {
            NationalJetFuelProcess jetFuelDB = new NationalJetFuelProcess();

            jetFuelDB = this.DbContext.NationalJetFuelProcess.AsNoTracking()
                .Where(c => c.PeriodCode == process.PeriodCode)
                .Include(p => p.StatusProcess)
                .Include(p => p.CalculationStatus)
                .Include(p => p.ConfirmationStatus)
                .FirstOrDefault();

            return jetFuelDB;
        }
    }
}
