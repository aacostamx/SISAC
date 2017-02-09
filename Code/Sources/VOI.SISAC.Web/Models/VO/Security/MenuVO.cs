//-----------------------------------------------------------------------
// <copyright file="MenuVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Security
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MenuVO
    {
        /// <summary>
        /// Gets or sets the menu code.
        /// </summary>
        /// <value>
        /// The menu code.
        /// </value>
        public string MenuCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the menu.
        /// </summary>
        /// <value>
        /// The name of the menu.
        /// </value>
        public string MenuName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is parent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is parent; otherwise, <c>false</c>.
        /// </value>
        public bool IsParent { get; set; }

        /// <summary>
        /// Gets or sets the parent code.
        /// </summary>
        /// <value>
        /// The parent code.
        /// </value>
        public string ParentCode { get; set; }

        /// <summary>
        /// Gets or sets the module.
        /// </summary>
        /// <value>
        /// The module.
        /// </value>
        public ICollection<ModuleVO> Module { get; set; }
    }
}