//------------------------------------------------------------------------
// <copyright file="InternationalFuelContract.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Entities.Finance
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Airport;
    using Catalog;

    /// <summary>
    /// International Fuel Contract
    /// </summary>
    public partial class InternationalFuelContract
    {
        /// <summary>
        /// Gets or sets the effective date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The effective date.
        /// </value>
        [Key]
        [Column(Order = 0)]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [Key]
        [Column(Order = 3)]
        [StringLength(12)]
        public string ServiceCode { get; set; }

        // <summary>
        /// Gets or sets the ProviderNumberPrimary.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Provider Number Primary.
        /// </value>
        [Key]
        [Column(Order = 4)]
        [StringLength(8)]
        public string ProviderNumberPrimary { get; set; }

        // <summary>
        /// Gets or sets the Aircraft Regist CC Flag.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Aircraft Regist CC Flag.
        /// </value>
        public bool AircraftRegistCCFlag { get; set; }

        /// <summary>
        /// Gets or sets the cost center number.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
        [StringLength(12)]
        public string CCNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InternationalFuelContract"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the accounting account number.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The accounting account number.
        /// </value>
        [Required]
        [StringLength(8)]
        public string AccountingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the liability account number.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The liability account number.
        /// </value>
        [Required]
        [StringLength(8)]
        public string LiabilityAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public int OperationTypeID { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        [Required]
        [StringLength(3)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the End Date Contract.
        /// </summary>
        /// <value>
        /// The EndDate Contract.
        /// </value>
        public DateTime? EndDateContract { get; set; }

        /// <summary>
        /// Gets or sets the Airline
        /// </summary>
        public virtual Airline Airline { get; set; }

        /// <summary>
        /// Gets or sets the Airport
        /// </summary>
        public virtual Airport Airport { get; set; }

        /// <summary>
        /// Gets or sets the Service
        /// </summary>
        public virtual Service Service { get; set; }

        /// <summary>
        /// Gets or sets the OperationType
        /// </summary>
        public virtual OperationType OperationType { get; set; }

        /// <summary>
        /// Gets or sets the AccountingAccount
        /// </summary>
        public virtual AccountingAccount AccountingAccount { get; set; }

        /// <summary>
        /// Gets or sets the CostCenter
        /// </summary>
        public virtual CostCenter CostCenter { get; set; }

        /// <summary>
        /// Gets or sets the Currency
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the LiabilityAccount
        /// </summary>
        public virtual LiabilityAccount LiabilityAccount { get; set; }

        /// <summary>
        /// Gets or sets the Provider
        /// </summary>
        public virtual Provider Provider { get; set; }

        /// <summary>
        /// Gets or sets the Reference to table InternationalFuelContractConcept
        /// </summary>
        public virtual ICollection<InternationalFuelContractConcept> InternationalFuelContractConcepts { get; set; }
    }
}
