//------------------------------------------------------------------------
// <copyright file="ServiceCalculationType.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Service calculation type entity.
    /// </summary>
    public class ServiceCalculationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCalculationType"/> class.
        /// </summary>
        public ServiceCalculationType()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
        }

        /// <summary>
        /// Gets or sets the calculation type identifier.
        /// </summary>
        /// <value>
        /// The calculation type identifier.
        /// </value>
        public int CalculationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the calculation type.
        /// </summary>
        /// <value>
        /// The name of the calculation type.
        /// </value>
        public string CalculationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContracts { get; set; }
    }
}
