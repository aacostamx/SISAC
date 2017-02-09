//------------------------------------------------------------------------
// <copyright file="StatusProcessJetFuel.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    ///  Static StatusProcessJetFuel Class
    /// </summary>
    public static class StatusProcessJetFuel
    {
        /// <summary>
        /// ERROR
        /// </summary>
        public static string ERROR { get { return "ERR"; } }

        /// <summary>
        /// FINISHED
        /// </summary>
        public static string FINISHED { get { return "FIN"; } }

        /// <summary>
        /// NEW
        /// </summary>
        public static string NEW { get { return "NEW"; } }

        /// <summary>
        /// REVERTED
        /// </summary>
        public static string REVERTED { get { return "REV"; } }

        /// <summary>
        /// RUNING
        /// </summary>
        public static string RUNING { get { return "RUN"; } }

        /// <summary>
        /// CLOSED
        /// </summary>
        public static string CLOSED { get { return "CLO"; } }

        /// <summary>
        /// OPEN
        /// </summary>
        public static string OPEN { get { return "OPE"; } }

        /// <summary>
        /// PENDING
        /// </summary>
        public static string PENDING { get { return "PEN"; } }

    }
}
