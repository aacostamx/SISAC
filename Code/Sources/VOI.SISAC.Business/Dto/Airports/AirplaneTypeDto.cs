//------------------------------------------------------------------------
// <copyright file="AirplaneTypeDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    /// <summary>
    /// Data transfer object for AirplaneType
    /// </summary>
    public class AirplaneTypeDto
    {
        #region Properties for columns
        /// <summary>
        /// Primary Key.
        /// Gets or sets the airplane model.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name="Modelo de Aeronave")]
        [MaxLength(12)]
        public string AirplaneModel { get; set; }

        /// <summary>
        /// Foreing Key.
        /// Gets or sets the compartment type code.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name="Tipo Compartimiento")]
        [MaxLength(8)]
        public string CompartmentTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the maximum takeoff weight.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name="Peso Máx. Despegue(Kg)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal MaximumTakeoffWeight { get; set; }

        /// <summary>
        /// Gets or sets the weight in pounds.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name="Peso Libras")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal WeightInPound { get; set; }

        /// <summary>
        /// Gets or sets the weight in tonnes.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Peso Toneladas")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal WeightInTonnes { get; set; }

        /// <summary>
        /// Gets or sets the empty operating weight.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Peso Vacío de Operación")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal EmptyOperatingWeight { get; set; }

        /// <summary>
        /// Gets or sets the filming maximum weight.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Peso Máx. Rodaje(Kg)")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal FilmingMaximumWeight { get; set; }

        /// <summary>
        /// Gets or sets the takeoff weight in tonnes.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Peso Despegue Toneladas")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal TakeoffWeightInTonnes { get; set; }

        /// <summary>
        /// Gets or sets the group weight.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Peso Cobro Grupos")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal GroupWeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum landing weight.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Peso Máx. Aterrizaje")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal MaximumLandingWeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum zero fuel weight.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Peso Máx. Cero Combustible")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal MaximumZeroFuelWeight { get; set; }

        /// <summary>
        /// Gets or sets the passenger capacity.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Capacidad Pasajeros")]        
        public int PassengerCapacity { get; set; }

        /// <summary>
        /// Gets or sets the crew capacity.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Capacidad Tripulantes")]
        public int CrewCapacity { get; set; }

        /// <summary>
        /// Gets or sets the magnitude.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Envergadura")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal Magnitude { get; set; }

        /// <summary>
        /// Gets or sets the fuel in liters.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Combustible Litros")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal FuelInLiters { get; set; }

        /// <summary>
        /// Gets or sets the fuel in kg.
        /// </summary>
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Combustible Kilogramos")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage = "Formato inválido")]
        public decimal FuelInKg { get; set; }

        /// <summary>
        /// Gets or sets the fuel in gallon.
        /// </summary>
        [Required(ErrorMessage="Campo requerido")]
        [Display(Name = "Combustible Galones")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+(\.)?([0-9]{1,2})?$", ErrorMessage="Formato inválido")]
        public decimal FuelInGallon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is status.
        /// </summary>
        [Display(Name = "Estado")]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airplanes.
        /// </summary>
        /// <value>
        /// The airplanes.
        /// </value>
        public IList<AirplaneDto> Airplanes { get; set; }
        #endregion
    }
}