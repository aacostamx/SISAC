//------------------------------------------------------------------------
// <copyright file="RemittanceSearchVO.cs" company="Volaris">
//     Copyright (c) Volaris. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Process
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    ///  RemittanceSearchVO Class
    /// </summary>
    public class RemittanceSearchVO
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
        [StringLength(8)]
        [Display(Name = "RemittanceId", ResourceType = typeof(Resources.Resource))]
        public string RemittanceId { get; set; }

        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        /// <value>
        /// The invoice number.
        /// </value>
        [StringLength(15)]
        [Display(Name = "InvoiceNumber", ResourceType = typeof(Resources.Resource))]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the station code.
        /// </summary>
        /// <value>
        /// The station code.
        /// </value>
        [Display(Name = "StationCode", ResourceType = typeof(Resources.Resource))]
        [StringLength(3, MinimumLength = 3, ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LengthMin3")]
        public string StationCode { get; set; }

        /// <summary>
        /// Gets or sets the month year.
        /// </summary>
        /// <value>
        /// The month year.
        /// </value>
        [Display(Name = "MonthYear", ResourceType = typeof(Resources.Resource))]
        public string MonthYear { get; set; }

        /// <summary>
        /// Gets or sets the period code.
        /// </summary>
        /// <value>
        /// The period code.
        /// </value>
        [StringLength(30)]
        [Display(Name = "PeriodRemittance", ResourceType = typeof(Resources.Resource))]
        public string Period { get; set; }
    }
}