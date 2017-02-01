//------------------------------------------------------------------------
// <copyright file="RemittanceStatusDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// RemittanceStatusVO Class
    /// </summary>
    public class RemittanceStatusVO
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