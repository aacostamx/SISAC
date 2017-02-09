//------------------------------------------------------------------------
// <copyright file="AirplaneWeightType.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Airplane weight type entity
    /// </summary>
    public class AirplaneWeightType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirplaneWeightType"/> class.
        /// </summary>
        public AirplaneWeightType()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
        }

        /// <summary>
        /// Gets or sets the airplane weight code.
        /// </summary>
        /// <value>
        /// The airplane weight code.
        /// </value>
        public string AirplaneWeightCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the airplane weight.
        /// </summary>
        /// <value>
        /// The name of the airplane weight.
        /// </value>
        public string AirplaneWeightName { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public ICollection<AirportServiceContract> AirportServiceContracts { get; set; }
    }
}
