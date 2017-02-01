//------------------------------------------------------------------------
// <copyright file="FuelConceptTypeDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Airports
{
    using Finances;
    using System.Collections.Generic;

    /// <summary>
    /// FuelConceptTypeDto
    /// </summary>
    public class FuelConceptTypeDto
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FuelConceptTypeDto()
        {
            InternationalFuelContractConcept = new List<InternationalFuelContractConceptDto>();
        }

        /// <summary>
        /// Key Fuel Concept Type Code
        /// </summary>
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// Fuel Concept Type
        /// </summary>
        public string FuelConceptTypeName { get; set; }

        /// <summary>
        /// InternationalFuelContractConcept Relationship
        /// </summary>
        public ICollection<InternationalFuelContractConceptDto> InternationalFuelContractConcept { get; set; }
    }
}
