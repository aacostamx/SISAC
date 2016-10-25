//------------------------------------------------------------------------
// <copyright file="NationalReconcileTypeProcessVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// NationalReconcileTypeProcessVO class
    /// </summary>
    public enum NationalReconcileTypeProcessVO
    {
        /// <summary>
        /// The national reconcile process all
        /// </summary>
        NationalReconcileProcessAll = 1,

        /// <summary>
        /// The national reconcile process pending
        /// </summary>
        NationalReconcileProcessPending = 2,

        /// <summary>
        /// The national reconcile process revert
        /// </summary>
        NationalReconcileProcessRevert = 3
    }
}