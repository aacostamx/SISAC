//------------------------------------------------------------------------
// <copyright file="ServiceDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Business.Dto.Catalogs;

    /// <summary>
    /// Data Transfer Objet for Services
    /// </summary>
    public class ServiceDto
    {
        /// <summary>
        /// Get or Set Service Code
        /// </summary>
        public string ServiceCode { get; set; }
        /// <summary>
        /// Get or Set Service Name
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// Get or Set the estatus Service
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// JetFuelTicket
        /// </summary>
        public virtual ICollection<JetFuelTicketDto> JetFuelTickets { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public ICollection<ReconciliationToleranceDto> ReconciliationTolerances { get; set; }
    }
}
