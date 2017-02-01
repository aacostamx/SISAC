//------------------------------------------------------------------------
// <copyright file="NationalInvoiceVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.Enums
{
    /// <summary>
    /// Type Process VO
    /// </summary>
    public enum NationalInvoiceVO
    {
        /// <summary>
        /// The error or not found
        /// </summary>
        ErrorOrNotFound = 0,

        /// <summary>
        /// The ready sent policy
        /// </summary>
        ReadySentPolicy = 1,

        /// <summary>
        /// The deleted sucess
        /// </summary>
        DeletedSucess = 2,

        /// <summary>
        /// The pending reconciled
        /// </summary>
        PendingReconciled = 3
    }
}