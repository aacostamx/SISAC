//------------------------------------------------------------------------
// <copyright file="FuelConceptVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Fuel Concept VO
    /// </summary>
    public class FuelConceptVO
    {
        /// <summary>
        /// Gets or sets the Fuel Concept.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Fuel Concept.
        /// </value>
        [Required]
        [Display(Name = "FuelConceptID", ResourceType = typeof(Resources.Resource))]
        public long FuelConceptID { get; set; }

        /// <summary>
        /// Gets or sets the name of the FuelConceptName.
        /// </summary>
        /// <value>
        /// The desciption of the FuelConceptName.
        /// </value>
        [Display(Name = "FuelConceptName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(60, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax60")]
        public string FuelConceptName { get; set; }
      
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
    }
}