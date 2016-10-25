//------------------------------------------------------------------------
// <copyright file="DrinkingWaterVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Drinking Water View Object
    /// </summary>
    public class DrinkingWaterVO
    {
        /// <summary>
        /// Gets or sets the drinking water.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The drinking water.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public long DrinkingWaterId { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax8")]
        [Display(Name = "EquipmentNumber", ResourceType = typeof(Resources.Resource))]
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the drinking water.
        /// </summary>
        /// <value>
        /// The name of the drinking water.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(100, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax100")]
        [Display(Name = "Description", ResourceType = typeof(Resources.Resource))]
        public string DrinkingWaterName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Quantity", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airplane.
        /// Relationship with Airplane entity.
        /// </summary>
        /// <value>
        /// The airplanes.
        /// </value>
        public AirplaneVO Airplane { get; set; }
    }
}
