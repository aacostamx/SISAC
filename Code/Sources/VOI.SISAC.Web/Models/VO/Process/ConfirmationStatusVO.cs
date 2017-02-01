//------------------------------------------------------------------------
// <copyright file="ConfirmationStatusVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// ConfirmationStatusVO Class
    /// </summary>
    public class ConfirmationStatusVO
    {
        /// <summary>
        /// ConfirmationStatus Constructor
        /// </summary>
        public ConfirmationStatusVO()
        {
            JetFuelProcesses = new List<JetFuelProcessVO>();
        }

        /// <summary>
        /// ConfirmationStatusID
        /// </summary>
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
        public IList<JetFuelProcessVO> JetFuelProcesses { get; set; }
    }
}