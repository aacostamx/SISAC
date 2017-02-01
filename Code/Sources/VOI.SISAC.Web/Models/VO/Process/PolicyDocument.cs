//------------------------------------------------------------------------
// <copyright file="PolicyDocument.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System.Collections.Generic;

    /// <summary>
    /// Policy document format
    /// </summary>
    public class PolicyDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyDocument"/> class.
        /// </summary>
        public PolicyDocument()
        {
            this.T_POLIZA = new List<PolicyRequestInformation>();
            this.T_DOCUMENTO = new List<PolicyResponseInformation>();
        }

        /// <summary>
        /// Gets or sets the format to send.
        /// </summary>
        /// <value>
        /// The t_ poliza.
        /// </value>
        public IList<PolicyRequestInformation> T_POLIZA { get; set; }

        /// <summary>
        /// Gets or sets the format to receive.
        /// </summary>
        /// <value>
        /// The t_ poliza.
        /// </value>
        public IList<PolicyResponseInformation> T_DOCUMENTO { get; set; }
    }
}