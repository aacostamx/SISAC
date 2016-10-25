//------------------------------------------------------------------------
// <copyright file="JetFuelProcessRepository.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Dal.Repository.Process
{
    using Entities.Process;
    using Infrastructure;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Data.SqlClient;
    using System.Diagnostics;
    
    /// <summary>
    /// JetFuelProcessRepository
    /// </summary>
    public class JetFuelProcessRepository : Repository<JetFuelProcess>, IJetFuelProcessRepository
    {
        /// <summary>
        /// JetFuelProcessRepository constructor
        /// </summary>
        /// <param name="factory"></param>
        public JetFuelProcessRepository(IDbFactory factory)
            : base(factory)
        { }

        /// <summary>
        /// Find Jet Fuel Process by Id
        /// </summary>
        /// <param name="periodCode"></param>
        /// <returns></returns>
        public JetFuelProcess FindJetFuelProcess(string periodCode)
        {
            JetFuelProcess jetFuelDB = new JetFuelProcess();

            jetFuelDB = this.DbContext.JetFuelProcess
                .Where(c => c.PeriodCode == periodCode)
                .Include(p => p.StatusProcesses)
                .Include(p => p.CalculationStatus)
                .Include(p => p.ConfirmationStatus)
                .Include(p => p.JetFuelProvisions)
                .FirstOrDefault();

            return jetFuelDB;

        }

        /// <summary>
        /// Get Current Period
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IList<JetFuelProcess> GetCurrentPeriod(DateTime startDate, DateTime endDate)
        {
            IList<JetFuelProcess> currentPeriod = new List<JetFuelProcess>();

            currentPeriod = this.DbContext.JetFuelProcess
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
            JetFuelProcess currentPeriod = new JetFuelProcess();
            currentPeriod = this.DbContext.JetFuelProcess.Where(c => c.PeriodCode == periodCode)
                .Include(i => i.JetFuelLogErrors)
                .FirstOrDefault();
            return currentPeriod.JetFuelLogErrors.Count;
        }

        /// <summary>
        /// Revert Jet Fuel Process
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        public bool RevertJetFuelProcess(JetFuelProcess jetFuelProcess)
        {
            var sucess = false;
            try
            {
                this.DbContext.CalculateInternationalFuel(jetFuelProcess.PeriodCode, (int)jetFuelProcess.TypeProcess);
                sucess = true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message, ex);
            }
            return sucess;
        }

        /// <summary>
        /// Execute JetFuelProcess
        /// </summary>
        /// <param name="jetFuelProcess"></param>
        /// <returns></returns>
        public bool StartJetFuelProcess(JetFuelProcess jetFuelProcess)
        {
            var sucess = false;
            try
            {
                this.DbContext.CalculateInternationalFuel(jetFuelProcess.PeriodCode, (int)jetFuelProcess.TypeProcess);
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
        public JetFuelProcess FindJetFuelProcessNoTracking(JetFuelProcess process)
        {
            JetFuelProcess jetFuelDB = new JetFuelProcess();

            jetFuelDB = this.DbContext.JetFuelProcess.AsNoTracking()
                .Where(c => c.PeriodCode == process.PeriodCode)
                .Include(p => p.StatusProcesses)
                .Include(p => p.CalculationStatus)
                .Include(p => p.ConfirmationStatus)
                .FirstOrDefault();

            return jetFuelDB;
        }
    }
}
