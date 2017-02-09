//------------------------------------------------------------------------
// <copyright file="NationalFuelContract.cs" company="AACOSTA">
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
    /// International Fuel Contract
    /// </summary>
    public class NationalFuelContract
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalFuelContract"/> class.
        /// </summary>
        public NationalFuelContract()
        {
            this.NationalFuelContractConcept = new HashSet<NationalFuelContractConcept>();
        }

        /// <summary>
        /// Gets or sets the effective date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The effective date.
        /// </value>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the Provider Number Primary.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The Provider Number Primary.
        /// </value>
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [aircraft registration cc flag].
        /// </summary>
        /// <value>
        /// <c>true</c> if [aircraft registration cc flag]; otherwise, <c>false</c>.
        /// </value>
        public bool AircraftRegistCCFlag { get; set; }

        /// <summary>
        /// Gets or sets the cost center number.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
        public string CostCenterNumber { get; set; }

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
        public string AccountingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the liability account number.
        /// Foreign key.
        /// </summary>
        /// <value>
        /// The liability account number.
        /// </value>
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
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the federal tax code.
        /// </summary>
        /// <value>
        /// The federal tax code.
        /// </value>
        public string FederalTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the federal tax value.
        /// </summary>
        /// <value>
        /// The federal tax value.
        /// </value>
        public decimal FederalTaxValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the federal tax is percentage or simple value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if federal tax is percentage; otherwise is a simple value
        /// </value>
        public bool FederalTaxFlag { get; set; }

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
        /// Gets or sets the airport tax.
        /// </summary>
        /// <value>
        /// The airport tax.
        /// </value>
        public virtual Tax FederalTax { get; set; }

        /// <summary>
        /// Gets or sets the Reference to table International Fuel Contract Concept
        /// </summary>
        public virtual ICollection<NationalFuelContractConcept> NationalFuelContractConcept { get; set; }
    }
}
