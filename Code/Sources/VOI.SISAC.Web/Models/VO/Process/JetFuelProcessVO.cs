//------------------------------------------------------------------------
// <copyright file="JetFuelProcessVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// JetFuelProcessVO CLass
    /// </summary>
    public class JetFuelProcessVO
    {
        /// <summary>
        /// JetFuelProcessVO Constructor
        /// </summary>
        public JetFuelProcessVO() { }

        /// <summary>
        /// PeriodCode
        /// </summary>
        [StringLength(30)]
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
        [StringLength(5)]
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
        [StringLength(50)]
        public string ProcessedByUserName { get; set; }

        /// <summary>
        /// CalculationStatusCode
        /// </summary>
        [StringLength(5)]
        public string CalculationStatusCode { get; set; }

        /// <summary>
        /// ConfirmationStatusCode
        /// </summary>
        [StringLength(5)]
        public string ConfirmationStatusCode { get; set; }

        /// <summary>
        /// ConfirmationDate
        /// </summary>
        public DateTime? ConfirmationDate { get; set; }

        /// <summary>
        /// ConfirmedByUserName
        /// </summary>
        [StringLength(50)]
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
        /// Type of Process
        /// </summary>
        public TypeProcessVO TypeProcess { get; set; }

        /// <summary>
        /// CalculationStatus
        /// </summary>
        public CalculationStatusVO CalculationStatus { get; set; }

        /// <summary>
        /// ConfirmationStatus
        /// </summary>
        public ConfirmationStatusVO ConfirmationStatus { get; set; }

        /// <summary>
        /// JetFuelLogError
        /// </summary>
        //public IList<JetFuelLogErrorVO> JetFuelLogErrors { get; set; }

        /// <summary>
        /// StatusProcess
        /// </summary>
        public StatusProcessVO StatusProcesses { get; set; }

        /// <summary>
        /// JetFuelProvision
        /// </summary>
        public IList<JetFuelProvisionVO> JetFuelProvisions { get; set; }
    }
}