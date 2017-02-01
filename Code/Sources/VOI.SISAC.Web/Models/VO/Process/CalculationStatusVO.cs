//------------------------------------------------------------------------
// <copyright file="CalculationStatusVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// CalculationStatusVO Class
    /// </summary>
    public class CalculationStatusVO
    {
        /// <summary>
        /// CalculationStatusVO constructor
        /// </summary>
        public CalculationStatusVO()
        {
            JetFuelProcesses = new List<JetFuelProcessVO>();
        }

        /// <summary>
        /// CalculationStatusID
        /// </summary>
        [StringLength(5)]
        public string CalculationStatusCode { get; set; }

        /// <summary>
        /// CalculationStatusName
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CalculationStatusName { get; set; }

        /// <summary>
        /// Jet Fuel Processes
        /// </summary>
        public IList<JetFuelProcessVO> JetFuelProcesses { get; set; }
    }
}