//------------------------------------------------------------------------
// <copyright file="InternationalFuelRate.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// International Fuel Rate Entity
    /// </summary>
    [Table("Finance.InternationalFuelRate")]    
    public partial class InternationalFuelRate
    {
        /// <summary>
        /// International Fuel Rate Identifier
        /// </summary>
        public long InternationalFuelRateID { get; set; }

        /// <summary>
        /// International Fuel Contract Concept identifier
        /// </summary>
        public long InternationalFuelContractConceptID { get; set; }

        /// <summary>
        /// Start Date or the Rate
        /// </summary>
        public DateTime RateStartDate { get; set; }

        /// <summary>
        /// End Date of the Rate
        /// </summary>
        public DateTime RateEndDate { get; set; }

        /// <summary>
        /// Value of the Rate
        /// </summary>
        [Column(TypeName = "numeric")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Relationship with the InternationalFuelContractConcept entity
        /// </summary>
        public virtual InternationalFuelContractConcept InternationalFuelContractConcept { get; set; }
    }

}
