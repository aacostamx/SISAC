//------------------------------------------------------------------------
// <copyright file="AirplaneWeightMeasureType.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Airplane weight measure type entity
    /// </summary>
    public class AirplaneWeightMeasureType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneWeightMeasureType"/> class.
        /// </summary>
        public AirplaneWeightMeasureType()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
        }

        /// <summary>
        /// Gets or sets the airplane weight measure identifier.
        /// </summary>
        /// <value>
        /// The airplane weight measure identifier.
        /// </value>
        public int AirplaneWeightMeasureId { get; set; }

        /// <summary>
        /// Gets or sets the name of the airplane weight measure.
        /// </summary>
        /// <value>
        /// The name of the airplane weight measure.
        /// </value>
        public string AirplaneWeightMeasureName { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public ICollection<AirportServiceContract> AirportServiceContracts { get; set; }
    }
}
