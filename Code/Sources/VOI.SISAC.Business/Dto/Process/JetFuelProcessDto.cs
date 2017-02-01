//------------------------------------------------------------------------
// <copyright file="JetFuelProcessDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using Enums;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// JetFuelProcessDto Class
    /// </summary>
    public class JetFuelProcessDto
    {
        /// <summary>
        /// JetFuelProcessDto Constructor
        /// </summary>
        public JetFuelProcessDto()
        {
            JetFuelLogErrors = new List<JetFuelLogErrorDto>();
            JetFuelProvisions = new List<JetFuelProvisionDto>();
        }

        /// <summary>
        /// JetFuelProcessDto Constructor with param periodCode
        /// </summary>
        /// <param name="periodCode"></param>
        public JetFuelProcessDto(string periodCode)
        {
            this.PeriodCode = periodCode;
        }

        /// <summary>
        /// Contructor JetFuelProcessDto
        /// </summary>
        /// <param name="periodCode"></param>
        /// <param name="StartDatePeriod"></param>
        /// <param name="EndDatePeriod"></param>
        public JetFuelProcessDto(string periodCode, DateTime StartDatePeriod, DateTime EndDatePeriod)
        {
            this.PeriodCode = periodCode;
            this.StartDatePeriod = StartDatePeriod;
            this.EndDatePeriod = EndDatePeriod;
            this.StatusProcessCode = string.Empty;
            this.ProcessProgress = 0;
            this.StartDateProcess = null;
            this.EndDateProcess = null;
            this.ProcessedByUserName = string.Empty;
            this.CalculationStatusCode = string.Empty;
            this.ConfirmationStatusCode = string.Empty;
            this.ConfirmationDate = null;
            this.ConfirmedByUserName = string.Empty;
            this.RecordsToProcess = 0;
            this.RecordsProcessed = 0;
            this.ProcessAll = false;
            this.ProcessPending = false;
            this.CalculationStatus = null;
            this.ConfirmationStatus = null;
            this.JetFuelLogErrors = null;
            this.StatusProcesses = null;
            this.JetFuelProvisions = null;
            this.ClosedProvision = false;
        }

        /// <summary>
        /// PeriodCode
        /// </summary>
        public string PeriodCode { get; set; }

        /// <summary>
        /// StartDatePeriod
        /// </summary>
        public DateTime StartDatePeriod { get; set; }

        /// <summary>
        /// EndDatePeriod
        /// </summary>
        public DateTime EndDatePeriod { get; set; }

        /// <summary>
        /// StatusProcessCode
        /// </summary>
        public string StatusProcessCode { get; set; }

        /// <summary>
        /// ProcessProgress
        /// </summary>
        public decimal? ProcessProgress { get; set; }

        /// <summary>
        /// StartDateProcess
        /// </summary>
        public DateTime? StartDateProcess { get; set; }

        /// <summary>
        /// EndDateProcess
        /// </summary>
        public DateTime? EndDateProcess { get; set; }

        /// <summary>
        /// ProcessedByUserName
        /// </summary>
        public string ProcessedByUserName { get; set; }

        /// <summary>
        /// CalculationStatusCode
        /// </summary>
        public string CalculationStatusCode { get; set; }

        /// <summary>
        /// ConfirmationStatusCode
        /// </summary>
        public string ConfirmationStatusCode { get; set; }

        /// <summary>
        /// ConfirmationDate
        /// </summary>
        public DateTime? ConfirmationDate { get; set; }

        /// <summary>
        /// ConfirmedByUserName
        /// </summary>
        public string ConfirmedByUserName { get; set; }

        /// <summary>
        /// RecordsToProcess
        /// </summary>
        public int? RecordsToProcess { get; set; }

        /// <summary>
        /// RecordsProcessed
        /// </summary>
        public int? RecordsProcessed { get; set; }

        /// <summary>
        /// Process All Records
        /// </summary>
        public bool ProcessAll { get; set; }

        /// <summary>
        /// Process Pending
        /// </summary>
        public bool ProcessPending { get; set; }

        /// <summary>
        /// ClosedProvision
        /// </summary>
        public bool ClosedProvision { get; set; }

        /// <summary>
        /// Type of Process
        /// </summary>
        public TypeProcessDto TypeProcess { get; set; }

        /// <summary>
        /// CalculationStatus
        /// </summary>
        public CalculationStatusDto CalculationStatus { get; set; }

        /// <summary>
        /// ConfirmationStatus
        /// </summary>
        public ConfirmationStatusDto ConfirmationStatus { get; set; }

        /// <summary>
        /// JetFuelLogError
        /// </summary>
        public IList<JetFuelLogErrorDto> JetFuelLogErrors { get; set; }

        /// <summary>
        /// StatusProcess
        /// </summary>
        public StatusProcessDto StatusProcesses { get; set; }

        /// <summary>
        /// JetFuelProvision
        /// </summary>
        public IList<JetFuelProvisionDto> JetFuelProvisions { get; set; }
    }
}
