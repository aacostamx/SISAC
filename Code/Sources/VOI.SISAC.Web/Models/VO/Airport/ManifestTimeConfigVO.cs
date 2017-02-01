//------------------------------------------------------------------------
// <copyright file="ManifestTimeConfigVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// MAnifest Time Class
    /// </summary>
    public class ManifestTimeConfigVO
    {
        /// <summary>
        /// Manifest ID
        /// </summary>
        [Display(Name = "ManifestTimeConfigID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [Range(0, int.MaxValue)]
        public long ManifestTimeConfigID { get; set; }

        /// <summary>
        /// Airline Code
        /// </summary>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Arrive Min
        /// </summary>
        [Display(Name = "ArrivalMinutesMin", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [Column(TypeName = "numeric")]
        [Range(0, double.MaxValue)]
        public decimal ArrivalMinutesMin { get; set; }

        /// <summary>
        /// Arrive Max
        /// </summary>
        /// </summary>
        [Display(Name = "ArrivalMinutesMax", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [Column(TypeName = "numeric")]
        [Range(0, double.MaxValue)]
        public decimal ArrivalMinutesMax { get; set; }

        /// <summary>
        /// Departure Min
        /// </summary>
        [Display(Name = "DepartureMinutesMin", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [Column(TypeName = "numeric")]
        [Range(0, double.MaxValue)]
        public decimal DepartureMinutesMin { get; set; }

        /// <summary>
        /// Departure Max
        /// </summary>
        [Display(Name = "DepartureMinutesMax", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [Column(TypeName = "numeric")]
        [Range(0, double.MaxValue)]
        public decimal DepartureMinutesMax { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [NotMapped]
        public bool Status { get; set; }
    }
}