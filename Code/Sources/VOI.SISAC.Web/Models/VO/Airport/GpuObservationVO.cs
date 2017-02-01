//------------------------------------------------------------------------
// <copyright file="GpuObservationVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Gpu Observartion
    /// </summary>
    public class GpuObservationVO
    {
        /// <summary>
        /// Gpu Observation Code
        /// </summary>
        [Display(Name = "GpuObservationCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax10")]
        public string GpuObservationCode { get; set; }

        /// <summary>
        /// Gpu Obsevation Name
        /// </summary>
        [Display(Name = "GpuObservationCodeName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(150, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax150")]
        public string GpuObservationCodeName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [NotMapped]
        public bool Status { get; set; }
    }
}