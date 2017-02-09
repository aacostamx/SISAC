//------------------------------------------------------------------------
// <copyright file="InternationalFuelContractConcept.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using Airport;
    using Catalog;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// International Fuel ContractConcept Entity
    /// </summary>
    [Table("Finance.InternationalFuelContractConcept")]
    public partial class InternationalFuelContractConcept
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
        [Required]
        [StringLength(2)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// StationCode
        /// </summary>
        [Required]
        [StringLength(3)]
        public string StationCode { get; set; }

        /// <summary>
        /// ServiceCode
        /// </summary>
        [Required]
        [StringLength(12)]
        public string ServiceCode { get; set; }

        /// <summary>
        /// ProviderNumberPrimary
        /// </summary>
        [Required]
        [StringLength(8)]
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// FuelConceptID
        /// </summary>
        public long FuelConceptID { get; set; }

        /// <summary>
        /// FuelConceptTypeCode
        /// </summary>
        [Required]
        [StringLength(5)]
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// ChargeFactorTypeID
        /// </summary>
        public int ChargeFactorTypeID { get; set; }

        /// <summary>
        /// ProviderNumber
        /// </summary>
        [Required]
        [StringLength(8)]
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
        /// InternationalFuelContract
        /// </summary>
        public virtual InternationalFuelContract InternationalFuelContract { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public virtual Provider Provider { get; set; }

        /// <summary>
        /// InternationalFuelRate
        /// </summary>
        public virtual ICollection<InternationalFuelRate> InternationalFuelRate { get; set; }
    }
}
