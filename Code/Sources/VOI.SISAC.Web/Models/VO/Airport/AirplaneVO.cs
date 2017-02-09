//------------------------------------------------------------------------
// <copyright file="AirplaneVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Airport
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// View model for airplane
    /// </summary>
    public class AirplaneVO
    {
        #region Properties for columns
        /// <summary>
        /// Gets or sets the equipment number.
        /// Primary Key.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "EquipmentNumber", ResourceType = typeof(Resources.Resource))]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax8")]
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the airplane model.
        /// Foreign Key for AirplaneType.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "AirplaneModel", ResourceType = typeof(Resources.Resource))]
        [StringLength(12, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax12")]
        public string AirplaneModel { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// Foreign Key for Airline code.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(2, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMin2")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the serial number.
        /// Foreign Key for AirplaneType.
        /// </summary>
        [Display(Name = "SerialNumber", ResourceType = typeof(Resources.Resource))]
        [StringLength(20, ErrorMessageResourceType = typeof(Resources.Resource),
                          ErrorMessageResourceName = "LengthMax20")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the maximum takeoff weight.
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [Display(Name = "MaximumTakeoffWeight", ResourceType = typeof(Resources.Resource))]
        //[DisplayFormat(ApplyFormatInEditMode = true, 
        //    DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        public decimal MaximumTakeoffWeight { get; set; }

        /// <summary>
        /// Gets or sets the weight in pound.
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
        /// Gets or sets the take off weight in tonnes.
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
        /// Gets or sets a value indicating whether this is active.
        /// </summary>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }
        #endregion

        #region Properties for relationships
        /////// <summary>
        /////// Gets or sets the type of the airplane.
        /////// Relation with AirplaneType.
        /////// </summary>
        /////// <value>
        /////// The type of the airplane.
        /////// </value>
        ////public virtual AirplaneType AirplaneType { get; set; }

        /////// <summary>
        /////// Gets or sets the drinking waters.
        /////// </summary>
        /////// <value>
        /////// The drinking waters.
        /////// </value>
        ////public IList<DrinkingWaterDto> DrinkingWaters { get; set; }
        #endregion
    }
}