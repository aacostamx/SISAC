//------------------------------------------------------------------------
// <copyright file="FunctionalAreaVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Catalog
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Functional Area VO
    /// </summary>
    public class FunctionalAreaVO
    {
        /// <summary>
        /// Gets or sets the FunctionalAreaID.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The FunctionalAreaID.
        /// </value>
        [Required]
        [Display(Name = "FunctionalAreaID", ResourceType = typeof(Resources.Resource))]
        public long FunctionalAreaID { get; set; }

        /// <summary>
        /// Gets or sets the name of the FunctionalAreaName.
        /// </summary>
        /// <value>
        /// The desciption of the FunctionalAreaName.
        /// </value>
        [Display(Name = "FunctionalAreaName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMaxHundred")]
        public string FunctionalAreaName { get; set; }

        /// <summary>
        /// Gets or sets the name of the FunctionalAreaDescription.
        /// </summary>
        /// <value>
        /// The desciption of the FunctionalAreaDescription.
        /// </value>
        [Display(Name = "FunctionalAreaDescription", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMaxHundred")]
        public string FunctionalAreaDescription { get; set; }
       
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}