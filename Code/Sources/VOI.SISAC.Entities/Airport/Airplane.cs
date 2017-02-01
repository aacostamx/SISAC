//------------------------------------------------------------------------
// <copyright file="Airplane.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System;
    using System.Collections.Generic;
    using Itineraries;

    /// <summary>
    /// Airplane entity
    /// </summary>
    public partial class Airplane
    {
        #region Properties for columns
        /// <summary>
        /// Primary Key.
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Foreing Key for AirplaneType.
        /// Gets or sets the airplane model.
        /// </summary>
        /// <value>
        /// The airplane model.
        /// </value>
        public string AirplaneModel { get; set; }

        /// <summary>
        /// Foreing Key for Airline.
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airplane model.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the maximum takeoff weight.
        /// </summary>
        /// <value>
        /// The maximum takeoff weight.
        /// </value>
        public decimal MaximumTakeoffWeight { get; set; }

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        /// <value>
        /// The serial number.
        /// </value>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the weight in pound.
        /// </summary>
        /// <value>
        /// The weight in pound.
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
        /// Gets or sets the take off weight in tonnes.
        /// </summary>
        /// <value>
        /// The take off weight in tonnes.
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
        /// Gets or sets a value indicating whether this <see cref="Airplane"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }
        #endregion

        #region Properties for relationships
        /// <summary>
        /// Gets or sets the type of the airplane.
        /// Relation with AirplaneType.
        /// </summary>
        /// <value>
        /// The type of the airplane.
        /// </value>
        public virtual AirplaneType AirplaneType { get; set; }

        /// <summary>
        /// Gets or sets the drinking waters.
        /// Relationship with DrinkingWater.
        /// </summary>
        /// <value>
        /// The drinking waters.
        /// </value>
        public virtual ICollection<DrinkingWater> DrinkingWaters { get; set; }

        /// <summary>
        /// Gets or sets the itinerary.
        /// </summary>
        /// <value>
        /// The itinerary.
        /// </value>
        public virtual ICollection<Itinerary> Itinerary { get; set; }

        /// <summary>
        /// Gets or sets the airline.
        /// </summary>
        /// <value>
        /// The airline.
        /// </value>
        public virtual Airline Airline { get; set; }
        #endregion
    }
}