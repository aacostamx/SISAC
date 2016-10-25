//------------------------------------------------------------------------
// <copyright file="AirportGroupVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Airport Group VO
    /// </summary>
    public class AirportGroupVO
    {
        /// <summary>
        /// Aiport Group Code
        /// </summary>
        [Display(Name = "AirportGroupCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax8")]
        public string AirportGroupCode { get; set; }

        /// <summary>
        /// Airport Group Name
        /// </summary>
        [Display(Name = "AirportGroupName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(40, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax40")]
        public string AirportGroupName { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [NotMapped]
        public bool Status { get; set; }
    }
}