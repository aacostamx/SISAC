//------------------------------------------------------------------------
// <copyright file="ToleranceTypeDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    /// ToleranceTypeDto Class
    /// </summary>
    public class ToleranceTypeDto
    {

        /// <summary>
        /// Gets or sets the tolerance type code.
        /// </summary>
        /// <value>
        /// The tolerance type code.
        /// </value>
        public string ToleranceTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the tolerance type.
        /// </summary>
        /// <value>
        /// The name of the tolerance type.
        /// </value>
        public string ToleranceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public ICollection<ReconciliationToleranceDto> ReconciliationTolerances { get; set; }
    }
}
