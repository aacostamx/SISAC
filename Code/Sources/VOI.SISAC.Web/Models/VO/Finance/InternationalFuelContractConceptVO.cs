//------------------------------------------------------------------------
// <copyright file="InternationalFuelContractConceptVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using Airport;
    using Catalog;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.Attributes;

    /// <summary>
    /// InternationalFuelContractConceptVO Class
    /// </summary>
    public class InternationalFuelContractConceptVO
    {
        /// <summary>
        /// InternationalFuelContractConcept ID
        /// </summary>
        [Display(Name = "ID")]
        public long InternationalFuelContractConceptID { get; set; }

        /// <summary>
        /// EffectiveDate
        /// </summary>
        [Display(Name = "EffectiveDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateTimeValidation")]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// AirlineCode
        /// </summary>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(2, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMin2")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// StationCode
        /// </summary>
        [Display(Name = "StationCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMin3")]
        public string StationCode { get; set; }

        /// <summary>
        /// ServiceCode
        /// </summary>
        [Display(Name = "ServiceCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(12, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ServiceCodeMaxLong")]
        public string ServiceCode { get; set; }

        /// <summary>
        /// ProviderNumberPrimary
        /// </summary>
        [Display(Name = "ProviderNumberPrimary", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        [StringLength(10, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "ProviderNumberMaxLong")]
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
        /// FuelConcept
        /// </summary>
        public FuelConceptVO FuelConcept { get; set; }

        /// <summary>
        /// FuelConceptType
        /// </summary>
        public FuelConceptTypeVO FuelConceptType { get; set; }

        /// <summary>
        /// ChargeFactorType
        /// </summary>
        public ChargeFactorTypeVO ChargeFactorType { get; set; }

        /// <summary>
        /// InternationalFuelContract
        /// </summary>
        public InternationalFuelContractVO InternationalFuelContract { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderVO Provider { get; set; }

        /// <summary>
        /// InternationalFuelRate
        /// </summary>
        public ICollection<InternationalFuelRateVO> InternationalFuelRate { get; set; }
    }
}