//------------------------------------------------------------------------
// <copyright file="ProcedureCalculationVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//--------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    /// <summary>
    /// Class Operation Type VO
    /// </summary>
    public class ProcedureCalculationVO
    {
        /// <summary>
        /// Gets or sets the procedure code.
        /// </summary>
        /// <value>
        /// The procedure code.
        /// </value>
        public string ProcedureCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the procedure.
        /// </summary>
        /// <value>
        /// The name of the procedure.
        /// </value>
        public string ProcedureName { get; set; }

        /// <summary>
        /// Gets or sets the name of the procedure database.
        /// </summary>
        /// <value>
        /// The name of the procedure database.
        /// </value>
        public string ProcedureDBName { get; set; }
    }
}