//------------------------------------------------------------------------
// <copyright file="OperationTypeDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
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
    public class OperationTypeDto
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
