﻿//------------------------------------------------------------------------
// <copyright file="JetFuelPolicyControlDto.cs" company="AACOSTA">
//     Copyright (c) http://aacosta.com.mx/ All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace VOI.SISAC.Business.Dto.Process
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// JetFuelPolicyControlDto Class
    /// </summary>
    public class JetFuelPolicyControlDto
    {
        /// <summary>
        /// Gets or sets the policy identifier.
        /// </summary>
        /// <value>
        /// The policy identifier.
        /// </value>
        public long PolicyId { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the airline code.
        /// </summary>
        /// <value>
        /// The airline code.
        /// </value>
        public string AirlineCode { get; set; }

        /// <summary>
        /// Gets or sets the service codes.
        /// </summary>
        /// <value>
        /// The service codes.
        /// </value>
        public string ServiceCodes { get; set; }

        /// <summary>
        /// Gets or sets the provider codes.
        /// </summary>
        /// <value>
        /// The provider codes.
        /// </value>
        public string ProviderCodes { get; set; }

        /// <summary>
        /// Gets or sets the airport codes.
        /// </summary>
        /// <value>
        /// The airport codes.
        /// </value>
        public string AirportCodes { get; set; }

        /// <summary>
        /// Gets or sets the start date real.
        /// </summary>
        /// <value>
        /// The start date real.
        /// </value>
        public DateTime StartDateReal { get; set; }

        /// <summary>
        /// Gets or sets the end date real.
        /// </summary>
        /// <value>
        /// The end date real.
        /// </value>
        public DateTime EndDateReal { get; set; }

        /// <summary>
        /// Gets or sets the start date complentary.
        /// </summary>
        /// <value>
        /// The start date complentary.
        /// </value>
        public DateTime StartDateComplementary { get; set; }

        /// <summary>
        /// Gets or sets the end date complementary.
        /// </summary>
        /// <value>
        /// The end date complementary.
        /// </value>
        public DateTime EndDateComplementary { get; set; }

        /// <summary>
        /// Gets or sets the date baseline.
        /// </summary>
        /// <value>
        /// The date baseline.
        /// </value>
        public DateTime DateBaseline { get; set; }

        /// <summary>
        /// Gets or sets the date value.
        /// </summary>
        /// <value>
        /// The date value.
        /// </value>
        public DateTime DateValue { get; set; }

        /// <summary>
        /// Gets or sets the date posting.
        /// </summary>
        /// <value>
        /// The date posting.
        /// </value>
        public DateTime DatePosting { get; set; }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>
        /// The header text.
        /// </value>
        public string HeaderText { get; set; }

        /// <summary>
        /// Gets or sets the item text.
        /// </summary>
        /// <value>
        /// The item text.
        /// </value>
        public string ItemText { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string SendByUserName { get; set; }

        /// <summary>
        /// Gets or sets the name of the confirmed by user.
        /// </summary>
        /// <value>
        /// The name of the confirmed by user.
        /// </value>
        public string ConfirmedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the total process.
        /// </summary>
        /// <value>
        /// The total process.
        /// </value>
        public int TotalProcess { get; set; }

        /// <summary>
        /// Gets or sets the total sucess.
        /// </summary>
        /// <value>
        /// The total sucess.
        /// </value>
        public int TotalSucess { get; set; }

        /// <summary>
        /// Gets or sets the total errors.
        /// </summary>
        /// <value>
        /// The total errors.
        /// </value>
        public int TotalErrors { get; set; }

        /// <summary>
        /// Gets or sets the jet fuel policies.
        /// </summary>
        /// <value>
        /// The jet fuel policies.
        /// </value>
        public IList<JetFuelPolicyDto> JetFuelPolicies { get; set; }

    }
}
