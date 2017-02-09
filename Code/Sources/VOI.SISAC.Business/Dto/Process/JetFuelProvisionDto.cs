//------------------------------------------------------------------------
// <copyright file="JetFuelProvisionDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using System;

    /// <summary>
    /// JetFuelProvisionDto
    /// </summary>
    public class JetFuelProvisionDto
    {
        /// <summary>
        /// Gets or sets the provision identifier.
        /// </summary>
        /// <value>
        /// The provision identifier.
        /// </value>
        public long ProvisionId { get; set; }

        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        public string PeriodCode { get; set; }

        /// <summary>
        /// Gets or sets the itinerary sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the itinerary airline code.
        /// </summary>
        /// <value>
        /// The itinerary airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the itinerary flight number.
        /// </summary>
        /// <value>
        /// The itinerary flight number.
        /// </value>
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the airplane equipment number.
        /// </summary>
        /// <value>
        /// The airplane equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        public int OperationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel ticked identifier.
        /// </summary>
        /// <value>
        /// The jet fuel ticked identifier.
        /// </value>
        public long JetFuelTicketID { get; set; }

        /// <summary>
        /// Gets or sets the fueling date.
        /// </summary>
        /// <value>
        /// The fueling date.
        /// </value>
        public DateTime FuelingDate { get; set; }

        /// <summary>
        /// Gets or sets the fueling time.
        /// </summary>
        /// <value>
        /// The fueling time.
        /// </value>
        public TimeSpan FuelingTime { get; set; }

        /// <summary>
        /// Gets or sets the apron position.
        /// </summary>
        /// <value>
        /// The apron position.
        /// </value>
        public string ApronPosition { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        /// <value>
        /// The ticket number.
        /// </value>
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the fueled quantity gallon.
        /// </summary>
        /// <value>
        /// The fueled quantity gallon.
        /// </value>
        public int FueledQuantityGallon { get; set; }

        /// <summary>
        /// Gets or sets the remaining quantity on kilo grams.
        /// </summary>
        /// <value>
        /// The remaining quantity.
        /// </value>
        public int? RemainingQuantityKiloGram { get; set; }

        /// <summary>
        /// Gets or sets the requested quantity on kilo grams.
        /// </summary>
        /// <value>
        /// The requested quantity.
        /// </value>
        public int? RequestedQuantityKiloGram { get; set; }

        /// <summary>
        /// Gets or sets the fueled quantity on kilo grams.
        /// </summary>
        /// <value>
        /// The fueled quantity.
        /// </value>
        public int? FueledQuantityKiloGram { get; set; }

        /// <summary>
        /// Gets or sets the density factor.
        /// </summary>
        /// <value>
        /// The density factor.
        /// </value>
        public decimal? DensityFactor { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the provider number primary.
        /// </summary>
        /// <value>
        /// The provider number primary.
        /// </value>
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the cost center number.
        /// </summary>
        /// <value>
        /// The cost center number.
        /// </value>
        public string CostCenterNumber { get; set; }

        /// <summary>
        /// Gets or sets the accounting account number.
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
        /// Gets or sets the international fuel contract concept identifier.
        /// </summary>
        /// <value>
        /// The international fuel contract concept identifier.
        /// </value>
        public long InternationalFuelContractConceptId { get; set; }

        /// <summary>
        /// Gets or sets the fuel concept identifier.
        /// </summary>
        /// <value>
        /// The fuel concept identifier.
        /// </value>
        public long FuelConceptId { get; set; }

        /// <summary>
        /// Gets or sets the fuel concept type code.
        /// </summary>
        /// <value>
        /// The fuel concept type code.
        /// </value>
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the charge factor type identifier.
        /// </summary>
        /// <value>
        /// The charge factor type identifier.
        /// </value>
        public int ChargeFactorTypeId { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the concept amount.
        /// </summary>
        /// <value>
        /// The concept amount.
        /// </value>
        public decimal ConceptAmount { get; set; }

        /// <summary>
        /// Gets or sets the policy identifier.
        /// </summary>
        /// <value>
        /// The policy identifier.
        /// </value>
        public long? PolicyID { get; set; }

        /////// <summary>
        /////// Gets or sets the jet fuel policy control.
        /////// </summary>
        /////// <value>
        /////// The jet fuel policy control.
        /////// </value>
        //public JetFuelPolicyControlDto JetFuelPolicyControl { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel process.
        /// </summary>
        /// <value>
        /// The jet fuel process.
        /// </value>
        public JetFuelProcessDto JetFuelProcess { get; set; }
    }
}
