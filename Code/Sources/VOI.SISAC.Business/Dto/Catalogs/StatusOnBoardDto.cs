//------------------------------------------------------------------------
// <copyright file="StatusOnBoardDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class StatusOnBoardDto
    {
        /// <summary>
        /// Gets or sets the status on board code.
        /// </summary>
        /// <value>
        /// The status on board code.
        /// </value>
        public string StatusOnBoardCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the status on board.
        /// </summary>
        /// <value>
        /// The name of the status on board.
        /// </value>
        public string StatusOnBoardName { get; set; }
    }
}
