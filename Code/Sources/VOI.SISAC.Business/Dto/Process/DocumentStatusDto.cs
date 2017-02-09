//------------------------------------------------------------------------
// <copyright file="DocumentStatusDto.cs" company="AACOSTA">
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
    /// DocumentStatusDto Class
    /// </summary>
    public class DocumentStatusDto
    {
        /// <summary>
        /// Document Code
        /// </summary>
        public string DocumentStatusCode { get; set; }

        /// <summary>
        /// Document Name
        /// </summary>
        public string DocumentStatusName { get; set; }
    }
}
