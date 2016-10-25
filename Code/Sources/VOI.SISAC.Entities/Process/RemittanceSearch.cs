//------------------------------------------------------------------------
// <copyright file="RemittanceSearch.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Entities.Process
{

    using System;

    /// <summary>
    /// RemittanceSearch Class
    /// </summary>
    public class RemittanceSearch
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
        /// Gets or sets the invoice number.
        /// </summary>
        /// <value>
        /// The invoice number.
        /// </value>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the month year.
        /// </summary>
        /// <value>
        /// The month year.
        /// </value>
        public string MonthYear { get; set; }

        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        public string Period { get; set; }
    }
}
