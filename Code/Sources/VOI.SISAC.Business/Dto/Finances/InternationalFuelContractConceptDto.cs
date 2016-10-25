//------------------------------------------------------------------------
// <copyright file="InternationalFuelContractConceptDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace VOI.SISAC.Business.Dto.Finances
{
    using Airports;
    using Catalogs;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Data Object of International Fuel Contract Concept
    /// </summary>
    public class InternationalFuelContractConceptDto
    {
        /// <summary>
        /// InternationalFuelContractConcept ID
        /// </summary>
        public long InternationalFuelContractConceptID { get; set; }

        /// <summary>
        /// EffectiveDate
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// AirlineCode
        /// </summary>
        public string AirlineCode { get; set; }

        /// <summary>
        /// StationCode
        /// </summary>
        public string StationCode { get; set; }

        /// <summary>
        /// ServiceCode
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// ProviderNumberPrimary
        /// </summary>
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// FuelConceptID
        /// </summary>
        public long FuelConceptID { get; set; }

        /// <summary>
        /// FuelConceptTypeCode
        /// </summary>
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// ChargeFactorTypeID
        /// </summary>
        public int ChargeFactorTypeID { get; set; }

        /// <summary>
        /// ProviderNumber
        /// </summary>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// FuelConcept
        /// </summary>
        public FuelConceptDto FuelConcept { get; set; }

        /// <summary>
        /// FuelConceptType
        /// </summary>
        public FuelConceptTypeDto FuelConceptType { get; set; }

        /// <summary>
        /// ChargeFactorType
        /// </summary>
        public ChargeFactorTypeDto ChargeFactorType { get; set; }

        /// <summary>
        /// InternationalFuelContract
        /// </summary>
        public InternationalFuelContractDto InternationalFuelContract { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderDto Provider { get; set; }

        /// <summary>
        /// InternationalFuelRate
        /// </summary>
        public ICollection<InternationalFuelRateDto> InternationalFuelRate { get; set; }
    }
}
