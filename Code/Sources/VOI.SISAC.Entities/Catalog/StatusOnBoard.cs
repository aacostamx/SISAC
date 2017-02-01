//------------------------------------------------------------------------
// <copyright file="StatusOnBoard.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class Status On Board
    /// </summary>
    public class StatusOnBoard
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
