//------------------------------------------------------------------------
// <copyright file="StatusOnBoardDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// 
    /// </summary>
    public class StatusOnBoardVO
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