//------------------------------------------------------------------------
// <copyright file="DocumentStatusVO.cs" company="Volaris">
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
    /// DocumentStatusVO class
    /// </summary>
    public class DocumentStatusVO
    {
        /// <summary>
        /// Gets or sets the document status code.
        /// </summary>
        /// <value>
        /// The document status code.
        /// </value>
        public string DocumentStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the document status.
        /// </summary>
        /// <value>
        /// The name of the document status.
        /// </value>
        public string DocumentStatusName { get; set; }
    }
}