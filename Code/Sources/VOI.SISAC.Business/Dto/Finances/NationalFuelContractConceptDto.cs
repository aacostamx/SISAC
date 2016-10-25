//------------------------------------------------------------------------
// <copyright file="NationalFuelContractConceptDto.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Finances
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// International Fuel ContractConcept Entity
    /// </summary>
    public class NationalFuelContractConceptDto
    {
        /// <summary>
        /// InternationalFuelContractConcept Id
        /// </summary>
        public long NationalFuelContractConceptId { get; set; }

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
        /// FuelConceptID
        /// </summary>
        public string FuelConceptName { get; set; }

        /// <summary>
        /// FuelConceptTypeCode
        /// </summary>
        public string FuelConceptTypeName { get; set; }

        /// <summary>
        /// ChargeFactorTypeID
        /// </summary>
        public string ChargeFactorTypeName { get; set; }

        /// <summary>
        /// ProviderNumber
        /// </summary>
        public string ProviderName { get; set; }
    }
}