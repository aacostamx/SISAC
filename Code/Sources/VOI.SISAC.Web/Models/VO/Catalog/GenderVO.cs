//------------------------------------------------------------------------
// <copyright file="GenderVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//---------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Class Gender VO
    /// </summary>
    public class GenderVO
    {
        /// <summary>
        /// Gets or sets the gender code.
        /// </summary>
        /// <value>
        /// The gender code.
        /// </value>
        [StringLength(1, ErrorMessage = "{0} must be 1 characters"), Required(ErrorMessage = "Missing {0}")]
        [DisplayName("Gender Code")]
        public string GenderCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the gender.
        /// </summary>
        /// <value>
        /// The name of the gender.
        /// </value>
        [StringLength(10, ErrorMessage = "{0} must be 10 characters"), Required(ErrorMessage = "Missing {0}")]
        [DisplayName("Gender Name")]
        public string GenderName { get; set; }
    }
}