//------------------------------------------------------------------------
// <copyright file="PolicyResponseInformation.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Policy response information
    /// </summary>
    [DataContract(Name="T_DOCUMENTO")]
    public class PolicyResponseInformation
    {
        /// <summary>
        /// Gets or sets the idreg.
        /// </summary>
        /// <value>
        /// The idreg.
        /// </value>
        [DataMember]
        public string IDREG { get; set; }

        /// <summary>
        /// Gets or sets the belnr.
        /// </summary>
        /// <value>
        /// The belnr.
        /// </value>
        [DataMember]
        public string BELNR { get; set; }

        /// <summary>
        /// Gets or sets the menv.
        /// </summary>
        /// <value>
        /// The menv.
        /// </value>
        [DataMember]
        public string MENV { get; set; }

        /// <summary>
        /// Gets or sets the msgid.
        /// </summary>
        /// <value>
        /// The msgid.
        /// </value>
        [DataMember]
        public string MSGID { get; set; }

        /// <summary>
        /// Gets or sets the rfclog.
        /// </summary>
        /// <value>
        /// The rfclog.
        /// </value>
        [DataMember]
        public string RFCLOG { get; set; }

        /// <summary>
        /// Gets or sets the XBLNR.
        /// </summary>
        /// <value>
        /// The XBLNR.
        /// </value>
        [DataMember]
        public string XBLNR { get; set; }
    }
}