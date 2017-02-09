//------------------------------------------------------------------------
// <copyright file="AirplaneDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    /// <summary>
    /// Data transfer object for Airplane
    /// </summary>
    public class AirplaneDto
    {
        #region Properties for columns
        /// <summary>
        /// Gets or sets the equipment number.
        /// Primary Key.        
        /// </summary>
        public string EquipmentNumber { get; set; }

        /// <summary>        
        /// Gets or sets the airplane model.
        /// Foreign Key for AirplaneType.
        /// </summary>
        public string AirplaneModel { get; set; }

        /// <summary>        
        /// Gets or sets the airline code.
        /// Foreign Key for Airline code.
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        /// <value>
        /// The serial number.
        /// </value>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the maximum takeoff weight.
        /// </summary>
        public decimal MaximumTakeoffWeight { get; set; }

        /// <summary>
        /// Gets or sets the weight in pound.
        /// </summary>
        public decimal WeightInPound { get; set; }

        /// <summary>
        /// Gets or sets the weight in tonnes.
        /// </summary>
        public decimal WeightInTonnes { get; set; }

        /// <summary>
        /// Gets or sets the empty operating weight.
        /// </summary>
        public decimal EmptyOperatingWeight { get; set; }

        /// <summary>
        /// Gets or sets the filming maximum weight.
        /// </summary>
        public decimal FilmingMaximumWeight { get; set; }

        /// <summary>
        /// Gets or sets the take off weight in tonnes.
        /// </summary>
        public decimal TakeoffWeightInTonnes { get; set; }

        /// <summary>
        /// Gets or sets the group weight.
        /// </summary>
        public decimal GroupWeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum landing weight.
        /// </summary>
        public decimal MaximumLandingWeight { get; set; }

        /// <summary>
        /// Gets or sets the maximum zero fuel weight.
        /// </summary>
        public decimal MaximumZeroFuelWeight { get; set; }

        /// <summary>
        /// Gets or sets the passenger capacity.
        /// </summary>
        public int PassengerCapacity { get; set; }

        /// <summary>
        /// Gets or sets the crew capacity.
        /// </summary>
        public int CrewCapacity { get; set; }

        /// <summary>
        /// Gets or sets the magnitude.
        /// </summary>
        public decimal Magnitude { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is active.
        /// </summary>
        public bool Status { get; set; }
        #endregion
    }
}