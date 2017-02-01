//------------------------------------------------------------------------
// <copyright file="ToleranceTypeVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace VOI.SISAC.Web.Models.VO.Catalog
{

    /// <summary>
    /// ToleranceTypeVO Class
    /// </summary>
    public class ToleranceTypeVO
    {
        /// <summary>
        /// Gets or sets the tolerance type code.
        /// </summary>
        /// <value>
        /// The tolerance type code.
        /// </value>
        [Display(Name = "ToleranceTypeCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public string ToleranceTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the tolerance type.
        /// </summary>
        /// <value>
        /// The name of the tolerance type.
        /// </value>
        [Display(Name = "ToleranceTypeName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public string ToleranceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public ICollection<ReconciliationToleranceVO> ReconciliationTolerances { get; set; }
    }
}