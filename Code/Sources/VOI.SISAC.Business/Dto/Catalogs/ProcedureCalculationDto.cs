//------------------------------------------------------------------------
// <copyright file="ProcedureCalculationDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Operation type Data transfer object
    /// </summary>
    public class ProcedureCalculationDto
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
