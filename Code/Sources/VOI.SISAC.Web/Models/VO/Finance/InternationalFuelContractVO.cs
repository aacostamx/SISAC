//------------------------------------------------------------------------
// <copyright file="InternationalFuelContractVO.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using VOI.SISAC.Web.Models.Attributes;
    using Airport;
    using Catalog;

    /// <summary>
    /// InternationalFuelContractVO
    /// </summary>
    public class InternationalFuelContractVO
    {
        /// <summary>
        /// Gets or sets the effectice date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The effectice date.
        /// </value>
        [Display(Name = "EffectiveDate", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        [DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource)
            , ErrorMessageResourceName = "DateTimeValidation")]
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
        //[StringLength(2, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources.Resource),
        //    ErrorMessageResourceName = "LengthMin2")]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        [Display(Name = "StationCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "RequiredField")]
        //[StringLength(3, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMin3")]
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
        //[StringLength(12, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource),
        //                  ErrorMessageResourceName = "ServiceCodeMaxLong")]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or Set the Provider Number
        /// </summary>
        [Display(Name = "ProviderNumberPrimary", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        //[StringLength(8, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Resource),
        //                  ErrorMessageResourceName = "ProviderNumberMaxLong")]
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// AircraftRegistCCFlag
        /// </summary>
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
        //[StringLength(12, ErrorMessageResourceType = typeof(Resources.Resource),
        //    ErrorMessageResourceName = "LengthMax12")]
        public string CCNumber { get; set; }


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
        //[StringLength(8, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Resource),
        //                  ErrorMessageResourceName = "AccountingAccountNumberMaxMinLong")]
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
        //[StringLength(8, MinimumLength = 6, ErrorMessageResourceType = typeof(Resources.Resource),
        //                  ErrorMessageResourceName = "LiabilityAccountNumberMaxMinLong")]
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
        public int OperationTypeID { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        [Display(Name = "CurrencyCode", ResourceType = typeof(Resources.Resource))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "RequiredField")]
        //[StringLength(3, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource),
        //                  ErrorMessageResourceName = "LengthMinThree")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the End Date Contract.
        /// </summary>
        /// <value>
        /// The EndDate Contract.
        /// </value>
        [Display(Name = "EndDateContract", ResourceType = typeof(Resources.Resource))]
        //[DateTimeValidation(ErrorMessageResourceType = typeof(Resources.Resource)
        //    , ErrorMessageResourceName = "DateTimeValidation")]
        public DateTime? EndDateContract { get; set; }

        /// <summary>
        /// Airline
        /// </summary>
        public AirlineVO Airline { get; set; }

        /// <summary>
        /// Airport
        /// </summary>
        public AirportVO Airport { get; set; }

        /// <summary>
        /// Service
        /// </summary>
        public ServiceVO Service { get; set; }

        /// <summary>
        /// OperationType
        /// </summary>
        public OperationTypeVO OperationType { get; set; }

        /// <summary>
        /// AccountingAccount
        /// </summary>
        public AccountingAccountVO AccountingAccount { get; set; }

        /// <summary>
        /// CostCenter
        /// </summary>
        public CostCenterVO CostCenter { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public CurrencyVO Currency { get; set; }

        /// <summary>
        /// LiabilityAccount
        /// </summary>
        public LiabilityAccountVO LiabilityAccount { get; set; }

        /// <summary>
        /// Provider
        /// </summary>
        public ProviderVO Provider { get; set; }

        /// <summary>
        /// Reference to table InternationalFuelContractConcept
        /// </summary>
        public ICollection<InternationalFuelContractConceptVO> InternationalFuelContractConcepts { get; set; }
    }
}