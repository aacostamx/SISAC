//------------------------------------------------------------------------
// <copyright file="StatusProcess.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// StatusProcess
    /// </summary>
    [Table("Process.StatusProcess")]
    public partial class StatusProcess
    {
        /// <summary>
        /// StatusProcess
        /// </summary>
        public StatusProcess()
        {
            JetFuelProcesses = new List<JetFuelProcess>();
        }

        /// <summary>
        /// StatusProcessCode
        /// </summary>
        [Key]
        [StringLength(5)]
        public string StatusProcessCode { get; set; }

        /// <summary>
        /// StatusProcessName
        /// </summary>
        [Required]
        [StringLength(50)]
        public string StatusProcessName { get; set; }

        /// <summary>
        /// JetFuelProcesses
        /// </summary>
        public virtual IList<JetFuelProcess> JetFuelProcesses { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel processes.
        /// </summary>
        /// <value>
        /// The national jet fuel processes.
        /// </value>
        public virtual IList<NationalJetFuelProcess> NationalJetFuelProcesses { get; set; }

    }
}
