// <copyright file="AirportServiceContractVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.Attributes;

    /// <summary>
    /// Class Airport Service Contract VO
    /// </summary>
    public class AirportServiceContractVO
    {
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
        // [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource),
        //    ErrorMessageResourceName = "DateTimeValidation")]
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
        /// Gets or sets the provider number.
        /// Primary key.
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
        /// Gets or sets the cost center number.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
        [Display(Name = "CCNumber", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        //[StringLength(14, ErrorMessageResourceType = typeof(Resources.Resource),
        //    ErrorMessageResourceName = "LengthMax14")]
        public string CostCenterNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AirportServiceContractVO"/> is active.
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
        /// Foreign key.
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
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        [Display(Name = "OperationTypeID", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int OperationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the service.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        [Display(Name = "ServiceTypeCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [StringLength(1, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax1")]
        public string ServiceTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the airport fee code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airport fee code.
        /// </value>
        [Display(Name = "AirportFeeCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax8")]
        public string AirportFeeCode { get; set; }

        /// <summary>
        /// Gets or sets the airport fee value.
        /// </summary>
        /// <value>
        /// The airport fee value.
        /// </value>
        [Display(Name = "Rate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        [RequiredIf("AirportFeeCode", "", ErrorMessageResourceType = typeof(Resources.Resource),
           ErrorMessageResourceName = "RequiredField")]
        public decimal? AirportFeeValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "AplicaPorcentaje_Valor", ResourceType = typeof(Resources.Resource))]
        public bool AirportFeeFlag { get; set; }

        /// <summary>
        /// Gets or sets the local tax code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The local tax code.
        /// </value>
        [Display(Name = "LocalTaxCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax8")]
        public string LocalTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the local tax value.
        /// </summary>
        /// <value>
        /// The local tax value.
        /// </value>
        [Display(Name = "Rate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        [RequiredIf("LocalTaxCode", "", ErrorMessageResourceType = typeof(Resources.Resource),
           ErrorMessageResourceName = "RequiredField")]
        public decimal? LocalTaxValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "AplicaPorcentaje_Valor", ResourceType = typeof(Resources.Resource))]
        public bool LocalTaxFlag { get; set; }

        /// <summary>
        /// Gets or sets the state tax code.
        /// </summary>
        /// <value>
        /// The state tax code.
        /// Foreign key.
        /// </value>
        [Display(Name = "StateTaxCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(8, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax8")]
        public string StateTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the state tax value.
        /// </summary>
        /// <value>
        /// The state tax value.
        /// </value>
        [Display(Name = "Rate", ResourceType = typeof(Resources.Resource))]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00}")]
        [RegularExpression(@"^([0-9]{1,8})+([\.\,])?([0-9]{1,2})?$",
            ErrorMessageResourceName = "InvalidFormat",
            ErrorMessageResourceType = typeof(Resources.Resource))]
        [RequiredIf("StateTaxCode", "", ErrorMessageResourceType = typeof(Resources.Resource),
           ErrorMessageResourceName = "RequiredField")]
        public decimal? StateTaxValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "AplicaPorcentaje_Valor", ResourceType = typeof(Resources.Resource))]
        public bool StateTaxFlag { get; set; }

        /// <summary>
        /// Gets or sets the federal tax code.
        /// Foreign key.
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
        public decimal? FederalTaxValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is fixed or percentage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the value is percentage otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "AplicaPorcentaje_Valor", ResourceType = typeof(Resources.Resource))]
        public bool FederalTaxFlag { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value depends on the airplane's weight.
        /// </summary>
        /// <value>
        ///   <c>true</c> if depends on the airplane's weight otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "AirplanetWeightFlag", ResourceType = typeof(Resources.Resource))]
        public bool AirplanetWeightFlag { get; set; }

        /// <summary>
        /// Gets or sets the airplane weight description.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airplane weight description.
        /// </value>
        [Display(Name = "AirplaneWeightCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(5, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax5")]
        [RequiredIf("AirplanetWeightFlag", true, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        public string AirplaneWeightCode { get; set; }

        /// <summary>
        /// Gets or sets the airplane weight unit of measure.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The airplane weight unit.
        /// </value>
        [Display(Name = "AirplaneWeightUnit", ResourceType = typeof(Resources.Resource))]
        [RequiredIf("AirplanetWeightFlag", true, ErrorMessageResourceType = typeof(Resources.Resource),
           ErrorMessageResourceName = "RequiredField")]
        public int? AirplaneWeightUnit { get; set; }

        /// <summary>
        /// Gets or sets the airplane weight multiplier.
        /// </summary>
        /// <value>
        /// The airplane weight multiplier.
        /// </value>
        [Display(Name = "AirplaneWeightMultiplier", ResourceType = typeof(Resources.Resource))]
        [RequiredIf("AirplanetWeightFlag", true, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        public long? AirplaneWeightMultiplier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the fee involves a 1:M relationship with the contract.
        /// </summary>
        /// <value>
        ///   <c>true</c> if involves a relationship otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "MultiRateFlag", ResourceType = typeof(Resources.Resource))]
        public bool MultiRateFlag { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        [Display(Name = "Rate", ResourceType = typeof(Resources.Resource))]
        [RequiredIf("MultiRateFlag", false, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:0.00000}")]
        public decimal? Rate { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        [Display(Name = "CurrencyCode", ResourceType = typeof(Resources.Resource))]
        [RequiredIf("MultiRateFlag", false, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "RequiredField")]
        [StringLength(3, ErrorMessageResourceType = typeof(Resources.Resource),
            ErrorMessageResourceName = "LengthMax3")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether requires to capture the service to estimate it.
        /// </summary>
        /// <value>
        ///   <c>true</c> if requires the service otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "ServiceRecordFlag", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public bool ServiceRecordFlag { get; set; }

        /// <summary>
        /// Gets or sets the calculation type identifier.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The calculation type identifier.
        /// </value>
        [Display(Name = "CalculationTypeId", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        public int CalculationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the end date contract.
        /// </summary>
        /// <value>
        /// The end date contract.
        /// </value>
        [Display(Name = "EndDateContract", ResourceType = typeof(Resources.Resource))]
        public DateTime? EndDateContract { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        //public CurrencyVO Currency { get; set; }

        /// <summary>
        /// Gets or sets the name of the airline.
        /// </summary>
        /// <value>
        /// The name of the airline.
        /// </value>
        [Display(Name = "AirlineName", ResourceType = typeof(Resources.Resource))]
        public string AirlineName { get; set; }

        /// <summary>
        /// Gets or sets the name of the airport.
        /// </summary>
        /// <value>
        /// The name of the airport.
        /// </value>
        [Display(Name = "StationName", ResourceType = typeof(Resources.Resource))]
        public string StationName { get; set; }

        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        [Display(Name = "ServiceName", ResourceType = typeof(Resources.Resource))]
        public string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the name of the provider.
        /// </summary>
        /// <value>
        /// The name of the provider.
        /// </value>
        [Display(Name = "ProviderName", ResourceType = typeof(Resources.Resource))]
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the cost center number.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
        [Display(Name = "CCName", ResourceType = typeof(Resources.Resource))]
        public string CCName { get; set; }

        /// <summary>
        /// Gets or sets the name of the accounting account.
        /// </summary>
        /// <value>
        /// The name of the accounting account.
        /// </value>
        [Display(Name = "AccountingAccountName", ResourceType = typeof(Resources.Resource))]
        public string AccountingAccountName { get; set; }

        /// <summary>
        /// Gets or sets the name of the liability account.
        /// </summary>
        /// <value>
        /// The name of the liability account.
        /// </value>
        [Display(Name = "LiabilityAccountName", ResourceType = typeof(Resources.Resource))]
        public string LiabilityAccountName { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation.
        /// </summary>
        /// <value>
        /// The name of the operation.
        /// </value>
        [Display(Name = "OperationName", ResourceType = typeof(Resources.Resource))]
        public string OperationName { get; set; }

        /// <summary>
        /// Gets or sets the name of the service type.
        /// </summary>
        /// <value>
        /// The name of the service type.
        /// </value>
        [Display(Name = "ServiceTypeCode", ResourceType = typeof(Resources.Resource))]
        public string ServiceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the airport tax.
        /// </summary>
        /// <value>
        /// The name of the airport tax.
        /// </value>
        [Display(Name = "AirportFeeCode", ResourceType = typeof(Resources.Resource))]
        public string AirportTaxName { get; set; }

        /// <summary>
        /// Gets or sets the name of the local tax.
        /// </summary>
        /// <value>
        /// The name of the local tax.
        /// </value>
        [Display(Name = "LocalTaxCode", ResourceType = typeof(Resources.Resource))]
        public string LocalTaxName { get; set; }

        /// <summary>
        /// Gets or sets the name of the state tax.
        /// </summary>
        /// <value>
        /// The name of the state tax.
        /// </value>
        [Display(Name = "StateTaxCode", ResourceType = typeof(Resources.Resource))]
        public string StateTaxName { get; set; }

        /// <summary>
        /// Gets or sets the name of the federal tax.
        /// </summary>
        /// <value>
        /// The name of the federal tax.
        /// </value>
        [Display(Name = "FederalTaxCode", ResourceType = typeof(Resources.Resource))]
        public string FederalTaxName { get; set; }

        /// <summary>
        /// Gets or sets the name of the currency.
        /// </summary>
        /// <value>
        /// The name of the currency.
        /// </value>
        [Display(Name = "CurrencyName", ResourceType = typeof(Resources.Resource))]
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the calculation type.
        /// </summary>
        /// <value>
        /// The name of the calculation type.
        /// </value>
        [Display(Name = "CalculationTypeName", ResourceType = typeof(Resources.Resource))]
        public string CalculationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the airplane weight.
        /// </summary>
        /// <value>
        /// The name of the airplane weight.
        /// </value>
        [Display(Name = "AirplaneWeightName", ResourceType = typeof(Resources.Resource))]
        public string AirplaneWeightName { get; set; }

        /// <summary>
        /// Gets or sets the name of the airplane weight unit.
        /// </summary>
        /// <value>
        /// The name of the airplane weight unit.
        /// </value>
        [Display(Name = "AirplaneWeightUnitName", ResourceType = typeof(Resources.Resource))]
        public string AirplaneWeightUnitName { get; set; }
    }
}