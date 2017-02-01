//------------------------------------------------------------------------
// <copyright file="CrewTypeVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//--------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Class CrewTypeVO
    /// </summary>
    public class CrewTypeVO
    {
        /// <summary>
        /// Gets or sets the crew type identifier.
        /// </summary>
        /// <value>
        /// The crew type identifier.
        /// </value>
        [StringLength(4, ErrorMessage = "{0} must be 4 characters"), Required(ErrorMessage = "Missing {0}")]
        [DisplayName("Crew Type ID")]
        public string CrewTypeID { get; set; }

        /// <summary>
        /// Gets or sets the name of the crew type.
        /// </summary>
        /// <value>
        /// The name of the crew type.
        /// </value>
        [StringLength(50, ErrorMessage = "{0} must be 50 characters"), Required(ErrorMessage = "Missing {0}")]
        [DisplayName("Crew Type Name")]
        public string CrewTypeName { get; set; }
    }
}