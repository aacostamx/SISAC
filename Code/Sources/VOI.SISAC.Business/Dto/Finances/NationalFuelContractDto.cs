//------------------------------------------------------------------------
// <copyright file="NationalFuelContractDto.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Finances
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Data Object of National Fuel Contract
    /// </summary>
    public class NationalFuelContractDto
    {
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
        /// Gets or sets the ProviderNumberPrimary
        /// Primary key.
        /// </summary>
        /// <value>
        /// the Provider Number Primary.
        /// </value>
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InternationalFuelContract"/> is active.
        /// </summary>
        /// <value>
        /// The  Aircraft Registration CCFlag.
        /// </value>
        public bool AircraftRegistCCFlag { get; set; }

        /// <summary>
        /// Gets or sets the cost center number.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
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
        public string AccountingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the liability account number.
        /// </summary>
        /// <value>
        /// The liability account number.
        /// </value>
        public string LiabilityAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public int OperationTypeID { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
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
        public string AirlineName { get; set; }

        /// <summary>
        /// Gets or sets the Airport
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// Gets or sets the Service
        /// </summary>
        public string ServiceDescription { get; set; }

        /// <summary>
        /// Gets or sets the OperationType
        /// </summary>
        public string OperationTypeName { get; set; }

        /// <summary>
        /// Gets or sets the AccountingAccount
        /// </summary>
        public string AccountingAccountName { get; set; }

        /// <summary>
        /// Gets or sets the CostCenter
        /// </summary>
        public string CostCenterName { get; set; }

        /// <summary>
        /// Gets or sets the Currency
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets the LiabilityAccount
        /// </summary>
        public string LiabilityAccountName { get; set; }

        /// <summary>
        /// Gets or sets the Provider
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the federal tax description.
        /// </summary>
        /// <value>
        /// The federal tax description.
        /// </value>
        public string FederalTaxDescription { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contract concept dto.
        /// </summary>
        /// <value>
        /// The national fuel contract concept dto.
        /// </value>
        public IList<NationalFuelContractConceptDto> NationalFuelContractConcept { get; set; }
    }
}
