//------------------------------------------------------------------------
// <copyright file="CalculationStatusDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using System.Collections.Generic;

    public class CalculationStatusDto
    {
        /// <summary>
        /// CalculationStatus constructor
        /// </summary>
        public CalculationStatusDto()
        {
            JetFuelProcesses = new List<JetFuelProcessDto>();
        }

        /// <summary>
        /// CalculationStatusID
        /// </summary>
        public string CalculationStatusCode { get; set; }

        /// <summary>
        /// CalculationStatusName
        /// </summary>
        public string CalculationStatusName { get; set; }

        /// <summary>
        /// Jet Fuel Processes
        /// </summary>
        public IList<JetFuelProcessDto> JetFuelProcesses { get; set; }
    }
}
