//------------------------------------------------------------------------
// <copyright file="RemittanceStatusDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// RemittanceStatusDto Class
    /// </summary>
    public class RemittanceStatusDto
    {
        /// <summary>
        /// Remittance Code
        /// </summary>
        public string RemittanceStatusCode { get; set; }

        /// <summary>
        /// Remittance Name
        /// </summary>
        public string RemittanceStatusName { get; set; }
    }
}
