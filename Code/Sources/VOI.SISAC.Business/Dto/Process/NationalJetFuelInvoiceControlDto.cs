//------------------------------------------------------------------------
// <copyright file="NationalJetFuelInvoiceControlDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using Enums;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Data transfer object for National jet fuel invoice control
    /// </summary>
    public class NationalJetFuelInvoiceControlDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NationalJetFuelInvoiceControlDto"/> class.
        /// </summary>
        public NationalJetFuelInvoiceControlDto()
        {
            NationalJetFuelInvoiceDetails = new List<NationalJetFuelInvoiceDetailDto>();
            NationalJetFuelInvoicePolicies = new List<NationalJetFuelInvoicePolicyDto>();
        }

        /// <summary>
        /// Gets or sets the remittance identifier.
        /// </summary>
        /// <value>
        /// The remittance identifier.
        /// </value>
        public string RemittanceID { get; set; }

        /// <summary>
        /// Gets or sets the month year.
        /// </summary>
        /// <value>
        /// The month year.
        /// </value>
        public string MonthYear { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        /// <value>
        /// The period.
        /// </value>
        public string Period { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the provider number.
        /// </summary>
        /// <value>
        /// The provider number.
        /// </value>
        public string ProviderNumber { get; set; }

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
        /// Gets or sets the document status code.
        /// </summary>
        /// <value>
        /// The document status code.
        /// </value>
        public string DocumentStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the date baseline.
        /// </summary>
        /// <value>
        /// The date baseline.
        /// </value>
        public DateTime? DateBaseline { get; set; }

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        public DateTime? DateValue { get; set; }

        /// <summary>
        /// Gets or sets the date posting.
        /// </summary>
        /// <value>
        /// The date posting.
        /// </value>
        public DateTime? DatePosting { get; set; }

        /// <summary>
        /// Gets or sets the society.
        /// </summary>
        /// <value>
        /// The society.
        /// </value>
        public string Society { get; set; }

        /// <summary>
        /// Gets or sets the process date.
        /// </summary>
        /// <value>
        /// The process date.
        /// </value>
        public DateTime? ProcessDate { get; set; }

        /// <summary>
        /// Gets or sets the reconciliation status code.
        /// </summary>
        /// <value>
        /// The reconciliation status code.
        /// </value>
        public string ReconciliationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the remittance status code.
        /// </summary>
        /// <value>
        /// The remittance status code.
        /// </value>
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
        public string StatusProcessCode { get; set; }

        /// <summary>
        /// Gets or sets the process progress.
        /// </summary>
        /// <value>
        /// The process progress.
        /// </value>
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
        public string ProcessedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the calculation status code.
        /// </summary>
        /// <value>
        /// The calculation status code.
        /// </value>
        public string CalculationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status code.
        /// </summary>
        /// <value>
        /// The confirmation status code.
        /// </value>
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
        /// Gets or sets the count nonconformity records.
        /// </summary>
        /// <value>
        /// The count nonconformity records.
        /// </value>
        public int? CountNonconformityRecords { get; set; }

        /// <summary>
        /// Gets or sets the nonconformity subtotal amount.
        /// </summary>
        /// <value>
        /// The nonconformity subtotal amount.
        /// </value>
        public decimal? NonconformitySubtotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the date nonconformity.
        /// </summary>
        /// <value>
        /// The date nonconformity.
        /// </value>
        public DateTime? DateNonconformity { get; set; }

        /// <summary>
        /// Gets or sets the nonconformity reference.
        /// </summary>
        /// <value>
        /// The nonconformity reference.
        /// </value>
        public string NonconformityReference { get; set; }

        /// <summary>
        /// Gets or sets the nonconfirmity status code.
        /// </summary>
        /// <value>
        /// The nonconfirmity status code.
        /// </value>
        public string NonconformityStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the total rows.
        /// </summary>
        /// <value>
        /// The total rows.
        /// </value>
        public int TotalRows { get; set; }

        /// <summary>
        /// Gets or sets the invoice count.
        /// </summary>
        /// <value>
        /// The invoice count.
        /// </value>
        public int InvoiceCount { get; set; }

        /// <summary>
        /// Gets or sets the name of the document status.
        /// </summary>
        /// <value>
        /// The name of the document status.
        /// </value>
        public string DocumentStatusName { get; set; }

        /// <summary>
        /// Gets or sets the name of the remittance status.
        /// </summary>
        /// <value>
        /// The name of the remittance status.
        /// </value>
        public string RemittanceStatusName { get; set; }

        /// <summary>
        /// Gets or sets the type process.
        /// </summary>
        /// <value>
        /// The type process.
        /// </value>
        public NationalReconcileTypeProcessDto TypeProcess { get; set; }

        /// <summary>
        /// Gets or sets the calculation status.
        /// </summary>
        /// <value>
        /// The calculation status.
        /// </value>
        public CalculationStatusDto CalculationStatus { get; set; }

        /// <summary>
        /// Gets or sets the confirmation status.
        /// </summary>
        /// <value>
        /// The confirmation status.
        /// </value>
        public ConfirmationStatusDto ConfirmationStatus { get; set; }

        /// <summary>
        /// Gets or sets the document status.
        /// </summary>
        /// <value>
        /// The document status.
        /// </value>
        public DocumentStatusDto DocumentStatus { get; set; }

        /// <summary>
        /// Gets or sets the remittance status.
        /// </summary>
        /// <value>
        /// The remittance status.
        /// </value>
        public RemittanceStatusDto RemittanceStatus { get; set; }

        /// <summary>
        /// Gets or sets the status process.
        /// </summary>
        /// <value>
        /// The status process.
        /// </value>
        public StatusProcessDto StatusProcess { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice details.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice details.
        /// </value>
        public IList<NationalJetFuelInvoiceDetailDto> NationalJetFuelInvoiceDetails { get; set; }

        /// <summary>
        /// Gets or sets the national jet fuel invoice policies.
        /// </summary>
        /// <value>
        /// The national jet fuel invoice policies.
        /// </value>
        public IList<NationalJetFuelInvoicePolicyDto> NationalJetFuelInvoicePolicies { get; set; }
    }
}
