//------------------------------------------------------------------------
// <copyright file="ServiceVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
 
namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VOI.SISAC.Web.Models.VO.Catalog;

    /// <summary>
    /// Services View Object
    /// </summary>
    public class ServiceVO
    {
        /// <summary>
        /// Get or Set Service Code
        /// </summary>
        [Display(Name ="ServiceCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(12, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "ServiceCodeMaxLong")]
        public string ServiceCode { get; set; }
        /// <summary>
        /// Get or Set Service Name
        /// </summary>
        [Display(Name = "ServiceName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(150, MinimumLength = 5, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "ServiceNameMaxLong")]
        public string ServiceName { get; set; }
        /// <summary>
        /// Get or Set the estatus Service
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// JetFuelTicket
        /// </summary>
        public virtual ICollection<JetFuelTicketVO> JetFuelTickets { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation tolerances.
        /// </summary>
        /// <value>
        /// The reconciliation tolerances.
        /// </value>
        public ICollection<ReconciliationToleranceVO> ReconciliationTolerances { get; set; }
    }
}