//------------------------------------------------------------------------
// <copyright file="AirplaneType.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// AirplaneType Entity
    /// </summary>
    public partial class AirplaneType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneType"/> class.
        /// </summary>
        public AirplaneType()
        {
            this.Airplanes = new HashSet<Airplane>();
        }

        #region Properties for columns
        /// <summary>
        /// Primary Key.
        /// Gets or sets the airplane model.
        /// </summary>
        /// <value>
        /// The airplane model.
        /// </value>
        public string AirplaneModel { get; set; }

        /// <summary>
        /// Foreing Key.
        /// Gets or sets the compartment type code.
        /// </summary>
        /// <value>
        /// The compartment type code.
        /// </value>
        public string CompartmentTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the maximum takeoff weight.
        /// </summary>
        /// <value>
        /// The maximum takeoff weight.
        /// </value>
        public decimal MaximumTakeoffWeight { get; set; }

        /// <summary>
        /// Gets or sets the weight in pounds.
        /// </summary>
        /// <value>
        /// The weight in pounds.
        /// </value>
        public decimal WeightInPound { get; set; }

        /// <summary>
        /// Gets or sets the weight in tonnes.
        /// </summary>
        /// <value>
        /// The weight in tonnes.
        /// </value>
        public decimal WeightInTonnes { get; set; }

        /// <summary>
        /// Gets or sets the empty operating weight.
        /// </summary>
        /// <value>
        /// The empty operating weight.
        /// </value>
        public decimal EmptyOperatingWeight { get; set; }

        /// <summary>
        /// Gets or sets the filming maximum weight.
        /// </summary>
        /// <value>
        /// The filming maximum weight.
        /// </value>
        public decimal FilmingMaximumWeight { get; set; }

        /// <summary>
        /// Gets or sets the takeoff weight in tonnes.
        /// </summary>
        /// <value>
        /// The takeoff weight in tonnes.
        /// </value>
        public decimal TakeoffWeightInTonnes { get; set; }

        /// <summary>
        /// Gets or sets the group weight.
        /// </summary>
        /// <value>
        /// The group weight.
        /// </value>
        public decimal GroupWeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum landing weight.
        /// </summary>
        /// <value>
        /// The maximum landing weight.
        /// </value>
        public decimal MaximumLandingWeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum zero fuel weight.
        /// </summary>
        /// <value>
        /// The maximum zero fuel weight.
        /// </value>
        public decimal MaximumZeroFuelWeight { get; set; }

        /// <summary>
        /// Gets or sets the passenger capacity.
        /// </summary>
        /// <value>
        /// The passenger capacity.
        /// </value>
        public int PassengerCapacity { get; set; }

        /// <summary>
        /// Gets or sets the crew capacity.
        /// </summary>
        /// <value>
        /// The crew capacity.
        /// </value>
        public int CrewCapacity { get; set; }

        /// <summary>
        /// Gets or sets the magnitude.
        /// </summary>
        /// <value>
        /// The magnitude.
        /// </value>
        public decimal Magnitude { get; set; }

        /// <summary>
        /// Gets or sets the fuel in liters.
        /// </summary>
        /// <value>
        /// The fuel in liters.
        /// </value>
        public decimal FuelInLiters { get; set; }

        /// <summary>
        /// Gets or sets the fuel in kg.
        /// </summary>
        /// <value>
        /// The fuel in kg.
        /// </value>
        public decimal FuelInKg { get; set; }

        /// <summary>
        /// Gets or sets the fuel in gallon.
        /// </summary>
        /// <value>
        /// The fuel in gallon.
        /// </value>
        public decimal FuelInGallon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AirplaneType"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
        #endregion

        #region Properties for relations
        /// <summary>
        /// Gets or sets the airplanes.
        /// </summary>
        /// <value>
        /// The airplanes.
        /// </value>
        public virtual ICollection<Airplane> Airplanes { get; set; }
        #endregion

        /// <summary>
        /// Gets or sets the type of the compartment.
        /// </summary>
        /// <value>
        /// The type of the compartment.
        /// </value>
        public virtual CompartmentType CompartmentType { get; set; }
    }
}
