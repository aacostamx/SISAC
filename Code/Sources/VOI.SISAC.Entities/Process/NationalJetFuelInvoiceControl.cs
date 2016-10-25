//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceControl.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;




    /// <summary>
    /// National Jet Fuel Invoice Control Entity
    /// </summary>
    [Table("Process.NationalJetFuelInvoiceControl")]
    public class NationalJetFuelInvoiceControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceControl"/> class.
        /// </summary>
        public NationalJetFuelInvoiceControl()
        {
            NationalJetFuelInvoiceDetails = new List<NationalJetFuelInvoiceDetail>();
            NationalJetFuelInvoicePolicies = new List<NationalJetFuelInvoicePolicy>();
        }

        /// <summary>
        /// Gets or sets the remittance identifier.
        /// </summary>
        /// <value>
        /// The remittance identifier.
        /// </value>
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string RemittanceID { get; set; }

        /// <summary>
        /// Gets or sets the month year.
        /// </summary>
        /// <value>
        /// The month year.
        /// </value>
        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string MonthYear { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        [Key]
        [Column(Order = 2)]
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
        [StringLength(8)]
        public string ProviderNumber { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel amount.
        /// </summary>
        /// <value>
        /// The jet fuel amount.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal? JetFuelAmount { get; set; }

        /// <summary>
        /// Gets or sets the freight amount.
        /// </summary>
        /// <value>
        /// The freight amount.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal? FreightAmount { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        /// <value>
        /// The discount amount.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the fueling amount.
        /// </summary>
        /// <value>
        /// The fueling amount.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal? FuelingAmount { get; set; }

        /// <summary>
        /// Gets or sets the subtotal amount.
        /// </summary>
        /// <value>
        /// The subtotal amount.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal? SubtotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the tax amount.
        /// </summary>
        /// <value>
        /// The tax amount.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal? TaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>
        /// The total amount.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the document status code.
        /// </summary>
        /// <value>
        /// The document status code.
        /// </value>
        [StringLength(4)]
        public string DocumentStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the date baseline.
        /// </summary>
        /// <value>
        /// The date baseline.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime? DateBaseline { get; set; }

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime? DateValue { get; set; }

        /// <summary>
        /// Gets or sets the date posting.
        /// </summary>
        /// <value>
        /// The date posting.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime? DatePosting { get; set; }

        /// <summary>
        /// Gets or sets the society.
        /// </summary>
        /// <value>
        /// The society.
        /// </value>
        [StringLength(30)]
        public string Society { get; set; }

        /// <summary>
        /// Gets or sets the process date.
        /// </summary>
        /// <value>
        /// The process date.
        /// </value>
        [Column(TypeName = "date")]
        public DateTime? ProcessDate { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation status code.
        /// </summary>
        /// <value>
        /// The reconciliation status code.
        /// </value>
        [StringLength(30)]
        public string ReconciliationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the remittance status code.
        /// </summary>
        /// <value>
        /// The remittance status code.
        /// </value>
        [StringLength(4)]
        public string RemittanceStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the count records.
        /// </summary>
        /// <value>
        /// The count records.
        /// </value>
        public int? CountRecords { get; set; }

        /// <summary>
        /// Gets or sets the count reconciled records.
        /// </summary>
        /// <value>
        /// The count reconciled records.
        /// </value>
        public int? CountReconciledRecords { get; set; }

        /// <summary>
        /// Gets or sets the reconciled subtotal amount.
        /// </summary>
        /// <value>
        /// The reconciled subtotal amount.
        /// </value>
        public decimal? ReconciledSubtotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the date reconciled.
        /// </summary>
        /// <value>
        /// The date reconciled.
        /// </value>
        public DateTime? DateReconciled { get; set; }

        /// <summary>
        /// Gets or sets the status process code.
        /// </summary>
        /// <value>
        /// The status process code.
        /// </value>
        [StringLength(5)]
        public string StatusProcessCode { get; set; }

        /// <summary>
        /// Gets or sets the process progress.
        /// </summary>
        /// <value>
        /// The process progress.
        /// </value>
        [Column(TypeName = "numeric")]
        public decimal? ProcessProgress { get; set; }

        /// <summary>
        /// Gets or sets the start date process.
        /// </summary>
        /// <value>
        /// The start date process.
        /// </value>
        public DateTime? StartDateProcess { get; set; }

        /// <summary>
        /// Gets or sets the end date process.
        /// </summary>
        /// <value>
        /// The end date process.
        /// </value>
        public DateTime? EndDateProcess { get; set; }

        /// <summary>
        /// Gets or sets the processed name of the by user.
        /// </summary>
        /// <value>
        /// The processed name of the by user.
        /// </value>
        [StringLength(50)]
        public string ProcessedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the calculation status code.
        /// </summary>
        /// <value>
        /// The calculation status code.
        /// </value>
        [StringLength(5)]
        public string CalculationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status code.
        /// </summary>
        /// <value>
        /// The confirmation status code.
        /// </value>
        [StringLength(5)]
        public string ConfirmationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the confirmation date.
        /// </summary>
        /// <value>
        /// The confirmation date.
        /// </value>
        public DateTime? ConfirmationDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the confirmed by user.
        /// </summary>
        /// <value>
        /// The name of the confirmed by user.
        /// </value>
        [StringLength(50)]
        public string ConfirmedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the records to process.
        /// </summary>
        /// <value>
        /// The records to process.
        /// </value>
        public int? RecordsToProcess { get; set; }

        /// <summary>
        /// Gets or sets the records processed.
        /// </summary>
        /// <value>
        /// The records processed.
        /// </value>
        public int? RecordsProcessed { get; set; }

        /// <summary>
        /// Gets or sets the total rows.
        /// </summary>
        /// <value>
        /// The total rows.
        /// </value>
        [NotMapped]
        public int TotalRows { get; set; }

        /// <summary>
        /// Gets or sets the type process.
        /// </summary>
        /// <value>
        /// The type process.
        /// </value>
        [NotMapped]
        public NationalReconcileTypeProcess TypeProcess { get; set; }

        /// <summary>
        /// Gets or sets the calculation status.
        /// </summary>
        /// <value>
        /// The calculation status.
        /// </value>
        public virtual CalculationStatus CalculationStatus { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status.
        /// </summary>
        /// <value>
        /// The confirmation status.
        /// </value>
        public virtual ConfirmationStatus ConfirmationStatus { get; set; }

        /// <summary>
        /// Gets or sets the document status.
        /// </summary>
        /// <value>
        /// The document status.
        /// </value>
        public virtual DocumentStatus DocumentStatus { get; set; }

        /// <summary>
        /// Gets or sets the remittance status.
        /// </summary>
        /// <value>
        /// The remittance status.
        /// </value>
        public virtual RemittanceStatus RemittanceStatus { get; set; }

        /// <summary>
        /// Gets or sets the status process.
        /// </summary>
        /// <value>
        /// The status process.
        /// </value>
        public virtual StatusProcess StatusProcess { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice detail.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice detail.
        /// </value>
        public virtual IList<NationalJetFuelInvoiceDetail> NationalJetFuelInvoiceDetails { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice policy.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice policy.
        /// </value>
        public virtual IList<NationalJetFuelInvoicePolicy> NationalJetFuelInvoicePolicies { get; set; }
    }
}
