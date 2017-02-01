//------------------------------------------------------------------------
// <copyright file="NationalFuelContractSearchVO.cs" company="Volaris">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Web.Models.VO.Finance
{
    using System;

    /// <summary>
    /// Data Object of National Fuel Contract
    /// </summary>
    public class NationalFuelContractSearchVO
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
        /// Gets or sets the effective date.
        /// Primary key.
        /// </summary>
        /// <value>
        /// The effective date.
        /// </value>
        public string EffectiveDate { get; set; }

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
    }
}
