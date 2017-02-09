//------------------------------------------------------------------------
// <copyright file="InternationalFuelRateDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Finances
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Entities.Finance;

    /// <summary>
    /// Data Object of International Fuel Rate
    /// </summary>
    public class InternationalFuelRateDto
    {
        /// <summary>
        /// Gets or sets International Fuel Rate Identifier
        /// </summary>
        public long InternationalFuelRateID { get; set; }

        /// <summary>
        /// Gets or sets International Fuel Contract Concept Identifier
        /// </summary>
        public long InternationalFuelContractConceptID { get; set; }

        /// <summary>
        /// Gets or sets the Rate Start Date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Rate Start Date.
        /// </value>
        public DateTime RateStartDate { get; set; }

        /// <summary>
        /// Gets or sets the Rate End Date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Rate End Date.
        /// </value>
        public DateTime RateEndDate { get; set; }

        /// <summary>
        /// Gets or sets the Rate.
        /// </summary>
        /// <value>
        /// The Rate.
        /// </value>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets a list of International Fuel Contract Concept
        /// </summary>
        public InternationalFuelContractConceptDto InternationalFuelContractConcept { get; set; }
    }
}
