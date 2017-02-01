//------------------------------------------------------------------------
// <copyright file="OperationTypeVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    /// <summary>
    /// Class Operation Type VO
    /// </summary>
    public class OperationTypeVO
    {
        /// <summary>
        /// Gets or sets the operation type identifier.
        /// </summary>
        /// <value>
        /// The operation type identifier.
        /// </value>
        public int OperationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation.
        /// </summary>
        /// <value>
        /// The name of the operation.
        /// </value>
        public string OperationName { get; set; }
    }
}