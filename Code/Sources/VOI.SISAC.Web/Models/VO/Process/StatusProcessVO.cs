//------------------------------------------------------------------------
// <copyright file="StatusProcessVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// StatusProcessVO Class
    /// </summary>
    public class StatusProcessVO
    {
        /// <summary>
        /// StatusProcessVO Constructor
        /// </summary>
        public StatusProcessVO()
        {
            JetFuelProcesses = new List<JetFuelProcessVO>();
        }

        /// <summary>
        /// StatusProcessID
        /// </summary>
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
        public IList<JetFuelProcessVO> JetFuelProcesses { get; set; }
    }
}