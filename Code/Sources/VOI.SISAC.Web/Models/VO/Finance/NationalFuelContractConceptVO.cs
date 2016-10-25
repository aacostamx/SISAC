//------------------------------------------------------------------------
// <copyright file="NationalFuelContractConceptVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Data Object of National Fuel Contract
    /// </summary>
    public class NationalFuelContractConceptVO
    {
        /// <summary>
        /// InternationalFuelContractConcept Id
        /// </summary>
        [Display(Name = "ID")]
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
        [Display(Name = "FuelConceptID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public long FuelConceptID { get; set; }

        /// <summary>
        /// FuelConceptTypeCode
        /// </summary>
        [Display(Name = "FuelConceptTypeCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// ChargeFactorTypeID
        /// </summary>
        [Display(Name = "ChargeFactorTypeID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public int ChargeFactorTypeID { get; set; }

        /// <summary>
        /// ProviderNumber
        /// </summary>
        [Display(Name = "ProviderNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProviderNumberMaxLong")]
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
