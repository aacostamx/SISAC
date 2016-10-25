//------------------------------------------------------------------------
// <copyright file="NationalFuelContractVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;
    using VOI.SISAC.Web.Helpers;
    using VOI.SISAC.Web.Models.Attributes;

    /// <summary>
    /// Data Object of National Fuel Contract
    /// </summary>
    public class NationalFuelContractVO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelContractVO"/> class.
        /// </summary>
        public NationalFuelContractVO()
        {
            this.NationalFuelContractConcept = new List<NationalFuelContractConceptVO>();
        }

        /// <summary>
        /// Gets or sets the effective date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The effective date.
        /// </value>
        [Display(Name = "EffectiveDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "DateTimeValidation")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Display(Name = "AirlineCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(2, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax2")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// Primary key.
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
        /// Primary key.
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
        /// Gets or sets the ProviderNumberPrimary
        /// Primary key.
        /// </summary>
        /// <value>
        /// the Provider Number Primary.
        /// </value>
        [Display(Name = "ProviderNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax8")]
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InternationalFuelContract"/> is active.
        /// </summary>
        /// <value>
        /// The  Aircraft Registration CCFlag.
        /// </value>
        [Display(Name = "AircraftRegistCCFlag", ResourceType = typeof(Resources.Resource))]
        public bool AircraftRegistCCFlag { get; set; }

        /// <summary>
        /// Gets or sets the cost center number.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
        [Display(Name = "CCNumber", ResourceType = typeof(Resources.Resource))]
        [RequiredIf("AircraftRegistCCFlag", false, ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public string CCNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InternationalFuelContract"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "Status", ResourceType = typeof(Resources.Resource))]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the accounting account number.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The accounting account number.
        /// </value>
        [Display(Name = "AccountingAccountNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax8")]
        public string AccountingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the liability account number.
        /// </summary>
        /// <value>
        /// The liability account number.
        /// </value>
        [Display(Name = "LiabilityAccountNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax8")]
        public string LiabilityAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        [Display(Name = "OperationTypeID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int OperationTypeID { get; set; }

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
        /// Gets or sets the federal tax code.
        /// </summary>
        /// <value>
        /// The federal tax code.
        /// </value>
        [Display(Name = "FederalTaxCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax8")]
        public string FederalTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the federal tax value.
        /// </summary>
        /// <value>
        /// The federal tax value.
        /// </value>
        [Display(Name = "Rate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        [RequiredIf("FederalTaxCode", "", ErrorMessageResourceType = typeof(Resources.Resource),
          ErrorMessageResourceName = "RequiredField")]
        public decimal FederalTaxValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the federal tax is percentage or simple value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if federal tax is percentage; otherwise is a simple value
        /// </value>
        [Display(Name = "AplicaPorcentaje_Valor", ResourceType = typeof(Resources.Resource))]
        public bool FederalTaxFlag { get; set; }

        /// <summary>
        /// Gets or sets the Airline
        /// </summary>
        [Display(Name = "AirlineName", ResourceType = typeof(Resources.Resource))]
        public string AirlineName { get; set; }

        /// <summary>
        /// Gets or sets the Airport
        /// </summary>
        [Display(Name = "StationName", ResourceType = typeof(Resources.Resource))]
        public string StationName { get; set; }

        /// <summary>
        /// Gets or sets the Service
        /// </summary>
        [Display(Name = "ServiceName", ResourceType = typeof(Resources.Resource))]
        public string ServiceDescription { get; set; }

        /// <summary>
        /// Gets or sets the OperationType
        /// </summary>
        [Display(Name = "OperationName", ResourceType = typeof(Resources.Resource))]
        public string OperationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the AccountingAccount
        /// </summary>
        [Display(Name = "AccountingAccountName", ResourceType = typeof(Resources.Resource))]
        public string AccountingAccountName { get; set; }

        /// <summary>
        /// Gets or sets the CostCenter
        /// </summary>
        [Display(Name = "CCName", ResourceType = typeof(Resources.Resource))]
        public string CostCenterName { get; set; }

        /// <summary>
        /// Gets or sets the Currency
        /// </summary>
        [Display(Name = "CurrencyName", ResourceType = typeof(Resources.Resource))]
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets the LiabilityAccount
        /// </summary>
        [Display(Name = "LiabilityAccountName", ResourceType = typeof(Resources.Resource))]
        public string LiabilityAccountName { get; set; }

        /// <summary>
        /// Gets or sets the Provider
        /// </summary>
        [Display(Name = "ProviderName", ResourceType = typeof(Resources.Resource))]
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the federal tax description.
        /// </summary>
        /// <value>
        /// The federal tax description.
        /// </value>
        [Display(Name = "FederalTaxCode", ResourceType = typeof(Resources.Resource))]
        public string FederalTaxDescription { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contract concept.
        /// </summary>
        /// <value>
        /// The national fuel contract concept.
        /// </value>
        public IList<NationalFuelContractConceptVO> NationalFuelContractConcept { get; set; }
    }
}
