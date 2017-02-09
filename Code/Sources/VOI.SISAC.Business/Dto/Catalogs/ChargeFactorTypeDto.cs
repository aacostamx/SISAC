//------------------------------------------------------------------------
// <copyright file="ChargeFactorTypeDto.cs" company="AACOSTA">
//  Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Catalogs
{
    using Finances;
    using System.Collections.Generic;

    /// <summary>
    /// Dto ChargeFactorType
    /// </summary>
    public class ChargeFactorTypeDto
    {
        /// <summary>
        /// ChargeFactorTypeID
        /// </summary>
        public int ChargeFactorTypeID { get; set; }

        /// <summary>
        /// ChargeFactorTypeName
        /// </summary>
        public string ChargeFactorTypeName { get; set; }

        /// <summary>
        /// InternationalFuelContractConcept
        /// </summary>
        public ICollection<InternationalFuelContractConceptDto> InternationalFuelContractConcept { get; set; }
    }
}
