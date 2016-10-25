//------------------------------------------------------------------------
// <copyright file="GpuVo.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// GpuVO
    /// </summary>
    public class GpuVO
    {
        /// <summary>
        /// Gpu Code
        /// </summary>
        [Display(Name = "GpuCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax50")]
        public string GpuCode { get; set; }

        /// <summary>
        /// Gpu Name
        /// </summary>
        [Display(Name = "GpuName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(150, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax150")]
        public string GpuName { get; set; }

        /// <summary>
        /// Station Code
        /// </summary>
        [Display(Name = "GPULocation", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string StationCode { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [NotMapped]
        public bool Status { get; set; }

        /// <summary>
        /// Airport
        /// </summary>
        public AirportVO Airport { get; set; }
    }
}