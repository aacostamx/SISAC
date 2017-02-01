//------------------------------------------------------------------------
// <copyright file="OperationType.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Catalog
{
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Operation type entity.
    /// </summary>
    public class OperationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationType"/> class.
        /// </summary>
        public OperationType()
        {
            this.AirportServiceContracts = new HashSet<AirportServiceContract>();
            this.NationalFuelContracts = new HashSet<NationalFuelContract>();
        }

        /// <summary>
        /// Gets or sets the operation type identifier.
        /// </summary>
        /// <value>
        /// The operation type identifier.
        /// </value>
        public int OperationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation.
        /// </summary>
        /// <value>
        /// The name of the operation.
        /// </value>
        public string OperationName { get; set; }

        /// <summary>
        /// Gets or sets the airport service contracts.
        /// </summary>
        /// <value>
        /// The airport service contracts.
        /// </value>
        public virtual ICollection<AirportServiceContract> AirportServiceContracts { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contracts.
        /// </summary>
        /// <value>
        /// The national fuel contracts.
        /// </value>
        public virtual ICollection<NationalFuelContract> NationalFuelContracts { get; set; }
    }
}
