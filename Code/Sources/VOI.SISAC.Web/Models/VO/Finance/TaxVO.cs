//------------------------------------------------------------------------
// <copyright file="TaxVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Data View Object for Tax
    /// </summary>
    public class TaxVO
    {
        /// <summary>
        /// Gets or sets the Tax number.
        /// </summary>
        /// <value>
        /// The tax number.
        /// </value>
        [Display(Name = "TaxCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "TaxCodeMaxLong")]
        public string TaxCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the tax. 
        /// </summary>
        [Display(Name = "TaxName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, MinimumLength = 5, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "TaxNameMaxLong")]
        public string TaxName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this tax is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}