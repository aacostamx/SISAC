//------------------------------------------------------------------------
// <copyright file="FuelConcept.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Airport
{
    using System.Collections.Generic;
    using VOI.SISAC.Entities.Finance;

    /// <summary>
    /// Fuel Concept
    /// </summary>
    public class FuelConcept
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuelConcept"/> class.
        /// </summary>
        public FuelConcept()
        {
            this.NationalFuelContractConcepts = new HashSet<NationalFuelContractConcept>();
        }

        /// <summary>
        /// Gets or sets the FuelConceptID.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Fuel Concept ID.
        /// </value>
        public long FuelConceptID { get; set; }

        /// <summary>
        /// Gets or sets the FuelConceptName.
        /// </summary>
        /// <value>
        /// The FuelConceptName.
        /// </value>
        public string FuelConceptName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FuelConcept"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contract concepts.
        /// </summary>
        /// <value>
        /// The national fuel contract concepts.
        /// </value>
        public virtual ICollection<NationalFuelContractConcept> NationalFuelContractConcepts { get; set; }
    }
}
