//------------------------------------------------------------------------
// <copyright file="NationalJetFuelProcessVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    /// <summary>
    /// NationalJetFuelProcessVO Class
    /// </summary>
    public class NationalJetFuelProcessVO
    {
        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        [StringLength(30)]
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
        [StringLength(5)]
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
        [StringLength(50)]
        public string ProcessedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the calculation status code.
        /// </summary>
        /// <value>
        /// The calculation status code.
        /// </value>
        [StringLength(5)]
        public string CalculationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status code.
        /// </summary>
        /// <value>
        /// The confirmation status code.
        /// </value>
        [StringLength(5)]
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
        [StringLength(50)]
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
        public NationalTypeProcessVO TypeProcess { get; set; }

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
        public CalculationStatusVO CalculationStatus { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status.
        /// </summary>
        /// <value>
        /// The confirmation status.
        /// </value>
        public ConfirmationStatusVO ConfirmationStatus { get; set; }

        /// <summary>
        /// Gets or sets the status process.
        /// </summary>
        /// <value>
        /// The status process.
        /// </value>
        public StatusProcessVO StatusProcess { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel costs.
        /// </summary>
        /// <value>
        /// The national jet fuel costs.
        /// </value>
        public IList<NationalJetFuelCostVO> NationalJetFuelCosts { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel log errors.
        /// </summary>
        /// <value>
        /// The national jet fuel log errors.
        /// </value>
        public IList<NationalJetFuelLogErrorVO> NationalJetFuelLogErrors { get; set; }
    }
}