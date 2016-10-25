//------------------------------------------------------------------------
// <copyright file="NationalFuelRateVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.Attributes;

    /// <summary>
    /// National fuel rate view object
    /// </summary>
    public class NationalFuelRateVO
    {
        /// <summary>
        /// Gets or sets the national fuel rate identifier.
        /// </summary>
        /// <value>
        /// The national fuel rate identifier.
        /// </value>
        public long NationalFuelRateId { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        [Display(Name = "StationCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax3")]
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [Display(Name = "ServiceCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(12, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax12")]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        [Display(Name = "ProviderNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax8")]
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the effective start date.
        /// </summary>
        /// <value>
        /// The effective start date.
        /// </value>
        [Display(Name = "RateStartDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateTimeValidation")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime EffectiveStartDate { get; set; }

        /// <summary>
        /// Gets or sets the effective end date.
        /// </summary>
        /// <value>
        /// The effective end date.
        /// </value>
        [Display(Name = "RateEndDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "DateTimeValidation")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime EffectiveEndDate { get; set; }

        /// <summary>
        /// Gets or sets the schedule type code.
        /// </summary>
        /// <value>
        /// The schedule type code.
        /// </value>
        [Display(Name = "ScheduleTypeCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string ScheduleTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the fuel concept type code.
        /// </summary>
        /// <value>
        /// The fuel concept type code.
        /// </value>
        [Display(Name = "FuelConceptTypeCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the rate value.
        /// </summary>
        /// <value>
        /// The rate value.
        /// </value>
        [Display(Name = "Rate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00000}")]
        public decimal RateValue { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        [Display(Name = "CurrencyCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax3")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the airport.
        /// </summary>
        /// <value>
        /// The airport.
        /// </value>
        [Display(Name = "StationName", ResourceType = typeof(Resources.Resource))]
        public string StationName { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        [Display(Name = "ServiceName", ResourceType = typeof(Resources.Resource))]
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        [Display(Name = "ProviderName", ResourceType = typeof(Resources.Resource))]
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the type of the schedule.
        /// </summary>
        /// <value>
        /// The type of the schedule.
        /// </value>
        [Display(Name = "ScheduleTypeName", ResourceType = typeof(Resources.Resource))]
        public string ScheduleTypeName { get; set; }

        /// <summary>
        /// Gets or sets the type of the fuel concept.
        /// </summary>
        /// <value>
        /// The type of the fuel concept.
        /// </value>
        [Display(Name = "FuelConceptTypeCode", ResourceType = typeof(Resources.Resource))]
        public string FuelConceptTypeName { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        [Display(Name = "CurrencyName", ResourceType = typeof(Resources.Resource))]
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public string EndDate { get; set; }
    }
}
