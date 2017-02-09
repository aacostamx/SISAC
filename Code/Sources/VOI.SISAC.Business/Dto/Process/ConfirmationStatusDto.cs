//------------------------------------------------------------------------
// <copyright file="ConfirmationStatusDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using System.Collections.Generic;

    public class ConfirmationStatusDto
    {
        /// <summary>
        /// ConfirmationStatus Constructor
        /// </summary>
        public ConfirmationStatusDto()
        {
            JetFuelProcesses = new List<JetFuelProcessDto>();
        }

        /// <summary>
        /// ConfirmationStatusID
        /// </summary>
        public string ConfirmationStatusCode { get; set; }

        /// <summary>
        /// ConfirmationStatusName
        /// </summary>
        public string ConfirmationStatusName { get; set; }

        /// <summary>
        /// JetFuelProcesses
        /// </summary>
        public IList<JetFuelProcessDto> JetFuelProcesses { get; set; }
    }
}
