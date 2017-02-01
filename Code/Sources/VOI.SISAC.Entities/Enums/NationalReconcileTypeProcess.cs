//------------------------------------------------------------------------
// <copyright file="NationalReconcileTypeProcess.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------


namespace VOI.SISAC.Entities.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    /// NationalReconcileTypeProcess class
    /// </summary>
    public enum NationalReconcileTypeProcess
    {
        /// <summary>
        /// The national jet fuel process all
        /// </summary>
        NationalReconcileProcessAll = 1,

        /// <summary>
        /// The national jet fuel process pending
        /// </summary>
        NationalReconcileProcessPending = 2,

        /// <summary>
        /// The national jet fuel process revert/
        /// </summary>
        NationalReconcileProcessRevert = 3
    }
}