//------------------------------------------------------------------------
// <copyright file="LiabilityAccountVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Liability Account VO
    /// </summary>
    public class LiabilityAccountVO
    {
        /// <summary>
        /// Gets or sets the LiabilityAccountNumber.
        /// </summary>
        /// <value>
        /// The currency's number.
        /// </value>
        [Display(Name = "LiabilityAccountNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(8, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LiabilityAccountNumberMaxMinLong")]
        public string LiabilityAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the LiabilityAccountName.
        /// </summary>
        /// <value>
        /// The desciption of the LiabilityAccountName.
        /// </value>
        [Display(Name = "LiabilityAccountName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax50")]
        public string LiabilityAccountName { get; set; }

     
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}