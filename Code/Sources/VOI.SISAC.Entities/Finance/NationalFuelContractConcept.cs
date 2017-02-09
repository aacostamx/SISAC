//------------------------------------------------------------------------
// <copyright file="NationalFuelContractConcept.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System;
    using System.Collections.Generic;
    using Airport;
    using Catalog;

    /// <summary>
    /// International Fuel ContractConcept Entity
    /// </summary>
    public class NationalFuelContractConcept
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
        /// FuelConcept
        /// </summary>
        public virtual FuelConcept FuelConcept { get; set; }

        /// <summary>
        /// FuelConceptType
        /// </summary>
        public virtual FuelConceptType FuelConceptType { get; set; }

        /// <summary>
        /// ChargeFactorType
        /// </summary>
        public virtual ChargeFactorType ChargeFactorType { get; set; }

        /// <summary>
        /// National Fuel Contract
        /// </summary>
        public virtual NationalFuelContract NationalFuelContract { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public virtual Provider Provider { get; set; }
    }
}