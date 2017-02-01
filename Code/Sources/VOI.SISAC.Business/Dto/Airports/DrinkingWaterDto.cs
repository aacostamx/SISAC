//------------------------------------------------------------------------
// <copyright file="DrinkingWaterDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// DrinkingWater Data transfer object
    /// </summary>
    public class DrinkingWaterDto
    {
        /// <summary>
        /// Gets or sets the drinking water.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The drinking water.
        /// </value>
        [Required]
        public long DrinkingWaterId { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// Foreing key.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        [Required]
        [MaxLength(8)]
        [Display(Name="Matrícula")]
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the drinking water.
        /// </summary>
        /// <value>
        /// The name of the drinking water.
        /// </value>
        [Required]
        [MaxLength(100)]
        [Display(Name="Descripción")]
        public string DrinkingWaterName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [Required]
        [Display(Name="Cantidad (Lts)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airplane.
        /// Relationship with Airplane entity.
        /// </summary>
        /// <value>
        /// The airplanes.
        /// </value>
        public AirplaneDto Airplane { get; set; }
    }
}
