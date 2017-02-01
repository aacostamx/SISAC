//------------------------------------------------------------------------
// <copyright file="CalculationStatus.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Calculation Status Class
    /// </summary>
    [Table("Process.CalculationStatus")]
    public partial class CalculationStatus
    {
        /// <summary>
        /// CalculationStatus constructor
        /// </summary>
        public CalculationStatus()
        {
            JetFuelProcesses = new List<JetFuelProcess>();
        }

        /// <summary>
        /// CalculationStatusCode
        /// </summary>
        [Key]
        [StringLength(5)]
        public string CalculationStatusCode { get; set; }

        /// <summary>
        /// CalculationStatusName
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CalculationStatusName { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel processes.
        /// </summary>
        /// <value>
        /// The jet fuel processes.
        /// </value>
        public virtual ICollection<JetFuelProcess> JetFuelProcesses { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice controls.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice controls.
        /// </value>
        public virtual ICollection<NationalJetFuelInvoiceControl> NationalJetFuelInvoiceControls { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel processes.
        /// </summary>
        /// <value>
        /// The national jet fuel processes.
        /// </value>
        public virtual ICollection<NationalJetFuelProcess> NationalJetFuelProcesses { get; set; }

    }
}
