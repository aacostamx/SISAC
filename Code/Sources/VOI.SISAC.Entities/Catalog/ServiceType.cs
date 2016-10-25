//------------------------------------------------------------------------
// <copyright file="ServiceType.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Service calculation type entity.
    /// </summary>
    public class ServiceType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceType"/> class.
        /// </summary>
        public ServiceType()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
        }

        /// <summary>
        /// Gets or sets the service type identifier.
        /// </summary>
        /// <value>
        /// The service type identifier.
        /// </value>
        public string ServiceTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the service type.
        /// </summary>
        /// <value>
        /// The name of the service type.
        /// </value>
        public string ServiceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContracts { get; set; } 
    }
}
