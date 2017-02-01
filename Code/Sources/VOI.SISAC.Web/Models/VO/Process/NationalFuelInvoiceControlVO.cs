//------------------------------------------------------------------------
// <copyright file="NationalFuelInvoiceControlVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using Newtonsoft.Json;
    using System;
    using VOI.SISAC.Web.Helpers;

    /// <summary>
    /// View object National jet fuel invoice control 
    /// </summary>
    public class NationalFuelInvoiceControlVO
    {
        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the remittance identifier.
        /// </summary>
        /// <value>
        /// The remittance identifier.
        /// </value>
        public string RemittanceId { get; set; }

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
        /// Gets or sets the name of the document status.
        /// </summary>
        /// <value>
        /// The name of the document status.
        /// </value>
        public string DocumentStatusName { get; set; }        

        /// <summary>
        /// Gets or sets the date baseline.
        /// </summary>
        /// <value>
        /// The date baseline.
        /// </value>
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? DateBaseline { get; set; }

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? DateValue { get; set; }

        /// <summary>
        /// Gets or sets the date posting.
        /// </summary>
        /// <value>
        /// The date posting.
        /// </value>
        [JsonConverter(typeof(JsonDateConverter))]
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
        /// Gets or sets the reconciliation status.
        /// </summary>
        /// <value>
        /// The reconciliation status.
        /// </value>
        public string ReconciliationStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the remittances status.
        /// </summary>
        /// <value>
        /// The remittances status.
        /// </value>
        public string RemittancesStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the remittance status.
        /// </summary>
        /// <value>
        /// The name of the remittance status.
        /// </value>
        public string RemittanceStatusName { get; set; }

        /// <summary>
        /// Gets or sets the invoice count.
        /// </summary>
        /// <value>
        /// The invoice count.
        /// </value>
        public int InvoiceCount { get; set; }
    }
}