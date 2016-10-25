//------------------------------------------------------------------------
// <copyright file="NationalJetFuelProcess.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    /// NationalJetFuelProcess Entity Class
    /// </summary>
    [Table("Process.NationalJetFuelProcess")]
    public partial class NationalJetFuelProcess
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelProcess"/> class.
        /// </summary>
        public NationalJetFuelProcess()
        {
            this.NationalJetFuelLogErrors = new List<NationalJetFuelLogError>();
            this.NationalJetFuelCosts = new List<NationalJetFuelCost>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelProcess"/> class.
        /// </summary>
        /// <param name="PeriodCode">The period code.</param>
        public NationalJetFuelProcess(string PeriodCode)
        {
            this.PeriodCode = PeriodCode;
        }

        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        [Key]
        [StringLength(30)]
        public string PeriodCode { get; set; }

        /// <summary>
        /// Gets or sets the start date period.
        /// </summary>
        /// <value>
        /// The start date period.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime StartDatePeriod { get; set; }

        /// <summary>
        /// Gets or sets the end date period.
        /// </summary>
        /// <value>
        /// The end date period.
        /// </value>
        [Column(TypeName = "date")]
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
        [Column(TypeName = "numeric")]
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
        [NotMapped]
        public NationalTypeProcess TypeProcess { get; set; }

        /// <summary>
        /// Gets or sets the calculation status.
        /// </summary>
        /// <value>
        /// The calculation status.
        /// </value>
        public virtual CalculationStatus CalculationStatus { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status.
        /// </summary>
        /// <value>
        /// The confirmation status.
        /// </value>
        public virtual ConfirmationStatus ConfirmationStatus { get; set; }

        /// <summary>
        /// Gets or sets the status process.
        /// </summary>
        /// <value>
        /// The status process.
        /// </value>
        public virtual StatusProcess StatusProcess { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel costs.
        /// </summary>
        /// <value>
        /// The national jet fuel costs.
        /// </value>
        public virtual IList<NationalJetFuelCost> NationalJetFuelCosts { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel log errors.
        /// </summary>
        /// <value>
        /// The national jet fuel log errors.
        /// </value>
        public virtual IList<NationalJetFuelLogError> NationalJetFuelLogErrors { get; set; }


    }
}
