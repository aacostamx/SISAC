//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceDetail.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// National Jet Fuel Invoice Detail Entity
    /// </summary>
    [Table("Process.NationalJetFuelInvoiceDetail")]
    public partial class NationalJetFuelInvoiceDetail
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the remittance identifier.
        /// </summary>
        /// <value>
        /// The remittance identifier.
        /// </value>
        [StringLength(8)]
        public string RemittanceID { get; set; }

        /// <summary>
        /// Gets or sets the month year.
        /// </summary>
        /// <value>
        /// The month year.
        /// </value>
        [StringLength(4)]
        public string MonthYear { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        [StringLength(5)]
        public string Period { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        [StringLength(3)]
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        [StringLength(50)]
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the service code.
        /// </summary>
        /// <value>
        /// The service code.
        /// </value>
        [StringLength(50)]
        public string ServiceCode { get; set; }

        /// <summary>
        /// Gets or sets the federal tax code.
        /// </summary>
        /// <value>
        /// The federal tax code.
        /// </value>
        [StringLength(50)]
        public string FederalTaxCode { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        [StringLength(50)]
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        /// <value>
        /// The invoice number.
        /// </value>
        [StringLength(255)]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the customer number.
        /// </summary>
        /// <value>
        /// The customer number.
        /// </value>
        [StringLength(50)]
        public string CustomerNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        [StringLength(255)]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the invoice date.
        /// </summary>
        /// <value>
        /// The invoice date.
        /// </value>
        public DateTime? InvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets the electronic invoice number.
        /// </summary>
        /// <value>
        /// The electronic invoice number.
        /// </value>
        [StringLength(500)]
        public string ElectronicInvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the electronic invoice date.
        /// </summary>
        /// <value>
        /// The electronic invoice date.
        /// </value>
        public DateTime? ElectronicInvoiceDate { get; set; }

        /// <summary>
        /// Gets or sets the product number.
        /// </summary>
        /// <value>
        /// The product number.
        /// </value>
        [StringLength(50)]
        public string ProductNumber { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        /// <value>
        /// The product description.
        /// </value>
        [StringLength(255)]
        public string ProductDescription { get; set; }

        /// <summary>
        /// Gets or sets the ticket number.
        /// </summary>
        /// <value>
        /// The ticket number.
        /// </value>
        [StringLength(255)]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the operation.
        /// </summary>
        /// <value>
        /// The type of the operation.
        /// </value>
        [StringLength(255)]
        public string OperationType { get; set; }

        /// <summary>
        /// Gets or sets the flight number.
        /// </summary>
        /// <value>
        /// The flight number.
        /// </value>
        [StringLength(100)]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        [StringLength(100)]
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the airplane model.
        /// </summary>
        /// <value>
        /// The airplane model.
        /// </value>
        [StringLength(100)]
        public string AirplaneModel { get; set; }

        /// <summary>
        /// Gets or sets the start date time.
        /// </summary>
        /// <value>
        /// The start date time.
        /// </value>
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the end date time.
        /// </summary>
        /// <value>
        /// The end date time.
        /// </value>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Gets or sets the start meter.
        /// </summary>
        /// <value>
        /// The start meter.
        /// </value>
        public decimal? StartMeter { get; set; }

        /// <summary>
        /// Gets or sets the end meter.
        /// </summary>
        /// <value>
        /// The end meter.
        /// </value>
        public decimal? EndMeter { get; set; }

        /// <summary>
        /// Gets or sets the volume m3.
        /// </summary>
        /// <value>
        /// The volume m3.
        /// </value>
        public decimal? VolumeM3 { get; set; }

        /// <summary>
        /// Gets or sets the type of the rate.
        /// </summary>
        /// <value>
        /// The type of the rate.
        /// </value>
        [StringLength(255)]
        public string RateType { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel amount.
        /// </summary>
        /// <value>
        /// The jet fuel amount.
        /// </value>
        public decimal? JetFuelAmount { get; set; }

        /// <summary>
        /// Gets or sets the freight amount.
        /// </summary>
        /// <value>
        /// The freight amount.
        /// </value>
        public decimal? FreightAmount { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        /// <value>
        /// The discount amount.
        /// </value>
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the fueling amount.
        /// </summary>
        /// <value>
        /// The fueling amount.
        /// </value>
        public decimal? FuelingAmount { get; set; }

        /// <summary>
        /// Gets or sets the subtotal amount.
        /// </summary>
        /// <value>
        /// The subtotal amount.
        /// </value>
        public decimal? SubtotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the tax amount.
        /// </summary>
        /// <value>
        /// The tax amount.
        /// </value>
        public decimal? TaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>
        /// The total amount.
        /// </value>
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        [StringLength(5000)]
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [StringLength(20)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation status.
        /// </summary>
        /// <value>
        /// The reconciliation status.
        /// </value>
        [StringLength(10)]
        public string ReconciliationStatus { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice control.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice control.
        /// </value>
        public virtual NationalJetFuelInvoiceControl NationalJetFuelInvoiceControl { get; set; }
    }
}
