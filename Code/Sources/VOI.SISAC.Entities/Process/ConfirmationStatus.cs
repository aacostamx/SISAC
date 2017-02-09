//------------------------------------------------------------------------
// <copyright file="ConfirmationStatus.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ConfirmationStatus
    /// </summary>
    [Table("Process.ConfirmationStatus")]
    public partial class ConfirmationStatus
    {
        /// <summary>
        /// ConfirmationStatus Constructor
        /// </summary>
        public ConfirmationStatus()
        {
            JetFuelProcesses = new List<JetFuelProcess>();
        }

        /// <summary>
        /// ConfirmationStatusCode
        /// </summary>
        [Key]
        [StringLength(5)]
        public string ConfirmationStatusCode { get; set; }

        /// <summary>
        /// ConfirmationStatusName
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ConfirmationStatusName { get; set; }

        /// <summary>
        /// JetFuelProcesses
        /// </summary>
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
