//------------------------------------------------------------------------
// <copyright file="DrinkingWater.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    /// <summary>
    /// DrinkingWater Entity
    /// </summary>
    public partial class DrinkingWater
    {
        /// <summary>
        /// Gets or sets the drinking water.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The drinking water.
        /// </value>
        public long DrinkingWaterId { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// Foreing key.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the drinking water.
        /// </summary>
        /// <value>
        /// The name of the drinking water.
        /// </value>
        public string DrinkingWaterName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DrinkingWaterId"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the airplane.
        /// Relationship with Airplane entity.
        /// </summary>
        /// <value>
        /// The airplanes.
        /// </value>
        public virtual Airplane Airplane { get; set; }
    }
}
