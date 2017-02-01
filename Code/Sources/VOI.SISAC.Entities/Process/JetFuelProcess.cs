//------------------------------------------------------------------------
// <copyright file="JetFuelProcess.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Jet Fuel Process
    /// </summary>
    [Table("Process.JetFuelProcess")]
    public partial class JetFuelProcess
    {
        /// <summary>
        /// JetFuelProcess Constructor
        /// </summary>
        public JetFuelProcess()
        {
            JetFuelLogErrors = new List<JetFuelLogError>();
            JetFuelProvisions = new List<JetFuelProvision>();
        }

        /// <summary>
        /// JetFuelProcess Constructor
        /// </summary>
        /// <param name="PeriodCode"></param>
        public JetFuelProcess(string PeriodCode)
        {
            this.PeriodCode = PeriodCode;
        }

        /// <summary>
        /// PeriodCode
        /// </summary>
        [Key]
        [StringLength(30)]
        public string PeriodCode { get; set; }

        /// <summary>
        /// StartDatePeriod
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime StartDatePeriod { get; set; }

        /// <summary>
        /// EndDatePeriod
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime EndDatePeriod { get; set; }

        /// <summary>
        /// StatusProcessCode
        /// </summary>
        [StringLength(5)]
        public string StatusProcessCode { get; set; }

        /// <summary>
        /// ProcessProgress
        /// </summary>
        [Column(TypeName = "numeric")]
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
        /// Type of Process
        /// </summary>
        [NotMapped]
        public TypeProcess TypeProcess { get; set; }

        /// <summary>
        /// CalculationStatus
        /// </summary>
        public virtual CalculationStatus CalculationStatus { get; set; }

        /// <summary>
        /// ConfirmationStatus
        /// </summary>
        public virtual ConfirmationStatus ConfirmationStatus { get; set; }

        /// <summary>
        /// JetFuelLogError
        /// </summary>
        public virtual IList<JetFuelLogError> JetFuelLogErrors { get; set; }

        /// <summary>
        /// StatusProcess
        /// </summary>
        public virtual StatusProcess StatusProcesses { get; set; }

        /// <summary>
        /// JetFuelProvision
        /// </summary>
        public virtual IList<JetFuelProvision> JetFuelProvisions { get; set; }
    }
}
