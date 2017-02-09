//------------------------------------------------------------------------
// <copyright file="ProviderDto.cs" company="AACOSTA">
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

    /// <summary>
    /// Data Transfer Object for Provider
    /// </summary>
    public class ProviderDto
    {
        /// <summary>
        /// Gets or Set the Provider Number
        /// </summary>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or Set the Provider Name
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or Set the Provider Short Name
        /// </summary>
        public string ProviderShortName { get; set; }

        /// <summary>
        /// Gets or Set the status of the provider
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// AirportServiceContracts
        /// </summary>
        public ICollection<AirportServiceContractDto> AirportServiceContracts { get; set; }

        /// <summary>
        /// InternationalFuelContracts
        /// </summary>
        public ICollection<InternationalFuelContractDto> InternationalFuelContracts { get; set; }

        /// <summary>
        /// InternationalFuelContractConcepts
        /// </summary>
        public ICollection<InternationalFuelContractConceptDto> InternationalFuelContractConcepts { get; set; }

    }
}
