//------------------------------------------------------------------------
// <copyright file="CurrencyVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.VO.Catalog;

    /// <summary>
    /// Object for views
    /// </summary>
    public class CurrencyVO
    {
        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency's code.
        /// </value>                
              
        [Display(Name = "CurrencyCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, MinimumLength=3, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMinThree")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency.
        /// </summary>
        /// <value>
        /// The description of the currency.
        /// </value>
        [Display(Name = "CurrencyName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax50")]
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public ICollection<ReconciliationToleranceVO> ReconciliationTolerances { get; set; }
    }
}