//------------------------------------------------------------------------
// <copyright file="NationalJetFuelCost.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// NationalJetFuelCost Entity Class
    /// </summary>
    [Table("Process.NationalJetFuelCost")]
    public partial class NationalJetFuelCost
    {
        /// <summary>
        /// Gets or sets the cost identifier.
        /// </summary>
        /// <value>
        /// The cost identifier.
        /// </value>
        [Key]
        public long CostID { get; set; }

        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        [Required]
        [StringLength(30)]
        public string PeriodCode { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int Sequence { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [Required]
        [StringLength(3)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        [Required]
        [StringLength(5)]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the itinerary key.
        /// </summary>
        /// <value>
        /// The itinerary key.
        /// </value>
        [Required]
        [StringLength(8)]
        public string ItineraryKey { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        [Required]
        [StringLength(12)]
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the operation type identifier.
        /// </summary>
        /// <value>
        /// The operation type identifier.
        /// </value>
        public int OperationTypeID { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel ticket identifier.
        /// </summary>
        /// <value>
        /// The national jet fuel ticket identifier.
        /// </value>
        public long NationalJetFuelTicketID { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [Required]
        [StringLength(12)]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the provider number primary.
        /// </summary>
        /// <value>
        /// The provider number primary.
        /// </value>
        [Required]
        [StringLength(10)]
        public string ProviderNumberPrimary { get; set; }

        /// <summary>
        /// Gets or sets the fueling start date.
        /// </summary>
        /// <value>
        /// The fueling start date.
        /// </value>
        public DateTime FuelingStartDate { get; set; }

        /// <summary>
        /// Gets or sets the fueling start time.
        /// </summary>
        /// <value>
        /// The fueling start time.
        /// </value>
        public TimeSpan FuelingStartTime { get; set; }

        /// <summary>
        /// Gets or sets the fueling end date.
        /// </summary>
        /// <value>
        /// The fueling end date.
        /// </value>
        public DateTime FuelingEndDate { get; set; }

        /// <summary>
        /// Gets or sets the fueling end time.
        /// </summary>
        /// <value>
        /// The fueling end time.
        /// </value>
        public TimeSpan FuelingEndTime { get; set; }

        /// <summary>
        /// Gets or sets the apron position.
        /// </summary>
        /// <value>
        /// The apron position.
        /// </value>
        [StringLength(8)]
        public string ApronPosition { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        /// <value>
        /// The ticket number.
        /// </value>
        [Required]
        [StringLength(10)]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the fueled qty LTS.
        /// </summary>
        /// <value>
        /// The fueled qty LTS.
        /// </value>
        public int FueledQtyLts { get; set; }

        /// <summary>
        /// Gets or sets the remaining qty KGS.
        /// </summary>
        /// <value>
        /// The remaining qty KGS.
        /// </value>
        public int? RemainingQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the requested qty KGS.
        /// </summary>
        /// <value>
        /// The requested qty KGS.
        /// </value>
        public int? RequestedQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the fueled qty KGS.
        /// </summary>
        /// <value>
        /// The fueled qty KGS.
        /// </value>
        public int? FueledQtyKgs { get; set; }

        /// <summary>
        /// Gets or sets the density factor.
        /// </summary>
        /// <value>
        /// The density factor.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal? DensityFactor { get; set; }

        /// <summary>
        /// Gets or sets the national fuel contract concept identifier.
        /// </summary>
        /// <value>
        /// The national fuel contract concept identifier.
        /// </value>
        public long NationalFuelContractConceptID { get; set; }

        /// <summary>
        /// Gets or sets the fuel concept identifier.
        /// </summary>
        /// <value>
        /// The fuel concept identifier.
        /// </value>
        public long FuelConceptID { get; set; }

        /// <summary>
        /// Gets or sets the fuel concept type code.
        /// </summary>
        /// <value>
        /// The fuel concept type code.
        /// </value>
        [Required]
        [StringLength(5)]
        public string FuelConceptTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the charge factor type identifier.
        /// </summary>
        /// <value>
        /// The charge factor type identifier.
        /// </value>
        public int ChargeFactorTypeID { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        [Required]
        [StringLength(10)]
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the schedule type code.
        /// </summary>
        /// <value>
        /// The schedule type code.
        /// </value>
        [Required]
        [StringLength(3)]
        public string ScheduleTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>
        /// The rate.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the concept amount.
        /// </summary>
        /// <value>
        /// The concept amount.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal ConceptAmount { get; set; }

        /// <summary>
        /// Gets or sets the federal tax code.
        /// </summary>
        /// <value>
        /// The federal tax code.
        /// </value>
        [Required]
        [StringLength(8)]
        public string FederalTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the federal tax value.
        /// </summary>
        /// <value>
        /// The federal tax value.
        /// </value>
        public decimal FederalTaxValue { get; set; }

        /// <summary>
        /// Gets or sets the federal tax amount.
        /// </summary>
        /// <value>
        /// The federal tax amount.
        /// </value>
        public decimal FederalTaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the cc number.
        /// </summary>
        /// <value>
        /// The cc number.
        /// </value>
        [Required]
        [StringLength(12)]
        public string CCNumber { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        [Required]
        [StringLength(3)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the accounting account number.
        /// </summary>
        /// <value>
        /// The accounting account number.
        /// </value>
        [Required]
        [StringLength(8)]
        public string AccountingAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the liability account number.
        /// </summary>
        /// <value>
        /// The liability account number.
        /// </value>
        [Required]
        [StringLength(8)]
        public string LiabilityAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the policy identifier.
        /// </summary>
        /// <value>
        /// The policy identifier.
        /// </value>
        public long? PolicyID { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation status.
        /// </summary>
        /// <value>
        /// The reconciliation status.
        /// </value>
        [StringLength(10)]
        public string ReconciliationStatus { get; set; }

        /// <summary>
        /// Gets or sets the invoice detail identifier.
        /// </summary>
        /// <value>
        /// The invoice detail identifier.
        /// </value>
        public long? InvoiceDetailID { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel process.
        /// </summary>
        /// <value>
        /// The national jet fuel process.
        /// </value>
        public virtual NationalJetFuelProcess NationalJetFuelProcess { get; set; }
    }

}