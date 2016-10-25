//------------------------------------------------------------------------
// <copyright file="AirplaneTypeVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Airplane View Object
    /// </summary>
    public class AirplaneTypeVO
    {
        #region Properties for columns
        /// <summary>
        /// Gets or sets the airplane model.
        /// Foreign Key for AirplaneType.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "AirplaneModel", ResourceType = typeof(Resources.Resource))]
        [StringLength(12, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax12")]
        public string AirplaneModel { get; set; }

        /// <summary>
        /// Gets or sets the compartment type code.
        /// Foreign Key.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "CompartmentTypeCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax8")]
        public string CompartmentTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the maximum takeoff weight.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "MaximumTakeoffWeight", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal MaximumTakeoffWeight { get; set; }

        /// <summary>
        /// Gets or sets the weight in pounds.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "WeightInPound", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal WeightInPound { get; set; }

        /// <summary>
        /// Gets or sets the weight in tonnes.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "WeightInTonnes", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal WeightInTonnes { get; set; }

        /// <summary>
        /// Gets or sets the empty operating weight.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "EmptyOperatingWeight", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal EmptyOperatingWeight { get; set; }

        /// <summary>
        /// Gets or sets the filming maximum weight.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "FilmingMaximumWeight", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal FilmingMaximumWeight { get; set; }

        /// <summary>
        /// Gets or sets the takeoff weight in tonnes.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "TakeoffWeightInTonnes", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal TakeoffWeightInTonnes { get; set; }

        /// <summary>
        /// Gets or sets the group weight.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "GroupWeight", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal GroupWeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum landing weight.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "MaximumLandingWeight", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal MaximumLandingWeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum zero fuel weight.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "MaximumZeroFuelWeight", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal MaximumZeroFuelWeight { get; set; }

        /// <summary>
        /// Gets or sets the passenger capacity.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "PassengerCapacity", ResourceType = typeof(Resources.Resource))]
        public int PassengerCapacity { get; set; }

        /// <summary>
        /// Gets or sets the crew capacity.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "CrewCapacity", ResourceType = typeof(Resources.Resource))]
        public int CrewCapacity { get; set; }

        /// <summary>
        /// Gets or sets the magnitude.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "Magnitude", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal Magnitude { get; set; }

        /// <summary>
        /// Gets or sets the fuel in liters.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "FuelInLiters", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true, 
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal FuelInLiters { get; set; }

        /// <summary>
        /// Gets or sets the fuel in kg.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "FuelInKg", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal FuelInKg { get; set; }

        /// <summary>
        /// Gets or sets the fuel in gallon.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "FuelInGallon", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal FuelInGallon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is status.
        /// </summary>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airplanes.
        /// </summary>
        /// <value>
        /// The airplanes.
        /// </value>
        public IList<AirplaneVO> Airplanes { get; set; }
        #endregion
    }
}
