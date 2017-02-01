//------------------------------------------------------------------------
// <copyright file="StatusProcessDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using System.Collections.Generic;

    public class StatusProcessDto
    {
        /// <summary>
        /// StatusProcess
        /// </summary>
        public StatusProcessDto()
        {
            JetFuelProcesses = new List<JetFuelProcessDto>();
        }

        /// <summary>
        /// StatusProcessID
        /// </summary>
        public string StatusProcessCode { get; set; }

        /// <summary>
        /// StatusProcessName
        /// </summary>
        public string StatusProcessName { get; set; }

        /// <summary>
        /// JetFuelProcesses
        /// </summary>
        public IList<JetFuelProcessDto> JetFuelProcesses { get; set; }
    }
}
