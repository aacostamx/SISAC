//------------------------------------------------------------------------
// <copyright file="NationalJetFuelProcessDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using System;
    using System.Collections.Generic;
    using Enums;

    /// <summary>
    /// NationalJetFuelProcessDto Class
    /// </summary>
    public class NationalJetFuelProcessDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelProcessDto"/> class.
        /// </summary>
        public NationalJetFuelProcessDto()
        {
            NationalJetFuelLogErrors = new List<NationalJetFuelLogErrorDto>();
            NationalJetFuelCosts = new List<NationalJetFuelCostDto>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelProcessDto"/> class.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        public NationalJetFuelProcessDto(string periodCode)
        {
            this.PeriodCode = periodCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelProcessDto"/> class.
        /// </summary>
        /// <param name="periodCode">The period code.</param>
        /// <param name="StartDatePeriod">The start date period.</param>
        /// <param name="EndDatePeriod">The end date period.</param>
        public NationalJetFuelProcessDto(string periodCode, DateTime StartDatePeriod, DateTime EndDatePeriod)
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
            this.StatusProcess = null;
            this.ClosedProvision = false;
            this.NationalJetFuelLogErrors = null;
            this.NationalJetFuelCosts = null;
        }

        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        public string PeriodCode { get; set; }

        /// <summary>
        /// Gets or sets the start date period.
        /// </summary>
        /// <value>
        /// The start date period.
        /// </value>
        public DateTime StartDatePeriod { get; set; }

        /// <summary>
        /// Gets or sets the end date period.
        /// </summary>
        /// <value>
        /// The end date period.
        /// </value>
        public DateTime EndDatePeriod { get; set; }

        /// <summary>
        /// Gets or sets the status process code.
        /// </summary>
        /// <value>
        /// The status process code.
        /// </value>
        public string StatusProcessCode { get; set; }

        /// <summary>
        /// Gets or sets the process progress.
        /// </summary>
        /// <value>
        /// The process progress.
        /// </value>
        public decimal? ProcessProgress { get; set; }

        /// <summary>
        /// Gets or sets the start date process.
        /// </summary>
        /// <value>
        /// The start date process.
        /// </value>
        public DateTime? StartDateProcess { get; set; }

        /// <summary>
        /// Gets or sets the end date process.
        /// </summary>
        /// <value>
        /// The end date process.
        /// </value>
        public DateTime? EndDateProcess { get; set; }

        /// <summary>
        /// Gets or sets the processed name of the by user.
        /// </summary>
        /// <value>
        /// The processed name of the by user.
        /// </value>
        public string ProcessedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the calculation status code.
        /// </summary>
        /// <value>
        /// The calculation status code.
        /// </value>
        public string CalculationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status code.
        /// </summary>
        /// <value>
        /// The confirmation status code.
        /// </value>
        public string ConfirmationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the confirmation date.
        /// </summary>
        /// <value>
        /// The confirmation date.
        /// </value>
        public DateTime? ConfirmationDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the confirmed by user.
        /// </summary>
        /// <value>
        /// The name of the confirmed by user.
        /// </value>
        public string ConfirmedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the records to process.
        /// </summary>
        /// <value>
        /// The records to process.
        /// </value>
        public int? RecordsToProcess { get; set; }

        /// <summary>
        /// Gets or sets the records processed.
        /// </summary>
        /// <value>
        /// The records processed.
        /// </value>
        public int? RecordsProcessed { get; set; }

        /// <summary>
        /// Gets or sets the type process.
        /// </summary>
        /// <value>
        /// The type process.
        /// </value>
        public NationalTypeProcessDto TypeProcess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [process all].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [process all]; otherwise, <c>false</c>.
        /// </value>
        public bool ProcessAll { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [process pending].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [process pending]; otherwise, <c>false</c>.
        /// </value>
        public bool ProcessPending { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [closed provision].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [closed provision]; otherwise, <c>false</c>.
        /// </value>
        public bool ClosedProvision { get; set; }

        /// <summary>
        /// Gets or sets the calculation status.
        /// </summary>
        /// <value>
        /// The calculation status.
        /// </value>
        public CalculationStatusDto CalculationStatus { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status.
        /// </summary>
        /// <value>
        /// The confirmation status.
        /// </value>
        public ConfirmationStatusDto ConfirmationStatus { get; set; }

        /// <summary>
        /// Gets or sets the status process.
        /// </summary>
        /// <value>
        /// The status process.
        /// </value>
        public StatusProcessDto StatusProcess { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel costs.
        /// </summary>
        /// <value>
        /// The national jet fuel costs.
        /// </value>
        public IList<NationalJetFuelCostDto> NationalJetFuelCosts { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel log errors.
        /// </summary>
        /// <value>
        /// The national jet fuel log errors.
        /// </value>
        public IList<NationalJetFuelLogErrorDto> NationalJetFuelLogErrors { get; set; }
    }
}
