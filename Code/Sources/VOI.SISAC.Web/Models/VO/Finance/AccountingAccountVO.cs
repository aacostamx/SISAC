//------------------------------------------------------------------------
// <copyright file="AccountingAccountVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Accounting Account VO
    /// </summary>
    public class AccountingAccountVO
    {
        /// <summary>
        /// Gets or sets the AccountingAccountNumber.
        /// </summary>
        /// <value>
        /// The currency's number.
        /// </value>
        [Display(Name = "AccountingAccountNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(8, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "AccountingAccountNumberMaxMinLong")]
        public string AccountingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the AccountingAccountName.
        /// </summary>
        /// <value>
        /// The desciption of the AccountingAccountName.
        /// </value>
        [Display(Name = "AccountingAccountName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax50")]
        public string AccountingAccountName { get; set; }

        
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]        
        public bool Status { get; set; }
    }
}