//------------------------------------------------------------------------
// <copyright file="AirportVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using VOI.SISAC.Web.Models.VO.Catalog;

    /// <summary>
    /// Airport View Object
    /// </summary>
    public class AirportVO
    {
        /// <summary>
        /// Station code
        /// </summary>
        [Display(Name = "StationCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMin3")]
        public string StationCode { get; set; }


        /// <summary>
        /// Station name
        /// </summary>
        [Display(Name = "StationName", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax100")]
        public string StationName { get; set; }

        /// <summary>
        /// Country code
        /// </summary>
        [Display(Name = "CountryCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Opening time
        /// </summary>
        [Display(Name = "OpeningTime", ResourceType = typeof(Resources.Resource))]
        //[DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}"/*, ApplyFormatInEditMode = true*/)]
        public TimeSpan? OpeningTime { get; set; }

        /// <summary>
        /// Closing time
        /// </summary>
        [Display(Name = "ClosingTime", ResourceType = typeof(Resources.Resource))]
        //[DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}"/*, ApplyFormatInEditMode = true*/)]
        public TimeSpan? ClosingTime { get; set; }

        /// <summary>
        /// Airport group
        /// </summary>
        [Display(Name = "AirportGroupCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMax8")]
        public string AirportGroupCode { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [NotMapped]
        public bool Status { get; set; }

        /// <summary>
        /// Country relation
        /// </summary>
        public CountryVO Country { get; set; }
        /// <summary>
        /// Gpu relation
        /// </summary>
        public ICollection<GpuVO> Gpu { get; set; }

        /// <summary>
        /// Constructor AirportVO
        /// </summary>
        public AirportVO()
        {
            Gpu = new List<GpuVO>();
        }

    }
}