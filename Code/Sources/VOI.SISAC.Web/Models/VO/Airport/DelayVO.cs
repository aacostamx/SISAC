//------------------------------------------------------------------------
// <copyright file="DelayVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using Catalog;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Delay VO
    /// </summary>
    public class DelayVO
    {
        /// <summary>
        /// Delay Code
        /// </summary>
        [Display(Name = "DelayCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(2, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax2")]
        public string DelayCode { get; set; }

        /// <summary>
        /// DelayName
        /// </summary>
        [Display(Name = "DelayName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(250, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax250")]
        public string DelayName { get; set; }

        /// <summary>
        /// Funtional Area ID
        /// </summary>
        [Display(Name = "FunctionalAreaID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public long FunctionalAreaID { get; set; }

        /// <summary>
        /// Under Control
        /// </summary>
        [Display(Name = "UnderControl", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public bool UnderControl { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [NotMapped]
        public bool Status { get; set; }

        /// <summary>
        /// Functional Area
        /// </summary>
        public FunctionalAreaVO FunctionalArea { get; set; }

        /// <summary>
        /// Delay Constructor
        /// </summary>
        public DelayVO()
        {
            FunctionalArea = new FunctionalAreaVO();
        }

    }
}